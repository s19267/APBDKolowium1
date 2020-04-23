using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public class Prescription
    {
        public int IdPrescription { get; set; }
        public String Date { get; set; }
        public String DueDate { get; set; }
        public int IdPatient { get; set; }
        public int IdDoctor { get; set; }
        public List<Medicament> Medicaments { get; set; }
    }
}