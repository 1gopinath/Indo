using Indo.TypeofCompanies;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Indo.Items
{
    public class Items : FullAuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }
        public string ItemShortCode { get; set; }
        public int CostforCompany { get; set; }
        public int Quantity { get; set; }
        public string ComeUnderWhichProduct { get; set; }

        private Items() { }
        internal Items(
            Guid id,
            [NotNull] string name
            )
            : base(id)
        {
            SetName(name);
        }
        internal Items ChangeName([NotNull] string name)
        {
            SetName(name);
            return this;
        }
        private void SetName([NotNull] string name)
        {
            Name = Check.NotNullOrWhiteSpace(
                name,
                nameof(name),
                maxLength: TypeofCompanyConsts.MaxNameLength
                );
        }
    }
}
