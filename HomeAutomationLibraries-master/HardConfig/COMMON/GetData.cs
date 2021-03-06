﻿using System.Collections.Generic;

namespace HardConfig.COMMON
{
    public static class GetData
    {
        public static string ValueFromDeviceDictionary( Dictionary<uint, string> dic, uint key )
        {
            // Try to get the result in the static Dictionary
            if (dic.TryGetValue( key, out string result ))
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
