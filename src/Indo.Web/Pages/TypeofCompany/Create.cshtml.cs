using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Indo.LeadRatings;
using Indo.TypeofCompanies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace Indo.Web.Pages.TypeofCompany
{
    public class CreateModel : IndoPageModel
    {
        [BindProperty]
        public CreateTypeofCompanyViewModel TypeofCompany { get; set; }

        private readonly ITypeofCompanyAppService _typeofCompanyAppService;
        public CreateModel(ITypeofCompanyAppService typeofCompanyAppService)
        {
            _typeofCompanyAppService = typeofCompanyAppService;
        }
        public void OnGet()
        {
            TypeofCompany = new CreateTypeofCompanyViewModel();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                var dto = ObjectMapper.Map<CreateTypeofCompanyViewModel, TypeofCompanyCreateDto>(TypeofCompany);
                await _typeofCompanyAppService.CreateAsync(dto);
                return NoContent();
            }
            catch (LeadRatingAlreadyExistsException ex)
            {
                throw new UserFriendlyException($"{ex.Code}");
            }
        }
        public class CreateTypeofCompanyViewModel
        {
            [Required]
            [StringLength(TypeofCompanyConsts.MaxNameLength)]
            public string Name { get; set; }

            [TextArea]
            public string Description { get; set; }
        }
    }
}
