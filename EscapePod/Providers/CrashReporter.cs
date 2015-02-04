using System;
using System.IO;
using System.Net;
using System.Reflection;

namespace EscapePod.Providers
{
    public interface ICrashReporter
    {
        void Report(string error);
        void Report(Exception exception);
    }

    public class CrashReporter : ICrashReporter
    {
        const string CrashReportUrl = @"http://escapepodcrashreport.azurewebsites.net/api/CrashReport/Report";
       // const string CrashReportUrl = @"http://localhost:55979/api/CrashReport/Report";
        
        public void Report(string error)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest) WebRequest.Create(CrashReportUrl);
            httpWebRequest.ContentType = "text/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                CrashReport crashReport = new CrashReport();
                crashReport.Error = error;
                crashReport.Version = GetCurrentVersion().ToString();

                string json = Newtonsoft.Json.JsonConvert.SerializeObject(crashReport);

                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();

                var httpResponse = (HttpWebResponse) httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                }
            }
        }

        public void Report(Exception exception)
        {
            Report(exception.ToString());
        }

        /// <summary>
        /// Retrieve the current version of the executing assembly.
        /// This is the one in the AssemblyVersion attribute.
        /// </summary>
        /// <returns></returns>
        public static Version GetCurrentVersion()
        {
            var assembly = Assembly.GetExecutingAssembly().FullName;
            var fullVersionNumber = assembly.Split('=')[1].Split(',')[0];

            Version version = new Version(fullVersionNumber);

            return version;
        }
    }
}
