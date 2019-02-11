using Opportunity.Model.Obiekty;
using Opportunity.ViewModel.Obiekty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opportunity.ViewModel
{
    public static class DokumentManager
    {
        public static ADokumentVM GetDokument(ADokument dokument)
        {
            if (dokument is Badanie)
                return new BadanieVM(dokument as Badanie);
            else if (dokument is Oswiadczenie)
                return new OswiadczenieVM(dokument as Oswiadczenie);
            else if (dokument is Paszport)
                return new PaszportVM(dokument as Paszport);
            else if (dokument is SzkolenieBHP)
                return new SzkolenieVM(dokument as SzkolenieBHP);
            else if (dokument is Wiza)
                return new WizaVM(dokument as Wiza);
            else if (dokument is Zameldowanie)
                return new ZameldowanieVM(dokument as Zameldowanie);
            else if (dokument is Zezwolenie)
                return new ZezwolenieVM(dokument as Zezwolenie);
            else if (dokument is Zatrudnienie)
                return new ZatrudnienieVM(dokument as Zatrudnienie);
            else
                return null;
        }
        /* NIE UŻYWANE
        public static ADokumentVM GetDokument(int i)
        {
            switch (i)
            {
                case 0:
                    return new PaszportVM();
                case 1:
                    return new ZezwolenieVM();
                case 2:
                    return new ZameldowanieVM();
                case 3:
                    return new WizaVM();
                case 4:
                    return new SzkolenieVM();
                case 5:
                    return new OswiadczenieVM();
                case 6:
                    return new BadanieVM();
                default:
                    return null;                    
            }
        }
        */
        public static ADokumentVM GetDokument(string nazwa)
        {
            switch (nazwa)
            {
                case "Paszport":
                    return new PaszportVM();
                case "Zezwolenie":
                    return new ZezwolenieVM();
                case "Zameldowanie":
                    return new ZameldowanieVM();
                case "Wiza":
                    return new WizaVM();
                case "Szkolenie":
                    return new SzkolenieVM();
                case "Oświadczenie":
                    return new OswiadczenieVM();
                case "Badanie":
                    return new BadanieVM();
                case "Zatrudnienie":
                    return new ZatrudnienieVM();
                default:
                    return null;
            }
        }
    }
}
