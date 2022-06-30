using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayerTrainning.Models
{
    public class ItensModel
    {
        public virtual String ItemCode { get; set; }
        public virtual String Quantity { get; set; }
        public virtual String UnitPrice { get; set; }
    }
}
