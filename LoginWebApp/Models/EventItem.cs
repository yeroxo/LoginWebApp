using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginWebApp.ViewModels
{
    public class EventItem
    {
        public EventItem(int Id,string Name, DateTime Start,DateTime End, bool Status)
        {
            this.Id = Id;
            this.Name = Name;
            this.Start = Start;
            this.End = End;
            this.Status = Status;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public bool Status { get; set; }
    }
}
