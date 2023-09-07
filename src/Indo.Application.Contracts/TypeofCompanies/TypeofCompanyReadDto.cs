using System;
using Volo.Abp.Application.Dtos;

namespace Indo.TypeofCompanies
{
    public class TypeofCompanyReadDto : AuditedEntityDto<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
