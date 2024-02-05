using CRUDdpto.WebAPI.Models;
using MySql.Data.MySqlClient;
using System.Data;

namespace CRUDdpto.WebAPI.Repositories
{
    public class FuncionarioDAO : IDAO<Funcionario>
    {
        private readonly MySqlConnection connection;
        private readonly DepartamentoDAO dptoDAO;

        public FuncionarioDAO()
        {
            try
            {
                connection = new MySqlConnection("Server=localhost;Database=CrudDepartamento;User Id=root;Password=root;");
                dptoDAO = new DepartamentoDAO();
            }
            catch (MySqlException)
            {
                throw;
            }
        }

        public void Create(Funcionario entity)
        {
            String query = "INSERT INTO Funcionarios (fun_nome, fun_foto, fun_rg, fun_dep_id) VALUES (@NomeFunc, @FotoFunc, @RgFunc, @DptoFunc)";

            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }

                    cmd.Parameters.AddWithValue("@NomeFunc", entity.getNome());
                    cmd.Parameters.AddWithValue("@FotoFunc", entity.getFoto());
                    cmd.Parameters.AddWithValue("@RgFunc", entity.getRg());
                    cmd.Parameters.AddWithValue("@DptoFunc", entity.getDepartamento().GetId());

                    cmd.ExecuteNonQuery();
                }
                catch (MySqlException)
                {
                    throw;
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
            }
        }

        public List<Funcionario> Read()
        {
            List<Funcionario> funcionarios = new List<Funcionario>();

            string query = "SELECT * FROM Funcionarios";

            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32("fun_id");
                            String nome = reader.GetString("fun_nome");
                            String foto = reader.GetString("fun_foto");
                            String rg = reader.GetString("fun_rg");
                            int idDpto = reader.GetInt32("fun_dep_id");

                            Departamento departamento = dptoDAO.FindById(idDpto);

                            Funcionario funcionario = new Funcionario(id, nome, foto, rg, departamento);
                            funcionarios.Add(funcionario);
                        }
                        return funcionarios;
                    }
                }
            }
            catch (MySqlException)
            {
                throw;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        public Funcionario Update(Funcionario entity)
        {
            string query = "UPDATE Funcionarios SET fun_nome = @NomeFunc, fun_foto = @FotoFunc, fun_rg = @RgFunc, fun_dep_id = @DptoFunc WHERE fun_id =@IdFunc";

            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@NomeFunc", entity.getNome());
                    cmd.Parameters.AddWithValue("@FotoFunc", entity.getFoto());
                    cmd.Parameters.AddWithValue("@RgFunc", entity.getRg());
                    cmd.Parameters.AddWithValue("@DptoFunc", entity.getDepartamento().GetId());
                    cmd.Parameters.AddWithValue("@IdFunc", entity.getId());

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return FindById(entity.getId());
                }
            }
            catch (MySqlException)
            {
                throw;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        public void Delete(int id)
        {
            String query = "DELETE FROM Funcionarios WHERE fun_id = @IdFunc";

            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@IdFunc", id);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (MySqlException)
            {
                throw;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        public Funcionario FindById(int id)
        {
            string query = "SELECT * FROM Funcionarios WHERE fun_id = @IdFunc";

            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@IdFunc", id);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int iD = reader.GetInt32("fun_id");
                            String nome = reader.GetString("fun_nome");
                            String foto = reader.GetString("fun_foto");
                            String rg = reader.GetString("fun_rg");
                            int idDpto = reader.GetInt32("fun_dep_id");

                            Departamento departamento = dptoDAO.FindById(idDpto);

                            Funcionario funcionario = new Funcionario(iD, nome, foto, rg, departamento);
                            return funcionario;
                        }
                        return null;
                    }
                }
            }
            catch (MySqlException)
            {
                throw;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
        public List<Funcionario> FindByDepartment(int id)
        {
            List<Funcionario> funcionarios = new List<Funcionario>();

            string query = "SELECT * FROM Funcionarios WHERE fun_dep_id = @IdDptoFunc";

            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@IdDptoFunc", id);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int iD = reader.GetInt32("fun_id");
                            String nome = reader.GetString("fun_nome");
                            String foto = reader.GetString("fun_foto");
                            String rg = reader.GetString("fun_rg");
                            int idDpto = reader.GetInt32("fun_dep_id");

                            Departamento departamento = dptoDAO.FindById(idDpto);

                            Funcionario funcionario = new Funcionario(iD, nome, foto, rg, departamento);
                            funcionarios.Add(funcionario);
                            return funcionarios;
                        }
                        return null;
                    }
                }
            }
            catch (MySqlException)
            {
                throw;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

    }
}
