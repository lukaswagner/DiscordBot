using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
public class RequireElevatedPermissionsAttribute : PreconditionAttribute
{
    public async override Task<PreconditionResult> CheckPermissions(ICommandContext context, CommandInfo command, IDependencyMap map)
    {
        // role id check
        foreach (ulong id in (context.User as IGuildUser).RoleIds)
        {
            // TODO load these from a server or something
            if (id == 230318241828962305) return PreconditionResult.FromSuccess(); // test server mod role
            if (id == 0) return PreconditionResult.FromSuccess(); // rlsd server mod role
        }

        // backdoor
        if (context.User.Id == (await context.Client.GetApplicationInfoAsync()).Owner.Id) return PreconditionResult.FromSuccess();
        
        return PreconditionResult.FromError("You need elevated permissions on this server to run this command, but you're underpriviledged.");
    }
}
