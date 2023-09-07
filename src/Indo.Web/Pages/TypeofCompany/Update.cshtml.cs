using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Indo.TypeofCompanies;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using Volo.Abp.ObjectMapping;

namespace Indo.Web.Pages.TypeofCompany
{
    public class UpdateModel : IndoPageModel
    {        

        [BindProperty]
        public TypeofCompanyUpdateViewModel TypeofCompany { get; set; }

        private readonly ITypeofCompanyAppService _typeofCompanyAppService;
        public UpdateModel(ITypeofCompanyAppService typeofCompanyAppService)
        {
            _typeofCompanyAppService = typeofCompanyAppService;
        }
        public async Task OnGetAsync(Guid id)
        {
            var dto = await _typeofCompanyAppService.GetAsync(id);
            TypeofCompany = ObjectMapper.Map<TypeofCompanyReadDto, TypeofCompanyUpdateViewModel>(dto);
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                await _typeofCompanyAppService.UpdateAsync(
                    TypeofCompany.Id,
                    ObjectMapper.Map<TypeofCompanyUpdateViewModel, TypeofCompanyUpdateDto>(TypeofCompany)
                    );
                return NoContent();
            }
            catch (TypeofCompanyAlreadyExistsException ex)
            {
                throw new UserFriendlyException($"{ex.Code}");
            }
        }
        public class TypeofCompanyUpdateViewModel
        {
            [HiddenInput]
            public Guid Id { get; set; }

            [Required]
            [StringLength(100)]
            public string Name { get; set; }

            [TextArea]
            public string Description { get; set; }
        }
    }
}
