using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
public class RequireNormalPermissionsAttribute : PreconditionAttribute
{
    public async override Task<PreconditionResult> CheckPermissions(ICommandContext context, CommandInfo command, IDependencyMap map)
    {
        // role id check
        foreach(ulong id in (context.User as IGuildUser).RoleIds)
        {
            // TODO load these from a server or something
            if (id == 322144615354466314) return PreconditionResult.FromSuccess(); // test server normal role
            if (id == 204738341659213824) return PreconditionResult.FromSuccess(); // rlsd server normal role
        }

        // backdoor
        if (context.User.Id == (await context.Client.GetApplicationInfoAsync()).Owner.Id) return PreconditionResult.FromSuccess();

        return PreconditionResult.FromError("You need normal permissions on this server to run this command, but you're underpriviledged.");
    }
}
