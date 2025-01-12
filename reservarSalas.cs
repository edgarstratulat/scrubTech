 private void BTNreservarsala_Click(object sender, EventArgs e)
 {
     
     if (TXTprofessor.Text.Length < 3)
     {
         MessageBox.Show("Reserva não registada devido a um problema:\nNome do professor não registado.", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
         TXTprofessor.Focus();
     }
     else if (TXTfinalidade.Text.Length < 5)
     {
         MessageBox.Show("Reserva não registada devido a um problema:\nPreencha corretamente a finalidade da reserva.", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
         TXTfinalidade.Focus();
     }
     else
     {

         DateTime dataHora = dateTimePicker1.Value.Date;

         string diasemana = dataHora.DayOfWeek.ToString();
         if (diasemana == "Monday")
         {
             diasemana = "Segunda";
         }
         if (diasemana == "Tuesday")
         {
             diasemana = "Terça";
         }
         if (diasemana == "Wednesday")
         {
             diasemana = "Quarta";
         }
         if (diasemana == "Thursday")
         {
             diasemana = "Quinta";
         }
         if (diasemana == "Friday")
         {
             diasemana = "Sexta";
         }




         TimeSpan horaInicio = TimeSpan.Parse(CMBhoras.Text.Substring(0, 5));
         TimeSpan horaFim = TimeSpan.Parse(CMBhoras.Text.Substring(8, 5));
         int salaID = (int)CMBsalasReserva.SelectedValue;
         string professor = TXTprofessor.Text;
         string finalidade = TXTfinalidade.Text;

         string query = "SELECT * FROM Reservas WHERE SalaID = @salaID AND Data = @data AND ((HoraInicio >= @horaInicio AND HoraInicio < @horaFim) OR (HoraFim > @horaInicio AND HoraFim <= @horaFim) OR (HoraInicio <= @horaInicio AND HoraFim >= @horaFim))";
         SqlCommand command = new SqlCommand(query, Program.TSqlConnect.TConnection);
         command.Parameters.AddWithValue("@salaID", salaID);
         command.Parameters.AddWithValue("@data", dataHora.Date);
         command.Parameters.AddWithValue("@horaInicio", horaInicio);
         command.Parameters.AddWithValue("@horaFim", horaFim);

         SqlDataReader reader = command.ExecuteReader();

         if (reader.HasRows)
         {

             MessageBox.Show("Reserva não registada devido a um problema:\nJá existe uma reserva para essa sala nesse horário.", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);

             reader.Close();
         }
         else
         {
             reader.Close();
             string horariosQuery = "SELECT * FROM Horarios WHERE SalaID = @salaID AND DiaSemana = @diaSemana AND ((HoraInicio <= @horaInicio AND HoraFim >= @horaInicio) OR (HoraInicio <= @horaFim AND HoraFim >= @horaFim))";
             SqlCommand horariosCommand = new SqlCommand(horariosQuery, Program.TSqlConnect.TConnection);

             horariosCommand.Parameters.AddWithValue("@salaID", salaID);
             horariosCommand.Parameters.AddWithValue("@diaSemana", diasemana);
             horariosCommand.Parameters.AddWithValue("@horaInicio", horaInicio);
             horariosCommand.Parameters.AddWithValue("@horaFim", horaFim);




             SqlDataReader horariosReader = horariosCommand.ExecuteReader();

             if (horariosReader.HasRows)
             {
                 MessageBox.Show("Reserva não registada devido a um problema:\nJá existe uma sala a ser ocupada nesse horário.", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                 horariosReader.Close();
             }
             else
             {
                 horariosReader.Close();


                 string query2= "INSERT INTO Reservas (SalaID, Data, HoraInicio, HoraFim, Professor, Finalidade) VALUES (@salaID, @data, @horaInicio, @horaFim, @professor, @finalidade)";
                 SqlCommand command2 = new SqlCommand(query2, Program.TSqlConnect.TConnection);
                 command2.Parameters.AddWithValue("@salaID", salaID);
                 command2.Parameters.AddWithValue("@data", dataHora.Date);
                 command2.Parameters.AddWithValue("@horaInicio", horaInicio);
                 command2.Parameters.AddWithValue("@horaFim", horaFim);
                 command2.Parameters.AddWithValue("@professor", professor);
                 command2.Parameters.AddWithValue("@finalidade", finalidade);



                 command2.ExecuteNonQuery();

                 mostrar_reservas();
                 TXTprofessor.Clear();
                 TXTfinalidade.Clear();





                 string query_movimentos = "INSERT INTO Movimentos (tipo_movimento, Detalhes, funcionario, data) VALUES (@tipo_movimento, @detalhes, @funcionario, @data)";
                 SqlCommand command_movimentos = new SqlCommand(query_movimentos, Program.TSqlConnect.TConnection);
                 command_movimentos.Parameters.AddWithValue("@tipo_movimento", "Adicionar Reserva");
                 command_movimentos.Parameters.AddWithValue("@detalhes", "Foi adicionada a reserva à sala \""+CMBsalasReserva.Text+ "\" para o dia \""+ dataHora.ToString("dd-MM-yyyy") + "\" no horários de \""+horaInicio+" - "+ horaFim +"\" pedida pelo professor \""+professor+"\" com a finalidade \""+finalidade+"\"");
                 command_movimentos.Parameters.AddWithValue("@funcionario", Nome);
                 command_movimentos.Parameters.AddWithValue("@data", DateTime.Now);

                 command_movimentos.ExecuteNonQuery();
             }
         }
     }
 }
