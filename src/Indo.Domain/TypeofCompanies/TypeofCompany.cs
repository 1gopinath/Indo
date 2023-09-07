using System;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp;

namespace Indo.TypeofCompanies
{
    public class TypeofCompany : FullAuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }

        private TypeofCompany() { }
        internal TypeofCompany(
            Guid id,
            [NotNull] string name
            )
            : base(id)
        {
            SetName(name);
        }
        internal TypeofCompany ChangeName([NotNull] string name)
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
