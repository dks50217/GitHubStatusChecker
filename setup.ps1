

$appSettingsPath = "appsettings.json"

if (-not (Test-Path $appSettingsPath)) {
        $settings = @{
        DiscordSettings = @{
            ServerId = "your_server_id"
            ChannelId = "your_channel_id"
            botToken = "your_bot_token"
        }
        GithubSettings = @{
            targetUsername = "your_username"
            targetRepositoryName = "your_repository_name"
        }
    }

    $json = $settings | ConvertTo-Json -Depth 2
    $json | Out-File -FilePath $appSettingsPath
}
