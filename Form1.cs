using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proiect
{
    public partial class Form1 : Form
    {
        public string NumeUtilizator { get; private set; } 
        public int PuncteUtilizator { get; private set; } 

        public Form1()
        {
            InitializeComponent();

            btnLogin.Click += (s, e) => DeschideFormLogin();

            listViewComanda.View = View.Details;
            listViewComanda.Columns.Add("Nume", 150); 
            listViewComanda.Columns.Add("Qty", 50);   
            listViewComanda.Columns.Add("Pret", 50);  

            
            btnMancare.Click += (s, e) => AfiseazaProduse(new List<Produs>
            {
                new Produs("Taco (150g)", "taco.jpeg", 30.00m),
                new Produs("Quesadilla (200g)", "quesadilla.jpeg", 20.00m),
                new Produs("Burrito (350g)", "burrito.jpeg", 25.00m),
                new Produs("Nachos (135g)", "nachos.jpeg", 18.50m)
            }, panelProduse);

            btnPuncte.Click += (s, e) => AfiseazaProdusePuncte(new List<Produs>
            {
            new Produs("Meniu Qsdilla", "meniuq.jpeg", 30), 
            new Produs("Meniu Diabet", "meniud.jpeg", 25), 
            new Produs("Meniu Burrito", "meniub.jpeg", 50), 
             new Produs("Meniu Taco", "meniut.jpeg", 25) 
            }, panelProduse);

            btnBauturi.Click += (s, e) => AfiseazaProduse(new List<Produs>
            {
                new Produs("Coca-Cola (400ml)", "cocacola.jpeg", 10.00m),
                new Produs("Fanta (400ml)", "fanta.jpeg", 10.00m),
                new Produs("Sprite (400ml)", "sprite.jpeg", 10.00m),
                new Produs("Bere (500ml)", "bere.jpeg", 10.00m)
            }, panelProduse);

            btnDesert.Click += (s, e) => AfiseazaProduse(new List<Produs>
            {
                new Produs("Înghetata", "icecream.jpeg", 15.00m),
                new Produs("Churros", "churros.jpeg", 20.00m),
                new Produs("Mexican Flan", "mexican flan.jpeg", 27.50m)
            }, panelProduse);

            btnSosuri.Click += (s, e) => AfiseazaProduse(new List<Produs>
            {
                new Produs("Ketchup (10g)", "ketchup.jpeg", 5.00m),
                new Produs("Maioneza (10g)", "mayo.jpeg", 5.00m),
                new Produs("BBQ (10g)", "bbqsauce.jpeg", 5.00m),
                new Produs("Cheddar (10g)", "cheddar.jpeg", 5.00m)
            }, panelProduse);  
        }

        private void AfiseazaProdusePuncte(List<Produs> produse, Panel panel)
        {
            panel.Controls.Clear(); 


            int yOffset = 10; 
            int imageWidth = 175;  
            int imageHeight = 175; 
            int spacing = 20; 
            int columnWidth = panel.Width / 2 - spacing; 
            int leftX = 10; 
            int rightX = leftX + columnWidth + spacing; 

            for (int i = 0; i < produse.Count; i++)
            {
                Produs produs = produse[i];

                
                int xPos = (i % 2 == 0) ? leftX : rightX;
                if (i % 2 == 0 && i > 0)
                    yOffset += imageHeight + 100; 

               
                Label labelProdus = new Label
                {
                    Text = produs.Nume,
                    AutoSize = true,
                    Font = new Font("Britannic bold", 12, FontStyle.Bold),
                    ForeColor = Color.Black,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Location = new Point(xPos, yOffset)
                };
                panel.Controls.Add(labelProdus);

                
                PictureBox pictureProdus = new PictureBox
                {
                    ImageLocation = Path.Combine("Resources", produs.Imagine),
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Size = new Size(imageWidth, imageHeight),
                    Location = new Point(xPos, yOffset + labelProdus.Height + 10)
                };
                panel.Controls.Add(pictureProdus);

              
                Label labelPuncte = new Label
                {
                    Text = $"{produs.Pret} puncte",
                    AutoSize = true,
                    Font = new Font("Arial", 10, FontStyle.Bold),
                    ForeColor = Color.Black,
                    Location = new Point(xPos, yOffset + labelProdus.Height + imageHeight + 10)
                };
                panel.Controls.Add(labelPuncte);

               
                Button btnCumpara = new Button
                {
                    Text = "Cumpără",
                    Size = new Size(75, 30),
                    Location = new Point(xPos, yOffset + labelProdus.Height + imageHeight + 40)
                };
                btnCumpara.Click += (s, e) => CumparaCuPuncte(produs);
                panel.Controls.Add(btnCumpara);
            }
        }

        private void ActualizeazaLabelPuncte()
        {
            labelUtilizator.Text = $"{NumeUtilizator} ({PuncteUtilizator} puncte)";
        }

        private void CumparaCuPuncte(Produs produs)
        {
            
            if (PuncteUtilizator >= produs.Pret)
            {
                
                PuncteUtilizator -= (int)produs.Pret;
                ActualizeazaPuncteInFisier(NumeUtilizator, PuncteUtilizator);
             
                ActualizeazaLabelPuncte();
                MessageBox.Show($"Ai cumpărat {produs.Nume} ", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                DeschideFormBonFinal(produs);
            }
            else
            {
                MessageBox.Show("Nu ai suficiente puncte pentru a cumpăra acest produs.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeschideFormLogin()
        {
            FormLogin formLogin = new FormLogin();
            formLogin.ShowDialog();
        }

        private void DeschideFormPlata()
        { 
            decimal totalPlata = CalculeazaTotalComanda();
            FormPlata formPlata = new FormPlata(listViewComanda, totalPlata.ToString());
            formPlata.Show();
        }



        private decimal CalculeazaTotalComanda()
        {
            decimal total = 0;
            foreach (ListViewItem item in listViewComanda.Items)
            {
                total += decimal.Parse(item.SubItems[2].Text);
            }
            return total;
        }

        private void DeschideFormBonFinal(Produs produs)
        {
            FormBonFinal formBonFinal = new FormBonFinal();
            formBonFinal.AfiseazaBonPlataCuPuncte(produs.Nume, (int)produs.Pret);
            formBonFinal.Show();
        }

        private void AfiseazaProduse(List<Produs> produse, Panel panel)
        {
            panel.Controls.Clear(); 


            int yOffset = 10; 
            int imageWidth = 175;  
            int imageHeight = 175; 
            int spacing = 20;
            int columnWidth = panel.Width / 2 - spacing; 
            int leftX = 10;
            int rightX = leftX + columnWidth + spacing; 

            for (int i = 0; i < produse.Count; i++)
            {
                Produs produs = produse[i];
                int xPos = (i % 2 == 0) ? leftX : rightX;
                if (i % 2 == 0 && i > 0)
                    yOffset += imageHeight + 100; 
                Label labelProdus = new Label
                {
                    Text = produs.Nume,
                    AutoSize = true,
                    Font = new Font("Britannic bold", 12, FontStyle.Bold),
                    ForeColor = Color.Black,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Location = new Point(xPos, yOffset)
                };
                panel.Controls.Add(labelProdus);

                PictureBox pictureProdus = new PictureBox
                {
                    ImageLocation = Path.Combine("Resources", produs.Imagine),
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Size = new Size(imageWidth, imageHeight),
                    Location = new Point(xPos, yOffset + labelProdus.Height + 10)
                };
                panel.Controls.Add(pictureProdus);
                Label labelPret = new Label
                {
                    Text = $"{produs.Pret} lei",
                    AutoSize = true,
                    Font = new Font("Arial", 10, FontStyle.Bold),
                    ForeColor = Color.Black,
                    Location = new Point(xPos, yOffset + labelProdus.Height + imageHeight + 10)
                };
                panel.Controls.Add(labelPret);
                Button btnAdauga = new Button
                {
                    Text = "Adaugă",
                    Size = new Size(75, 30),
                    Location = new Point(xPos, yOffset + labelProdus.Height + imageHeight + 40)
                };
                btnAdauga.Click += (s, e) => AdaugaInComanda(produs);
                panel.Controls.Add(btnAdauga);
            }
        }

        private void AdaugaInComanda(Produs produs)
        { 
            ListViewItem item = listViewComanda.Items.Cast<ListViewItem>()
                .FirstOrDefault(i => i.Text == produs.Nume);

            if (item != null)
            { 
                int qty = int.Parse(item.SubItems[1].Text);
                item.SubItems[1].Text = (qty + 1).ToString();

        
                decimal pret = decimal.Parse(item.SubItems[2].Text);
                item.SubItems[2].Text = (pret + produs.Pret).ToString("0.00");
            }
            else
            {
            
                ListViewItem newItem = new ListViewItem(produs.Nume);
                newItem.SubItems.Add("1");
                newItem.SubItems.Add(produs.Pret.ToString("0.00"));
                listViewComanda.Items.Add(newItem);
            }

       
            ActualizeazaTotal();
        }
        private void ActualizeazaTotal()
        {
            decimal total = 0;
            foreach (ListViewItem item in listViewComanda.Items)
            {
                decimal pret = decimal.Parse(item.SubItems[2].Text);
                total += pret;
            }
            txtTotal.Text = total.ToString("0.00"); 
        }

  
        public void UtilizatorLogat(string nume, int puncte)
        {
            NumeUtilizator = nume;
            PuncteUtilizator = puncte;

   
            btnLogin.Visible = false;
            labelUtilizator.Text = $"{nume} ({puncte} puncte)";
            labelUtilizator.Visible = true;
        }
        public void ActualizeazaPuncte(int puncteNoi)
        {
            PuncteUtilizator += puncteNoi;
            labelUtilizator.Text = $"{NumeUtilizator} ({PuncteUtilizator} puncte)";

            ActualizeazaPuncteInFisier(NumeUtilizator, PuncteUtilizator);
        }

        private void ActualizeazaPuncteInFisier(string nume, int puncte)
        {
            string caleFisier = "conturi.txt";

            if (File.Exists(caleFisier))
            {
                var conturi = File.ReadAllLines(caleFisier);
                var conturiActualizate = new List<string>();

                foreach (var cont in conturi)
                {
                    string[] dateCont = cont.Split('|');
                    if (dateCont[0] == nume)
                    {
                        conturiActualizate.Add($"{dateCont[0]}|{dateCont[1]}|{puncte}");
                    }
                    else
                    {
                        conturiActualizate.Add(cont);
                    }
                }
                File.WriteAllLines(caleFisier, conturiActualizate);
            }
        }
        private void btnPlata_Click(object sender, EventArgs e)
        {
            DeschideFormPlata();
        }

        public class Produs
        {
            public string Nume { get; set; }
            public string Imagine { get; set; }
            public decimal Pret { get; set; }

            public Produs(string nume, string imagine, decimal pret)
            {
                Nume = nume;
                Imagine = imagine;
                Pret = pret;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}