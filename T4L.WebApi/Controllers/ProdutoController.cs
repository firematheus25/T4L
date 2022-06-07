using Microsoft.AspNetCore.Mvc;
using T4L.Domain;
using T4L.Domain.Entities;
using T4L.WebApi.Repository;

namespace T4L.WebApi.Controllers
{
    [ApiController]  
    public class ProdutoController : ControllerBase
    {
        private readonly ProdutoRepository _produtoRepository;
        public ProdutoController(ProdutoRepository repository)
        {
            _produtoRepository = repository;
        }

        [HttpGet("v1/vwconsulta")]
        public async Task<GenericResult> GetAsync()
        => await _produtoRepository.GetAll();

        [HttpGet("v1/produto/id/{id:int}")]
        public async Task<GenericResult> GetByIdAsync([FromRoute] int id)
            => await _produtoRepository.GetById(id);


        [HttpGet("v1/vwconsulta/keyWord/{keyWord}")]
        public async Task<GenericResult> GetByKeyWordAsync([FromRoute] string keyWord)
            => await _produtoRepository.GetByKeyWord(keyWord);

        [HttpPost("v1/produto")]
        public async Task<GenericResult> PostAsync([FromBody] Produto produto)
        => await _produtoRepository.Add(produto);


        [HttpPut("v1/produto")]
        public async Task<GenericResult> PutAsync([FromBody] Produto produto)
        => await _produtoRepository.Edit(produto);

        [HttpDelete("v1/produto/id/{id:int}")]
        public async Task<GenericResult> DeleteAsync([FromRoute] int id)
        => await _produtoRepository.Delete(id);
    }
}
