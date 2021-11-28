using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBS
{
    public class CFlowRate
    {
        public enum Unit { CMS, CMM, CMH, lpm }

        public double Value { get; set; }

        public Dictionary<Unit, double> Conversion { get; set; }
        public Dictionary<Unit, double> Offset { get; set; }

        public CFlowRate(double value)
        {
            Initialization();
            Value = value;
        }

        public CFlowRate(double value, Unit unit)
        {
            Initialization();
            Value = Conversion[unit] * value + Offset[unit];
        }

        public void Initialization()
        {
            Conversion = new Dictionary<Unit, double>();
            Offset = new Dictionary<Unit, double>();

            // CMS --> CMS
            Conversion.Add(Unit.CMS, 1.0);
            Offset.Add(Unit.CMS, 0.0);

            // CMM --> CMS
            Conversion.Add(Unit.CMM, 1.0 / 60.0);
            Offset.Add(Unit.CMM, 0.0);

            // CMH --> CMS
            Conversion.Add(Unit.CMH, 1.0 / 3600.0);
            Offset.Add(Unit.CMH, 0.0);

            // lpm --> CMS
            Conversion.Add(Unit.lpm, 1.0 / 60000.0);
            Offset.Add(Unit.lpm, 0.0);
        }

        public double this[Unit unit]
        {
            get
            {
                return (Value - Offset[unit]) / Conversion[unit];
            }
        }

        public static implicit operator CFlowRate(double d)
        {
            return new CFlowRate(d);
        }

        public static implicit operator double(CFlowRate temperature)
        {
            return temperature.Value;
        }

        public static double operator +(CFlowRate a, CFlowRate b)
        {
            return a.Value + b.Value;
        }

        public static double operator -(CFlowRate a, CFlowRate b)
        {
            return a.Value - b.Value;
        }

        public static double operator *(CFlowRate a, CFlowRate b)
        {
            return a.Value * b.Value;
        }

        public static double operator /(CFlowRate a, CFlowRate b)
        {
            return a.Value / b.Value;
        }

        public static bool operator <(CFlowRate a, CFlowRate b)
        {
            return a.Value < b.Value;
        }

        public static bool operator >(CFlowRate a, CFlowRate b)
        {
            return a.Value > b.Value;
        }

        public static bool operator ==(CFlowRate a, CFlowRate b)
        {
            return a.Value == b.Value;
        }

        public static bool operator !=(CFlowRate a, CFlowRate b)
        {
            return a.Value != b.Value;
        }

        public static bool operator <=(CFlowRate a, CFlowRate b)
        {
            return a.Value <= b.Value;
        }

        public static bool operator >=(CFlowRate a, CFlowRate b)
        {
            return a.Value >= b.Value;
        }

        public override bool Equals(object a)
        {
            return !(a is CFlowRate) ? false : this == (CFlowRate)a;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}