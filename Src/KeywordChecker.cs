using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiscordBot
{
    internal delegate IResult KeywordDelegate(SocketUserMessage message, ICommandContext context, int index); 
    internal static class KeywordChecker
    {
        private static readonly Dictionary<string, KeywordDelegate> delegates;

        static KeywordChecker()
        {
            delegates = new Dictionary<string, KeywordDelegate>
            {
                { Banana.Keyword, Banana.Delegate }
            };
        }

        public static async Task<IResult> ExecuteAsync(SocketUserMessage message, ICommandContext context)
        {
            StringComparison compare = StringComparison.OrdinalIgnoreCase; ;
            foreach(string key in delegates.Keys)
            {
                int index = message.Content.IndexOf(key, compare);
                if (index > -1)
                {
                    return await Task.Run(() => delegates[key].Invoke(message, context, index));
                }
            }
            return ExecuteResult.FromSuccess();
        }
    }
}