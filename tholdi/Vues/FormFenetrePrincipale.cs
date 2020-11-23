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

            Form formAjouter = new FormAjouterDeclaration();
            formAjouter.Show();

        }



        private void buttonSupprimerDeclaration_Click(object sender, EventArgs e)
        {
            Form formSupprimerDeclaration = new FormSupprimer();
            formSupprimerDeclaration.Show();
        }

        private void buttonConsulterOuModifierDeclaration_Click(object sender, EventArgs e)
        {
            Form FormConsulterOuSupprimer = new FormConsultationEtModification();
            FormConsulterOuSupprimer.Show();
        }

        private void FormFenetrePrincipale_Load(object sender, EventArgs e)
        {

        }
    }
}
