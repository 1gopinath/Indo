using Indo.EntityFrameworkCore;
using Indo.LeadRatings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Indo.TypeofCompanies
{
    public class EfCoreTypeofCompanyRepository : EfCoreRepository<IndoDbContext, TypeofCompany, Guid>,
            ITypeofCompanyRepository
    {
        public EfCoreTypeofCompanyRepository(
            IDbContextProvider<IndoDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }
}
