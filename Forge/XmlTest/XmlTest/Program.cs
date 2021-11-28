using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace XmlTest
{
    class Program
    {
        static void Main(string[] args)
        {
            // Gets current directory and loads xml data
            string path = @"C:\Users\yongcheol\source\repos\HBS\Forge\XmlTest\XmlTest\M_gas_calculation.xml";

            // Loading from a file, you can also load from a stream
            XmlDocument doc = new XmlDocument();
            doc.Load(path);

            //XmlNode node = doc.DocumentElement.SelectSingleNode("BFG");

            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                if (node.Name == "M_gas_calculation")
                {
                    foreach (XmlNode subNode in node.ChildNodes)
                    {
                        switch (subNode.Name)
                        {
                            case "BFG":
                                foreach (XmlNode subsubNode in subNode.ChildNodes)
                                {
                                    Debug.WriteLine(subsubNode.Name + ", " + subsubNode.InnerText);
                                }
                                break;
                            case "COG":
                                break;
                            case "X_Gas":
                                break;
                        }

                    }
                }
            }



            doc.Load(path);
            XmlNode sNode = doc.SelectSingleNode("HBS_v1.1.0.3/M_gas_calculation/BFG/CH4");

            if (sNode != null)
            {
                sNode.InnerText = "VALUE";
                Console.WriteLine(sNode.InnerText);
            }
            doc.Save(path);
        }
    }
}
