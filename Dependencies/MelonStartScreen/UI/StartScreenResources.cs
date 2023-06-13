using System.IO;
using System.Reflection;

namespace MelonLoader.MelonStartScreen.UI
{
    internal static class StartScreenResources
    {
        //Logos
        public static byte[] LLC_Zero_Logo => GetResource("Logo_LLC_Zero.dat");
        private static byte[] GetResource(string name)
        {
            using var s = Assembly.GetExecutingAssembly().GetManifestResourceStream($"MelonLoader.MelonStartScreen.Resources.{name}");
            if (s == null)
                return null;
#if NET6_0
            using var ms = new MemoryStream();
            s.CopyTo(ms);
            return ms.ToArray();
#else
            var ret = new byte[s.Length];
            s.Read(ret, 0, ret.Length);
            return ret;
#endif
        }
    }
}