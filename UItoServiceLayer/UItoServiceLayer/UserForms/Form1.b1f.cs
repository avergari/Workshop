using System;
using System.Collections.Generic;
using System.Xml;
using SAPbouiCOM.Framework;
using UItoServiceLayer.Models;
using UItoServiceLayer.Business;
using UItoServiceLayer.Core;

namespace UItoServiceLayer
{
    [FormAttribute("UItoServiceLayer.Form1", "UserForms/Form1.b1f")]
    class Form1 : UserFormBase
    {
        public Form1()
        {
        }

        /// <summary>
        /// Initialize components. Called by framework after form created.
        /// </summary>
        public override void OnInitializeComponent()
        {
            this.Button0 = ((SAPbouiCOM.Button)(this.GetItem("btInsert").Specific));
            this.Button0.PressedAfter += new SAPbouiCOM._IButtonEvents_PressedAfterEventHandler(this.Button0_PressedAfter);
            this.EditText0 = ((SAPbouiCOM.EditText)(this.GetItem("txtItCode").Specific));
            this.EditText1 = ((SAPbouiCOM.EditText)(this.GetItem("txtItName").Specific));
            this.StaticText0 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_3").Specific));
            this.StaticText1 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_4").Specific));
            this.OnCustomInitialize();

        }

        /// <summary>
        /// Initialize form event. Called by framework before form creation.
        /// </summary>
        public override void OnInitializeFormEvents()
        {
        }

        private SAPbouiCOM.Button Button0;

        private void OnCustomInitialize()
        {

        }

        private SAPbouiCOM.EditText EditText0;
        private SAPbouiCOM.EditText EditText1;
        private SAPbouiCOM.StaticText StaticText0;
        private SAPbouiCOM.StaticText StaticText1;

        private void Button0_PressedAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            try
            {
                var _itemCode = this.UIAPIRawForm.DataSources.UserDataSources.Item("UD_0").Value;
                var _itemName = this.UIAPIRawForm.DataSources.UserDataSources.Item("UD_1").Value;
                
                
                ItensModel itemModel = new ItensModel();

                itemModel.ItemCode = _itemCode;
                itemModel.ItemName = _itemName;

                ResponseModel responseItem = new ResponseModel();

                responseItem = ItemOperation.InsertItem(itemModel, CommonClass.SLTOKEN);

                Application.SBO_Application.SetStatusBarMessage("Item: " + responseItem.data + " criado com sucesso pela Service Layer...", SAPbouiCOM.BoMessageTime.bmt_Short, false);
                
                this.UIAPIRawForm.DataSources.UserDataSources.Item("UD_0").Value = String.Empty;
                this.UIAPIRawForm.DataSources.UserDataSources.Item("UD_1").Value = String.Empty;
            }
            catch (Exception ex)
            {
                Application.SBO_Application.SetStatusBarMessage(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, true);
            }
        }
    }
}