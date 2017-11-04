﻿using Common.Tools;
using Data.Controller;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Timers;

namespace Data.Services
{
    public class SpecialicedBookService
    {
        public delegate void SpecialicedBookListDownloadEventHandler(IList<string> bookList, bool success, string response);
        public delegate void SpecialicedBookServiceErrorEventHandler(string error);

        private const string TAG = "SpecialicedBookService";
        private readonly Logger _logger;

        private const int TIMEOUT = 6 * 60 * 60 * 1000;

        private readonly LocalDriveController _localDriveController;

        private static SpecialicedBookService _instance = null;
        private static readonly object _padlock = new object();

        private DriveInfo _libraryDrive;
        private string _specialicedBookDir = string.Empty;
        private IList<string> _bookList = new List<string>();

        private Timer _reloadTimer;

        SpecialicedBookService()
        {
            _logger = new Logger(TAG);

            _localDriveController = new LocalDriveController();

            _libraryDrive = _localDriveController.GetLibraryDrive();
            if (_libraryDrive == null)
            {
                _logger.Error("Found no library drive!");
            }
            else
            {
                _specialicedBookDir = _libraryDrive.Name + "Books\\Sachbücher";
            }

            _reloadTimer = new Timer(TIMEOUT);
            _reloadTimer.Elapsed += _reloadTimer_Elapsed;
            _reloadTimer.AutoReset = true;
            _reloadTimer.Start();
        }

        public event SpecialicedBookListDownloadEventHandler OnSpecialicedBookListDownloadEventHandler;
        private void publishOnSpecialicedBookListDownloadEventHandler(IList<string> bookList, bool success, string response)
        {
            OnSpecialicedBookListDownloadEventHandler?.Invoke(bookList, success, response);
        }

        public event SpecialicedBookServiceErrorEventHandler OnSpecialicedBookServiceError;
        private void publishOnSpecialicedBookServiceError(string error)
        {
            OnSpecialicedBookServiceError?.Invoke(error);
        }

        public static SpecialicedBookService Instance
        {
            get
            {
                lock (_padlock)
                {
                    if (_instance == null)
                    {
                        _instance = new SpecialicedBookService();
                    }

                    return _instance;
                }
            }
        }

        public string SpecialicedBookDir
        {
            get
            {
                return _specialicedBookDir;
            }
        }

        public IList<string> BookList
        {
            get
            {
                return _bookList;
            }
        }

        public List<string> FoundBooks(string searchKey)
        {
            List<string> foundBooks = _bookList
                        .Where(entry =>
                            entry.Contains(searchKey))
                        .Select(book => book)
                        .ToList();

            return foundBooks;
        }

        public void StartReading(string title)
        {
            if (!directoryAvailable())
            {
                return;
            }

            if (title == null || title == string.Empty)
            {
                _logger.Error("Title is null or empty!");
                publishOnSpecialicedBookServiceError("Title is null or empty!");
                return;
            }

            string command = string.Format(@"{0}\{1}", _specialicedBookDir, title);
            Process.Start(command);
        }

        public void LoadBookList()
        {
            if (!directoryAvailable())
            {
                publishOnSpecialicedBookListDownloadEventHandler(null, false, "Book directory is not available!");
                return;
            }

            string[] extensionArray = new string[] { ".pdf", ".epub" };
            _bookList = _localDriveController.ReadFileNamesInDir(_specialicedBookDir, extensionArray);

            _bookList = _bookList
                .Select(fileName => fileName.Replace(_specialicedBookDir, "").Replace("\\", ""))
                .OrderBy(fileName => fileName)
                .ToArray();

            publishOnSpecialicedBookListDownloadEventHandler(_bookList, true, "Success");
        }

        private void _reloadTimer_Elapsed(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            _logger.Debug(string.Format("_reloadTimer_Elapsed with sender {0} and elapsedEventArgs {1}", sender, elapsedEventArgs));
            LoadBookList();
        }

        private bool directoryAvailable()
        {
            if (_specialicedBookDir == string.Empty)
            {
                _logger.Error("No directory for books! Trying to read again...");
                _libraryDrive = _localDriveController.GetLibraryDrive();

                if (_libraryDrive == null)
                {
                    _logger.Error("Found no library drive!");
                    publishOnSpecialicedBookServiceError("Found no book drive! Please check your attached storages!");
                    return false;
                }
                else
                {
                    _specialicedBookDir = _libraryDrive.Name + "Books\\Sachbücher";
                }
            }

            return true;
        }

        public void Dispose()
        {
            _logger.Debug("Dispose");

            _reloadTimer.Elapsed -= _reloadTimer_Elapsed;
            _reloadTimer.AutoReset = false;
            _reloadTimer.Stop();
        }
    }
}