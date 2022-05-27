using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Lavalink;
using ServiceClassLibrary.Interfaces;
using ServiceClassLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleBotClassLibrary
{
    public  class MusicModule : BaseCommandModule
    {

        public  List<LavalinkTrack> myTracks { get; set; } = new List<LavalinkTrack> { };


        private IUtilsService utilsService { get; set; }


        public MusicModule()
        {
            utilsService = new UtilsService()
        }
        
        [Command("join")]
        public async Task Join(CommandContext ctx, DiscordChannel channel)
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

        [Command("leave")]
        public async Task Leave(CommandContext ctx, DiscordChannel channel)
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

        [Command]
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

            await conn.PlayAsync(track);


            builder.Title = "Playing music";

            builder.Color = DiscordColor.Blue;
            builder.Description = $"Now playing {track.Title}!";
         


            await ctx.RespondAsync(builder.Build());

         
        }

        [Command("stop")]
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

            await conn.StopAsync();


            builder.Title = "Stop music";

            builder.Color = DiscordColor.Red;
            builder.Description = $"Stop playing {track.Title}!";
        }
       
        [Command("pause")]
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

            await conn.ResumeAsync();

            builder.Description = "Lavalink is not connected.";
            builder.Color = DiscordColor.Green;
            await ctx.RespondAsync(builder.Build());
            
           
        }

        [Command("volume")]
        public async Task Volume(CommandContext ctx,string volume)
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


            if (vol < 0  && vol > 100)
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


            myTracks.Add(track);

            await ctx.RespondAsync($"Add to queue {track.Title}!");

        }

        [Command("skip")]
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


            var track = myTracks.First();
            await conn.PlayAsync(track);

            var buildere = new DiscordEmbedBuilder
            {
                Title = "List track",

                Color = DiscordColor.Green,
                Description = "Skip music"

            };
            await ctx.RespondAsync(buildere.Build());

        }
        [Command("clear-queue")]
        public async Task CleaningQueue(CommandContext ctx)
        {

            myTracks.Clear();           
            var builder = utilsService.CreateNewEmbed("List track", DiscordColor.Red, "Cleaning Queue");
            await ctx.RespondAsync(builder.Build());
               
         

        }



        [Command("lists-queues")]
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



  
