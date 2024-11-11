#if ANDROID
using DateTimeFormatMAUI.Platforms.Android;
#endif
#if IOS
using DateTimeFormatMAUI.Platforms.iOS;
#endif
using DateTimeFormatSample;
using Microsoft.Extensions.Configuration;
using System.Globalization;

namespace DateTimeFormatMAUI
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        private Label dateTimeLabel;
        private Label cultureInfoLabel;
        private Label calendarsLabel;

        public MainPage()
        {
            InitializeComponent();
            dateTimeLabel = new Label();
            cultureInfoLabel = new Label();
            calendarsLabel = new Label();

            var stackLayout = new VerticalStackLayout
            {
                Children = {
                    dateTimeLabel,
                    cultureInfoLabel,
                    calendarsLabel
                }
            };

            Content = stackLayout;
            UpdateDateTimeDisplay();

        }

        public void SetCulture(string localeToUse)
        {
            var locale = localeToUse.Replace("_", "-");
            CultureInfo newCultureInfo;
            try
            {
                newCultureInfo = new CultureInfo(locale);
            }
            catch (CultureNotFoundException)
            {
                newCultureInfo = new CultureInfo("en");
            }

            CultureInfo.CurrentUICulture = newCultureInfo; // set the Thread for locale-aware methods
            CultureInfo.CurrentCulture = newCultureInfo; // set the Thread for locale-aware methods
            CultureInfo.DefaultThreadCurrentCulture = newCultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = newCultureInfo;
        }

        private void UpdateDateTimeDisplay()
        {
            var currentDateTime = DateTime.Now;
            var platformLocale = string.Empty;
#if ANDROID
                platformLocale = AndroidUtil.GetSystemLocale();
#else
                platformLocale = iOSUtil.GetSystemLocale();
#endif
            SetCulture(platformLocale);
            // 1. Current locale
            var currentCulture = CultureInfo.DefaultThreadCurrentUICulture;
            cultureInfoLabel.Text = $"Current Locale: {currentCulture.Name}";

            // 2. Current calendar
            var currentCalendar = currentCulture.DateTimeFormat.Calendar;
            var calendarType = currentCalendar.GetType().Name;
            calendarsLabel.Text = $"Current Calendar: {calendarType}";

            // 3. Formatted date based on current culture and calendar
            var longDateString = currentDateTime.FormatLongDateForDisplay();
            var shortDateString1 = currentDateTime.FormatShortDateForDisplay1();
            var shortDateString2 = currentDateTime.FormatShortDateForDisplay2();
            var shortDateString3 = currentDateTime.FormatShortDateForDisplay3();

            // 4. Additional calendar info
            var cal1 = CultureInfo.DefaultThreadCurrentUICulture.DateTimeFormat.Calendar;
            var cal2 = CultureInfo.DefaultThreadCurrentCulture.DateTimeFormat.Calendar;
            var cal3 = CultureInfo.CurrentUICulture.DateTimeFormat.Calendar;
            var cal4 = CultureInfo.CurrentCulture.DateTimeFormat.Calendar;

            var calendarsInfo = $"Additional Calendars:\n" +
                               $"DefaultThreadCurrentUICulture: {cal1.GetType().Name}\n" +
                               $"DefaultThreadCurrentCulture: {cal2.GetType().Name}\n" +
                               $"CurrentUICulture: {cal3.GetType().Name}\n" +
                               $"CurrentCulture: {cal4.GetType().Name}";

            dateTimeLabel.Text = $"Long Date: {longDateString}\n" +
                                $"Short Date 1: {shortDateString1}\n" +
                                $"Short Date 2: {shortDateString2}\n" +
                                $"Short Date 3: {shortDateString3}\n\n" +
                                calendarsInfo;

            Console.WriteLine(cultureInfoLabel.Text);
            Console.WriteLine(calendarsLabel.Text);
            Console.WriteLine(dateTimeLabel.Text);
        }
    }

}
