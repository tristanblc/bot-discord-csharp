using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Net.Mime.MediaTypeNames;

namespace BotBlazonApplication.Services.Classes
{
    public class Citation : BaseEntity
    {

    
        public string citation { get; private set; }
        public string auteur { get; private set; }



        public Citation()
        {

        }

    }
}