using System;

namespace WebApplication1.Models
{
    public class Medicament
    {
        public int IdMedicament { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public String Type { get; set; }
        public int  Dose { get; set; }
        public String Details { get; set; }
    }
}