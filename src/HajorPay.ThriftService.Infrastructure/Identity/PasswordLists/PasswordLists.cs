using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HajorPay.ThriftService.Infrastructure.Identity.PasswordLists
{
    internal static class PasswordLists
    {
        //public static HashSet<string> Top1000Passwords { get; } = LoadPasswordList("HajorPay.ThriftService.Infrastructure.Identity.PasswordLists.10-million-password-list-top-1000.txt"); //TODO: If the name of the projects or solution changes, you need to update this.
        public static HashSet<string> Top1000Passwords { get; } = new HashSet<string>(LoadPasswordList("HajorPay.ThriftService.Infrastructure.Identity.PasswordLists.10-million-password-list-top-1000.txt"), StringComparer.OrdinalIgnoreCase);

        //private static HashSet<string> LoadPasswordList(string resourceName)
        //{
        //    var assembly = typeof(PasswordLists).GetTypeInfo().Assembly;
        //    using (var stream = assembly.GetManifestResourceStream(resourceName))
        //    {
        //        if (stream == null)
        //        {
        //            throw new FileNotFoundException($"Embedded resource '{resourceName}' not found.");
        //        }

        //        using (var reader = new StreamReader(stream))
        //        {
        //            var hashSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        //            while (!reader.EndOfStream)
        //            {
        //                var line = reader.ReadLine();
        //                if (!string.IsNullOrWhiteSpace(line))
        //                {
        //                    hashSet.Add(line.Trim());
        //                }
        //            }
        //            return hashSet;
        //        }
        //    }
        //}

        private static IEnumerable<string> LoadPasswordList(string resourceName)
        {
            var assembly = typeof(PasswordLists).GetTypeInfo().Assembly;
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                {
                    throw new FileNotFoundException($"Embedded resource '{resourceName}' not found.");
                }

                using (var reader = new StreamReader(stream))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        if (!string.IsNullOrWhiteSpace(line))
                        {
                            yield return line.Trim();
                        }
                    }
                }
            }
        }
    }
}
