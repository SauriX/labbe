using Service.MedicalRecord.Domain.Status;
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
                    var status = new List<StatusRequestStudy>()
                    {
                        new StatusRequestStudy(1, "P", "Pendiente", "#f8e117"),
                        new StatusRequestStudy(2, "TM", "Toma de muestra", "#f8e117"),
                        new StatusRequestStudy(3, "S", "Solicitado", "#f8e117"),
                        new StatusRequestStudy(4, "C", "Capturado", "#f88d17"),
                        new StatusRequestStudy(5, "V", "Validado", "#f88d17"),
                        new StatusRequestStudy(6, "L", "Liberado", "#f88d17"),
                        new StatusRequestStudy(7, "E", "Enviado", "#28d417"),
                        new StatusRequestStudy(8, "ER", "En ruta", "#f8e117"),
                        new StatusRequestStudy(9, "CL", "Cancelado", "#e11616"),
                        new StatusRequestStudy(10, "EO", "Entregado", "#28d417"),
                        new StatusRequestStudy(11, "U", "Urgente", "#e11616")
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
                    var status = new List<StatusRequest>()
                    {
                        new StatusRequest(1, "V", "Vigente", "#345454"),
                        new StatusRequest(2, "CO", "Completado", "#345454"),
                        new StatusRequest(3, "CA", "Cancelado", "#345454")
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

            if (!context.Estatus_Solicitud_Pago.Any())
            {
                using var transaction = context.Database.BeginTransaction();
                try
                {
                    var status = new List<StatusRequestPayment>()
                    {
                        new StatusRequestPayment(1, "P", "Pagado"),
                        new StatusRequestPayment(2, "F", "Facturado"),
                        new StatusRequestPayment(3, "C", "Cancelado"),
                        new StatusRequestPayment(4, "FC", "Factura Cancelada")
                    };

                    context.Estatus_Solicitud_Pago.AddRange(status);

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
