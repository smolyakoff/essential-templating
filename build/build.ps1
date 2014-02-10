Param
(
    [String] $TaskName = "Package"
)
Import-Module ..\tools\psake\psake.psm1
Invoke-Psake -Task $TaskName -Properties @{ Targets = @("Common", "Razor", "Razor.Email") }
Write-Host `r`nPress any key to continue... -BackgroundColor Blue
try 
{
    $host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown") | Out-Null
}
catch [Exception]
{
}
