using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayerTrainning.Models
{
    class ErrorModel
    {
        public virtual Int32 code { get; set; }

        public virtual MessageModel message { get; set; }
    }
}
