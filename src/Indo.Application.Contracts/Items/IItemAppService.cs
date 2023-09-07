using System.Threading.Tasks;
using System;
using Volo.Abp.Application.Services;
using System.Collections.Generic;

namespace Indo.Items
{
    public interface IItemAppService : IApplicationService
    {
        Task<ItemReadDto> GetAsync(Guid id);
        Task<List<ItemReadDto>> GetListAsync();
        Task<ItemReadDto> CreateAsync(ItemCreateDto input);
        Task UpdateAsync(Guid id, ItemUpdateDto input);
        Task DeleteAsync(Guid id);
    }
}
