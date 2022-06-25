using deconto_tf2_pagamentos.DTO;
using deconto_tf2_pagamentos.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace deconto_tf2_pagamentos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagamentoController : ControllerBase
    {
        protected readonly PagamentoService _pagamentoService;
        public PagamentoController(PagamentoService pagamentoService)
        {
            _pagamentoService = pagamentoService;
        }

        [HttpPost]
        public async Task<bool> Cadastar(PagamentoDTO pagamentoDTO)
        {
            return await _pagamentoService.Cadastrar(pagamentoDTO);
        }
      
    }
}
