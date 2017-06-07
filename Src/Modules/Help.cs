using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiscordBot.Modules
{
    public class Help : ModuleBase
    {
        public CommandService Commands { get; }
        public IDependencyMap DependencyMap { get; }
        public Help(CommandService commands, IDependencyMap dependencyMap)
        {
            Commands = commands;
            DependencyMap = dependencyMap;
        }

        private static string _fixTypeNames(string name)
        {
            switch (name)
            {
                case "IUser":
                    return "User mention";
                default:
                    return name;
            }
        }

        [Command("help"), Summary("Print information about a specified command.")]
        [Alias("h", "commands")]
        public async Task ShowHelp([Summary("The command for which to print help")] string command)
        {
            string endl = "\n";
            SearchResult s = Commands.Search(Context, command);
            if (!s.IsSuccess)
            {
                await ReplyAsync("Could not find help for this command. Error: " + s.ErrorReason);
            }
            else
            {
                string result = s.Commands.Count > 1 ? "Found multiple commands:\n\n" : "";

                foreach(CommandMatch cm in s.Commands)
                {
                    CommandInfo ci = cm.Command;

                    result += "Command: " + ci.Name + endl;
                    result += "Aliases: " + (ci.Aliases.Count == 0 ? "None" : "");

                    foreach (string alias in ci.Aliases)
                        result += alias + ", ";
                    result = result.Remove(result.Length - 2, 2);
                    result += endl;

                    result += "Summary: " + ci.Summary + endl;

                    result += "Parameters: " + (ci.Parameters.Count == 0 ? "none\n" : endl);
                    foreach(ParameterInfo pi in ci.Parameters)
                        result += "    " + pi.Name + " | " + pi.Summary + " | " + _fixTypeNames(pi.Type.Name) + (pi.IsOptional ? " | Optional\n" : endl);

                    bool allowed = true;
                    string reasons = "";
                    foreach (PreconditionAttribute pa in ci.Preconditions)
                    {
                        PreconditionResult pr = await pa.CheckPermissions(Context, ci, DependencyMap);
                        if (!pr.IsSuccess)
                        {
                            allowed = false;
                            reasons += ". " + pr.ErrorReason;
                        }
                    }
                    result += "Permission: You're " + (allowed ? "" : "not ") + "allowed to use this command." + (allowed ? "" : " Reason(s):" + reasons);

                    result += endl + endl;
                }
                result = result.Remove(result.Length - 2, 2);

                await ReplyAsync(result);
            }
        }

        [Command("help"), Summary("Print the available commands.")]
        [Alias("h", "commands")]
        public async Task ShowHelpAll()
        {
            string result = "Available commands:\n";

            IOrderedEnumerable<CommandInfo> sorted = Commands.Commands.OrderBy(ci => ci.Name);

            foreach(CommandInfo ci in sorted)
                result += ci.Name + ": " + ci.Summary + "\n";

            result += "Use !help <commandname> for more information about a specific command.";

            await ReplyAsync(result);
        }
    }
}
