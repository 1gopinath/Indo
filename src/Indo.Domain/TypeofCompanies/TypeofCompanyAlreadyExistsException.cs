using Volo.Abp;

namespace Indo.TypeofCompanies
{
    public class TypeofCompanyAlreadyExistsException : BusinessException
    {
        public TypeofCompanyAlreadyExistsException(string name)
            : base("TypeofCompanyAlreadyExists")
        {
            WithData("name", name);
        }
    }
}
