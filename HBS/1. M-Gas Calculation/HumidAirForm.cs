using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HBS_Shared;

namespace HBS
{
    public partial class HumidAirForm : Form
    {
     
        public CGas DryAir { get; set; }
        public CGas HumidAir { get; set; }

        public HumidAirForm()
        {
            InitializeComponent();

            foreach (CGas.Composition composition in CGas.GetCompositionList())
            {
                if (composition == CGas.Composition.N2 || composition == CGas.Composition.O2 || composition == CGas.Composition.Ar || composition == CGas.Composition.CO2 || composition == CGas.Composition.H2O)
                {
                    object[] rows1 = new object[] { composition, null, null, null, null, null };
                    dgvGasCompositions.Rows.Add(rows1);
                }
            }
        }

        private void HumidAirForm_Load(object sender, EventArgs e)
        {
            DataGridView dataGridView = dgvGasCompositions;

            CGas DryAir = new CGas();
            CGas HumidAir = new CGas();
            CGas.Fraction air = new CGas.Fraction(0.0);

            DryAir.MoleFraction = CGas.Fraction.DryAir();
            DryAir.MassFraction = DryAir.MoleFraction.GetMassFraction();
            HumidAir.MoleFraction = air.GetMoleFraction();


            for (int i = 0; i < dataGridView.RowCount; i++ )
            {
                CGas.Composition comp = (CGas.Composition)Enum.Parse(typeof(CGas.Composition), dataGridView[0, i].Value.ToString());

                dataGridView[1, i].Value = CGas.MolarMass[comp].ToString("#0.00");
                dataGridView[2, i].Value = (DryAir.MoleFraction[comp] * 100.0).ToString("#0.00");
                dataGridView[3, i].Value = (HumidAir.MoleFraction[comp] * 100.0).ToString("#0.00");
            }
        }

        private void btnCalculation_Click(object sender, EventArgs e)
        {
            try
            {
                double relativeHumidity = double.Parse(txtRelativeHumidity.Text);
                double temperature = double.Parse(txtTemperature.Text);
                double satVaporDensity = GetSaturatedVaporDensity(temperature);
                txtSatVaporDensity.Text = satVaporDensity.ToString("#0.00");

                double relativeVaporDensity = relativeHumidity / 100.0 * satVaporDensity;

                CGas.Fraction molarMass = CGas.MolarMass;
                CGas humidAir = new CGas();
                humidAir.MassFraction = new CGas.Fraction(0);

                for (int i = 0; i < dgvGasCompositions.RowCount; i++)
                {
                    CGas.Composition comp = (CGas.Composition)Enum.Parse(typeof(CGas.Composition), dgvGasCompositions[0, i].Value.ToString());

                    if (comp == CGas.Composition.H2O)
                        humidAir.MassFraction[comp] = relativeVaporDensity;
                    else
                        humidAir.MassFraction[comp] = molarMass[comp] * DryAir.MoleFraction[comp] * 1000.0 / 22.4;
                }

                humidAir.MassFraction.Normalize();
                humidAir.MoleFraction = humidAir.MassFraction.GetMoleFraction();
                HumidAir = humidAir;

                for (int i = 0; i < dgvGasCompositions.RowCount; i++)
                {
                    CGas.Composition comp = (CGas.Composition)Enum.Parse(typeof(CGas.Composition), dgvGasCompositions[0, i].Value.ToString());

                    dgvGasCompositions[3, i].Value = (HumidAir.MoleFraction[comp] * 100.0).ToString("#0.00");
                }
            }
            catch (System.Exception exception)
            {
                MessageBox.Show(exception.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private double GetSaturatedVaporDensity(double temperature)
        {
            if (temperature < -10 || temperature > 100)
                throw new System.Exception("Temperature range is -10 < T <= 100");

            return 7.30952245413030E-04 * Math.Pow(temperature, 3.0) - 2.08873272053899E-02 * Math.Pow(temperature, 2.0) + 6.71790470960837E-01 * temperature + 6.33540346956122E+00;
        }
    }
}