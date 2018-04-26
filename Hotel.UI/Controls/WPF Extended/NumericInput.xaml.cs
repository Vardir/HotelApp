using System;
using System.Windows;
using System.Windows.Controls;

namespace Hotel.UI.Controls
{
    /// <summary>
    /// Interaction logic for NumericInput.xaml
    /// </summary>
    public partial class NumericInput : UserControl
    {
        public double Min
        {
            get => (double)GetValue(MinProperty);
            set
            {
                if (Max >= value)
                    SetValue(MinProperty, value);
            }
        }
        public double Max
        {
            get => (double)GetValue(MaxProperty);
            set
            {
                if (Min <= value)
                    SetValue(MaxProperty, value);
            }
        }
        public double Value
        {
            get => (double)GetValue(ValueProperty);
            set
            {
                if (value <= Max && value >= Min)
                {
                    SetValue(ValueProperty, Round(value, DecimalPlaces));
                }
            }
        }
        public int DecimalPlaces
        {
            get => (int)GetValue(DecimalPlacesProperty);
            set
            {
                if (value > -1 && value < 100)
                    SetValue(DecimalPlacesProperty, value);
            }
        }

        #region Dependency Properties
        public static readonly DependencyProperty MaxProperty =
            DependencyProperty.Register(nameof(Max), typeof(double), typeof(NumericInput), new UIPropertyMetadata(0.0, MaxChanged));

        public static readonly DependencyProperty MinProperty =
            DependencyProperty.Register(nameof(Min), typeof(double), typeof(NumericInput), new UIPropertyMetadata(0.0, MinChanged));

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(nameof(Value), typeof(double), typeof(NumericInput), new PropertyMetadata(0.0));

        public static readonly DependencyProperty DecimalPlacesProperty =
            DependencyProperty.Register(nameof(DecimalPlaces), typeof(int), typeof(NumericInput), new PropertyMetadata(0, DecimalPlacesChanged));
        #endregion

        public NumericInput()
        {
            InitializeComponent();
        }

        static void MinChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as NumericInput;
            if (control.Value < (double)e.NewValue)
                control.Value = (double)e.NewValue;
        }
        static void MaxChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as NumericInput;
            if (control.Value > (double)e.NewValue)
                control.Value = (double)e.NewValue;
        }
        static void DecimalPlacesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as NumericInput;
            control.Value = Round(control.Value, (int)e.NewValue);
        }

        static double Round(double value, int decimalPlaces) => Math.Round(value, decimalPlaces);
    }
}
