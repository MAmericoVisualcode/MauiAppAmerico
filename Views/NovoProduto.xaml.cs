using MauiAppAmerico;
using MauiAppAmerico.Models;

namespace MauiAppAmerico.Views
{
    public partial class NovoProduto : ContentPage
    {
#pragma warning disable CS8618 // O campo não anulável precisa conter um valor não nulo ao sair do construtor. Considere adicionar o modificador "obrigatório" ou declarar como anulável.
        public NovoProduto()
#pragma warning restore CS8618 // O campo não anulável precisa conter um valor não nulo ao sair do construtor. Considere adicionar o modificador "obrigatório" ou declarar como anulável.
        {
            InitializeComponent(); // Chamada correta para InitializeComponent()
        }

        public Entry Txt_descricao { get; private set; }
        public Entry Txt_quantidade { get; private set; }
        public Entry Txt_preco { get; private set; }
        public Picker Pck_categoria { get; private set; } // Adicionada esta linha

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(Txt_descricao.Text) ||
                    string.IsNullOrEmpty(Txt_quantidade.Text) ||
                    string.IsNullOrEmpty(Txt_preco.Text) ||
                    Pck_categoria.SelectedItem == null)
                {
                    await DisplayAlert("Atenção", "Por favor, preencha todos os campos.", "OK");
                    return;
                }

#pragma warning disable CS8601 // Possível atribuição de referência nula.
                Produto p = new()
                {
                    Descricao = Txt_descricao.Text,
                    Quantidade = Convert.ToDouble(Txt_quantidade.Text),
                    Preco = Convert.ToDouble(Txt_preco.Text),
                    Categoria = Pck_categoria.SelectedItem.ToString() // Obtém a categoria selecionada
                };
#pragma warning restore CS8601 // Possível atribuição de referência nula.

                await App.Db.Insert(p);
                await DisplayAlert("Sucesso!", "Registro Inserido", "OK");
                await Navigation.PopAsync();

            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "OK");
            }
        }
    }
}
