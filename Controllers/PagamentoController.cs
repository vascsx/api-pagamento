using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class PagamentoController : ControllerBase
{
    private readonly IMongoCollection<Pagamento> _pagamentos;

    public PagamentoController(IMongoDatabase database)
    {
        _pagamentos = database.GetCollection<Pagamento>("Pagamentos");
    }

    [HttpPost]
    public async Task<IActionResult> CriarPagamento([FromBody] Pagamento pagamento)
    {
        if (pagamento == null)
        {
            return BadRequest("Os dados de pagamento s�o obrigat�rios.");
        }

        if (pagamento.TipoPagamento == null || pagamento.Valor <= 0)
        {
            return BadRequest("M�todo de Pagamento e valor s�o obrigat�rios e o valor deve ser maior que zero.");
        }

        pagamento.DataCriacao = DateTime.UtcNow;

        try
        {
            await _pagamentos.InsertOneAsync(pagamento);

            return CreatedAtAction(nameof(CriarPagamento), new { id = pagamento.Id }, pagamento);
        }
        catch (MongoException mongoEx)
        {
            return StatusCode(500, $"Erro no banco de dados: {mongoEx.Message}");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao criar pagamento: {ex.Message}");
        }
    }
}
