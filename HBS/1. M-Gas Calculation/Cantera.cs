using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using HBS_Shared;

namespace HBS
{
    public class Cantera
    {
        public enum CalculationType { Sim0D = 0, Sim1D }
        public CalculationType Type { get; set; }
        public double FlameWidth { get; set; }

        public double T_mgas { get; set; }
        public double T_air { get; set; }
        public double p_mgas { get; set; }
        public double p_air { get; set; }
        public double Q_mgas { get; set; }
        public double Q_air { get; set; }
        public bool IsAuto { get; set; }

        public CGas M_Gas { get; set; }
        public CGas Air { get; set; }

        public double T_combustedgas { get; set; }
        public double p_combustedgas { get; set; }
        public double X_NO_combustedgas { get; set; }
        public double X_NO2_combustedgas { get; set; }
        public double X_N2O_combustedgas { get; set; }
        public double X_NOx_combustedgas { get; set; }
        public double X_CO_combustedgas { get; set; }

        public CGas Combusted_Gas { get; set; }

        public string Results { get; set; }

        public void WritePyFile()
        {
            switch (Type)
            {
                case CalculationType.Sim0D:
                    Write0DPyFile();
                    break;
                case CalculationType.Sim1D:
                    Write1DPyFile();
                    break;
                default:
                    throw CException.Show(CException.Type.UnsupportedKeyword);
            }
        }
        
        public void ExcuteCantera()
        {
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();

            if (Type == CalculationType.Sim0D)
                cmd.StandardInput.WriteLine(@"py 0D.py > results.txt");
            else
                cmd.StandardInput.WriteLine(@"py 1D.py > results.txt");

            cmd.StandardInput.Close();
            cmd.WaitForExit();
            //Results = cmd.StandardOutput.ReadToEnd();
        }

        public void UpdateData()
        {
            List<string> contents = File.ReadAllLines("results.txt").ToList();

            int index_start = -1, index_end = -1;


            T_combustedgas = GetValuesFromString(contents, "temperature")[0] - 273.15;
            p_combustedgas = (GetValuesFromString(contents, "pressure")[0] - 101325.0) / 9.80665;
            
            if (GetValuesFromString(contents, "CO") != null)
                X_CO_combustedgas = GetValuesFromString(contents, "CO")[1] * 1.0e6;
            else
                X_CO_combustedgas = 0.0;

            if (GetValuesFromString(contents, "NO") != null)
                X_NO_combustedgas = GetValuesFromString(contents, "NO")[1] * 1.0e6;
            else
                X_NO_combustedgas = 0.0;

            if(GetValuesFromString(contents, "NO2") != null)
                X_NO2_combustedgas = GetValuesFromString(contents, "NO2")[1] * 1.0e6;
            else
                X_NO2_combustedgas = 0.0;

            if (GetValuesFromString(contents, "N2O") != null)
                X_N2O_combustedgas = GetValuesFromString(contents, "N2O")[1] * 1.0e6;
            else
                X_N2O_combustedgas = 0.0;

            X_NOx_combustedgas = X_NO_combustedgas + X_NO2_combustedgas + X_N2O_combustedgas;
        
            for (int i = 0; i < contents.Count; i++)
            {
                if (contents[i].Length > 8)
                {
                    if (contents[i].Trim().Substring(0, 8) == "mass fra")
                        index_start = i;
                    if (contents[i].Trim().Substring(0, 8) == "end simu")
                        index_end = i;
                }
            }

            if (index_start >= 0 && index_end >= 0)
            {
                Combusted_Gas = new CGas();
                Combusted_Gas.MassFraction = new CGas.Fraction(0);

                for (int i = index_start + 2; i < index_end - 1; i++)
                {
                    string[] data = contents[i].Trim().Split(' ');
                    CGas.Composition comp = CGas.Composition.H2;

                    if (Enum.TryParse(data[0], out comp))
                    {
                        double d = 0.0;

                        for (int j = 1; j < data.Length; j++)
                        {
                            if (double.TryParse(data[j], out d))
                            {
                                Combusted_Gas.MassFraction[comp] = d;
                                break;
                            }
                        }
                    }
                }

                Combusted_Gas.MassFraction.Normalize();
                Combusted_Gas.MoleFraction = Combusted_Gas.MassFraction.GetMoleFraction();
            }
        }

        
        private List<double> GetValuesFromString(List<string> contents, string keyword)
        {
            int length = keyword.Length;

            foreach (string content in contents)
            {
                if (content.Length > length)
                {
                    if (content.Trim().Substring(0, length) == keyword)
                    {
                        string[] str = content.Split(' ');
                        double d = 0.0;
                        List<double> values = new List<double>();

                        for (int j = 0; j < str.Length; j++)
                            if (double.TryParse(str[j], out d))
                                values.Add(d);

                        return values;
                    }
                }                    
            }
            return null;
        }

        private void Write0DPyFile()
        {
            List<string> contents = new List<string>();

            contents.Add("from __future__ import print_function");
            contents.Add("from __future__ import division");
            contents.Add("");
            contents.Add("import cantera as ct");
            contents.Add("import numpy as np");
            contents.Add("");
            contents.Add("combustion = ct.Solution('gri30.xml')");
            contents.Add("");
            contents.Add("T_mgas = " + (T_mgas + 273.15).ToString());
            contents.Add("p_mgas = " + (p_mgas * 9.80665).ToString() + " + ct.one_atm");
            contents.Add("X_mgas = " + GetGasFriction(M_Gas));
            contents.Add("");
            contents.Add("M_Gas = ct.Quantity(combustion, constant='HP')");
            contents.Add("M_Gas.TPX = T_mgas, p_mgas, X_mgas");
            contents.Add("M_Gas.moles = " + (Q_mgas * 1000.0 / 24.4 / 3600.0).ToString());
            contents.Add("");
            contents.Add("T_air = " + (T_air + 273.15).ToString());
            contents.Add("p_air = " + (p_air * 133.3224).ToString() + " + ct.one_atm");
            contents.Add("X_air = " + GetGasFriction(Air));
            contents.Add("");
            contents.Add("Air = ct.Quantity(combustion, constant='HP')");
            contents.Add("Air.TPX = T_air, p_air, X_air");
            contents.Add("Air.moles = " + (Q_air * 1000.0 / 24.4 / 3600.0).ToString());
            contents.Add("");
            contents.Add("Fuel = Air + M_Gas");
            contents.Add("combustion.equilibrate('HP')");
            contents.Add("");
            contents.Add("combustion()");
            contents.Add("print(\"end simulation\")");

            File.WriteAllLines("0D.py", contents);
        }
        private void Write1DPyFile()
        {
            List<string> contents = new List<string>();

            contents.Add("from __future__ import print_function");
            contents.Add("from __future__ import division");
            contents.Add("");
            contents.Add("import cantera as ct");
            contents.Add("import numpy as np");
            contents.Add("");
            contents.Add("combustion = ct.Solution('gri30.xml')");
            contents.Add("");
            contents.Add("T_mgas = " + (T_mgas + 273.15).ToString());
            contents.Add("p_mgas = " + (p_mgas * 9.80665).ToString() + " + ct.one_atm");
            contents.Add("X_mgas = " + GetGasFriction(M_Gas));
            contents.Add("");
            contents.Add("M_Gas = ct.Quantity(combustion, constant='HP')");
            contents.Add("M_Gas.TPX = T_mgas, p_mgas, X_mgas");
            contents.Add("M_Gas.moles = " + (Q_mgas * 1000.0 / 24.4 / 3600.0).ToString());
            contents.Add("");
            contents.Add("T_air = " + (T_air + 273.15).ToString());
            contents.Add("p_air = " + (p_air * 133.3224).ToString() + " + ct.one_atm");
            contents.Add("X_air = " + GetGasFriction(Air));
            contents.Add("");
            contents.Add("Air = ct.Quantity(combustion, constant='HP')");
            contents.Add("Air.TPX = T_air, p_air, X_air");
            contents.Add("Air.moles = " + (Q_air * 1000.0 / 24.4 / 3600.0).ToString());
            contents.Add("");
            contents.Add("Fuel = Air + M_Gas");
            contents.Add("");
            contents.Add("Flame = ct.FreeFlame(combustion, width =" + FlameWidth.ToString() + ")");
            contents.Add("Flame.set_refine_criteria(ratio=3, slope=0.06, curve=0.12)");
            if (IsAuto)
                contents.Add("Flame.solve(loglevel = 1, auto = True)");
            else
                contents.Add("Flame.solve(loglevel = 1, auto = False)");
            contents.Add("");
            contents.Add("combustion()");
            contents.Add("print(\"end simulation\")");

            File.WriteAllLines("1D.py", contents);
        }
        private string GetGasFriction(CGas gas)
        {
            string str = "\'";
            foreach (KeyValuePair<CGas.Composition, double> entry in gas.MoleFraction)
                str += entry.Key.ToString() + ":" + entry.Value.ToString() + ",";
            str = str.Substring(0, str.Length - 1) + "\'";

            return str;
        }
    }
}
