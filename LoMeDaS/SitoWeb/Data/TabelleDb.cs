using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SitoWeb.Data
{
    public class Utente
    {
        //Campi Tabella dbo.Utenti
        [DisplayName("Id Utente")]
        public string Id { get; set; }
        //----------------------------------
        [DataType(DataType.EmailAddress)]
        [StringLength(254, ErrorMessage = "Valore troppo lungo")]
        public string Email { get; set; }
        //----------------------------------
        [DataType(DataType.Password)]
        [StringLength(32, ErrorMessage = "Valore troppo lungo")]
        public string Password { get; set; }
        //----------------------------------
        [StringLength(32, ErrorMessage = "Valore troppo lungo")]
        public string HalfPassword { get; set; }
        //----------------------------------
        public Ruolo ruolo { get; set; }
        //----------------------------------        
        [DataType(DataType.DateTime, ErrorMessage = "Formato non corretto")]
        public DateTime DataRegistrazione { get; set; }

        //Campi Tabella ana.Utenti
        [StringLength(16, ErrorMessage = "Valore troppo lungo")]
        public string CodiceFiscale { get; set; }
        //----------------------------------
        [DataType(DataType.Text, ErrorMessage = "Il campo non può contenere numeri")]
        [StringLength(50, ErrorMessage = "Valore troppo lungo")]
        public string Nome { get; set; }
        //----------------------------------
        [DataType(DataType.Text, ErrorMessage = "Il campo non può contenere numeri")]
        [StringLength(50, ErrorMessage = "Valore troppo lungo")]
        public string Cognome { get; set; }
        //----------------------------------
        [DataType(DataType.Date, ErrorMessage = "Formato non corretto")]
        public DateTime DataDiNascita { get; set; }
        //----------------------------------
        public Indirizzo residenza { get; set; }
        //----------------------------------
        public Indirizzo domicilio { get; set; }
        //----------------------------------
        [StringLength(10, ErrorMessage = "Valore troppo lungo")]
        public string CartaIdentita { get; set; }
        //----------------------------------
        [StringLength(10, ErrorMessage = "Valore troppo lungo")]
        public string Patente { get; set; }
        //----------------------------------
        [DataType(DataType.PhoneNumber)]
        [StringLength(10, ErrorMessage = "Valore troppo lungo")]
        public long Telefono { get; set; }
        //----------------------------------
        [StringLength(27, ErrorMessage = "Valore troppo lungo")]
        public string Iban { get; set; }
        //----------------------------------
        [StringLength(50, ErrorMessage = "Valore troppo lungo")]
        public string Professione { get; set; }
    }
    public class Cliente
    {
        //Campi Tabella cst.Clienti
        public string Id { get; set; }
        public Utente commerciale { get; set; }
        public Utente responsabile { get; set; }
        public DateTime DataRegistrazioneLead { get; set; }
        public DateTime DataRegistrazioneCliente { get; set; }

        //Campi Tabella cst.Colloqui
        public int IdColloquio { get; set; }
        public DateTime DataColloquio { get; set; }
        public int CheckSitoWeb { get; set; }
        public int CheckEcommerce { get; set; }
        public string UrlSitoWeb { get; set; }
        public int CheckGestionale { get; set; }
        public string UrlGestionale { get; set; }
        public int CheckCampagneSocial { get; set; }
        public string NoteColloquio { get; set; }

        //Campi Tabella cst.ProposteColloqui
        public Prodotto sitoWeb { get; set; }
        public Prodotto gestionale { get; set; }
        public Prodotto social { get; set; }
        public string NoteProposteProdotti { get; set; }

        //Campi Tabella ana.Clienti
        public string RagioneSociale { get; set; }
        public string PartitaIva { get; set; }
        public int NumeroDipendenti { get; set; }
        public string SDI { get; set; }
        public string Telefono { get; set; }
        public Indirizzo sedeLegale { get; set; }
        public Indirizzo sedeOperativa { get; set; }
        public string Pec { get; set; }
        public string Email { get; set; }
        public long CapitaleSociale { get; set; }
        public long Fatturato { get; set; }
    }
    public class Indirizzo
    {
        //Campi Tabella ana.Indirizzi
        public int Id { get; set; }
        public string ViaPiazza { get; set; }
        public string Civico { get; set; }
        public string Comune { get; set; }
        public string CAP { get; set; }
        public string Provincia { get; set; }
        public int Interno { get; set; }
    }
    public class Ruolo
    {
        public string Id { get; set; }
        public string Descrizione { get; set; }
        public int PannelloCliente { get; set; }
        public int PannelloCommerciale { get; set; }
        public int PannelloAmministrativo { get; set; }
        public int PannelloSQL { get; set; }
    }
    public class Prodotto
    {
        public int Id { get; set; }
        public string Titolo { get; set; }
        public string Descrizione { get; set; }
        public Tipologia tipologia { get; set; }
        public Settore settore { get; set; }
        public decimal costo { get; set; }
    }
    public class Tipologia
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descrizione { get; set; }
    }
    public class Settore
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descrizione { get; set; }
    }
    public class Ordine
    {
        //Campi tabella prj.Ordini
        public int Id { get; set; }
        public Cliente cliente { get; set; }
        public decimal Totale { get; set; }
        public DateTime Data { get; set; }

        //Campi tabella prg.Ordine_Prodotti
        public Prodotto prodotto { get; set; }
        public decimal Costo { get; set; }
        public decimal Sconto { get; set; }
        public DateTime DataAttivazione { get; set; }
        public DateTime DataScadenza { get; set; }
    }
}
