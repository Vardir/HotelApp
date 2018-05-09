using System;
using System.Reflection;
using System.Globalization;
using System.ComponentModel;

namespace HotelsApp.UI.ValueConverters
{
    /// <summary>
    /// Use to extract <see cref="DescriptionAttribute"/> text of <see cref="Enum"/> type elements
    /// <para>Posted by Brian Lagunas</para>
    /// <link>http://brianlagunas.com/a-better-way-to-data-bind-enums-in-wpf/</link>
    /// </summary>
    public class EnumDescriptionTypeConverter : EnumConverter
    {
        public EnumDescriptionTypeConverter(Type type) : base(type) { }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                if (value != null)
                {
                    FieldInfo fi = value.GetType().GetField(value.ToString());
                    if (fi != null)
                    {
                        var attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];
                        return ((attributes.Length > 0) && (!string.IsNullOrEmpty(attributes[0].Description))) ? attributes[0].Description : value.ToString();
                    }
                }
                return string.Empty;
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}