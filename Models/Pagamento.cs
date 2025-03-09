using PagamentoMongoDB.Enum;
using System.Text.Json.Serialization;

public class Pagamento
{
    [JsonIgnore] 
    public string Id { get; set; }

    public string Nome { get; set; }
    public decimal Valor { get; set; }
    public MetodoPagamento TipoPagamento { get; set; }

    [JsonIgnore] 
    public DateTime DataCriacao { get; set; }
}
