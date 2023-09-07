using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Indo.Employees;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace Indo.Web.Pages.Employee
{
    public class UpdateModel : IndoPageModel
    {
        [BindProperty]
        public EmployeeUpdateViewModel Employee { get; set; }
        public List<SelectListItem> Departments { get; set; }
        public List<SelectListItem> Designation { get; set; }
        public List<SelectListItem> EmployeeType { get; set; }
        public List<SelectListItem> Role { get; set; }
        public List<SelectListItem> ActiveStatus { get; set; }

        private readonly IEmployeeAppService _employeeAppService;
        public UpdateModel(IEmployeeAppService employeeAppService)
        {
            _employeeAppService = employeeAppService;
        }
        public async Task OnGetAsync(Guid id)
        {
            var dto = await _employeeAppService.GetAsync(id);
            Employee = ObjectMapper.Map<EmployeeReadDto, EmployeeUpdateViewModel>(dto);


            var departmentLookup = await _employeeAppService.GetDepartmentLookupAsync();
            Departments = departmentLookup.Items
                .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
                .ToList();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                await _employeeAppService.UpdateAsync(
                    Employee.Id,
                    ObjectMapper.Map<EmployeeUpdateViewModel, EmployeeUpdateDto>(Employee)
                );
                return NoContent();

            }
            catch (EmployeeAlreadyExistsException ex)
            {
                throw new UserFriendlyException($"{ex.Code}");
            }
        }

        public class EmployeeUpdateViewModel
        {
            [HiddenInput]
            public Guid Id { get; set; }

            [SelectItems(nameof(Departments))]
            [DisplayName("Department")]
            public Guid DepartmentId { get; set; }

            [Required]
            [StringLength(EmployeeConsts.MaxNameLength)]
            public string Name { get; set; }

            [Required]
            [StringLength(EmployeeConsts.MaxNameLength)]
            [DisplayName("Employee ID #")]
            public string EmployeeNumber { get; set; }

            [SelectItems(nameof(Desigantion))]
            [DisplayName("Designation")]
            public EmployeeDesignations Desigantion { get; set; }

            [SelectItems(nameof(Role))]
            [DisplayName("Role")]
            public string Role { get; set; }

            [DisplayName("Employee Group")]
            public EmployeeGroup EmployeeGroup { get; set; }

            [DisplayName("Employee Type")]
            [SelectItems(nameof(EmployeeType))]
            public string EmployeeType { get; set; }

            [DisplayName("Active Status")]
            [SelectItems(nameof(ActiveStatus))]
            public EmployeeActiveStatus ActiveStatus { get; set; }

            [TextArea]
            public string Street { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string ZipCode { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
        }
    }
}
