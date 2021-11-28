using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitClassTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Temperature t1 = new Temperature(200, Temperature.Unit.C);
            Temperature t2 = new Temperature(300, Temperature.Unit.K);

            Console.WriteLine("t1 = {0}", t1);
            Console.WriteLine("t1 = {0} C", t1[Temperature.Unit.C]);
            Console.WriteLine("t1 = {0} K", t1[Temperature.Unit.K]);

            Console.WriteLine("t2 = {0} C", t2[Temperature.Unit.C]);
            Console.WriteLine("t2 = {0} K", t2[Temperature.Unit.K]);

            Temperature t3 = t2 + t1;
            Console.WriteLine("t3 = {0} C", t3[Temperature.Unit.C]);

            double t4 = t2 / t1;
            double t5 = t2;
            Temperature t6 = 2;


            Console.WriteLine("t4 = {0} K", t4);

            FlowRate fr1 = 1;
            Console.WriteLine("fr1 = {0} CMS", fr1);
            Console.WriteLine("fr1 = {0} CMM", fr1[FlowRate.Unit.CMM]);
            Console.WriteLine("fr1 = {0} CMH", fr1[FlowRate.Unit.CMH]);
            Console.WriteLine("fr1 = {0} lpm", fr1[FlowRate.Unit.lpm]);

            Pressure p1 = 100000;
            Console.WriteLine("p1 = {0} pa", p1);
            Console.WriteLine("p1 = {0} mmAq", p1[Pressure.Unit.mmAq]);
            Console.WriteLine("p1 = {0} bar", p1[Pressure.Unit.bar]);
            Console.WriteLine("p1 = {0} psi", p1[Pressure.Unit.psi]);

            Time time1 = 86400;
            Console.WriteLine("time1 = {0} s", time1);
            Console.WriteLine("time1 = {0} min", time1[Time.Unit.min]);
            Console.WriteLine("time1 = {0} hr", time1[Time.Unit.hr]);
            Console.WriteLine("time1 = {0} day", time1[Time.Unit.day]);

            Percent per1 = 1;
            Console.WriteLine("per1 = {0} num", per1);
            Console.WriteLine("per1 = {0} %", per1[Percent.Unit.percent]);
            Console.WriteLine("per1 = {0} ‰", per1[Percent.Unit.permille]);

            Length len1 = 1;
            Console.WriteLine("len1 = {0} num", len1);
            Console.WriteLine("len1 = {0} ", len1[Length.Unit.m]);
            Console.WriteLine("len1 = {0} ", len1[Length.Unit.mm]);
            Console.WriteLine("len1 = {0} ", len1[Length.Unit.cm]);
            Console.WriteLine("len1 = {0} ", len1[Length.Unit.km]);
        }
    }
}
