using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Resources;

namespace ModuleBotClassLibrary.RessourceManager
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Parameter, AllowMultiple = false)]
    public class DescriptionCustomAttribute : Attribute
    {
        public string Description { get; set; } 
        public DescriptionCustomAttribute(string key)
        {
            Description = String.IsNullOrEmpty(botResource.ResourceManager.GetString(key)) == true ? "" : botResource.ResourceManager.GetString(key);
        }

    }
}
