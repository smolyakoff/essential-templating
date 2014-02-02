Import-Module ..\tools\psake\psake.psm1
Invoke-Psake -Properties @{ Targets = @("Common", "Razor") }