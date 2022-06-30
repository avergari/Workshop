using Newtonsoft.Json;
using RestSharp;
using ServiceLayerTrainning.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayerTrainning.Business
{
    class SAPClass
    {
        public static String SessionID;

        private String MainURL;

        public SAPClass(String ServiceLayerURL)
        {
            MainURL = ServiceLayerURL;
        }

        #region ' Login '

        public ResponseModel SAPLogin(LoginModel login)
        {
            ResponseModel responseLogin = new ResponseModel();

            try
            {
                var restClient = new RestClient(MainURL);
                var restRequest = new RestRequest("/Login", Method.POST);
                restRequest.AddHeader("content-type", "application/json");
                restRequest.AddParameter("application/json", JsonConvert.SerializeObject(login), ParameterType.RequestBody);

                ServicePointManager.ServerCertificateValidationCallback += new System.Net.Security.RemoteCertificateValidationCallback(ValidateServerCertificate);


                RestSharp.IRestResponse response = restClient.Execute(restRequest);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    responseLogin.Status = 0;
                    responseLogin.Msg = "";
                    responseLogin.NumErro = 0;
                    responseLogin.data = response.Cookies.FirstOrDefault().Value;

                    SessionID = responseLogin.data;
                }


                return responseLogin;
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region ' Order Methods '

        public ResponseModel InsertOrder(OrdersModel order)
        {
            ResponseModel responseLogin = new ResponseModel();

            try
            {
                var restClient = new RestClient(MainURL);
                var restRequest = new RestRequest("/Orders", Method.POST);

                CookieContainer cookiecon = new CookieContainer();
                cookiecon.Add(new Cookie("B1SESSION", SessionID, "/b1s/v1/Orders", "groupeicc.local"));

                restClient.CookieContainer = cookiecon;
                restRequest.AddHeader("content-type", "application/json");
                restRequest.AddParameter("application/json", JsonConvert.SerializeObject(order), ParameterType.RequestBody);

                ServicePointManager.ServerCertificateValidationCallback += new System.Net.Security.RemoteCertificateValidationCallback(ValidateServerCertificate);

                IRestResponse restResponse = restClient.Execute(restRequest);

                if (restResponse.StatusCode == HttpStatusCode.OK || restResponse.StatusCode == HttpStatusCode.Created)
                {
                    var result = JsonConvert.DeserializeObject<OrdersModel>(restResponse.Content);

                    responseLogin.Status = 1;
                    responseLogin.Msg = String.Empty;
                    responseLogin.NumErro = 0;
                    responseLogin.data = result.DocEntry.ToString();
                }
                else
                {
                    var result = JsonConvert.DeserializeObject<ExceptionModel>(restResponse.Content);

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
                responseLogin.NumErro = 99;
                responseLogin.data = String.Empty;
            }

            return responseLogin;
        }

        public ResponseModel InsertProductionOrder(ProductionOrderModel productionOrder)
        {
            ResponseModel responseLogin = new ResponseModel();

            try
            {
                var restClient = new RestClient(MainURL);
                var restRequest = new RestRequest("/ProductionOrders", Method.POST);

                CookieContainer cookiecon = new CookieContainer();
                cookiecon.Add(new Cookie("B1SESSION", SessionID, "/b1s/v1/ProductionOrders", "groupeicc.local"));

                restClient.CookieContainer = cookiecon;
                restRequest.AddHeader("content-type", "application/json");
                restRequest.AddParameter("application/json", JsonConvert.SerializeObject(productionOrder), ParameterType.RequestBody);

                ServicePointManager.ServerCertificateValidationCallback += new System.Net.Security.RemoteCertificateValidationCallback(ValidateServerCertificate);

                IRestResponse restResponse = restClient.Execute(restRequest);

                if (restResponse.StatusCode == HttpStatusCode.OK || restResponse.StatusCode == HttpStatusCode.Created)
                {
                    var result = JsonConvert.DeserializeObject<ProductionOrderModel>(restResponse.Content);

                    responseLogin.Status = 1;
                    responseLogin.Msg = String.Empty;
                    responseLogin.NumErro = 0;
                    responseLogin.data = result.AbsoluteEntry.ToString();
                }
                else
                {
                    var result = JsonConvert.DeserializeObject<ExceptionModel>(restResponse.Content);

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
                responseLogin.NumErro = 99;
                responseLogin.data = String.Empty;
            }

            return responseLogin;
        }

        #endregion


        public BusinessPartnersModel GetBusinessPartners()
        {
            ResponseModel responseLogin = new ResponseModel();

            try
            {
                var restClient = new RestClient(MainURL);
                var restRequest = new RestRequest("/BusinessPartners", Method.GET);

                CookieContainer cookiecon = new CookieContainer();
                cookiecon.Add(new Cookie("B1SESSION", SessionID, "/b1s/v1/BusinessPartners", "groupeicc.local"));

                restClient.CookieContainer = cookiecon;


                ServicePointManager.ServerCertificateValidationCallback += new System.Net.Security.RemoteCertificateValidationCallback(ValidateServerCertificate);

                IRestResponse restResponse = restClient.Execute(restRequest);

                if (restResponse.StatusCode == HttpStatusCode.OK || restResponse.StatusCode == HttpStatusCode.Created)
                {
                    var result = JsonConvert.DeserializeObject<BusinessPartnersModel>(restResponse.Content);

                    responseLogin.Status = 1;
                    responseLogin.Msg = String.Empty;
                    responseLogin.NumErro = 0;

                    return result;
                }
                else
                {
                    var result = JsonConvert.DeserializeObject<ExceptionModel>(restResponse.Content);

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
                responseLogin.NumErro = 99;
                responseLogin.data = String.Empty;
            }

            return null;
        }

        public ItemsModel GetBOMItems()
        {
            ResponseModel responseLogin = new ResponseModel();

            try
            {
                var restClient = new RestClient(MainURL);
                var restRequest = new RestRequest("/Items?$filter=ComponentWarehouse eq 'bomcw_BOM' and ProcurementMethod eq 'bom_Make'", Method.GET);

                CookieContainer cookiecon = new CookieContainer();
                cookiecon.Add(new Cookie("B1SESSION", SessionID, "/b1s/v1/Items", "groupeicc.local"));

                restClient.CookieContainer = cookiecon;


                ServicePointManager.ServerCertificateValidationCallback += new System.Net.Security.RemoteCertificateValidationCallback(ValidateServerCertificate);

                IRestResponse restResponse = restClient.Execute(restRequest);

                if (restResponse.StatusCode == HttpStatusCode.OK || restResponse.StatusCode == HttpStatusCode.Created)
                {
                    var result = JsonConvert.DeserializeObject<ItemsModel>(restResponse.Content);

                    responseLogin.Status = 1;
                    responseLogin.Msg = String.Empty;
                    responseLogin.NumErro = 0;

                    return result;
                }
                else
                {
                    var result = JsonConvert.DeserializeObject<ExceptionModel>(restResponse.Content);

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
                responseLogin.NumErro = 99;
                responseLogin.data = String.Empty;
            }

            return null;
        }




        public static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
    }
}
