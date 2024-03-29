using System.Globalization;

namespace FlightInvoice.BackgroundServices
{
    internal class PrivateLogFormatter : IFormatProvider, ICustomFormatter
    {
        private static string SafeString(string value)
        {
            if (String.IsNullOrEmpty(value))
                return value;

            return value.Replace("\n", "\\n").Replace("\r", "").Replace("'", "\\'");
        }

        private static Exception GetRootException(Exception exception)
        {
            while (exception.InnerException != null)
                exception = exception.InnerException;

            return exception;
        }

        private static string FormatException(Exception exception, HashSet<Exception> list)
        {
            if (list.Add(exception))
            {
                if (exception.InnerException == null)
                    return String.Format("[Message: '{0}', StackTrace: '{1}']", SafeString(exception.Message), SafeString(exception.StackTrace));
                else
                    return String.Format("[Message: '{0}', StackTrace: '{1}', InnerException: {2}]", SafeString(exception.Message), SafeString(exception.StackTrace), FormatException(exception.InnerException, list));
            }
            else
            {
                return "$Reference";
            }
        }

        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            if (String.IsNullOrEmpty(format))
            {
                if (arg == null)
                    return "(null)";
                else if (arg is string)
                    return "'" + SafeString(Convert.ToString(arg)) + "'";
                else if (arg is int)
                    return ((int)arg).ToString(CultureInfo.InvariantCulture);
                else if (arg is double)
                    return ((double)arg).ToString(CultureInfo.InvariantCulture);
                else if (arg is bool)
                    return ((bool)arg).ToString(CultureInfo.InvariantCulture);
                else if (arg is char)
                    return ((char)arg).ToString(CultureInfo.InvariantCulture);
                else if (arg is DateTime)
                    return ((DateTime)arg).ToUniversalTime().ToString("s");
                else if (arg is string[])
                    return "[" + String.Join(", ", Array.ConvertAll<string, string>((string[])arg, s => Format(format, s, this))) + "]";
                else if (arg is int[])
                    return "[" + String.Join(", ", Array.ConvertAll<int, string>((int[])arg, s => Format(format, s, this))) + "]";
                else if (arg is double[])
                    return "[" + String.Join(", ", Array.ConvertAll<double, string>((double[])arg, s => Format(format, s, this))) + "]";
                else if (arg is Exception)
                    return FormatException((Exception)arg, new HashSet<Exception>());
                else if (arg is byte[])
                    return "[" + Convert.ToBase64String(((byte[])arg)) + "]";
                else if (arg is Uri)
                    return Format(format, ((Uri)arg).ToString(), this);
                else if (arg is Enum)
                    return arg.ToString();
                else if (arg is long)
                    return ((long)arg).ToString(CultureInfo.InvariantCulture);
                else
                    return arg.ToString();
            }
            else if (format == "#")
            {
                return Convert.ToString(arg);
            }
            else
            {
                return String.Format("{0:" + format + "}", arg);
            }
        }

        public object GetFormat(Type formatType)
        {
            return this;
        }
    }
}