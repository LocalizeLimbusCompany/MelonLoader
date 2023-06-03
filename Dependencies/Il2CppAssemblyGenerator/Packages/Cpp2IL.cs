using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Semver;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Net;
using System.Text;
using System;

namespace MelonLoader.Il2CppAssemblyGenerator.Packages
{
    internal class Cpp2IL : Models.ExecutablePackage
    {
        private static string ReleaseName =>
            MelonUtils.IsWindows ? "Windows-Netframework472" : MelonUtils.IsUnix ? "Linux" : "OSX";
        // 获取最快节点
        fastestNode = GetFastnetNode();
            
        internal Cpp2IL()
        {
            Version = MelonLaunchOptions.Il2CppAssemblyGenerator.ForceVersion_Dumper;
#if !DEBUG
            if (string.IsNullOrEmpty(Version) || Version.Equals("0.0.0.0"))
                Version = RemoteAPI.Info.ForceDumperVersion;
#endif
            if (string.IsNullOrEmpty(Version) || Version.Equals("0.0.0.0"))
                Version = "2022.1.0-pre-release.10";

            Name = nameof(Cpp2IL);
            Destination = Path.Combine(Core.BasePath, Name);
            OutputFolder = Path.Combine(Destination, "cpp2il_out");
            
            URL = $"https://"+fastestNode+"/{Name}-{Version}-{ReleaseName}.zip";

            ExeFilePath = Path.Combine(Destination, $"{Name}.exe");
            
            FilePath = Path.Combine(Core.BasePath, $"{Name}_{Version}.zip");

            if (MelonUtils.IsWindows) 
                return;
            
            URL = URL.Replace(".zip", "");
            ExeFilePath = ExeFilePath.Replace(".exe", "");
            FilePath = FilePath.Replace(".zip", "");
        }

        internal override bool ShouldSetup() 
            => string.IsNullOrEmpty(Config.Values.DumperVersion) 
            || !Config.Values.DumperVersion.Equals(Version);

        internal override void Cleanup() { }

        internal override void Save()
            => Save(ref Config.Values.DumperVersion);

        internal override bool Execute()
        {
            if (SemVersion.Parse(Version) <= SemVersion.Parse("2022.0.999"))
                return ExecuteOld();
            return ExecuteNew();
        }

        private bool ExecuteNew()
        {
            if (Execute(new string[] {
                MelonDebug.IsEnabled() ? "--verbose" : string.Empty,
                "--game-path",
                "\"" + Path.GetDirectoryName(Core.GameAssemblyPath) + "\"",
                "--exe-name",
                "\"" + Process.GetCurrentProcess().ProcessName + "\"",
                "--use-processor",
                "attributeinjector",
                "--output-as",
                "dummydll"

            }, false, new Dictionary<string, string>() {
                {"NO_COLOR", "1"}
            }))
                return true;

            return false;
        }

        private bool ExecuteOld()
        {
            if (Execute(new string[] {
                MelonDebug.IsEnabled() ? "--verbose" : string.Empty,
                "--game-path",
                "\"" + Path.GetDirectoryName(Core.GameAssemblyPath) + "\"",
                "--exe-name",
                "\"" + Process.GetCurrentProcess().ProcessName + "\"",

                "--skip-analysis",
                "--skip-metadata-txts",
                "--disable-registration-prompts"

            }, false, new Dictionary<string, string>() {
                {"NO_COLOR", "1"}
            }))
                return true;

            return false;
        }
        private string GetFastnetNode()
        {
            string[] urls = {"limbus.determination.top", "llc.determination.top", "dl.determination.top" };

            Dictionary<string, long> pingTimes = new Dictionary<string, long>();

            foreach (string url in urls)
            {
                Ping ping = new Ping();
                try
                {
                    PingReply reply = ping.Send(url);
                    if (reply.Status == IPStatus.Success)
                    {
                        pingTimes.Add(url, reply.RoundtripTime);
                    }
                }
                catch
                {
                }
            }

            List<KeyValuePair<string, long>> pingTimesList = new List<KeyValuePair<string, long>>(pingTimes);
            pingTimesList.Sort(delegate (KeyValuePair<string, long> pair1, KeyValuePair<string, long> pair2)
            {
                return pair1.Value.CompareTo(pair2.Value);
            });
            return pingTimesList[0].Key;
        }
    }
}
