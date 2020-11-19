using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using tholdi.Ressources;

namespace tholdi.Modele
{
    class Probleme
    {
        public int CodeProbleme { get; private set; }
        public string LibelleProbleme { get; set; }
        private bool isNew = true;
        public Probleme()
        {

        }



        private static string _selectSql = "SELECT * FROM probleme";
        private static string _selectByIdSql = "SELECT * FROM probleme WHERE codeProbleme = ?codeProbleme ";
        private static string _insertSql = "INSERT INTO probleme  (libelleProbleme) VALUES (?libelleProbleme)";
        private static string _updateSql = "UPDATE probleme SET libelleProbleme=?libelleProbleme  WHERE codeProbleme  =?codeProbleme  ";

        public static List<Probleme> FetchAll()
        {
            List<Probleme> resultat = new List<Probleme>();//Variable résultat de type collectiond'intervenant
            MySqlConnection openConnection = DataBaseAccess.getOpenMySqlConnection();//Déclaration et initialisation d'un objet de connexion
            MySqlCommand commandSql = openConnection.CreateCommand();//Déclaration et initialisation d'un objet permettant d'interroger la base de données
            commandSql.CommandText = _selectSql;//La représentation textuelle de la requête SQL est transmise à  l'objet en charge d'interroger la base de données

            MySqlDataReader jeuEnregistrements = commandSql.ExecuteReader();//Exécution de la requête SQL
            while (jeuEnregistrements.Read())//Le jeu d'enregistrement contenant le résultat de la requête est parcouru
            {
                Probleme unProbleme = new Probleme();//à chaque itération un intervenant est créé et valorisé

                string idProbleme = jeuEnregistrements["codeProbleme"].ToString();
                unProbleme.CodeProbleme = Convert.ToInt16(idProbleme);

                unProbleme.LibelleProbleme = jeuEnregistrements["libelleProbleme"].ToString();

                unProbleme.isNew = false;//L'intervenant n'est pas nouveau dans le contexte applicatif puisqu'il provient de la base de données
                resultat.Add(unProbleme);//L'intervenant valorisé à partir des informations de la base de données est ajouté à la collection
            }
            openConnection.Close();//La connexion est fermée

            return resultat;
        }

        public static Probleme Fetch(int codeProbleme)
        {
            Probleme unProbleme = null;
            MySqlConnection openConnection = DataBaseAccess.getOpenMySqlConnection();

            MySqlCommand commandSql = openConnection.CreateCommand();//Initialisation d'un objet permettant d'interroger la bd commandSql.CommandText = _selectByIdSql;//Définit la requete à utiliser commandSql.Parameters.Add(new MySqlParameter("?id", idIntervenant));//Transmet un paramètre à utiliser lors de l'envoie de la requête. Il s’agit ici de l’identifiant transmis en paramètre.
            commandSql.CommandText = _selectByIdSql;
            commandSql.Parameters.Add(new MySqlParameter("?codeProbleme", codeProbleme));
            commandSql.Prepare();//Prépare la requête (modification du paramètre de la requête)
            MySqlDataReader jeuEnregistrements = commandSql.ExecuteReader();//Exécution de la requête
            bool existEnregistrement = jeuEnregistrements.Read();//Lecture du premier enregistrement

            if (existEnregistrement)//Si l'enregistrement existe
            {//alors
                unProbleme = new Probleme();//à chaque itération un intervenant est créé et valorisé

                string idProbleme = jeuEnregistrements["codeProbleme"].ToString();
                unProbleme.CodeProbleme = Convert.ToInt16(idProbleme);

                unProbleme.LibelleProbleme = jeuEnregistrements["libelleProbleme"].ToString();

                unProbleme.isNew = false;//L'intervenant n'est pas nouveau dans le contexte applicatif puisqu'il provient de la base de données

            }
            openConnection.Close();//fermeture de la connexion

            return unProbleme;
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

            commandSql.Parameters.Add(new MySqlParameter("?libelleProbleme ", LibelleProbleme));

            commandSql.Prepare();
            commandSql.ExecuteNonQuery();
            CodeProbleme = Convert.ToInt16(commandSql.LastInsertedId);
            openConnection.Close();
        }

        private void Update()
        {
            MySqlConnection openConnection =
            DataBaseAccess.getOpenMySqlConnection();
            MySqlCommand commandSql = openConnection.CreateCommand();
            commandSql.CommandText = _updateSql;
            commandSql.Parameters.Add(new MySqlParameter("?codeProbleme ", CodeProbleme));
            commandSql.Parameters.Add(new MySqlParameter("?libelleProbleme ", LibelleProbleme));

            commandSql.Prepare();
            commandSql.ExecuteNonQuery();
            openConnection.Close();

        }



    }
}
