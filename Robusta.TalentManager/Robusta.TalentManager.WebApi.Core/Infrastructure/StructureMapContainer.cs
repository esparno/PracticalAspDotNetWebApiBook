using System.Web.Http.Dependencies;
using StructureMap;

namespace Robusta.TalentManager.WebApi.Core.Infrastructure
{
    public class StructureMapContainer : StructureMapDependencyScope, IDependencyResolver
    {
        private readonly IContainer container = null;

        public StructureMapContainer(IContainer container)
            : base(container)
        {
            this.container = container;
        }

        public IDependencyScope BeginScope()
        {
            return new StructureMapDependencyScope(container.GetNestedContainer());
        }
    }
}
