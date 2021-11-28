using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBS_Shared
{
    public class CMatricReport
    {
        public static double VolumeAverageValue(List<double> dVs, List<double> values)
        {
            double numerator = 0.0;
            double denominator = 0.0;
            for (int i = 0; i < dVs.Count; i++)
            {
                numerator += dVs[i] * values[i];
                denominator += dVs[i];
            }
            return numerator / denominator;
        }

        public static double LengthAverageValue(CCell cell_0, CCell cell_1, CCell.DataOrder order)
        {
            double phi_0, phi_1;
            switch (order)
            {
                case CCell.DataOrder.DV:
                    phi_0 = cell_0.DV;
                    phi_1 = cell_1.DV;
                    break;
                case CCell.DataOrder.A_n:
                    phi_0 = cell_0.A_n;
                    phi_1 = cell_1.A_n;
                    break;
                case CCell.DataOrder.A_s:
                    phi_0 = cell_0.A_s;
                    phi_1 = cell_1.A_s;
                    break;
                default:
                    throw CException.Show(CException.Type.UnsupportedKeyword);
            
            }

            if (cell_0.IsFirst)
                return phi_0 + cell_1.Dx_w / (cell_1.Dx_we) * phi_1;
            else if (cell_1.IsLast)
                return cell_0.Dx_e / (cell_0.Dx_we) * phi_0 + phi_1;
            else
                return cell_0.Dx_e / (cell_0.Dx_we) * phi_0 + cell_1.Dx_w / (cell_1.Dx_we) * phi_1;
        }

        public static double VolumeAverageValue(CCell cell1, CCell cell2, CCell.DataOrder order)
        {
            double numerator = 0.0;

            switch (order)
            {
                case CCell.DataOrder.u:
                    numerator = cell1.u * cell1.DV + cell2.u * cell2.DV;
                    break;
                case CCell.DataOrder.p:
                    numerator = cell1.p * cell1.DV + cell2.p * cell2.DV;
                    break;
                case CCell.DataOrder.T:
                    numerator = cell1.T * cell1.DV + cell2.T * cell2.DV;
                    break;
                case CCell.DataOrder.rho:
                    numerator = cell1.rho * cell1.DV + cell2.rho * cell2.DV;
                    break;
                case CCell.DataOrder.c_p:
                    numerator = cell1.c_p * cell1.DV + cell2.c_p * cell2.DV;
                    break;
                case CCell.DataOrder.k:
                    numerator = cell1.k * cell1.DV + cell2.k * cell2.DV;
                    break;
                case CCell.DataOrder.mu:
                    numerator = cell1.mu * cell1.DV + cell2.mu * cell2.DV;
                    break;
                default:
                    throw CException.Show(CException.Type.UnsupportedKeyword);
            }
           
            double denominator = cell1.DV + cell2.DV;

            return numerator / denominator;
        }

        public static double PartialLinearInterpolatedValue(List<double> x, List<double> y, double x0)
        {
            List<double> distance = new List<double>();
            
            for (int i = 0; i < x.Count; i++)
            {
                if (x[i] == x0)
                    return y[i];

                distance.Add(Math.Abs(x[i] - x0));
            }

            double distance_min = distance.Min();
            int i1 = distance.IndexOf(distance_min);

            int i2;
            if (i1 == 0)
                i2 = 1;
            else if (i1 == x.Count - 1)
                i2 = x.Count - 2;
            else
            {
                if (x[i1] > x0)
                    i2 = i1 - 1;
                else
                    i2 = i1 + 1;
            }

            return y[i1] +  (y[i2] - y[i1]) / (x[i2] - x[i1]) * (x0 - x[i1]);
        }
    }
}
