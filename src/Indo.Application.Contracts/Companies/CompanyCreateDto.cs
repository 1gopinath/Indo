﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Indo.Companies
{
    public class CompanyCreateDto
    {

        [Required]
        [StringLength(CompanyConsts.MaxNameLength)]
        public string Name { get; set; }
        public string Phone { get; set; }
        public string ContactPerson { get; set; }
        public string Email { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string GSTNumber { get; set; }
        public Guid CurrencyId { get; set; }
        public Guid DefaultWarehouseId { get; set; }
        public string TypeofCompanyId { get; set; }
    }
}
