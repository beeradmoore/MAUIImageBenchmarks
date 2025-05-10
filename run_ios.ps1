
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

# Config for device
$runtime_identifier = 'ios-arm64'
$device_udid = 'DEVICE-UUID-GOES-HERE'

# Config for simulator

# For simulator udid you can get it with the following command
# xcrun simctl list devices "iPhone 15"

#$runtime_identifier = 'iossimulator-arm64'
#$device_udid = ':v2:udid=SIMULATOR-UDID-GOES-HERE'

dotnet build -v diag `
    /t:Run `
    --framework net9.0-ios `
    --configuration $configuration `
    -p:RuntimeIdentifier=$runtime_identifier `
    -p:_DeviceName=$device_udid `
    MAUIImageBenchmarks/MAUIImageBenchmarks.csproj
