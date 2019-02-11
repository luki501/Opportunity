using Opportunity.Model.Obiekty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opportunity.Model.Database
{
    public interface IDbAdapter
    {
        List<Pracownik> GetListaPracownikow();
        void PracownikInsert(Pracownik p);
        void PracownikUpdate(Pracownik p);
        void DataBaseUpdate();
        void PracownikRemove(Pracownik pracownik);
        /*
int PracownikInsert(Pracownik p);
void PracownikUpdate(Pracownik p);
void PracownikDelete(int id);
Pracownik PracownikSelect(int id);

int AdresInsert(Adres a);
void AdresUpdate(Adres a);
void AdresDelete(int id);
Adres AdresSelect(int id);

int PaszportInsert(Paszport p);
void PaszportUpdate(Paszport p);
void PaszportDelete(int id);
Paszport PaszportSelect(int id);
List<Paszport> ListaPaszportowPracownika(int idPracownika);
List<Paszport> GetListaPaszportow();

int SzkolenieBHPInsert(SzkolenieBHP s);
void SzkolenieBHPUpdate(SzkolenieBHP s);
void SzkolenieBHPDelete(int id);
SzkolenieBHP SzkolenieBHPSelect(int id);
List<SzkolenieBHP> ListaSzkolenPracownika(int idPracownika);
List<SzkolenieBHP> GetListaSzkolen();

int WizaInsert(Wiza w);
void WizaUpdate(Wiza s);
void WizaDelete(int id);
Wiza WizaSelect(int id);
List<Wiza> ListaWizPracownika(int idPracownika);
List<Wiza> GetListaWiz();

int OswiadczenieInsert(Oswiadczenie o);
void OswiadczenieUpdate(Oswiadczenie o);
void OswiadczenieDelete(int id);
Oswiadczenie OswiadczenieSelect(int id);
List<Oswiadczenie> ListaOswiadczenPracownika(int idPracownika);
List<Oswiadczenie> GetListaOswiadczen();

int ZezwolenieInsert(Zezwolenie z);
void ZezwolenieUpdate(Zezwolenie z);
void ZezwolenieDelete(int id);
Zezwolenie ZezwolenieSelect(int id);
List<Zezwolenie> ListaZezwolenPracownika(int idPracownika);
List<Zezwolenie> GetListaZezwolen();

int ZaproszenieInsert(Zaproszenie z);
void ZaproszenieUpdate(Zaproszenie z);
void ZaproszenieDelete(int id);
Zaproszenie ZaproszenieSelect(int id);
List<Zaproszenie> ListaZaproszenPracownika(int idPracownika);
List<Zaproszenie> GetListaZaproszen();
*/
    }
}
