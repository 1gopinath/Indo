using System;
using System.ComponentModel.DataAnnotations;


namespace Indo.TypeofCompanies
{
    public class TypeofCompanyUpdateDto
    {

        [Required]
        [StringLength(TypeofCompanyConsts.MaxNameLength)]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
