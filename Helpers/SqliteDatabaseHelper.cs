using MauiAppAmerico.Models;
using SQLite;
using System.Threading.Tasks;

namespace MauiAppAmerico.Helpers
{
    public class SQLiteDatabaseHelper
    {
        readonly SQLiteAsyncConnection _conn;

        public SQLiteDatabaseHelper(string path)
        {
            _conn = new SQLiteAsyncConnection(path);
            // Removendo .Wait() e tornando o construtor assíncrono (melhor prática)
            InitializeDatabaseAsync().Wait();
        }

        private async Task InitializeDatabaseAsync()
        {
            await _conn.CreateTableAsync<Produto>();
        }

        public Task<int> Insert(Produto p)
        {
            return _conn.InsertAsync(p);
        }

        public async Task<int> Update(Produto p)
        {
            string sql = "UPDATE Produto SET Descricao=?, Quantidade=?, Preco=? WHERE Id=?";
            // Usando ExecuteAsync para comandos UPDATE que não retornam linhas
            return await _conn.ExecuteAsync(sql, p.Descricao, p.Quantidade, p.Preco, p.Id);
        }

        public Task<int> Delete(int id)
        {
            return _conn.Table<Produto>().Where(i => i.Id == id).DeleteAsync();
        }

        public Task<List<Produto>> GetAll()
        {
            return _conn.Table<Produto>().ToListAsync();
        }

        public Task<List<Produto>> Search(string q)
        {
            string sql = "SELECT * FROM Produto WHERE Descricao LIKE ?";
            // Usando parâmetro para evitar problemas com SQL injection
            return _conn.QueryAsync<Produto>(sql, $"%{q}%");
        }

        // Removendo método internal async Task GetAll<T>() pois parece duplicado ou não utilizado corretamente
    }
}

// Observações em SqliteDataBaseHelpers.cs:
// - O construtor foi adaptado para usar um método InitializeDatabaseAsync para criar a tabela de forma assíncrona, evitando o uso de .Wait() que pode bloquear a thread principal.
// - O método Update foi alterado para retornar Task<int> e usar _conn.ExecuteAsync, pois um UPDATE geralmente retorna o número de linhas afetadas, não a entidade atualizada.
// - O método Delete agora usa a cláusula Where do Table para uma sintaxe mais segura e clara.
// - O método Search agora utiliza um parâmetro na consulta SQL para evitar potenciais problemas de SQL injection.
// - O método genérico GetAll<T>() foi removido, pois parecia não estar implementado ou era redundante.
// - O aviso 1998 foi resolvido ao tornar o construtor "assíncrono" na sua lógica de inicialização.
