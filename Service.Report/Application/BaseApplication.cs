﻿using Service.Report.Application.IApplication;
using Service.Report.Domain.Catalogs;
using Service.Report.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Report.Application
{
    public class BaseApplication : IBaseApplication
    {
        private readonly IRepository<Branch> _branchRepository;
        private readonly IRepository<Medic> _medicRepository;
        private readonly IRepository<Company> _companyRepository;

        public BaseApplication(IRepository<Branch> branchRepository = null, IRepository<Medic> medicRepository = null, IRepository<Company> companyRepository = null)
        {
            _branchRepository = branchRepository;
            _medicRepository = medicRepository;
            _companyRepository = companyRepository;
        }

        public async Task<IEnumerable<string>> GetBranchNames(List<Guid> ids)
        {
            var branches = await _branchRepository.Get(x => ids.Contains(x.Id));

            return branches.Select(x => x.Sucursal);
        }

        public async Task<IEnumerable<string>> GetDoctorNames(List<Guid> ids)
        {
            var doctors = await _medicRepository.Get(x => ids.Contains(x.Id));

            return doctors.Select(x => x.NombreMedico);
        }

        public async Task<IEnumerable<string>> GetCompanyNames(List<Guid> ids)
        {
            var companies = await _companyRepository.Get(x => ids.Contains(x.Id));

            return companies.Select(x => x.NombreEmpresa);
        }
    }
}
