using System.Reflection;

namespace ACRP_API.Utils
{
    public static class GenericMapper
    {
        public static TDto MapToDto<TModel, TDto>(TModel model)
            where TDto : new()
            where TModel : notnull
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model), "El modelo no puede ser null");
            }

            TDto dto = new TDto();
            PropertyInfo[] modelProperties = typeof(TModel).GetProperties();
            PropertyInfo[] dtoProperties = typeof(TDto).GetProperties();

            foreach (PropertyInfo dtoProperty in dtoProperties)
            {
                PropertyInfo? modelProperty = Array.Find(
                    modelProperties, 
                    p => p.Name == dtoProperty.Name && 
                         p.PropertyType == dtoProperty.PropertyType
                );

                if (modelProperty != null)
                {
                    object? value = modelProperty.GetValue(model);
                    dtoProperty.SetValue(dto, value);
                }
            }

            return dto;
        }
    }
}