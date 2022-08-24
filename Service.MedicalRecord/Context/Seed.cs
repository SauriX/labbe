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
                        new RequestStudyStatus(1, "P", "Pendiente", "#f8e117"),
                        new RequestStudyStatus(2, "TM", "Toma de muestra", "#f8e117"),
                        new RequestStudyStatus(3, "S", "Solicitado", "#f8e117"),
                        new RequestStudyStatus(4, "C", "Capturado", "#f88d17"),
                        new RequestStudyStatus(5, "V", "Validado", "#f88d17"),
                        new RequestStudyStatus(6, "L", "Liberado", "#f88d17"),
                        new RequestStudyStatus(7, "E", "Enviado", "#28d417"),
                        new RequestStudyStatus(8, "ER", "En ruta", "#f8e117"),
                        new RequestStudyStatus(9, "CL", "Cancelado", "#345454"),
                        new RequestStudyStatus(10, "EO", "Entregado", "#28d417"),
                        new RequestStudyStatus(11, "U", "Urgente", "#e11616")
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
