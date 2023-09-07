using Indo.TypeofCompanies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using Volo.Abp;
using Indo.Items;
using System.ComponentModel;
using System.Collections.Generic;

namespace Indo.Web.Pages.Items
{
    public class UpdateModel : IndoPageModel
    {
        [BindProperty]
        public ItemUpdateViewModel Item { get; set; }
        public List<SelectItems> ComeUnderWhichProduct { get; set; }

        private readonly IItemAppService _ItemAppService;
        public UpdateModel(IItemAppService itemAppService)
        {
            _ItemAppService = itemAppService;
        }
        public async Task OnGetAsync(Guid id)
        {
            var dto = await _ItemAppService.GetAsync(id);
            Item = ObjectMapper.Map<ItemReadDto, ItemUpdateViewModel>(dto);
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                await _ItemAppService.UpdateAsync(
                    Item.Id,
                    ObjectMapper.Map<ItemUpdateViewModel, ItemUpdateDto>(Item)
                    );
                return NoContent();
            }
            catch (ItemAlreadyExistsException ex)
            {
                throw new UserFriendlyException($"{ex.Code}");
            }
        }
        public class ItemUpdateViewModel
        {
            [HiddenInput]
            public Guid Id { get; set; }

            [Required]
            [StringLength(100)]
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
