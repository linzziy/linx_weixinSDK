using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace Linx.Weixin.Framework
{
    public class WxBaseConfig
    {
        /// <summary>
        /// 微信配置Token信息
        /// </summary>
        public static string Token
        {
            set;get;
        }
        /// <summary>
        /// 微信AppID
        /// </summary>
        public static string AppID
        {
            set; get;
        }
        /// <summary>
        /// 微信AppSecret
        /// </summary>
        public static string AppSecret
        {
            set; get;
        }
    }
}