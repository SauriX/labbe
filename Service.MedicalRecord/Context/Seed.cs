using Service.MedicalRecord.Domain.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Context
{
    public class Seed
    {
        public static async Task SeedData(ApplicationDbContext context)
        {
            if (!context.Estatus_Solicitud_Estudio.Any())
            {
                using var transaction = context.Database.BeginTransaction();
                try
                {
                    var status = new List<RequestStudyStatus>()
                    {
                        new RequestStudyStatus(1, "P", "Pendiente", "#345454"),
                        new RequestStudyStatus(2, "TM", "Toma de muestra", "#345454"),
                        new RequestStudyStatus(3, "S", "Solicitado", "#345454"),
                        new RequestStudyStatus(4, "C", "Capturado", "#345454"),
                        new RequestStudyStatus(5, "V", "Validado", "#345454"),
                        new RequestStudyStatus(6, "L", "Liberado", "#345454"),
                        new RequestStudyStatus(7, "E", "Enviado", "#345454"),
                        new RequestStudyStatus(8, "ER", "En ruta", "#345454"),
                        new RequestStudyStatus(9, "CL", "Cancelado", "#345454"),
                        new RequestStudyStatus(10, "EO", "Entregado", "#345454"),
                        new RequestStudyStatus(11, "U", "Urgente", "#345454")
                    };

                    context.Estatus_Solicitud_Estudio.AddRange(status);

                    await context.SaveChangesAsync();

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }

            if (!context.Estatus_Solicitud.Any())
            {
                using var transaction = context.Database.BeginTransaction();
                try
                {
                    var status = new List<RequestStatus>()
                    {
                        new RequestStatus(1, "V", "Vigente", "#345454"),
                        new RequestStatus(2, "CO", "Completado", "#345454"),
                        new RequestStatus(3, "CA", "Cancelado", "#345454")
                    };

                    context.Estatus_Solicitud.AddRange(status);

                    await context.SaveChangesAsync();

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
