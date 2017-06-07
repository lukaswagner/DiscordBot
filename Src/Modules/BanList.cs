using Discord;
using Discord.Commands;
using System.Threading.Tasks;

namespace DiscordBot.Modules
{
    public class BanListModule : ModuleBase
    {
        [Command("ban"), Summary("Ban a user.")]
        public async Task Ban([Summary("The user to ban")] IUser user, [Remainder, Summary("The reason for the ban")] string reason)
        {
            // ReplyAsync is a method on ModuleBase
            await ReplyAsync("not implemented");
        }

        [Command("multiban"), Summary("Ban a user multiple times.")]
        [Alias("mban")]
        public async Task MultiBan([Summary("The user to ban")] IUser user, [Summary("How often to ban the user")] int amount, [Remainder, Summary("The reason for the bans")] string reason)
        {
            // ReplyAsync is a method on ModuleBase
            await ReplyAsync("not implemented");
        }

        [Command("unban"), Summary("Unban a user.")]
        public async Task Unban([Summary("The user to unban")] IUser user, [Remainder, Summary("The reason for the unban")] string reason)
        {
            // ReplyAsync is a method on ModuleBase
            await ReplyAsync("not implemented");
        }

        [Command("multiunban"), Summary("Unban a user multiple times.")]
        [Alias("muban")]
        public async Task MultiUnban([Summary("The user to ban")] IUser user, [Summary("How often to ban the user")] int amount, [Remainder, Summary("The reason for the bans")] string reason)
        {
            // ReplyAsync is a method on ModuleBase
            await ReplyAsync("not implemented");
        }

        [Command("bancount"), Summary("Display how many times a user has been banned.")]
        [Alias("bc")]
        public async Task BanCount([Summary("The (optional) user to get info for")] IUser user = null)
        {
            var userInfo = user ?? Context.Client.CurrentUser;
            // ReplyAsync is a method on ModuleBase
            await ReplyAsync("not implemented");
        }

        [Command("scoreboard"), Summary("Display how many times each user has been banned.")]
        public async Task Scoreboard()
        {
            // ReplyAsync is a method on ModuleBase
            await ReplyAsync("not implemented");
        }

        [Command("banlist"), Summary("Link a list of all bans.")]
        public async Task Banlist()
        {
            // ReplyAsync is a method on ModuleBase
            await ReplyAsync("not implemented");
        }

        [Command("recalculateBans"), Summary("Recalculate the number of bans for each user.")]
        [Alias("rb")]
        public async Task RecalculateBans()
        {
            // ReplyAsync is a method on ModuleBase
            await ReplyAsync("not implemented");
        }
    }
}
