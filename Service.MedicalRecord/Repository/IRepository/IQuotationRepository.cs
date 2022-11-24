﻿using Service.MedicalRecord.Domain.Quotation;
using Service.MedicalRecord.Dtos.Quotation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Repository.IRepository
{
    public interface IQuotationRepository
    {
        Task<Quotation> FindAsync(Guid id);
        Task<List<Quotation>> GetByFilter(QuotationFilterDto filter);
        Task<List<Quotation>> GetActive();
        Task<Quotation> GetById(Guid id);
        Task<string> GetLastCode(Guid branchId, string date);
        Task<List<QuotationStudy>> GetStudyById(Guid quotationId, IEnumerable<int> studiesIds);
        Task<List<QuotationStudy>> GetStudiesByQuotation(Guid quotationId);
        Task<List<QuotationPack>> GetPacksByQuotation(Guid quotationId);
        Task Create(Quotation quotation);
        Task Update(Quotation quotation);
        Task BulkInsertUpdatePacks(Guid quotationId, List<QuotationPack> packs);
        Task BulkDeletePacks(Guid quotationId, List<QuotationPack> packs);
        Task BulkInsertUpdateStudies(Guid quotationId, List<QuotationStudy> studies);
        Task BulkDeleteStudies(Guid quotationId, List<QuotationStudy> studies);
    }
}