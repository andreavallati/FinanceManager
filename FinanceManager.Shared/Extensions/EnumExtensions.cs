using System.ComponentModel;
using System.Reflection;

namespace FinanceManager.Shared.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum enumObj)
        {
            var fieldInfo = enumObj?.GetType().GetField(enumObj.ToString());
            var attribArray = fieldInfo?.GetCustomAttributes(false);

            if (attribArray?.Length == 0)
            {
                return enumObj?.ToString() ?? string.Empty;
            }
            else
            {
                DescriptionAttribute? attrib = null;

                if (attribArray != null)
                {
                    foreach (var att in attribArray)
                    {
                        if (att is DescriptionAttribute attribute)
                        {
                            attrib = attribute;
                        }
                    }
                }

                return attrib is not null ? attrib.Description : enumObj?.ToString() ?? string.Empty;
            }
        }

        public static string GetEnumStringFromDescription<T>(string description) where T : Enum
        {
            var enumValues = Enum.GetValues(typeof(T)).Cast<T>();

            foreach (var enumValue in enumValues)
            {
                var enumField = typeof(T).GetField(enumValue.ToString());
                var descriptionAttribute = enumField?.GetCustomAttribute<DescriptionAttribute>();

                if (descriptionAttribute != null && descriptionAttribute.Description == description)
                {
                    return enumValue.ToString();
                }
            }

            return string.Empty;
        }

        public static T GetValueFromDescription<T>(string description) where T : Enum
        {
            foreach (var field in typeof(T).GetFields())
            {
                if (Attribute.GetCustomAttribute(field,
                typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
                {
                    if (attribute.Description == description)
                    {
                        return (T)field.GetValue(null);
                    }
                }
                else
                {
                    if (field.Name == description)
                    {
                        return (T)field.GetValue(null);
                    }
                }
            }

            throw new ArgumentException("Not found.", nameof(description));
        }
    }
}
