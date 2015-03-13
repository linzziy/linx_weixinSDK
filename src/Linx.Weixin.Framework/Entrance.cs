using System;
using System.IO;
using System.Security.Cryptography;

namespace Linx.Weixin.Framework
{
    public class Entrance
    {
        /// <summary>
        /// 微信接入的入口
        /// </summary>
        /// <param name="signature">signature</param>
        /// <param name="timestamp">timestamp</param>
        /// <param name="nonce">nonce</param>
        /// <param name="echostr">echostr</param>
        /// <param name="inputXml">微信post过来数据</param>
        /// <returns></returns>
        public string In(string signature, string timestamp, string nonce, string echostr, string inputXml)
        {
            string token = WxBaseConfig.Token;

            string[] arrKey = new string[] { token, timestamp, nonce };
            Array.Sort(arrKey);

            string strkey = string.Join("", arrKey);
            strkey = SecurityHelper.SHA1(strkey);

            //Compare strkey signature
            if (string.Compare(strkey, signature, true) == 0)
            {
                if (!string.IsNullOrEmpty(inputXml))
                {
                    return XmlResponse(inputXml);
                }

                return echostr;
            }

            return string.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string XmlResponse(string xml)
        {
            //获取MsgType
            //Logger.Instance.Info("消息：" + xml);

            string msgType = XmlHelper.ReadByXml(xml, "/xml/MsgType", "");

            switch (msgType)
            {
                //事件支持
                case "event":
                    return OnAttention(xml);
                default:
                    return "不被支持的关键字！";
            }
        }

        /// <summary>
        /// 当用户关注微信帐号的时候触发，事件响应
        /// </summary>
        /// <param name="xml"></param>
        private string OnAttention(string xml)
        {
            //WxEventInfo wxevent = XmlEntityHelper.ConvertXml2Entity<WxEventInfo>(xml);

            //WxTextMsgInfo msg = new WxTextMsgInfo();
            //msg.ToUserName = wxevent.FromUserName;
            //msg.FromUserName = wxevent.ToUserName;
            //msg.CreateTime = DateTime.Now.Ticks;
            //msg.MsgType = "text";

            ////如果是关注，则发送欢迎消息
            //switch (wxevent.Event)
            //{
            //    //用户关注
            //    case EventEnum.subscribe:
            //        msg.Content = WxMsgConfig.SubscribedMsg;
            //        break;
            //    case EventEnum.CLICK:
            //        msg.Content = OnEvent(wxevent);
            //        break;
            //    default:
            //        msg.Content = "暂未处理的事件：Event" + wxevent.Event + ";EventKey:" + wxevent.EventKey;
            //        break;
            //}

            //string rst = XmlEntityHelper.ConvertEntity2Xml<WxTextMsgInfo>(msg);
            string rst = "";

            //Logger.Instance.Info("消息回复：" + rst);

            return rst;
        }

        private string OnEvent(WxEventInfo wxevent)
        {
            string rstMsg = "";
            //switch (wxevent.EventKey)
            //{
            //    case EventKeyEnum.IdBinding:
            //        rstMsg = new BindingService().GetBindText(wxevent.FromUserName);
            //        break;

            //    default:
            //        break;
            //}
            return rstMsg;
        }
    }
}