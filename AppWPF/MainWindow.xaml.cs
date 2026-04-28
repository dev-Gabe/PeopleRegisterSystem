using AppWPF.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Windows;

namespace AppWPF {
    public partial class MainWindow : Window {
        // cliente pra falar com a api
        HttpClient client = new HttpClient();
        // string url = "http://localhost:5006/api/persons"; Troquei para azure
        string url = "https://apiregistro-h4epbvcmend2cvcq.brazilsouth-01.azurewebsites.net/api/persons";
        // pra nao dar erro de letra maiuscula no json
        JsonSerializerOptions options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        public MainWindow() {
            InitializeComponent();
            CarregarLista(); // ja chama pra ver se tem gente cadastrada
        }

        // funcao que busca os dados no banco (via api)
        async void CarregarLista() {
            try {
                var res = await client.GetStringAsync(url);
                var lista = JsonSerializer.Deserialize<List<Pessoa>>(res, options);
                
                gridPessoas.ItemsSource = null;
                gridPessoas.ItemsSource = lista;
            } catch (Exception ex) {
                MessageBox.Show("Erro ao buscar dados: " + ex.Message);
            }
        }

        // botao de salvar
        async void Add_Click(object sender, RoutedEventArgs e) {
            try {
                if (string.IsNullOrEmpty(txtNome.Text) || string.IsNullOrEmpty(txtSobrenome.Text) || string.IsNullOrEmpty(txtTelefone.Text)) {
                    MessageBox.Show("Preenche todos os campos antes de salvar!");
                    return;
                }

                var p = new Pessoa { 
                    nome = txtNome.Text, 
                    sobrenome = txtSobrenome.Text, 
                    telefone = txtTelefone.Text 
                };

                var json = JsonSerializer.Serialize(p);
                var dados = new StringContent(json, Encoding.UTF8, "application/json");

                var resp = await client.PostAsync(url, dados);
                if (resp.IsSuccessStatusCode) {
                    Limpar();
                    CarregarLista();
                }
            } catch (Exception ex) {
                MessageBox.Show("Erro ao salvar contato: " + ex.Message);
            }
        }

        // botao de atualizar quem ta selecionado
        async void Update_Click(object sender, RoutedEventArgs e) {
            try {
                var selecionado = gridPessoas.SelectedItem as Pessoa;
                if (selecionado != null) {
                    // nao deixa atualizar se algum campo tiver vazio
                    if (string.IsNullOrEmpty(txtNome.Text) || string.IsNullOrEmpty(txtSobrenome.Text) || string.IsNullOrEmpty(txtTelefone.Text)) {
                        MessageBox.Show("Preenche todos os campos antes de atualizar!");
                        return;
                    }

                    selecionado.nome = txtNome.Text;
                    selecionado.sobrenome = txtSobrenome.Text;
                    selecionado.telefone = txtTelefone.Text;

                    var json = JsonSerializer.Serialize(selecionado);
                    var dados = new StringContent(json, Encoding.UTF8, "application/json");

                    var res = await client.PutAsync($"{url}/{selecionado.id}", dados);
                    if (res.IsSuccessStatusCode) {
                        MessageBox.Show("Contato atualizado!");
                        CarregarLista();
                    }
                } else {
                    MessageBox.Show("Clica em alguem na tabela primeiro!");
                }
            } catch (Exception ex) {
                MessageBox.Show("Erro no update: " + ex.Message);
            }
        }

        // botao de excluir
        async void Delete_Click(object sender, RoutedEventArgs e) {
            try {
                var p = gridPessoas.SelectedItem as Pessoa;
                if (p != null) {
                    var ok = MessageBox.Show($"Quer mesmo apagar o {p.nome}?", "Aviso", MessageBoxButton.YesNo);
                    if (ok == MessageBoxResult.Yes) {
                        await client.DeleteAsync($"{url}/{p.id}");
                        CarregarLista();
                    }
                }
            } catch (Exception ex) {
                MessageBox.Show("Erro ao deletar: " + ex.Message);
            }
        }

        void Clear_Click(object sender, RoutedEventArgs e) => Limpar();

        void Limpar() {
            txtNome.Clear();
            txtSobrenome.Clear();
            txtTelefone.Clear();
        }
    }
}
