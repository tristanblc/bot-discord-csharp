using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Converters;
using DSharpPlus.CommandsNext.Entities;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_Bot_Discord_CSharp.Formatter
{
    public class BotHelpFormatter : BaseHelpFormatter
    {
        protected DiscordEmbedBuilder _embed { get; set; }

        protected string _commandList { get; set; }
        protected StringBuilder _strBuilder { get; set; }
        protected CommandContext _ctx { get; set; }

        public BotHelpFormatter(CommandContext ctx) : base(ctx)
        {
            _embed = new DiscordEmbedBuilder();
             _strBuilder = new StringBuilder();
            _commandList = "";
            _ctx = ctx;
        }

        public override BaseHelpFormatter WithCommand(Command command)
        {
            _embed.AddField(command.Name, command.Description);            
            _strBuilder.AppendLine($"{command.Name} - {command.Description}");

            return this;
        }


        public override BaseHelpFormatter WithSubcommands(IEnumerable<Command> cmds)
        {
            _embed = new DiscordEmbedBuilder();
            _embed.Title = $"Bot Command - Use ! prefix";
            _embed.Color = DiscordColor.Chartreuse;
            cmds.ToList().ForEach(cmd =>
            {
                if (cmd.Description == null)
                    _embed.Description += $"\n{cmd.Name} - This command doesn't have a description";
                else
                    _embed.Description += $"\n{cmd.Name} - {cmd.Description}";

            });
            return this;
        }

        public override CommandHelpMessage Build()
        {
             return new CommandHelpMessage(embed: _embed);
  
        }

   
     }
}
