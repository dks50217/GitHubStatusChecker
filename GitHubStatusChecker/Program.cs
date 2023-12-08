using System;
using Discord;
using Discord.WebSocket;
using GitHubStatusChecker.Helper;
using Microsoft.Extensions.Configuration;
using Octokit;


class Program
{
    private static IConfigurationRoot _configuration;
    private const string ConfigFilePath = "version.json";
    private static DiscordSocketClient _client;
    private static ulong DiscordServerId;
    private static ulong DiscordChannelId;

    static async Task Main()
    {
         // 載入設定檔
        _configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var discordSettings = _configuration.GetSection("DiscordSettings");
        DiscordServerId = ulong.Parse(discordSettings["ServerId"]);
        DiscordChannelId = ulong.Parse(discordSettings["ChannelId"]);  

        _client = new DiscordSocketClient();
        _client.Log += Log;

        string botToken = discordSettings["botToken"];
        await _client.LoginAsync(TokenType.Bot, botToken);
        await _client.StartAsync();

        _client.Ready += Ready;

        // 應用程式持續運行，不要結束
        await Task.Delay(-1);
    }
    
    private static async Task CheckForNewReleasePeriodically(TimeSpan interval)
    {
        while (true)
        {
            // 在每次迴圈中檢查新版本
            await CheckForNewRelease();

            // 等待指定的時間間隔
            await Task.Delay(interval);
        }
    }


    private static async Task Ready()
    {
        Console.WriteLine("Bot is connected.");

        // 啟動定時檢查版本的循環
        await CheckForNewReleasePeriodically(TimeSpan.FromMinutes(30));
    }

    private static Task Log(LogMessage arg)
    {
        Console.WriteLine(arg);
        return Task.CompletedTask;
    }

    private static async Task CheckForNewRelease()
    {
        Console.WriteLine("Bot is connected.");

        // Discord 伺服器和頻道
        var server = _client.GetGuild(DiscordServerId);
        var channel = server.GetTextChannel(DiscordChannelId);

        // 替換以下變數為目標 GitHub 帳戶和儲存庫信息
        var gitHubSettings = _configuration.GetSection("GithubSettings");
        string targetUsername = gitHubSettings["targetUsername"];
        string targetRepositoryName = gitHubSettings["targetRepositoryName"];

        var github = new GitHubClient(new ProductHeaderValue("GitHubUpdateChecker"));

        try
        {
            var releases = await github.Repository.Release.GetAll(targetUsername, targetRepositoryName);

            if (releases.Count > 0)
            {
                var latestRelease = releases[0];

                Console.WriteLine($"Latest Release Tag: {latestRelease.TagName}");
                Console.WriteLine($"Published at: {latestRelease.PublishedAt}");

                var lastCheckedRelease = VersionHelper.ReadLastCheckedRelease(ConfigFilePath);

                if (lastCheckedRelease == null || latestRelease.PublishedAt > lastCheckedRelease.PublishedAt)
                {
                    Console.WriteLine("New Release detected!");

                    VersionHelper.SaveLastCheckedRelease(latestRelease, configFilePath: ConfigFilePath);

                    await channel.SendMessageAsync($"漢化有新版本了: {latestRelease.TagName}! 連結: {latestRelease.HtmlUrl}");
                }
                else
                {
                    Console.WriteLine("No new releases.");
                }
            }
            else
            {
                Console.WriteLine("No releases found for the repository.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}







