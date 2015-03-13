using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;

namespace Linx.Weixin.Framework
{
    public class XmlEntityHelper
    {
        /// <summary>
        /// 将XML转换为对象
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static T ConvertXml2Entity<T>(string xml) where T : new()
        {
            XmlDocument doc = new XmlDocument();
            PropertyInfo[] propinfos = null;
            doc.LoadXml(xml);
            XmlNodeList nodelist = doc.SelectNodes("/xml");
            T entity = new T();
            foreach (XmlNode node in nodelist)
            {
                //初始化propertyinfo
                if (propinfos == null)
                {
                    Type objtype = entity.GetType();
                    propinfos = objtype.GetProperties();
                }
                //填充entity类的属性
                foreach (PropertyInfo pi in propinfos)
                {
                    XmlNode cnode = node.SelectSingleNode(pi.Name);
                    //支持枚举
                    if (pi.PropertyType.IsEnum)
                    {
                        pi.SetValue(entity, Enum.Parse(pi.PropertyType, cnode.InnerText), null);
                        continue;
                    }
                    pi.SetValue(entity, Convert.ChangeType(cnode.InnerText, pi.PropertyType), null);
                }
            }
            return entity;
        }

        /// <summary>
        /// 构造微信消息
        /// </summary>
        /// <param name="t">对象实体</param>
        /// <returns>返回微信消息xml格式</returns>
        public static string ConvertEntity2Xml<T>(T t) where T : new()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<xml>");
            Type objtype = t.GetType();
            //填充entity类的属性
            foreach (PropertyInfo pi in objtype.GetProperties())
            {
                object obj = pi.GetValue(t,null);
                string value = obj == null ? "" : obj.ToString();
                if (pi.PropertyType.Name.ToLower() == "int64")
                    builder.Append("<" + pi.Name + ">" + value + "</" + pi.Name + ">");
                else
                    builder.Append("<" + pi.Name + "><![CDATA[" + value + "]]></" + pi.Name + ">");
            }
            builder.Append("</xml>");
            return builder.ToString();
        }
    }
}
