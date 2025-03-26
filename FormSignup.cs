using System;
using System.IO;
using System.Windows.Forms;

namespace Proiect
{
    public partial class FormSignUp : Form
    {
        public FormSignUp()
        {
            InitializeComponent();
            btnSignup.Click += (s, e) => CreeazaCont();
            btnAscundeArata.Click += (s, e) => ComutaVizibilitateParola();
        }

        private void CreeazaCont()
        {
            string nume = textBoxNume.Text;
            string parola = textBoxParola.Text;
            if (string.IsNullOrEmpty(nume) || string.IsNullOrEmpty(parola))
            {
                MessageBox.Show("Numele si parola nu pot fi goale!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (ContExistaDeja(nume))
            {
                MessageBox.Show("Numele de utilizator exista deja!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SalveazaCont(nume, parola);

            MessageBox.Show("Contul a fost creat!", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
            DeschideFormLogin();
        }

        private bool ContExistaDeja(string nume)
        {
            string caleFisier = "conturi.txt";

            if (File.Exists(caleFisier))
            {
                var conturi = File.ReadAllLines(caleFisier);

                foreach (var cont in conturi)
                {
                    string[] dateCont = cont.Split('|');
                    if (dateCont[0] == nume)
                    {
                        return true; 
                    }
                }
            }

            return false;
        }

        private void SalveazaCont(string nume, string parola)
        {
            string caleFisier = "conturi.txt";
            if (!File.Exists(caleFisier))
            {
                File.WriteAllText(caleFisier, ""); 
            }
            string cont = $"{nume}|{parola}|0";
            File.AppendAllText(caleFisier, cont + Environment.NewLine);
        }

        private void DeschideFormLogin()
        {
            FormLogin formLogin = new FormLogin();
            formLogin.Show(); 
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