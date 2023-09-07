using Indo.TypeofCompanies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using Volo.Abp.Guids;
using Volo.Abp;
using JetBrains.Annotations;

namespace Indo.Items
{
    public class ItemManager : DomainService
    {
        private readonly IItemRepository _ItemRepository;

        public ItemManager(IItemRepository ItemRepository)
        {
            _ItemRepository = ItemRepository;
        }
        public async Task<Items> CreateAsync(
            [NotNull] string name)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));

            var existing = await _ItemRepository.FindAsync(x => x.Name.Equals(name));
            if (existing != null)
            {
                throw new TypeofCompanyAlreadyExistsException(name);
            }

            return new Items(
                GuidGenerator.Create(),
                name
            );
        }
        public async Task ChangeNameAsync(
           [NotNull] Items item,
           [NotNull] string newName)
        {
            Check.NotNull(item, nameof(item));
            Check.NotNullOrWhiteSpace(newName, nameof(newName));

            var existing = await _ItemRepository.FindAsync(x => x.Name.Equals(newName));
            if (existing != null && existing.Id != item.Id)
            {
                throw new TypeofCompanyAlreadyExistsException(newName);
            }

            item.ChangeName(newName);
        }
    }

}
