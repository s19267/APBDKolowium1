using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface IDbService
    {
        public Prescription GetPrescription(int id);
        public Prescription CreatePrescription(Prescription prescription);
    }
}