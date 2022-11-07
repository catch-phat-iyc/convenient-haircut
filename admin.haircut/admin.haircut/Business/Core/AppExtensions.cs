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

        public static string CreateMD5(this string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                return Convert.ToHexString(hashBytes); // .NET 5 +
            }
        }
    }
}
