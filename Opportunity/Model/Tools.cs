using log4net;
using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Opportunity.Model
{
    public static class Tools
    {
        static ILog log = LogManager.GetLogger("LogFileAppender");
        static DialogService dialog = new DialogService();

        public static bool ZapiszPlik(string file, string fileName, string path)
        {
            try
            {
                path = CreateAbsolutePath(path);
                System.IO.Directory.CreateDirectory(path);
                string newPath = string.Format("{0}/{1}",  path, fileName);
                bool zapis = true;
                if (File.Exists(newPath))
                    if (!dialog.ShowQuestion("Nadpisać?", "Plik istnieje"))
                        zapis = false;
                if (zapis)
                {
                    File.Copy(file, newPath, true);
                }
                return true;
                
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }

        private static string CreateAbsolutePath(string path)
        {
            try
            {
                string p = Ustawienia.MediaPath.Replace("\\", "/");
                if (p.EndsWith("/"))
                    p = p.Substring(0, p.Length - 1);
#if DEBUG
                p = "C:/Freelans/Opportunity";
#endif
                return string.Format("{0}/{1}", p, path);

            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }

        public static bool ZapiszPlikFtp(string fileName, string ftpPath, string user, string password)
        {
            try
            {
                using (WebClient client = new WebClient())
                {                    
                    client.Credentials = new NetworkCredential(user, password);
                    bool zapis = true;
                    if (File.Exists(ftpPath.Replace(Ustawienia.FtpPath, "media")))
                        if (!dialog.ShowQuestion("Nadpisać?", "Plik istnieje"))
                            zapis = false;
                    if (zapis)
                    {
                        //client.UploadFile(ftpPath, WebRequestMethods.Ftp.UploadFile, fileName);                        
                        File.Copy(fileName, ftpPath.Replace(Ustawienia.FtpPath, "media"), true);                        
                    }
                    return true;
                }
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        public static bool FtpCreateFolder(string ftpAddress, string ftpUName, string ftpPWord, string katalog)
        {
            try
            {
                FtpWebRequest reqFTP = (FtpWebRequest)FtpWebRequest.Create(ftpAddress);
                reqFTP.Method = WebRequestMethods.Ftp.MakeDirectory;
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(ftpUName, ftpPWord);
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();
                ftpStream.Close();
                response.Close();
                /*
                WebRequest ftpRequest = WebRequest.Create(string.Format("ftp://{0}/{1}", ftpAddress, katalog));
                ftpRequest.Method = WebRequestMethods.Ftp.MakeDirectory;
                ftpRequest.Credentials = new NetworkCredential(ftpUName, ftpPWord);
                using (var resp = (FtpWebResponse)ftpRequest.GetResponse())
                {
                    string s = resp.StatusCode.ToString();
                }
                */
                return true;
            }
            catch (Exception ex) { log.Error(ex); throw ex; }            
        }
        public static BitmapSource BitmapToBitmapSource(Bitmap source)
        {
            return System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                          source.GetHbitmap(),
                          IntPtr.Zero,
                          Int32Rect.Empty,
                          BitmapSizeOptions.FromEmptyOptions());
        }

        public static byte[] SHA256(string password)
        {
            var crypt = new System.Security.Cryptography.SHA256Managed();
            var hash = new System.Text.StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(password));
            return crypto;
            /*foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return Encoding.ASCII.GetBytes(hash.ToString()); */
        }
    }
}
