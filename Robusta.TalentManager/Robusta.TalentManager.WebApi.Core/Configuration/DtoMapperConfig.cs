using AutoMapper;
using Robusta.TalentManager.Domain;
using Robusta.TalentManager.WebApi.Dto;


namespace Robusta.TalentManager.WebApi.Core.Configuration
{
    public class DtoMapperConfig
    {
        public static void CreateMaps()
        {
            Mapper.Initialize(c =>
            {
                c.CreateMap<EmployeeDto, Employee>();
                c.CreateMap<Employee, EmployeeDto>();
            }
                );
        }
    }
}
