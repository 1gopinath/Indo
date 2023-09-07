﻿using System;
using Volo.Abp.Application.Dtos;

namespace Indo.Calendars
{
    public class CalendarReadDto : AuditedEntityDto<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsDone { get; set; }
        public string Location { get; set; }
    }
}
