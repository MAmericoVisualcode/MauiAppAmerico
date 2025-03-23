sing MauiAppAmerico;
using MauiAppAmerico.Models;

namespace MauiAppamerico.Views;

public partial class NovoProduto : ContentPage
{
#pragma warning disable CS8618 // O campo não anulável precisa conter um valor não nulo ao sair do construtor. Considere adicionar o modificador "obrigatório" ou declarar como anulável.
    public NovoProduto()
#pragma warning restore CS8618 // O campo não anulável precisa conter um valor não nulo ao sair do construtor. Considere adicionar o modificador "obrigatório" ou declarar como anulável.
    {
        InitializeComponent();
    }

    public object txt_descricao { get; private set; }
    public object txt_quantidade { get; private set; }
    public object txt_preco { get; private set; }

    private void InitializeComponent()
    {
        throw new NotImplementedException();
    }

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            Produto p = new Produto
            {
                Descricao = (string)txt_descricao,
                Quantidade = Convert.ToDouble(txt_quantidade),
                Preco = Convert.ToDouble(txt_preco)
            };

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
