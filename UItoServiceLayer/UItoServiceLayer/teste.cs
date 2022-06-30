using SAPbouiCOM.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace UItoServiceLayer
{
    class teste
    {

        private int PlayServiceLayer()
        {
            string serviceLayerAddress = "https://hanaserver:50000/b1s/v1";
            string sConnectionContext = null;


            //Step 1: Get a session cookie from service layer
            try
            {
                sConnectionContext = Application.SBO_Application.Company.GetServiceLayerConnectionContext(serviceLayerAddress);
            }
            catch (System.Exception ex)
            {
            }

            if (sConnectionContext == null)
            {
                Application.SBO_Application.MessageBox("Get service layer connection context error", 1, "Err", "", "");
                return -1;
            }


            //Step 2: Send requests with session cookie
            try
            {
                var request = WebRequest.Create(serviceLayerAddress + "/Items?$top=1") as HttpWebRequest;
                request.AllowAutoRedirect = false;
                request.Timeout = 30 * 1000;
                request.ServicePoint.Expect100Continue = false;
                request.CookieContainer = new CookieContainer();
                //ServicePointManager.ServerCertificateValidationCallback += BypassSslCallback;
                
                string[] cookieItems = sConnectionContext.Split(';');
                foreach (var cookieItem in cookieItems)
                {
                    string[] parts = cookieItem.Split('=');
                    if (parts.Length == 2)
                    {
                        request.CookieContainer.Add(request.RequestUri, new Cookie(parts[0].Trim(), parts[1].Trim()));
                    }
                }

                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    Application.SBO_Application.MessageBox("Get item error", 1, "Err", "", "");
                    return -1;
                }

                string responseContent = null;
                using (var reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    responseContent = reader.ReadToEnd();
                }
                Application.SBO_Application.MessageBox(responseContent, 1, "Ok", "", "");
            }
            catch (System.Exception ex)
            {
                Application.SBO_Application.MessageBox(ex.ToString(), 1, "Err", "", "");
            }
            return 0;
        }

    }
}
