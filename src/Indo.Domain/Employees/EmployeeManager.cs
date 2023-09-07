using System.Threading.Tasks;
using Indo.Companies;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Indo.Employees
{
    public class EmployeeManager : DomainService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeManager(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public async Task<Employee> CreateAsync(
            [NotNull] string name,
            [NotNull] string employeeNumber
            )
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.NotNullOrWhiteSpace(employeeNumber, nameof(employeeNumber));

            var existing = await _employeeRepository.FindAsync(x => x.EmployeeNumber.Equals(employeeNumber));
            if (existing != null)
            {
                throw new EmployeeAlreadyExistsException(employeeNumber);
            }

            return new Employee(
                GuidGenerator.Create(),
                name,
                employeeNumber
            );
        }
        public async Task ChangeNameAsync(
           [NotNull] Employee employee,
           [NotNull] string newName
            )
        {
            await Task.Yield();

            Check.NotNull(employee, nameof(employee));
            Check.NotNullOrWhiteSpace(newName, nameof(newName));

            employee.ChangeName(newName);
        }
        public async Task ChangeEmployeeNumberAsync(
           [NotNull] Employee employee,
           [NotNull] string newEmployeeNumber
            )
        {
            Check.NotNull(employee, nameof(employee));
            Check.NotNullOrWhiteSpace(newEmployeeNumber, nameof(newEmployeeNumber));

            var existing = await _employeeRepository.FindAsync(x => x.EmployeeNumber.Equals(newEmployeeNumber));
            if (existing != null && existing.Id != employee.Id)
            {
                throw new EmployeeAlreadyExistsException(newEmployeeNumber);
            }

            employee.ChangeEmployeeNumber(newEmployeeNumber);
        }
        public async Task<Employee> GetDefaultEmployeeAsync()
        {

            var name = await _employeeRepository.FindAsync(x => x.Name.Equals(x.Name));
            return name;
        }
    }
}
