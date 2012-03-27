using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

namespace LocalKuduRequest
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {                
                //Uri scm = new Uri(HttpContext.Current.Request.Url,"live/scm/info");
                string scm = "http://localhost/live/scm/info";
                string user = HttpContext.Current.Request.Params["user"];
                string password = HttpContext.Current.Request.Params["password"];

                HttpWebRequest kuduRequest = HttpWebRequest.Create(scm) as HttpWebRequest;
                string credParameter = Convert.ToBase64String(Encoding.ASCII.GetBytes(user+ ":" + password));
                kuduRequest.Headers.Add("Authorization", "Basic "+ credParameter);

                Label1.Text = "Requesting: " + scm + "<br />";
                var response = kuduRequest.GetResponse() as HttpWebResponse;
                Label1.Text += "Code:" + response.StatusCode + "<br />";
                StreamReader reader = new StreamReader(response.GetResponseStream());
                Label1.Text += reader.ReadToEnd();

            }
            catch (WebException ex)
            {
                Label1.Text += string.Format("<br/> Error code: {0} <br/>", ex.Status);
                Label1.Text += ex.ToString().Replace("---", "<br />");               

                try
                {
                    HttpWebResponse res = (HttpWebResponse)ex.Response;
                    Label1.Text += "<br /> " + (int)res.StatusCode;
                }
                catch (Exception ee)
                {
                    Label1.Text += "<br />  " + ee.ToString();
                }
                
               
            }

        }
    }
}
