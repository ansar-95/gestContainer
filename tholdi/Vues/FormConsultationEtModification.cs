using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using tholdi.Modele;

namespace tholdi.Vues
{
    public partial class FormConsultationEtModification : Form
    {
        private Modele.Container _containerSelectionee;
        private List<Declaration> lesDeclarations;
        private Declaration _declarationSelectionee;


        string[] tableau = new string[] { "CodeDeclaration", "CodeProbleme", "NumContainer", "commentaireDeclaration", "DateDeclaration", "Urgence", "Traite", "Docker" };

        public FormConsultationEtModification()
        {
            InitializeComponent();
        }

        private void FormConsultationEtModification_Load(object sender, EventArgs e)
        {

            comboBoxContainer.DataSource = Modele.Container.FetchAll();
            comboBoxContainer.DisplayMember = "NumContainer";
            comboBoxTraiter.Text = "non";

        }

        private void comboBoxContainer_SelectedIndexChanged(object sender, EventArgs e)
        {
            _containerSelectionee = comboBoxContainer.SelectedItem as Modele.Container;

            if (_containerSelectionee != null)
            {
                labelTypeContainerResultat.Text = _containerSelectionee.TypeContaier;

                lesDeclarations = Declaration.LesDeclarationParContainers(_containerSelectionee.NumContainer);

                DataTable table = new DataTable();

                for (int i = 0; i < tableau.Length; i++)
                {
                    if(tableau[i] == "CodeDeclaration" || tableau[i] == "NumContainer")
                    {
                        table.Columns.Add(tableau[i], typeof(int));
                    }
                    else
                    {
                        table.Columns.Add(tableau[i], typeof(string));
                    }
                    
                }



                foreach (Declaration declaration in lesDeclarations)
                {
                    table.Rows.Add(declaration.CodeDeclaration, declaration.CodeProbleme.LibelleProbleme, declaration.NumContainer.NumContainer, declaration.commentaireDeclaration, declaration.DateDeclaration, declaration.Urgence, declaration.Traite, declaration.Docker); 
                }

                dataGridViewInformationsDclaration.DataSource = table;
            }
        }



        



        private void dataGridViewInformationsDclaration_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewInformationsDclaration.SelectedRows.Count > 0)
            {
                var declaration = dataGridViewInformationsDclaration.SelectedCells;
                _declarationSelectionee = Declaration.Fetch(Convert.ToInt32(declaration[0].Value));

            }
        }

        private void comboBoxTraiter_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboBoxTraiter.Text == "oui")
            {
              comboBoxTraiter.Text = "oui";
            }
            else
            {
                comboBoxTraiter.Text = "non";
            }


        }

        private void buttonModification_Click(object sender, EventArgs e)
        {
            _declarationSelectionee.Traite = comboBoxTraiter.Text;
            _declarationSelectionee.Save();

            var declaration = dataGridViewInformationsDclaration.SelectedCells;
            declaration[6].Value = _declarationSelectionee.Urgence;
        }
    }
}
