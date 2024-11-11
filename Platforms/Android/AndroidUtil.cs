using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateTimeFormatMAUI.Platforms.Android
{
    public class AndroidUtil
    {
        public static string GetSystemLocale()
        {
            return  Java.Util.Locale.Default.ToString();
        }
    }
}
