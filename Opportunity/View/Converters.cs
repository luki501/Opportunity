using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Opportunity.View
{

    public class BoolToOppositeBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targettype, object parameter, System.Globalization.CultureInfo culture)
        {
            return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return !(bool)value;
        }
    }

    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targettype, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                if ((bool)value)
                    return System.Windows.Visibility.Visible;
                else
                    return System.Windows.Visibility.Collapsed;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }

    public class BoolOppositeToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targettype, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                if ((bool)value)
                    return System.Windows.Visibility.Collapsed;
                else
                    return System.Windows.Visibility.Visible;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }

    public class BoolToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targettype, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                if ((bool)value)
                    return new SolidColorBrush(Colors.Green);
                else
                    return new SolidColorBrush(Colors.Red);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }

    public class NullToFalseConverter : IValueConverter
    {
        public object Convert(object value, Type targettype, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                if (value != null)
                    return true;
                else
                    return false;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }

    public class NotNullToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targettype, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
                return System.Windows.Visibility.Visible;
            else
                return System.Windows.Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
    public class IntToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targettype, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                int mode = (int)value;
                if (mode > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
    public class IntToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targettype, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                int mode = (int)value;
                if (mode > 0)
                    return new SolidColorBrush(Colors.Green);
                else
                    return new SolidColorBrush(Colors.Red);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
    public class IntToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targettype, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                int mode = (int)value;
                if (mode > 0)
                    return System.Windows.Visibility.Visible;
                else
                    return System.Windows.Visibility.Collapsed;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }

    public class EnumToIntConverter : IValueConverter
    {
        public object Convert(object value, Type targettype, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                int returnValue = 0;
                if (value != null && value.GetType().IsEnum)
                {
                    returnValue = (int)value;
                }
                return returnValue;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
    public class DecimalConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString().Replace(",", ".");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString().Replace(",", ".");
        }

    }
    public class MultiBindingConverter : IMultiValueConverter
    {
        public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.Clone();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
