using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ReaderClassLibrary.Reader
{
    public class GenericApiReader<T> where T : class
    {
        private HttpClient _httpClient { get; }
        private string uri { get; set; }

        private IMapper Mapper { get; set; }

        public GenericApiReader(HttpClient httpClient, string baseuri)
        {
            //Init mapper
            var config = new MapperConfiguration(cfg => {
           
            });
            Mapper =  new Mapper(config);

            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Accept.Clear();
          
            uri = baseuri;
        
            _httpClient.BaseAddress = new Uri(uri);



        }


        public virtual async Task<T> Get()
        {

            try
            {
              
                var resultat  = await _httpClient.GetFromJsonAsync<T>(uri);

                return resultat;
            }
            catch (Exception ex){
                return null;
            }
      


        }


        public virtual async Task<IEnumerable<T>> GetAll()
        {

            try
            {

                IEnumerable<T> resultat = await _httpClient.GetFromJsonAsync<IEnumerable<T>>(uri);

                return resultat;
            }
            catch (Exception ex)
            {
                return null;
            }



        }




    }
}
