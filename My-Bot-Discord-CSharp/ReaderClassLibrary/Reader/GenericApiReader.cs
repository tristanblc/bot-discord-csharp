using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        public virtual async Task<T> Get(Guid id)
        {

            try
            {

                var uri_get = uri + "id?=" + id.ToString();

                var resultat = await _httpClient.GetFromJsonAsync<T>(uri_get);

                return resultat;
            }
            catch (Exception ex)
            {
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


        public virtual async Task<ActionResult> Add(T item) 
        {
            try
            {
                var resultat = await _httpClient.PostAsJsonAsync<T>(uri, item); 
                if(resultat.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    
                    return new BadRequestResult();
                }
               
                return new OkResult();
              

            }
            catch(Exception ex)
            {
                return new BadRequestResult();
            }

        }


        public virtual async Task<ActionResult> Update(T item)
        {

            try
            {
                var resultat = await _httpClient.PutAsJsonAsync<T>(uri,item);
                if (resultat.StatusCode != System.Net.HttpStatusCode.OK)
                {

                    return new BadRequestResult();
                }

                return new OkResult();
            }
            catch (Exception ex)
            {
                return new BadRequestResult();
            }
        }



        public virtual async Task<ActionResult> Delete(T item)
        {
            try
            {
                var resultat = await _httpClient.DeleteAsync(uri);
                if (resultat.StatusCode != System.Net.HttpStatusCode.OK)
                {

                    return new BadRequestResult();
                }

                return new OkResult();
            }
            catch(Exception ex)
            {
                return new BadRequestResult();
            }
        }



    }
}
