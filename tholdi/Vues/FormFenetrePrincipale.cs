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
        private Form formSupprimerDeclaration;
        private Form formAjouter;
        private Form FormStatistique;
        private Form FormConsulterOuSupprimer;

        public FormFenetrePrincipale()
        {
            InitializeComponent();
        }

        private void buttonAjouterDeclaration_Click(object sender, EventArgs e)
        {

            formAjouter = new FormAjouterDeclaration();
            formAjouter.Show();
         
        }



        private void buttonSupprimerDeclaration_Click(object sender, EventArgs e)
        {

            formSupprimerDeclaration = new FormSupprimer();
            formSupprimerDeclaration.Show();
        }

        private void buttonConsulterOuModifierDeclaration_Click(object sender, EventArgs e)
        {

            FormConsulterOuSupprimer = new FormConsultationEtModification();
            FormConsulterOuSupprimer.Show();
        }

        private void FormFenetrePrincipale_Load(object sender, EventArgs e)
        {

        }

        private void buttonStatistique_Click(object sender, EventArgs e)
        {
            FormStatistique = new FormStatistique();
            FormStatistique.Show();

        }

        private void FormFenetrePrincipale_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}
