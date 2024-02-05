using System.Reflection.Metadata;
using System.Text.Json.Serialization;

namespace CRUDdpto.WebAPI.Models
{
    public class Funcionario
    {
        public int id { get; set; }

        public String nome { get; set; }

        public String foto { get; set; }

        public String rg { get; set; }

        public Departamento departamento { get; set; }


        public Funcionario(int id, String nome, String foto, String rg, Departamento departamento) {
            this.setId(id);
            this.setNome(nome);
            this.setFoto(foto);
            this.setRg(rg);
            this.setDepartamento(departamento);
        }

        [JsonConstructor]
        public Funcionario(String nome, String foto, String rg, Departamento departamento)
        {
            this.setNome(nome);
            this.setFoto(foto);
            this.setRg(rg);
            this.setDepartamento(departamento);
        }


        public int getId() {  return id; }
        public void setId(int id) { this.id = id; }

        public String getNome() {  return nome; }
        public void setNome(String nome)
        {
            if (nome == null)
            {
                throw new ArgumentNullException(nameof(nome), "O nome do funcionário não pode ser nulo.");
            }
            this.nome = nome;
        }

        public String getFoto() { return foto; }
        public void setFoto(String foto)
        {
            if (Equals(foto, null))
            {
                throw new ArgumentNullException(nameof(foto), "A foto do funcionário não pode ser nula.");
            }
            this.foto = foto;
        }

        public String getRg() { return rg; }
        public void setRg(String rg)
        {
            if (rg == null)
            {
                throw new ArgumentNullException(nameof(rg), "O RG do funcionário não pode ser nulo.");
            }
            this.rg = rg;
        }

        public Departamento getDepartamento() { return departamento; }
        public void setDepartamento(Departamento departamento) { this.departamento = departamento; }

    }
}
