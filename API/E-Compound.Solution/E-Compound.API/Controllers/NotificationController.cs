using System.IO;
using System.Net;
using System.Text;
using System.Web.Http;
using E_Compound.API.Infrastructure;
using E_Compound.API.Models;

namespace E_Compound.API.Controllers
{
    public class NotificationController : BaseApiController
    {
        // GET: Notification
        public NotificationController()
        {

        }

        [Route("api/Notification", Name = "Notification")]
        [HttpPost]
        public IHttpActionResult SendNotification([FromBody] NotificationModel notificationModel)
        {
            var request = WebRequest.Create("https://onesignal.com/api/v1/notifications") as HttpWebRequest;
            request.KeepAlive = true;
            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";
            request.Headers.Add("authorization", "Basic NTAyOTVjZjUtNjFhOS00Njg4LWFiYTktMjliZTkxYmY4YjEx");
            byte[] byteArray = Encoding.UTF8.GetBytes("{"
                                                      + "\"app_id\": \"e3f4dc1a-d77d-4b84-b098-4d43140d9e16\","
                                                      + "\"contents\": {\"en\": \"" + notificationModel.Message + "\"},"
                                                      + "\"headings\": {\"en\": \""+ notificationModel .Title+ "\"},"
                                        + "\"included_segments\": [\"All\"]}");

            string responseContent = null;

            try
            {
                using (var writer = request.GetRequestStream())
                {
                    writer.Write(byteArray, 0, byteArray.Length);
                }

                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        responseContent = reader.ReadToEnd();
                    }
                }
            }
            catch (WebException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(new StreamReader(ex.Response.GetResponseStream()).ReadToEnd());
            }

            System.Diagnostics.Debug.WriteLine(responseContent);
            return Ok();

        }
    }
}