using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Drawing;

using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace İleriProgramlamaProje
{
   
    public partial class Form1 : Form
    {
        // Veritabanı yöneticisi
        DatabaseManager db = new DatabaseManager();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ListeyiDoldur();
            if (cmbLanguage.Items.Count > 0) cmbLanguage.SelectedIndex = 0;
            cmbLanguage.Items.Clear(); 
            cmbLanguage.Items.AddRange(new object[] {
        "C#",
        "Python",
        "SQL",
        "Java",
        "JavaScript",
        "C++",
        "HTML/CSS",
        "PHP",
        "C"
          });

            
            btnSave.Click += BtnSave_Click;
            btnDelete.Click += BtnDelete_Click;
            btnCopy.Click += BtnCopy_Click;
            txtSearch.TextChanged += TxtSearch_TextChanged;
            lstSnippets.SelectedIndexChanged += LstSnippets_SelectedIndexChanged;
            rtbCode.TextChanged += RtbCode_TextChanged;
            SetPlaceholder(txtTitle, "Başlık Girin");
            SetPlaceholder(txtSearch, "Ara");
            SetPlaceholder(cmbLanguage, "Dil seçiniz");
            btnUpdate.Click += BtnUpdate_Click;
        }

        private void ListeyiDoldur()
        {
            lstSnippets.Items.Clear();
            var snippets = db.GetAllSnippets();
            foreach (var s in snippets)
            {
                lstSnippets.Items.Add(s);
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            
            if (txtTitle.Text == "Başlık Girin..." || string.IsNullOrWhiteSpace(rtbCode.Text))
            {
                MessageBox.Show("Lütfen başlık ve kod alanını doldurun.");
                return;
            }

            Snippet yeniSnippet = new Snippet
            {
                Title = txtTitle.Text,
                CodeContent = rtbCode.Text,
                Language = cmbLanguage.Text
            };

            db.AddSnippet(yeniSnippet);
            ListeyiDoldur();
            MessageBox.Show("Kaydedildi!");
            txtTitle.Clear();
            rtbCode.Clear();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (lstSnippets.SelectedItem == null) return;

            Snippet secilen = (Snippet)lstSnippets.SelectedItem;
            DialogResult cevap = MessageBox.Show("Silinsin mi?", "Onay", MessageBoxButtons.YesNo);

            if (cevap == DialogResult.Yes)
            {
                db.DeleteSnippet(secilen.Id);
                ListeyiDoldur();
                txtTitle.Clear();
                rtbCode.Clear();
            }
        }

        private void BtnCopy_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(rtbCode.Text))
            {
                Clipboard.SetText(rtbCode.Text);
                MessageBox.Show("Kopyalandı!");
            }
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            
            string aranan = txtSearch.Text;

            lstSnippets.Items.Clear();

            List<Snippet> sonuclar;

            if (string.IsNullOrWhiteSpace(aranan))
            {
                sonuclar = db.GetAllSnippets(); // Boşsa hepsini getir
            }
            else
            {
                sonuclar = db.SearchSnippets(aranan); // Doluysa ara
            }

            // Sonuçları listeye ekle
            foreach (var s in sonuclar)
            {
                lstSnippets.Items.Add(s);
            }
        }

        private void LstSnippets_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstSnippets.SelectedItem == null) return;

            Snippet secilen = (Snippet)lstSnippets.SelectedItem;
            txtTitle.Text = secilen.Title;
            rtbCode.Text = secilen.CodeContent;
            cmbLanguage.Text = secilen.Language;
            Renklendir();
        }

        


private void BtnUpdate_Click(object sender, EventArgs e)
        {
            
            if (lstSnippets.SelectedItem == null)
            {
                MessageBox.Show("Lütfen güncellenecek kaydı listeden seçin.");
                return;
            }

            
            if (txtTitle.Text == "Başlık Girin..." || string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                MessageBox.Show("Lütfen geçerli bir başlık girin.");
                return;
            }

            
            Snippet secilenEskiVeri = (Snippet)lstSnippets.SelectedItem;

           
            Snippet guncelVeri = new Snippet
            {
                Id = secilenEskiVeri.Id, 
                Title = txtTitle.Text,
                CodeContent = rtbCode.Text,
                Language = cmbLanguage.Text
            };

            
            db.UpdateSnippet(guncelVeri);

            
            ListeyiDoldur();
            MessageBox.Show("Kayıt başarıyla güncellendi!");

            
            txtTitle.Text = "Başlık Girin..."; 
            txtTitle.ForeColor = Color.Gray;
            rtbCode.Clear();
        }


        private void RtbCode_TextChanged(object sender, EventArgs e)
        {
            Renklendir();
        }

        private void Renklendir()
        {
            int cursorIndex = rtbCode.SelectionStart;
            rtbCode.SelectAll();
            rtbCode.SelectionColor = Color.Black;
            rtbCode.SelectionFont = new Font("Consolas", 10, FontStyle.Regular);

            string keywords = @"\b(public|private|protected|class|void|int|string|return|if|else|for|while|new|using)\b";
            Regex rex = new Regex(keywords);

            foreach (Match m in rex.Matches(rtbCode.Text))
            {
                rtbCode.Select(m.Index, m.Length);
                rtbCode.SelectionColor = Color.Blue;
                rtbCode.SelectionFont = new Font("Consolas", 10, FontStyle.Bold);
            }

            rtbCode.SelectionLength = 0;
            rtbCode.SelectionStart = cursorIndex;
            rtbCode.SelectionColor = Color.Black;
        }
        
        private void SetPlaceholder(Control kutu, string yerTutucuMetni)
        {
            
            kutu.Text = yerTutucuMetni;
            kutu.ForeColor = Color.Black;

            
            kutu.Enter += (sender, e) =>
            {
                if (kutu.Text == yerTutucuMetni)
                {
                    kutu.Text = "";
                    kutu.ForeColor = Color.Black;
                }
            };

            
            kutu.Leave += (sender, e) =>
            {
                if (string.IsNullOrWhiteSpace(kutu.Text))
                {
                    kutu.Text = yerTutucuMetni;
                    kutu.ForeColor = Color.Black;
                }
            };
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }

    

    public class Snippet
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string CodeContent { get; set; }
        public string Language { get; set; }

        public override string ToString()
        {
            return $"[{Language}] {Title}";
        }
    }

    public class DatabaseManager
    {
        private string dbFile = "MySnippets.db";
        private string connectionString;

        public DatabaseManager()
        {
            connectionString = $"Data Source={dbFile};Version=3;";

            // Dosya yoksa oluştur (Boş dosya oluşturur)


            if (!File.Exists(dbFile))
            {
                SQLiteConnection.CreateFile(dbFile);
            }

            
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string sql = @"
            CREATE TABLE IF NOT EXISTS Snippets (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Title TEXT,
                CodeContent TEXT,
                Language TEXT
            )";
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void AddSnippet(Snippet s)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string sql = "INSERT INTO Snippets (Title, CodeContent, Language) VALUES (@t, @c, @l)";
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@t", s.Title);
                    cmd.Parameters.AddWithValue("@c", s.CodeContent);
                    cmd.Parameters.AddWithValue("@l", s.Language);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteSnippet(int id)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string sql = "DELETE FROM Snippets WHERE Id = @id";
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Snippet> GetAllSnippets()
        {
            var list = new List<Snippet>();
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT * FROM Snippets ORDER BY Id DESC";
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new Snippet
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Title = reader["Title"].ToString(),
                                CodeContent = reader["CodeContent"].ToString(),
                                Language = reader["Language"].ToString()
                            });
                        }
                    }
                }
            }
            return list;
        }
        

        public List<Snippet> SearchSnippets(string keyword)
        {
            var list = new List<Snippet>();
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
               
                string sql = "SELECT * FROM Snippets WHERE Title LIKE @k OR CodeContent LIKE @k OR Language LIKE @k ORDER BY Id DESC";

                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    
                    cmd.Parameters.AddWithValue("@k", "%" + keyword + "%");

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new Snippet
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Title = reader["Title"].ToString(),
                                CodeContent = reader["CodeContent"].ToString(),
                                Language = reader["Language"].ToString()
                            });
                        }
                    }
                }
            }
            return list;
        }

        
        public void UpdateSnippet(Snippet s)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                
                string sql = "UPDATE Snippets SET Title=@t, CodeContent=@c, Language=@l WHERE Id=@id";
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@t", s.Title);
                    cmd.Parameters.AddWithValue("@c", s.CodeContent);
                    cmd.Parameters.AddWithValue("@l", s.Language);
                    cmd.Parameters.AddWithValue("@id", s.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}