using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using Volo.Abp;
using JetBrains.Annotations;

namespace Indo.TypeofCompanies
{
    public class TypeofCompanyManager : DomainService
    {
        private readonly ITypeofCompanyRepository _typeofCompanyRepository;

        public TypeofCompanyManager(ITypeofCompanyRepository typeofCompanyRepository)
        {
            _typeofCompanyRepository = typeofCompanyRepository;
        }
        public async Task<TypeofCompany> CreateAsync(
            [NotNull] string name)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));

            var existing = await _typeofCompanyRepository.FindAsync(x => x.Name.Equals(name));
            if (existing != null)
            {
                throw new TypeofCompanyAlreadyExistsException(name);
            }

            return new TypeofCompany(
                GuidGenerator.Create(),
                name
            );
        }
        public async Task ChangeNameAsync(
           [NotNull] TypeofCompany typeofCompany,
           [NotNull] string newName)
        {
            Check.NotNull(typeofCompany, nameof(typeofCompany));
            Check.NotNullOrWhiteSpace(newName, nameof(newName));

            var existing = await _typeofCompanyRepository.FindAsync(x => x.Name.Equals(newName));
            if (existing != null && existing.Id != typeofCompany.Id)
            {
                throw new TypeofCompanyAlreadyExistsException(newName);
            }

            typeofCompany.ChangeName(newName);
        }
    }
}

