using Microsoft.AspNetCore.Mvc.Rendering;

namespace CarvedRock.Admin;

public static class LookupModelExtensions
{
    public static SelectListItem ToItem(this LookupModel model)
    {
        return new SelectListItem(model.Name, model.Value);
    }
    public static List<SelectListItem> ToItem(this List<LookupModel> model)
    {
        var list = new List<SelectListItem>();
        list.AddRange(model.Select(s => new SelectListItem(s.Name, s.Value)));
        return list;
    }

}
