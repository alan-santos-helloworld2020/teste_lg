namespace cadastro.models
{
    public class Contato
    {
        public int id { get; set; } = 0;
        public string nome { get; set; } = string.Empty;
        public string telefone { get; set; } = string.Empty;
        public string grau_parentesco  { get; set; } = string.Empty;
        public int idCliente  { get; set; } = 0;
    }
}