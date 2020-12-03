using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.DirectoryServices;
using System.Threading; 

namespace tholdi.Vues
{
    public partial class FormConnexion : Form
    {
        public FormConnexion()
        {
            InitializeComponent();
        }

        public static string Docker { get; private set; }
        private void buttonConnexion_Click(object sender, EventArgs e)
        {
            try
            {
                DirectoryEntry Ldap = new DirectoryEntry("LDAP://sio.local/OU=OU-SLAM,OU=OU-Etudiants,DC=sio,DC=local", textBoxUser.Text, textBoxPassword.Text);
                DirectoryEntry de = Ldap.NativeObject as DirectoryEntry;

                Thread t = new Thread(() => Application.Run(new FormFenetrePrincipale()));

                Docker = textBoxUser.Text;

                Form menu = new FormFenetrePrincipale();
                menu.Show();
                this.Hide();
            }
            catch (Exception Ex)
            {

                MessageBox.Show("erreur LDAP" + Ex.Message);
            }



        }

        private void textBoxUser_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
