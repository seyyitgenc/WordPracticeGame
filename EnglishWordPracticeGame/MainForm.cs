using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;

namespace EnglishWordPracticeGame
{
    public class MainForm : Form
    {
        //fields
        private ArrayList words_turkish = new ArrayList();
        private ArrayList words_english = new ArrayList();

        int wordindex;
        Random rand = new Random();

        private int point;
        int point2 = 10;
        private Label lbl_turkish = new Label();
        private Label lbl_english = new Label();
        private Label lbl_status = new Label();
        private Label lbl_point = new Label();

        private TextBox txt_turkish = new TextBox();
        private TextBox txt_english = new TextBox();

        private Button btn_add = new Button();
        private Button btn_start = new Button();
        private Button btn_restart = new Button();
        private Button btn_try = new Button();

        public MainForm()
        {
            this.Text = "Main Form";
            this.Size = new Size(500, 500);
            this.BackColor = Color.Aqua;
            CenterToScreen();
            Render();
        }
        void Render()
        {
            //label customization
            lbl_english.Text = "İngilizce Kelimeniz : ";
            lbl_english.AutoSize = true;
            lbl_english.Font = new Font("Mono", 12, FontStyle.Bold);
            lbl_english.Location = new Point(20, 140);
            lbl_english.ForeColor = Color.Black;

            lbl_turkish.Text = "Türkçe Kelimeniz : ";
            lbl_turkish.AutoSize = true;
            lbl_turkish.Font = new Font("Mono", 12, FontStyle.Bold);
            lbl_turkish.Location = new Point(20, 100);
            lbl_turkish.ForeColor = Color.Black;

            lbl_status.AutoSize = true;
            lbl_status.Font = new Font("Mono", 12, FontStyle.Bold);
            lbl_status.ForeColor = Color.DarkGreen;
            lbl_status.Location = new Point(20, 300);

            lbl_point.AutoSize = true;
            lbl_point.Font = new Font("Mono", 12, FontStyle.Bold);
            lbl_point.ForeColor = Color.DarkGreen;
            lbl_point.Location = new Point(10, 10);

            //textbox customization
            txt_english.Size = new Size(200, 30);
            txt_english.Font = new Font("Mono", 10, FontStyle.Regular);
            txt_english.Location = new Point(250, 140);

            txt_turkish.Size = new Size(200, 30);
            txt_turkish.Font = new Font("Mono", 10, FontStyle.Regular);
            txt_turkish.Location = new Point(250, 100);
            //button customization
            btn_add.Text = "Diziye Ekle";
            btn_add.AutoSize = true;
            btn_add.BackColor = Color.Bisque;
            btn_add.ForeColor = Color.Black;
            btn_add.Font = new Font("Mono", 12, FontStyle.Bold);
            btn_add.Location = new Point(200, 200);

            btn_start.Text = "Oyuna Başla";
            btn_start.AutoSize = true;
            btn_start.BackColor = Color.Bisque;
            btn_start.ForeColor = Color.Black;
            btn_start.Font = new Font("Mono", 12, FontStyle.Bold);
            btn_start.Location = new Point(200, 400);

            btn_try.Text = "Dene";
            btn_try.AutoSize = true;
            btn_try.BackColor = Color.Bisque;
            btn_try.ForeColor = Color.Black;
            btn_try.Font = new Font("Mono", 12, FontStyle.Bold);
            btn_try.Location = new Point(210, 250);

            btn_restart.Text = "Başa Dön";
            btn_restart.AutoSize = true;
            btn_restart.BackColor = Color.Bisque;
            btn_restart.ForeColor = Color.Black;
            btn_restart.Font = new Font("Mono", 12, FontStyle.Bold);
            btn_restart.Location = new Point(20, 40);

            //adding controls
            this.Controls.Add(lbl_point);
            this.Controls.Add(txt_turkish);
            this.Controls.Add(txt_english);
            this.Controls.Add(btn_add);
            this.Controls.Add(btn_try);
            this.Controls.Add(lbl_english);
            this.Controls.Add(lbl_turkish);
            this.Controls.Add(btn_start);
            this.Controls.Add(btn_restart);
            this.Controls.Add(lbl_status);

            btn_add.Click += Btn_Add_Click;
            btn_start.Click += Btn_Start_Click;
            btn_restart.Click += Btn_Restart_Click;
            btn_try.Click += Btn_Try_Click;
            this.Load += Handle_Load;

        }
        //form load event
        void Handle_Load(object sender, EventArgs e)
        {
            btn_restart.Enabled = false;
            btn_try.Enabled = false;
        }
        //btn add click event for adding new words
        void Btn_Add_Click(object sender, EventArgs e)
        {
            words_turkish.Add(txt_turkish.Text);//adding turkish words
            words_english.Add(txt_english.Text);//adding english words
        }
        //btn start click event for randomize the english word
        void Btn_Start_Click(object sender, EventArgs e)
        {
            if (words_english.Count == 0)
                MessageBox.Show("Lütfen önce kelime ekleyiniz", "Warning", MessageBoxButtons.OK);
            else
            {
                wordindex = rand.Next(words_english.Count);
                txt_english.Text = words_english[wordindex].ToString();//randomizing the english word
                lbl_status.Text = "Yeni ingilizce kelimeniz tahmin için hazır.";
                txt_turkish.Text = "";
                txt_english.Enabled = false;
                btn_add.Enabled = false;
                btn_try.Enabled = true;
                btn_restart.Enabled = true;
            }
        }
        //btn restart click event for add words again
        void Btn_Restart_Click(object sender, EventArgs e)
        {
            txt_english.Text = "";
            txt_turkish.Text = "";
            lbl_status.Text = "Tekrar kelime ekleyebilirsiniz.";
            btn_add.Enabled = true;
            txt_english.Enabled = true;
            btn_try.Enabled = false;
            point = 0;
        }
        //btn try click event for compare the words
        void Btn_Try_Click(object sender, EventArgs e)
        {
            int wrongletters = 0;
            string txtcompare = txt_turkish.Text;
            string wordscompare = words_turkish[wordindex].ToString();
            int length;

            //finding small length for loop
            if (txtcompare.Length < wordscompare.Length)
                length = txtcompare.Length;
            else
                length = wordscompare.Length;

            for (int i = 0; i < length; i++)//comparing letters
            {
                if (txtcompare[i] != wordscompare[i])
                    wrongletters++;
            }
            //if all letters correct
            if (wrongletters == 0)
            {
                if (txtcompare.Length < wordscompare.Length)
                {
                    lbl_status.Text = "Kelimeyi eksik bildiniz\npuanınız 4 arttı";
                    point += 4;
                    wordindex = rand.Next(words_english.Count);
                    txt_english.Text = words_english[wordindex].ToString();//randomizing the english word
                }
                else if (txtcompare.Length > wordscompare.Length)
                {
                    lbl_status.Text = "Kelimeniz fazla uzun lütfen tekrar deneyin";
                }
                else
                {
                    lbl_status.Text = "Tebrikleri kelimeyi doğru bildiniz\npuanınız 10 arttı";
                    point += 10;
                    wordindex = rand.Next(words_english.Count);
                    txt_english.Text = words_english[wordindex].ToString();//randomizing the english word
                }
            }
            //if 1 letter is wrong
            else if (wrongletters == 1)
            {
                lbl_status.Text = "Kelimenizde 1 hata bulundu\npuanınız 8 arttı";
                point += 8;
                wordindex = rand.Next(words_english.Count);
                txt_english.Text = words_english[wordindex].ToString();//randomizing the english word
            }
            else
                lbl_status.Text = "Kelimenizde 1'den çok hata var \nlütfen tekrar deneyin";

            lbl_point.Text = "Point : " + point;
        }

    }
}
