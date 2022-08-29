﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dictionary
{
    public class Catalogs
    {
        public class Department
        {
            public const int PAQUETES = 1;
            public const int IMAGENOLOGIA = 2;
            public const int PATOLOGIA = 3;
            public const int LABORATORIO = 4;
        }

        public class Branch
        {
            public static readonly Guid MT = new("698e416e-e5cb-4e7a-90ed-74448d408f20");
            public static readonly Guid CMSS = new("20ced55b-49b2-4d11-872f-d2346e9c4790");
            public static readonly Guid U200 = new("9b87d7d2-0dbb-47b7-9c8d-0285c519deec");
            public static readonly Guid U300 = new("0120faeb-8343-4fef-b600-3b71038fce27");
            public static readonly Guid ALAMEDA = new("fb16b406-19b4-45cf-a0c5-38761a2f2718");
            public static readonly Guid HACIENDAS = new("cea654b4-d2f7-4c1e-8552-7f1a77cb5c23");
            public static readonly Guid REFORMA = new("5b4484d8-230f-47a3-995d-fec8081666d5");
            public static readonly Guid MORELOS = new("bc5a588a-2289-4739-920a-345ad141be5b");
            public static readonly Guid SOLIDARIDAD = new("de8c4cae-0d30-485a-89cc-f470e8c81f5d");
            public static readonly Guid MNORTE = new("0c03c54b-c598-4137-9c9c-3c3c7becde4a");
            public static readonly Guid CANTABRIA = new("c47d97b2-0738-4355-b542-be67f120f1f7");
            public static readonly Guid UNIDAD = new("f497da56-e0e9-4b41-a345-f79c3ac0aa65");
            public static readonly Guid NAVOJOA = new("c4b35cf3-f6c9-4f26-8085-8202b51c7fc5");
            public static readonly Guid NAVOJOA2 = new("22c17091-64d1-4ffb-9387-147ca43132d2");
            public static readonly Guid KENNEDY = new("c8abc53b-3c9f-45b3-9f19-de10bb885def");
            public static readonly Guid NOGALES1 = new("362a468e-3ff3-4bc0-8d97-c5aeecd496c6");
            public static readonly Guid CUMBRES = new("234f1cb4-4b26-4d8a-a5df-03063dd17a85");
            public static readonly Guid SPGG = new("a5a3f4bf-9330-429c-a0ea-f2be9f13ca09");
        }
    }
}
