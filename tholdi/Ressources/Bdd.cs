using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace tholdi.Modele
{
    class Bdd
    {

        private  MySqlConnection connection;

        public Bdd()
        {

        }


        public MySqlConnection InitConnexion()
        {
            // Création de la chaîne de connexion
            string connectionString = "SERVER=srv-mydon.sio.local; DATABASE=tholdippe3; UID=maha; PASSWORD=06/06/2001";
            return this.connection = new MySqlConnection(connectionString);
        }
    }
}
