using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace DiscordBot
{
    public class Program
    {
        private CommandService commands;
        private DiscordSocketClient client;
        private DependencyMap map;

        public static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            client = new DiscordSocketClient();
            commands = new CommandService();
            map = new DependencyMap();

            client.Log += Log;

            string token = "MzA2NDY4OTYyODU2NTk5NTUz.C-ENDA.WDO_vWHrtiRsiZ4sVGq8FuqHGOs";

            await InstallCommands();
            await client.LoginAsync(TokenType.Bot, token);
            await client.StartAsync();

            // Block this task until the program is closed.
            await Task.Delay(-1);
        }

        public async Task InstallCommands()
        {
            map.Add(new InsultProvider());
            // Hook the MessageReceived Event into our Command Handler
            client.MessageReceived += HandleCommand;
            // Discover all of the commands in this assembly and load them.
            await commands.AddModulesAsync(Assembly.GetEntryAssembly());
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        public async Task HandleCommand(SocketMessage messageParam)
        {
            if (messageParam.Author.Id == client.CurrentUser.Id) return;
            // Don't process the command if it was a System Message
            var message = messageParam as SocketUserMessage;
            if (message == null) return;
            // Create a number to track where the prefix ends and the command begins
            int argPos = 0;
            // Create a Command Context
            var context = new CommandContext(client, message);
            // Determine if the message is a command, based on if it starts with '!' or a mention prefix
            if (message.HasCharPrefix('!', ref argPos) || message.HasMentionPrefix(client.CurrentUser, ref argPos))
            {
                // Execute the command. (result does not indicate a return value, 
                // rather an object stating if the command executed succesfully)
                var result = await commands.ExecuteAsync(context, argPos, map);
                if (!result.IsSuccess)
                    await context.Channel.SendMessageAsync(result.ErrorReason);
            }
            var keywordResult = await KeywordChecker.ExecuteAsync(message, context);
            if (!keywordResult.IsSuccess)
                await context.Channel.SendMessageAsync(keywordResult.ErrorReason);
        }
    }
}
