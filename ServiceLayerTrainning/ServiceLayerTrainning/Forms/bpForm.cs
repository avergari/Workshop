using ServiceLayerTrainning.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServiceLayerTrainning.Forms
{
    public partial class bpForm : Form
    {
        BusinessPartnersModel _bps = new BusinessPartnersModel();

        public bpForm()
        {
            InitializeComponent();
        }

        private void bpForm_Load(object sender, EventArgs e)
        {
            try
            {


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
