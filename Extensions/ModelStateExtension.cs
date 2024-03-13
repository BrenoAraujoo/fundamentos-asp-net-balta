using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Blog.Extensions
{
    public static class ModelStateExtension
    {
        public static List<string> GetErros(this ModelStateDictionary modelState)
        {
            List<string> errosList = new();

            foreach (var item in modelState.Values)
                errosList.AddRange(item.Errors.Select(erro => erro.ErrorMessage));

            return errosList;
        }
    }
}
