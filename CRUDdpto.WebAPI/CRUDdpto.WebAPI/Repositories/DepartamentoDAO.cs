using CRUDdpto.WebAPI.Models;
using Microsoft.AspNetCore.Hosting.Server;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.Common;

namespace CRUDdpto.WebAPI.Repositories
{

    public class DepartamentoDAO : IDAO<Departamento>
    {

        private MySqlConnection connection;
        public DepartamentoDAO()
        {
            try
            {
                connection = new MySqlConnection("Server=localhost;Database=CrudDepartamento;User Id=root;Password=root;");
            }
            catch (MySqlException)
            {
                throw;
            }
        }

        public void Create(Departamento entity)
        {
            String query = "INSERT INTO Departamentos (dep_nome, dep_sigla) VALUES (@NomeDpto, @SiglaDpto)";

            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@NomeDpto", entity.GetNome());
                    cmd.Parameters.AddWithValue("@SiglaDpto", entity.GetSigla());

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

        public List<Departamento> Read()
        {
            List<Departamento> departamentos = new List<Departamento>();

            string query = "SELECT * FROM Departamentos";

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
                            int id = reader.GetInt32("dep_id");
                            String nome = reader.GetString("dep_nome");
                            String sigla = reader.GetString("dep_sigla");

                            Departamento departamento = new Departamento(id, nome, sigla);
                            departamentos.Add(departamento);
                        }
                    }
                }
                return departamentos;
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
            String query = "DELETE FROM Departamentos WHERE dep_id = @IdDept";

            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@IdDept", id);
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

        public Departamento FindById(int id)
        {
            string query = "SELECT * FROM Departamentos WHERE dep_id = @IdDpto";

            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@IdDpto", id);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int idDpto = reader.GetInt32("dep_id");
                            String nome = reader.GetString("dep_nome");
                            String sigla = reader.GetString("dep_sigla");

                            Departamento departamento = new Departamento(idDpto, nome, sigla);
                            return departamento;
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

        public Departamento Update(Departamento entity)
        {
            string query = "UPDATE Departamentos SET dep_nome = @NomeDpto, dep_sigla = @SiglaDpto WHERE dep_id =@IdDpto";

            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@NomeDpto", entity.GetNome());
                    cmd.Parameters.AddWithValue("@SiglaDpto", entity.GetSigla());
                    cmd.Parameters.AddWithValue("@IdDpto", entity.GetId());

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return FindById(entity.GetId());
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
