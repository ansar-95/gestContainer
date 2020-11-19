using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using tholdi.Ressources;

namespace tholdi.Modele
{
    class Declaration
    {
        public int CodeDeclaration { get; private set; }
        public int CodeProbleme { get; set; }
        public int NumContainer { get; set; }

        public string commentaireDeclaration { get; set; }
        public DateTime DateDeclaration { get; set; }
        public bool Urgence { get; set; }
        public bool Traiter { get; set; }
        public string Docker { get; set; }

        private bool isNew = true;

        private static string _selectSql = "SELECT * FROM declaration";
        private static string _selectByIdSql = "SELECT * FROM declaration WHERE codeDeclaration = ?codeDeclaration ";
        private static string _updateSql = "UPDATE declaration SET traiter=?traiter  WHERE codeDeclaration =?codeDeclaration ";
        private static string _insertSql = "INSERT INTO intervenant (codeProbleme,numContainer,commentaireDeclaration,urgence,traiter,docker) VALUES (?codeProbleme,?numContainer,?commentaireDeclaration,?urgence,?traiter,?docker)";


        public Declaration()
        {
            DateDeclaration = DateTime.Today;
            Traiter = false;
        }


        public static List<Declaration> FetchAll()
        {
            List<Declaration> resultat = new List<Declaration>();//Variable résultat de type collectiond'intervenant
            MySqlConnection openConnection = DataBaseAccess.getOpenMySqlConnection();//Déclaration et initialisation d'un objet de connexion
            MySqlCommand commandSql = openConnection.CreateCommand();//Déclaration et initialisation d'un objet permettant d'interroger la base de données
            commandSql.CommandText = _selectSql;//La représentation textuelle de la requête SQL est transmise à  l'objet en charge d'interroger la base de données

            MySqlDataReader jeuEnregistrements = commandSql.ExecuteReader();//Exécution de la requête SQL
            while (jeuEnregistrements.Read())//Le jeu d'enregistrement contenant le résultat de la requête est parcouru
            {
                Declaration uneDeclaration = new Declaration();//à chaque itération un intervenant est créé et valorisé

                string idDeclaration = jeuEnregistrements["codeDeclaration"].ToString();
                uneDeclaration.CodeDeclaration = Convert.ToInt16(idDeclaration);

                string idProbleme = jeuEnregistrements["codeProbleme"].ToString();
                uneDeclaration.CodeProbleme = Convert.ToInt16(idProbleme);

                string idNumContainer = jeuEnregistrements["numContainer"].ToString();
                uneDeclaration.NumContainer = Convert.ToInt16(idNumContainer);

                uneDeclaration.commentaireDeclaration = jeuEnregistrements["commentaireDeclaration"].ToString();

                string dateDeclaration = jeuEnregistrements["dateDeclaration"].ToString();
                uneDeclaration.DateDeclaration = Convert.ToDateTime(dateDeclaration);

                string urgence = jeuEnregistrements["urgence"].ToString();
                uneDeclaration.Traiter = Convert.ToBoolean(urgence);

                string traiter = jeuEnregistrements["traiter"].ToString();
                uneDeclaration.Traiter = Convert.ToBoolean(traiter);

                uneDeclaration.isNew = false;//L'intervenant n'est pas nouveau dans le contexte applicatif puisqu'il provient de la base de données
                resultat.Add(uneDeclaration);//L'intervenant valorisé à partir des informations de la base de données est ajouté à la collection
            }
            openConnection.Close();//La connexion est fermée

            return resultat;
        }


        public static Declaration Fetch(int idIntervenant)
        {
            Declaration unedeclaration = null;
            MySqlConnection openConnection = DataBaseAccess.getOpenMySqlConnection();
            MySqlCommand commandSql = openConnection.CreateCommand();//Initialisation d'un objet permettant d'interroger la bd commandSql.CommandText = _selectByIdSql;//Définit la requete à utiliser commandSql.Parameters.Add(new MySqlParameter("?id", idIntervenant));//Transmet un paramètre à utiliser lors de l'envoie de la requête. Il s’agit ici de l’identifiant transmis en paramètre.
            commandSql.Prepare();//Prépare la requête (modification du paramètre de la requête)
            MySqlDataReader jeuEnregistrements = commandSql.ExecuteReader();//Exécution de la requête
            bool existEnregistrement = jeuEnregistrements.Read();//Lecture du premier enregistrement

            if (existEnregistrement)//Si l'enregistrement existe
            {//alors
                unedeclaration = new Declaration();//Initialisation de la variable Contact

                string idDeclaration = jeuEnregistrements["codeDeclaration"].ToString();
                unedeclaration.CodeDeclaration = Convert.ToInt16(idDeclaration);

                string idProbleme = jeuEnregistrements["codeProbleme"].ToString();
                unedeclaration.CodeProbleme = Convert.ToInt16(idProbleme);

                string idNumContainer = jeuEnregistrements["numContainer"].ToString();
                unedeclaration.NumContainer = Convert.ToInt16(idNumContainer);

                unedeclaration.commentaireDeclaration = jeuEnregistrements["commentaireDeclaration"].ToString();

                string dateDeclaration = jeuEnregistrements["dateDeclaration"].ToString();
                unedeclaration.DateDeclaration = Convert.ToDateTime(dateDeclaration);

                string urgence = jeuEnregistrements["urgence"].ToString();
                unedeclaration.Traiter = Convert.ToBoolean(urgence);

                string traiter = jeuEnregistrements["traiter"].ToString();
                unedeclaration.Traiter = Convert.ToBoolean(traiter);

                unedeclaration.isNew = false;//L'intervenant n'est pas nouveau dans le contexte applicatif puisqu'il provient de la base de données

            }
            openConnection.Close();//fermeture de la connexion

            return unedeclaration;
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

            commandSql.Parameters.Add(new MySqlParameter("?codeProbleme", CodeDeclaration));
            commandSql.Parameters.Add(new MySqlParameter("?numContaier", NumContainer));
            commandSql.Parameters.Add(new MySqlParameter("?commentaireDeclaration", DateDeclaration));
            commandSql.Parameters.Add(new MySqlParameter("?urgence", Urgence));
            commandSql.Parameters.Add(new MySqlParameter("?traiter", Traiter));
            commandSql.Parameters.Add(new MySqlParameter("?docker", Docker));
            commandSql.Prepare();
            commandSql.ExecuteNonQuery();
            CodeDeclaration = Convert.ToInt16(commandSql.LastInsertedId);
            openConnection.Close();
        }

        private void Update()
        {
            MySqlConnection openConnection =
           DataBaseAccess.getOpenMySqlConnection();
            MySqlCommand commandSql = openConnection.CreateCommand();
            commandSql.CommandText = _updateSql;
            commandSql.Parameters.Add(new MySqlParameter("?codeDeclaration", CodeDeclaration));
            commandSql.Parameters.Add(new MySqlParameter("?traiter", Traiter));

            commandSql.Prepare();
            commandSql.ExecuteNonQuery();
            openConnection.Close();

        }
    }
}
