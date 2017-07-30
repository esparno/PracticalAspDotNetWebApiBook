using System;
using System.Linq;
using System.Web.Http.ModelBinding;
using System.Web.Http.Controllers;
using RequestBinding.Models;

namespace RequestBinding
{
    public class TalentScoutModelBinder : IModelBinder
    {
        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            var scoutCriteria = (TalentScout)bindingContext.Model ?? new TalentScout();

            var result = bindingContext.ValueProvider.GetValue("dept");
            if(result != null)
            {
                scoutCriteria.Departments = result.AttemptedValue.Split(',').Select(d => d.Trim()).ToList();
            }
            result = bindingContext.ValueProvider.GetValue("xctcbased");
            if(result != null)
            {
                int basedOn;
                if(Int32.TryParse(result.AttemptedValue, out basedOn))
                {
                    scoutCriteria.IsCtcBased = (basedOn > 0);
                }
            }
            result = bindingContext.ValueProvider.GetValue("doj");
            if (result != null)
            {
                DateTime doj;
                if (DateTime.TryParse(result.AttemptedValue, out doj))
                {
                    scoutCriteria.Doj = doj;
                }
            }
            bindingContext.Model = scoutCriteria;
            return true;
        }
    }
}