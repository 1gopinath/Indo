using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Indo.Employees
{
    public class EmployeeUpdateDto
    {

        [Required]
        [StringLength(EmployeeConsts.MaxNameLength)]
        public string Name { get; set; }
        public EmployeeDesignations Designation { get; set; }
        public string Role { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string EmployeeType { get; set; }
        public Guid DepartmentId { get; set; }

        [Required]
        public string EmployeeNumber { get; set; }
        public EmployeeGroup EmployeeGroup { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public EmployeeActiveStatus ActiveStatus { get; set; }
    }
}
