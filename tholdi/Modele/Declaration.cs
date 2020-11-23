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
        public Probleme CodeProbleme { get; set; }
        public Modele.Container NumContainer { get; set; }

        public string commentaireDeclaration { get; set; }
        public DateTime DateDeclaration { get; set; }
        public string Urgence { get; set; }
        public string Traite { get; set; }
        public string Docker { get; set; }

        private bool isNew = true;

        private static string _selectSql = "SELECT * FROM declaration ";
        private static string _selectByIdSql = "SELECT * FROM declaration  WHERE codeDeclaration  = ?codeDeclaration  ";
        private static string _updateSql = "UPDATE declaration  SET traite=?traite  WHERE codeDeclaration  =?codeDeclaration  ";
        private static string _insertSql = "INSERT INTO declaration  (numContainer,codeProbleme ,commentaireDeclaration,dateDeclaration,urgence,traite,docker) VALUES (?numContainer,?codeProbleme,?commentaireDeclaration,?dateDeclaration,?urgence,?traite,?docker)";
        private static string _lesDeclarationParContainer = "SELECT * from declaration d, container c where c.numContainer = d.numContainer and c.numContainer = ?numContainer";

        public Declaration()
        {
            DateDeclaration = DateTime.Today;
            Traite = "non";
            Docker = "XBA";
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

                string idNumContainer = jeuEnregistrements["numContainer"].ToString();
                uneDeclaration.NumContainer = Modele.Container.Fetch(Convert.ToInt16(idNumContainer));

                string idProbleme = jeuEnregistrements["codeProbleme"].ToString();
                uneDeclaration.CodeProbleme = Probleme.Fetch(Convert.ToInt32(idProbleme));



                uneDeclaration.commentaireDeclaration = jeuEnregistrements["commentaireDeclaration"].ToString();

                uneDeclaration.DateDeclaration =Convert.ToDateTime(jeuEnregistrements["dateDeclaration"].ToString());


                uneDeclaration.Urgence = jeuEnregistrements["urgence"].ToString();


                uneDeclaration.Traite = jeuEnregistrements["traite"].ToString();

                uneDeclaration.isNew = false;//L'intervenant n'est pas nouveau dans le contexte applicatif puisqu'il provient de la base de données
                resultat.Add(uneDeclaration);//L'intervenant valorisé à partir des informations de la base de données est ajouté à la collection
            }
            openConnection.Close();//La connexion est fermée

            return resultat;
        }


        public static Declaration Fetch(int codeDeclaration)
        {
            Declaration unedeclaration = null;
            MySqlConnection openConnection = DataBaseAccess.getOpenMySqlConnection();
            MySqlCommand commandSql = openConnection.CreateCommand();//Initialisation d'un objet permettant d'interroger la bd commandSql.CommandText = _selectByIdSql;//Définit la requete à utiliser commandSql.Parameters.Add(new MySqlParameter("?id", idIntervenant));//Transmet un paramètre à utiliser lors de l'envoie de la requête. Il s’agit ici de l’identifiant transmis en paramètre.
            commandSql.Prepare();//Prépare la requête (modification du paramètre de la requête)

            commandSql.CommandText = _selectByIdSql;
            commandSql.Parameters.Add(new MySqlParameter("?codeDeclaration", codeDeclaration));
            MySqlDataReader jeuEnregistrements = commandSql.ExecuteReader();//Exécution de la requête
            bool existEnregistrement = jeuEnregistrements.Read();//Lecture du premier enregistrement

            if (existEnregistrement)//Si l'enregistrement existe
            {//alors
                unedeclaration = new Declaration();//Initialisation de la variable Contact

                string idDeclaration = jeuEnregistrements["codeDeclaration"].ToString();
                unedeclaration.CodeDeclaration = Convert.ToInt16(idDeclaration);

                string idNumContainer = jeuEnregistrements["numContainer"].ToString();
                unedeclaration.NumContainer = Modele.Container.Fetch(Convert.ToInt16(idNumContainer));

                string idProbleme = jeuEnregistrements["codeProbleme"].ToString();
                unedeclaration.CodeProbleme = Probleme.Fetch(Convert.ToInt32(idProbleme));


                unedeclaration.commentaireDeclaration = jeuEnregistrements["commentaireDeclaration"].ToString();

                string dateDeclaration = jeuEnregistrements["dateDeclaration"].ToString();
                unedeclaration.DateDeclaration = Convert.ToDateTime(dateDeclaration);

                unedeclaration.Urgence = jeuEnregistrements["urgence"].ToString();

                unedeclaration.Traite = jeuEnregistrements["traite"].ToString();

                unedeclaration.isNew = false;//L'intervenant n'est pas nouveau dans le contexte applicatif puisqu'il provient de la base de données

            }
            openConnection.Close();//fermeture de la connexion

            return unedeclaration;
        }


        public static List<Declaration> LesDeclarationParContainers(int numContainer)
        {
            List<Declaration> resultat = new List<Declaration>();
            MySqlConnection openConnection = DataBaseAccess.getOpenMySqlConnection();
            MySqlCommand commandSql = openConnection.CreateCommand();//Initialisation d'un objet permettant d'interroger la bd 
            commandSql.Prepare();//Prépare la requête (modification du paramètre de la requête)

            commandSql.CommandText = _lesDeclarationParContainer;
            commandSql.Parameters.Add(new MySqlParameter("?numContainer", numContainer));
            MySqlDataReader jeuEnregistrements = commandSql.ExecuteReader();//Exécution de la requête

            while (jeuEnregistrements.Read())//Le jeu d'enregistrement contenant le résultat de la requête est parcouru
            {
                Declaration uneDeclaration = new Declaration();//à chaque itération un intervenant est créé et valorisé

                string idDeclaration = jeuEnregistrements["codeDeclaration"].ToString();
                uneDeclaration.CodeDeclaration = Convert.ToInt16(idDeclaration);

                string idNumContainer = jeuEnregistrements["numContainer"].ToString();
                uneDeclaration.NumContainer = Modele.Container.Fetch(Convert.ToInt16(idNumContainer));

                string idProbleme = jeuEnregistrements["codeProbleme"].ToString();
                uneDeclaration.CodeProbleme = Probleme.Fetch(Convert.ToInt32(idProbleme));


                uneDeclaration.commentaireDeclaration = jeuEnregistrements["commentaireDeclaration"].ToString();

                uneDeclaration.DateDeclaration = Convert.ToDateTime(jeuEnregistrements["dateDeclaration"].ToString());


                uneDeclaration.Urgence = jeuEnregistrements["urgence"].ToString();


                uneDeclaration.Traite = jeuEnregistrements["traite"].ToString();

                uneDeclaration.isNew = false;//L'intervenant n'est pas nouveau dans le contexte applicatif puisqu'il provient de la base de données
       
                resultat.Add(uneDeclaration);//L'intervenant valorisé à partir des informations de la base de données est ajouté à la collection
            }

            openConnection.Close();//La connexion est fermée
            return resultat;



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

            commandSql.Parameters.Add(new MySqlParameter("?numContainer", NumContainer.NumContainer));
            commandSql.Parameters.Add(new MySqlParameter("?codeProbleme", CodeProbleme.CodeProbleme));
            commandSql.Parameters.Add(new MySqlParameter("?commentaireDeclaration", commentaireDeclaration));
            commandSql.Parameters.Add(new MySqlParameter("?dateDeclaration", DateDeclaration));
            commandSql.Parameters.Add(new MySqlParameter("?urgence", Urgence));
            commandSql.Parameters.Add(new MySqlParameter("?traite", Traite));
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
            commandSql.Parameters.Add(new MySqlParameter("?traite", Traite));

            commandSql.Prepare();
            commandSql.ExecuteNonQuery();
            openConnection.Close();

        }

        public override string ToString()
        {
            return  this.CodeDeclaration.ToString()+ ", " + this.NumContainer.NumContainer.ToString() + ", " + this.CodeProbleme.LibelleProbleme + ", " + this.commentaireDeclaration + ", " + this.DateDeclaration.ToString() + ", " + this.Urgence + ", " + this.Traite + ", " + this.Docker;
        }




    }
}
