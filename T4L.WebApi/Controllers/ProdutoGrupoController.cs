using Microsoft.AspNetCore.Mvc;
using T4L.Domain;
using T4L.WebApi.Repository;

namespace T4L.WebApi.Controllers
{
    [ApiController]    
    public class ProdutoGrupoController : ControllerBase
    {
        private readonly GrupoRepository _grupoRepository;
        public ProdutoGrupoController(GrupoRepository repository)
        {
            _grupoRepository = repository;
        }

        [HttpGet("v1/produtogrupo")]
        public async Task<GenericResult> GetAsync()
        => await _grupoRepository.GetAll();
    }
}
