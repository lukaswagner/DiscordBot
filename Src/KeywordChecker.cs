using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiscordBot
{
    internal static class KeywordChecker
    {
        private static readonly Dictionary<string[], Func<SocketUserMessage, ICommandContext, int, string, Task<ExecuteResult>>> delegates;

        static KeywordChecker()
        {
            delegates = new Dictionary<string[], Func<SocketUserMessage, ICommandContext, int, string, Task<ExecuteResult>>>
            {
                { Banana.Keywords, Banana.Delegate },
                { Eksdee.Keywords, Eksdee.Delegate }
            };
        }

        public static async Task<IResult> ExecuteAsync(SocketUserMessage message, ICommandContext context)
        {
            StringComparison compare = StringComparison.OrdinalIgnoreCase; ;
            foreach (string[] keys in delegates.Keys)
            {
                foreach (string key in keys)
                {
                    int index = message.Content.IndexOf(key, compare);
                    if (index > -1)
                    {
                        return await delegates[keys].Invoke(message, context, index, key);
                    }
                }
            }
            return ExecuteResult.FromSuccess();
        }
    }
}