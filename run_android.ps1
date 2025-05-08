

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

#emulator -list-avds
#emulator -avd Pixel_3a_API_34

$configuration = "Release"
$runtime_identifier = "android-arm64"

dotnet build `
    /t:Run `
    --framework net9.0-android `
    --configuration $configuration `
    -p:RuntimeIdentifier=$runtime_identifier `
    MAUIImageBenchmarks/MAUIImageBenchmarks.csproj
