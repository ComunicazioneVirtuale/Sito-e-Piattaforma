using SitoWeb.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SitoWeb.Data
{
    public class MetodiUtente : Utente
    {
        bool isSuccesfull = false;
        string query = string.Empty;
        DAObject risultato = null;

        /// <summary>
        /// Questo metodo permette di recuperare le informazioni di un utente partendo dal suo indirizzo email e password o dall'ID
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool GetUser(string email = null, string password = null, string id = null)
        {
            string vistaInformazioni = "InfoUtente";

            if (id.isNotNullOrEmpty())
                query = $"select * from {vistaInformazioni} where id = '{id}';";
            else if (email.isNotNullOrEmpty() && password.isNotNullOrEmpty())
                query = $"select * from {vistaInformazioni} where email = '{email}' and password = '{password}';";
            else if (email.isNotNullOrEmpty())
                query = $"select * from {vistaInformazioni} where email = '{email}';";

            if (query.isNotNullOrEmpty())
            {
                risultato = DAL.ExecuteQueryDataTable(query);

                isSuccesfull = risultato.isOk && risultato.ResultRecordSetDataTable.Rows.Count == 1 ? true : false;

                if (isSuccesfull)
                {
                    foreach (DataColumn colonna in risultato.ResultRecordSetDataTable.Columns)
                    {
                        // Informazioni di login
                        Id = colonna.ColumnName.Equals("id") ? risultato.ResultRecordSetDataTable.Rows[0].ToString() : string.Empty;
                        Email = colonna.ColumnName.Equals("email") ? risultato.ResultRecordSetDataTable.Rows[0].ToString() : string.Empty;
                        Password = colonna.ColumnName.Equals("password") ? risultato.ResultRecordSetDataTable.Rows[0].ToString() : string.Empty;
                        HalfPassword = colonna.ColumnName.Equals("half_password") ? risultato.ResultRecordSetDataTable.Rows[0].ToString() : string.Empty;
                        ruolo.Id = colonna.ColumnName.Equals("id_ruolo") ? risultato.ResultRecordSetDataTable.Rows[0].ToString() : string.Empty;
                        ruolo.Descrizione = colonna.ColumnName.Equals("descrizione_ruolo") ? risultato.ResultRecordSetDataTable.Rows[0].ToString() : string.Empty;
                        ruolo.PannelloAmministrativo = colonna.ColumnName.Equals("pannello_amministrativo") ? Convert.ToInt32(risultato.ResultRecordSetDataTable.Rows[0].ToString()) : 0;
                        ruolo.PannelloCliente = colonna.ColumnName.Equals("pannello_cliente") ? Convert.ToInt32(risultato.ResultRecordSetDataTable.Rows[0].ToString()) : 0;
                        ruolo.PannelloCommerciale = colonna.ColumnName.Equals("pannello_commerciale") ? Convert.ToInt32(risultato.ResultRecordSetDataTable.Rows[0].ToString()) : 0;
                        ruolo.PannelloSQL = colonna.ColumnName.Equals("pannello_sql") ? Convert.ToInt32(risultato.ResultRecordSetDataTable.Rows[0].ToString()) : 0;
                        // Informazioni anagrafica
                        CodiceFiscale = colonna.ColumnName.Equals("codice_fiscale") ? risultato.ResultRecordSetDataTable.Rows[0].ToString() : string.Empty;
                        Nome = colonna.ColumnName.Equals("nome") ? risultato.ResultRecordSetDataTable.Rows[0].ToString() : string.Empty;
                        Cognome = colonna.ColumnName.Equals("cognome") ? risultato.ResultRecordSetDataTable.Rows[0].ToString() : string.Empty;
                        DataDiNascita = colonna.ColumnName.Equals("data_nascita") ? Convert.ToDateTime(risultato.ResultRecordSetDataTable.Rows[0].ToString()) : DateTime.MinValue;
                        residenza.Id = colonna.ColumnName.Equals("id_residenza") ? Convert.ToInt32(risultato.ResultRecordSetDataTable.Rows[0].ToString()) : 0;
                        residenza.ViaPiazza = colonna.ColumnName.Equals("via_residenza") ? risultato.ResultRecordSetDataTable.Rows[0].ToString() : string.Empty;
                        residenza.Civico = colonna.ColumnName.Equals("civico_residenza") ? risultato.ResultRecordSetDataTable.Rows[0].ToString() : string.Empty;
                        residenza.Comune = colonna.ColumnName.Equals("comune_residenza") ? risultato.ResultRecordSetDataTable.Rows[0].ToString() : string.Empty;
                        residenza.CAP = colonna.ColumnName.Equals("cap_residenza") ? risultato.ResultRecordSetDataTable.Rows[0].ToString() : string.Empty;
                        residenza.Provincia = colonna.ColumnName.Equals("provincia_residenza") ? risultato.ResultRecordSetDataTable.Rows[0].ToString() : string.Empty;
                        residenza.Interno = colonna.ColumnName.Equals("interno_residenza") ? Convert.ToInt32(risultato.ResultRecordSetDataTable.Rows[0].ToString()) : 0;
                        domicilio.Id = colonna.ColumnName.Equals("id_domicilio") ? Convert.ToInt32(risultato.ResultRecordSetDataTable.Rows[0].ToString()) : 0;
                        domicilio.ViaPiazza = colonna.ColumnName.Equals("via_domicilio") ? risultato.ResultRecordSetDataTable.Rows[0].ToString() : string.Empty;
                        domicilio.Civico = colonna.ColumnName.Equals("civico_domicilio") ? risultato.ResultRecordSetDataTable.Rows[0].ToString() : string.Empty;
                        domicilio.Comune = colonna.ColumnName.Equals("comune_domicilio") ? risultato.ResultRecordSetDataTable.Rows[0].ToString() : string.Empty;
                        domicilio.CAP = colonna.ColumnName.Equals("cap_domicilio") ? risultato.ResultRecordSetDataTable.Rows[0].ToString() : string.Empty;
                        domicilio.Provincia = colonna.ColumnName.Equals("provincia_domicilio") ? risultato.ResultRecordSetDataTable.Rows[0].ToString() : string.Empty;
                        domicilio.Interno = colonna.ColumnName.Equals("interno_domicilio") ? Convert.ToInt32(risultato.ResultRecordSetDataTable.Rows[0].ToString()) : 0;
                        CartaIdentita = colonna.ColumnName.Equals("carta_identita") ? risultato.ResultRecordSetDataTable.Rows[0].ToString() : string.Empty;
                        Patente = colonna.ColumnName.Equals("patente") ? risultato.ResultRecordSetDataTable.Rows[0].ToString() : string.Empty;
                        Telefono = colonna.ColumnName.Equals("telefono") ? Convert.ToInt64(risultato.ResultRecordSetDataTable.Rows[0].ToString()) : 0;
                        Iban = colonna.ColumnName.Equals("iban") ? risultato.ResultRecordSetDataTable.Rows[0].ToString() : string.Empty;
                        Professione = colonna.ColumnName.Equals("professione") ? risultato.ResultRecordSetDataTable.Rows[0].ToString() : string.Empty;
                    }
                }
            }
            else
                isSuccesfull = false;

            return isSuccesfull;
        }

        //TODO: Metodo salva utente nel db
        public bool Save()
        {

            return isSuccesfull;
        }
    }
}
