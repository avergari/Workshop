using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using UItoServiceLayer.Models;

namespace UItoServiceLayer.Business
{
    public class ItemOperation
    {

        public static ResponseModel InsertItem(ItensModel oItem, String token)
        {
            ResponseModel responseItem = new ResponseModel();

            try
            {
                var restClient = new RestClient("https://EUL1240:50000/b1s/v1");
                var restRequest = new RestRequest("/Items", Method.POST);

                CookieContainer cookiecon = new CookieContainer();
                cookiecon.Add(new Cookie("B1SESSION", token, "/b1s/v1/Items", "EUL1240"));

                restClient.CookieContainer = cookiecon;
                restRequest.AddHeader("content-type", "application/json");
                restRequest.AddParameter("application/json", JsonConvert.SerializeObject(oItem), ParameterType.RequestBody);

                ServicePointManager.ServerCertificateValidationCallback += new System.Net.Security.RemoteCertificateValidationCallback(ValidateServerCertificate);

                IRestResponse restResponse = restClient.Execute(restRequest);

                if (restResponse.StatusCode == HttpStatusCode.OK || restResponse.StatusCode == HttpStatusCode.Created)
                {
                    var result = JsonConvert.DeserializeObject<ItensModel>(restResponse.Content);

                    responseItem.Status = 1;
                    responseItem.Msg = String.Empty;
                    responseItem.NumErro = 0;
                    responseItem.data = result.ItemCode.ToString();
                }
                else
                {
                    var result = JsonConvert.DeserializeObject<ExceptionModel>(restResponse.Content);

                    responseItem.Status = 0;
                    responseItem.Msg = result.error.message.value;
                    responseItem.NumErro = result.error.code;
                    responseItem.data = String.Empty;
                }
            }
            catch (Exception ex)
            {
                responseItem.Status = 0;
                responseItem.Msg = ex.Message;
                responseItem.NumErro = 99;
                responseItem.data = String.Empty;
            }

            return responseItem;
        }

        public static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
    }
}
