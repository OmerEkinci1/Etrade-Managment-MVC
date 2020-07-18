using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace And.Etrade.Core.Model.Entity
{
    public class Basket: EntityBase
    {
        public int UserID { get; set; }
        public int ProductID { get; set; }
        public  Product Product { get; set; }
        // product tablosuyla ilişki kurmak için bu satırı yazdık..
        public int Quantity { get; set; }
    }
}
