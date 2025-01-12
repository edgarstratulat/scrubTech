 public Form1()
 {
     InitializeComponent();
     Program.TSqlConnect = new Conexao();
     Program.TSqlConnect.Ligar();

 }

 private void BTNsair_Click(object sender, EventArgs e)
 {
     this.Close();
 }

 public void login()
 {
     if (TXTemail.Text.Length < 4)
     {
         MessageBox.Show("ERRO: Preencha o email!", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
         TXTemail.Focus();
     }
     else
     {
         string query = "SELECT permissao, nome, sexo,email FROM login WHERE email = @email AND password= @password";
         SqlCommand comando = new SqlCommand(query, Program.TSqlConnect.TConnection);
         comando.Parameters.AddWithValue("@email", TXTemail.Text);
         comando.Parameters.AddWithValue("@password", TXTpassword.Text);

         using (SqlDataReader reader = comando.ExecuteReader())
         {
             if (reader.HasRows)
             {
                 reader.Read();
                 string tipo = reader.GetString(0);
                 string nome = reader.GetString(1);
                 string sexo = reader.GetString(2);
                 string email = reader.GetString(3);
                 reader.Close();

                 if (tipo == "Administrador")
                 {


                     FormAdministrador admin = new FormAdministrador();
                     this.Hide();
                     admin.Nome = nome;
                     admin.ShowDialog();

                 }
                 else
                 {
                     FormFuncionario principal = new FormFuncionario();
                     principal.Nome = nome;
                     principal.Sexo = sexo;
                     principal.Email = email;
                     this.Hide();
                     principal.ShowDialog();
                 }
             }
             else
             {
                 MessageBox.Show("ERRO: Email ou password não correspondem ao utilizador!", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                 TXTpassword.Clear();
                 TXTemail.Focus();
             }
         }
     }
 }
 private void BTNlogin_Click(object sender, EventArgs e)
 {

     login();
 }

 private void Form1_Load(object sender, EventArgs e)
 {
     TXTemail.Focus();

 }

 private void TXTemail_TextChanged(object sender, EventArgs e)
 {

 }

 private void Form1_KeyDown(object sender, KeyEventArgs e)
 {
     
 }

 private void TXTemail_KeyDown(object sender, KeyEventArgs e)
 {
     if (e.KeyCode == Keys.Down)
     {
         TXTpassword.Focus();
     }
 }

 private void TXTpassword_KeyDown(object sender, KeyEventArgs e)
 {
     if (e.KeyCode == Keys.Up)
     {
         TXTemail.Focus();
     }

     if (e.KeyCode == Keys.Enter)
     {
         login();
     }
 }
