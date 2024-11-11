using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateTimeFormatMAUI.Platforms.iOS
{
    public class iOSUtil
    {
        public static string GetSystemLocale()
        {
            return NSLocale.PreferredLanguages.FirstOrDefault();
        }
    }
}
