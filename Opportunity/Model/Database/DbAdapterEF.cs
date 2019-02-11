using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using Opportunity.Model.Obiekty;
using Opportunity.ViewModel.Obiekty;

namespace Opportunity.Model.Database
{
    public static class DbAdapterEF
    {
        public static string User = "user";
        public static string Password = "password";
        private static string DataSource = Ustawienia.DataSource;
        private static ILog log = LogManager.GetLogger("LogFileAppender");
        private static string InitialCatalog = "Opportunity";
        public static string Conn { get { return GetConnectionString(); } }
        private static string GetConnectionString()
        {
            try
            {
                SqlConnectionStringBuilder sqlString = new SqlConnectionStringBuilder()
                {
                    DataSource = Ustawienia.DataSource,
                    InitialCatalog = "Opportunity",
                    UserID = User,
                    Password = Password
                };
                EntityConnectionStringBuilder entityString = new EntityConnectionStringBuilder()
                {
                    Provider = "System.Data.SqlClient",
                    Metadata = "res://*/Model.Database.ModelOpportunity.csdl|res://*/Model.Database.ModelOpportunity.ssdl|res://*/Model.Database.ModelOpportunity.msl",
                    ProviderConnectionString = sqlString.ToString()
                };
                return entityString.ConnectionString;
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }

        #region all
        public static void DataBaseUpdate()
        {
            try
            {                
                using (OpportunityEntities context = new OpportunityEntities(Conn))
                {                    
                    context.SaveChanges();
                }                
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }

        internal static bool CheckStatus()
        {
            try
            {                
                using (OpportunityEntities context = new OpportunityEntities(Conn))
                {
                    return context.Database.Exists();
                }
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        internal static bool ZmianaHasla(string numer, string noweHaslo, string stareHaslo)
        {
            try
            {
                using (OpportunityEntities context = new OpportunityEntities(Conn))
                {                    
                    string sql = string.Format("ALTER LOGIN {0} WITH PASSWORD='{1}' OLD_PASSWORD='{2}'", numer, noweHaslo, stareHaslo);
                    context.Database.ExecuteSqlCommand(sql);                 
                    return true;
                }
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }        
        
        internal static bool DodajUzytkownika(string numer, string haslo)
        {
            try
            {
                using (OpportunityEntities context = new OpportunityEntities(Conn))
                {                    
                    string sql = "CREATE LOGIN " + numer + "\n";
                    sql += "WITH PASSWORD = N'" + haslo + "',\n";
                    sql += "CHECK_POLICY = OFF,\n";
                    sql += "CHECK_EXPIRATION = OFF;";
                    context.Database.ExecuteSqlCommand(sql);
                    sql = "EXEC sp_addsrvrolemember\n";
                    sql += "@loginame = N'" + numer + "', \n";
                    sql += "@rolename = N'sysadmin';";
                    context.Database.ExecuteSqlCommand(sql);
                    return true;
                }
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        internal static Pracownik GetUzytkownika(string user)
        {
            try
            {
                using (OpportunityEntities context = new OpportunityEntities(Conn))
                {
                    if (context.Pracownicy.Any(p => p.login.Equals(user)))
                        return new Pracownik(context.Pracownicy.Where(p => p.login.Equals(user)).First());
                    else
                        return null;
                }
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        #endregion

        #region pracownicy
        public static List<Pracownik> GetListaPracownikow()
        {
            try
            {
                List<Pracownik> lista = new List<Pracownik>();
                using (OpportunityEntities context = new OpportunityEntities(Conn))
                {
                    List<Pracownicy> pracownicy = context.Pracownicy.ToList();
                    foreach (Pracownicy p in pracownicy)
                    {
                        Pracownik pracownik = new Pracownik(p);
                        lista.Add(pracownik);
                    }                    
                }

                return lista;
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }

        public static void PracownikInsert(Pracownik p)
        {
            try
            {
                using (OpportunityEntities context = new OpportunityEntities(Conn))
                {
                    if (p.Login != null && context.Pracownicy.AsEnumerable().Any(pr => pr.login.Equals(p.Login)))
                        throw new Exception("Podany login jest zajęty");
                    if (context.Pracownicy.AsEnumerable().Any(pr => pr.numer.Equals(p.Numer)))
                        throw new Exception("Podany numer jest zajęty");                                      
                    context.Pracownicy.Add(p.PracownikDb);
                    p.PracownikDb.last_update = DateTime.Now;
                    p.PracownikDb.uzytkownik = Ustawienia.NazwaUzytkownika;
                    context.SaveChanges();
                }
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }

        public static void PracownikRemove(Pracownik p)
        {
            try
            {
                using (OpportunityEntities context = new OpportunityEntities(Conn))
                {
                    p.PracownikDb.last_update = DateTime.Now;
                    p.PracownikDb.nieaktywny = true;
                    p.PracownikDb.uzytkownik = Ustawienia.NazwaUzytkownika;
                    context.Entry(p.PracownikDb).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }

        public static void PracownikUpdate(Pracownik p)
        {
            try
            {
                using (OpportunityEntities context = new OpportunityEntities(Conn))
                {
                    p.PracownikDb.last_update = DateTime.Now;
                    context.Entry(p.PracownikDb).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        internal static Pracownik GetPracownikaByTowar(int idTowaru)
        {
            try
            {
                using (OpportunityEntities context = new OpportunityEntities(Conn))
                {
                    int idPracownika = (int)context.Towary.Where(t => t.id == idTowaru).First().Wydania.Where(w => !w.nieaktywne).OrderByDescending(w => w.data).Select(w => w.id_przyjmujacego).First();
                    Pracownicy pracownik = context.Pracownicy.Where(p => p.id == idPracownika).FirstOrDefault();
                    if (pracownik != null)
                        return new Pracownik(pracownik);
                    else
                        return null;
                }
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }

        #endregion

        #region zezwolenie
        internal static void ZezwolenieInsert(Zezwolenie zezwolenie)
        {
            try
            {
                using (OpportunityEntities context = new OpportunityEntities(Conn))
                {
                    zezwolenie.ZezwolenieDb.last_update = DateTime.Now;
                    zezwolenie.ZezwolenieDb.uzytkownik = Ustawienia.NazwaUzytkownika;
                    context.Zezwolenia.Add(zezwolenie.ZezwolenieDb);
                    context.SaveChanges();
                }
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }

        internal static void ZezwolenieUpdate(Zezwolenie zezwolenie)
        {
            try
            {
                using (OpportunityEntities context = new OpportunityEntities(Conn))
                {
                    zezwolenie.ZezwolenieDb.last_update = DateTime.Now;
                    context.Entry(zezwolenie.ZezwolenieDb).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }

        internal static void ZezwolenieRemove(Zezwolenie zezwolenie)
        {
            try
            {
                using (OpportunityEntities context = new OpportunityEntities(Conn))
                {
                    zezwolenie.ZezwolenieDb.nieaktywne = true;
                    zezwolenie.ZezwolenieDb.last_update = DateTime.Now;
                    zezwolenie.ZezwolenieDb.uzytkownik = Ustawienia.NazwaUzytkownika;
                    context.Entry(zezwolenie.ZezwolenieDb).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }

        #endregion

        #region wiza
        internal static void WizaUpdate(Wiza wiza)
        {
            try
            {
                using (OpportunityEntities context = new OpportunityEntities(Conn))
                {
                    wiza.WizaDb.last_update = DateTime.Now;
                    context.Entry(wiza.WizaDb).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }

        internal static void WizaInsert(Wiza wiza)
        {
            try
            {
                using (OpportunityEntities context = new OpportunityEntities(Conn))
                {
                    wiza.WizaDb.last_update = DateTime.Now;
                    wiza.WizaDb.uzytkownik = Ustawienia.NazwaUzytkownika;
                    context.Wizy.Add(wiza.WizaDb);
                    context.SaveChanges();
                }
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        internal static void WizaRemove(Wiza wiza)
        {
            try
            {
                using (OpportunityEntities context = new OpportunityEntities(Conn))
                {
                    wiza.WizaDb.nieaktywna = true;
                    wiza.WizaDb.last_update = DateTime.Now;
                    wiza.WizaDb.uzytkownik = Ustawienia.NazwaUzytkownika;
                    context.Entry(wiza.WizaDb).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        #endregion

        #region zaproszenie
        internal static void ZameldowanieInsert(Zameldowanie zameldowanie)
        {
            try
            {
                using (OpportunityEntities context = new OpportunityEntities(Conn))
                {
                    zameldowanie.ZaproszenieDb.last_update = DateTime.Now;
                    zameldowanie.ZaproszenieDb.uzytkownik = Ustawienia.NazwaUzytkownika;
                    context.Zaproszenia.Add(zameldowanie.ZaproszenieDb);
                    context.SaveChanges();
                }
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        internal static void ZameldowanieUpdate(Zameldowanie zameldowanie)
        {
            try
            {
                using (OpportunityEntities context = new OpportunityEntities(Conn))
                {
                    zameldowanie.ZaproszenieDb.last_update = DateTime.Now;
                    context.Entry(zameldowanie.ZaproszenieDb).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        internal static void ZameldowanieeRemove(Zameldowanie zamledowanie)
        {
            try
            {
                using (OpportunityEntities context = new OpportunityEntities(Conn))
                {
                    zamledowanie.ZaproszenieDb.nieaktywne = true;
                    zamledowanie.ZaproszenieDb.last_update = DateTime.Now;
                    zamledowanie.ZaproszenieDb.uzytkownik = Ustawienia.NazwaUzytkownika;
                    context.Entry(zamledowanie.ZaproszenieDb).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        #endregion

        #region szkolenie
        internal static void SzkolenieBHPInsert(SzkolenieBHP szkolenieBHP)
        {
            try
            {
                using (OpportunityEntities context = new OpportunityEntities(Conn))
                {
                    szkolenieBHP.SzkolenieDb.last_update = DateTime.Now;
                    szkolenieBHP.SzkolenieDb.uzytkownik = Ustawienia.NazwaUzytkownika;
                    context.SzkoleniaBHP.Add(szkolenieBHP.SzkolenieDb);
                    context.SaveChanges();
                }
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        internal static void SzkolenieBHPUpdate(SzkolenieBHP szkolenieBHP)
        {
            try
            {
                using (OpportunityEntities context = new OpportunityEntities(Conn))
                {
                    szkolenieBHP.SzkolenieDb.last_update = DateTime.Now;
                    context.Entry(szkolenieBHP.SzkolenieDb).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        internal static void SzkolenieBHPRemove(SzkolenieBHP szkolenieBHP)
        {
            try
            {
                using (OpportunityEntities context = new OpportunityEntities(Conn))
                {
                    szkolenieBHP.SzkolenieDb.last_update = DateTime.Now;
                    szkolenieBHP.SzkolenieDb.uzytkownik = Ustawienia.NazwaUzytkownika;
                    szkolenieBHP.SzkolenieDb.nieaktywne = true;
                    context.Entry(szkolenieBHP.SzkolenieDb).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        #endregion

        #region paszport
        internal static void PaszportUpdate(Paszport paszport)
        {
            try
            {
                using (OpportunityEntities context = new OpportunityEntities(Conn))
                {
                    paszport.PaszportDb.last_update = DateTime.Now;                             
                    context.Entry(paszport.PaszportDb).State = EntityState.Modified;                    
                    context.SaveChanges();
                }
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        internal static void PaszportInsert(Paszport paszport)
        {
            try
            {
                using (OpportunityEntities context = new OpportunityEntities(Conn))
                {
                    paszport.PaszportDb.last_update = DateTime.Now;
                    paszport.PaszportDb.uzytkownik = Ustawienia.NazwaUzytkownika;
                    context.Paszporty.Add(paszport.PaszportDb);
                    context.SaveChanges();
                }
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        internal static void PaszportRemove(Paszport paszport)
        {
            try
            {
                using (OpportunityEntities context = new OpportunityEntities(Conn))
                {
                    paszport.PaszportDb.last_update = DateTime.Now;
                    paszport.PaszportDb.nieaktywny = true;
                    paszport.PaszportDb.uzytkownik = Ustawienia.NazwaUzytkownika;
                    context.Entry(paszport.PaszportDb).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        #endregion

        #region oświadczenie
        internal static void OswiadczenieInsert(Oswiadczenie oswiadczenie)
        {
            try
            {
                using (OpportunityEntities context = new OpportunityEntities(Conn))
                {
                    oswiadczenie.OswiadczenieDb.last_update = DateTime.Now;
                    oswiadczenie.OswiadczenieDb.uzytkownik = Ustawienia.NazwaUzytkownika;
                    context.Oswiadczenia.Add(oswiadczenie.OswiadczenieDb);
                    context.SaveChanges();
                }
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }

        internal static void OswiadczenieUpdate(Oswiadczenie oswiadczenie)
        {
            try
            {
                using (OpportunityEntities context = new OpportunityEntities(Conn))
                {
                    oswiadczenie.OswiadczenieDb.last_update = DateTime.Now;
                    context.Entry(oswiadczenie.OswiadczenieDb).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        internal static void OswiadczenieRemove(Oswiadczenie oswiadczenie)
        {
            try
            {
                using (OpportunityEntities context = new OpportunityEntities(Conn))
                {
                    oswiadczenie.OswiadczenieDb.last_update = DateTime.Now;
                    oswiadczenie.OswiadczenieDb.nieaktywne = true;
                    oswiadczenie.OswiadczenieDb.uzytkownik = Ustawienia.NazwaUzytkownika;
                    context.Entry(oswiadczenie.OswiadczenieDb).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        #endregion

        #region badanie
        internal static void BadanieInsert(Badanie badanie)
        {
            try
            {
                using (OpportunityEntities context = new OpportunityEntities(Conn))
                {
                    badanie.BadanieDb.last_update = DateTime.Now;
                    badanie.BadanieDb.uzytkownik = Ustawienia.NazwaUzytkownika;
                    context.Badania.Add(badanie.BadanieDb);
                    context.SaveChanges();
                }
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        internal static void BadanieUpdate(Badanie badanie)
        {
            try
            {
                using (OpportunityEntities context = new OpportunityEntities(Conn))
                {                    
                    badanie.BadanieDb.last_update = DateTime.Now;
                    context.Entry(badanie.BadanieDb).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        internal static void BadanieRemove(Badanie badanie)
        {
            try
            {
                using (OpportunityEntities context = new OpportunityEntities(Conn))
                {
                    badanie.BadanieDb.nieaktywne = true;
                    badanie.BadanieDb.last_update = DateTime.Now;
                    badanie.BadanieDb.uzytkownik = Ustawienia.NazwaUzytkownika;
                    context.Entry(badanie.BadanieDb).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        #endregion

        #region towar
        internal static Towar GetTowar(int idTowaru)
        {
            try
            {
                using (OpportunityEntities context = new OpportunityEntities(Conn))
                {
                    if (idTowaru > 0)
                    {
                        Towary towar = context.Towary.Where(t => t.id == idTowaru).First();
                        return new Towar(towar);
                    }
                    else
                        return null;

                }
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        internal static List<WydanieView> GetListaWydan()
        {
            try
            {
                using (OpportunityEntities context = new OpportunityEntities(Conn))
                {
                    List<WydanieView> lista = new List<WydanieView>();                    
                    foreach (v_wydania w in context.v_wydania)
                        lista.Add(new WydanieView(w));
                    return lista;
                }
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        internal static List<WydanieView> GetListaWydan(int idTowaru)
        {
            try
            {
                using (OpportunityEntities context = new OpportunityEntities(Conn))
                {
                    List<WydanieView> lista = new List<WydanieView>();
                    IQueryable<v_wydania> wydania = context.v_wydania.Where(w => w.id_towaru == idTowaru);                    
                    foreach (v_wydania w in wydania)
                        lista.Add(new WydanieView(w));
                    return lista;
                }
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }

        internal static List<Wycofanie> GetListaWycofan(int idTowaru)
        {
            try
            {
                using (OpportunityEntities context = new OpportunityEntities(Conn))
                {
                    List<Wycofanie> lista = new List<Wycofanie>();
                    IQueryable<Wycofania> wycofania = context.Wycofania.Where(w => w.id_towaru == idTowaru && !w.nieaktywne);
                    foreach (Wycofania w in wycofania)
                        lista.Add(new Wycofanie(w));
                    return lista;
                }
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }

        internal static List<Przyjecie> GetListaPrzyjec(int idTowaru)
        {
            try
            {
                using (OpportunityEntities context = new OpportunityEntities(Conn))
                {
                    List<Przyjecie> lista = new List<Przyjecie>();
                    IQueryable<Przyjecia> przyjecia = context.Przyjecia.Where(w => w.id_towaru == idTowaru && !w.nieaktywne);
                    foreach (Przyjecia w in przyjecia)
                        lista.Add(new Przyjecie(w));
                    return lista;
                }
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        internal static string GetNazwaGrupyTowarow(int? idGrupy)
        {
            try
            {
                if (idGrupy != null && idGrupy > 0)
                {
                    using (OpportunityEntities context = new OpportunityEntities(Conn))
                    {
                        if (idGrupy == null)
                            return "";
                        return context.Slownik_typy_towarow.Where(s => s.id == idGrupy).Select(s => s.nazwa).First();
                    }
                }
                else
                    return "";
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        internal static int GetIdGrupyTowarow() // dla domyślnego typu towarów nie premium
        {
            try
            {
                using (OpportunityEntities context = new OpportunityEntities(Conn))
                {
                    if (context.Slownik_typy_towarow.Count() > 0)
                        return context.Slownik_typy_towarow.OrderBy(s => s.id).Select(s => s.id).FirstOrDefault();
                    else
                        return 0;
                }

            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        internal static int GetIdGrupyTowarow(string nazwa)
        {
            try
            {
                if (!nazwa.Equals(""))
                {
                    using (OpportunityEntities context = new OpportunityEntities(Conn))
                    {                        
                        return context.Slownik_typy_towarow.Where(s => s.nazwa == nazwa).Select(s => s.id).First();
                    }
                }
                else
                    return 0;
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }

        internal static List<TypTowaru> GetListaTypowTowarow()
        {
            try
            {                
                using (OpportunityEntities context = new OpportunityEntities(Conn))
                {
                    IQueryable<Slownik_typy_towarow> lista = context.Slownik_typy_towarow.Where(t => !t.nieaktywna);
                    if (lista.Count() > 0)
                    {
                        List<TypTowaru> listaTypow = new List<TypTowaru>();
                        foreach (Slownik_typy_towarow typ in lista)
                        {
                            listaTypow.Add(new TypTowaru(typ));
                        }
                        return listaTypow;
                    }
                    else
                        return new List<TypTowaru>();
                }
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        internal static List<MarkaTowaru> GetListaMarekTowarow()
        {
            try
            {
                using (OpportunityEntities context = new OpportunityEntities(Conn))
                {
                    IQueryable<Slownik_marki> lista = context.Slownik_marki.Where(t => !t.nieaktywna);
                    if (lista.Count() > 0)
                    {
                        List<MarkaTowaru> listaMarek = new List<MarkaTowaru>();
                        foreach (Slownik_marki marka in lista)
                        {
                            listaMarek.Add(new MarkaTowaru(marka));
                        }
                        return listaMarek;
                    }
                    else
                        return new List<MarkaTowaru>();
                }
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        internal static string GetNazwaMarkiTowaru(int? idMarki)
        {
            try
            {
                if (idMarki != null && idMarki > 0)
                {
                    using (OpportunityEntities context = new OpportunityEntities(Conn))
                    {
                        if (idMarki == null)
                            return "";
                        return context.Slownik_marki.Where(s => s.id == idMarki).Select(s => s.nazwa).First();
                    }
                }
                else
                    return "";
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        internal static int GetIdMarkiTowarow(string nazwa)
        {
            try
            {
                if (!nazwa.Equals(""))
                {
                    using (OpportunityEntities context = new OpportunityEntities(Conn))
                    {
                        return context.Slownik_marki.Where(s => s.nazwa == nazwa).Select(s => s.id).First();
                    }
                }
                else
                    return 0;
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }

        internal static void TowarInsert(Towar towar)
        {
            try
            {
                using (OpportunityEntities context = new OpportunityEntities(Conn))
                {
                    if (towar.IdTypu == 0)
                        towar.TowarDb.id_typu = GetIdGrupyTowarow();
                    towar.TowarDb.last_update = DateTime.Now;
                    towar.TowarDb.uzytkownik = Ustawienia.NazwaUzytkownika;
                    context.Towary.Add(towar.TowarDb);
                    context.SaveChanges();
                }
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }

        internal static void TowarUpdate(Towar towar)
        {
            try
            {
                using (OpportunityEntities context = new OpportunityEntities(Conn))
                {
                    if (towar.IdTypu == 0)
                        towar.TowarDb.id_typu = GetIdGrupyTowarow();
                    towar.TowarDb.last_update = DateTime.Now;                    
                    context.Entry(towar.TowarDb).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        internal static void TowarRemove(Towar towar)
        {
            try
            {
                using (OpportunityEntities context = new OpportunityEntities(Conn))
                {
                    towar.TowarDb.nieaktywny = true;
                    towar.TowarDb.last_update = DateTime.Now;
                    towar.TowarDb.uzytkownik = Ustawienia.NazwaUzytkownika;
                    context.Entry(towar.TowarDb).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        internal static List<Towar> GetListaTowarow()
        {
            try
            {
                List<Towar> lista = new List<Towar>();
                using (OpportunityEntities context = new OpportunityEntities(Conn))
                {
                    List<Towary> towary = context.Towary.ToList();
                    foreach (Towary t in towary.Where(t => !t.nieaktywny))
                    {
                        Towar towar = new Towar(t);
                        lista.Add(towar);
                    }
                    return lista;
                }
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        internal static void TypTowaruInsert(string nazwa)
        {
            try
            {
                using (OpportunityEntities context = new OpportunityEntities(Conn))
                {
                    Slownik_typy_towarow towar = new Slownik_typy_towarow();
                    towar.nazwa = nazwa;
                    towar.last_update = DateTime.Now;
                    towar.uzytkownik = Ustawienia.NazwaUzytkownika;
                    context.Slownik_typy_towarow.Add(towar);
                    context.SaveChanges();
                }
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        internal static void TypTowaruUpdate(Slownik_typy_towarow towar)
        {
            try
            {
                using (OpportunityEntities context = new OpportunityEntities(Conn))
                {                                       
                    towar.last_update = DateTime.Now;                    
                    context.Entry(towar).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        internal static void TypTowarRemove(Slownik_typy_towarow towar)
        {
            try
            {
                using (OpportunityEntities context = new OpportunityEntities(Conn))
                {
                    /*towar.nieaktywna = true;
                    towar.last_update = DateTime.Now;
                    context.Entry(towar).State = EntityState.Modified;*/
                    towar.uzytkownik = Ustawienia.NazwaUzytkownika;
                    context.Slownik_typy_towarow.Attach(towar);
                    context.Slownik_typy_towarow.Remove(towar);
                    context.SaveChanges();
                }
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }

        internal static void MarkaTowaruInsert(string nazwa)
        {
            try
            {
                using (OpportunityEntities context = new OpportunityEntities(Conn))
                {
                    Slownik_marki marka = new Slownik_marki();
                    marka.nazwa = nazwa;
                    marka.last_update = DateTime.Now;
                    marka.uzytkownik = Ustawienia.NazwaUzytkownika;
                    context.Slownik_marki.Add(marka);
                    context.SaveChanges();
                }
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        internal static void MarkaTowaruUpdate(Slownik_marki marka)
        {
            try
            {
                using (OpportunityEntities context = new OpportunityEntities(Conn))
                {
                    marka.last_update = DateTime.Now;
                    context.Entry(marka).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        internal static void MarkaTowarRemove(Slownik_marki marka)
        {
            try
            {
                using (OpportunityEntities context = new OpportunityEntities(Conn))
                {
                    /*towar.nieaktywna = true;
                    towar.last_update = DateTime.Now;
                    context.Entry(towar).State = EntityState.Modified;*/
                    marka.uzytkownik = Ustawienia.NazwaUzytkownika;
                    context.Slownik_marki.Attach(marka);
                    context.Slownik_marki.Remove(marka);
                    context.SaveChanges();
                }
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        internal static TypTowaru GetTypTowaru(string nazwa)
        {
            try
            {
                using (OpportunityEntities context = new OpportunityEntities(Conn))
                {
                    return new TypTowaru(context.Slownik_typy_towarow.Where(t => t.nazwa.Equals(nazwa)).FirstOrDefault());
                }
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        internal static Slownik_typy_towarow GetSlownikTypyTowarow(TypTowaru typ)
        {
            try
            {
                using (OpportunityEntities context = new OpportunityEntities(Conn))
                {
                    return context.Slownik_typy_towarow.Where(t => t.id == typ.Id).FirstOrDefault();
                }
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        #endregion

        #region potwierdzenie
        internal static void PotwierdzenieInsert(Potwierdzenie potwierdzenie)
        {
            try
            {
                using (OpportunityEntities context = new OpportunityEntities(Conn))
                {
                    potwierdzenie.PotwierdzenieDb.last_update = DateTime.Now;
                    potwierdzenie.PotwierdzenieDb.uzytkownik = Ustawienia.NazwaUzytkownika;
                    context.Potwierdzenia.Add(potwierdzenie.PotwierdzenieDb);
                    context.SaveChanges();
                }
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        internal static void PotwierdzenieUpdate(Potwierdzenie potwierdzenie)
        {
            try
            {
                using (OpportunityEntities context = new OpportunityEntities(Conn))
                {
                    potwierdzenie.PotwierdzenieDb.last_update = DateTime.Now;
                    context.Entry(potwierdzenie.PotwierdzenieDb).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        internal static void PotwierdzenieRemove(Potwierdzenie potwierdzenie)
        {
            try
            {
                using (OpportunityEntities context = new OpportunityEntities(Conn))
                {
                    potwierdzenie.PotwierdzenieDb.nieaktywne = true;
                    potwierdzenie.PotwierdzenieDb.last_update = DateTime.Now;
                    potwierdzenie.PotwierdzenieDb.uzytkownik = Ustawienia.NazwaUzytkownika;
                    context.Entry(potwierdzenie.PotwierdzenieDb).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        #endregion

        #region przyjecia
        internal static void PrzyjecieInsert(Przyjecie przyjecie)
        {
            try
            {
                using (OpportunityEntities context = new OpportunityEntities(Conn))
                {
                    przyjecie.PrzyjecieDb.last_update = DateTime.Now;
                    przyjecie.PrzyjecieDb.uzytkownik = Ustawienia.NazwaUzytkownika;
                    context.Przyjecia.Add(przyjecie.PrzyjecieDb);
                    context.SaveChanges();
                }
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        internal static void PrzyjecieUpdate(Przyjecie przyjecie)
        {
            try
            {
                using (OpportunityEntities context = new OpportunityEntities(Conn))
                {
                    przyjecie.PrzyjecieDb.last_update = DateTime.Now;
                    context.Entry(przyjecie.PrzyjecieDb).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        internal static void PrzyjecieRemove(Przyjecie przyjecie)
        {
            try
            {
                using (OpportunityEntities context = new OpportunityEntities(Conn))
                {
                    przyjecie.PrzyjecieDb.nieaktywne = true;
                    przyjecie.PrzyjecieDb.last_update = DateTime.Now;
                    przyjecie.PrzyjecieDb.uzytkownik = Ustawienia.NazwaUzytkownika;
                    context.Entry(przyjecie.PrzyjecieDb).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        #endregion

        #region wydania
        internal static void WydanieInsert(Wydanie wydanie)
        {
            try
            {
                using (OpportunityEntities context = new OpportunityEntities(Conn))
                {
                    wydanie.WydanieDb.last_update = DateTime.Now;
                    wydanie.WydanieDb.uzytkownik = Ustawienia.NazwaUzytkownika;
                    context.Wydania.Add(wydanie.WydanieDb);
                    context.SaveChanges();
                }
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        internal static void WydanieUpdate(Wydanie wydanie)
        {
            try
            {
                using (OpportunityEntities context = new OpportunityEntities(Conn))
                {
                    wydanie.WydanieDb.last_update = DateTime.Now;
                    context.Entry(wydanie.WydanieDb).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        internal static void WydanieRemove(Wydanie wydanie)
        {
            try
            {
                using (OpportunityEntities context = new OpportunityEntities(Conn))
                {
                    wydanie.WydanieDb.nieaktywne = true;
                    wydanie.WydanieDb.last_update = DateTime.Now;
                    wydanie.WydanieDb.uzytkownik = Ustawienia.NazwaUzytkownika;
                    context.Entry(wydanie.WydanieDb).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        internal static void OznaczWykonanieWydania(int id, bool wykonane)
        {
            try
            {
                using (OpportunityEntities context = new OpportunityEntities(Conn))
                {
                    Wydania wydanie = context.Wydania.Where(w => w.id == id).FirstOrDefault();
                    if (wydanie != null)
                    {
                        wydanie.wykonano = wykonane ? 1 : 0;
                        context.Entry(wydanie).State = EntityState.Modified;
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        #endregion

        #region wycofania
        internal static void WycofanieInsert(Wycofanie wycofanie)
        {
            try
            {
                using (OpportunityEntities context = new OpportunityEntities(Conn))
                {
                    wycofanie.WycofanieDb.last_update = DateTime.Now;
                    wycofanie.WycofanieDb.uzytkownik = Ustawienia.NazwaUzytkownika;
                    context.Wycofania.Add(wycofanie.WycofanieDb);
                    context.SaveChanges();
                }
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        internal static void WycofanieUpdate(Wycofanie wycofanie)
        {
            try
            {
                using (OpportunityEntities context = new OpportunityEntities(Conn))
                {
                    wycofanie.WycofanieDb.last_update = DateTime.Now;
                    context.Entry(wycofanie.WycofanieDb).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        internal static void WycofanieRemove(Wycofanie wycofanie)
        {
            try
            {
                using (OpportunityEntities context = new OpportunityEntities(Conn))
                {
                    wycofanie.WycofanieDb.nieaktywne = true;
                    wycofanie.WycofanieDb.last_update = DateTime.Now;
                    wycofanie.WycofanieDb.uzytkownik = Ustawienia.NazwaUzytkownika;
                    context.Entry(wycofanie.WycofanieDb).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        #endregion

        #region oświadczenie
        internal static void ZatrudnienieInsert(Zatrudnienie zatrudnienie)
        {
            try
            {
                using (OpportunityEntities context = new OpportunityEntities(Conn))
                {
                    zatrudnienie.ZatrudnienieDb.last_update = DateTime.Now;
                    zatrudnienie.ZatrudnienieDb.uzytkownik = Ustawienia.NazwaUzytkownika;
                    context.Zatrudnienia.Add(zatrudnienie.ZatrudnienieDb);
                    context.SaveChanges();
                }
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }

        internal static void ZatrudnienieUpdate(Zatrudnienie zatrudnienie)
        {
            try
            {
                using (OpportunityEntities context = new OpportunityEntities(Conn))
                {
                    zatrudnienie.ZatrudnienieDb.last_update = DateTime.Now;
                    context.Entry(zatrudnienie.ZatrudnienieDb).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        internal static void ZatrudnienieRemove(Zatrudnienie zatrudnienie)
        {
            try
            {
                using (OpportunityEntities context = new OpportunityEntities(Conn))
                {
                    zatrudnienie.ZatrudnienieDb.last_update = DateTime.Now;
                    zatrudnienie.ZatrudnienieDb.nieaktywne = true;
                    zatrudnienie.ZatrudnienieDb.uzytkownik = Ustawienia.NazwaUzytkownika;
                    context.Entry(zatrudnienie.ZatrudnienieDb).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        #endregion
    }
}
