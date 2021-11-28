using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBS
{
    public class CTemperature
    {
        public enum Unit { K, C }

        public double Value { get; set; }

        public Dictionary<Unit, double> Conversion { get; set; }
        public Dictionary<Unit, double> Offset { get; set; }

        public CTemperature(double value)
        {
            Initialization();
            Value = value;
        }

        public CTemperature(double value, Unit unit)
        {
            Initialization();
            Value = Conversion[unit] * value + Offset[unit];
        }

        public void Initialization()
        {
            Conversion = new Dictionary<Unit, double>();
            Offset = new Dictionary<Unit, double>();

            // K --> K
            Conversion.Add(Unit.K, 1.0);
            Offset.Add(Unit.K, 0.0);

            // C --> K
            Conversion.Add(Unit.C, 1.0);
            Offset.Add(Unit.C, 273.15);
        }

        public double this[Unit unit]
        {
            get
            {
                return (Value - Offset[unit]) / Conversion[unit];
            }
        }

        public static implicit operator CTemperature(double d)
        {
            return new CTemperature(d);
        }

        public static implicit operator double(CTemperature temperature)
        {
            return temperature.Value;
        }

        public static double operator +(CTemperature a, CTemperature b)
        {
            return a.Value + b.Value;
        }

        public static double operator -(CTemperature a, CTemperature b)
        {
            return a.Value - b.Value;
        }

        public static double operator *(CTemperature a, CTemperature b)
        {
            return a.Value * b.Value;
        }

        public static double operator /(CTemperature a, CTemperature b)
        {
            return a.Value / b.Value;
        }

        public static bool operator <(CTemperature a, CTemperature b)
        {
            return a.Value < b.Value;
        }

        public static bool operator >(CTemperature a, CTemperature b)
        {
            return a.Value > b.Value;
        }

        public static bool operator ==(CTemperature a, CTemperature b)
        {
            return a.Value == b.Value;
        }

        public static bool operator !=(CTemperature a, CTemperature b)
        {
            return a.Value != b.Value;
        }

        public static bool operator <=(CTemperature a, CTemperature b)
        {
            return a.Value <= b.Value;
        }

        public static bool operator >=(CTemperature a, CTemperature b)
        {
            return a.Value >= b.Value;
        }

        public override bool Equals(object a)
        {
            return !(a is CTemperature) ? false : this == (CTemperature)a;
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
