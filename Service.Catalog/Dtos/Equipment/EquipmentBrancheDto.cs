﻿using System;

namespace Service.Catalog.Dtos.Equipment
{
    public class EquipmentBrancheDto
    {
        public Guid BranchId { get; set; }
        public int EquipmentId { get; set; }
        public int Num_serie { get; set; }
    }
}
