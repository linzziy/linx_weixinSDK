using Linx.Weixin.Framework;
using Microsoft.AspNet.Mvc;
using System.IO;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WeixinDemo
{
    public class WeixinController : Controller
    {
        // GET: /<controller>/
        public string Index(string signature, string timestamp, string nonce, string echostr)
        {
            StreamReader reader = new StreamReader(Request.Body);

            string inputXml = reader.ReadToEnd();

            return new Entrance().In(signature, timestamp, nonce, echostr, inputXml);
        }
    }
}
