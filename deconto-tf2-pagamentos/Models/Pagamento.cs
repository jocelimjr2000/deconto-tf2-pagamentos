namespace deconto_tf2_pagamentos.Models
{
    public class Pagamento
    {
        public int Id { get; set; }
        public decimal Valor { get; set; }
        public string IdEntrega { get; set; }
        public bool Pago { get; set; }
        public DateTime DataPagamento { get; set; }
    }
}
