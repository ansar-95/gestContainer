using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace tholdi.Modele
{
    class Declaration
    {
        public int NumContainer { get; set; }
        public int CodeProbleme { get; set; }
        public string commentaireDeclaration { get; set; }
        public string DateDeclaration { get; set; }
        public string Urgence { get; set; }
        public string Traiter { get; set; }
        public string Docker { get; set; }



        public void AjouterContact()
        {
            Bdd uneBdd = new Bdd();
            MySqlConnection mySqlConnection = uneBdd.InitConnexion();

            mySqlConnection.Open();


        }
    }





}
