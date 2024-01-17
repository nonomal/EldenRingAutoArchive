﻿using AutoArchive.DataBase;
using AutoArchivePlus.Common;
using AutoArchivePlus.Forms;
using AutoArchivePlus.Language;
using AutoArchivePlus.Mapper;
using AutoArchivePlus.Model;
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;

namespace AutoArchivePlus
{
    public class App : Application
    {
        private const String PRODUCT_NAME = "AUTO_ARCHIVE";

        private static String iconPath = "Icons";

        private static MainForm mainForm = new MainForm();

        public static string ICON_PATH { get => iconPath; }

        [STAThread]
        static void Main(string[] args)
        {
            iconPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, iconPath);
            if (!Directory.Exists(iconPath))
            {
                Directory.CreateDirectory(iconPath);
            }
            dbInit();
            setLanguage(args);
            Boolean ret;
            Mutex mutex = new Mutex(true, PRODUCT_NAME, out ret);
            if (ret)
            {
                Application app = new Application();
                app.Run(mainForm);
                mutex.ReleaseMutex();
            }
            else
            {
                MessageBox.Show(LanguageManager.Instance["LaunchFailed"], LanguageManager.Instance["Tip"], MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public static Window GetMainWindow()
        {
            return mainForm;
        }

        public static void GlobalMessage(String msg, MessageTypeEnum messageType = MessageTypeEnum.INFO)
        {
            mainForm?.Message(msg, messageType);
        }

        static void setLanguage(string[] args)
        {
            var local = "--local:";
            foreach (var item in args)
            {
                if (item.Contains(local))
                {
                    var lan = item.Substring(local.Length);
                    LanguageManager.Instance.ChangeLanguage(new CultureInfo(lan));
                    break;
                }
            }
        }

        static void dbInit()
        {
            using BaseContext<Project> baseContext = new DBContext<Project>();
            var data = baseContext.Entity.ToList();
            Trace.WriteLine(data);
        }
    }
}
