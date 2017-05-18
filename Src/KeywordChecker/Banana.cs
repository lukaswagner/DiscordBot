using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordBot
{
    static class Banana
    {
        public static string Keyword = "banana";

        public static KeywordDelegate Delegate = (SocketUserMessage message, ICommandContext context, int index) =>
        {
            try
            {
                context.Channel.SendFileAsync("Resources/Banana/Banana.jpg", "I found this image of a banana.");
                return ExecuteResult.FromSuccess();
            }
            catch(Exception ex)
            {
                return ExecuteResult.FromError(ex);
            }
        };

    }
}
