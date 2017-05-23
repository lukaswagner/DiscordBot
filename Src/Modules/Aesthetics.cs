using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Modules
{
    public class Aesthetics : ModuleBase
    {
        [Command("aesthetic"), Summary("Aesthetify a message.")]
        [Alias("aest", "jcostyle")]
        public async Task Ban([Remainder, Summary("The string to make aesthetic.")] string message)
        {
            string result = "";
            for(int index = 0; index < message.Length; index++)
            {
                char c = message[index];
                byte[] b = BitConverter.GetBytes(c);
                if (b[0] > 32 && b[0] < 127 && b[1] == 0)
                {
                    b[1] = 255;
                    b[0] -= 32;
                    result += BitConverter.ToChar(b, 0);
                }
                else if(b[0] == 32 && b[1] == 0)
                {
                    b[1] = 48;
                    b[0] = 0;
                    result += BitConverter.ToChar(b, 0);
                }
                else result += c;
            }
            await ReplyAsync(result);
        }
    }
}
