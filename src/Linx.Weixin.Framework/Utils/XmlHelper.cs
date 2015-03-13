using System;
using System.Data;
using System.Xml;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Collections;


namespace Linx.Weixin.Framework
{
    /// <summary>
    /// Read可用，其他待扩展 2014.11.20
    /// </summary>
    public class XmlHelper
    {
        private XmlHelper()
        {
        }

        #region other
        /*****************************************************************
         * Exemple:
         *   RockXML.List(path, "/Node", "name", "")
         *   RockXML.List(path, "/Node/Element[@Attribute='Name']", "name", "Attribute")
         *****************************************************************/
        public static DataTable List(string strPath, string strNode, string strAttributeKey, string strAttributeValue)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(strAttributeKey.ToUpper(), typeof(string));
            dt.Columns.Add((strAttributeValue == string.Empty ? "Value" : strAttributeValue).ToUpper(), typeof(string));

            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(strPath);
                XmlNode xn = doc.SelectSingleNode(strNode);

                foreach (XmlNode n in xn.ChildNodes)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = strAttributeKey.Equals(string.Empty) ? string.Empty : n.Attributes[strAttributeKey].Value;
                    dr[1] = strAttributeValue.Equals(string.Empty) ? n.InnerText : n.Attributes[strAttributeValue].Value;
                    dt.Rows.Add(dr);
                }
            }
            catch { }

            return dt;
        }

        public static bool Create(string xmlFileName, string rootNodeName, string version, string encoding)
        {
            bool isSuccess = false;
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                XmlDeclaration xmlDeclaration = xmlDoc.CreateXmlDeclaration(version, encoding, null);
                XmlNode root = xmlDoc.CreateElement(rootNodeName);
                xmlDoc.AppendChild(xmlDeclaration);
                xmlDoc.AppendChild(root);
                xmlDoc.Save(xmlFileName);
                isSuccess = true;
            }
            catch (Exception ex)
            {
                throw ex; //这里可以定义你自己的异常处理
            }
            return isSuccess;
        }
        #endregion

        #region Read
        /*****************************************************************
         * Exemple:
         *   RockXML.Read(path, "/Node", "")
         *   RockXML.Read(path, "/Node/Element[@Attribute='Name']", "Attribute")
         *****************************************************************/
        /// <summary>
        /// 从路径中加载XML
        /// </summary>
        /// <param name="strPath"></param>
        /// <param name="strNode"></param>
        /// <param name="strAttribute"></param>
        /// <returns></returns>
        public static string ReadByPath(string strPath, string strNode, string strAttribute)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(strPath);

            return ReadCore(doc, strNode, strAttribute);
        }
        /// <summary>
        /// 从xml字符串中加载XML
        /// </summary>
        /// <param name="strXml"></param>
        /// <param name="strNode"></param>
        /// <param name="strAttribute"></param>
        /// <returns></returns>
        public static string ReadByXml(string strXml, string strNode, string strAttribute)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(strXml);

            return ReadCore(doc, strNode, strAttribute);
        }
        private static string ReadCore(XmlDocument doc, string strNode, string strAttribute)
        {
            string value = string.Empty;

            XmlNode xn = doc.SelectSingleNode(strNode);
            value = (strAttribute.Equals(string.Empty) ? xn.InnerText : xn.Attributes[strAttribute].Value);

            return value;
        }
        #endregion

        #region modify
        /*****************************************************************
         * Exemple:
         *   RockXML.Insert(path, "/Node", "Element", "", "Value")
         *   RockXML.Insert(path, "/Node", "Element", "Attribute", "Value")
         *   RockXML.Insert(path, "/Node", "", "Attribute", "Value")
         *****************************************************************/
        public static void Insert(string strPath, string strNode, string strElement, string strAttribute, string strValue)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(strPath);
                XmlNode xn = doc.SelectSingleNode(strNode);
                if (strElement.Equals(string.Empty))
                {
                    if (!strAttribute.Equals(string.Empty))
                    {
                        XmlElement xe = (XmlElement)xn;
                        xe.SetAttribute(strAttribute, strValue);
                    }
                }
                else
                {
                    XmlElement xe = doc.CreateElement(strElement);
                    if (strAttribute.Equals(string.Empty))
                        xe.InnerText = strValue;
                    else
                        xe.SetAttribute(strAttribute, strValue);
                    xn.AppendChild(xe);
                }
                doc.Save(strPath);
            }
            catch { }
        }

        /*****************************************************************
         * Exemple:
         *   RockXML.Modify(path, "/Node", "", "Value")
         *   RockXML.Modify(path, "/Node", "Attribute", "Value")
         *****************************************************************/
        public static void Modify(string strPath, string strNode, string strAttribute, string strValue)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(strPath);
                XmlNode xn = doc.SelectSingleNode(strNode);
                XmlElement xe = (XmlElement)xn;
                if (strAttribute.Equals(string.Empty))
                    xe.InnerText = strValue;
                else
                    xe.SetAttribute(strAttribute, strValue);
                doc.Save(strPath);
            }
            catch { }
        }

        /*****************************************************************
         * Exemple:
         * RockXML.Delete(path, "/Node", "")
         * RockXML.Delete(path, "/Node", "Attribute")
         *****************************************************************/
        public static void Delete(string strPath, string strNode, string strAttribute)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(strPath);
                XmlNode xn = doc.SelectSingleNode(strNode);
                XmlElement xe = (XmlElement)xn;
                if (strAttribute.Equals(string.Empty))
                    xn.ParentNode.RemoveChild(xn);
                else
                    xe.RemoveAttribute(strAttribute);
                doc.Save(strPath);
            }
            catch { }
        }
        #endregion
    }
}