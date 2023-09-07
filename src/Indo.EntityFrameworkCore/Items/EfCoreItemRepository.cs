using Indo.EntityFrameworkCore;
using Indo.ImportantDates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Indo.Items
{
    public class EfCoreItemRepository
   : EfCoreRepository<IndoDbContext, Items, Guid>,
            IItemRepository
    {
        public EfCoreItemRepository(
            IDbContextProvider<IndoDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }
}
