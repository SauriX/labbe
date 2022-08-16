using Service.Catalog.Domain.Constant;

namespace Service.Catalog.Domain.Branch
{
    public class BranchFolioConfig
    {
        public BranchFolioConfig()
        {
        }

        public BranchFolioConfig(byte estadoId, short ciudadId, byte consecutivoEstado, byte consecutivoCiudad)
        {
            EstadoId = estadoId;
            CiudadId = ciudadId;
            ConsecutivoEstado = consecutivoEstado;
            ConsecutivoCiudad = consecutivoCiudad;
        }

        public int Id { get; set; }
        public byte EstadoId { get; set; }
        public virtual State Estado { get; set; }
        public short CiudadId { get; set; }
        public virtual City Ciudad { get; set; }
        public byte ConsecutivoEstado { get; set; }
        public byte ConsecutivoCiudad { get; set; }
    }
}
