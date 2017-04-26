using System.Text.RegularExpressions;

namespace NIP.Tools
{
    public class KeyValidator
    {
        public string GetNormalizedKey(string key)
        {
            return Regex.Replace(key, "[-,PL]", string.Empty);
        }
        public bool IsNIP(string key)
        {
            return Regex.IsMatch(key, "^[0-9]{10}$");
        }

        public bool IsKRS(string key)
        {
            return Regex.IsMatch(key, "^[0-9]{10}$");
        }

        public bool IsREGON(string key)
        {
            return Regex.IsMatch(key, "^[0-9]{9}$");
        }
    }
}