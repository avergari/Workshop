using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayerTrainning.Models
{
    class OrdersModel
    {
        public virtual Int32 DocEntry { get; set; }
        public virtual String CardCode { get; set; }
        public virtual DateTime TaxDate { get; set; }
        public virtual DateTime DocDate { get; set; }
        public virtual DateTime DocDueDate { get; set; }
        public virtual Int32 SlpCode { get; set; }


        public List<ItensModel> DocumentLines { get => documentLines; set => documentLines = value; }
        private List<ItensModel> documentLines = new List<ItensModel>();
    }
}
