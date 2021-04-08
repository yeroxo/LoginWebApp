using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginWebApp.ViewModels
{
    public class OrdersModel
    {
        public OrdersModel(string Id, DateTime now, int Amount, int Sum)
        {
            this.Id = Id;
            this.Data = now;
            this.Amount = Amount;
            this.Sum = Sum;
        }

        public string Id { get; set; }
        public DateTime Data { get; set; }

        public int Amount { get; set; }
        public int Sum { get; set; }
    }
}
