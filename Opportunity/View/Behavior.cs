using GalaSoft.MvvmLight;
using Gat.Controls;
using Microsoft.Win32;
using Opportunity.Model;
using Opportunity.ViewModel;
using Opportunity.ViewModel.Obiekty;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media.Imaging;

namespace Opportunity.View
{
    public class AboutBoxOpen : Behavior<MenuItem>
    {        

        protected override void OnAttached()
        {
            Window parent = Application.Current.MainWindow;

            AssociatedObject.Click += (sender, e) =>
            {
                About about = new About();
                about.AdditionalNotes = "Icons made by Smashicons (https://smashicons.com) from www.flaticon.com is licensed by CC 3.0 BY";
                about.Publisher = "Luki501";
                about.Version = "1.0.1";
                about.Copyright = "";
                about.Show();
            };
        }
    }

    public class PracownikEdycjaOpen : Behavior<Button>
    {
        public bool Edycja { get; set; }

        protected override void OnAttached()
        {
            Window parent = Application.Current.MainWindow;            

            AssociatedObject.Click += (sender, e) =>
            {
                PracownikEdycjaWindow window = new PracownikEdycjaWindow();                
                window.ShowDialog();
            };
        }
    }

    public class TowarPremiumEdycjaOpen : Behavior<Button>
    {

        protected override void OnAttached()
        {
            Window parent = Application.Current.MainWindow;

            AssociatedObject.Click += (sender, e) =>
            {
                TowarPremiumEdycjaWindow window = new TowarPremiumEdycjaWindow();
                (window.DataContext as TowarEdycjaVM).Premium = true;
                window.ShowDialog();
            };
        }
    }
    public class TowarEdycjaOpen : Behavior<Button>
    {

        protected override void OnAttached()
        {
            Window parent = Application.Current.MainWindow;

            AssociatedObject.Click += (sender, e) =>
            {
                TowarEdycjaWindow window = new TowarEdycjaWindow();
                (window.DataContext as TowarEdycjaVM).Premium = false;
                window.ShowDialog();
            };
        }
    }
    public class PasswordEdycjaOpen : Behavior<Button>
    {        
        public bool NowyUzytkownik { get; set; }
        public bool EdycjaPracownika { get; set; }
        protected override void OnAttached()
        {            
            AssociatedObject.Click += (sender, e) =>
            {
                Window parent = Window.GetWindow((sender as Button));
                if (parent is PracownikEdycjaWindow)
                {
                    PracownikEdycjaVM vmPracownik = parent.DataContext as PracownikEdycjaVM;
                    if (vmPracownik.ValidateBrygadzista())
                    {
                        PasswordBoxWindow window = new PasswordBoxWindow();
                        PasswordBoxVM vm = window.DataContext as PasswordBoxVM;
                        vm.Login = vmPracownik.NowyPracownik.Numer;
                        vm.NowyUzytkownik = NowyUzytkownik;
                        window.ShowDialog();
                        if (vm.IsHasloNadane)
                            vmPracownik.NowyPracownik.Login = vm.Login;
                    }
                }
                else if (parent is MainWindow)
                {
                    MainViewModel mvm = parent.DataContext as MainViewModel;

                    PasswordBoxWindow window = new PasswordBoxWindow();
                    PasswordBoxVM vm = window.DataContext as PasswordBoxVM;
                    if (EdycjaPracownika)
                        vm.Login = mvm.PracownikWybrany.Numer;
                    else
                        vm.Login = mvm.User;
                    vm.NowyUzytkownik = NowyUzytkownik;
                    window.ShowDialog();
                    if (vm.IsHasloNadane)
                    {
                        if (EdycjaPracownika)
                        {
                            mvm.PracownikWybrany.Brygadzista = true;
                            mvm.PracownikWybrany.Save();
                        }
                        if (mvm.Uzytkownik != null && NowyUzytkownik)
                        {
                            mvm.Uzytkownik.Login = vm.Login;
                            mvm.Uzytkownik.Save();
                        }
                    }
                }
            };
        }
    }
    public class PictureOpen : Behavior<Image>
    {
        public bool Edycja { get; set; }

        protected override void OnAttached()
        {
            Window parent = Application.Current.MainWindow;

            AssociatedObject.MouseDown += (sender, e) =>
            {
                WindowPicture window = new WindowPicture();
                window.DataContext = new PictureVM((parent.DataContext as MainViewModel).TowarWybrany.BitmapZdjecie);
                //(window.DataContext as PictureVM).SourcePicture = (parent.DataContext as MainViewModel).TowarWybrany.BitmapZdjecie;
                window.ShowDialog();
            };
        }
    }
    public class OtworzKatalog : Behavior<Button>
    {
        public bool Edycja { get; set; }

        protected override void OnAttached()
        {
            Window parent = Application.Current.MainWindow;

            AssociatedObject.Click += (sender, e) =>
            {
                TowarVM towar = (parent.DataContext as MainViewModel).TowarWybrany;
                string path;
                if (towar.Premium)
                    path = string.Format("{0}/media/Narzedzia/{1}", AppDomain.CurrentDomain.BaseDirectory.Replace("\\", "/"), towar.Towar.Katalog);
                else
                    path = string.Format("{0}/media/Magazyn/{1}", AppDomain.CurrentDomain.BaseDirectory.Replace("\\", "/"), towar.Towar.Katalog);
                System.IO.Directory.CreateDirectory(path);
                Process.Start(path);
            };
        }
    }
    public class ZamknijOkno : Behavior<Button>
    {
        public bool Edycja { get; set; }

        protected override void OnAttached()
        {
            Window parent = Window.GetWindow(AssociatedObject);

            AssociatedObject.Click += (sender, e) =>
            {
                parent.Close();                                
            };
        }
    }

    public class ZamykanieOkna : Behavior<Window>
    {
        public bool Edycja { get; set; }

        protected override void OnAttached()
        {            
            AssociatedObject.Closed += (sender, e) =>
            {
                Window parent = Window.GetWindow(AssociatedObject);
                Window main = Application.Current.MainWindow;
                if (parent is PracownikEdycjaWindow && (parent.DataContext as PracownikEdycjaVM).PracownikZapisany)
                {
                    (main.DataContext as MainViewModel).OdswiezListePracownikow();
                    (parent.DataContext as PracownikEdycjaVM).NowyPracownik = new Model.Obiekty.Pracownik();
                }
                else if ((parent is TowarPremiumEdycjaWindow || parent is TowarEdycjaWindow) && (parent.DataContext as TowarEdycjaVM).IsTowarZapisany)
                {
                    if ((parent.DataContext as TowarEdycjaVM).NowyTowar.Premium)
                        (main.DataContext as MainViewModel).OdswiezListeTowarow();
                    else
                        (main.DataContext as MainViewModel).OdswiezListeMagazyn();
                    (parent.DataContext as TowarEdycjaVM).NowyTowar = new Model.Obiekty.Towar();
                }
                // do wykorzystania
                else if (parent is PasswordBoxWindow && (parent.DataContext as PasswordBoxVM).IsHasloNadane)
                {

                }
            };
        }
    }
    public class DodajPlik : Behavior<Button>
    {
        public string Field { get; set; }
        DialogService dialog = new DialogService();
        protected override void OnAttached()
        {            
            AssociatedObject.Click += (sender, e) =>
            {
                try
                {
                    OpenFileDialog open = new OpenFileDialog();
                    if (open.ShowDialog() == true)
                    {
                        Window parent = Window.GetWindow(AssociatedObject);

                        if (parent is TowarPremiumEdycjaWindow && Field.Equals("path_zdjecie"))
                        {
                            string katalog = (parent.DataContext as TowarEdycjaVM).NowyTowar.Katalog;                            
                            string path = string.Format("Media/{0}/{1}", "Narzedzia", katalog);
                            string fileName = Path.GetFileName(open.FileName);
                            //string ftpPath = string.Format("{0}/{1}/{2}", Ustawienia.FtpPath, katalog, path);
                            //Tools.FtpCreateFolder(Ustawienia.FtpPath, Ustawienia.FtpUser, Ustawienia.FtpPassword, katalog);
                            //if (Tools.ZapiszPlikFtp(open.FileName, ftpPath, Ustawienia.FtpUser, Ustawienia.FtpPassword))
                            if (Tools.ZapiszPlik(open.FileName, fileName, path))
                                (parent.DataContext as TowarEdycjaVM).NowyTowar.PathZdjecie = string.Format("{0}/{1}", path, fileName); ;
                        }
                        else if (parent is TowarPremiumEdycjaWindow && Field.Equals("path_gwarancja"))
                        {
                            string katalog = (parent.DataContext as TowarEdycjaVM).NowyTowar.Katalog;
                            string path = string.Format("Media/{0}/{1}", "Narzedzia", katalog);
                            string fileName = Path.GetFileName(open.FileName);
                            if (Tools.ZapiszPlik(open.FileName, fileName, path))                                
                                (parent.DataContext as TowarEdycjaVM).NowyTowar.PathGwarancja = string.Format("{0}/{1}", path, fileName);
                        }
                        else if (parent is MainWindow && Field.Equals("path_zdjecie"))
                        {
                            string katalog = (parent.DataContext as MainViewModel).TowarWybrany.Towar.Katalog;
                            string path = string.Format("Media/{0}/{1}", "Narzedzia", katalog);
                            string fileName = Path.GetFileName(open.FileName);
                            if (Tools.ZapiszPlik(open.FileName, fileName, path))
                                (parent.DataContext as MainViewModel).TowarWybrany.PathZdjecie = string.Format("{0}/{1}", path, fileName); ;
                        }
                        else if (parent is MainWindow && Field.Equals("path_gwarancja"))
                        {
                            string katalog = (parent.DataContext as MainViewModel).TowarWybrany.Towar.Katalog;
                            string path = string.Format("Media/{0}/{1}", "Narzedzia", katalog);
                            string fileName = Path.GetFileName(open.FileName);
                            if (Tools.ZapiszPlik(open.FileName, fileName, path))
                                (parent.DataContext as MainViewModel).TowarWybrany.PathGwarancja = string.Format("{0}/{1}", path, fileName); ;
                        }
                        else if (parent is MainWindow && Field.Equals("path_towar_przyjecie_nowe"))
                        {
                            string katalog = (parent.DataContext as MainViewModel).TowarWybrany.Towar.Katalog;
                            string path = string.Format("Media/{0}/{1}", "Narzedzia", katalog);
                            string fileName = Path.GetFileName(open.FileName);
                            if (Tools.ZapiszPlik(open.FileName, fileName, path))
                                (parent.DataContext as MainViewModel).TowarWybrany.PathPrzyjecie = string.Format("{0}/{1}", path, fileName); ;
                        }
                        else if (parent is MainWindow && Field.Equals("path_towar_wycofanie_nowe"))
                        {
                            string katalog = (parent.DataContext as MainViewModel).TowarWybrany.Towar.Katalog;
                            string path = string.Format("Media/{0}/{1}", "Narzedzia", katalog);
                            string fileName = Path.GetFileName(open.FileName);
                            if (Tools.ZapiszPlik(open.FileName, fileName, path))
                                (parent.DataContext as MainViewModel).TowarWybrany.PathWycofanie = string.Format("{0}/{1}", path, fileName); ;
                        }

                        if (parent is MainWindow)
                        {
                            (parent.DataContext as MainViewModel).OdswiezListeTowarow();
                        }
                    }
                }
                catch (Exception ex) { dialog.ShowError(ex); }
            };
        }
    }
    public class DodajPliki : Behavior<Button>
    {
        public string Field { get; set; }
        DialogService dialog = new DialogService();
        protected override void OnAttached()
        {
            AssociatedObject.Click += (sender, e) =>
            {
                try
                {
                    OpenFileDialog open = new OpenFileDialog();
                    open.Multiselect = true;
                    if (open.ShowDialog() == true)
                    {
                        Window parent = Window.GetWindow(AssociatedObject);
                        TowarVM towar = (parent.DataContext as MainViewModel).TowarWybrany;
                        string katalog = towar.Towar.Katalog;
                        string path;
                        if (towar.Premium)
                            path = string.Format("Media/{0}/{1}", "Narzedzia", katalog);
                        else
                            path = string.Format("Media/{0}/{1}", "Magazyn", katalog);
                        foreach (string file in open.FileNames)
                        {
                            string fileName = Path.GetFileName(file);
                            Tools.ZapiszPlik(open.FileName, fileName, path);
                        }                           
                    }
                }
                catch (Exception ex) { dialog.ShowError(ex); }
            };
        }
    }
    /*
    public class ZapiszTowar : Behavior<Button>
    {
        DialogService dialog = new DialogService();
        protected override void OnAttached()
        {
            AssociatedObject.Click += (sender, e) =>
            {
                try
                {
                    Window parent = Window.GetWindow(AssociatedObject);
                    (parent.DataContext as TowarEdycjaVM).ZapiszTowar();
                }
                catch (Exception ex) { dialog.ShowError(ex); }
            };
        }
    }
    */

}
