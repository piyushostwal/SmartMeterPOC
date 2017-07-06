using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Nop.Services.Customers
{
    public static class CustomerProductDetailsExtension
    {
        public async Task<bool> UpdateDeviceStatus(Guid deviceId)
        {
            Uri serviceRoot = new Uri("https://smarthome-46be4.firebaseio.com/smartmeters/" + deviceId + ".json");
            //var token = await GetAppTokenAsync();
            string requestUrl = "https://smarthome-46be4.firebaseio.com/smartmeters/" + deviceId + ".json";

            HttpClient hc = new HttpClient();
            //hc.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            hc.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer");

            var method = new HttpMethod("PATCH");

            var request = new HttpRequestMessage(method, requestUrl)
            {
                Content = new StringContent("{ \"extension_33e037a7b1aa42ab96936c22d01ca338_Compania\": \"Empresa1\" }", Encoding.UTF8, "application/json")
            };

            HttpResponseMessage hrm = await hc.GetAsync(new Uri(requestUrl));
            if (hrm.IsSuccessStatusCode)
            {
                string jsonresult = await hrm.Content.ReadAsStringAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
