﻿using Common.Common;
using Common.Dto;
using Common.Tools;
using Data.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Navigation;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace LucaHome.Pages
{
    public partial class CoinUpdatePage : Page, INotifyPropertyChanged
    {
        private const string TAG = "CoinUpdatePage";
        private readonly Logger _logger;

        private readonly CoinService _coinService;
        private readonly NavigationService _navigationService;

        private readonly Notifier _notifier;

        private CoinDto _updateCoin;

        public CoinUpdatePage(NavigationService navigationService, CoinDto updateCoin)
        {
            _logger = new Logger(TAG, Enables.LOGGING);

            _coinService = CoinService.Instance;
            _navigationService = navigationService;

            _updateCoin = updateCoin;

            InitializeComponent();
            DataContext = this;

            _notifier = new Notifier(cfg =>
            {
                cfg.PositionProvider = new WindowPositionProvider(
                    parentWindow: Application.Current.MainWindow,
                    corner: Corner.BottomRight,
                    offsetX: 15,
                    offsetY: 15);

                cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                    notificationLifetime: TimeSpan.FromSeconds(2),
                    maximumNotificationCount: MaximumNotificationCount.FromCount(2));

                cfg.Dispatcher = Application.Current.Dispatcher;

                cfg.DisplayOptions.TopMost = true;
                cfg.DisplayOptions.Width = 250;
            });

            _notifier.ClearMessages();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public CollectionView TypeList
        {
            get
            {
                IList<string> typeList = new List<string>();
                typeList.Add("XBT");
                typeList.Add("DASH");
                typeList.Add("ETC");
                typeList.Add("ETH");
                typeList.Add("LTC");
                typeList.Add("XMR");
                typeList.Add("ZEC");
                return new CollectionView(typeList);
            }
        }

        public string Type
        {
            get
            {
                return _updateCoin.Type;
            }
            set
            {
                _updateCoin.Type = value;
                OnPropertyChanged("Type");
            }
        }

        public double Amount
        {
            get
            {
                return _updateCoin.Amount;
            }
            set
            {
                _updateCoin.Amount = value;
                OnPropertyChanged("Amount");
            }
        }

        public string User
        {
            get
            {
                return _updateCoin.User;
            }
            set
            {
                _updateCoin.User = value;
                OnPropertyChanged("User");
            }
        }

        private void Page_Unloaded(object sender, RoutedEventArgs routedEventArgs)
        {
            _logger.Debug(string.Format("Page_Unloaded with sender {0} and routedEventArgs: {1}", sender, routedEventArgs));

            _coinService.OnCoinUpdateFinished -= _onCoinUpdateFinished;
            _coinService.OnCoinDownloadFinished -= _onCoinDownloadFinished;
        }

        private void UpdateCoin_Click(object sender, RoutedEventArgs routedEventArgs)
        {
            _logger.Debug(string.Format("UpdateCoin_Click with sender {0} and routedEventArgs: {1}", sender, routedEventArgs));
            _coinService.OnCoinUpdateFinished += _onCoinUpdateFinished;

            _logger.Debug(string.Format("Trying to update coin {0}", _updateCoin));
            _coinService.UpdateCoin(_updateCoin);
        }

        private void _onCoinUpdateFinished(bool success, string response)
        {
            _logger.Debug(string.Format("_onCoinUpdateFinished was successful {0}", success));

            _coinService.OnCoinAddFinished -= _onCoinUpdateFinished;

            if (success)
            {
                _notifier.ShowSuccess("Updated coin!");

                _coinService.OnCoinDownloadFinished += _onCoinDownloadFinished;
                _coinService.LoadCoinList();
            }
            else
            {
                _notifier.ShowError(string.Format("Updating coin failed!\n{0}", response));
            }
        }

        private void _onCoinDownloadFinished(IList<CoinDto> coinList, bool success, string response)
        {
            _logger.Debug(string.Format("_onCoinDownloadFinished with model {0} was successful {1}", coinList, success));

            _coinService.OnCoinDownloadFinished -= _onCoinDownloadFinished;
            _navigationService.GoBack();
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs routedEventArgs)
        {
            _logger.Debug(string.Format("ButtonBack_Click with sender {0} and routedEventArgs {1}", sender, routedEventArgs));
            _navigationService.GoBack();
        }
    }
}