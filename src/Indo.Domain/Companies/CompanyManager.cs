using System;
using System.Linq;
using System.Threading.Tasks;
using Indo.Currencies;
using System.Xml.Linq;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Services;
using System.Linq.Dynamic.Core;

namespace Indo.Companies
{
    public class CompanyManager : DomainService
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyManager(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }
        public async Task<Company> CreateAsync(
            [NotNull] string name,
            [NotNull] Guid currencyId,
            [NotNull] Guid defaultWarehouseId
            )
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.NotNull<Guid>(currencyId, nameof(currencyId));
            Check.NotNull<Guid>(defaultWarehouseId, nameof(defaultWarehouseId));

            var existing = await _companyRepository.FindAsync(x => x.Name.Equals(name));
            if (existing != null)
            {
                throw new CompanyAlreadyExistsException(name);
            }

            return new Company(
                GuidGenerator.Create(),
                name,
                currencyId,
                defaultWarehouseId
            );
        }
        public async Task ChangeNameAsync(
           [NotNull] Company company,
           [NotNull] string newName)
        {
            Check.NotNull(company, nameof(company));
            Check.NotNullOrWhiteSpace(newName, nameof(newName));

            var existing = await _companyRepository.FindAsync(x => x.Name.Equals(newName));
            if (existing != null && existing.Id != company.Id)
            {
                throw new CompanyAlreadyExistsException(newName);
            }

            company.ChangeName(newName);
        }

        public async Task<Company> GetDefaultCompanyAsync()
        {
             
             var name = await _companyRepository.FindAsync(x => x.Name.Equals(x.Name));
            return name;
        }
    }
}
