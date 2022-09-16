using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace LButility
{
    public class Utility
    {

        static SqlCommand cmd;
        bool evaluar = false;
        SqlTransaction sqlTransaction;

        public bool Guardar(string nombreprocediemnto, List<SqlParameter> parametro)
        {
            if (parametro == null)
            {
                return false;
            }

            try
            {
                abrirConexion();
                cnn.CreateCommand();
                sqlTransaction = cnn.BeginTransaction();

                cmd = new SqlCommand(nombreprocediemnto, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddRange(parametro.ToArray());

                if (cmd.ExecuteNonQuery() > 0)
                {
                    sqlTransaction.Commit();
                    evaluar = true;

                }

            }
            catch (Exception ex)
            {
                sqlTransaction.Rollback();
                MessageBox.Show($"Error al ejecutar el {nombreprocediemnto}   Clase" + ex.Message);
                evaluar = false;
            }

            return evaluar;
        }

        public bool Guardar(string nombreprocediemnto1, string nombreprocediemnto2, List<SqlParameter> parametro1, List<List<SqlParameter>> parametro2)
        {


            if (string.IsNullOrEmpty(nombreprocediemnto1) || string.IsNullOrEmpty(nombreprocediemnto1))
            {
                return false;
            }

            try
            {
                abrirConexion();

                sqlTransaction = cnn.BeginTransaction();

                cmd = cnn.CreateCommand();
                cmd.Transaction = sqlTransaction;
                cmd.CommandText = nombreprocediemnto1;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddRange(parametro1.ToArray());

                SqlCommand cmd2 = cnn.CreateCommand();
                cmd2.Transaction = sqlTransaction;
                cmd2.CommandText = nombreprocediemnto2;
                cmd2.CommandType = CommandType.StoredProcedure;

                int rowsAffected = 0;

                foreach (List<SqlParameter> parametros in parametro2)
                {
                    cmd2.Parameters.AddRange(parametros.ToArray());
                    rowsAffected += cmd2.ExecuteNonQuery();
                    cmd2.Parameters.Clear();
                }

                if (cmd.ExecuteNonQuery() > 0 && rowsAffected > 0)
                {
                    sqlTransaction.Commit();
                    evaluar = true;
                }

            }
            catch (Exception ex)
            {
                sqlTransaction.Rollback();
                MessageBox.Show($"Error al ejecutar el {nombreprocediemnto1},{nombreprocediemnto2}   Clase" + ex.Message);
                evaluar = false;

            }


            return evaluar;

        }

        public bool Guardar(string nombreprocediemnto1, string nombreprocediemnto2, string nombreprocediemiento3, List<SqlParameter> parametro1, List<List<SqlParameter>> parametro2, List<List<SqlParameter>> parametro3)
        {
            if (string.IsNullOrEmpty(nombreprocediemnto1) || string.IsNullOrEmpty(nombreprocediemnto2) || string.IsNullOrEmpty(nombreprocediemiento3))
            {
                return false;
            }

            try
            {
                abrirConexion();

                sqlTransaction = cnn.BeginTransaction();

                cmd = cnn.CreateCommand();
                cmd.Transaction = sqlTransaction;
                cmd.CommandText = nombreprocediemnto1;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddRange(parametro1.ToArray());

                SqlCommand cmd2 = cnn.CreateCommand();
                cmd2.Transaction = sqlTransaction;
                cmd2.CommandText = nombreprocediemnto2;
                cmd2.CommandType = CommandType.StoredProcedure;


                SqlCommand cmd3 = cnn.CreateCommand();
                cmd3.Transaction = sqlTransaction;
                cmd3.CommandText = nombreprocediemiento3;
                cmd3.CommandType = CommandType.StoredProcedure;

                int rowsAffectedTwo = 0;

                int rowsAffected = 0;



                foreach (List<SqlParameter> parametros in parametro2)
                {
                    cmd2.Parameters.AddRange(parametros.ToArray());
                    rowsAffected += cmd2.ExecuteNonQuery();
                    cmd2.Parameters.Clear();
                }

                foreach (List<SqlParameter> parameters in parametro3)
                {
                    cmd3.Parameters.AddRange(parameters.ToArray());
                    rowsAffectedTwo += cmd3.ExecuteNonQuery();
                    cmd3.Parameters.Clear();
                }

                if (cmd.ExecuteNonQuery() > 0 && rowsAffected > 0 && rowsAffectedTwo > 0)
                {
                    sqlTransaction.Commit();
                    evaluar = true;
                }
            }
            catch (Exception ex)
            {
                sqlTransaction.Rollback();
                MessageBox.Show($"Error al ejecutar el {nombreprocediemnto1},{nombreprocediemnto2},{nombreprocediemiento3}   Clase" + ex.Message);
                evaluar = false;

            }


            return evaluar;
        }

        public bool Update(string select)
        {
            if (string.IsNullOrEmpty(select ))
            {
                return false;
            }

            try
            {
                abrirConexion();
                
                cmd = new SqlCommand(select, cnn);
                cmd.CommandType = CommandType.Text;

                if (cmd.ExecuteNonQuery() > 0)
                {
                    evaluar = true;
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show($"Error al eecutar el Procedimiento {select}  Clase" + ex.Message);
                evaluar = false;

            }
            return evaluar;

        }

        //public int Update(string select)
        //{
        //    if (string.IsNullOrEmpty(select))
        //    {
        //        return 0;
        //    }

        //    try
        //    {
        //        abrirConexion();

        //        cmd = new SqlCommand(select, cnn);
        //        cmd.CommandType = CommandType.Text;

        //        return cmd.ExecuteNonQuery();
                

        //    }
        //    catch (Exception ex)
        //    {
        //        System.Diagnostics.Debug.Print(ex.Message);
                
        //    }


        //    return 0;

        //}

        public bool Update( string nombreProcedimientoUno, string nombreProcedimientoDos, List<SqlParameter> parametersuno, List<List<SqlParameter>> parametrodos)
        {

            try
            {
                if ( string.IsNullOrEmpty(nombreProcedimientoDos) || string.IsNullOrEmpty(nombreProcedimientoUno))
                {
                    return false;
                }

                abrirConexion();
                sqlTransaction = cnn.BeginTransaction();

                SqlCommand cmd2= cnn.CreateCommand();
                cmd2.Transaction = sqlTransaction;
                cmd2.CommandText = nombreProcedimientoUno;
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.Parameters.AddRange(parametersuno.ToArray());
             

                int rowsAffect = 0;

                //sqlTransaction = cnn.BeginTransaction();
                SqlCommand cmd3 = cnn.CreateCommand();
                cmd3.Transaction = sqlTransaction;
                cmd3.CommandText = nombreProcedimientoDos;
                cmd3.CommandType = CommandType.StoredProcedure;




                foreach (List<SqlParameter> sqlParameters in parametrodos)
                {

                    cmd3.Parameters.AddRange(sqlParameters.ToArray());
                    rowsAffect += cmd3.ExecuteNonQuery();

                    cmd3.Parameters.Clear();
                }

                if ( cmd2.ExecuteNonQuery()>0  && rowsAffect > 0)
                {
                    sqlTransaction.Commit();
                    evaluar = true;
                }

              

               
            }
            catch (Exception ex)
            {
                sqlTransaction.Rollback();
                MessageBox.Show($"Error al ejecutar el procedimiento{nombreProcedimientoUno},{nombreProcedimientoDos}Clase" + ex.Message);
                cerrarconexion();
                evaluar = false;
            }

            return evaluar;
        }




        static string cadenaConexion = "Server=10.0.6.14; Database=actfijo1;User ID =contabilidad; Password=contab1$2020;";

        public SqlConnection cnn = new SqlConnection(cadenaConexion);


        public void abrirConexion()
        {
            try
            {
                if (cnn.State == ConnectionState.Closed)
                {
                    cnn.Open();
                }

                Console.Write("La conexion esta Abierta");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al conectarse a la  Base de Datos " + ex.Message);

                throw;
            }


        }

        public void cerrarconexion()
        {
            try
            {

                if (cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al  desconectarse de la  Base de Datos " + ex.Message);

                throw;
            }
        }


        public static void SoloNumero(KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (char.IsSeparator(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }

        }




        public static void SoloLetra(KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (char.IsSeparator(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }

        }



        public static void SoloNumeroDecimal(KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (char.IsSeparator(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (e.KeyChar.ToString().Equals("."))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }


        public static object MensajeGuardar()
        {

            return MessageBox.Show("No se ha Guardado Correctamente", "Guardando Registro", MessageBoxButtons.OK, MessageBoxIcon.Error);


        }
        public static object MensajeActualizar()
        {

          return  MessageBox.Show("No se ha Actualizado Correctamente", "Actualizando Registro", MessageBoxButtons.OK, MessageBoxIcon.Error);


        }
        public static void MensajeCampoVacio()
        {
            MessageBox.Show("Faltan datos por completar", "Validando Campo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }


        public static void CleanForm(Control.ControlCollection controlCollection)
        {
            foreach (Control control in controlCollection)
            {
                if (control is Panel || control is GroupBox || control is Form)
                {
                    CleanForm(control.Controls);
                }
                else if (control is TextBox)
                {
                    ((TextBox)control).Clear();
                }
                else if (control is ComboBox)
                {
                    ((ComboBox)control).Text = "";
                }
                else if (control is MaskedTextBox)
                {
                    ((MaskedTextBox)control).Clear();
                }
                else if (control is CheckBox)
                {
                    ((CheckBox)control).Checked = false;
                }
                else if (control is NumericUpDown)
                {
                    ((NumericUpDown)control).Text = "0";
                }
                else if (control is DateTimePicker)
                {
                    DateTime date = DateTime.Now;
                    ((DateTimePicker)control).Value = date;
                }
                else if (control is DataGridView)
                {
                    ((DataGridView)control).Rows.Clear();
                }
            }
        }
        public static void ValidarCamposVacios(Control control)
        {
            ErrorProvider errorProvider = new ErrorProvider();

            if (control.Text == "")
            {
                errorProvider.SetError(control, "Es obligatorio completar este campo");
            }
            else
            {
                errorProvider.SetError(control, "");
            }

        }


        public DataTable Buscar(string procedimientoNombre, List<SqlParameter> parameters)
        {
            if (parameters == null)
            {
                return null;
            }

            try
            {

                abrirConexion();

                cmd = new SqlCommand(procedimientoNombre, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddRange(parameters.ToArray());

                cmd.ExecuteNonQuery();

                DataTable dt = new DataTable();
                SqlDataAdapter ds = new SqlDataAdapter(cmd);
                ds.Fill(dt);

                return dt;
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Error al ejecutar el Procedimiento {procedimientoNombre}  Error: " + ex.Message);
                return null;
            }




        }
        public DataTable GetFill(string nombreProcediento)
        {
            try
            {

                abrirConexion();
                cmd = new SqlCommand(nombreProcediento, cnn);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.ExecuteNonQuery();

                DataTable dataTable = new DataTable();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);


                sqlDataAdapter.Fill(dataTable);
                return dataTable;





            }
            catch (Exception ex)
            {
                cerrarconexion();
                MessageBox.Show($"Error al executar el Procedimiento {nombreProcediento}  Clase" + ex.Message);
                return null;
            }
            finally
            {
                cerrarconexion();

            }

        }
        public DataTable GetFillSelect(string select)
        {
            try
            {

                abrirConexion();
                cmd = new SqlCommand(select, cnn);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();


                DataTable dataTable = new DataTable();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);

                sqlDataAdapter.Fill(dataTable);
                cerrarconexion();
                return dataTable;

            }
            catch (Exception ex)
            {
                cerrarconexion();
                MessageBox.Show($"Error al executar el Vista {select}  Clase" + ex.Message);
                return null;
            }
        }


        public string GetCodigoSelect(string select)
        {
            try
            {
                var codigo = "";

                abrirConexion();
                cmd = new SqlCommand(select, cnn);
                cmd.CommandType = CommandType.Text;


                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    codigo = reader["codigo"].ToString();
                }


                cerrarconexion();
                return codigo;

            }
            catch (Exception ex)
            {
                cerrarconexion();
                MessageBox.Show($"Error al ejecutar el Select {select}  Clase" + ex.Message);
                return null;
            }
        }
        public string GetCodigoSelect(string select, string nombrecampo)
        {
            try
            {
                var resultado = "";

                abrirConexion();
                cmd = new SqlCommand(select, cnn);
                cmd.CommandType = CommandType.Text;


                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    resultado = reader[nombrecampo].ToString();
                }


                cerrarconexion();
                return resultado;

            }
            catch (Exception ex)
            {
                cerrarconexion();
                MessageBox.Show($"Error al ejecutar el Select {select}  Clase" + ex.Message);
                return null;
            }
        }

        public string GetCodigoSelect(string select, string nombrecampo,string parameters,string valor)
        {
            try
            {
                var resultado = "";

                abrirConexion();
                cmd = new SqlCommand(select, cnn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(parameters,SqlDbType.VarChar).Value=valor;

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    resultado = reader[nombrecampo].ToString();
                }


                cerrarconexion();
                return resultado;

            }
            catch (Exception ex)
            {
                cerrarconexion();
                MessageBox.Show($"Error al ejecutar el Select {select}  Clase" + ex.Message);
                return null;
            }
        }

        public void GetFillDatableWith(DataTable table, SqlCommand command)
        {
            try
            {
                abrirConexion();
                command.Connection = cnn;

                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                dataAdapter.Fill(table);


            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Fail("Error" + ex.Message);

            }
            finally
            {
                cerrarconexion();
            }

        }

        public void GetFillDatableWith(DataTable table, string consulta, SqlParameter[] parameter)
        {
            GetFillDatableWith(table, CreateComand(consulta, parameter));
        }

        public SqlCommand CreateComand(string consulta, CommandType commandType, SqlParameter[] parameters)
        {

            SqlCommand command = new SqlCommand(consulta);
            command.CommandType = commandType;

            if (parameters != null && parameters.Length > 0)
            {
                command.Parameters.AddRange(parameters.ToArray());
            }
            return command;



        }




        public SqlCommand CreateComand(string consulta, SqlParameter[] parameter)
        {
            return CreateComand(consulta, CommandType.Text, parameter);
        }

        public DataTable GetFillParametro(string nombreProcediento, List<SqlParameter> parameters)
        {
            try
            {

                abrirConexion();
                cmd = new SqlCommand(nombreProcediento, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddRange(parameters.ToArray());

                cmd.ExecuteNonQuery();


                DataTable dataTable = new DataTable();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);

                sqlDataAdapter.Fill(dataTable);
                cerrarconexion();
                return dataTable;


            }
            catch (Exception ex)
            {
                cerrarconexion();
                MessageBox.Show($"Error al executar el Procedimiento {nombreProcediento}  Clase" + ex.Message);
                return null;
            }

        }

    }

}

