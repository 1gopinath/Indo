using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Indo.Companies;
using Indo.Employees;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using static Indo.Web.Pages.Company.CreateModel;

namespace Indo.Web.Pages.Employee
{
    public class CreateModel : IndoPageModel
    {
        [BindProperty]
        public CreateEmployeeViewModel Employee { get; set; }
        public List<SelectListItem> Departments { get; set; }
        public List<SelectListItem> Designation { get; set; }
        public List<SelectListItem> EmployyeeType { get; set; }
        public List<SelectListItem> Role { get; set; }
        public List<SelectListItem> ActiveStatus { get; set; }

        private readonly IEmployeeAppService _employeeAppService;
        public CreateModel(IEmployeeAppService employeeAppService)
        {
            _employeeAppService = employeeAppService;
        }
        public async Task OnGetAsync(Guid id)
        {
            Employee = new CreateEmployeeViewModel();

            var dto = await _employeeAppService.GetAsync(id);
            Employee = ObjectMapper.Map<EmployeeReadDto, CreateEmployeeViewModel>(dto);

            var departmentLookup = await _employeeAppService.GetDepartmentLookupAsync();
            Departments = departmentLookup.Items
                .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
                .ToList();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                await _employeeAppService.CreateAsync(
                    ObjectMapper.Map<CreateEmployeeViewModel, EmployeeCreateDto>(Employee)
                    );
                return NoContent();

            }
            catch (EmployeeAlreadyExistsException ex)
            {
                throw new UserFriendlyException($"{ex.Code}");
            }
        }
        public class CreateEmployeeViewModel
        {
            [SelectItems(nameof(Departments))]
            [DisplayName("Department")]
            public Guid DepartmentId { get; set; }

            [Required]
            [StringLength(EmployeeConsts.MaxNameLength)]
            public string Name { get; set; }

            public string EmployeeNumber { get; set; }

            [SelectItems(nameof(Role))]
            public string Role { get; set; }

            [DisplayName("Designation")]
            public EmployeeDesignations Designation { get; set; }

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
