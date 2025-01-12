private void BTNenviarRelatorio_Click(object sender, EventArgs e)
{
    if (TXTtextoRelatorio.Text.Length < 15)
    {
        MessageBox.Show("Relatório não enviado devido a um problema: !\nMensagem curta demais.", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
        TXTtextoRelatorio.Focus();
    }
    else if (TXTtextoRelatorio.Text.Length == 0)
    {
        MessageBox.Show("Relatório não enviado devido a um problema: !\nMensagem vazia.", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
        TXTtextoRelatorio.Focus();
    }
    else
    {
        try
        {
            string sqlIdAssunto = "SELECT id_assunto FROM assuntos WHERE nome_assunto = @nome_assunto";
            SqlCommand cmdIdAssunto = new SqlCommand(sqlIdAssunto, Program.TSqlConnect.TConnection);
            cmdIdAssunto.Parameters.Add("@nome_assunto", SqlDbType.VarChar).Value = CMBassuntos.Text;
            int idAssunto = (int)cmdIdAssunto.ExecuteScalar();

            string sql = "INSERT INTO relatorios (id_assunto, texto, nome_funcionario, data) VALUES (@assunto, @texto, @nome_funcionario, @data)";
            string dataString = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            data = DateTime.ParseExact(dataString, "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture);

            SqlCommand cmd = new SqlCommand(sql, Program.TSqlConnect.TConnection);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@assunto", SqlDbType.Int).Value = idAssunto;
            cmd.Parameters.Add("@texto", SqlDbType.Text).Value = TXTtextoRelatorio.Text;
            cmd.Parameters.Add("@nome_funcionario", SqlDbType.VarChar).Value = Nome;
            cmd.Parameters.Add("@data", SqlDbType.DateTime).Value = data;
            cmd.ExecuteNonQuery();

            MessageBox.Show("Relatório registado com sucesso!", "INFORMAÇÃO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            TXTtextoRelatorio.Clear();


        }

        catch (Exception ex)
        {
            MessageBox.Show("Relatório não foi registado devido a um erro:\n " + ex.Message, "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }


}
