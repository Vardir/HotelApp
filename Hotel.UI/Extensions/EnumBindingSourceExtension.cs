using System;
using System.Windows.Markup;

namespace Hotel.UI.Extensions
{
    /// <summary>
    /// Use for binding any <see cref="ItemsControl"/> to <see cref="Enum"/> type elements
    /// <para>Posted by Brian Lagunas</para>
    /// <link>http://brianlagunas.com/a-better-way-to-data-bind-enums-in-wpf/</link>
    /// </summary>
    public class EnumBindingSourceExtension : MarkupExtension
    {
        Type enumType;

        public Type EnumType
        {
            get => enumType;
            set
            {
                if (value != enumType)
                {
                    if (value != null)
                    {
                        Type enumType = Nullable.GetUnderlyingType(value) ?? value;
                        if (!enumType.IsEnum)
                            throw new ArgumentException("Type must be for an Enum.");
                    }
                    enumType = value;
                }
            }
        }

        public EnumBindingSourceExtension() { }
        public EnumBindingSourceExtension(Type enumType) => EnumType = enumType;        

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (null == enumType)
                throw new InvalidOperationException("The EnumType must be specified.");

            Type actualEnumType = Nullable.GetUnderlyingType(enumType) ?? enumType;
            Array enumValues = Enum.GetValues(actualEnumType);

            if (actualEnumType == enumType)
                return enumValues;

            Array tempArray = Array.CreateInstance(actualEnumType, enumValues.Length + 1);
            enumValues.CopyTo(tempArray, 1);
            return tempArray;
        }
    }
}