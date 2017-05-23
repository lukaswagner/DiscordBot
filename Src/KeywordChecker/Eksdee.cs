using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DiscordBot
{
    static class Eksdee
    {
        public static string[] Keywords = { "xdee", "xd", "eksdee", "eks dee" };

        public static Func<SocketUserMessage, ICommandContext, int, string, Task<ExecuteResult>> Delegate = new Func<SocketUserMessage, ICommandContext, int, string, Task<ExecuteResult>>(async (SocketUserMessage message, ICommandContext context, int index, string foundWord) =>
        {
            try
            {
                string disclaimer = message.Author.Mention + " used a very naughty word. It has been censored in order to not trigger Tron. Here is a cleaned-up copy of the message:\n";
                
                string changedString = message.Content;
                foreach (string word in Keywords)
                    changedString = Regex.Replace(changedString, word, "[Redacted]", RegexOptions.IgnoreCase);
                await message.DeleteAsync();
                await context.Channel.SendMessageAsync(disclaimer + changedString);
                return ExecuteResult.FromSuccess();
            }
            catch (Exception ex)
            {
                return ExecuteResult.FromError(ex);
            }
        });
    }
}
