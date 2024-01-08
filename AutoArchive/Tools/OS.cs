﻿using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Tools
{
    public class OS
    {
        [DllImport("shell32.dll", EntryPoint = "#261", CharSet = CharSet.Unicode)]
        public static extern void GetUserTilePath(string username,UInt32 whatever, StringBuilder picpath, int maxLength);

        public static string CreateUserTilePath(string username)
        {   
            var sb = new StringBuilder(1000);
            GetUserTilePath(null, 0x80000000, sb, sb.Capacity);
            return sb.ToString();
        }

        public static String GetCurrentUserPicturePath() {
            return Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Temp\" + Environment.UserName + ".bmp";
        }

        public static String GetCurrentUserName()
        {
            return Environment.UserName;
        }

        public static void extraIcon() {
            //Icon icon = IconUtilities.ExtractIcon("d:\\Program Files\\alipaykeytool\\支付宝开放平台开发助手.exe", IconSize.Jumbo);
            ////img.Source = icon.ToBitmap();

            //Bitmap bmp = icon.ToBitmap();
            //IntPtr hBitmap = bmp.GetHbitmap();
            //System.Windows.Media.ImageSource WpfBitmap = Imaging.CreateBitmapSourceFromHBitmap(hBitmap, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            //img.Source = WpfBitmap;
        }
    }
}
