Properties {
    $BaseDir = resolve-path ..
    $SolutionFile = "$BaseDir\Essential Templating.sln"
    $BuildDir = "$BaseDir\build"
    $SrcDir = "$BaseDir\src"
    $TestDir = "$BaseDir\test"
    $DeployDir = "$BaseDir\build\package" 

    $PackageName = "Essential.Templating"
    $Configuration = "Release"
    $Frameworks = @("net45")

    $ToolsDir = "$BaseDir\tools"
    $NuGetFile = "$ToolsDir\nuget\nuget.exe"
    $XUnitFile = "$BaseDir\packages\xunit.runner.console.2.4.1\tools\net471\xunit.console.exe"
    $Packages = @{
        "Common" = @{ Name = "Essential.Templating.Common" };
        "Razor" = @{ Name = "Essential.Templating.Razor" };
        "Razor.Email" = @{ Name = "Essential.Templating.Razor.Email" }
    }
    if (!$Targets){
        $Targets = $("Common", "Razor", "Razor.Email")
    }
}

FormatTaskName {
    Param($taskName)
    Write-Host "`r`nExecuting [$taskName] ..." -ForegroundColor Blue -BackgroundColor DarkYellow
}

Task Default -Depends Package

Task Publish -Depends Package {
    $apiKey = Read-Host NuGet api key?
    foreach ($target in $Targets){
        $package = $Packages.Get_Item($target)
        try {
            [Xml]$nuspec = Get-Content $DeployDir\$($package.Name)\$($package.Name).nuspec
            $packageFile = "$DeployDir\$($package.Name)\$($package.Name).$($nuspec.package.metadata.version).nupkg"
            Exec { &$NuGetFile push $packageFile $apiKey }
            Log-Message "Package $($package.Name) was successfully published." Success
        } catch [Exception] {
            Log-Message "Failed to publish a package: $($_.Exception.Message)" Error
        }           
    }
}

Task Package -Depends Clean, Versioning, Test {
    foreach ($target in $Targets){
        $package = $Packages.Get_Item($target)
        $packageDir = "$DeployDir\$($package.Name)"
        New-Item $packageDir\$($package.Name).nuspec -Force -ItemType File | Out-Null
        Copy-Item -Path $BuildDir\$($package.Name).nuspec.tmpl -Destination $packageDir\$($package.Name).nuspec -Force -Recurse
        $frameworkGroups = Get-ChildItem -Path $SrcDir -Include @("$($package.Name)*.dll", "$($package.Name)*.xml", "$($package.Name)*.pdb") -Recurse |
            where FullName -like "*\$($package.Name)\bin\$Configuration\*" |
            group { $_.Directory.Name }
        Copy-AsNuGetStructure $packageDir\lib $frameworkGroups
        Exec { &$NuGetFile pack $packageDir\$($package.Name).nuspec -OutputDirectory $packageDir }
        Log-Message "Package $($package.Name) was successfully created." Success
    }
}

Task Versioning {
    foreach ($target in $Targets){
        $package = $Packages.Get_Item($target)
        $packageName = $package.Name
        [Xml]$nuspec = Get-Content $BuildDir\$packageName.nuspec.tmpl
        Log-Message "Changing [$packageName] assemblies version to $($nuspec.package.metadata.version)."
        Get-ChildItem $SrcDir -Include "AssemblyInfo.cs" -Recurse |
            where FullName -Like "*$packageName*" |
            foreach { Update-AssemblyInfo $_ $nuspec}     
    } 
}

Task Test -Depends Compile {
    Set-Location $BaseDir
    foreach ($target in $Targets){
        try {
            $package = $Packages.Get_Item($target)
            $testAssemblies = Get-ChildItem -Path $TestDir -Include "*.Tests.dll" -Recurse |
                Where-Object FullName -Like "*bin\$Configuration\*" |
                Where-Object Name -Like "$($package.Name).Tests.dll" |
                Select-Object -ExpandProperty FullName
            $msTestParams = $testAssemblies -join " "
            Exec { & $XUnitFile $msTestParams }
            Log-Message "All tests for [$($package.Name)] passed." Success
        }
        catch [Exception] {
            Log-Message "Some tests failed for [$($package.Name)]." Error
            throw;
        }
    }
    Set-Location $BuildDir
}

Task Compile -Depends RestorePackages {
    foreach ($target in $Targets){
        try {
            $package = $Packages.Get_Item($target)
            $projectPath = "$SrcDir\$($package.Name)\$($package.Name).csproj";
            $testPath = "$TestDir\$($package.Name).Tests\$($package.Name).Tests.csproj";
            if (Test-Path $testPath){
                Exec { msbuild $testPath /t:Rebuild /v:minimal /p:Configuration=$Configuration }  
            } else {
                Exec { msbuild $projectPath /t:Rebuild /v:minimal /p:Configuration=$Configuration } 
            }            
            Log-Message "Compilation of [$($package.Name)] was successful." Success
        } catch [Exception] {
            Log-Message "Compilation of [$($package.Name)] completed with errors" Error
            throw;
        }
    }
}

Task RestorePackages {
   Exec { & "$NuGetFile" restore "$SolutionFile" }
}

Task Clean {
    if (Test-Path $DeployDir)
    {
        Remove-Item $DeployDir -Force -Recurse
    }
    if (Test-Path "$BaseDir\TestResults")
    {
        Remove-Item "$BaseDir\TestResults" -Force -Recurse
    }
    New-Item -ItemType directory -Path $DeployDir | Out-Null
    Log-Message "Created deploy directory: $DeployDir" Info
    Exec { msbuild $SolutionFile /target:Clean /property:Configuration=$Configuration /verbosity:quiet }
    Log-Message "Performed clean on VS solution." Info
}

Function Log-Message($message, [String]$type = "Info")
{
    $msgColor = switch -regex ($type)
    {
        "[Ii]nfo"    { "Magenta" }
        "[Ww]arning" { "Yellow" }
        "[Ee]rror"   { "Red" }
        "[Ss]uccess" { "Green" }
    }
    Write-Host $message -ForegroundColor $msgColor -BackgroundColor Black
}

Function Copy-AsNuGetStructure([String]$directory, $frameworkGroups)
{
    foreach ($frameworkGroup in $frameworkGroups)
    {
        foreach($packageFile in $frameworkGroup.Group)
        {
            $targetFileName = "$directory\$($frameworkGroup.Name)\$($packageFile.Name)"
            if (!(Test-Path $targetFileName)){
                New-Item $targetFileName -ItemType File -Force | Out-Null
            }
            Copy-Item -Path $($packageFile.FullName) -Destination $targetFileName
        }
    }
}

Function Update-AssemblyInfo([String]$path, $nuspec)
{
    $assemblyVersionPattern = 'AssemblyVersion\(.*\)'
    $fileVersionPattern = 'AssemblyFileVersion\(.*\)'

    $versionString = Extract-Version $($nuspec.package.metadata.version)
    $assemblyVersion = "AssemblyVersion(`"$versionString`")"
    $fileVersion = "AssemblyFileVersion(`"$versionString`")"

    Get-Content $path | ForEach-Object {
            % {$_ -replace $assemblyVersionPattern, $assemblyVersion } |
            % {$_ -replace $fileVersionPattern, $fileVersion }
        } | Set-Variable -Name assemblyInfo
    Set-Content $path $assemblyInfo -Encoding UTF8
}

Function Extract-Version([String]$versionString)
{
    $versionString -match "\d{1,2}\.\d{1,2}\.\d{1,2}(\.\d{1,2})?" | Out-Null
    return $Matches[0]
}


