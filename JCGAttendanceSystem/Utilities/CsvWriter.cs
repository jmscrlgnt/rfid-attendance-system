using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace JCGAttendanceSystem.Utilities
{
    internal static class CsvWriter
    {
        public static void Write(string path, IEnumerable<string> headers, IEnumerable<IEnumerable<string>> rows)
        {
            using (var writer = new StreamWriter(path, false, new UTF8Encoding(true)))
            {
                writer.WriteLine(string.Join(",", headers.Select(Escape)));
                foreach (var row in rows)
                    writer.WriteLine(string.Join(",", row.Select(Escape)));
            }
        }

        public static string Escape(string value)
        {
            value = value ?? string.Empty;
            var escaped = value.Replace("\"", "\"\"");
            return value.IndexOfAny(new[] { ',', '"', '\r', '\n' }) >= 0 ? "\"" + escaped + "\"" : escaped;
        }
    }
}
