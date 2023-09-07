using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Indo.Items
{
    public class ItemAlreadyExistsException : BusinessException
    {
        public ItemAlreadyExistsException(string name)
            : base("ItemAlreadyExistsException")
        {
            WithData("name", name);
        }
    }
}
