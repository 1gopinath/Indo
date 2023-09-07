﻿using System;
using Volo.Abp.Application.Dtos;

namespace Indo.Tasks
{
    public class TaskReadDto : AuditedEntityDto<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsDone { get; set; }
        public string Location { get; set; }
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }
        public Guid ActivityId { get; set; }
        public string ActivityName { get; set; }
    }
}
