using System.Globalization;

namespace ToDoListApp.Converters
{
    public class TaskColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isComplete && isComplete)
                return Color.FromArgb("#A5D6A7"); // green shade for complete
            return Color.FromArgb("#FF9800");     // your existing orange for incomplete
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}