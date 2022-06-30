using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UItoServiceLayer.Models
{
    public class ResponseModel
    {
        #region Properties

        public virtual Int32 Status { get; set; }
        public virtual Int32 NumErro { get; set; }
        public virtual String Msg { get; set; }
        public virtual String data { get; set; }

        #endregion
    }
}
