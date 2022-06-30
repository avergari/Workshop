using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayerTrainning.Models
{
    class ResponseModel
    {
        public virtual int Status { get; set; }
        public virtual int NumErro { get; set; }
        public virtual String Msg { get; set; }
        public virtual String data { get; set; }
    }
}
