﻿using Common.Common;
using Common.Tools;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace LucaHome.Pages
{
    public partial class MainPage : Page
    {
        private const string TAG = "MainPage";
        private readonly Logger _logger;

        private readonly NavigationService _navigationService;

        private readonly BirthdayPage _birthdayPage;
        private readonly WeatherPage _weatherPage;
        private readonly WirelessSocketPage _wirelessSocketPage;

        public MainPage(NavigationService navigationService)
        {
            _logger = new Logger(TAG, Enables.LOGGING);
            _navigationService = navigationService;

            _birthdayPage = new BirthdayPage(_navigationService);
            _weatherPage = new WeatherPage(_navigationService);
            _wirelessSocketPage = new WirelessSocketPage(_navigationService);

            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _navigationService.RemoveBackEntry();
        }

        private void OpenWeather_Click(object sender, RoutedEventArgs routedEventArgs)
        {
            _logger.Debug(string.Format("Received click of sender {0} with arguments {1}", sender, routedEventArgs));
            _navigationService.Navigate(_weatherPage);
        }

        private void Socket_Click(object sender, RoutedEventArgs routedEventArgs)
        {
            _logger.Debug(string.Format("Received click of sender {0} with arguments {1}", sender, routedEventArgs));
            _navigationService.Navigate(_wirelessSocketPage);
        }

        private void Birthday_Click(object sender, RoutedEventArgs routedEventArgs)
        {
            _logger.Debug(string.Format("Received click of sender {0} with arguments {1}", sender, routedEventArgs));
            _navigationService.Navigate(_birthdayPage);
        }

        private void Movie_Click(object sender, RoutedEventArgs routedEventArgs)
        {
            _logger.Debug(string.Format("Received click of sender {0} with arguments {1}", sender, routedEventArgs));
        }

        private void Temperature_Click(object sender, RoutedEventArgs routedEventArgs)
        {
            _logger.Debug(string.Format("Received click of sender {0} with arguments {1}", sender, routedEventArgs));
        }

        private void Settings_Click(object sender, RoutedEventArgs routedEventArgs)
        {
            _logger.Debug(string.Format("Received click of sender {0} with arguments {1}", sender, routedEventArgs));
        }
    }
}