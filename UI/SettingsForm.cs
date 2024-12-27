using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RandomPictureSelector.UI
{
    

    public partial class SettingsForm : Form
    {
        public int MinShuffleCount
        {
            get { return (int)numMinShuffleCount.Value; }
            set
            {
                // Ensure the value is within the allowed range
                value = Math.Clamp(value, (int)numMinShuffleCount.Minimum, (int)numMinShuffleCount.Maximum);
                numMinShuffleCount.Value = value;
            }
        }


        public int ShuffleSpeed
        {
            get { return (int)numShuffleSpeed.Value; }
            set
            {
                // Ensure the value is within the allowed range
                value = Math.Clamp(value, (int)numShuffleSpeed.Minimum, (int)numShuffleSpeed.Maximum);
                numShuffleSpeed.Value = value;
            }
        }


        public SettingsForm()
        {
            InitializeComponent();

            numMinShuffleCount.Minimum = 1; // Minimum allowed shuffle count
            numMinShuffleCount.Value = 20; // Default shuffle count
            numShuffleSpeed.Minimum = 10; // Minimum allowed shuffle speed
            numShuffleSpeed.Value = 100;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
