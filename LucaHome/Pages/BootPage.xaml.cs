﻿using Common.Common;
using Common.Dto;
using Common.Tools;
using Data.Services;
using OpenWeather.Models;
using OpenWeather.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace LucaHome.Pages
{
    public partial class BootPage : Page, INotifyPropertyChanged
    {
        private const string TAG = "BootPage";
        private readonly Logger _logger;

        private int _downloadCount = 0;
        private int _progressPercent = 0;
        private string _progressText = "Loading LucaHome WPF Application ... {0} %";
        private IList<string> _objectFinishedList = new List<string>();

        private readonly BirthdayService _birthdayService;
        private readonly CoinService _coinService;
        private readonly LibraryService _libraryService;
        private readonly MapContentService _mapContentService;
        private readonly MenuService _menuService;
        private readonly MovieService _movieService;
        private readonly NavigationService _navigationService;
        private readonly NovelService _novelService;
        private readonly OpenWeatherService _openWeatherService;
        private readonly ScheduleService _scheduleService;
        private readonly SecurityService _securityService;
        private readonly SeriesService _seriesService;
        private readonly ShoppingListService _shoppingListService;
        private readonly SpecialicedBookService _specialicedBookService;
        private readonly TemperatureService _temperatureService;
        private readonly WirelessSocketService _wirelessSocketService;

        public BootPage(NavigationService navigationService)
        {
            _logger = new Logger(TAG, Enables.LOGGING);

            _birthdayService = BirthdayService.Instance;
            _coinService = CoinService.Instance;
            _libraryService = LibraryService.Instance;
            _mapContentService = MapContentService.Instance;
            _menuService = MenuService.Instance;
            _movieService = MovieService.Instance;
            _navigationService = navigationService;
            _novelService = NovelService.Instance;
            _openWeatherService = OpenWeatherService.Instance;
            _scheduleService = ScheduleService.Instance;
            _securityService = SecurityService.Instance;
            _seriesService = SeriesService.Instance;
            _shoppingListService = ShoppingListService.Instance;
            _specialicedBookService = SpecialicedBookService.Instance;
            _temperatureService = TemperatureService.Instance;
            _wirelessSocketService = WirelessSocketService.Instance;

            InitializeComponent();
            DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string ProgressText
        {
            get
            {
                return String.Format(_progressText, _progressPercent);
            }
            set
            {
                OnPropertyChanged("ProgressText");
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs routedEventArgs)
        {
            _logger.Debug(string.Format("Page_Loaded with sender {0} and routedEventArgs: {1}", sender, routedEventArgs));

            _birthdayService.OnBirthdayDownloadFinished += _birthdayDownloadFinished;
            _coinService.OnCoinConversionDownloadFinished += _onCoinConversionDownloadFinished;
            _libraryService.OnMagazinListDownloadFinished += _onMagazinListDownloadFinished;
            _menuService.OnListedMenuDownloadFinished += _onListedMenuDownloadFinished;
            _movieService.OnMovieDownloadFinished += _movieDownloadFinished;
            _novelService.OnNovelListDownloadFinished += _onNovelListDownloadFinished;
            _openWeatherService.OnCurrentWeatherDownloadFinished += _currentWeatherDownloadFinished;
            _openWeatherService.OnForecastWeatherDownloadFinished += _forecastWeatherDownloadFinished;
            _securityService.OnSecurityDownloadFinished += _onSecurityDownloadFinished;
            _seriesService.OnSeriesListDownloadFinished += _onSeriesListDownloadFinished;
            _shoppingListService.OnShoppingListDownloadFinished += _onShoppingListDownloadFinished;
            _specialicedBookService.OnSpecialicedBookListDownloadEventHandler += _onSpecialicedBookListDownloadEventHandler;
            _wirelessSocketService.OnWirelessSocketDownloadFinished += _wirelessSocketDownloadFinished;

            _birthdayService.LoadBirthdayList();
            _coinService.LoadCoinConversionList();
            _libraryService.LoadMagazinList();
            _menuService.LoadListedMenuList();
            _movieService.LoadMovieList();
            _novelService.LoadNovelList();
            _openWeatherService.LoadCurrentWeather();
            _openWeatherService.LoadForecastModel();
            _securityService.LoadSecurity();
            _seriesService.LoadSeriesList();
            _shoppingListService.LoadShoppingList();
            _specialicedBookService.LoadBookList();
            _wirelessSocketService.LoadWirelessSocketList();
        }

        private void Page_Unloaded(object sender, RoutedEventArgs routedEventArgs)
        {
            _logger.Debug(string.Format("Page_Unloaded with sender {0} and routedEventArgs: {1}", sender, routedEventArgs));

            _downloadCount = 0;

            _birthdayService.OnBirthdayDownloadFinished -= _birthdayDownloadFinished;
            _coinService.OnCoinConversionDownloadFinished -= _onCoinConversionDownloadFinished;
            _coinService.OnCoinDownloadFinished -= _onCoinDownloadFinished;
            _libraryService.OnMagazinListDownloadFinished -= _onMagazinListDownloadFinished;
            _mapContentService.OnMapContentDownloadFinished -= _mapContentDownloadFinished;
            _menuService.OnListedMenuDownloadFinished -= _onListedMenuDownloadFinished;
            _menuService.OnMenuDownloadFinished -= _onMenuDownloadFinished;
            _movieService.OnMovieDownloadFinished -= _movieDownloadFinished;
            _novelService.OnNovelListDownloadFinished -= _onNovelListDownloadFinished;
            _openWeatherService.OnCurrentWeatherDownloadFinished -= _currentWeatherDownloadFinished;
            _openWeatherService.OnForecastWeatherDownloadFinished -= _forecastWeatherDownloadFinished;
            _scheduleService.OnScheduleDownloadFinished -= _scheduleDownloadFinished;
            _securityService.OnSecurityDownloadFinished -= _onSecurityDownloadFinished;
            _seriesService.OnSeriesListDownloadFinished -= _onSeriesListDownloadFinished;
            _shoppingListService.OnShoppingListDownloadFinished -= _onShoppingListDownloadFinished;
            _specialicedBookService.OnSpecialicedBookListDownloadEventHandler -= _onSpecialicedBookListDownloadEventHandler;
            _temperatureService.OnTemperatureDownloadFinished -= _temperatureDownloadFinished;
            _wirelessSocketService.OnWirelessSocketDownloadFinished -= _wirelessSocketDownloadFinished;
        }

        private void _birthdayDownloadFinished(IList<BirthdayDto> birthdayList, bool success, string response)
        {
            _logger.Debug(string.Format("_birthdayDownloadFinished with model {0} was successful: {1}", birthdayList, success));
            checkDownloadCount("_birthdayDownloadFinished");
        }

        private void _currentWeatherDownloadFinished(WeatherModel currentWeather, bool success)
        {
            _logger.Debug(string.Format("_currentWeatherDownloadFinished with model {0} was successful: {1}", currentWeather, success));
            checkDownloadCount("_currentWeatherDownloadFinished");

            // Start download of temperatures after downloading current weather
            _temperatureService.OnTemperatureDownloadFinished += _temperatureDownloadFinished;
            _temperatureService.LoadTemperatureList();
        }

        private void _forecastWeatherDownloadFinished(ForecastModel forecastWeather, bool success)
        {
            _logger.Debug(string.Format("_forecastWeatherDownloadFinished with model {0} was successful: {1}", forecastWeather, success));
            checkDownloadCount("_forecastWeatherDownloadFinished");
        }

        private void _mapContentDownloadFinished(IList<MapContentDto> mapContentList, bool success, string response)
        {
            _logger.Debug(string.Format("_mapContentDownloadFinished with model {0} was successful: {1}", mapContentList, success));
            checkDownloadCount("_mapContentDownloadFinished");
        }

        private void _movieDownloadFinished(IList<MovieDto> movieList, bool success, string response)
        {
            _logger.Debug(string.Format("_movieDownloadFinished with model {0} was successful: {1}", movieList, success));
            checkDownloadCount("_movieDownloadFinished");
        }

        private void _onCoinConversionDownloadFinished(IList<KeyValuePair<string, double>> coinConversionList, bool success, string response)
        {
            _logger.Debug(string.Format("_onCoinConversionDownloadFinished with model {0} was successful: {1}", coinConversionList, success));
            checkDownloadCount("_onCoinConversionDownloadFinished");

            // Start download of coins after downloading coin conversion
            _coinService.OnCoinDownloadFinished += _onCoinDownloadFinished;
            _coinService.LoadCoinList();
        }

        private void _onCoinDownloadFinished(IList<CoinDto> coinList, bool success, string response)
        {
            _logger.Debug(string.Format("_onCoinDownloadFinished with model {0} was successful: {1}", coinList, success));
            checkDownloadCount("_onCoinDownloadFinished");
        }

        private void _onListedMenuDownloadFinished(IList<ListedMenuDto> listedMenuList, bool success, string response)
        {
            _logger.Debug(string.Format("_onListedMenuDownloadFinished with model {0} was successful: {1}", listedMenuList, success));
            checkDownloadCount("_onListedMenuDownloadFinished");

            // Start download of menu after downloading listed menu entries
            _menuService.OnMenuDownloadFinished += _onMenuDownloadFinished;
            _menuService.LoadMenuList();
        }

        private void _onMagazinListDownloadFinished(IList<MagazinDirDto> magazinList, bool success, string response)
        {
            _logger.Debug(string.Format("_onMagazinListDownloadFinished with model {0} was successful: {1}", magazinList, success));
            checkDownloadCount("_onMagazinListDownloadFinished");
        }

        private void _onMenuDownloadFinished(IList<MenuDto> menuList, bool success, string response)
        {
            _logger.Debug(string.Format("_onMenuDownloadFinished with model {0} was successful: {1}", menuList, success));
            checkDownloadCount("_onMenuDownloadFinished");
        }

        private void _onNovelListDownloadFinished(IList<NovelDto> novelList, bool success, string response)
        {
            _logger.Debug(string.Format("_onNovelListDownloadFinished with model {0} was successful: {1}", novelList, success));
            checkDownloadCount("_onNovelListDownloadFinished");
        }

        private void _onSecurityDownloadFinished(SecurityDto security, bool success, string response)
        {
            _logger.Debug(string.Format("_onSecurityDownloadFinished with model {0} was successful: {1}", security, success));
            checkDownloadCount("_onSecurityDownloadFinished");
        }

        private void _onSeriesListDownloadFinished(IList<SeriesDto> seriesList, bool success, string response)
        {
            _logger.Debug(string.Format("_onSeriesListDownloadFinished with model {0} was successful: {1}", seriesList, success));
            checkDownloadCount("_onSeriesListDownloadFinished");
        }

        private void _onShoppingListDownloadFinished(IList<ShoppingEntryDto> shoppingList, bool success, string response)
        {
            _logger.Debug(string.Format("_onShoppingListDownloadFinished with model {0} was successful: {1}", shoppingList, success));
            checkDownloadCount("_onShoppingListDownloadFinished");
        }

        private void _onSpecialicedBookListDownloadEventHandler(IList<string> bookList, bool success, string response)
        {
            _logger.Debug(string.Format("_onSpecialicedBookListDownloadEventHandler with model {0} was successful: {1}", bookList, success));
            checkDownloadCount("_onSpecialicedBookListDownloadEventHandler");
        }

        private void _scheduleDownloadFinished(IList<ScheduleDto> scheduleList, bool success, string response)
        {
            _logger.Debug(string.Format("_scheduleDownloadFinished with model {0} was successful: {1}", scheduleList, success));
            checkDownloadCount("_scheduleDownloadFinished");

            // Start download of mapcontent after downloading wirelesssockets AND schedules
            _mapContentService.OnMapContentDownloadFinished += _mapContentDownloadFinished;
            _mapContentService.LoadMapContentList();
        }

        private void _temperatureDownloadFinished(IList<TemperatureDto> temperatureList, bool success, string response)
        {
            _logger.Debug(string.Format("_temperatureDownloadFinished with model {0} was successful: {1}", temperatureList, success));
            checkDownloadCount("_temperatureDownloadFinished");
        }

        private void _wirelessSocketDownloadFinished(IList<WirelessSocketDto> wirelessSocketList, bool success, string response)
        {
            _logger.Debug(string.Format("_wirelessSocketDownloadFinished with model {0} was successful: {1}", wirelessSocketList, success));
            checkDownloadCount("_wirelessSocketDownloadFinished");

            // Start download of schedules after downloading wirelesssockets
            _scheduleService.OnScheduleDownloadFinished += _scheduleDownloadFinished;
            _scheduleService.LoadScheduleList();
        }

        private void checkDownloadCount(string objectFinished)
        {
            _downloadCount++;
            _logger.Debug(string.Format("checkDownloadCount: Download {0}/{1}", _downloadCount, Constants.DOWNLOAD_STEPS));

            _objectFinishedList.Add(objectFinished);
            _logger.Debug(string.Format("_objectFinishedList: {0}", String.Join(", ", _objectFinishedList)));

            _progressPercent = (_downloadCount * 100) / Constants.DOWNLOAD_STEPS;
            OnPropertyChanged("ProgressText");

            if (_downloadCount >= Constants.DOWNLOAD_STEPS)
            {
                Application.Current.Dispatcher.Invoke(new Action(() => { _navigationService.Navigate(new MainPage(_navigationService)); }));
            }
        }
    }
}
