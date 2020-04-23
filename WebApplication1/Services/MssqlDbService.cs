using System.Collections.Generic;
using System.Data.SqlClient;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class MssqlDbService : IDbService
    {
        public Prescription GetPrescription(int id)
        {
            Prescription prescription = new Prescription();
            using (var con = new SqlConnection("Data Source=db-mssql;Initial Catalog=s19267;Integrated Security=True"))
            using (var com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "select * from Prescription where IdPrescription=@IdPrescription";
                com.Parameters.AddWithValue("IdPrescription", id);

                con.Open();
                var dr = com.ExecuteReader();
                if (!dr.Read())
                {
                    return null;
                }

                prescription.IdPrescription = (int) dr["IdPrescription"];
                prescription.Date = dr["Date"].ToString();
                prescription.DueDate = dr["DueDate"].ToString();
                prescription.IdPatient = (int) dr["IdPatient"];
                prescription.IdDoctor = (int) dr["IdDoctor"];

                dr.Close();
                com.CommandText =
                    "select * from Medicament inner join Prescription_Medicament on Medicament.IdMedicament=Prescription_Medicament.IdMedicament where Prescription_Medicament.IdPrescription=@IdPrescription";
                dr = com.ExecuteReader();
                prescription.Medicaments = new List<Medicament>();
                while (dr.Read())
                {
                    Medicament medicament = new Medicament
                    {
                        IdMedicament = (int) dr["IdMedicament"],
                        Name = dr["Name"].ToString(),
                        Description = dr["Description"].ToString(),
                        Type = dr["Type"].ToString(),
                        Details = dr["Details"].ToString(),
                        Dose = (int) dr["Dose"]
                    };
                    prescription.Medicaments.Add(medicament);
                }

                return prescription;
            }

            // return new Prescription();
        }

        public Prescription CreatePrescription(Prescription prescription)
        {
            Prescription newprescription = new Prescription();
            using (var con = new SqlConnection("Data Source=db-mssql;Initial Catalog=s19267;Integrated Security=True"))
            using (var com = new SqlCommand())
            {
                com.Connection = con;
                con.Open();
                var tran = con.BeginTransaction();
                com.Transaction = tran;
                com.CommandText = "select max(IdPrescription) from Prescription";

                
                var dr = com.ExecuteReader();
                if (!dr.Read())
                {
                    tran.Rollback();
                    return null;
                }

                int id = (int) dr["IdPrescription"];
                id++;
                dr.Close();


                com.CommandText =
                    "insert into Prescription(IdPrescription,Date,DueDate,IdPatient,Iddoctor) values(@IdPrescription,@Date,@DueDate,@IdPatient,@IdDoctor)";
                com.Parameters.AddWithValue("@IdPrescription", id);
                com.Parameters.AddWithValue("@Date", prescription.Date);
                com.Parameters.AddWithValue("@DueDate", prescription.DueDate);
                com.Parameters.AddWithValue("@IdPatient", prescription.IdPatient);
                com.Parameters.AddWithValue("@IdDoctor", prescription.IdDoctor);

                com.ExecuteNonQuery();

                newprescription = GetPrescription(id);

                tran.Commit();
                return newprescription;

            }
        }
    }
}