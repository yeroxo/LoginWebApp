using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LoginWebApp.ViewModels
{
    public class EventItemModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Не указано имя действия")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Не указано время начала")]
        [DataType(DataType.DateTime)]
        public DateTime Start { get; set; }

        [Required(ErrorMessage = "Не указано время завершения")]
        [DataType(DataType.DateTime)]
        public DateTime End { get; set; }

        public bool Status { get; set; }

        public IEnumerable<EventItem> ListEvents { get; set; }
    }
}
