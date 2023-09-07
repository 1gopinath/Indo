﻿using System;
using System.ComponentModel.DataAnnotations;


namespace Indo.LeadRatings
{
    public class LeadRatingUpdateDto
    {

        [Required]
        [StringLength(LeadRatingConsts.MaxNameLength)]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
