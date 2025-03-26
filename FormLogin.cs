using System;
using System.IO;
using System.Windows.Forms;

namespace Proiect
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
            btnLogin.Click += (s, e) => Login();
            linkLabelCreeazaCont.Click += (s, e) => DeschideFormSignUp();
            btnAscundeArata.Click += (s, e) => ComutaVizibilitateParola();
        }

        private void Login()
        {
            string nume = textBoxNume.Text;
            string parola = textBoxParola.Text;

            
            var (loginReusit, puncte) = VerificaDateLogin(nume, parola);

            if (loginReusit)
            {
                Form1 formPrincipal = Application.OpenForms["Form1"] as Form1;
                if (formPrincipal != null)
                {
                    formPrincipal.UtilizatorLogat(nume, puncte);
                }

                MessageBox.Show("Login reusit!", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close(); 
            }
            else
            {
                MessageBox.Show("Nume sau parola incorecte!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private (bool, int) VerificaDateLogin(string nume, string parola)
        {
            string caleFisier = "conturi.txt";

            if (File.Exists(caleFisier))
            {
                string[] conturi = File.ReadAllLines(caleFisier); 

                foreach (var cont in conturi)
                {
                    string[] dateCont = cont.Split('|'); 
                    if (dateCont.Length >= 3 && dateCont[0] == nume && dateCont[1] == parola)
                    {
                        int puncte = int.Parse(dateCont[2]); 
                        return (true, puncte); 
                    }
                }
            }

            return (false, 0); 
        }

        private void DeschideFormSignUp()
        {
            FormSignUp formSignUp = new FormSignUp();
            formSignUp.ShowDialog();
        }

        private void ComutaVizibilitateParola()
        {
            if (textBoxParola.PasswordChar == '*')
            {
                textBoxParola.PasswordChar = '\0'; 
                btnAscundeArata.Text = "Ascunde"; 
            }
            else
            {
      
                textBoxParola.PasswordChar = '*';
                btnAscundeArata.Text = "Arata";
            }
        }
    }
}