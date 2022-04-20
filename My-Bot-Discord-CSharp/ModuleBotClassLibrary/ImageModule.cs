using BotClassLibrary;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using EffectClassLibrary;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleBotClassLibrary
{
    public class ImageModule : BaseCommandModule
    {

        [Command("gray")]
        public async Task CommandImageToGray(CommandContext ctx,Bitmap bit)
        {
            try
            {
                Effect effect = new Effect();
         

                Bitmap bitmap = new Bitmap(bit);
                bitmap = effect.SetToGray(bitmap);
                using (MemoryStream stream = new MemoryStream())
                {
                    bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                    stream.Seek(0, SeekOrigin.Begin);
                    bitmap.Dispose();
                    var embed = new DiscordEmbedBuilder()
                        .WithImageUrl($"attachment://{bitmap}")
                     .Build();
                    await ctx.RespondAsync(embed);
                }
            }
            catch(Exception ex)
            {
                await ctx.RespondAsync(ex.ToString());
            }
            
        }


        [Command("wb")]
        public async Task CommandImageToWb(CommandContext ctx, Bitmap bit)
        {
            try
            {
                Effect effect = new Effect();


                Bitmap bitmap = new Bitmap(bit);
                bitmap = effect.SetToBlackAndWhite(bitmap);
                using (MemoryStream stream = new MemoryStream())
                {
                    bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                    stream.Seek(0, SeekOrigin.Begin);
                    bitmap.Dispose();
                    var embed = new DiscordEmbedBuilder()
                        .WithImageUrl($"attachment://{bitmap}")
                     .Build();
                    await ctx.RespondAsync(embed);
                }
            }
            catch (Exception ex)
            {
                await ctx.RespondAsync(ex.ToString());
            }

        }

        [Command("permut")]
        public async Task CommandImagePermut(CommandContext ctx, Bitmap bit)
        {
            try
            {
                Effect effect = new Effect();


                Bitmap bitmap = new Bitmap(bit);
                bitmap = effect.PermutColor(bitmap);
                using (MemoryStream stream = new MemoryStream())
                {
                    bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                    stream.Seek(0, SeekOrigin.Begin);
                    bitmap.Dispose();
                    var embed = new DiscordEmbedBuilder()
                        .WithImageUrl($"attachment://{bitmap}")
                     .Build();
                    await ctx.RespondAsync(embed);
                }
            }
            catch (Exception ex)
            {
                await ctx.RespondAsync(ex.ToString());
            }

        }


    }
}
