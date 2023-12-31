﻿using Service.Catalog.Domain.Branch;
using Service.Catalog.Domain.Price;
using Service.Catalog.Domain.Route;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Repository.IRepository
{
    public interface IPriceListRepository
    {
        Task<List<PriceList>> GetAll(string search);
        Task<PriceList> GetById(Guid Id);
        Task<List<PriceList_Study>> GetStudiesById(Guid Id);
        Task<PriceList_Study> GetPriceStudyById(int studyId, Guid branchId, Guid companyId);
        Task<List<PriceList_Study>> GetPriceStudyById(Guid priceListId, IEnumerable<int> studyId);
        Task<PriceList_Packet> GetPricePackById(int packId, Guid branchId, Guid companyId);
        Task<List<PriceList>> GetActive();
        Task<List<PriceList>> GetOptions();
        Task<List<Branch>> GetBranchesByPriceListId(Guid id);
        Task<PriceList> GetStudiesAndPacks(Guid priceListId);
        Task<bool> IsDuplicate(PriceList price);
        Task Create(PriceList price);
        Task Update(PriceList price);
        Task<List<Price_Branch>> GetAllBranch(Guid branchId);
        Task<List<Price_Company>> GetAllCompany(Guid companyId);
        Task<List<Price_Medics>> GetAllMedics(Guid medicsId);
        Task<Route_Study> GetStudyRoute(int id);
        Task<List<Route_Study>> GetStudyRoute(IEnumerable<int> id);
        Task<bool> DuplicateSMC(PriceList price);
    }
}
