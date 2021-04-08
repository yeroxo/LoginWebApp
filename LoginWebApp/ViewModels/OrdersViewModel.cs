using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginWebApp.ViewModels
{
    public class OrdersViewModel
    {
        public IEnumerable<OrdersModel> ListOrders { get; set; }
    }
}
