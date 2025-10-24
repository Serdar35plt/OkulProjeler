using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace telefon.Models
{
    public class Contacts
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Message { get; set; }
        public bool IsRead { get; set; } = false;
    }
}