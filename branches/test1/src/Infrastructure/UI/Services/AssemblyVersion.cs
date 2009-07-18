using System.Diagnostics;
using System.Reflection;

namespace July09v31.Infrastructure.UI.Services
{
    public class AssemblyVersion : IAssemblyVersion
    {
        public string GetAssemblyVersion()
        {
            Assembly executingAssembly = Assembly.GetExecutingAssembly();
            FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(executingAssembly.Location);
            return versionInfo.FileVersion;
        }
    }
}