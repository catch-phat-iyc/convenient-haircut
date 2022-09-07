using System.ComponentModel;

namespace Admin.Haircut.Business.Core
{
    public static class AppExtensions
    {
        public static string FormatDate(this DateTime? dateTime)
        {
            if (dateTime == null || dateTime.Value.Year == 1)
            {
                return "";
            }
            else
            {
                return $"{dateTime:dd/MM/yyyy}";
            }
        }
        public static string FormatDateTime(this DateTime? dateTime)
        {
            if (dateTime == null || dateTime.Value.Year == 1)
            {
                return "";
            }
            else
            {
                return $"{dateTime:HH:mm:ss, dd/MM/yyyy}";
            }
        }

        public static string GetDescription<T>(this T source)
        {
            if (source == null)
                return "NULL";

            var fi = source.GetType().GetField(source.ToString());

            if (fi == null)
                return "NULL";

            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute), false);

            return attributes.Length > 0 ? attributes[0].Description : source.ToString();
        }
    }
}
