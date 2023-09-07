using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Indo.TypeofCompanies
{
        public interface ITypeofCompanyAppService : IApplicationService
        {
            Task<TypeofCompanyReadDto> GetAsync(Guid id);

            Task<List<TypeofCompanyReadDto>> GetListAsync();

            Task<TypeofCompanyReadDto> CreateAsync(TypeofCompanyCreateDto input);

            Task UpdateAsync(Guid id, TypeofCompanyUpdateDto input);

            Task DeleteAsync(Guid id);
        }
    }
