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

        private string lastPlayedMusic = "";

        private string urlThumb = "https://cdn-icons-png.flaticon.com/512/2480/2480421.png";

        private int volumeStat { get; set; } = 50;

        private string[] urlYtbThumb = { "https://i.ytimg.com/vi/", "/hqdefault.jpg" };

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
            var loadResult = await node.Rest.GetTracksAsync(search, LavalinkSearchType.Youtube);


            if (loadResult.LoadResultType == LavalinkLoadResultType.LoadFailed
                || loadResult.LoadResultType == LavalinkLoadResultType.NoMatches)
            {

                builder.Description = $"Track search failed for {search}.";


                await ctx.RespondAsync(builder.Build());


                return;
            }

            var client = ctx.Client;
            var interactivity = client.GetInteractivity();

            var track = loadResult.Tracks.First();


            if (myTracks.Count >= 1)
            {
                builder.Title = "Playing music"; if (myTracks.Count != 0)
                    await conn.PlayAsync(myTracks.First());
                builder.Color = DiscordColor.Blue;
                builder.Description = $"Now playing {track.Title}! - Author : {track.Author}";

      


       
                lastPlayedMusic = track.Title;

                DiscordActivity activitye = new DiscordActivity(track.Title, ActivityType.ListeningTo);

                await ctx.Client.UpdateStatusAsync(activitye);

                await ctx.RespondAsync(builder.Build());
                await conn.PlayAsync(myTracks.First());
                return;
            }





            if (EmojiCache == null)
            {
                EmojiCache = new[] {
                        DiscordEmoji.FromName(client, ":one:"),
                        DiscordEmoji.FromName(client, ":two:"),
                        DiscordEmoji.FromName(client, ":three:"),
                        DiscordEmoji.FromName(client, ":four:"),
                        DiscordEmoji.FromName(client, ":five:")
                    };
            }

            // Creating the poll message

            var question = "What do you choice ?";


            var trackName = new StringBuilder();
            trackName.Append("**").Append("List of disponible music:").AppendLine("**");


            int i = 0;
            while( i< 5)
            {
                trackName.Append("**").Append("\n" +loadResult.Tracks.ToList()[i].Title +" - "+ loadResult.Tracks.ToList()[i].Author).AppendLine("**");

                i++;
            }


            var embed = utilsService.CreateNewEmbed($"Music choice", DiscordColor.HotPink, trackName.ToString());
            await ctx.RespondAsync(embed.Build());


            var pollStartText = new StringBuilder();
            pollStartText.Append("**").Append(question).AppendLine("**");

            var pollStartMessage = await ctx.RespondAsync(pollStartText.ToString());

            // DoPollAsync adds automatically emojis out from an emoji array to a special message and waits for the "duration" of time to calculate results.
            var pollResult = await interactivity.DoPollAsync(pollStartMessage, EmojiCache, PollBehaviour.KeepEmojis, new TimeSpan(0, 0, 0, 20)); ;
            var firstVotes = pollResult[0].Total;
            var twiceVotes = pollResult[1].Total;
            var threeVotes = pollResult[2].Total;
            var fourVotes = pollResult[3].Total;
            var fifthVotes = pollResult[4].Total;


            // Printing out the result
            var pollResultText = new StringBuilder();
            pollResultText.AppendLine(question);
            pollResultText.Append("Poll result: ");
            pollResultText.Append("**");
            if (firstVotes > twiceVotes && firstVotes > threeVotes && firstVotes > fourVotes && firstVotes > fifthVotes)
                track = loadResult.Tracks.ToList()[0];
            if (twiceVotes > firstVotes && twiceVotes > threeVotes && twiceVotes > fourVotes && twiceVotes > fifthVotes)
                track = loadResult.Tracks.ToList()[1];
            if (threeVotes > firstVotes  && threeVotes > twiceVotes && threeVotes > fourVotes && threeVotes > fifthVotes )
                track = loadResult.Tracks.ToList()[2];
            if (fourVotes > firstVotes && fourVotes > twiceVotes   && fourVotes > threeVotes && fourVotes > fifthVotes)
                track = loadResult.Tracks.ToList()[3];
            if (fifthVotes > firstVotes && fifthVotes > threeVotes && fifthVotes > fourVotes && fifthVotes > twiceVotes)
                track = loadResult.Tracks.ToList()[4];

            await conn.PlayAsync(track);


            builder.Title = "Playing music";

            builder.Color = DiscordColor.Blue;
            builder.Description = $"Now playing {track.Title}! - Author : {track.Author}";
            builder.WithImageUrl($"{urlYtbThumb[0]}{track.Uri.ToString().Split("=")[1]}{urlYtbThumb[1]}");
            lastPlayedMusic = track.Title;

            DiscordActivity activity = new DiscordActivity(track.Title, ActivityType.ListeningTo);

            await ctx.Client.UpdateStatusAsync(activity);

            await ctx.RespondAsync(builder.Build());



        }



        [Command("playSC")]
        [DescriptionCustomAttribute("playAudioCommand")]
        public async Task PlaySC(CommandContext ctx, [RemainingText] string search)
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
            var loadResult = await node.Rest.GetTracksAsync(search, LavalinkSearchType.SoundCloud);



            if (loadResult.LoadResultType == LavalinkLoadResultType.LoadFailed
                || loadResult.LoadResultType == LavalinkLoadResultType.NoMatches)
            {

                builder.Description = $"Track search failed for {search}.";


                await ctx.RespondAsync(builder.Build());


                return;
            }

            var track = loadResult.Tracks.First();
            var client = ctx.Client;
            var interactivity = client.GetInteractivity();
            if (EmojiCache == null)
            {
                EmojiCache = new[] {
                        DiscordEmoji.FromName(client, ":one:"),
                        DiscordEmoji.FromName(client, ":two:"),
                        DiscordEmoji.FromName(client, ":three:"),
                        DiscordEmoji.FromName(client, ":four:"),
                        DiscordEmoji.FromName(client, ":five:")
                    };

            }
            // Creating the poll message

            var question = "What do you choice ?";


            var trackName = new StringBuilder();
            trackName.Append("**").Append("List of disponible music:").AppendLine("**");


            int i = 0;
            while (i < 5)
            {
                trackName.Append("**").Append("\n" + loadResult.Tracks.ToList()[i].Title + " - " + loadResult.Tracks.ToList()[i].Author).AppendLine("**");

                i++;
            }


                    lastPlayedMusic = track.Title;

            var embed = utilsService.CreateNewEmbed($"Music choice", DiscordColor.HotPink, trackName.ToString());
            await ctx.RespondAsync(embed.Build());


            var pollStartText = new StringBuilder();
            pollStartText.Append("**").Append(question).AppendLine("**");

            var pollStartMessage = await ctx.RespondAsync(pollStartText.ToString());

            // DoPollAsync adds automatically emojis out from an emoji array to a special message and waits for the "duration" of time to calculate results.
            var pollResult = await interactivity.DoPollAsync(pollStartMessage, EmojiCache, PollBehaviour.KeepEmojis, new TimeSpan(0, 0, 0, 20)); ;
            var firstVotes = pollResult[0].Total;
            var twiceVotes = pollResult[1].Total;
            var threeVotes = pollResult[2].Total;
            var fourVotes = pollResult[3].Total;
            var fifthVotes = pollResult[4].Total;
            // Printing out the result
            var pollResultText = new StringBuilder();
            pollResultText.AppendLine(question);
            pollResultText.Append("Poll result: ");
            pollResultText.Append("**");
            if (firstVotes > twiceVotes && firstVotes > threeVotes && firstVotes > fourVotes && firstVotes > fifthVotes)
                track = loadResult.Tracks.ToList()[0];
            if (twiceVotes > firstVotes && twiceVotes > threeVotes && twiceVotes > fourVotes && twiceVotes > fifthVotes)
                track = loadResult.Tracks.ToList()[1];
            if (threeVotes > firstVotes && threeVotes > twiceVotes && threeVotes > fourVotes && threeVotes > fifthVotes)
                track = loadResult.Tracks.ToList()[2];
            if (fourVotes > firstVotes && fourVotes > twiceVotes && fourVotes > threeVotes && fourVotes > fifthVotes)
                track = loadResult.Tracks.ToList()[3];
            if (fifthVotes > firstVotes && fifthVotes > threeVotes && fifthVotes > fourVotes && fifthVotes > twiceVotes)
                track = loadResult.Tracks.ToList()[4];
            await conn.PlayAsync(track);


            builder.Title = "Playing music";

            builder.Color = DiscordColor.Blue;
            builder.Description = $"Now playing {track.Title}! - Author : {track.Author}";

            lastPlayedMusic = track.Title;

            DiscordActivity activity = new DiscordActivity(track.Title, ActivityType.ListeningTo);

            await ctx.Client.UpdateStatusAsync(activity);

            await ctx.RespondAsync(builder.Build());

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

          
            var question = $"Do you want to stop track {lastPlayedMusic} ?";

            if (!string.IsNullOrEmpty(question))
            {
                var client = ctx.Client;
                var interactivity = client.GetInteractivity();
                if (EmojiCache != null)
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
                    builder.Description = $"Stop playing {lastPlayedMusic}!";

                    DiscordActivity activity = new DiscordActivity();

                    await ctx.Client.UpdateStatusAsync(activity);
                    await ctx.RespondAsync(builder.Build());

                }
                else if (yesVotes == noVotes)
                {
                    throw new Exception("No choice ");
                }
                if (yesVotes < noVotes)
                {

                    builder.Title = $"Not Stoping  music {lastPlayedMusic}";

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

                EmojiCache = new[] {
                        DiscordEmoji.FromName(client, ":white_check_mark:"),
                        DiscordEmoji.FromName(client, ":x:")
                    };
  

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

                    builder.Description = $"Resume track {lastPlayedMusic}";
                    builder.Color = DiscordColor.Green;

                    DiscordActivity activity = new DiscordActivity($"Resume track {lastPlayedMusic}");

                    await ctx.Client.UpdateStatusAsync(activity);



                    await ctx.RespondAsync(builder.Build());

                }
                else if (yesVotes == noVotes)
                {
                    throw new Exception("No choice ");
                }
                if (yesVotes < noVotes)
                {

                    builder.Title = $"Not resuming track  {lastPlayedMusic} ";

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
        [RequirePermissions(Permissions.Administrator)]
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


            
            volumeStat = int.Parse(volume);


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

        [Command("volume-equalizer")]
        [RequirePermissions(Permissions.Administrator)]
        [DescriptionCustomAttribute("setVolumeAudioCommand")]

        public async Task Volume(CommandContext ctx)
        {


            var builder = utilsService.CreateNewEmbed("Add or remove volume", DiscordColor.Red, "");
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
            var question = $"Your choice ?";

            if (!string.IsNullOrEmpty(question))
            {
                var client = ctx.Client;
                var interactivity = client.GetInteractivity();

                EmojiCache = new[] {
                        DiscordEmoji.FromName(client, ":arrow_left:"),
                        DiscordEmoji.FromName(client, ":arrow_backward:"),
                        DiscordEmoji.FromName(client, ":white_medium_square:"),
                        DiscordEmoji.FromName(client, ":arrow_forward:"),
                        DiscordEmoji.FromName(client, ":arrow_right:")
                    };
               

                // Creating the poll message
                var pollInfo = new StringBuilder();
                pollInfo.Append("Information: ");
                pollInfo.Append($"\n{DiscordEmoji.FromName(client, ":arrow_left:")} = + 10 to volume");
                pollInfo.Append($"\n{DiscordEmoji.FromName(client, ":arrow_backward:")} = + 5 to volume");
                pollInfo.Append($"\n{DiscordEmoji.FromName(client, ":white_medium_square:")} set to 0");
                pollInfo.Append($"\n{DiscordEmoji.FromName(client, ":arrow_forward:")} set - 5 to volume");
                pollInfo.Append($"\n{DiscordEmoji.FromName(client, ":arrow_right:")} set - 10 to volume");

                var info = utilsService.CreateNewEmbed("Information about Volume equalizer", DiscordColor.Orange ,pollInfo.ToString());
                await ctx.RespondAsync(info.Build());
                var pollStartText = new StringBuilder();
                pollStartText.Append(question);
               
                var pollStartMessage = await ctx.RespondAsync(pollStartText.ToString());

                // DoPollAsync adds automatically emojis out from an emoji array to a special message and waits for the "duration" of time to calculate results.
                var pollResult = await interactivity.DoPollAsync(pollStartMessage, EmojiCache, PollBehaviour.KeepEmojis, new TimeSpan(0, 0, 0, 10));
                var leftVotes = pollResult[0].Total;
                var backwardVotes = pollResult[1].Total;
                var centerVotes = pollResult[2].Total;
                var forwardVotes = pollResult[3].Total;
                var rightVotes = pollResult[4].Total;

                // Printing out the result
                var pollResultText = new StringBuilder();
                pollResultText.AppendLine(question);
                pollResultText.Append("Poll result: ");
                pollResultText.Append("**");
                if (leftVotes > backwardVotes && leftVotes > centerVotes && leftVotes > forwardVotes && leftVotes > rightVotes)
                {


                    if (volumeStat - 10 < 0)
                    {


                        volumeStat = 0;
                    }
                    else
                    {
                        volumeStat -= 10;
                    }
                    await conn.SetVolumeAsync(volumeStat);
            


                }
                else if (backwardVotes > leftVotes && backwardVotes > centerVotes && backwardVotes > forwardVotes && backwardVotes > rightVotes)
                {
                    if (volumeStat - 5 < 0)
                    {

                        volumeStat = 0;
                    }
                    else
                    {
                        volumeStat -= 5;
                    }
                    await conn.SetVolumeAsync(volumeStat);

                }
                else if (centerVotes > leftVotes && backwardVotes < centerVotes && centerVotes > forwardVotes && centerVotes > rightVotes)
                {
                    volumeStat = 0;             
                    await conn.SetVolumeAsync(volumeStat);
                }

                else if (forwardVotes > leftVotes && forwardVotes > centerVotes && rightVotes < forwardVotes && forwardVotes > rightVotes)
                {
                    if (volumeStat + 5 > 100)
                    {

                        volumeStat = 100;
                    }
                    else
                    {
                        volumeStat += 5;
                    }
                    await conn.SetVolumeAsync(volumeStat);
                }
                else if (rightVotes > leftVotes && rightVotes > centerVotes && rightVotes > forwardVotes && backwardVotes < rightVotes)
                {
                    if (volumeStat + 10 > 100) {

                        volumeStat = 100;
                    }
                    else
                    {
                        volumeStat += 10;
                    }
                    await conn.SetVolumeAsync(volumeStat);
                }
                else
                {
                    throw new Exception("Cannot exists parameters");
                }
            }
            else
            {
                await ctx.RespondAsync("Error: the question can't be empty");
            }


            builder.Description = "Volume set to " + volumeStat.ToString();

            await ctx.RespondAsync(builder.Build());


        }

        [Command("add-queue")]
        [DescriptionCustomAttribute("queueCmd")]
        public async Task Queueing(CommandContext ctx, [RemainingText] string search)
        {

            var builder = utilsService.CreateNewEmbed("List track", DiscordColor.Red, "Cleaning Queue");

            var client = ctx.Client;
            var interactivity = client.GetInteractivity();
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




            EmojiCache = new[] {
                        DiscordEmoji.FromName(client, ":one:"),
                        DiscordEmoji.FromName(client, ":two:"),
                        DiscordEmoji.FromName(client, ":three:"),
                        DiscordEmoji.FromName(client, ":four:"),
                        DiscordEmoji.FromName(client, ":five:")
                    };
            // Creating the poll message

            var question = "What do you choice ?";


            var trackName = new StringBuilder();
            trackName.Append("**").Append("List of disponible music:").AppendLine("**");


            int i = 0;
            while (i < 5)
            {
                trackName.Append("**").Append("\n" + loadResult.Tracks.ToList()[i].Title + " - " + loadResult.Tracks.ToList()[i].Author).AppendLine("**");

                i++;
            }


            var embed = utilsService.CreateNewEmbed($"Music choice", DiscordColor.HotPink, trackName.ToString());
            await ctx.RespondAsync(embed.Build());


            var pollStartText = new StringBuilder();
            pollStartText.Append("**").Append(question).AppendLine("**");

            var pollStartMessage = await ctx.RespondAsync(pollStartText.ToString());

            // DoPollAsync adds automatically emojis out from an emoji array to a special message and waits for the "duration" of time to calculate results.
            var pollResult = await interactivity.DoPollAsync(pollStartMessage, EmojiCache, PollBehaviour.KeepEmojis, new TimeSpan(0, 0, 0, 20)); ;
            var firstVotes = pollResult[0].Total;
            var twiceVotes = pollResult[1].Total;
            var threeVotes = pollResult[2].Total;
            var fourVotes = pollResult[3].Total;
            var fifthVotes = pollResult[4].Total;
            // Printing out the result
            var pollResultText = new StringBuilder();
            pollResultText.AppendLine(question);
            pollResultText.Append("Poll result: ");
            pollResultText.Append("**");
            if (firstVotes > twiceVotes && firstVotes > threeVotes && firstVotes > fourVotes && firstVotes > fifthVotes)
                track = loadResult.Tracks.ToList()[0];
            if (twiceVotes > firstVotes && twiceVotes > threeVotes && twiceVotes > fourVotes && twiceVotes > fifthVotes)
                track = loadResult.Tracks.ToList()[1];
            if (threeVotes > firstVotes && threeVotes > twiceVotes && threeVotes > fourVotes && threeVotes > fifthVotes)
                track = loadResult.Tracks.ToList()[2];
            if (fourVotes > firstVotes && fourVotes > twiceVotes && fourVotes > threeVotes && fourVotes > fifthVotes)
                track = loadResult.Tracks.ToList()[3];
            if (fifthVotes > firstVotes && fifthVotes > threeVotes && fifthVotes > fourVotes && fifthVotes > twiceVotes)
                track = loadResult.Tracks.ToList()[4];


            myTracks.Add(track);

            var buildere = new DiscordEmbedBuilder
            {
                Title = $"Add track {track.Title} to queue",

                Color = DiscordColor.Green

            };
            builder.WithImageUrl($"{urlYtbThumb[0]}{track.Uri.ToString().Split("=")[1]}{urlYtbThumb[1]}");
            await ctx.RespondAsync(buildere.Build());
                      
        }

        [Command("force-stop")]
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


            var question = $"Do you skip track {lastPlayedMusic} ?";

            if (!string.IsNullOrEmpty(question))
            {
                var client = ctx.Client;
                var interactivity = client.GetInteractivity();
                EmojiCache = new[] {
                        DiscordEmoji.FromName(client, ":white_check_mark:"),
                        DiscordEmoji.FromName(client, ":x:")
                    };


                // Creating the poll message
                var pollStartText = new StringBuilder();
                pollStartText.Append("**").Append($"Skip track {lastPlayedMusic}").AppendLine("**");
                pollStartText.Append(question);
                var pollStartMessage = await ctx.RespondAsync(pollStartText.ToString());

                // DoPollAsync adds automatically emojis out from an emoji array to a special message and waits for the "duration" of time to calculate results.
                var pollResult = await interactivity.DoPollAsync(pollStartMessage, EmojiCache, PollBehaviour.KeepEmojis, new TimeSpan(0, 0, 0, 15));
                var yesVotes = pollResult[0].Total;
                var noVotes = pollResult[1].Total;

                // Printing out the result
                var pollResultText = new StringBuilder();
                pollResultText.AppendLine(question);
                pollResultText.Append("Result: ");
                pollResultText.Append("**");
                if (yesVotes > noVotes)
                {

                    await conn.StopAsync();
                    builder.Description = $"Skip track {myTracks[0].Title}";
                    builder.Color = DiscordColor.Green;
                    await ctx.RespondAsync(builder.Build());

                    var track = myTracks.First();


                     lastPlayedMusic = track.Title;


                    var embed = utilsService.CreateNewEmbed($"Skip song {lastPlayedMusic}", DiscordColor.Aquamarine, $"Skip song {lastPlayedMusic}");
                    await ctx.RespondAsync(embed.Build());
                        

                        if (myTracks.Count != 0)
                        {
                        await conn.PlayAsync(myTracks.First());


                         var buildere = new DiscordEmbedBuilder

                        {
                            Title = $"Now playing {track.Title}",

                            Color = DiscordColor.Green
                        };
                        builder.WithImageUrl($"{urlYtbThumb[0]}{track.Uri.ToString().Split("=")[1]}{urlYtbThumb[1]}");

                        DiscordActivity activity = new DiscordActivity(track.Title, ActivityType.ListeningTo);

                        await ctx.Client.UpdateStatusAsync(activity);


                        await ctx.RespondAsync(buildere.Build());
                    }
          

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


        [Command("force-skip")]
        [RequirePermissions(Permissions.Administrator)]
        public async Task HandleSkipForceAdmin(CommandContext ctx)
        {

            var lava = ctx.Client.GetLavalink();
            var node = lava.ConnectedNodes.Values.First();
            var conn = node.GetGuildConnection(ctx.Member.VoiceState.Guild);
            var builder = utilsService.CreateNewEmbed("skip track", DiscordColor.Red, "Stoping");


            await conn.StopAsync();

            builder.Description = $"Skip track {myTracks[0].Title}";
            builder.Color = DiscordColor.Green;
            await ctx.RespondAsync(builder.Build());

            var track = myTracks.First();


            lastPlayedMusic = track.Title;


            var embed = utilsService.CreateNewEmbed($"Skip song {lastPlayedMusic}", DiscordColor.Aquamarine, $"Skip song {lastPlayedMusic}");
            await ctx.RespondAsync(embed.Build());


            if (myTracks.Count != 0)
            {
                var tracks = myTracks.First();
                await conn.PlayAsync(track);
                myTracks.Remove(tracks);


                var buildere = new DiscordEmbedBuilder
                {
                    Title = $"Now playing {track.Title}",

                    Color = DiscordColor.Green
                };
                buildere.WithImageUrl($"{urlYtbThumb[0]}{track.Uri.ToString().Split("=")[1]}{urlYtbThumb[1]}");

                DiscordActivity activity = new DiscordActivity(track.Title, ActivityType.ListeningTo);

                await ctx.Client.UpdateStatusAsync(activity);

                await ctx.RespondAsync(buildere.Build());


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


        [Command("list-queues")]

        [DescriptionCustomAttribute("listAudioCommand")]
        public async Task ListsQueue(CommandContext ctx)
        {
         
            string print = "Lists of tracks :";
            int i = 0;

            var Text = new StringBuilder();
         


            if (myTracks.Count == 0)
               Text.Append("**").Append("\n Empty song list queue").AppendLine("**");           

            myTracks.ForEach(action =>
           {

              Text.Append("**").Append("\n " + i.ToString() + " - " + action.Title.ToString()).AppendLine("**");

               i++;
           });


            var builder = utilsService.CreateNewEmbed("List track", DiscordColor.Azure, Text.ToString());
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



  
