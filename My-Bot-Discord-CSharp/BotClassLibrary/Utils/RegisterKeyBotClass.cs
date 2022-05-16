using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotClassLibrary.Utils
{
    public class RegisterKeyBotClass 
    {

        private string path { get; init; }


        public RegisterKeyBotClass()
        {
           path = Environment.GetFolderPath(Environment.SpecialFolder.Startup);     
        }

        public ActionResult CreateRegisterKey()
        {

            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(this.path)) {
                try
                {
                    key.SetValue("discord_bot",this.path);
                }

               catch(Exception ex)
                {
                    return new BadRequestResult();
                }
                                

            }

            return new OkResult();

        }



        public ActionResult DestructRegisterKey()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(this.path))
            {
                try
                {
                    key.DeleteSubKey("discord_bot");
                }

                catch (Exception ex)
                {
                    return new BadRequestResult();
                }


            }

            return new OkResult();

        }


        public bool ExistsRegisterKey(string path)
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(this.path))
            {
                    if (key.ValueCount == 1)
                        return true;
            }
            return false;

        }


       
    }
}
