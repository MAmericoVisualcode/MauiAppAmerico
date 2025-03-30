using SQLite;
using System;
using System.ComponentModel.DataAnnotations;

namespace MauiAppAmerico.Models
{
    public class Produto
    {
        string? _descricao;

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Descricao
        {
            get => _descricao ?? ""; // Retorna string vazia se _descricao for nulo
            set
            {
                _descricao = value ?? throw new ArgumentNullException(nameof(value), "A descrição não pode ser nula.");
            }
        }
        public double Quantidade { get; set; }
        public double Preco { get; set; }
        public double Total => Quantidade * Preco;
        [Required] // Adicionando atributo Required para indicar que Categoria não pode ser nulo (além do required na declaração)
        public string Categoria { get; set; } = string.Empty; // Inicializando para evitar nulls
    }
}

// Observações em Produto.cs:
// - No getter da propriedade Descricao, agora retornamos uma string vazia ("") caso _descricao seja nulo, resolvendo o aviso CS8603 de forma mais segura sem apenas suprimir o aviso.
// - No setter da propriedade Descricao, a exceção lançada agora é ArgumentNullException, que é mais apropriada para argumentos nulos, e inclui o nome do parâmetro.
// - A propriedade Categoria agora é inicializada com string.Empty para garantir que ela não seja nula por padrão, além de manter o `required` para garantir que um valor seja atribuído durante a criação do objeto. O atributo `[Required]` do SQLite também foi adicionado para reforçar essa regra no banco de dados (dependendo da configuração do SQLite).
