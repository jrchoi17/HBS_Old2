using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using HBS_Shared;

namespace HBS_Shared
{
    public class ST_PD
    {
        // define data types...
        public enum RegionType { Fluid, Solid, Wall }
        public enum ComponentType { CombustionChamber = 0, Dome, RegenerativeStage, GasWasteBranch }

        private struct SpaticalDataType
        {
            // domain parameters...
            public RegionType Region { get; set; }
            public ComponentType Component { get; set; }

            // range parameters...
            public int CellNo { get; set; }
            public int RegionCellNo { get; set; }
            public double AbsolutePosition { get; set; }
            public double RelativePosition { get; set; }

            public SpaticalDataType(RegionType region, ComponentType component, int cellNo, int regionCellNo, double absolutePosition, double relativePosition)
            {
                Region = region;
                Component = component;
                CellNo = cellNo;
                RegionCellNo = regionCellNo;
                AbsolutePosition = absolutePosition;
                RelativePosition = relativePosition;
            }
        }

        private class RawData
        {
            // information data
            public string Version { get; set; }
            public DateTime CreatedDate { get; set; }
            public DateTime ModifiedDate { get; set; }

            // spatial lists
            public List<SpaticalDataType> SpaticalData { get; set; }
            public List<TemporalDataType> TemporalData { get; set; }
        }

        private static ST_PD _objThis = null;

        private RawData _objRawData = new RawData();

        public string Version
        {
            get
            {
                return _objRawData.Version;
            }
            set
            {
                _objRawData.Version = value;
            }
        }
        public DateTime CreatedDate
        {
            get
            {
                return _objRawData.CreatedDate;
            }
            set
            {
                _objRawData.CreatedDate = value;
            }
        }
        public DateTime ModifiedDate
        {
            get
            {
                return _objRawData.ModifiedDate;
            }
            set
            {
                _objRawData.ModifiedDate = value;
            }
        }

        public ST_PD()
        {

        }

        public void BuildPdFromFile(string filepath)
        {
            FileInfo fileInfo = new FileInfo(filepath);

            if (!fileInfo.Exists)
                throw CException.Show(CException.Type.NoFile);
            else
                _objRawData = new RawData();

            List<string> rows = File.ReadAllLines(filepath).ToList();

            int irow = rows.Count;
            int icol = rows[0].Split(',').Length;

            for (int i = 0; i < 4; i++)
            {
                List<string> columns = rows[i].Split(',').ToList();

                if (columns[0] == "Version")
                    Version = columns[1];
                else if (columns[0] == "Created Date")
                    CreatedDate = DateTime.Parse(columns[1]);
                else if (columns[0] == "Modified Date")
                    ModifiedDate = DateTime.Parse(columns[1]);
            }

            int paddingIndexForColumn = 5;
            int paddingIndexForRow = 7;

            List<string> strRegion = rows[0].Split(',').ToList();
            strRegion.RemoveRange(0, paddingIndexForColumn);
            List<RegionType> region = strRegion.Select(x => (RegionType)Enum.Parse(typeof(RegionType), x)).ToList();

            List<string> strComponent = rows[1].Split(',').ToList();
            strComponent.RemoveRange(0, paddingIndexForColumn);
            List<ComponentType> component = strComponent.Select(x => (ComponentType)Enum.Parse(typeof(ComponentType), x.Replace(" ", String.Empty))).ToList();

            List<string> strCellNo = rows[2].Split(',').ToList();
            strCellNo.RemoveRange(0, paddingIndexForColumn);
            List<int> cellNo = strCellNo.Select(x => int.Parse(x)).ToList();

            List<string> strRegionCellNo = rows[3].Split(',').ToList();
            strRegionCellNo.RemoveRange(0, paddingIndexForColumn);
            List<int> regionCellNo = strRegionCellNo.Select(x => int.Parse(x)).ToList();

            List<string> strAbsolutePosition = rows[4].Split(',').ToList();
            strAbsolutePosition.RemoveRange(0, paddingIndexForColumn);
            List<double> absolutePosition = strAbsolutePosition.Select(x => double.Parse(x)).ToList();

            List<string> strRelativePosition = rows[5].Split(',').ToList();
            strRelativePosition.RemoveRange(0, paddingIndexForColumn);
            List<double> relativePosition = strRelativePosition.Select(x => double.Parse(x)).ToList();

            // build spatial data
            _objRawData.SpaticalData = new List<SpaticalDataType>();
            for (int j = 0; j < icol - paddingIndexForColumn; j++)
            {
                SpaticalDataType spaticalDataType = new SpaticalDataType(region[j], component[j], cellNo[j], regionCellNo[j], absolutePosition[j], relativePosition[j]);
                _objRawData.SpaticalData.Add(spaticalDataType);
            }

            _objRawData.TemporalData = new List<TemporalDataType>();
            // build temporal data
            for (int i = paddingIndexForRow; i < rows.Count; i++)
            {
                if (rows[i] == string.Empty)
                    break;

                List<string> column = rows[i].Split(',').ToList();

                int cycle = int.Parse(column[0]);
                int timeStepPerCycle = int.Parse(column[1]);
                int timeStep = int.Parse(column[2]);
                double elapsedTime = double.Parse(column[3]);
                double elapsedTimePerCycle = double.Parse(column[4]);

                List<double> temperature = new List<double>();
                for (int j = paddingIndexForColumn; j < icol; j++)
                    temperature.Add(double.Parse(column[j]));

                TemporalDataType temporalData = new TemporalDataType(cycle, timeStepPerCycle, timeStep, elapsedTime, elapsedTimePerCycle, temperature);
                _objRawData.TemporalData.Add(temporalData);
            }
        }

        public void AddSpatialData(RegionType Region, int NodeNo, double AbsolutePosition, ComponentType Component, double RelativePosition)
        {
            SpaticalDataType spaticalDataType = new SpaticalDataType();
            spaticalDataType.Region = Region;
            spaticalDataType.RegionCellNo = NodeNo;
            spaticalDataType.AbsolutePosition = AbsolutePosition;
            spaticalDataType.Component = Component;
            spaticalDataType.RelativePosition = RelativePosition;

            _objRawData.SpaticalData.Add(spaticalDataType);
        }

        public List<int> GetCellNo(RegionType type)
        {
            List<int> cellNo = new List<int>();

            for (int i = 0; i < _objRawData.SpaticalData.Count; i++)
                if (_objRawData.SpaticalData[i].Region == type)
                    cellNo.Add(_objRawData.SpaticalData[i].CellNo);

            return cellNo;
        }
        public List<int> GetCellNo(ComponentType type)
        {
            List<int> cellNo = new List<int>();

            for (int i = 0; i < _objRawData.SpaticalData.Count; i++)
                if (_objRawData.SpaticalData[i].Component == type)
                    cellNo.Add(_objRawData.SpaticalData[i].CellNo);

            return cellNo;
        }
        public List<int> GetCellNo(RegionType Regiontype, ComponentType Componenttype)
        {
            List<int> cellNo = new List<int>();

            for (int i = 0; i < _objRawData.SpaticalData.Count; i++)
                if (_objRawData.SpaticalData[i].Region == Regiontype && _objRawData.SpaticalData[i].Component == Componenttype)
                    cellNo.Add(_objRawData.SpaticalData[i].CellNo);

            return cellNo;
        }

        public List<int> GetRegionCellNo(RegionType type)
        {
            List<int> regionCellNo = new List<int>();

            for (int i = 0; i < _objRawData.SpaticalData.Count; i++)
                if (_objRawData.SpaticalData[i].Region == type)
                    regionCellNo.Add(_objRawData.SpaticalData[i].RegionCellNo);

            return regionCellNo;
        }
        public List<int> GetRegionCellNo(ComponentType type)
        {
            List<int> regionCellNo = new List<int>();

            for (int i = 0; i < _objRawData.SpaticalData.Count; i++)
                if (_objRawData.SpaticalData[i].Component == type)
                    regionCellNo.Add(_objRawData.SpaticalData[i].RegionCellNo);

            return regionCellNo;
        }
        public List<int> GetRegionCellNo(RegionType Regiontype, ComponentType componentType)
        {
            List<int> nodeNo = new List<int>();

            for (int i = 0; i < _objRawData.SpaticalData.Count; i++)
                if (_objRawData.SpaticalData[i].Region == Regiontype && _objRawData.SpaticalData[i].Component == componentType)
                    nodeNo.Add(_objRawData.SpaticalData[i].RegionCellNo);

            return nodeNo;
        }

        public List<double> GetAbsolutePosition(RegionType type)
        {
            List<double> absolutePosition = new List<double>();

            for (int i = 0; i<_objRawData.SpaticalData.Count; i++)
                if (_objRawData.SpaticalData[i].Region == type)
                    absolutePosition.Add(_objRawData.SpaticalData[i].AbsolutePosition);

            return absolutePosition;
        }
        public List<double> GetAbsolutePosition(ComponentType type)
        {
            List<double> absolutePosition = new List<double>();

            for (int i = 0; i < _objRawData.SpaticalData.Count; i++)
                if (_objRawData.SpaticalData[i].Component == type)
                    absolutePosition.Add(_objRawData.SpaticalData[i].AbsolutePosition);

            return absolutePosition;
        }
        public List<double> GetAbsolutePosition(RegionType Regiontype, ComponentType componentType)
        {
            List<double> absolutePosition = new List<double>();

            for (int i = 0; i < _objRawData.SpaticalData.Count; i++)
                if (_objRawData.SpaticalData[i].Region == Regiontype && _objRawData.SpaticalData[i].Component == componentType)
                    absolutePosition.Add(_objRawData.SpaticalData[i].AbsolutePosition);

            return absolutePosition;
        }

        public List<double> GetRelativePosition(RegionType type)
        {
            List<double> relativePosition = new List<double>();

            for (int i = 0; i < _objRawData.SpaticalData.Count; i++)
                if (_objRawData.SpaticalData[i].Region == type)
                    relativePosition.Add(_objRawData.SpaticalData[i].RelativePosition);

            return relativePosition;
        }
        public List<double> GetRelativePosition(ComponentType type)
        {
            List<double> relativePosition = new List<double>();

            for (int i = 0; i < _objRawData.SpaticalData.Count; i++)
                if (_objRawData.SpaticalData[i].Component == type)
                    relativePosition.Add(_objRawData.SpaticalData[i].RelativePosition);

            return relativePosition;
        }
        public List<double> GetRelativePosition(RegionType Regiontype, ComponentType componentType)
        {
            List<double> relativePosition = new List<double>();

            for (int i = 0; i < _objRawData.SpaticalData.Count; i++)
                if (_objRawData.SpaticalData[i].Region == Regiontype && _objRawData.SpaticalData[i].Component == componentType)
                    relativePosition.Add(_objRawData.SpaticalData[i].RelativePosition);

            return relativePosition;
        }

        private struct TemporalDataType
        {
            // domain paramters...
            public int Cycle { get; set; }
            public int TimeStepPerCycle { get; set; }

            // range parameters...
            public int TimeStep { get; set; }
            public double ElapsedTime { get; set; }
            public double ElapsedTimePerCycle { get; set; }
            public List<double> Temperature { get; set; }

            public TemporalDataType(int cycle, int timeStepPerCycle, int timeStep, double elapsedTime, double elapsedTimePerCycle, List<double> temperature)
            {
                Cycle = cycle;
                TimeStepPerCycle = timeStepPerCycle;
                TimeStep = timeStep;
                ElapsedTime = elapsedTime;
                ElapsedTimePerCycle = elapsedTimePerCycle;

                Temperature = new List<double>();
                for (int j = 0; j < temperature.Count; j++)
                    Temperature.Add(temperature[j]);
            }
        }

        public List<int> Cycle
        {
            get
            {
                List<int> cycle = new List<int>();

                for (int i = 0; i < _objRawData.TemporalData.Count; i++)
                    cycle.Add(_objRawData.TemporalData[i].Cycle);

                return cycle;
            }
        }

        public List<int> TimeStep
        {
            get
            {
                List<int> timeStep = new List<int>();

                for (int i = 0; i < _objRawData.TemporalData.Count; i++)
                    timeStep.Add(_objRawData.TemporalData[i].TimeStep);

                return timeStep;
            }
        }

        public int GetLastCycle()
        {
            return Cycle.Max();
        }
        public int GetLastTimeStepPerCycle()
        {
            int lastCycle = GetLastCycle();
            List<int> timeStepPerCycle = GetTimeStepPerCycle(lastCycle);

            return timeStepPerCycle.Max();
        }

        public int GetTotalTimeStep()
        {
            return TimeStep.Count();
        }

        public int GetTimeStep(int cycle, int timeStepPerCycle)
        {
            for (int i = 0; i < _objRawData.TemporalData.Count; i++)
                if (_objRawData.TemporalData[i].Cycle == cycle && _objRawData.TemporalData[i].TimeStepPerCycle == timeStepPerCycle)
                    return _objRawData.TemporalData[i].TimeStep;
            return -1;
        }

        public List<List<int>> GetTimeStepsPerCycle()
        {
            List<List<int>> timeStepsPerCycle = new List<List<int>>();
            int lastCycle = GetLastCycle();

            for (int i = 0; i < lastCycle; i++)
                timeStepsPerCycle.Add(GetTimeStepPerCycle(i));

            return timeStepsPerCycle;
        }

        public List<List<double>> GetElapsedTimesPerCycle()
        {
            List<List<double>> elapsedTimesPerCycle = new List<List<double>>();
            int lastCycle = GetLastCycle();

            for (int i = 0; i < lastCycle; i++)
                elapsedTimesPerCycle.Add(GetElapsedTimePerCycle(i));

            return elapsedTimesPerCycle;

        }

        public List<int> GetTimeStepPerCycle(int cycle)
        {
            List<int> timeStepPerCycle = new List<int>();

            for (int i = 0; i < _objRawData.TemporalData.Count; i++)
                if (_objRawData.TemporalData[i].Cycle == cycle)
                    timeStepPerCycle.Add(_objRawData.TemporalData[i].TimeStepPerCycle);

            return timeStepPerCycle;
        }

        public List<double> GetElapsedTimePerCycle(int cycle)
        {
            List<double> elapsedTimePerCycle = new List<double>();

            for (int i = 0; i < _objRawData.TemporalData.Count; i++)
                if (_objRawData.TemporalData[i].Cycle == cycle)
                    elapsedTimePerCycle.Add(_objRawData.TemporalData[i].ElapsedTimePerCycle);

            return elapsedTimePerCycle;
        }

        public double GetElapsedTime(int cycle, int timeStepPerCycle)
        {
            int timeStep = GetTimeStep(cycle, timeStepPerCycle);

            return _objRawData.TemporalData[timeStep].ElapsedTime;
        }

        public double GetElapsedTimePerCycle(int cycle, int timeStepPerCycle)
        {
            int timeStep = GetTimeStep(cycle, timeStepPerCycle);

            return _objRawData.TemporalData[timeStep].ElapsedTimePerCycle;
        }

        public List<double> GetTemperature(int cycle, int timeStepPerCycle)
        {
            int timeStep = GetTimeStep(cycle, timeStepPerCycle);

            return _objRawData.TemporalData[timeStep].Temperature;
        }
        public List<double> GetTemperature(int cycle, int timeStepPerCycle, RegionType type)
        {
            List<double> allTemperatures = GetTemperature(cycle, timeStepPerCycle);
            List<int> cellNos = GetCellNo(type);

            int minCellNo = cellNos.Min();
            int countCellNo = cellNos.Count;

            return allTemperatures.GetRange(minCellNo, countCellNo);
        }

        public List<double> GetTemperature(int cycle, int timeStepPerCycle, RegionType regionRype, ComponentType componentType)
        {
            List<double> allTemperatures = GetTemperature(cycle, timeStepPerCycle);
            List<int> cellNos = GetCellNo(regionRype, componentType);

            int minCellNo = cellNos.Min();
            int countCellNo = cellNos.Count;

            return allTemperatures.GetRange(minCellNo, countCellNo);
        }

        public static ST_PD GetInstance()
        {
            if (_objThis == null)
                _objThis = new ST_PD();

            return _objThis;
        }
    }
}
