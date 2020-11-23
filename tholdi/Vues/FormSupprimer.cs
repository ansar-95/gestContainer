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
    public partial class FormSupprimer : Form
    {
        private Modele.Container _containerSelectionee;
        private List<Declaration> lesDeclarations;
        private Declaration _declarationSelectionee;
        string[] tableau = new string[] { "CodeDeclaration", "CodeProbleme", "NumContainer", "commentaireDeclaration", "DateDeclaration", "Urgence", "Traite", "Docker" };
        public FormSupprimer()
        {
            InitializeComponent();
        }

        private void FormSupprimer_Load(object sender, EventArgs e)
        {
            comboBoxContainer.DataSource = Modele.Container.FetchAll();
            comboBoxContainer.DisplayMember = "NumContainer";


        }

        private void comboBoxContainer_SelectedIndexChanged(object sender, EventArgs e)
        {
            _containerSelectionee = comboBoxContainer.SelectedItem as Modele.Container;

            if (_containerSelectionee != null)
            {
                labelTypeContainerResultat.Text = _containerSelectionee.TypeContaier;

                dataGridViewSuprimmer.DataSource = declarationTable(Declaration.LesDeclarationParContainers(_containerSelectionee.NumContainer));
            }
        }

        private DataTable declarationTable (List<Declaration> lesDeclarations)
        {
            DataTable table = new DataTable();

            for (int i = 0; i < tableau.Length; i++)
            {
                if (tableau[i] == "CodeDeclaration" || tableau[i] == "NumContainer")
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

            return table;
        }

        private void buttonSupprimer_Click(object sender, EventArgs e)
        {
            if (_declarationSelectionee != null)
            {
                _declarationSelectionee.Delete();
                var declaration = dataGridViewSuprimmer.SelectedCells;
                dataGridViewSuprimmer.DataSource = declarationTable(Declaration.LesDeclarationParContainers(_containerSelectionee.NumContainer));


            }
        }
        private void dataGridViewSuprimmer_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewSuprimmer.SelectedRows.Count > 0)
            {
                var declaration = dataGridViewSuprimmer.SelectedCells;
                _declarationSelectionee = Declaration.Fetch(Convert.ToInt32(declaration[0].Value));

            }
        }
    }
}
