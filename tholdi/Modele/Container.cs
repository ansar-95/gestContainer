using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using tholdi.Ressources;


namespace tholdi.Modele
{
    class Container
    {
        public int NumContainer { get; private set; }        
        public DateTime DateAchat { get; set; }
        public string TypeContaier { get; set; }
        public DateTime DateDerniereInsp { get; set; }
        private bool isNew = true;
        public Container()
        {

        }

        private static string _selectSql = "SELECT * FROM container ";
        private static string _selectByIdSql = "SELECT * FROM container WHERE numContainer = ?numContainer ";
        private static string _insertSql = "INSERT INTO container (dateAchat,typeContainer,dateDerniereInsp) VALUES (?dateAchat,?typeContainer,?dateDerniereInsp)";
        private static string _updateSql = "UPDATE container SET dateDerniereInsp=?dateDerniereInsp  WHERE numContainer  =?numContainer  ";
        public static List<Container> FetchAll()
        {
            List<Container> resultat = new List<Container>();//Variable résultat de type collectiond'intervenant
            MySqlConnection openConnection = DataBaseAccess.getOpenMySqlConnection();//Déclaration et initialisation d'un objet de connexion
            MySqlCommand commandSql = openConnection.CreateCommand();//Déclaration et initialisation d'un objet permettant d'interroger la base de données
            commandSql.CommandText = _selectSql;//La représentation textuelle de la requête SQL est transmise à  l'objet en charge d'interroger la base de données

            MySqlDataReader jeuEnregistrements = commandSql.ExecuteReader();//Exécution de la requête SQL
            while (jeuEnregistrements.Read())//Le jeu d'enregistrement contenant le résultat de la requête est parcouru
            {
                Container unContainer = new Container();//à chaque itération un intervenant est créé et valorisé

                string idContainer = jeuEnregistrements["numContainer"].ToString();
                unContainer.NumContainer = Convert.ToInt16(idContainer);

                string dateAchat = jeuEnregistrements["dateAchat"].ToString();
                unContainer.DateAchat = Convert.ToDateTime(dateAchat);

                unContainer.TypeContaier = jeuEnregistrements["typeContainer"].ToString();

                string dateDerniereInsp = jeuEnregistrements["dateDerniereInsp"].ToString();
                unContainer.DateDerniereInsp = Convert.ToDateTime(dateDerniereInsp);




                unContainer.isNew = false;//L'intervenant n'est pas nouveau dans le contexte applicatif puisqu'il provient de la base de données
                resultat.Add(unContainer);//L'intervenant valorisé à partir des informations de la base de données est ajouté à la collection
            }
            openConnection.Close();//La connexion est fermée

            return resultat;
        }


        public static Container Fetch(int numContainer)
        {
            Container unContainer = null;
            MySqlConnection openConnection = DataBaseAccess.getOpenMySqlConnection();
            
            MySqlCommand commandSql = openConnection.CreateCommand();//Initialisation d'un objet permettant d'interroger la bd commandSql.CommandText = _selectByIdSql;//Définit la requete à utiliser commandSql.Parameters.Add(new MySqlParameter("?id", idIntervenant));//Transmet un paramètre à utiliser lors de l'envoie de la requête. Il s’agit ici de l’identifiant transmis en paramètre.
            commandSql.CommandText = _selectByIdSql;
            commandSql.Parameters.Add(new MySqlParameter("?numContainer", numContainer));
            commandSql.Prepare();//Prépare la requête (modification du paramètre de la requête)
            MySqlDataReader jeuEnregistrements = commandSql.ExecuteReader();//Exécution de la requête
            bool existEnregistrement = jeuEnregistrements.Read();//Lecture du premier enregistrement

            if (existEnregistrement)//Si l'enregistrement existe
            {//alors
                unContainer = new Container();//à chaque itération un intervenant est créé et valorisé

                string idContainer = jeuEnregistrements["numContainer"].ToString();
                unContainer.NumContainer = Convert.ToInt16(idContainer);

                string dateAchat = jeuEnregistrements["dateAchat"].ToString();
                unContainer.DateAchat = Convert.ToDateTime(dateAchat);

                unContainer.TypeContaier = jeuEnregistrements["typeContainer"].ToString();

                string dateDerniereInsp = jeuEnregistrements["dateDerniereInsp"].ToString();
                unContainer.DateDerniereInsp = Convert.ToDateTime(dateDerniereInsp);


                unContainer.isNew = false;//L'intervenant n'est pas nouveau dans le contexte applicatif puisqu'il provient de la base de données

            }
            openConnection.Close();//fermeture de la connexion

            return unContainer;
        }

        public void Save()
        {
            if (isNew)
            {
                Insert();
            }
            else
            {
                Update();
            }
        }


        private void Insert()
        {
            MySqlConnection openConnection = DataBaseAccess.getOpenMySqlConnection();
            MySqlCommand commandSql = openConnection.CreateCommand();
            commandSql.CommandText = _insertSql;

            commandSql.Parameters.Add(new MySqlParameter("?dateAchat ", DateAchat));
            commandSql.Parameters.Add(new MySqlParameter("?typeContainer ", TypeContaier));
            commandSql.Parameters.Add(new MySqlParameter("?dateDerniereInsp  ", DateDerniereInsp));
            commandSql.Prepare();
            commandSql.ExecuteNonQuery();
            NumContainer = Convert.ToInt16(commandSql.LastInsertedId);
            openConnection.Close();
        }

        private void Update()
        {
            MySqlConnection openConnection =
            DataBaseAccess.getOpenMySqlConnection();
            MySqlCommand commandSql = openConnection.CreateCommand();
            commandSql.CommandText = _updateSql;
            commandSql.Parameters.Add(new MySqlParameter("?numContainer ", NumContainer));
            commandSql.Parameters.Add(new MySqlParameter("?dateDerniereInsp ", DateDerniereInsp));

            commandSql.Prepare();
            commandSql.ExecuteNonQuery();
            openConnection.Close();

        }



    }
}
