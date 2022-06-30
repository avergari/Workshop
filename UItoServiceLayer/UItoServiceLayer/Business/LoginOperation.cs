using Newtonsoft.Json;
using RestSharp;
using SAPbouiCOM.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using UItoServiceLayer.Core;
using UItoServiceLayer.Models;

namespace UItoServiceLayer.Business
{
    public class LoginOperation
    {

        public static ResponseModel SAPLogin(LoginModel login)
        {
            ResponseModel responseLogin = new ResponseModel();

            try
            {
                var restClient = new RestClient("https://EUL1240:50000/b1s/v1");
                var restRequest = new RestRequest("/Login", Method.POST);
                restRequest.AddHeader("content-type", "application/json");
                restRequest.AddParameter("application/json", JsonConvert.SerializeObject(login), ParameterType.RequestBody);

                ServicePointManager.ServerCertificateValidationCallback += new System.Net.Security.RemoteCertificateValidationCallback(ValidateServerCertificate);
                IRestResponse response = restClient.Execute(restRequest);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    responseLogin.Status = 1;
                    responseLogin.Msg = String.Empty;
                    responseLogin.NumErro = 0;
                    responseLogin.data = response.Cookies.FirstOrDefault().Value;
                }
                else
                {
                    var result = JsonConvert.DeserializeObject<ExceptionModel>(response.Content);

                    responseLogin.Status = 0;
                    responseLogin.Msg = result.error.message.value;
                    responseLogin.NumErro = result.error.code;
                    responseLogin.data = String.Empty;
                }
            }
            catch (Exception ex)
            {
                responseLogin.Status = 0;
                responseLogin.Msg = ex.Message;
                responseLogin.NumErro = 0;
                responseLogin.data = String.Empty;
            }

            return responseLogin;
        }

        public static void GetUIConnection()
        {
            try
            {
                var ServiceLayerAddress = "https://EUL1240:50000/b1s/v1";
                var UIToken = Application.SBO_Application.Company.GetServiceLayerConnectionContext(ServiceLayerAddress);

                string[] cookieItems = UIToken.Split(';');
                string[] parts = cookieItems[0].Split('=');

                CommonClass.SLTOKEN = parts[1].Trim();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Validate Server Certificate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="certificate"></param>
        /// <param name="chain"></param>
        /// <param name="sslPolicyErrors"></param>
        /// <returns></returns>
        public static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
    }
}
