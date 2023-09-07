using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Indo.ServiceOrderDetails;
using Indo.ServiceOrders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace Indo.Web.Pages.ServiceOrder
{
    public class UpdateDetailModel : IndoPageModel
    {
        [BindProperty]
        public ServiceOrderDetailUpdateViewModel ServiceOrderDetail { get; set; }
        public List<SelectListItem> ServiceOrders { get; set; }
        public List<SelectListItem> Services { get; set; }
        public ServiceOrderStatus Status { get; set; }

        private readonly IServiceOrderDetailAppService _serviceOrderDetailAppService;
        public UpdateDetailModel(IServiceOrderDetailAppService serviceOrderDetailAppService)
        {
            _serviceOrderDetailAppService = serviceOrderDetailAppService;
        }
        public async Task OnGetAsync(Guid id)
        {
            var dto = await _serviceOrderDetailAppService.GetAsync(id);
            Status = dto.Status;
            ServiceOrderDetail = ObjectMapper.Map<ServiceOrderDetailReadDto, ServiceOrderDetailUpdateViewModel>(dto);

            var serviceOrderLookup = await _serviceOrderDetailAppService.GetServiceOrderLookupAsync();
            ServiceOrders = serviceOrderLookup.Items
                .Where(x => x.Id.Equals(ServiceOrderDetail.ServiceOrderId))
                .Select(x => new SelectListItem(x.Number, x.Id.ToString()))
                .ToList();

            var serviceLookup = await _serviceOrderDetailAppService.GetServiceLookupAsync();
            Services = serviceLookup.Items
                .Select(x => new SelectListItem($"{x.Name} [Price: {x.Price.ToString("##,##.00")}]", x.Id.ToString()))
                .ToList();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                await _serviceOrderDetailAppService.UpdateAsync(
                    ServiceOrderDetail.Id,
                    ObjectMapper.Map<ServiceOrderDetailUpdateViewModel, ServiceOrderDetailUpdateDto>(ServiceOrderDetail)
                );
                return NoContent();

            }
            catch (Exception)
            {
                throw new UserFriendlyException($"Posting Error");
            }
        }

        public class ServiceOrderDetailUpdateViewModel
        {
            [HiddenInput]
            public Guid Id { get; set; }

            [Required]
            [SelectItems(nameof(ServiceOrders))]
            [DisplayName("Service Orders")]
            public Guid ServiceOrderId { get; set; }

            [Required]
            [SelectItems(nameof(Services))]
            [DisplayName("Services")]
            public Guid ServiceId { get; set; }

            [Required]
            public float Quantity { get; set; }

            [DisplayName("Discount Amount")]
            public float DiscAmt { get; set; }
        }
    }
}
