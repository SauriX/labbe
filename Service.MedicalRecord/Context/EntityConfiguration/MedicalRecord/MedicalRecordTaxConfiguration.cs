using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.MedicalRecord.Domain.MedicalRecord;

namespace Service.MedicalRecord.Context.EntityConfiguration.MedicalRecord
{
    public class MedicalRecordTaxConfiguration : IEntityTypeConfiguration<Domain.MedicalRecord.MedicalRecordTaxData>
    {
        public void Configure(EntityTypeBuilder<MedicalRecordTaxData> builder)
        {
            builder.ToTable("Relacion_Expediente_Factura");
            builder.HasKey(x => new { x.FacturaID, x.ExpedienteID });
        }
    }
}
