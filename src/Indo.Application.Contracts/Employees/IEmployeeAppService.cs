using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Indo.Employees
{
    public interface IEmployeeAppService : IApplicationService
    {
        Task<EmployeeReadDto> GetAsync(Guid id);

        Task<List<EmployeeReadDto>> GetListAsync();

        Task<List<EmployeeReadDto>> GetBuyerListAsync();

        Task<List<EmployeeReadDto>> GetSalesListAsync();

        Task<EmployeeReadDto> CreateAsync(EmployeeCreateDto input);

        Task UpdateAsync(Guid id, EmployeeUpdateDto input);

        Task DeleteAsync(Guid id);

        Task<ListResultDto<DepartmentLookupDto>> GetDepartmentLookupAsync();
    }
}
