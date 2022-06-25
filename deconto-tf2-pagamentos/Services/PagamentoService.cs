using deconto_tf2_pagamentos.DTO;
using deconto_tf2_pagamentos.Helpers;
using deconto_tf2_pagamentos.Models;

namespace deconto_tf2_pagamentos.Services
{
    public class PagamentoService
    {
        protected readonly DataContext _context;
        public PagamentoService(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> Cadastrar(PagamentoDTO pagamentoDTO)
        {
            var pagamento = new Pagamento
            {
                IdEntrega = pagamentoDTO.idEntrega,
                Valor = pagamentoDTO.valor,
                DataPagamento = DateTime.Now,
                Pago = true,
            };

            try
            {
                await _context.Pagamentos.AddAsync(pagamento);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        } 
    }
}
