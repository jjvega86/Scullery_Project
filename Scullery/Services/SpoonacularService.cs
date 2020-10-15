using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Scullery.Services
{
    public class SpoonacularService
    {
        private readonly HttpClient client;
        public SpoonacularService()
        {
            client = new HttpClient();

        }


    }
}
