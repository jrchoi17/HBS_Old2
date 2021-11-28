using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace HBS_Shared
{
    public class CFileIO
    {
        public static bool IsFileExisted(string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);

            return fileInfo.Exists;
        }

        public static List<string> GetParsingData(List<string> contents, string keyword)
        {
            List<string> parsedData = new List<string>();
            int indexStart = contents.IndexOf(keyword.ToLower() + "{");
            int indexEnd = contents.IndexOf(@"}/" + keyword.ToLower());

            if (indexStart == -1 || indexEnd == -1)
                return null;

            List<string> paragraph = contents.GetRange(indexStart + 1, indexEnd - indexStart - 1);
            for (int i = 0; i < paragraph.Count; i++)
                if (paragraph[i].Trim()[0] != '#')
                    parsedData.Add(paragraph[i].Trim());
            return parsedData;
        }

        public static List<double> GetDoubleExcelFormatColumn(List<string> contents, string columnKey)
        {
            List<string> strData = GetStringExcelFormatColumn(contents, columnKey);
            List<double> dData = strData.Select(x => double.Parse(x)).ToList();
            
            return dData;
        }

        public static List<string> GetStringExcelFormatColumn(List<string> contents, string columnKey)
        {
            List<string> columnHead = contents[0].Split('\t').ToList();
            int index = columnHead.IndexOf(columnKey);
            List<string> result = new List<string>();

            for (int i = 1; i < contents.Count; i++)
            {
                List<string> columns = contents[i].Split('\t').ToList();
                result.Add(columns[index]);
            }

            return result;
        }

        public static double GetDoubleValueFromKeyword(List<string> contents, string keyword)
        {
            double d;
            string str = GetStringValueFromKeyword(contents, keyword);

            if (!double.TryParse(str, out d))
                throw CException.Show(CException.Type.NotNumber);

            return d;
        }

        public static string GetStringValueFromKeyword(List<string> contents, string keyword)
        {
            foreach (string content in contents)
            {
                string item = content.Split('\t')[0];
                string value = content.Split('\t')[1];

                if (item == keyword)
                    return value;
            }

            throw CException.Show(CException.Type.Null);
        }

        public static XmlNodeList GetXmlAllNodeList(string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);

            if (!fileInfo.Exists)
                throw CException.Show(CException.Type.NoFile);

            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);

            return doc.DocumentElement.ChildNodes;
        }

        public static XmlNodeList GetXmlSubNodeList(XmlNodeList parents, string keyword)
        {
             if (parents == null)
                return null;

            foreach(XmlNode node in parents)
            {
                if (node.Name == keyword)
                    return node.ChildNodes;
            }

            return null;
        }

        public static string GetXmlValueAsString(XmlNodeList nodes, string keyword)
        {
            if (nodes == null)
                throw CException.Show(CException.Type.Null);

            foreach (XmlNode node in nodes)
                if (node.Name == keyword)
                    return  node.InnerText;

            throw CException.Show(CException.Type.Null);
        }

        public static double GetXmlValueAsDouble(XmlNodeList nodes, string keyword)
        {
            string strValue = GetXmlValueAsString(nodes, keyword);
            double value = 0.0;

            if (double.TryParse(strValue, out value))
                return value;
            else
                throw CException.Show(CException.Type.NotNumber);
        }

        public static bool GetxmlValueAsBool(XmlNodeList nodes, string keyword)
        {
            string strValue = GetXmlValueAsString(nodes, keyword);
            bool value = false;

            if (bool.TryParse(strValue, out value))
                return value;
            else
                throw CException.Show(CException.Type.NotNumber);
        }

        public static int GetXmlValueAsInt(XmlNodeList nodes, string keyword)
        {
            string strValue = GetXmlValueAsString(nodes, keyword);
            int value = 0;

            if (int.TryParse(strValue, out value))
                return value;
            else
                throw CException.Show(CException.Type.NotNumber);
        }

        public static List<double> GetXml1DTableAsDoubleList(XmlNodeList nodes)
        {
            List<double> list_1D = new List<double>();
            if (nodes == null)
                throw CException.Show(CException.Type.Null);

            for (int i = 0; i < nodes.Count; i++)
            {
                XmlNode node = nodes[i];
                list_1D.Add(GetXmlValueAsDouble(nodes, node.Name));
            }

            return list_1D;
        }

        public static List<string> GetXml1DTableAsStringList(XmlNodeList nodes)
        {
            List<string> list_1D = new List<string>();
            if (nodes == null)
                throw CException.Show(CException.Type.Null);

            for (int i = 0; i < nodes.Count; i++)
            {
                XmlNode node = nodes[i];
                list_1D.Add(GetXmlValueAsString(nodes, node.Name));
            }

            return list_1D;
        }

        public static List<List<double>> GetXml2DTableAsDoubleList(XmlNodeList nodes)
        {
            List<List<double>> list_2D = new List<List<double>>();
            if (nodes == null)
                throw CException.Show(CException.Type.Null);

            for (int i = 0; i < nodes.Count; i++)
            {
                XmlNode node = nodes[i];
                XmlNodeList subNodes = GetXmlSubNodeList(nodes, node.Name);

                List<double> list_1D = new List<double>();
                for (int j = 0; j < subNodes.Count; j++)
                    list_1D.Add(GetXmlValueAsDouble(subNodes, subNodes[j].Name));

                list_2D.Add(list_1D);
            }

            return list_2D;
        }
    }
}