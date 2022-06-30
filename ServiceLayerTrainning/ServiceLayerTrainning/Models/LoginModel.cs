using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayerTrainning.Models
{
    class LoginModel
    {
        public virtual String CompanyDB { get; set; }
        public virtual String UserName { get; set; }
        public virtual String Password { get; set; }
    }
}
