using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayerTrainning.Models
{
    class ProductionOrderModel
    {
        public virtual Int32 AbsoluteEntry { get; set; }
        public virtual String ItemNo { get; set; }
        public virtual DateTime DueDate { get; set; }
    }
}
