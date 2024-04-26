using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace MvcCoreLogicApps.Services
{
    public class ServiceLogicApps
    {
        private MediaTypeWithQualityHeaderValue header;

        public ServiceLogicApps()
        {
            this.header = new MediaTypeWithQualityHeaderValue("application/json");

        }

        public async Task SendEmailAsync
            (string email, string asunto, string mensaje)
        {
            //esto habria que almacenarlo en keyvault
            string urlLogicApp = "https://prod-197.westeurope.logic.azure.com:443/workflows/25869b60f9f841f5972c8031d8a796c4/triggers/When_a_HTTP_request_is_received/paths/invoke?api-version=2016-10-01&sp=%2Ftriggers%2FWhen_a_HTTP_request_is_received%2Frun&sv=1.0&sig=hF0jh8-vQTkMui1PyVq1TOeWHJA1LYPGGqqH_aqvG-U";

            var model = new
            {
                email = email,
                asunto = asunto,
                mensaje = mensaje
            };
            
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);

                string json = JsonConvert.SerializeObject(model);

                StringContent content = new StringContent
                    (json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await
                    client.PostAsync(urlLogicApp, content);
            }
        }
    }
}
