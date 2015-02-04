using System.IO;

namespace EscapePod.Providers
{
    public interface IDirectoryProvider
    {
        string SanitiseFileName(string name);
    }

    public class DirectoryProvider : IDirectoryProvider
    {
        public string SanitiseFileName(string name)
        {
            string invalid = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());

            foreach (char c in invalid)
            {
                name = name.Replace(c.ToString(), "");
            }

            return name;
        }

    }
}
