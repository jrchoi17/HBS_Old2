using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.ComponentModel;

namespace HBS_Shared
{
    public partial class ST_UD
    {
        public BrickConfigurationDataType BrickConfiguration { get; set; }

        public class BrickConfigurationDataType
        {
            [Category("Brick Configuration"),
            Description("Default length(mm), diameter(mm) and number of bricks")]
            public double Length { get; set; }

            [Category("Brick Configuration"),
            Description("Default length(mm), diameter(mm) and number of bricks")]
            public double Diameter { get; set; }

            [Category("Brick Configuration"),
            Description("Default length(mm), diameter(mm) and number of bricks")]
            public double NumberofBricks { get; set; }

            [Category("Brick Names"),
            Description("Available Brick Types")]

            public List<string> BrickNames { get; set; }

            [Category("Brick Heights"),
            Description("Height of Individual Brick")]
            public List<double> BrickHeights { get; set; }

            public BrickConfigurationDataType()
            {

            }
            public BrickConfigurationDataType(string filePath)
            {
                FileInfo fileInfo = new FileInfo(filePath);


                if (!fileInfo.Exists)
                    throw CException.Show(CException.Type.NoFile);

                XmlNodeList xmlAll = CFileIO.GetXmlAllNodeList(filePath);
                XmlNodeList xmlBrickConfiguration = CFileIO.GetXmlSubNodeList(xmlAll, "BrickConfiguration");
                XmlNodeList xmlBrickNames = CFileIO.GetXmlSubNodeList(xmlBrickConfiguration, "BrickNames");
                XmlNodeList xmlBrickHeights = CFileIO.GetXmlSubNodeList(xmlBrickConfiguration, "BrickHeights");
                XmlNodeList xmlUnitBrick = CFileIO.GetXmlSubNodeList(xmlBrickConfiguration, "UnitBrick");

                Length = CFileIO.GetXmlValueAsDouble(xmlUnitBrick, "Length");
                Diameter = CFileIO.GetXmlValueAsDouble(xmlUnitBrick, "Diameter");
                NumberofBricks = CFileIO.GetXmlValueAsInt(xmlUnitBrick, "NumberofBricks");
                BrickNames = CFileIO.GetXml1DTableAsStringList(xmlBrickNames);
                BrickHeights = CFileIO.GetXml1DTableAsDoubleList(xmlBrickHeights);
            }

            public List<string> GetBrickConfigurationDataToList()
            {
                List<string> contents = new List<string>();
                contents.Add(@"    <BrickNames>");
                for (int i = 0; i < BrickNames.Count; i++)
                {
                    contents.Add(@"      <brick_" + (i + 1) + @">" + BrickNames[i] + @"</brick_" + (i + 1) + @">");
                }
                contents.Add(@"    </BrickNames>");

                contents.Add(@"    <BrickHeights>");
                for (int i = 0; i < BrickHeights.Count; i++)
                {
                    contents.Add(@"      <brick_" + (i + 1) + @">" + BrickHeights[i] + @"</brick_" + (i + 1) + @">");
                }
                contents.Add(@"    </BrickHeights>");

                contents.Add(@"    <UnitBrick>");
                contents.Add(@"      <Length>" + Length + @"</Length>");
                contents.Add(@"      <Diameter>" + Diameter + @"</Diameter>");
                contents.Add(@"      <NumberofBricks>" + NumberofBricks + @"</NumberofBricks>");
                contents.Add(@"    </UnitBrick>");
                return contents;
            }
        }
    }
}
