using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using tholdi.Modele;

namespace tholdi.Vues
{
    public partial class FormAjouterDeclaration : Form
    {
        private Modele.Container _containerSelectionee;
        private Probleme _problemeSelectionee;
        public FormAjouterDeclaration()
        {
            InitializeComponent();
        }



        private void FormAjouterDeclaration_Load(object sender, EventArgs e)
        {

            ComboBoxContainer.DataSource = Modele.Container.FetchAll();
            ComboBoxContainer.DisplayMember = "NumContainer";

            comboBoxProbleme.DataSource = Probleme.FetchAll();
            comboBoxProbleme.DisplayMember = "LibelleProbleme";
            
        }


        private void ComboBoxContainer_SelectedIndexChanged(object sender, EventArgs e)
        {
            _containerSelectionee = ComboBoxContainer.SelectedItem as Modele.Container;


            if (_containerSelectionee != null)
            {
                labelTypeContainer.Text = _containerSelectionee.TypeContaier;
            }
        }

        private void comboBoxProbleme_SelectedIndexChanged(object sender, EventArgs e)
        {
            _problemeSelectionee = comboBoxProbleme.SelectedItem as Probleme;
        }


        private void buttonAjouterDeclaration_Click(object sender, EventArgs e)
        {
            if(textBoxCommentaire.Text == "")
            {
                MessageBox.Show("Veuillez remplir le champ commentaire");
            }
            else if(comboBoxUrgence.Text == "")
            {
                MessageBox.Show("Veuillez sélectionner");
            }
            else {

                Declaration uneDeclaration = new Declaration();
                uneDeclaration.NumContainer = _containerSelectionee;
                uneDeclaration.CodeProbleme = _problemeSelectionee;
                uneDeclaration.commentaireDeclaration = textBoxCommentaire.Text;

                uneDeclaration.Urgence = comboBoxUrgence.Text;
                uneDeclaration.Save();

                this.Close();

            }

        }

        private void FormAjouterDeclaration_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool r = false;
            foreach(Form f in Application.OpenForms)
            {
                if(f.Name == "FormFenetrePrincipale")
                {
                    r = true;
                }
            }

            if (!r)
            {
                FormFenetrePrincipale f = new FormFenetrePrincipale();
                f.Show();
            }
        }


    }
}
