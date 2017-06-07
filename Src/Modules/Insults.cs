using Discord;
using Discord.Commands;
using System.Threading.Tasks;

namespace DiscordBot.Modules
{
    public class InsultModule : ModuleBase
    {
        public InsultProvider Insults { get; set; }

        [Command("insult"), Summary("Insult a user.")]
        public async Task Ban([Summary("The user to insult")] IUser user)
        {
            await ReplyAsync(Insults.GetInsult().Replace("#", user.Mention));
        }
    }
}
