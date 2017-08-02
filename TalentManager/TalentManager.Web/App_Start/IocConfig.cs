using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using AutoMapper;
using StructureMap;
using TalentManager.Web.Models;
using TalentManager.Domain;
using TalentManager.Data;

namespace TalentManager.Web.App_Start
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
                        .Where(a => a.GetName().Name.StartsWith("TalentManager"))
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