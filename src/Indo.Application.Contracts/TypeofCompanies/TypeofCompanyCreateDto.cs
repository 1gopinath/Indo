using System;
using System.ComponentModel.DataAnnotations;

namespace Indo.TypeofCompanies
{
    public class TypeofCompanyCreateDto
    {

        [Required]
        [StringLength(TypeofCompanyConsts.MaxNameLength)]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
