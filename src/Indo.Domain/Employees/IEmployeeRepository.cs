using System;
using Volo.Abp.Domain.Repositories;

namespace Indo.Employees
{
    public interface IEmployeeRepository : IRepository<Employee, Guid>
    {
    }
}
