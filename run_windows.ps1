# If you can't execute powershell scripts you need to change execution policies. 
# Set-ExecutionPolicy Unrestricted

$directories_to_delete = @(
    'MAUIImageBenchmarks/bin'
    'MAUIImageBenchmarks/obj'
    '~/.nuget/packages/sixlabors.imagesharp/0.0.1'
)

foreach ($directory_to_delete in $directories_to_delete)
{
    if (Test-Path -Path $directory_to_delete -PathType Container)
    {
        Remove-Item -Path $directory_to_delete -Recurse -Force
    }
}

$configuration = "Release"
$runtime_identifier = "win10-x64"
$osArchitecture = (Get-CimInstance Win32_operatingsystem).OSArchitecture
if ($osArchitecture.Contains("ARM 64"))
{
    $runtime_identifier = "win10-arm64"
}

dotnet publish `
    --framework net9.0-windows10.0.19041.0 `
    --configuration $configuration `
    -p:RuntimeIdentifierOverride=$runtime_identifier `
    -p:WindowsPackageType=None

Start-Process -FilePath "MAUIImageBenchmarks\bin\Release\net9.0-windows10.0.19041.0\$runtime_identifier\publish\MAUIImageBenchmarks.exe"
