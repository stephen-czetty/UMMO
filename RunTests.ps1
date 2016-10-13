Param(
    [Switch]$Report
)

$msbuild = Join-Path -Path (Get-ItemProperty HKLM:/software/Microsoft/MSBuild/ToolsVersions/14.0).MSBuildToolsPath -ChildPath "MSBuild.exe"
$solution = Get-ChildItem -Recurse -Filter "UMMO.sln" | Select-Object -First 1 | %{ $_.FullName }

$projectPath = Get-ChildItem -Recurse -Filter "UMMO.Extensions.Specs"
$projectJson = Join-Path $projectPath.FullName "project.json"
$projectData = Get-Content $projectJson | ConvertFrom-Json
$projectVersion = $projectData.tools.'Machine.Specifications.Runner.Console'
$mspecRunner = Join-Path (Join-Path (Join-Path $env:HOME ".nuget\packages\Machine.Specifications.Runner.Console") $projectVersion) "tools/mspec-clr4.exe"

$reportFile = New-TemporaryFile | Rename-Item -NewName { $_ -replace 'tmp$', 'html' } -PassThru

& 'C:\Program Files\dotnet\dotnet.exe' restore
& $msbuild /m $solution

$specDlls = Get-ChildItem -recurse -filter "Debug" | Get-ChildItem -recurse -filter "*.Specs.dll" | %{ $_.FullName }

& $mspecRunner --html $reportFile.FullName $specDlls

if ($Report) {
    & $reportFile.FullName
}