using System;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace RequestBinding
{
    public class TalentScoutModelBinderProvider : ModelBinderProvider
    {
        public override IModelBinder GetBinder(HttpConfiguration configuration, Type modelType)
        {
            return new TalentScoutModelBinder();
        }
    }
}