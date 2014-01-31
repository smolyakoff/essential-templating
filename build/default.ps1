Properties {
    $BaseDir = resolve-path ..
    $SolutionFile = "$BaseDir\Essential Templating.sln"
    $BuildDir = "$BaseDir\build"
    $SrcDir = "$BaseDir\src"
    $TestDir = "$BaseDir\test"
    $PackageDir = "$BaseDir\build\package" 

    $PackageName = "Essential.Templating"
    $Configuration = "Release"
    $Frameworks = @("net45")

    $ToolsDir = "$BaseDir\tools"
    $NuGetFile = "$ToolsDir\nuget\nuget.exe"
    $MsTestFile = "C:\Program Files (x86)\Microsoft Visual Studio 12.0\Common7\IDE\mstest.exe"
    $CurrentVersion = ""
}

FormatTaskName {
    Param($taskName)
    Write-Host "`r`nExecuting [$taskName] ..." -ForegroundColor Blue -BackgroundColor DarkYellow
}

Task Default -Depends Package

Task Publish -Depends Package {
    try {
            [Xml]$nuspec = Get-Content $PackageDir\$PackageName.nuspec
        $packageFile = "$PackageDir\$PackageName.$($nuspec.package.metadata.version).nupkg"
        $apiKey = Read-Host NuGet api key?
        Exec { &$NuGetFile push $packageFile $apiKey }
        Log-Message "Package was successfully published." Success
    } catch [Exception] {
        Log-Message "Failed to publish a package: $($_.Exception.Message)" Error
    }
}

Task Package -Depends Versioning, Test {
    New-Item $PackageDir\$PackageName.nuspec -Force -ItemType File | Out-Null
    Copy-Item -Path $BuildDir\$PackageName.nuspec.tmpl -Destination $PackageDir\$PackageName.nuspec -Force -Recurse
    Create-NuGetPackageStructure $PackageDir $Frameworks
    $frameworkGroups = Get-Files @("$PackageName*.dll", "$PackageName*.xml", "$PackageName*.pdb") $SrcDir $Configuration | Group-Object { $_.Directory.Name }
    Copy-AsNuGetStructure $PackageDir\lib $frameworkGroups
    Exec { &$NuGetFile pack $PackageDir\$PackageName.nuspec -OutputDirectory $PackageDir }
    Log-Message "Package was successfully created." Success
}

Task Versioning {
    [Xml]$nuspec = Get-Content $BuildDir\$PackageName.nuspec.tmpl
    Log-Message "Changing assemblies version to $($nuspec.package.metadata.version)."
    Get-ChildItem $SrcDir -Include "AssemblyInfo.cs" -Recurse | ForEach-Object { Update-AssemblyInfo $_ $nuspec} 
}

Task Test -Depends Compile {
    try {
        Set-Location $BaseDir
        $msTestParams = ""
        Get-Files "*.Tests.dll" $TestDir $Configuration | ForEach-Object { $msTestParams = "`"/testcontainer:$_`" $msTestParams" }
        Exec { &$MsTestFile $msTestParams }
        Set-Location $BuildDir
        Log-Message "All Tests Passed" Success
    } catch [Exception] {
        Log-Message "Some tests failed." Error
    }
}

Task Compile {
    try {
        Exec { msbuild $SolutionFile /t:Build /v:minimal /p:Configuration=$Configuration }
        Log-Message "Compilation was successful." Success
    } catch [Exception] {
        Log-Message "Compilation completed with errors" Error
    }
}

Task Clean {
    if (Test-Path $PackageDir)
    {
        Remove-Item $PackageDir -Force -Recurse
    }
    if (Test-Path "$BaseDir\TestResults")
    {
        Remove-Item "$BaseDir\TestResults" -Force -Recurse
    }
    New-Item -ItemType directory -Path $PackageDir | Out-Null
    Log-Message "Created distro directory: $PackageDir" Info
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

Function Get-Files([String[]]$patterns, [String]$directory, [String]$configuration)
{
    return Get-ChildItem $directory -Recurse -Include $patterns | Where-Object FullName -Like "*\bin\Release\*"
}

Function Create-NuGetPackageStructure([String]$directory, [String[]] $frameworks)
{
    foreach ($framework in $frameworks)
    {
        New-Item -ItemType Directory -Path $directory\lib\$framework -Force | Out-Null
    }
}

Function Copy-AsNuGetStructure([String]$directory, $frameworkGroups)
{
    foreach ($frameworkGroup in $frameworkGroups)
    {
        $framework = $frameworkGroup.Name
        foreach($packageFile in $frameworkGroup.Group)
        {
            Copy-Item -Path $($packageFile.FullName) -Destination $directory\$framework\$($packageFile.Name) -Force
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
    Set-Content $path $assemblyInfo
}

Function Extract-Version([String]$versionString)
{
    $versionString -match "\d{1,2}\.\d{1,2}\.\d{1,2}" | Out-Null
    return $Matches[0]
}


