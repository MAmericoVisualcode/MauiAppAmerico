using MauiAppAmerico;
using MauiAppAmerico.Models;

namespace MauiAppamerico.Views;

public partial class NovoProduto : ContentPage
{
    private object txt_descricao;
    private object txt_preco;
    private object txt_quantidade;

    public NovoProduto()
    {
        InitializeComponent();
    }

    public object Txt_preco { get => txt_preco; set => txt_preco = value; }
    public object Txt_descricao { get => txt_descricao; set => txt_descricao = value; }
    public object Txt_quantidade { get => txt_quantidade; set => txt_quantidade = value; }

    private void InitializeComponent()
    {
        throw new NotImplementedException();
    }

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            Produto p = GetP();

            await App.Db.Insert(p);
            await DisplayAlert("Sucesso!", "Registro Inserido", "OK");
            await Navigation.PopAsync();

        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }

    private Produto GetP()
    {
        return new Produto
        {
            Descricao = Txt_descricao.Text,
            Quantidade = Convert.ToDouble(Txt_quantidade.Text),
            Preco = Convert.ToDouble(Txt_preco.Text)
        };
    }
}