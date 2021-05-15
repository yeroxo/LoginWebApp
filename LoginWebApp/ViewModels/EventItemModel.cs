using System;
using System.ComponentModel.DataAnnotations;

namespace LoginWebApp.ViewModels
{
    public class EventItemModel
    {
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime start { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime end { get; set; }
    }
}
