using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Indo.Companies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace Indo.Web.Pages.Company
{
    public class CreateModel : IndoPageModel
    {

        [BindProperty]
        public CompanyCreateViewModel Company { get; set; }
        public List<SelectListItem> Currencies { get; set; }
        public List<SelectListItem> Warehouses { get; set; }
        public List<SelectListItem> TypeofCompany { get; set; }

        private readonly ICompanyAppService _companyAppService;
        public CreateModel(ICompanyAppService companyAppService)
        {
            _companyAppService = companyAppService;
        }
        public async Task OnGetAsync(Guid id)
        {
            var dto = await _companyAppService.GetAsync(id);
            Company = ObjectMapper.Map<CompanyReadDto, CompanyCreateViewModel>(dto);

            var currencyLookup = await _companyAppService.GetCurrencyLookupAsync();
            Currencies = currencyLookup.Items
                .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
                .ToList();

            var warehouseLookup = await _companyAppService.GetWarehouseLookupAsync();
            Warehouses = warehouseLookup.Items
                .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
                .ToList();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                await _companyAppService.CreateAsync(
                    ObjectMapper.Map<CompanyCreateViewModel, CompanyCreateDto>(Company)
                    );
                return NoContent();
            }
            catch (CompanyAlreadyExistsException ex)
            {
                throw new UserFriendlyException($"{ex.Code}");
            }
        }
        public class CompanyCreateViewModel
        {

            [SelectItems(nameof(Currencies))]
            [DisplayName("Currency")]
            [Required]
            public Guid CurrencyId { get; set; }

            [SelectItems(nameof(Warehouses))]
            [DisplayName("Warehouses")]
            [Required]
            public Guid DefaultWarehouseId { get; set; }

            [SelectItems(nameof(TypeofCompanies))]
            [DisplayName("Type of Companies")]
            [Required]
            public Guid TypeofCompanyId { get; set; }

            [Required]
            [StringLength(CompanyConsts.MaxNameLength)]
            public string Name { get; set; }
            public string Phone { get; set; }
            public string ContactPerson { get; set; }
            public string TypeofCompany { get; set; }
            public string Email { get; set; }

            [TextArea]
            public string Street { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string ZipCode { get; set; }
        }
    }
}
