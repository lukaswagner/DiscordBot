using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot
{
    static class Banana
    {
        public static string[] Keywords = { "banana" };

        public static Func<SocketUserMessage, ICommandContext, int, string, Task<ExecuteResult>> Delegate = new Func<SocketUserMessage, ICommandContext, int, string, Task<ExecuteResult>>(async (SocketUserMessage message, ICommandContext context, int index, string foundWord) =>
        {
            try
            {
                await context.Channel.SendFileAsync("Resources/Banana/Banana.jpg", "I found this image of a banana.");
                return ExecuteResult.FromSuccess();
            }
            catch(Exception ex)
            {
                return ExecuteResult.FromError(ex);
            }
        });
    }
}
