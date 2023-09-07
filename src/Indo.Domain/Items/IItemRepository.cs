using Indo.TypeofCompanies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Indo.Items
{
    public interface IItemRepository : IRepository<Items, Guid>
    {
    }
}
