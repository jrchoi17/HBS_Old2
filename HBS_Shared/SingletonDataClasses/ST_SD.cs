using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

namespace HBS_Shared
{
    public class ST_SD
    {
        private static ST_SD _objThis;

        #region Enums
        public enum FlowDirection { WestToEast, EastToWest }
        public enum XmlProfileOrder { Time = 0, FlowRate, Temperature, Pressure }
        #endregion


        #region Constant
        public const double epsilon_a = 1.0e-6;
        public const double Sigma = 5.67e-8;
        #endregion


        #region Cycle parameter
        public int C { get; set; }

        public int C_max { get; set; }
        #endregion



        #region Time step parameter
        /// <summary>
        /// Present time step.
        /// </summary>
        public int T { get; set; }

        /// <summary>
        /// Maximum time steps as a stopping criterion.
        /// </summary>
        public int T_Max { get; set; }
        #endregion



        #region Time parameter (per cycle)
        /// <summary>
        /// Maximum time as a stopping criterion. Unit: s.
        /// </summary>
        public double t { get; set; }

        /// <summary>
        /// Time interval. Unit: s.
        /// </summary>
        public double Dt { get; set; }

        public double ElasedTimePerCycle
        {
            get
            {
                double elasedTimePerCycle = t % (Dt * (double)T_Max);

                switch (elasedTimePerCycle)
                {
                    case 0.0:
                        return elasedTimePerCycle + (Dt * (double)T_Max);
                    default:
                        return elasedTimePerCycle;
                }
            }
        }
        #endregion



        #region Inner iteration parameter
        public int I_Max { get; set; }
        #endregion



        #region CombustedGas
        public CGas CombustedGas { get; set; }
        #endregion



        #region Geometric parameter
        /// <summary>
        /// Diameter of pipe. Unit: m.
        /// </summary>
        public double d { get; set; }


        /// <summary>
        /// Length of pipe. Unit: m.
        /// </summary>
        public double L { get; set; }
        #endregion



        #region Operating condition
        public struct OperatingCondition
        {
            public double t;
            public double Q;
            public double T;
            public double p;
        }
        public List<OperatingCondition> WestOperatingCondition { get; set; }
        public List<OperatingCondition> EastOperatingCondition { get; set; }

        public FlowDirection FlowDir
        {
            get
            {
                double t_w_min = WestOperatingCondition[0].t;
                double t_w_max = WestOperatingCondition[WestOperatingCondition.Count - 1].t;

                double t_e_min = EastOperatingCondition[0].t;
                double t_e_max = EastOperatingCondition[EastOperatingCondition.Count - 1].t;

                if (ElasedTimePerCycle > t_w_min && ElasedTimePerCycle <= t_w_max)
                    return FlowDirection.WestToEast;
                else if (ElasedTimePerCycle > t_e_min && ElasedTimePerCycle <= t_e_max)
                    return FlowDirection.EastToWest;
                else
                    throw CException.Show(CException.Type.InvalidRange);
            }
        }

        private List<double> GetTimes(List<OperatingCondition> oc)
        {
            List<double> times = new List<double>();

            for (int i = 0; i < oc.Count; i++)
                times.Add(oc[i].t);

            return times;
        }

        private List<double> GetTemperatures(List<OperatingCondition> oc)
        {
            List<double> temperatures = new List<double>();

            for (int i = 0; i < oc.Count; i++)
                temperatures.Add(oc[i].T);

            return temperatures;
        }

        private List<double> GetFlowRates(List<OperatingCondition> oc)
        {
            List<double> flowRates = new List<double>();

            for (int i = 0; i < oc.Count; i++)
                flowRates.Add(oc[i].Q);

            return flowRates;
        }

        private List<double> GetPressures(List<OperatingCondition> oc)
        {
            List<double> pressures = new List<double>();

            for (int i = 0; i < oc.Count; i++)
                pressures.Add(oc[i].p);

            return pressures;
        }

        public double Temperature_Now
        {
            get
            {
                List<double> times, temperatures;
                switch(FlowDir)
                {
                    case FlowDirection.WestToEast:
                        times = GetTimes(WestOperatingCondition);
                        temperatures = GetTemperatures(WestOperatingCondition);
                        break;
                    case FlowDirection.EastToWest:
                        times = GetTimes(EastOperatingCondition);
                        temperatures = GetTemperatures(EastOperatingCondition);
                        break;
                    default:
                        throw CException.Show(CException.Type.UnsupportedKeyword);
                }
                return CMatricReport.PartialLinearInterpolatedValue(times, temperatures, ElasedTimePerCycle);
            }
        }

        public double Velocity_Now
        {
            get
            {
                double flowRate = FlowRate_Now;
                double temperature = Temperature_Now;
                double A_bound;
                switch(FlowDir)
                {
                    case FlowDirection.WestToEast:
                        A_bound = FluidRegion.Cells[0].A_w;
                        break;
                    case FlowDirection.EastToWest:
                        A_bound = FluidRegion.Cells[FluidRegion.N - 1].A_e;
                        break;
                    default:
                        throw CException.Show(CException.Type.UnsupportedKeyword);
                }

                double rho_20C = FluidRegion.Property[0].Density(293.15);
                double rho_now = FluidRegion.Property[0].Density(temperature);
                
                return rho_20C / rho_now * flowRate / A_bound;
            }
        }

        public double FlowRate_Now
        {
            get
            {
                List<double> times, flowrate;
                switch (FlowDir)
                {
                    case FlowDirection.WestToEast:
                        times = GetTimes(WestOperatingCondition);
                        flowrate = GetFlowRates(WestOperatingCondition);
                        break;
                    case FlowDirection.EastToWest:
                        times = GetTimes(EastOperatingCondition);
                        flowrate = GetFlowRates(EastOperatingCondition);
                        break;
                    default:
                        throw CException.Show(CException.Type.UnsupportedKeyword);
                }
                return CMatricReport.PartialLinearInterpolatedValue(times, flowrate, ElasedTimePerCycle);
            }
        }

        public double Pressure_Now
        {
            get
            {
                List<double> times, pressure;
                switch (FlowDir)
                {
                    case FlowDirection.WestToEast:
                        times = GetTimes(WestOperatingCondition);
                        pressure = GetPressures(WestOperatingCondition);
                        break;
                    case FlowDirection.EastToWest:
                        times = GetTimes(EastOperatingCondition);
                        pressure = GetPressures(EastOperatingCondition);
                        break;
                    default:
                        throw CException.Show(CException.Type.UnsupportedKeyword);
                }
                return CMatricReport.PartialLinearInterpolatedValue(times, pressure, ElasedTimePerCycle);
            }
        }
        #endregion


        #region Constructor method(s)
        /// <summary>
        /// Constructor method.
        /// </summary>
        public ST_SD()
        {
        }
        #endregion



        #region Public methods
        public void SetDataFromXml(string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);

            if (!fileInfo.Exists)
                throw CException.Show(CException.Type.NoFile);

            XmlNodeList xmlAll = CFileIO.GetXmlAllNodeList(filePath);

            // Set cycle parameter
            SetDataFromXml_CycleParameter(filePath);

            // Set time parameter
            SetDataFromXml_TimeParameter(filePath);

            // Set time step parameter
            SetDataFromXml_TimeStepParameter(filePath);

            // Set inner iteration parameter
            SetDataFromXml_InnerIterationParameter(filePath);

            // Set combusted gas
            SetDataFromXml_CombustedGas(filePath);

            // Set geometric parameter
            SetDataFromXml_GeometricParameter(filePath);

            if (AllRegions == null)
                AllRegions = new List<CRegion>();

            // Set regions
            SetDataFromXml_Region(filePath, CRegion.RegionType.Fluid);
            SetDataFromXml_Region(filePath, CRegion.RegionType.Solid);
            SetDataFromXml_Region(filePath, CRegion.RegionType.Wall);

            // Set operating condition
            SetDataFromXml_OperatingCondition(filePath);
        }
        #endregion



        #region Private methods
        private void SetCellDataToRegion(CRegion region, List<List<double>> list_2d)
        {
            for (int i = 0; i < list_2d.Count; i++)
            {
                CCell cell = region.Cells[i];
                List<double> list_1d = list_2d[i];

                cell.N_J = (int)list_1d[(int)CCell.XmlDataOrder.N_J];   //  0
                cell.N_I = (int)list_1d[(int)CCell.XmlDataOrder.N_I];   //  1
                cell.S_J = (int)list_1d[(int)CCell.XmlDataOrder.S_J];   //  2
                cell.S_I = (int)list_1d[(int)CCell.XmlDataOrder.S_I];   //  3
                cell.Prop = (int)list_1d[(int)CCell.XmlDataOrder.Prop]; //  4
                cell.u = list_1d[(int)CCell.XmlDataOrder.u];            //  5
                cell.p = list_1d[(int)CCell.XmlDataOrder.p];            //  6
                cell.T = list_1d[(int)CCell.XmlDataOrder.T];            //  7
                cell.T_n = list_1d[(int)CCell.XmlDataOrder.T_n];        //  8
                cell.T_s = list_1d[(int)CCell.XmlDataOrder.T_s];        //  9
                cell.Dx_w = list_1d[(int)CCell.XmlDataOrder.Dx_w];      // 10
                cell.Dx_e = list_1d[(int)CCell.XmlDataOrder.Dx_e];      // 11
                cell.Dx_n = list_1d[(int)CCell.XmlDataOrder.Dx_n];      // 12
                cell.Dx_s = list_1d[(int)CCell.XmlDataOrder.Dx_s];      // 13
                cell.A_w = list_1d[(int)CCell.XmlDataOrder.A_w];        // 14
                cell.A_e = list_1d[(int)CCell.XmlDataOrder.A_e];        // 15
                cell.A_n = list_1d[(int)CCell.XmlDataOrder.A_n];        // 16
                cell.A_s = list_1d[(int)CCell.XmlDataOrder.A_s];        // 17
                cell.DV = list_1d[(int)CCell.XmlDataOrder.DV];          // 18
            }
        }

        private void SetPropertyDataToRegion(CRegion region, XmlNodeList xml)
        {
            if (region.Property == null)
                region.Property = new List<CMatProp>();

            ;
            XmlNodeList xmlMaterialProperty = CFileIO.GetXmlSubNodeList(xml, "MaterialProperty");

            CMatProp property;
            for (int i = 0; i < xmlMaterialProperty.Count; i++)
            {
                property = new CMatProp();
                XmlNodeList xmlProp_x = CFileIO.GetXmlSubNodeList(xmlMaterialProperty, "Prop_" + i.ToString());

                // Density...
                {
                    string method = CFileIO.GetXmlValueAsString(xmlProp_x, "DensityMethod");
                    property.DensityMethod = (CMatProp.MatPropMethod)Enum.Parse(typeof(CMatProp.MatPropMethod), method);

                    XmlNodeList xmlCoeff = CFileIO.GetXmlSubNodeList(xmlProp_x, "DensityCoeff");
                    property.DensityCoeff = CFileIO.GetXml1DTableAsDoubleList(xmlCoeff);

                    property.T_MaxForDensity = CFileIO.GetXmlValueAsDouble(xmlProp_x, "T_MaxForDensity");
                    property.T_MinForDensity = CFileIO.GetXmlValueAsDouble(xmlProp_x, "T_MinForDensity");
                }

                // Specific heat...
                {
                    string method = CFileIO.GetXmlValueAsString(xmlProp_x, "SpecificHeatMethod");
                    property.SpecificHeatMethod = (CMatProp.MatPropMethod)Enum.Parse(typeof(CMatProp.MatPropMethod), method);

                    XmlNodeList xmlCoeff = CFileIO.GetXmlSubNodeList(xmlProp_x, "SpecificHeatCoeff");
                    property.SpecificHeatCoeff = CFileIO.GetXml1DTableAsDoubleList(xmlCoeff);

                    property.T_MaxForDensity = CFileIO.GetXmlValueAsDouble(xmlProp_x, "T_MaxForSpecificHeat");
                    property.T_MinForDensity = CFileIO.GetXmlValueAsDouble(xmlProp_x, "T_MinForSpecificHeat");
                }

                // Thermal conductivity...
                {
                    string method = CFileIO.GetXmlValueAsString(xmlProp_x, "ThermalConductivityMethod");
                    property.ThermalConductivityMethod = (CMatProp.MatPropMethod)Enum.Parse(typeof(CMatProp.MatPropMethod), method);

                    XmlNodeList xmlCoeff = CFileIO.GetXmlSubNodeList(xmlProp_x, "ThermalConductivityCoeff");
                    property.ThermalConductivityCoeff = CFileIO.GetXml1DTableAsDoubleList(xmlCoeff);

                    property.T_MaxForThermalConductivity = CFileIO.GetXmlValueAsDouble(xmlProp_x, "T_MaxForThermalConductivity");
                    property.T_MinForThermalConductivity = CFileIO.GetXmlValueAsDouble(xmlProp_x, "T_MinForThermalConductivity");
                }

                // Dynamic viscosity...
                {
                    string method = CFileIO.GetXmlValueAsString(xmlProp_x, "DynamicViscosityMethod");
                    property.DynamicViscosityMethod = (CMatProp.MatPropMethod)Enum.Parse(typeof(CMatProp.MatPropMethod), method);

                    XmlNodeList xmlCoeff = CFileIO.GetXmlSubNodeList(xmlProp_x, "DynamicViscosityCoeff");
                    property.DynamicViscosityCoeff = CFileIO.GetXml1DTableAsDoubleList(xmlCoeff);

                    property.T_MaxForDynamicViscosity = CFileIO.GetXmlValueAsDouble(xmlProp_x, "T_MaxForDynamicViscosity");
                    property.T_MinForDynamicViscosity = CFileIO.GetXmlValueAsDouble(xmlProp_x, "T_MinForDynamicViscosity");
                }

                region.Property.Add(property);
            }      
        }

        private void SetDataFromXml_CycleParameter(string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);

            if (!fileInfo.Exists)
                throw CException.Show(CException.Type.NoFile);

            XmlNodeList xmlAll = CFileIO.GetXmlAllNodeList(filePath);
            XmlNodeList xml = CFileIO.GetXmlSubNodeList(xmlAll, "CycleParameter");

            C = CFileIO.GetXmlValueAsInt(xml, "C");
            C_max = CFileIO.GetXmlValueAsInt(xml, "C_max");
        }

        private void SetDataFromXml_TimeParameter(string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);

            if (!fileInfo.Exists)
                throw CException.Show(CException.Type.NoFile);

            XmlNodeList xmlAll = CFileIO.GetXmlAllNodeList(filePath);
            XmlNodeList xml = CFileIO.GetXmlSubNodeList(xmlAll, "TimeParameter");

            t = CFileIO.GetXmlValueAsDouble(xml, "t");
            Dt = CFileIO.GetXmlValueAsDouble(xml, "dt");
        }

        private void SetDataFromXml_TimeStepParameter(string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);

            if (!fileInfo.Exists)
                throw CException.Show(CException.Type.NoFile);

            XmlNodeList xmlAll = CFileIO.GetXmlAllNodeList(filePath);
            XmlNodeList xml = CFileIO.GetXmlSubNodeList(xmlAll, "TimeStepParameter");

            T = CFileIO.GetXmlValueAsInt(xml, "T");
            T_Max = CFileIO.GetXmlValueAsInt(xml, "T_max");
        }

        private void SetDataFromXml_InnerIterationParameter(string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);

            if (!fileInfo.Exists)
                throw CException.Show(CException.Type.NoFile);

            XmlNodeList xmlAll = CFileIO.GetXmlAllNodeList(filePath);
            XmlNodeList xml = CFileIO.GetXmlSubNodeList(xmlAll, "InnerIterationParameter");

            I_Max = CFileIO.GetXmlValueAsInt(xml, "I_Max");
        }

        private void SetDataFromXml_CombustedGas(string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);

            if (!fileInfo.Exists)
                throw CException.Show(CException.Type.NoFile);

            XmlNodeList xmlAll = CFileIO.GetXmlAllNodeList(filePath);
            XmlNodeList xml = CFileIO.GetXmlSubNodeList(xmlAll, "CombustedGas");

            CombustedGas = CGas.GetGasFromXmlNodeList(xml);
        }

        private void SetDataFromXml_GeometricParameter(string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);

            if (!fileInfo.Exists)
                throw CException.Show(CException.Type.NoFile);

            XmlNodeList xmlAll = CFileIO.GetXmlAllNodeList(filePath);
            XmlNodeList xml = CFileIO.GetXmlSubNodeList(xmlAll, "GeometricParameter");

            d = CFileIO.GetXmlValueAsDouble(xml, "d");
            L = CFileIO.GetXmlValueAsDouble(xml, "L");
        }

        private CRegion SetRegionDataFromXml(XmlNodeList xmlAll, string nodeName)
        {
            XmlNodeList xml = CFileIO.GetXmlSubNodeList(xmlAll, nodeName);

            if (xml == null)
                return null;

            int n = CFileIO.GetXmlValueAsInt(xml, "N");
            CRegion region = new CRegion(n);

            string type = CFileIO.GetXmlValueAsString(xml, "Type");
            region.Type = (CRegion.RegionType)Enum.Parse(typeof(CRegion.RegionType), type);

            region.h_inf = CFileIO.GetXmlValueAsDouble(xml, "h_inf");
            region.T_inf = CFileIO.GetXmlValueAsDouble(xml, "T_inf");

            XmlNodeList xmlCells = CFileIO.GetXmlSubNodeList(xml, "Cells");
            List<List<double>> cells = CFileIO.GetXml2DTableAsDoubleList(xmlCells);

            // Cells
            SetCellDataToRegion(region, cells);

            // Material properties...
            SetPropertyDataToRegion(region, xml);

            return region;
        }

        private void SetDataFromXml_Region(string filePath, CRegion.RegionType type)
        {
            FileInfo fileInfo = new FileInfo(filePath);

            if (!fileInfo.Exists)
                throw CException.Show(CException.Type.NoFile);

            XmlNodeList xmlAll = CFileIO.GetXmlAllNodeList(filePath);

            if (AllRegions == null)
                AllRegions = new List<CRegion>();

            switch (type)
            {
                case CRegion.RegionType.Fluid:
                    FluidRegion = SetRegionDataFromXml(xmlAll, "FluidRegion");

                    if (FluidRegion == null)
                        return;
                    else
                    {
                        FluidRegion.SetPseudoCell();
                        FluidRegion.SetCellParent();
                    }

                    AllRegions.Add(FluidRegion);
                    break;
                case CRegion.RegionType.Solid:
                    SolidRegion = SetRegionDataFromXml(xmlAll, "SolidRegion");

                    if (SolidRegion == null)
                        return;
                    else
                    {
                        SolidRegion.SetPseudoCell();
                        SolidRegion.SetCellParent();
                    }
                    
                    AllRegions.Add(SolidRegion);
                    break;
                case CRegion.RegionType.Wall:
                    WallRegion = SetRegionDataFromXml(xmlAll, "WallRegion");
                    
                    if (WallRegion == null)
                        return;
                    else
                    {
                        WallRegion.SetPseudoCell();
                        WallRegion.SetCellParent();
                    }

                    AllRegions.Add(WallRegion);
                    break;
                default:
                    throw CException.Show(CException.Type.UnsupportedKeyword);
            }
        }

        private void SetDataFromXml_OperatingCondition(string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);

            if (!fileInfo.Exists)
                throw CException.Show(CException.Type.NoFile);

            XmlNodeList xmlAll = CFileIO.GetXmlAllNodeList(filePath);
            XmlNodeList xml = CFileIO.GetXmlSubNodeList(xmlAll, "OperatingCondition");

            if (xml != null)
            {
                // West operating conditions
                XmlNodeList xmlWestOperatingCondition = CFileIO.GetXmlSubNodeList(xml, "WestOperatingCondition");
                List<List<double>> westProfiles = CFileIO.GetXml2DTableAsDoubleList(xmlWestOperatingCondition);

                WestOperatingCondition = new List<OperatingCondition>();

                for (int i = 0; i < westProfiles.Count; i++)
                {
                    List<double> westOperatingProfile = westProfiles[i];
                    OperatingCondition westCondition = new OperatingCondition();
                    westCondition.t = westOperatingProfile[(int)XmlProfileOrder.Time];        // 0
                    westCondition.Q = westOperatingProfile[(int)XmlProfileOrder.FlowRate];    // 1
                    westCondition.T = westOperatingProfile[(int)XmlProfileOrder.Temperature]; // 2
                    westCondition.p = westOperatingProfile[(int)XmlProfileOrder.Pressure];    // 3

                    WestOperatingCondition.Add(westCondition);
                }

                // East operating conditions
                XmlNodeList xmlEastOperatingCondition = CFileIO.GetXmlSubNodeList(xml, "EastOperatingCondition");
                List<List<double>> eastProfiles = CFileIO.GetXml2DTableAsDoubleList(xmlEastOperatingCondition);

                EastOperatingCondition = new List<OperatingCondition>();

                for (int i = 0; i < eastProfiles.Count; i++)
                {
                    List<double> eastOperatingProfile = eastProfiles[i];
                    OperatingCondition eastCondition = new OperatingCondition();
                    eastCondition.t = eastOperatingProfile[(int)XmlProfileOrder.Time];        // 0
                    eastCondition.Q = eastOperatingProfile[(int)XmlProfileOrder.FlowRate];    // 1
                    eastCondition.T = eastOperatingProfile[(int)XmlProfileOrder.Temperature]; // 2
                    eastCondition.p = eastOperatingProfile[(int)XmlProfileOrder.Pressure];    // 3

                    EastOperatingCondition.Add(eastCondition);
                }
            }
        }

        #endregion



        #region Instance method(s)
        /// <summary>
        /// Get instance of static UD class with lazy initialization
        /// </summary>
        /// <returns></returns>
        public static ST_SD GetInstance()
        {
            if (_objThis == null)
                _objThis = new ST_SD();

            return _objThis;
        }
        #endregion



        public List<CRegion> AllRegions { get; set; }
        public CRegion FluidRegion { get; set; }
        public CRegion SolidRegion { get; set; }
        public CRegion WallRegion { get; set; }
    }
}
