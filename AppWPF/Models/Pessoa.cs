namespace AppWPF.Models {
    // modelo da pessoa, deixei minúsculo pra bater com a api sem frescura
    public class Pessoa {
        public int id { get; set; }
        public string nome { get; set; }
        public string sobrenome { get; set; }
        public string telefone { get; set; }
    }
}
