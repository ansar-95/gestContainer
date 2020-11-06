using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tholdi.Vues
{
    public partial class FormFenetrePrincipale : Form
    {
        public FormFenetrePrincipale()
        {
            InitializeComponent();
        }

        private void buttonAjouterDeclaration_Click(object sender, EventArgs e)
        {

            Form FormAjouter = new FormAjouterDeclaration();
            FormAjouter.Show();

        }

        private void buttonConsulterDeclaration_Click(object sender, EventArgs e)
        {

        }

        private void buttonModifierDeclaration_Click(object sender, EventArgs e)
        {

        }

        private void buttonSupprimerDeclaration_Click(object sender, EventArgs e)
        {

        }
    }
}
