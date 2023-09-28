using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Сomputer_shutdown_timer.Pages;

namespace Сomputer_shutdown_timer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Focus();
        }

        private int received_Hours = 0, received_Minutes = 0, received_Seconds = 0;

        private void Toolbar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => DragMove();

        async void Async_blinking_animation()
        {
            while (true)
            {
                Right_blink_system_time.Visibility = Visibility.Hidden;
                Left_blink_system_time.Visibility = Visibility.Hidden;
                await Task.Delay(500);

                Right_blink_system_time.Visibility = Visibility.Visible;
                Left_blink_system_time.Visibility = Visibility.Visible;
                await Task.Delay(500);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) => Async_blinking_animation();

        private void start_countdown_Click(object sender, RoutedEventArgs e)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\CST");

            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(Setting_time);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();

            if (key != null)
            {
                this.Visibility = Visibility.Hidden;
            }

            Tray.Visibility = Visibility.Visible;

            Get_setting(ref received_Hours, ref received_Minutes, ref received_Seconds);
            Display_time_settings.Text = $"{received_Hours}:{received_Minutes}:{received_Seconds}";
        }

        private void Setting_time(object? sender, EventArgs e)
        {
            Show_system_time();
            Time_comparison();
        }

        private void Show_system_time()
        {
            int hours_System_Time = DateTime.Now.Hour,
                minute_System_Time = DateTime.Now.Minute,
                seconds_System_Time = DateTime.Now.Second;

            if (seconds_System_Time < 10)
            {
                Seconds_system_time.Text = "0" + seconds_System_Time;
            }
            else
            {
                Seconds_system_time.Text = seconds_System_Time.ToString();
            }

            if (minute_System_Time < 10)
            {
                Minute_system_time.Text = "0" + minute_System_Time;
            }
            else
            {
                Minute_system_time.Text = minute_System_Time.ToString();
            }

            if (hours_System_Time < 10)
            {
                Hours_system_time.Text = "0" + hours_System_Time;
            }
            else
            {
                Hours_system_time.Text = hours_System_Time.ToString();
            }
        }

        private void Settings_Click(object sender, RoutedEventArgs e) => Show_pages.Content = new settings();

        private void Get_setting(ref int hours, ref int minute, ref int second)
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\CST"))
                {
                    hours = int.Parse(key.GetValue("Hours", hours).ToString());
                    minute = int.Parse(key.GetValue("Minute", minute).ToString());
                    second = int.Parse(key.GetValue("Second", second).ToString());
                }
            }
            catch (Exception)
            {

                Show_pages.Content = new settings();
            }
        }

        private void Tray_TrayLeftMouseDown(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Visible;
            Tray.Visibility = Visibility.Hidden;
        }

        private void Tray_setting_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Visible;
            Show_pages.Content = new settings();
        }

        private void Tray_close_Click(object sender, RoutedEventArgs e) => this.Close();

        private void Exit_program_Click(object sender, RoutedEventArgs e) => this.Close();

        private void Time_comparison()
        {
            if (DateTime.Now.Hour == received_Hours && DateTime.Now.Minute == received_Minutes && DateTime.Now.Second == received_Seconds)
            {
                Process.Start("shutdown", "/s /t 0");
            }
        }

        private void Roll_into_tray_Click(object sender, RoutedEventArgs e)
        {
            Tray_visibility();
        }

        private void Tray_visibility()
        {
            this.Visibility = Visibility.Hidden;
            Tray.Visibility = Visibility.Visible;
        }

    }
}
