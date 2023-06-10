using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using Semver;

#pragma warning disable 0649

namespace MelonLoader.Il2CppAssemblyGenerator
{
    internal static class RemoteAPI
    {
        internal class InfoStruct
        {
            internal string ForceDumperVersion = null;
            internal string ObfuscationRegex = null;
            internal string MappingURL = null;
            internal string MappingFileSHA512 = null;
        }
        internal static InfoStruct Info = new InfoStruct();

        private class HostInfo
        {
            internal string URL = null;
            internal LemonFunc<string, InfoStruct> Func = null;
            internal HostInfo(string url, LemonFunc<string, InfoStruct> func)
            {
                URL = url;
                Func = func;
            }
        }
        private static List<HostInfo> HostList = null;

        static RemoteAPI()
        {
            
        }

        internal static void Contact()
        {

        }

        private static void ContactHosts()
        {
            
        }

        private class DefaultHostInfo
        {
            internal static class Melon
            {
                internal static string API_VERSION = "v1";
                internal static string API_URL = $"https://api.melonloader.com/api/{API_VERSION}/game/";
                internal static string API_URL_1 = $"https://api-1.melonloader.com/api/{API_VERSION}/game/";
                internal static string API_URL_2 = $"https://api-2.melonloader.com/api/{API_VERSION}/game/";
                internal static string API_URL_SAMBOY = $"https://melon.samboy.dev/api/{API_VERSION}/game/";

                internal static InfoStruct Contact(string response_str)
                {
                    ResponseStruct responseobj = MelonUtils.ParseJSONStringtoStruct<ResponseStruct>(response_str);
                    if (responseobj == null)
                        return null;

                    InfoStruct returninfo = new InfoStruct();
                    returninfo.ForceDumperVersion = responseobj.forceCpp2IlVersion;
                    returninfo.ObfuscationRegex = responseobj.obfuscationRegex;
                    returninfo.MappingURL = responseobj.mappingUrl;
                    returninfo.MappingFileSHA512 = responseobj.mappingFileSHA512;
                    return returninfo;
                }

                internal class ResponseStruct
                {
                    public string gameSlug = null;
                    public string gameName = null;
                    public string mappingUrl = null;
                    public string mappingFileSHA512 = null;
                    public string forceCpp2IlVersion = null;
                    public string forceUnhollowerVersion = null; //TODO: Remove this from the API
                    public string obfuscationRegex = null;
                }
            }

            internal static class Ruby
            {
                internal static string API_URL = "https://ruby-core.com/api/ml/";

                internal static InfoStruct Contact(string response_str)
                {
                    ResponseStruct responseobj = MelonUtils.ParseJSONStringtoStruct<ResponseStruct>(response_str);
                    if (responseobj == null)
                        return null;

                    InfoStruct returninfo = new InfoStruct();
                    //returninfo.ForceDumperVersion = responseobj.forceDumperVersion;
                    returninfo.ObfuscationRegex = responseobj.obfuscationRegex;
                    returninfo.MappingURL = responseobj.mappingURL;
                    returninfo.MappingFileSHA512 = responseobj.mappingFileSHA512;
                    return returninfo;
                }

                private class ResponseStruct
                {
                    public string forceDumperVersion = null;
                    public string forceUnhollowerVersion = null; //TODO: Remove this from the API
                    public string obfuscationRegex = null;
                    public string mappingURL = null;
                    public string mappingFileSHA512 = null;
                }
            }
        }
    }
}
