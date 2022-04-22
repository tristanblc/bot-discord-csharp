using BotClassLibrary;
using ReaderClassLibrary.Interfaces;
using ReaderClassLibrary.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReaderClassLibrary.Services
{
    public class JourFerieService : GenericApiReader<JourFerie>, IGenericInterface<JourFerie>
    {
        public JourFerieService(HttpClient httpClient, string uri) : base(httpClient, uri)
        {

        }

        public Task<JourFerie> GetApprochDateTime()
        {
            var list = this.GetAll().Result.ToList();

            var delta = 1000000;

            JourFerie jourferie = null;
            foreach (var item in list)
            {
                if(delta > (item.DateTime - DateTime.Now).Days)
                {
                    jourferie = item;

                }

            }


            return Task.FromResult(jourferie);

          
        }
    }
}
