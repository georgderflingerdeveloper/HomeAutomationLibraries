using System.Collections.Generic;

namespace HardConfig.COMMON
{
    public static class GetData
    {
        public static string ValueFromDeviceDictionary( Dictionary<uint, string> dic, uint key )
        {
            // Try to get the result in the static Dictionary
            string result;
            if (dic.TryGetValue( key, out result ))
            {
                return result;
            }
            else
            {
                return "";
            }
        }
    }
}
