using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Reflection;

namespace ConsoleCommander
{
    /// <summary>
    /// turns text into objects
    /// </summary>
    static class Parser
    {
        // most of the code in this class is copy-pasted from PENIS for Unity
        public static object Parse(string text, Type type)
        {
            ParseMethod method;
            if (ParseMethods.TryGetValue(type, out method))
                return method(text);

            if (type.IsEnum)
                return ParseEnum(text, type);

            throw new Exception($"The text {text} cannot be converted into type {type}");
        }

        private delegate object ParseMethod(string text);
        private static Dictionary<Type, ParseMethod> ParseMethods = new Dictionary<Type, ParseMethod>()
        {
            [typeof(string)] = ParseString,

            // integer types
            [typeof(int)] = ParseInt,
            [typeof(decimal)] = ParseDecimal,
            [typeof(long)] = ParseLong,
            [typeof(short)] = ParseShort,
            [typeof(uint)] = ParseUint,
            [typeof(ulong)] = ParseUlong,
            [typeof(ushort)] = ParseUshort,

            // floating point types
            [typeof(float)] = ParseFloat,
            [typeof(double)] = ParseDouble,

            [typeof(byte)] = ParseByte,
            [typeof(sbyte)] = ParseSbyte,

            [typeof(bool)] = ParseBool,
            [typeof(DateTime)] = ParseDateTime,
            [typeof(char)] = ParseChar,
            [typeof(Type)] = ParseType,
        };


        private static object ParseString(string text)
        {
            if (text.Length > 1 && text[0] == '"' && text[text.Length - 1] == '"')
                text = text.Substring(1, text.Length - 2);

            return text;
        }

        // all the annoying variations of the "number" object...
        private static object ParseInt(string text) { return int.Parse(text); }
        private static object ParseDecimal(string text) { return decimal.Parse(text); }
        private static object ParseLong(string text) { return long.Parse(text); }
        private static object ParseShort(string text) { return short.Parse(text); }
        private static object ParseUint(string text) { return uint.Parse(text); }
        private static object ParseUlong(string text) { return ulong.Parse(text); }
        private static object ParseUshort(string text) { return ushort.Parse(text); }

        private static readonly NumberFormatInfo LowercaseParser = new NumberFormatInfo()
        {
            PositiveInfinitySymbol = "infinity",
            NegativeInfinitySymbol = "-infinity",
            NaNSymbol = "nan"
        };
        private static object ParseFloat(string text) { return float.Parse(text.ToLower(), LowercaseParser); }
        private static object ParseDouble(string text) { return double.Parse(text.ToLower(), LowercaseParser); }

        private static object ParseByte(string text) { return byte.Parse(text); }
        private static object ParseSbyte(string text) { return sbyte.Parse(text); }

        private static object ParseBool(string text)
        {
            text = text.ToLower();
            if (text == "true") { return true; }
            if (text == "false") { return false; }
            throw new FormatException($"{text} is not a boolean value");
        }

        private static object ParseDateTime(string text)
        {
            DateTime result;
            if (DateTime.TryParseExact(text, "yyyy-MM-dd_HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
                return result;

            throw new FormatException("DateTimes must be in the format 'yyyy-MM-dd_HH:mm:ss'");
        }

        private static object ParseEnum(string text, Type type)
        {
            return Enum.Parse(type, text);
        }

        private static object ParseChar(string text)
        {
            return text[0];
        }

        private static Dictionary<string, Type> TypeCache = new Dictionary<string, Type>();
        private static object ParseType(string typeName)
        {
            Type t;
            if (TypeCache.TryGetValue(typeName, out t))
            {
                return t;
            }

            foreach (Assembly a in AppDomain.CurrentDomain.GetAssemblies())
            {
                t = a.GetType(typeName);
                if (t != null)
                {
                    TypeCache.Add(typeName, t);
                    return t;
                }
            }

            throw new Exception(typeName + " is not a type!");
        }
    }
}
