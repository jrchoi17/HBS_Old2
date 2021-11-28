using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBS_Shared
{
    public static class CHeatTransferRadiation
    {
        public static double epsilon(double p_t, double T, double X_CO2, double X_H2O, double L)
        {
            return epsilon_BordbarModel(p_t, T, X_CO2, X_H2O, L);
        }

        public static double epsilon_BordbarModel(double p_t, double T, double X_CO2, double X_H2O, double L)
        {
            double epsilon_g = 0.0;
            int N_g = BordbarModel.N_g; // the number of gray gases used in the model
            List<double> a = BordbarModel.a(T, X_CO2, X_H2O); // weight factors
            List<double> K = BordbarModel.K(X_CO2, X_H2O); // absorption coefficients of gray gases.

            for (int i = 0; i < N_g; i++)
            {
                epsilon_g += a[i] * (1.0 - Math.Exp(-K[i] * p_t * (X_CO2 + X_H2O) * L));
            }

            return epsilon_g;
        }

        public static double alpha(double p_t, double T, double X_CO2, double X_H2O, double L)
        {
            return epsilon(p_t, T, X_CO2, X_H2O, L);
        }

        public class BordbarModel
        {
            // the number of gray gases used in the model
            public const int N_g = 4;

            public static List<double> K(double X_CO2, double X_H2O)
            {
                double M_r = X_H2O / X_CO2;
                List<double> KK = new List<double>();
                List<List<double>> dd = d();
                
                for (int i = 0; i < N_g; i++)
                {
                    double K_i = 0.0;
                    for (int k = 0; k < 5; k++)
                    {
                        K_i += dd[i][k] * Math.Pow(M_r, k);
                    }
                    KK.Add(K_i);
                }

                return KK;
            }

            public static List<double> a(double T, double X_CO2, double X_H2O)
            {
                List<double> aa = new List<double>();

                double T_r = T / 1200.0;
                List<List<double>> bb = b(X_CO2, X_H2O);

                for (int i = 0; i < N_g; i++)
                {
                    double a_i = 0.0;

                    for (int j = 0; j < 5; j++)
                        a_i += bb[i][j] * Math.Pow(T_r, j);
                    
                    aa.Add(a_i);
                }

                return aa;
            }

            public static List<List<double>> b(double X_CO2, double X_H2O)
            {
                double M_r = X_H2O / X_CO2;
                List<List<double>> bb = new List<List<double>>();
                List<List<List<double>>> cc = c();

                for (int i = 0; i < N_g; i++)
                {
                    List<double> bb_i = new List<double>();
                    for (int j = 0; j < 5; j++) 
                    {
                        double bb_ij = 0.0;
                        for (int k = 0; k < 5; k++)
                        {
                            bb_ij += cc[i][j][k] * Math.Pow(M_r, k);
                        }
                        bb_i.Add(bb_ij);
                    }
                    bb.Add(bb_i);
                }

                return bb;
            }

            public static List<List<List<double>>> c()
            {
                // i = 1;
                double c_100 =  0.7412956;   double c_101 = -0.5244441;   double c_102 =  0.5822860;   double c_103 = -0.2096994;   double c_104 =  0.02420312;
                double c_110 = -0.9412652;   double c_111 =  0.2799577;   double c_112 = -0.7672319;   double c_113 =  0.3204027;   double c_114 = -0.03910174;
                double c_120 =  0.8531866;   double c_121 =  0.08230754;   double c_122 =  0.5289430;   double c_123 = -0.2468463;   double c_124 =  0.03109396;
                double c_130 = -0.3342806;   double c_131 =  0.1474987;   double c_132 = -0.4160689;   double c_133 =  0.1697627;   double c_134 = -0.0204066;
                double c_140 =  0.04314362;   double c_141 = -0.06886217;   double c_142 =  0.1109773;   double c_143 = -0.04208608;   double c_144 =  0.004918817;

                List<double> c_10k = (new double[] { c_100, c_101, c_102, c_103, c_104 }).ToList();
                List<double> c_11k = (new double[] { c_110, c_111, c_112, c_113, c_114 }).ToList();
                List<double> c_12k = (new double[] { c_120, c_121, c_122, c_123, c_124 }).ToList();
                List<double> c_13k = (new double[] { c_130, c_131, c_132, c_133, c_134 }).ToList();
                List<double> c_14k = (new double[] { c_140, c_141, c_142, c_143, c_144 }).ToList();

                List<List<double>> c_1jk = new List<List<double>>();
                c_1jk.Add(c_10k); c_1jk.Add(c_11k); c_1jk.Add(c_12k); c_1jk.Add(c_13k); c_1jk.Add(c_14k);

                // i = 2;
                double c_200 =  0.1552073;   double c_201 = -0.4862117;   double c_202 =  0.3668088;   double c_203 = -0.1055508;   double c_204 =  0.01058568;
                double c_210 =  0.6755648;   double c_211 =  1.4092710;   double c_212 = -1.3834490;   double c_213 =  0.4575210;   double c_214 = -0.0501976;
                double c_220 = -1.1253940;   double c_221 = -0.5913199;   double c_222 =  0.9085441;   double c_223 = -0.3334201;   double c_224 =  0.03842361;
                double c_230 =  0.6040543;   double c_231 = -0.05533854;   double c_232 = -0.1733014;   double c_233 =  0.07916083;   double c_234 = -0.009893357;
                double c_240 = -0.1105453;   double c_241 =  0.04646634;   double c_242 = -0.001612982;   double c_243 = -0.003539835;   double c_244 =  0.000612177;

                List<double> c_20k = (new double[] { c_200, c_201, c_202, c_203, c_204 }).ToList();
                List<double> c_21k = (new double[] { c_210, c_211, c_212, c_213, c_214 }).ToList();
                List<double> c_22k = (new double[] { c_220, c_221, c_222, c_223, c_224 }).ToList();
                List<double> c_23k = (new double[] { c_230, c_231, c_232, c_233, c_234 }).ToList();
                List<double> c_24k = (new double[] { c_240, c_241, c_242, c_243, c_244 }).ToList();

                List<List<double>> c_2jk = new List<List<double>>();
                c_2jk.Add(c_20k); c_2jk.Add(c_21k); c_2jk.Add(c_22k); c_2jk.Add(c_23k); c_2jk.Add(c_24k);

                // i = 3;
                double c_300 =  0.2550242;   double c_301 =  0.3805403;   double c_302 = -0.4249709;   double c_303 =  0.1429446;   double c_304 = -0.01574075;
                double c_310 = -0.6065428;   double c_311 =  0.3494024;   double c_312 =  0.1853509;   double c_313 = -0.1013694;   double c_314 =  0.01302441;
                double c_320 =  0.8123855;   double c_321 = -1.1020090;   double c_322 =  0.4046178;   double c_323 = -0.08118223;   double c_324 =  0.006298101;
                double c_330 = -0.4532290;   double c_331 =  0.6784475;   double c_332 = -0.3432603;   double c_333 =  0.08830883;   double c_334 = -0.008415221;
                double c_340 =  0.08693093;   double c_341 = -0.1306996;   double c_342 =  0.07414464;   double c_343 = -0.02029294;   double c_344 =  0.002010969;

                List<double> c_30k = (new double[] { c_300, c_301, c_302, c_303, c_304 }).ToList();
                List<double> c_31k = (new double[] { c_310, c_311, c_312, c_313, c_314 }).ToList();
                List<double> c_32k = (new double[] { c_320, c_321, c_322, c_323, c_324 }).ToList();
                List<double> c_33k = (new double[] { c_330, c_331, c_332, c_333, c_334 }).ToList();
                List<double> c_34k = (new double[] { c_340, c_341, c_342, c_343, c_344 }).ToList();

                List<List<double>> c_3jk = new List<List<double>>();
                c_3jk.Add(c_30k); c_3jk.Add(c_31k); c_3jk.Add(c_32k); c_3jk.Add(c_33k); c_3jk.Add(c_34k);

                // i = 4;
                double c_400 = -0.03451994;   double c_401 =  0.2656726;   double c_402 = -0.1225365;   double c_403 =  0.03001508;   double c_404 = -0.002820525;
                double c_410 =  0.4112046;   double c_411 = -0.5728350;   double c_412 =  0.2924490;   double c_413 = -0.07980766;   double c_414 =  0.007996603;
                double c_420 = -0.5055995;   double c_421 =  0.4579559;   double c_422 = -0.2616436;   double c_423 =  0.07648413;   double c_424 = -0.007908356;
                double c_430 =  0.2317509;   double c_431 = -0.1656759;   double c_432 =  0.1052608;   double c_433 = -0.03219347;   double c_434 =  0.003386965;
                double c_440 = -0.03754908;   double c_441 =  0.02295193;   double c_442 = -0.01600472;   double c_443 =  0.005046318;   double c_444 = -0.0005364326;

                List<double> c_40k = (new double[] { c_400, c_401, c_402, c_403, c_404 }).ToList();
                List<double> c_41k = (new double[] { c_410, c_411, c_412, c_413, c_414 }).ToList();
                List<double> c_42k = (new double[] { c_420, c_421, c_422, c_423, c_424 }).ToList();
                List<double> c_43k = (new double[] { c_430, c_431, c_432, c_433, c_434 }).ToList();
                List<double> c_44k = (new double[] { c_440, c_441, c_442, c_443, c_444 }).ToList();

                List<List<double>> c_4jk = new List<List<double>>();
                c_4jk.Add(c_40k); c_4jk.Add(c_41k); c_4jk.Add(c_42k); c_4jk.Add(c_43k); c_4jk.Add(c_44k);

                List <List<List<double>>> c = new List<List<List<double>>>();
                c.Add(c_1jk);
                c.Add(c_2jk);
                c.Add(c_3jk);
                c.Add(c_4jk);

                return c;
            }

            public static List<List<double>> d()
            {
                // i = 1
                double d_10 =  0.03404288;   double d_11 =  0.06523048;   double d_12 = -0.04636852;   double d_13 =  0.01386835;   double d_14 = -0.001444993;

                List<double> d_1k = (new double[] { d_10, d_11, d_12, d_13, d_14 }).ToList();

                // i = 2
                double d_20 =  0.3509457;   double d_21 =  0.7465138;   double d_22 = -0.5293090;   double d_23 =  0.1594423;   double d_24 = -0.01663261;

                List<double> d_2k = (new double[] { d_20, d_21, d_22, d_23, d_24 }).ToList();

                // i = 3
                double d_30 =  4.5707400;   double d_31 =  2.1680670;   double d_32 = -1.4989010;   double d_33 =  0.4917165;   double d_34 = -0.0542999;

                List<double> d_3k = (new double[] { d_30, d_31, d_32, d_33, d_34 }).ToList();

                // i = 4
                double d_40 =  109.81690;   double d_41 = -50.923590;   double d_42 =  23.432360;   double d_43 = -5.1638920;   double d_44 = 0.4393889;

                List<double> d_4k = (new double[] { d_40, d_41, d_42, d_43, d_44 }).ToList();

                List<List<double>> dd = new List<List<double>>();
                dd.Add(d_1k); dd.Add(d_2k); dd.Add(d_3k); dd.Add(d_4k);

                return dd;
            }
        }
    }
}
