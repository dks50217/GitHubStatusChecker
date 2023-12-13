# GitHub Status Checker

GitHub Status Checker 是一個使用 .NET 6.0 開發的應用程式，主要功能是定期檢查 GitHub 儲存庫的新版本並在 Discord 頻道中發布通知。

## 專案結構

- [`Program.cs`](Program.cs): 主程式入口點，包含主要的應用程式邏輯。
- [`Helper/VersionHelper.cs`](Helper/VersionHelper.cs): 提供版本相關的輔助功能。
- [`Model/ReleaseInfo.cs`](Model/ReleaseInfo.cs): 定義了發布資訊的資料模型。
- `appsettings.json`: 存放應用程式設定，例如 Discord 伺服器和頻道 ID。
- [`version.json`](version.json): 存放當前應用程式的版本資訊。

## 如何使用

1. 執行`setup.ps1`產生出 `appsettings.json`
2. 在 `appsettings.json` 中設定你的 Discord 伺服器和頻道 ID。
3. 在 [`Program.cs`](Program.cs) 中設定你要追蹤的 GitHub 帳戶和儲存庫名稱。
4. 執行 `dotnet run` 來啟動應用程式。

## 如何建置

1. 執行`setup.ps1`產生出 `appsettings.json`
2. 執行 `dotnet build` 來建置應用程式。
3. 執行 `dotnet publish -c Release -o ./publish` 來發布應用程式。

## 依賴套件

- Discord.Net: 一個 .NET 的 Discord API 庫，包含以下組件：
  - Discord.Net.Webhook
  - Discord.Net.WebSocket
  - Discord.Net.Rest
  - Discord.Net.Interactions
  - Discord.Net.Core
  - Discord.Net.Commands

## 版本控制

我們使用 [`version.json`](version.json) 來追蹤應用程式的版本資訊。每次發布新版本時，都應該更新這個檔案。


## DiscordSettings

這個區塊包含了與 Discord 相關的設定：

- `ServerId`: 這是 Discord 伺服器的唯一識別碼，用於與特定的 Discord 伺服器進行互動。
- `ChannelId`: 這是 Discord 頻道的唯一識別碼，用於與特定的 Discord 頻道進行互動。
- `botToken`: 這是 Discord 機器人的權杖（token），用於驗證並授權機器人對 Discord 伺服器和頻道進行操作。

## GithubSettings

這個區塊包含了與 GitHub 相關的設定：

- `targetUsername`: 這是目標使用者的 GitHub 用戶名，用於與特定的使用者進行互動，例如獲取使用者的個人資訊或存取其存儲庫。
- `targetRepositoryName`: 這是目標存儲庫的名稱，用於與特定的存儲庫進行互動，例如獲取存儲庫的內容或進行提交操作。

## 貢獻

如果你有任何建議或問題，歡迎開 issue 或提交 pull request。