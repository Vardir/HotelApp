using System;
using System.Windows;
using System.Windows.Controls;

namespace HotelsApp.UI.Controls
{
    /// <summary>
    /// Interaction logic for NumericUpDown.xaml
    /// </summary>
    public partial class NumericUpDown : UserControl
    {
        public int DecimalPlaces
        {
            get => (int)GetValue(DecimalPlacesProperty);
            set
            {
                if (value > -1 && value < 100)
                    SetValue(DecimalPlacesProperty, value);
            }
        }
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
        public double Increment
        {
            get => (double)GetValue(IncrementProperty);
            set => SetValue(IncrementProperty, Round(value, DecimalPlaces));            
        }

        #region Dependency Properties
        public static readonly DependencyProperty MaxProperty =
            DependencyProperty.Register(nameof(Max), typeof(double), typeof(NumericUpDown), new UIPropertyMetadata(0.0, MaxChanged));

        public static readonly DependencyProperty MinProperty =
            DependencyProperty.Register(nameof(Min), typeof(double), typeof(NumericUpDown), new UIPropertyMetadata(0.0, MinChanged));

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(nameof(Value), typeof(double), typeof(NumericUpDown), new PropertyMetadata(0.0));

        public static readonly DependencyProperty DecimalPlacesProperty =
            DependencyProperty.Register(nameof(DecimalPlaces), typeof(int), typeof(NumericUpDown), new PropertyMetadata(0, DecimalPlacesChanged));

        public static readonly DependencyProperty IncrementProperty =
            DependencyProperty.Register(nameof(Increment), typeof(double), typeof(NumericUpDown), new PropertyMetadata(1.0));
        #endregion

        public NumericUpDown()
        {
            InitializeComponent();
        }
        
        static void MinChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as NumericUpDown;
            if (control.Value < (double)e.NewValue)
                control.Value = (double)e.NewValue;
        }
        static void MaxChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as NumericUpDown;
            if (control.Value > (double)e.NewValue)
                control.Value = (double)e.NewValue;
        }
        static void DecimalPlacesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as NumericUpDown;
            control.Value = Round(control.Value, (int)e.NewValue);
        }

        static double Round(double value, int decimalPlaces) => Math.Round(value, decimalPlaces);

        void keyDown_Click(object sender, RoutedEventArgs e)
        {
            Value -= Increment;
        }
        void keyUp_Click(object sender, RoutedEventArgs e)
        {
            Value += Increment;
        }
    }
}
