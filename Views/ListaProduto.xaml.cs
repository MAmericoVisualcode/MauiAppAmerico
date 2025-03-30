using MauiAppAmerico.Models;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Maui.Controls;

namespace MauiAppAmerico.Views;

public partial class ListaProduto : ContentPage
{
    readonly ObservableCollection<Produto> lista = [];
    private List<Produto> todosProdutos = []; // Inicializado para evitar NullReferenceException

    public ListaProduto()
    {
        InitializeComponent();
        lst_produtos.ItemsSource = lista; // Agora usando a referência do XAML
    }

    protected async override void OnAppearing()
    {
        try
        {
            lista.Clear();
            todosProdutos = await App.Db.GetAll();
            todosProdutos.ForEach(i => lista.Add(i));
            FiltrarProdutos();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }

    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            Navigation.PushAsync(new NovoProduto());
        }
        catch (Exception ex)
        {
            DisplayAlert("Ops", ex.Message, "OK");
        }
    }

    private async void Txt_search_TextChanged(object sender, TextChangedEventArgs e)
    {
        try
        {
            string? q = e.NewTextValue;
            if (string.IsNullOrWhiteSpace(q)) return; // Evita pesquisa com string vazia

            lst_produtos.IsRefreshing = true;
            lista.Clear();
            List<Produto> tmp = await App.Db.Search(q);
            tmp.ForEach(i => lista.Add(i));
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
        finally
        {
            lst_produtos.IsRefreshing = false;
        }
    }

    private void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {
        double soma = lista.Sum(i => i.Preco * i.Quantidade);
        string msg = $"O total é {soma:C}";
        DisplayAlert("Total dos Produtos", msg, "OK");
    }

    private async void MenuItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            if (sender is not MenuItem selecionado || selecionado.BindingContext is not Produto p) return;

            bool confirm = await DisplayAlert("Tem Certeza?", $"Remover {p.Descricao}?", "Sim", "Não");

            if (confirm)
            {
                await App.Db.Delete(p.Id);
                lista.Remove(p);
                todosProdutos.Remove(p);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }

    private void Lst_produtos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        try
        {
            if (e.SelectedItem is not Produto p) return; // Corrigido possível erro de null

            Navigation.PushAsync(new EditarProduto
            {
                BindingContext = p,
            });

            lst_produtos.SelectedItem = null;
        }
        catch (Exception ex)
        {
            DisplayAlert("Ops", ex.Message, "OK");
        }
    }

    private async void Lst_produtos_Refreshing(object sender, EventArgs e)
    {
        try
        {
            lista.Clear();
            todosProdutos = await App.Db.GetAll();
            todosProdutos.ForEach(i => lista.Add(i));
            FiltrarProdutos();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
        finally
        {
            lst_produtos.IsRefreshing = false;
        }
    }

    private void Pck_filtroCategoria_SelectedIndexChanged(object sender, EventArgs e)
    {
        FiltrarProdutos();
    }

    private void FiltrarProdutos()
    {
        string categoriaSelecionada = Pck_filtroCategoria.SelectedItem?.ToString() ?? "Todas"; // Corrige possível erro de null

        if (categoriaSelecionada == "Todas" || string.IsNullOrEmpty(categoriaSelecionada))
        {
            lst_produtos.ItemsSource = todosProdutos;
        }
        else
        {
            lst_produtos.ItemsSource = todosProdutos.Where(p => p.Categoria == categoriaSelecionada).ToList();
        }
    }
}
