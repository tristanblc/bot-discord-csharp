using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Enums;
using DSharpPlus.Interactivity.Extensions;
using DSharpPlus.Lavalink;
using ModuleBotClassLibrary.RessourceManager;
using ServiceClassLibrary.Interfaces;
using ServiceClassLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleBotClassLibrary
{
    public class MusicModule : BaseCommandModule
    {

        public List<LavalinkTrack> myTracks { get; set; } = new List<LavalinkTrack> { };

        private DiscordEmoji[] EmojiCache;
        private IUtilsService utilsService { get; set; }


        public MusicModule()
        {
            utilsService = new UtilsService();
        }

        [Command("join")]
        [DescriptionCustomAttribute("joinAudioCommand")]
        public async Task Join(CommandContext ctx, DiscordChannel channel)
        {
            try
            {
                var builder = utilsService.CreateNewEmbed("Status", DiscordColor.Azure, "");

                var lava = ctx.Client.GetLavalink();
                if (!lava.ConnectedNodes.Any())
                {
                    builder.Description = "The Lavalink connection is not established";

                    await ctx.RespondAsync(builder.Build());

                    return;
                }

                var node = lava.ConnectedNodes.Values.First();

                if (channel.Type != ChannelType.Voice)
                {
                    builder.Description = "Not a valid voice channel.";

                    await ctx.RespondAsync(builder.Build());

                    return;
                }

                await node.ConnectAsync(channel);
                builder.Description = $"Joined {channel.Name}!";
                builder.Color = DiscordColor.Green;

                await ctx.RespondAsync(builder.Build());

            }
            catch (Exception ex)
            {
                var errorMessage = $"Error to join channel :  {ex.Message.ToLower()}";
                var errorEmbed = utilsService.CreateNewEmbed("Error to join", DiscordColor.Red, errorMessage);
                await ctx.RespondAsync(errorEmbed.Build());
            }


        }

        [Command("leave")]
        [DescriptionCustomAttribute("joinAudioCommand")]
        [Description("leave channel")]
        public async Task Leave(CommandContext ctx, DiscordChannel channel)
        {
            try
            {

                var builder = utilsService.CreateNewEmbed("Status", DiscordColor.Azure, "");
                var lava = ctx.Client.GetLavalink();
                if (!lava.ConnectedNodes.Any())
                {
                    await ctx.RespondAsync("The Lavalink connection is not established");
                    return;
                }

                var node = lava.ConnectedNodes.Values.First();

                if (channel.Type != ChannelType.Voice)
                {
                    builder.Description = "Not a valid voice channel.";


                    await ctx.RespondAsync(builder.Build());

                    return;
                }

                var conn = node.GetGuildConnection(channel.Guild);

                if (conn == null)
                {
                    builder.Description = "Lavalink is not connected.";

                    await ctx.RespondAsync(builder.Build());

                    return;
                }

                await conn.DisconnectAsync();
                builder.Description = $"Left {channel.Name}!";
                builder.Color = DiscordColor.Green;
                await ctx.RespondAsync(builder.Build());
            }
            catch (Exception ex)
            {
                var errorMessage = $"Error to join channel :  {ex.Message.ToLower()}";
                var errorEmbed = utilsService.CreateNewEmbed("Error to join", DiscordColor.Red, errorMessage);
                await ctx.RespondAsync(errorEmbed.Build());
            }
        }

        [Command("play")]
        [DescriptionCustomAttribute("playAudioCommand")]
        public async Task Play(CommandContext ctx, [RemainingText] string search)
        {


            var builder = utilsService.CreateNewEmbed("Status", DiscordColor.Azure, "");
            if (ctx.Member.VoiceState == null || ctx.Member.VoiceState.Channel == null)
            {

                builder.Description = $"You are not in a voice channel.";

                await ctx.RespondAsync(builder.Build());

                return;
            }

            var lava = ctx.Client.GetLavalink();
            var node = lava.ConnectedNodes.Values.First();
            var conn = node.GetGuildConnection(ctx.Member.VoiceState.Guild);

            if (conn == null)
            {
                builder.Description = $"Lavalink is not connected.";


                await ctx.RespondAsync(builder.Build());

                return;
            }

            var loadResult = await node.Rest.GetTracksAsync(search);






            if (loadResult.LoadResultType == LavalinkLoadResultType.LoadFailed
                || loadResult.LoadResultType == LavalinkLoadResultType.NoMatches)
            {

                builder.Description = $"Track search failed for {search}.";


                await ctx.RespondAsync(builder.Build());


                return;
            }

            var track = loadResult.Tracks.First();
            var question = $"Do you want to play : {track.Title} ?";

            if (!string.IsNullOrEmpty(question))
            {
                var client = ctx.Client;
                var interactivity = client.GetInteractivity();
                if (EmojiCache == null)
                {
                    EmojiCache = new[] {
                        DiscordEmoji.FromName(client, ":white_check_mark:"),
                        DiscordEmoji.FromName(client, ":x:")
                    };
                }

                // Creating the poll message
                var pollStartText = new StringBuilder();
                pollStartText.Append("**").Append("Poll started for:").AppendLine("**");
                pollStartText.Append(question);
                var pollStartMessage = await ctx.RespondAsync(pollStartText.ToString());

                // DoPollAsync adds automatically emojis out from an emoji array to a special message and waits for the "duration" of time to calculate results.
                var pollResult = await interactivity.DoPollAsync(pollStartMessage, EmojiCache, PollBehaviour.KeepEmojis, new TimeSpan(0, 0, 0, 30)); ;
                var yesVotes = pollResult[0].Total;
                var noVotes = pollResult[1].Total;

                // Printing out the result
                var pollResultText = new StringBuilder();
                pollResultText.AppendLine(question);
                pollResultText.Append("Poll result: ");
                pollResultText.Append("**");
                if (yesVotes > noVotes)
                {

                    await conn.PlayAsync(track);


                    builder.Title = "Playing music";

                    builder.Color = DiscordColor.Blue;
                    builder.Description = $"Now playing {track.Title}! - Author : {track.Author}";



                    await ctx.RespondAsync(builder.Build());

                }
                else if (yesVotes == noVotes)
                {
                    throw new Exception("No choice ");
                }
                if (yesVotes < noVotes)
                {

                    builder.Title = $"Not Playing music ";

                    builder.Color = DiscordColor.Red;
                    builder.Description = $"Not playing song";



                    await ctx.RespondAsync(builder.Build());
                    return;
                }

            }
            else
            {
                await ctx.RespondAsync("Error: the question can't be empty");
            }

        }

        [Command("stop")]
        [DescriptionCustomAttribute("stopAudioCommand")]
        public async Task Stop(CommandContext ctx, [RemainingText] string search)
        {


            var builder = utilsService.CreateNewEmbed("Status", DiscordColor.Red, "");

            if (ctx.Member.VoiceState == null || ctx.Member.VoiceState.Channel == null)
            {
                builder.Description = $"You are not in a voice channel.";

                await ctx.RespondAsync(builder.Build());
                return;
            }

            var lava = ctx.Client.GetLavalink();
            var node = lava.ConnectedNodes.Values.First();
            var conn = node.GetGuildConnection(ctx.Member.VoiceState.Guild);

            if (conn == null)
            {
                builder.Description = $"Lavalink is not connected.";


                await ctx.RespondAsync(builder.Build());
                return;
            }

            var loadResult = await node.Rest.GetTracksAsync(search);

            if (loadResult.LoadResultType == LavalinkLoadResultType.LoadFailed
                || loadResult.LoadResultType == LavalinkLoadResultType.NoMatches)
            {

                builder.Description = $"Track search failed for {search}.";


                await ctx.RespondAsync(builder.Build());
                return;
            }

            var track = loadResult.Tracks.First();
            var question = $"Do you want to stop track {track.Title} ?";

            if (!string.IsNullOrEmpty(question))
            {
                var client = ctx.Client;
                var interactivity = client.GetInteractivity();
                if (EmojiCache == null)
                {
                    EmojiCache = new[] {
                        DiscordEmoji.FromName(client, ":white_check_mark:"),
                        DiscordEmoji.FromName(client, ":x:")
                    };
                }

                // Creating the poll message
                var pollStartText = new StringBuilder();
                pollStartText.Append("**").Append("Poll started for:").AppendLine("**");
                pollStartText.Append(question);
                var pollStartMessage = await ctx.RespondAsync(pollStartText.ToString());

                // DoPollAsync adds automatically emojis out from an emoji array to a special message and waits for the "duration" of time to calculate results.
                var pollResult = await interactivity.DoPollAsync(pollStartMessage, EmojiCache, PollBehaviour.KeepEmojis, new TimeSpan(0, 0, 0, 20));
                var yesVotes = pollResult[0].Total;
                var noVotes = pollResult[1].Total;

                // Printing out the result
                var pollResultText = new StringBuilder();
                pollResultText.AppendLine(question);
                pollResultText.Append("Poll result: ");
                pollResultText.Append("**");
                if (yesVotes > noVotes)
                {

                    await conn.StopAsync();


                    builder.Title = "Stop music";

                    builder.Color = DiscordColor.Red;
                    builder.Description = $"Stop playing {track.Title}!";



                    await ctx.RespondAsync(builder.Build());

                }
                else if (yesVotes == noVotes)
                {
                    throw new Exception("No choice ");
                }
                if (yesVotes < noVotes)
                {

                    builder.Title = $"Not Stoping  music {track.Title}";

                    builder.Color = DiscordColor.Red;
                    builder.Description = $"Not stoping song";



                    await ctx.RespondAsync(builder.Build());
                }

            }
            else
            {
                await ctx.RespondAsync("Error: the question can't be empty");
            }
        }

        [Command("pause")]
        [DescriptionCustomAttribute("pauseAudioCommand")]
        public async Task Pause(CommandContext ctx)
        {


            var builder = utilsService.CreateNewEmbed("Status", DiscordColor.Red, "");
            if (ctx.Member.VoiceState == null || ctx.Member.VoiceState.Channel == null)
            {
                builder.Description = $"You are not in a voice channel.";

                await ctx.RespondAsync(builder.Build());
                return;
            }

            var lava = ctx.Client.GetLavalink();
            var node = lava.ConnectedNodes.Values.First();
            var conn = node.GetGuildConnection(ctx.Member.VoiceState.Guild);

            if (conn == null)
            {

                builder.Description = "Lavalink is not connected.";

                await ctx.RespondAsync(builder.Build());
                return;
            }
            var question = $"Do you want to resume track ?";

            if (!string.IsNullOrEmpty(question))
            {
                var client = ctx.Client;
                var interactivity = client.GetInteractivity();
                if (EmojiCache == null)
                {
                    EmojiCache = new[] {
                        DiscordEmoji.FromName(client, ":white_check_mark:"),
                        DiscordEmoji.FromName(client, ":x:")
                    };
                }

                // Creating the poll message
                var pollStartText = new StringBuilder();
                pollStartText.Append("**").Append("Poll started for:").AppendLine("**");
                pollStartText.Append(question);
                var pollStartMessage = await ctx.RespondAsync(pollStartText.ToString());

                // DoPollAsync adds automatically emojis out from an emoji array to a special message and waits for the "duration" of time to calculate results.
                var pollResult = await interactivity.DoPollAsync(pollStartMessage, EmojiCache, PollBehaviour.KeepEmojis, new TimeSpan(0, 0, 0, 20));
                var yesVotes = pollResult[0].Total;
                var noVotes = pollResult[1].Total;

                // Printing out the result
                var pollResultText = new StringBuilder();
                pollResultText.AppendLine(question);
                pollResultText.Append("Poll result: ");
                pollResultText.Append("**");
                if (yesVotes > noVotes)
                {

                    await conn.ResumeAsync();

                    builder.Description = "Resume track";
                    builder.Color = DiscordColor.Green;
                    await ctx.RespondAsync(builder.Build());



                    await ctx.RespondAsync(builder.Build());

                }
                else if (yesVotes == noVotes)
                {
                    throw new Exception("No choice ");
                }
                if (yesVotes < noVotes)
                {

                    builder.Title = $"Not resuming track";

                    builder.Color = DiscordColor.Red;
                    builder.Description = $"Not resuming track";



                    await ctx.RespondAsync(builder.Build());
                }

            }
            else
            {
                await ctx.RespondAsync("Error: the question can't be empty");
            }



        }

        [Command("volume")]
        [DescriptionCustomAttribute("setVolumeAudioCommand")]

        public async Task Volume(CommandContext ctx, string volume)
        {
            var builder = utilsService.CreateNewEmbed("Status", DiscordColor.Red, "");
            if (ctx.Member.VoiceState == null || ctx.Member.VoiceState.Channel == null)
            {

                builder.Description = $"You are not in a voice channel.";

                await ctx.RespondAsync(builder.Build());
                return;
            }

            var lava = ctx.Client.GetLavalink();
            var node = lava.ConnectedNodes.Values.First();
            var conn = node.GetGuildConnection(ctx.Member.VoiceState.Guild);

            if (conn == null)
            {

                builder.Description = "Lavalink is not connected.";

                await ctx.RespondAsync(builder.Build());

                return;
            }
            int vol = 0;

            int.TryParse(volume, out vol);


            if (vol < 0 && vol > 100)
            {
                builder.Description = "Not possible";

                await ctx.RespondAsync(builder.Build());
                return;
            }



            await conn.SetVolumeAsync(vol);
            builder.Description = "Volume set to " + vol.ToString();

            await ctx.RespondAsync(builder.Build());


        }

        [Command("add-queue")]
        [DescriptionCustomAttribute("queueCmd")]
        public async Task Queueing(CommandContext ctx, [RemainingText] string search)
        {

            var builder = utilsService.CreateNewEmbed("List track", DiscordColor.Red, "Cleaning Queue");

            if (ctx.Member.VoiceState == null || ctx.Member.VoiceState.Channel == null)
            {
                builder.Description = "You are not in a voice channel.";
                await ctx.RespondAsync(builder.Build());
                return;
            }

            var lava = ctx.Client.GetLavalink();
            var node = lava.ConnectedNodes.Values.First();
            var conn = node.GetGuildConnection(ctx.Member.VoiceState.Guild);

            if (conn == null)
            {
                await ctx.RespondAsync("Lavalink is not connected.");
                return;
            }

            var loadResult = await node.Rest.GetTracksAsync(search);

            if (loadResult.LoadResultType == LavalinkLoadResultType.LoadFailed
                || loadResult.LoadResultType == LavalinkLoadResultType.NoMatches)
            {
                builder.Description = "You are not in a voice channel.";
                await ctx.RespondAsync(builder.Build());
                return;
            }

            var track = loadResult.Tracks.First();

            var question = $"Do you add track {track.Title} to queue ?";


            if (!string.IsNullOrEmpty(question) && track is not null)
            {
                var client = ctx.Client;
                var interactivity = client.GetInteractivity();
                if (EmojiCache == null)
                {
                    EmojiCache = new[] {
                        DiscordEmoji.FromName(client, ":white_check_mark:"),
                        DiscordEmoji.FromName(client, ":x:")
                    };
                }

                // Creating the poll message
                var pollStartText = new StringBuilder();
                pollStartText.Append("**").Append("Poll started for:").AppendLine("**");
                pollStartText.Append(question);
                var pollStartMessage = await ctx.RespondAsync(pollStartText.ToString());

                // DoPollAsync adds automatically emojis out from an emoji array to a special message and waits for the "duration" of time to calculate results.
                var pollResult = await interactivity.DoPollAsync(pollStartMessage, EmojiCache, PollBehaviour.KeepEmojis, new TimeSpan(0, 0, 0, 20));
                var yesVotes = pollResult[0].Total;
                var noVotes = pollResult[1].Total;

                // Printing out the result
                var pollResultText = new StringBuilder();
                pollResultText.AppendLine(question);
                pollResultText.Append("Poll result: ");
                pollResultText.Append("**");
                if (yesVotes > noVotes)
                {

                    builder.Description = $"Add track {track.Title} to queue";
                    builder.Color = DiscordColor.Green;
                    await ctx.RespondAsync(builder.Build());


                    await conn.PlayAsync(track);

                    var buildere = new DiscordEmbedBuilder
                    {
                        Title = $"Add track {track.Title} to queue",

                        Color = DiscordColor.Green
                    };
                    await ctx.RespondAsync(buildere.Build());

                }
                else if (yesVotes == noVotes)
                {
                    throw new Exception("No choice ");
                }
                if (yesVotes < noVotes)
                {

                    builder.Title = $"Votes no win . I can't add track to queue list";

                    builder.Color = DiscordColor.Red;

                    await ctx.RespondAsync(builder.Build());
                }

            }


        }

        [Command("stop-admin")]
        [RequirePermissions(Permissions.Administrator)]
        public async Task HandleStopMusicAdmin(CommandContext ctx)
        {

            var lava = ctx.Client.GetLavalink();
            var node = lava.ConnectedNodes.Values.First();
            var conn = node.GetGuildConnection(ctx.Member.VoiceState.Guild);
            var builder = utilsService.CreateNewEmbed("Stop admin music", DiscordColor.Azure, $"Force stop");

            if (ctx.Member.VoiceState == null || ctx.Member.VoiceState.Channel == null)
            {
              
                builder.Description = "You are not in a voice channel.";
                await ctx.RespondAsync(builder.Build());

                return;
            }


            await conn.StopAsync();
            await ctx.RespondAsync(builder.Build());
        }


        [Command("skip")]
        [DescriptionCustomAttribute("skipAudioCommand")]
        public async Task Skip(CommandContext ctx)
        {
         
            var builder = utilsService.CreateNewEmbed("skip track", DiscordColor.Red, "Cleaning Queue");

          

            if (ctx.Member.VoiceState == null || ctx.Member.VoiceState.Channel == null)
            {
                builder.Description = "You are not in a voice channel.";
                await ctx.RespondAsync(builder.Build());
               
                return;
            }

            var lava = ctx.Client.GetLavalink();
            var node = lava.ConnectedNodes.Values.First();
            var conn = node.GetGuildConnection(ctx.Member.VoiceState.Guild);

  
            if (myTracks.Count == 0)
            {
                builder.Description = $"No track in queue !";
                await ctx.RespondAsync(builder.Build());
                return;
            }


            var question = $"Do you skip track {myTracks[0].Title} ?";

            if (!string.IsNullOrEmpty(question))
            {
                var client = ctx.Client;
                var interactivity = client.GetInteractivity();
                if (EmojiCache == null)
                {
                    EmojiCache = new[] {
                        DiscordEmoji.FromName(client, ":white_check_mark:"),
                        DiscordEmoji.FromName(client, ":x:")
                    };
                }

                // Creating the poll message
                var pollStartText = new StringBuilder();
                pollStartText.Append("**").Append("Poll started for:").AppendLine("**");
                pollStartText.Append(question);
                var pollStartMessage = await ctx.RespondAsync(pollStartText.ToString());

                // DoPollAsync adds automatically emojis out from an emoji array to a special message and waits for the "duration" of time to calculate results.
                var pollResult = await interactivity.DoPollAsync(pollStartMessage, EmojiCache, PollBehaviour.KeepEmojis, new TimeSpan(0, 0, 0, 20));
                var yesVotes = pollResult[0].Total;
                var noVotes = pollResult[1].Total;

                // Printing out the result
                var pollResultText = new StringBuilder();
                pollResultText.AppendLine(question);
                pollResultText.Append("Poll result: ");
                pollResultText.Append("**");
                if (yesVotes > noVotes)
                {

                    await conn.ResumeAsync();

                    builder.Description = $"Skip track {myTracks[0].Title}";
                    builder.Color = DiscordColor.Green;
                    await ctx.RespondAsync(builder.Build());

                    var track = myTracks.First();
                    await conn.PlayAsync(track);

                    var buildere = new DiscordEmbedBuilder
                    {
                        Title = $"Now playing {track.Title}",

                        Color = DiscordColor.Green
                    };
                    await ctx.RespondAsync(buildere.Build());

                }
                else if (yesVotes == noVotes)
                {
                    throw new Exception("No choice ");
                }
                if (yesVotes < noVotes)
                {

                    builder.Title = $"Not skiping track {myTracks[0].Title}";

                    builder.Color = DiscordColor.Red;
                    builder.Description = $"Not skiping track";



                    await ctx.RespondAsync(builder.Build());
                }

            }
            else
            {
                await ctx.RespondAsync("Error: the question can't be empty");
            }

         

        }
        [Command("clear-queue")]
        [Description("clear queue")]
        public async Task CleaningQueue(CommandContext ctx)
        {
            try
            {
                myTracks.Clear();
                var builder = utilsService.CreateNewEmbed("List track", DiscordColor.Red, "Cleaning Queue");
                await ctx.RespondAsync(builder.Build());

            }
            catch(Exception ex)
            {
                var builder = utilsService.CreateNewEmbed("Error clear", DiscordColor.Red, $"I can't clear list track");
                await ctx.RespondAsync(builder.Build());
            }





        }



        [Command("lists-queues")]
        [DescriptionCustomAttribute("listAudioCommand")]
        public async Task ListsQueue(CommandContext ctx)
        {
         
            string print = "Lists of tracks :";
            int i = 0;
            myTracks.ForEach(action =>
           {
               print += "\n - " + i.ToString() + " - " + action.Title.ToString();

               i++;
           });


            var builder = utilsService.CreateNewEmbed("List track", DiscordColor.Azure, print);
            await ctx.RespondAsync(builder.Build());


        }


        [Command("del-queue")]
        [DescriptionCustomAttribute("deleteAudioCommand")]
        public async Task delQueue(CommandContext ctx, string state)
        {
 
            var builder = utilsService.CreateNewEmbed("Status music", DiscordColor.Azure, "");

            int i = 0;

            try
            {
                int.TryParse(state, out i);
                myTracks.RemoveAt(i);
                builder.Description = $"del track { i.ToString() } in queue ";
                builder.Color = DiscordColor.Green;

                await ctx.RespondAsync(builder.Build());    
            }
            catch(Exception ex)
            {
                builder.Description = $"Error ";
                builder.Color = DiscordColor.Red;

                await ctx.RespondAsync(builder.Build());

            }

           


        }


    }
}



  
