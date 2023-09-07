using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Indo.Departments;
using Indo.ProjectOrders;
using Indo.PurchaseOrders;
using Indo.SalesOrders;
using Indo.ServiceOrders;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using static Volo.Abp.Identity.IdentityPermissions;

namespace Indo.Employees
{
    public class EmployeeAppService : IndoAppService, IEmployeeAppService
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly EmployeeManager _employeeManager;
        private readonly IProjectOrderRepository _projectOrderRepository;
        private readonly IServiceOrderRepository _serviceOrderRepository;
        private readonly ISalesOrderRepository _salesOrderRepository;
        private readonly IPurchaseOrderRepository _purchaseOrderRepository;
        public EmployeeAppService(
            IEmployeeRepository employeeRepository,
            EmployeeManager employeeManager,
            IDepartmentRepository departmentRepository,
            IProjectOrderRepository projectOrderRepository,
            IServiceOrderRepository serviceOrderRepository,
            ISalesOrderRepository salesOrderRepository,
            IPurchaseOrderRepository purchaseOrderRepository
            )
        {
            _employeeRepository = employeeRepository;
            _employeeManager = employeeManager;
            _departmentRepository = departmentRepository;
            _projectOrderRepository = projectOrderRepository;
            _serviceOrderRepository = serviceOrderRepository;
            _salesOrderRepository = salesOrderRepository;
            _purchaseOrderRepository = purchaseOrderRepository;
        }
        protected IIdentityRoleRepository RoleRepository { get; }
        public async Task<EmployeeReadDto> GetAsync(Guid id)
        {
            var queryable = await _employeeRepository.GetQueryableAsync();
            var query = from employee in queryable
                        join department in _departmentRepository on employee.DepartmentId equals department.Id
                        where employee.Id == id
                        select new { employee, department };
            var queryResult = await AsyncExecuter.FirstOrDefaultAsync(query);
            if (queryResult == null)
            {
                throw new EntityNotFoundException(typeof(Employee), id);
            }
            var dto = ObjectMapper.Map<Employee, EmployeeReadDto>(queryResult.employee);
            dto.DepartmentName = queryResult.department.Name;
            return dto;
        }

        public async Task<List<EmployeeReadDto>> GetListAsync()
        {
            var queryable = await _employeeRepository.GetQueryableAsync();
            var query = from employee in queryable
                        join department in _departmentRepository on employee.DepartmentId equals department.Id
                        select new { employee, department };          
            var queryResult = await AsyncExecuter.ToListAsync(query);
            var dtos = queryResult.Select(x =>
            {
                var dto = ObjectMapper.Map<Employee, EmployeeReadDto>(x.employee);
                dto.DepartmentName = x.department.Name;
                return dto;
            }).ToList();

            return dtos;
        }
        public async Task<List<EmployeeReadDto>> GetBuyerListAsync()
        {
            var queryable = await _employeeRepository.GetQueryableAsync();
            var query = from employee in queryable
                        join department in _departmentRepository on employee.DepartmentId equals department.Id
                        where employee.EmployeeGroup == EmployeeGroup.Buyer
                        select new { employee, department };
            var queryResult = await AsyncExecuter.ToListAsync(query);
            var dtos = queryResult.Select(x =>
            {
                var dto = ObjectMapper.Map<Employee, EmployeeReadDto>(x.employee);
                dto.DepartmentName = x.department.Name;
                return dto;
            }).ToList();

            return dtos;
        }
        public async Task<List<EmployeeReadDto>> GetSalesListAsync()
        {
            var queryable = await _employeeRepository.GetQueryableAsync();
            var query = from employee in queryable
                        join department in _departmentRepository on employee.DepartmentId equals department.Id
                        where employee.EmployeeGroup == EmployeeGroup.Sales
                        select new { employee, department };
            var queryResult = await AsyncExecuter.ToListAsync(query);
            var dtos = queryResult.Select(x =>
            {
                var dto = ObjectMapper.Map<Employee, EmployeeReadDto>(x.employee);
                dto.DepartmentName = x.department.Name;
                return dto;
            }).ToList();

            return dtos;
        }
        public async Task<ListResultDto<DepartmentLookupDto>> GetDepartmentLookupAsync()
        {
            var list = await _departmentRepository.GetListAsync();
            return new ListResultDto<DepartmentLookupDto>(
                ObjectMapper.Map<List<Department>, List<DepartmentLookupDto>>(list)
            );
        }
        public virtual async Task<ListResultDto<IdentityRoleDto>> GetRoleListAsync()
        {
            var list = await RoleRepository.GetListAsync();
            return new ListResultDto<IdentityRoleDto>(
                ObjectMapper.Map<List<IdentityRole>, List<IdentityRoleDto>>(list)
            );
        }

        public async Task<EmployeeReadDto> CreateAsync(EmployeeCreateDto input)
        {
            var obj = await _employeeManager.CreateAsync(
                input.Name,
                input.EmployeeNumber
            );

            obj.Role = input.Role;
            obj.Street = input.Street;
            obj.City = input.City;
            obj.State = input.State;
            obj.ZipCode = input.ZipCode;
            obj.EmployeeType = input.EmployeeType;
            obj.Designation = input.Designation;
            obj.DepartmentId = input.DepartmentId;
            obj.EmployeeGroup = input.EmployeeGroup;
            obj.Phone = input.Phone;
            obj.Email = input.Email;
            obj.ActiveStatus = input.ActiveStatus;

            await _employeeRepository.InsertAsync(obj);

            return ObjectMapper.Map<Employee, EmployeeReadDto>(obj);
        }
        public async Task UpdateAsync(Guid id, EmployeeUpdateDto input)
        {
            var obj = await _employeeRepository.GetAsync(id);

            if (obj.EmployeeNumber != input.EmployeeNumber)
            {
                await _employeeManager.ChangeEmployeeNumberAsync(obj, input.EmployeeNumber);
            }
            obj.Role = input.Role;
            obj.Street = input.Street;
            obj.City = input.City;
            obj.State = input.State;
            obj.ZipCode = input.ZipCode;
            obj.EmployeeType = input.EmployeeType;
            obj.Designation = input.Designation;
            obj.DepartmentId = input.DepartmentId;
            obj.EmployeeGroup = input.EmployeeGroup;
            obj.Phone = input.Phone;
            obj.Email = input.Email;
            obj.ActiveStatus = input.ActiveStatus;

            await _employeeRepository.UpdateAsync(obj);
        }
        public async Task DeleteAsync(Guid id)
        {
            if (_projectOrderRepository.Where(x => x.SalesExecutiveId.Equals(id)).Any())
            {
                throw new UserFriendlyException("Unable to delete. Already have transaction.");
            }
            if (_serviceOrderRepository.Where(x => x.SalesExecutiveId.Equals(id)).Any())
            {
                throw new UserFriendlyException("Unable to delete. Already have transaction.");
            }
            if (_salesOrderRepository.Where(x => x.SalesExecutiveId.Equals(id)).Any())
            {
                throw new UserFriendlyException("Unable to delete. Already have transaction.");
            }
            if (_purchaseOrderRepository.Where(x => x.BuyerId.Equals(id)).Any())
            {
                throw new UserFriendlyException("Unable to delete. Already have transaction.");
            }
            await _employeeRepository.DeleteAsync(id);
        }
    }
}
