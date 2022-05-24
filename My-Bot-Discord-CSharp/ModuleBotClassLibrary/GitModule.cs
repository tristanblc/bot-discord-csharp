using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using ReaderClassLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleBotClassLibrary
{
    public class GitModule : BaseCommandModule
    {

        [Command("gituser")]
        public async Task UserInfoCommand(CommandContext ctx, string message)
        {
           
            HttpClient client = new HttpClient();
            string url = $"https://api.github.com/users/"+message;
            GitService gitService = new GitService(client,url);
            var gituser = await gitService.Get();


            var builder = new DiscordEmbedBuilder
            {
                Title = "Git Information",

                Color = DiscordColor.Azure               

            };

            if (gituser != null)
            {


         

                var reply = $" Name :  { gituser.login} \n";

                reply += $" Created at :  { gituser.created_at} \n";
                reply += $"  Blog :  { gituser.blog} \n";
                reply += $" Number of public repos :  { gituser.public_repos} \n";


                builder.WithImageUrl(gituser.avatar_url);

                
        

            }

            else
            {
                builder.Color = DiscordColor.Red;
                builder.Description = "No exists";

            }


            await ctx.RespondAsync(builder.Build());
        }
    }
}
