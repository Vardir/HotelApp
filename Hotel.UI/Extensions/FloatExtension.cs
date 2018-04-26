using System;
using System.Windows.Markup;

namespace Hotel.UI.Extensions
{
    public sealed class FloatExtension : MarkupExtension
    {
        public FloatExtension(float value) { Value = value; }
        public float Value { get; set; }
        public override Object ProvideValue(IServiceProvider sp) { return Value; }
    }
}