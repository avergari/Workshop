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
    public partial class resultForm : Form
    {
        private Int32 rowIndex = -1;

        BusinessPartnersModel _bps = new BusinessPartnersModel();
        public String CardCode { get; set; }
        public String CardName { get; set; }

        public resultForm(BusinessPartnersModel bps)
        {
            InitializeComponent();
            _bps = bps;
        }

        private void resultForm_Load(object sender, EventArgs e)
        {
            try
            {
                gridBps.DataSource = _bps.value;
                gridBps.AutoResizeColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gridBps_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            rowIndex = e.RowIndex;
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            CardCode = gridBps.Rows[rowIndex].Cells[0].Value.ToString();
            CardName = gridBps.Rows[rowIndex].Cells[1].Value.ToString();

            this.Close();
        }
    }
}
