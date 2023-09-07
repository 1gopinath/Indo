using Indo.LeadRatings;
using Indo.TypeofCompanies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using Volo.Abp;
using Indo.Items;
using System.ComponentModel;
using System.Collections.Generic;

namespace Indo.Web.Pages.Items
{
    public class CreateModel : IndoPageModel
    {

        [BindProperty]
        public CreateItemViewModel Item { get; set; }
        public List<SelectItems> ComeUnderWhichProduct { get; set; }

        private readonly IItemAppService _ItemAppService;
        public CreateModel(IItemAppService itemAppService)
        {
            _ItemAppService = itemAppService;
        }
        public void OnGet()
        {
            Item = new CreateItemViewModel();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                var dto = ObjectMapper.Map<CreateItemViewModel, ItemCreateDto>(Item);
                await _ItemAppService.CreateAsync(dto);
                return NoContent();
            }
            catch (ItemAlreadyExistsException ex)
            {
                throw new UserFriendlyException($"{ex.Code}");
            }
        }
        public class CreateItemViewModel
        {
            [Required]
            public string Name { get; set; }

            [Required]
            [DisplayName("Item Short Code")]
            public string ItemShortCode { get; set; }

            [Required]
            [DisplayName("Cost for Company")]
            public int CostforCompany { get; set; }

            [Required]
            [StringLength(ItemConsts.MinLength)]
            public int Quantity { get; set; }

            [Required]
            [SelectItems(nameof(ComeUnderWhichProduct))]
            [DisplayName("ComesUnder which Product")]
            public string ComeUnderWhichProduct { get; set; }
        }
    }
}