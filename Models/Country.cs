using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace country_API.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Capital { get; set; }
        public string Language { get; set; }
    }
}
