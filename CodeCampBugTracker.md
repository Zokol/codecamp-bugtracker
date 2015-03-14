**Worklist**

  * Login <img src='http://www.clker.com/cliparts/0/f/7/0/11954234311954389563ok_mark_h_kon_l_vdal_02.svg.med.png' height='10px' />
  * Tietojen tallennus tietokantaan tai tiedostoon <img src='http://www.clker.com/cliparts/0/f/7/0/11954234311954389563ok_mark_h_kon_l_vdal_02.svg.med.png' height='10px' />
  * Bugilista <img src='http://www.clker.com/cliparts/0/f/7/0/11954234311954389563ok_mark_h_kon_l_vdal_02.svg.med.png' height='10px' />
  * Uuden bugin raportointi ( Title, Description, Criticality, Status, User) <img src='http://www.clker.com/cliparts/0/f/7/0/11954234311954389563ok_mark_h_kon_l_vdal_02.svg.med.png' height='10px' />
  * Bugilistan suodatus (Status ja Criticality)
  * Ilmoitukset statuksen muutoksesta (bugia seuraaville käyttäjille) <img src='http://www.clker.com/cliparts/0/f/7/0/11954234311954389563ok_mark_h_kon_l_vdal_02.svg.med.png' height='10px' />
  * Bugin ilmoittaneen käyttäjän yhteystiedot

  * Kaikkien kurssilaisten testattavissa ohjelman esittelyn aikana

**Keskiviikko-illalla mietitty TO-DO lista**

![http://users.utu.fi/jjholv/taulu1.jpg](http://users.utu.fi/jjholv/taulu1.jpg)

  * Bugilistan järjestäminen otsikoista klikkaamalla

  * Yläpalkki:
    * Logo
    * Filteröinti: status (näyttää vain tietyn statuksen bugit) ja criticality (valittu aste ja siitä ylös). Drop-down-lista.
    * Bugin lisäys nappula (pop-up)
    * Notifications nappula (Status Events-ikkuna jossa näkyy tilattujen bugien eventit, avautuu vasempaan palkkiin) - väri vaihtuu jos uusia status eventtejä on tullut viime loggauksen jälkeen
    * Subscriptions nappula (Bugin tilaaminen vetämällä Bug listalta nappulan päälle, Drag-and-Drop script)
    * Bug ID Search

  * Subscriptions näkymä: Lista käyttäjän Subscriptioneista (filteröity lista bugeja) <img src='http://www.clker.com/cliparts/0/f/7/0/11954234311954389563ok_mark_h_kon_l_vdal_02.svg.med.png' height='10px' />

  * Status Events näkymä: käyttäjän seuraamien bugien statusmuutokset (lista bugeista) <img src='http://www.clker.com/cliparts/0/f/7/0/11954234311954389563ok_mark_h_kon_l_vdal_02.svg.med.png' height='10px' />

  * Profiili näkymä (pop-up)
    * Edit nappula: tekstit muuttuu kentiksi, joihin tiedot voi muuttaa. Nappuloiden tekstit muutetaan Edit->Save ja Close -> Cancel
      * Password
      * e-mail

  * Bug add/edit näkymä
    * Kommenttikenttä
    * Näkymään drop-down listat

  * Bug view näkymä
    * Edit nappula (pop-up)
<br>
<br>
<hr />
<h1>.old</h1></li></ul>

## Perusrakenne ##

**Models**

![http://users.utu.fi/jjholv/model.png](http://users.utu.fi/jjholv/model.png)

**Controllers**
  * Bug
  * Event
  * Subscription
  * User

**Views**
![http://users.utu.fi/jjholv/2011-08-29%2015.01.31.jpg](http://users.utu.fi/jjholv/2011-08-29%2015.01.31.jpg)

  * Bug list ( List view )
    * Listaa kaikki bugit, mahdollisuus järjestellä ja suodattaa ( 2.1 )

  * Bug details ( Detail view )
    * Näyttää kaikki tiedot bugista sivupalkissa ( 2.2.1 )

  * Bug edit ( Edit view )
    * Editointinäkymä, jossa voi muokata statusta jne. ( 4 )

  * Event list ( List view )
    * Bugin historia, näkyy bugin tietojen alapuolella ( 2.2.2 )

  * Filter view ( Top view )
    * Yläpalkissa ( 2.3 )

  * User login ( Login view )
    * Ensimmäinen ruutu ( 1 )

  * User list ( List view )
    * Lista käyttäjän tilaamista bugeista ( 2.1 )

  * User details ( Detail view )
    * Käyttäjän tarkemmat tiedot ja mahdollisuus editoida tilauksia. ( 3.2.1 )

  * User history view
    * Käyttäjän tekemät eventit (3.2.2)

  * User edit view ( Edit view )
    * Editointi-ikkuna ( 4 )

**Bugin statukset**
  * ilmoitettu
  * vahvistettu
  * hylätty
  * priorisoitu
  * varattu
  * korjattu
  * valmis
  * ei korjata

**Jälkeenpäin lisättävät ominaisuudet**

  * Käyttäjäryhmät ( None, Creator, Tester, Interest ), vastaavat BugGrouppia.
  * Bugin poistaminen
  * Statuksen muutos drag-and-dropilla
  * Statistiikka (keskimääräinen ikä, resolution rate)


**Linkit**
  * Brieffin slidet tehtävänannosta: http://www.slideshare.net/tomijuhola/codecamp-general-info