using System;
using System.Globalization;
using System.Windows.Input;

namespace HotelsApp.UI.ValueConverters
{
    public struct CommandKeys
    {
        public bool Undefined => MajorKey == null;
        public readonly Key? MajorKey;
        public readonly Key? ModifierKey;

        public override string ToString()
        {
            if (Undefined) return "Undefined";
            if (ModifierKey != null)
                return $"{ModifierKey} + {MajorKey}";
            return MajorKey.ToString();
        }

        public static CommandKeys GetKeysOf(string commandValue)
        {
            var command = commandValue as string;
            if (command == null) return new CommandKeys();
            switch (command)
            {
                case "demark": return new CommandKeys(Key.D, Key.LeftCtrl);

                case "add_input": return new CommandKeys(Key.I, Key.LeftAlt);
                case "add_medium": return new CommandKeys(Key.M, Key.LeftAlt);
                case "add_output": return new CommandKeys(Key.O, Key.LeftAlt);

                case "refresh": return new CommandKeys(Key.R, Key.LeftCtrl);

                case "remove": return new CommandKeys(Key.Delete, null);

                case "link": return new CommandKeys(Key.L, Key.LeftCtrl);
                case "unlink": return new CommandKeys(Key.U, Key.LeftCtrl);

                case "save": return new CommandKeys(Key.S, Key.LeftCtrl);
                case "revert": return new CommandKeys(Key.R, Key.LeftCtrl);
                case "back": return new CommandKeys(Key.Left, Key.LeftAlt);
            }
            return new CommandKeys();
        }

        private CommandKeys(Key major, Key? modifier)
        {
            MajorKey = major;
            ModifierKey = modifier;
        }
    }

    public class CommandTagToKeyConverter : BaseValueConverter<CommandTagToKeyConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var res = CommandKeys.GetKeysOf(value as string);
            if (res.Undefined)
                return Key.None;
            return res.MajorKey;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class CommandTagToModifierConverter : BaseValueConverter<CommandTagToModifierConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var res = CommandKeys.GetKeysOf(value as string);
            if (res.Undefined)
                return Key.None;
            return res.ModifierKey;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class CommandTagToStringConverter : BaseValueConverter<CommandTagToStringConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var res = CommandKeys.GetKeysOf(value as string);
            if (res.Undefined)
                return Key.None;
            return res.ToString();
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}