using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Infrastructure.Sqlite;
using Questao5.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Questao5.Repositories
{
    public class ContaCorrenteRepository : IContaCorrenteRepository
    {
        private readonly DatabaseConfig _databaseConfig;

        public ContaCorrenteRepository(DatabaseConfig databaseConfig)
        {
            _databaseConfig = databaseConfig;
        }

        private SqliteConnection GetConnection() => new SqliteConnection(_databaseConfig.Name);

        public async Task<bool> ContaExiste(string idContaCorrente)
        {
            using var connection = GetConnection();
            var result = await connection.QuerySingleOrDefaultAsync<int>(
                "SELECT COUNT(*) FROM contacorrente WHERE idcontacorrente = @Id", new { Id = idContaCorrente });
            return result > 0;
        }

        public async Task<bool> ContaAtiva(string idContaCorrente)
        {
            using var connection = GetConnection();
            var result = await connection.QuerySingleOrDefaultAsync<int>(
                "SELECT ativo FROM contacorrente WHERE idcontacorrente = @Id", new { Id = idContaCorrente });
            return result == 1;
        }

        public async Task RegistrarMovimento(MovimentacaoRequest request)
        {
            using var connection = GetConnection();
            await connection.ExecuteAsync(
                "INSERT INTO movimento (idmovimento, idcontacorrente, datamovimento, tipomovimento, valor) VALUES (@Id, @Conta, @Data, @Tipo, @Valor)",
                new { Id = Guid.NewGuid().ToString(), Conta = request.IdContaCorrente, Data = DateTime.UtcNow.ToString("yyyy-MM-dd"), Tipo = request.TipoMovimento, Valor = request.Valor });
        }

        public async Task<decimal> ObterSaldo(string idContaCorrente)
        {
            using var connection = GetConnection();
            var saldo = await connection.QuerySingleOrDefaultAsync<decimal>(
                "SELECT COALESCE(SUM(CASE WHEN tipomovimento = 'C' THEN valor ELSE -valor END), 0) FROM movimento WHERE idcontacorrente = @Id",
                new { Id = idContaCorrente });
            return saldo;
        }

        public async Task<(int Numero, string Nome)> ObterDadosConta(string idContaCorrente)
        {
            using var connection = GetConnection();
            return await connection.QuerySingleOrDefaultAsync<(int, string)>(
                "SELECT numero, nome FROM contacorrente WHERE idcontacorrente = @Id", new { Id = idContaCorrente });
        }
    }
}
