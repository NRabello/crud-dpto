using System.Text.Json.Serialization;

namespace CRUDdpto.WebAPI.Models
{
    public class Departamento
    {
        public int id { get; set; }

        public String nome { get; set; }

        public String sigla { get; set; }


        [JsonConstructor]
        public Departamento(String nome, String sigla) {
            this.SetNome(nome);
            this.SetSigla(sigla);  
        }

        public Departamento(int id,String nome, String sigla)
        {
            this.SetId(id);
            this.SetNome(nome);
            this.SetSigla(sigla);
        }

        public void SetId(int id) { this.id = id; }
        public int GetId() {  return id; }

        public String GetNome() { return nome; }
        public void SetNome(String nome)
        {
            if (nome == null)
            {
                throw new ArgumentNullException(nameof(nome), "O nome do departamento não pode ser nulo.");
            }
            this.nome = nome;
        }

        public String GetSigla() { return sigla; }
        public void SetSigla(String sigla)
        {
            if (sigla == null)
            {
                throw new ArgumentNullException(nameof(sigla), "A sigla do departamento não pode ser nulo.");
            }
            this.sigla = sigla;
        }
    }
}
