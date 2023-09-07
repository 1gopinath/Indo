﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Indo.ImportantDates
{
    public class ImportantDateCreateDto
    {

        [Required]
        [StringLength(ImportantDateConsts.MaxNameLength)]
        public string Name { get; set; }
        public string Description { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }
        public bool IsDone { get; set; }
        public string Location { get; set; }

        [Required]
        public Guid CustomerId { get; set; }
    }
}
