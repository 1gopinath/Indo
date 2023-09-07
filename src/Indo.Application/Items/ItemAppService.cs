using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;

namespace Indo.Items
{
    public class ItemAppService : IndoAppService, IItemAppService
    {
        private readonly IItemRepository _ItemRepository;
        private readonly ItemManager _itemManager;
        public ItemAppService(
                IItemRepository ItemRepository,
                ItemManager itemManager
                )
        {
            _ItemRepository = ItemRepository;
            _itemManager = itemManager;
        }
        public async Task<ItemReadDto> GetAsync(Guid id)
        {
            var obj = await _ItemRepository.GetAsync(id);
            return ObjectMapper.Map<Items, ItemReadDto>(obj);
        }

        public async Task<List<ItemReadDto>> GetListAsync()
        {
            var queryable = await _ItemRepository.GetQueryableAsync();
            var query = from typeofCompany in queryable
                        select new { typeofCompany };
            var queryResult = await AsyncExecuter.ToListAsync(query);
            var dtos = queryResult.Select(x =>
            {
                var dto = ObjectMapper.Map<Items, ItemReadDto>(x.typeofCompany);
                return dto;
            }).ToList();
            return dtos;
        }
        public async Task<ItemReadDto> CreateAsync(ItemCreateDto input)
        {
            var obj = await _itemManager.CreateAsync(
                input.Name
            );

            obj.Quantity = input.Quantity;
            obj.ItemShortCode = input.ItemShortCode;
            obj.CostforCompany = input.CostforCompany;
            obj.ComeUnderWhichProduct = input.ComeUnderWhichProduct;

            await _ItemRepository.InsertAsync(obj);

            return ObjectMapper.Map<Items, ItemReadDto>(obj);
        }
        public async Task UpdateAsync(Guid id, ItemUpdateDto input)
        {
            var obj = await _ItemRepository.GetAsync(id);

            if (obj.Name != input.Name)
            {
                await _itemManager.ChangeNameAsync(obj, input.Name);
            }

            obj.Quantity = input.Quantity;
            obj.ItemShortCode = input.ItemShortCode;
            obj.CostforCompany = input.CostforCompany;
            obj.ComeUnderWhichProduct = input.ComeUnderWhichProduct;

            await _ItemRepository.UpdateAsync(obj);
        }
        public async Task DeleteAsync(Guid id)
        {
            if (_ItemRepository.Where(x => x.Id.Equals(id)).Any())
            {
                throw new UserFriendlyException("Unable to delete. Already have transaction.");
            }
            await _ItemRepository.DeleteAsync(id);
        }
    }
}
