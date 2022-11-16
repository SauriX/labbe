using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integration.WeeClinic.Dtos
{
    public class WeeServiceNodeDto
    {
        public WeeServiceNodeDto()
        {
        }

        public WeeServiceNodeDto(string idServicio, string idNodo)
        {
            IdServicio = idServicio;
            IdNodo = idNodo;
        }

        public string IdServicio { get; set; }
        public string IdNodo { get; set; }
    }
}
