using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Forms;

namespace HBS_Shared
{
    public class CValidityCheck
    {
        /// <summary>
        /// Check the textbox. Is input text number?
        /// if value is number, the method will return true.
        /// </summary>
        /// <param name="textBox">Textbox</param>
        /// <param name="e">CancelEventArgs</param>
        /// <param name="value">Output value</param>
        /// <returns>true/false</returns>
        public static bool IsNumber(TextBox textBox, CancelEventArgs e, out double value)
        {
            if (!double.TryParse(textBox.Text, out value))
            {
                e.Cancel = true;
                MessageBox.Show(Properties.Settings.Default.MSG_INPUT_NUMBER, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
                return true;
        }

        /// <summary>
        /// Check that the value is positive.
        /// </summary>
        /// <param name="value">Value (x)</param>
        /// <param name="isWithZero">if (x ≥ 0), please input false, if (x > 0), please input true.</param>
        /// <param name="e">CancelEventArgs</param>
        /// <returns>true/false</returns>
        public static bool IsPositiveNumber(double value, bool isWithZero, CancelEventArgs e)
        {
            if (isWithZero)
            {
                if (value < 0.0)
                {
                    e.Cancel = true;
                    MessageBox.Show(Properties.Settings.Default.MSG_INPUT_POSITIVE_NUMBER_WITHOUT_ZERO, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                else
                    return true;
            }
            else
            {
                if (value <= 0.0)
                {
                    e.Cancel = true;
                    MessageBox.Show(Properties.Settings.Default.MSG_INPUT_POSITIVE_NUMBER_WITHOUT_ZERO, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                else
                    return true;
            }
        }

        /// <summary>
        /// Is 
        /// </summary>
        /// <param name="e"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsInRangeTemp(CancelEventArgs e, double min, double max, double value)
        {
            if (value < min || value > max)
            {
                e.Cancel = true;
                MessageBox.Show(Properties.Settings.Default.MSG_INPUT_NUMBER_RANGE_TEMPERATURE.Replace("aaa", min.ToString("#,000.0")).Replace("bbb", max.ToString("#,000.0")), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
                return true;
        }

        public static bool IsInRangePressure(CancelEventArgs e, double min, double max, double value)
        {
            if (value < min || value > max)
            {
                e.Cancel = true;
                MessageBox.Show(Properties.Settings.Default.MSG_INPUT_NUMBER_RANGE_PRESSURE.Replace("aaa", min.ToString("#,000.0")).Replace("bbb", max.ToString("#,000.0")), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
                return true;
        }

        public static bool IsInRangeFlowRate(CancelEventArgs e, double min, double max, double value)
        {
            if (value < min || value > max)
            {
                e.Cancel = true;
                MessageBox.Show(Properties.Settings.Default.MSG_INPUT_NUMBER_RANGE_FLOWRATE.Replace("aaa", min.ToString("#,000.0")).Replace("bbb", max.ToString("#,000.0")), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
                return true;
        }

        public static bool IsInRangeCycle (CancelEventArgs e, double min, double max, double value)
        {
            if (value < min || value > max)
            {
                e.Cancel = true;
                MessageBox.Show(HBS_Shared.Properties.Settings.Default.MSG_INPUT_NUMBER_RANGE_CYCLE.Replace("aaa", min.ToString("#,0")).Replace("bbb", max.ToString("#,0")), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
                return true;
        }

        public static bool IsInRangeValue(CancelEventArgs e, double min, double max, double value)
        {
            if (value < min || value > max)
            {
                e.Cancel = true;
                MessageBox.Show(HBS_Shared.Properties.Settings.Default.MSG_INPUT_VALUE_RANGE.Replace("aaa", min.ToString("0.####")).Replace("bbb", max.ToString("0.####")), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
                return true;
        }

        public static bool IsInRangeDynamicViscosity(CancelEventArgs e, double min, double max, double value)
        {
            if (value < min || value > max)
            {
                e.Cancel = true;
                MessageBox.Show(HBS_Shared.Properties.Settings.Default.MSG_INPUT_VALUE_RANGE.Replace("aaa", min.ToString("E2")).Replace("bbb", max.ToString("E2")), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
                return true;
        }

    }
}
