using IllusionPlugin;
using MelonLoader.Utils;
using System.Collections.Generic;

namespace IllusionInjector
{
    public static class PluginManager
    {
        internal static List<IPlugin> _Plugins = new List<IPlugin>();
        public static IEnumerable<IPlugin> Plugins { get => _Plugins; }
        public class AppInfo
        {
            public static string StartupPath { get => MelonEnvironment.GameRootDirectory; }
        }
    }
}