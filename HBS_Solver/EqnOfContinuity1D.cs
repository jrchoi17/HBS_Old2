using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HBS_Shared;

namespace HBS_Solver
{
    public class EqnOfContinuity1D
    {
        public static void Solving(CRegion region, ST_SD.FlowDirection dir)
        {
            ST_SD sd = ST_SD.GetInstance();

            List<CPseudoCell> pcells = region.PCells;
            List<CCell> cells = region.Cells;
            List<CCell> cells_0 = region.Region0.Cells;
            
            switch(dir)
            {
                case ST_SD.FlowDirection.WestToEast:
                    for (int I = 0; I < region.N; I++)
                    {
                        double F_w = pcells[I].F_1;
                        double u_e = 1.0 / (pcells[I + 1].rho * cells[I].A_e) * (F_w - (cells[I].rho - cells_0[I].rho) * cells[I].DV / sd.Dt);
                        pcells[I + 1].u = u_e;
                    }
                    break;
                case ST_SD.FlowDirection.EastToWest:
                    for (int I = region.N - 1; I >= 0; I--)
                    {
                        double F_e = pcells[I + 1].F_1;
                        double u_w = 1.0 / (pcells[I].rho * cells[I].A_w) * (F_e + (cells[I].rho - cells_0[I].rho) * cells[I].DV / sd.Dt);
                        pcells[I].u = u_w;
                    }
                    break;
                default:
                    throw CException.Show(CException.Type.UnsupportedKeyword);
            }
        }
    }
}
