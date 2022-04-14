﻿using Service.Catalog.Domain;
using Service.Catalog.Dtos.Study;
using System.Collections.Generic;
using System.Linq;

namespace Service.Catalog.Mapper
{
    public static class StudyMapper
    {
        public static IEnumerable<StudyListDto> ToStudyListDto(this List<Study> model)
        {
            if (model == null) return null;

            return model.Select(x => new StudyListDto
            {
                Id = x.Id,
                Nombre = x.Nombre,
                AreaId = x.AreaId,
                Area = x.Area,
            });
        }
    }
}
