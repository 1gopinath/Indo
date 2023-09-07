using Indo.Companies;
using Indo.Warehouses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;

namespace Indo.TypeofCompanies
{
    public class TypeofCompanyAppService: IndoAppService, ITypeofCompanyAppService
    {
            private readonly ITypeofCompanyRepository _typeofCompanyRepository;
            private readonly TypeofCompanyManager _typeofCompanyManager;
            private readonly IWarehouseRepository _warehouseRepository;
            private readonly WarehouseManager _warehouseManager;
            private readonly CompanyManager _companyManager;
        public TypeofCompanyAppService(
                ITypeofCompanyRepository typeofCompanyRepository,
                TypeofCompanyManager typeofCompanyManager,
                IWarehouseRepository warehouseRepository,
                WarehouseManager warehouseManagery,
                CompanyManager companyManager
                )
            {
                _typeofCompanyRepository = typeofCompanyRepository;
                _typeofCompanyManager = typeofCompanyManager;
                _warehouseRepository = warehouseRepository;
                _warehouseManager = warehouseManagery;
                _companyManager = companyManager;
            }
            public async Task<TypeofCompanyReadDto> GetAsync(Guid id)
            {
                var obj = await _typeofCompanyRepository.GetAsync(id);
                return ObjectMapper.Map<TypeofCompany, TypeofCompanyReadDto>(obj);
            }

        public async Task<List<TypeofCompanyReadDto>> GetListAsync()
            {
                var queryable = await _typeofCompanyRepository.GetQueryableAsync();
                var query = from typeofCompany in queryable
                            select new { typeofCompany };
                var queryResult = await AsyncExecuter.ToListAsync(query);
                var dtos = queryResult.Select(x =>
                {
                    var dto = ObjectMapper.Map<TypeofCompany, TypeofCompanyReadDto>(x.typeofCompany);
                    return dto;
                }).ToList();
                return dtos;
            }
            public async Task<TypeofCompanyReadDto> CreateAsync(TypeofCompanyCreateDto input)
            {
                var obj = await _typeofCompanyManager.CreateAsync(
                    input.Name
                );

                obj.Description = input.Description;

                await _typeofCompanyRepository.InsertAsync(obj);

                return ObjectMapper.Map<TypeofCompany, TypeofCompanyReadDto>(obj);
            }
            public async Task UpdateAsync(Guid id, TypeofCompanyUpdateDto input)
            {
                var obj = await _typeofCompanyRepository.GetAsync(id);

                if (obj.Name != input.Name)
                {
                    await _typeofCompanyManager.ChangeNameAsync(obj, input.Name);
                }

                obj.Description = input.Description;

                await _typeofCompanyRepository.UpdateAsync(obj);
            }
            public async Task DeleteAsync(Guid id)
            {
                if (_typeofCompanyRepository.Where(x => x.Id.Equals(id)).Any())
                {
                    throw new UserFriendlyException("Unable to delete. Already have transaction.");
                }
                await _typeofCompanyRepository.DeleteAsync(id);
            }
        }
    }
