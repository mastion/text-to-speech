[CmdletBinding()]
param (
    [Parameter(Mandatory=$true)]
    [string]$subscriptionKey,
    [Parameter(Mandatory=$false)]
    [string]$region = "westus"
)

dotnet tool restore
dotnet paket restore
$Env:tts_SubscriptionKey = $subscriptionKey
$Env:tts_ServiceRegion = $region
dotnet run -r win-x64