using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Linx.Weixin.Framework
{
    public class WxEventInfo
    {
        /// <summary>
        /// 接收人
        /// </summary>
        public string ToUserName { get; set; }
        /// <summary>
        /// 发送人
        /// </summary>
        public string FromUserName { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        public string CreateTime { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public string MsgType { get; set; }
        /// <summary>
        /// 事件
        /// </summary>
        public EventEnum Event { get; set; }

        public EventKeyEnum EventKey { get; set; }
    }
}
