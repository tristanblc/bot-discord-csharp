using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Converters;
using DSharpPlus.CommandsNext.Entities;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;

using ExceptionClassLibrary;
using ModuleBotClassLibrary.RessourceManager;
using OpenQA.Selenium.Internal;
using ServiceClassLibrary.Interfaces;
using ServiceClassLibrary.Services;

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
        protected string _strBuilder { get; set; }
        protected CommandContext _ctx { get; set; }

        private ILoggerProject _logger { get; init; }


        public BotHelpFormatter(CommandContext ctx) : base(ctx)
        {
            _embed = new DiscordEmbedBuilder();
            _embed.Color = DiscordColor.Cyan;
            _embed.Title = $"Bot Help - Use ! prefix";
            _strBuilder = "";
            _commandList = "";
            _ctx = ctx;
            _logger = new LoggerProject();
        }
        public override BaseHelpFormatter WithCommand(Command command)
        {
            try
            {
                _embed.Title = $"Command {command.Name} Help";

                var commandHelpString = "";
                command.CustomAttributes.ToList().ForEach(action =>
                {
                    if (action.GetType() == typeof(DescriptionCustomAttribute))
                    {
                        var descAction = action as DescriptionCustomAttribute;
                        _strBuilder += $"\n{command.Name} - {descAction}";
                    };


                }
                );

                if (command.Aliases != null)
                {
                    _strBuilder += $"\nAlias - {command.Aliases.Count} number of alias";
                    command.Aliases.ToList().ForEach(alias =>
                    {
                        _strBuilder += $"\n{alias}";
                    });
                }

                _strBuilder += $"\n Command from module {command.Module.ModuleType.Name}";

                return this;
            }
            catch (Exception)
            {
                var exception = $"Cannot get commmand information {command.Name}";
                _logger.WriteLogErrorLog(exception);
                throw new HelpFormatterException(exception);
            }

        }


        public override BaseHelpFormatter WithSubcommands(IEnumerable<Command> cmds)
        {

            try
            {
                _embed.Title = $"Bot Help - Use ! prefix";


                cmds.ToList().ForEach(cmd =>
                {

                    cmd.CustomAttributes.ToList().ForEach(action =>
                    {

                        if (action.GetType() == typeof(DescriptionCustomAttribute))
                        {
                            var descAction = action as DescriptionCustomAttribute;
                            if (String.IsNullOrEmpty(descAction.Description))
                                _strBuilder += $"\n{cmd.Name} - This command don't have a description";
                            else
                                _strBuilder += $"\n{cmd.Name} - {descAction.Description}";

                        };


                    });

                });
                return this;
            }
            catch (Exception)
            {
                var exception = $"Cannot get sub commmands information ";
                _logger.WriteLogErrorLog(exception);
                throw new HelpFormatterException(exception);
            }

        }

        public override CommandHelpMessage Build()
        {
            var helpStringFormatter = _strBuilder;
            var interactivity = _ctx.Client.GetInteractivity();
            var pages = interactivity.GeneratePagesInEmbed(helpStringFormatter, DSharpPlus.Interactivity.Enums.SplitType.Line, _embed);


            pages.ToList().ForEach(page =>
            {
                _ctx.RespondAsync(page.Embed);
            });
            return new CommandHelpMessage();

        }
    }

}