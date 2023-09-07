using System;
using System.Collections.Generic;
using System.Text;

namespace Indo.Items
{
    public class ItemReadDto
    {
        public string ItemName { get; set; }
        public string ItemShortCode { get; set; }
        public int CostforCompany { get; set; }
        public int Quantity { get; set; }
        public string ComeUnderWhichProduct { get; set; }
    }
}
