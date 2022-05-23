using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Library.BusinessLogicLayer.Blobs
{
    public class BlobService : IBlobService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient client;
        public BlobService(IConfiguration config)
        {
            this._configuration = config;
            client = new HttpClient();
            client.BaseAddress = new Uri(_configuration["Services:FileService"]);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<BlobDto> GetById(long id)
        {
            var response = await client.GetAsync($"api/admin/blobs/{id}");
            if (response.IsSuccessStatusCode)
            {
                var blob = JsonConvert.DeserializeObject<BlobDto>(await response.Content.ReadAsStringAsync());
                return blob;
            }
            return null;
        }
    }
}
