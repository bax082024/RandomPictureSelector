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
            set { numMinShuffleCount.Value = value; }
        }

        public int ShuffleSpeed
        {
            get { return (int)numShuffleSpeed.Value; }
            set { numShuffleSpeed.Value = value; }
        }

        public SettingsForm()
        {
            InitializeComponent();
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
