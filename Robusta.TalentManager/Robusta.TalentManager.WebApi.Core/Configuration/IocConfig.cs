using System;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using Robusta.TalentManager.Domain;
using Robusta.TalentManager.Data;
using Robusta.TalentManager.WebApi.Dto;
using Robusta.TalentManager.WebApi.Core.Infrastructure;
using StructureMap;

namespace Robusta.TalentManager.WebApi.Core.Configuration
{
    public static class IocConfig
    {
        public static void RegisterDependencyResolver(HttpConfiguration config)
        {
            var container = new Container(x =>
            {
                x.Scan(scan =>
                {
                    scan.WithDefaultConventions();

                    AppDomain.CurrentDomain.GetAssemblies()
                        .Where(a => a.GetName().Name.StartsWith("Robusta.TalentManager"))
                            .ToList()
                                .ForEach(a => scan.Assembly(a));
                });


                x.For<IMapper>().Use(new Mapper(new MapperConfiguration(
                    c =>
                    {
                        c.CreateMap<EmployeeDto, Employee>();
                        c.CreateMap<Employee, EmployeeDto>();
                    })));
                x.For(typeof(IRepository<>)).Use(typeof(Repository<>));
            });

            config.DependencyResolver = new StructureMapContainer(container);
        }
    }
}
