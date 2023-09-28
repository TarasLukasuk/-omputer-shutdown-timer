using Microsoft.Win32;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;

namespace Сomputer_shutdown_timer.Pages
{
    /// <summary>
    /// Логика взаимодействия для settings.xaml
    /// </summary>
    public partial class settings : Page
    {
        public settings()
        {
            InitializeComponent();
        }

        private const int MAX_VALUE_HOURS = 23;
        private const int MIN_VALUE_HOURS = 0;
        private const int MAX_VALUE_SECOND_MINUTE = 59;
        private const int MIN_VALUE_SECOND_MINUTE = 0;

        private int hours = 0, minute = 0, second = 0;

        private void Reducing_number_hours_Click(object sender, RoutedEventArgs e)
        {
            if (hours > MIN_VALUE_HOURS)
            {
                hours--;
                Content_hours.Text = hours.ToString();
            }

            if (hours < 10)
            {
                Content_hours.Text = "0" + hours.ToString();
            }

            Shutdown_time();
        }

        private void Increasing_number_hours_Click(object sender, RoutedEventArgs e)
        {

            if (hours < MAX_VALUE_HOURS)
            {
                hours++;
                Content_hours.Text = hours.ToString();
            }

            if (hours < 10)
            {
                Content_hours.Text = "0" + hours.ToString();
            }

            Shutdown_time();
        }

        private void Reducing_number_minute_Click(object sender, RoutedEventArgs e)
        {
            if (minute > MIN_VALUE_SECOND_MINUTE)
            {
                minute--;
                Content_minute.Text = minute.ToString();
            }

            if (minute < 10)
            {
                Content_minute.Text = "0" + minute.ToString();
            }

            Shutdown_time();
        }

        private void Increasing_number_minute_Click(object sender, RoutedEventArgs e)
        {
            if (minute < MAX_VALUE_SECOND_MINUTE)
            {
                minute++;
                Content_minute.Text = minute.ToString();
            }

            if (minute < 10)
            {
                Content_minute.Text = "0" + minute.ToString();
            }

            Shutdown_time();
        }

        private void Increasing_number_second_Click(object sender, RoutedEventArgs e)
        {
            if (second < MAX_VALUE_SECOND_MINUTE)
            {
                second++;
                Content_second.Text = second.ToString();
            }

            if (second < 10)
            {
                Content_second.Text = "0" + second.ToString();
            }

            Shutdown_time();
        }

        private void Return_main_page_Click(object sender, RoutedEventArgs e)=> this.Visibility = Visibility.Hidden;

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Save_settings();
            Closing_program();
        }

        private void Reducing_number_second_Click(object sender, RoutedEventArgs e)
        {
            if (second > MIN_VALUE_SECOND_MINUTE)
            {
                second--;
                Content_second.Text = second.ToString();
            }

            if (second < 10)
            {
                Content_second.Text = "0" + second.ToString();
            }

            Shutdown_time();
        }

        private void Shutdown_time() => Shutdown_time_text.Text = $"Ваш комп'ютер вимкнеця о {hours}:{minute}:{second}";

        private void Save_settings()
        {
            using (RegistryKey key=Registry.CurrentUser.CreateSubKey(@"Software\CST"))
            {
                key.SetValue("Hours",hours);
                key.SetValue("Minute", minute);
                key.SetValue("Second", second);
            }
        }

        private void Closing_program()=>Process.GetCurrentProcess().Kill();
    }
}
