using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HBS_Solver;
using HBS_Shared;
using System.Diagnostics;

namespace HBS_Solver_Console_Exe
{
    partial class Program
    {
        /// <summary>
        /// Version 1.1
        /// </summary>
        static void Main(string[] args)
        {
            SolverMain solver = new SolverMain();
            solver.Excute();
        }
    }
}