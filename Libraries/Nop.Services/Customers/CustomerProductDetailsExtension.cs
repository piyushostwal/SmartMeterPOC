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
        //public static async Task<bool> UpdateDeviceStatus(Guid deviceId, bool status)
        //{
        //    Uri serviceRoot = new Uri("https://smarthome-46be4.firebaseio.com/smartmeters/" + deviceId + ".json");
        //    //var token = await GetAppTokenAsync();
        //    string requestUrl = "https://smarthome-46be4.firebaseio.com/smartmeters/" + deviceId + ".json";

        //    HttpClient hc = new HttpClient();
        //    //hc.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        //    hc.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer");

        //    var method = new HttpMethod("PATCH");

        //    var request = new HttpRequestMessage(method, requestUrl)
        //    {
        //        Content = new StringContent("{ \"isActive\": \"true \" }", Encoding.UTF8, "application/json")
        //    };

        //    HttpResponseMessage hrm = await hc.SendAsync(request);
        //    if (hrm.IsSuccessStatusCode)
        //    {
        //        string jsonresult = await hrm.Content.ReadAsStringAsync();
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}
    }
}
