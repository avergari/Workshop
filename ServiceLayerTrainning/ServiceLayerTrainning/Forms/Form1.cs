using ServiceLayerTrainning.Business;
using ServiceLayerTrainning.Forms;
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

namespace ServiceLayerTrainning
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btLogin_Click(object sender, EventArgs e)
        {
            SAPClass sapClass = new SAPClass(txtURL.Text);

            try
            {
                LoginModel login = new LoginModel();

                login.CompanyDB = txtDb.Text;
                login.UserName = txtUser.Text;
                login.Password = txtPassword.Text;

                txtSession.Text = sapClass.SAPLogin(login).data;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SAPClass sapClass = new SAPClass(txtURL.Text);
            ResponseModel responseOrder = new ResponseModel();

            try
            {
                OrdersModel oOrder = new OrdersModel();

                oOrder.CardCode = txtCardCode.Text;
                oOrder.DocDate = DateTime.Now;
                oOrder.DocDueDate = DateTime.Now;
                oOrder.TaxDate = DateTime.Now;
                oOrder.SlpCode = -1;

                foreach (DataGridViewRow item in gridItems.Rows)
                {
                    ItensModel oItem = new ItensModel();
                    if (item.Cells[0].Value != null)
                    {
                        oItem.ItemCode = item.Cells[0].Value.ToString();
                        oItem.Quantity = item.Cells[1].Value.ToString();
                        oItem.UnitPrice = item.Cells[2].Value.ToString();

                        oOrder.DocumentLines.Add(oItem);
                    }
                }

                responseOrder = sapClass.InsertOrder(oOrder);

                if (responseOrder.Msg == "")
                    txtDocNum.Text = responseOrder.data;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        private void button1_Click_1(object sender, EventArgs e)
        {
            SAPClass sapClass = new SAPClass(txtURL.Text);

            try
            {
                var customers = sapClass.GetBusinessPartners();

                resultForm rForm = new resultForm(customers);
                rForm.ShowDialog();

                if (rForm.CardCode != "")
                {
                    txtCardCode.Text = rForm.CardCode;
                    txtCardName.Text = rForm.CardName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btAddPo_Click(object sender, EventArgs e)
        {
            SAPClass sapClass = new SAPClass(txtURL.Text);
            ResponseModel responseProductionOrder = new ResponseModel();

            try
            {
               ProductionOrderModel oPOrder = new ProductionOrderModel();

                oPOrder.ItemNo = "LM4029";
                oPOrder.DueDate = DateTime.Now;

                responseProductionOrder = sapClass.InsertProductionOrder(oPOrder);

                if (responseProductionOrder.Msg == "")
                    txtPoNum.Text = responseProductionOrder.data;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SAPClass sapClass = new SAPClass(txtURL.Text);

            try
            {
                var customers = sapClass.GetBOMItems();

                //resultForm rForm = new resultForm(customers);
                //rForm.ShowDialog();

                //if (rForm.CardCode != "")
                //{
                //    txtCardCode.Text = rForm.CardCode;
                //    txtCardName.Text = rForm.CardName;
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
