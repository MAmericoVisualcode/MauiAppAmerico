using MauiAppAmerico.Models;
using System;

namespace MauiAppAmerico.Views;

public partial class EditarProduto : ContentPage
{
    public EditarProduto()
    {
        InitializeComponent();
    }

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            if (BindingContext is Produto produto_anexado &&
                !string.IsNullOrEmpty(txt_descricao.Text) &&
                double.TryParse(txt_quantidade.Text, out double quantidade) &&
                double.TryParse(txt_preco.Text, out double preco))
            {
                Produto p = new()
                {
                    Id = produto_anexado.Id,
                    Descricao = txt_descricao.Text,
                    Quantidade = quantidade,
                    Preco = preco,
                    Categoria = produto_anexado.Categoria // Mantendo a categoria original (ou adicione um campo para edição)
                };

                await App.Db.Update(p);
                await DisplayAlert("Sucesso!", "Registro Atualizado", "OK");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Atenção", "Por favor, preencha todos os campos corretamente.", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }
}

// Observações em EditarProduto.xaml.cs:
// - Adicionada uma verificação para garantir que o BindingContext é do tipo Produto antes de acessá-lo, resolvendo o aviso CS8600 indiretamente ao garantir um tipo correto.
// - Adicionadas verificações para garantir que os campos de texto não estão vazios e que a conversão para double é bem-sucedida antes de criar o objeto Produto, evitando a desreferência de possíveis valores nulos (CS8600 e CS8602 implicitamente).
// - A propriedade Categoria do produto anexado é mantida ao atualizar. Se você deseja permitir a edição da categoria, precisará adicionar um campo correspondente no XAML e vinculá-lo.
