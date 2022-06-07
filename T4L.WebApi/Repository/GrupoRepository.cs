using Microsoft.EntityFrameworkCore;
using T4L.Domain;
using T4L.WebApi.Context;

namespace T4L.WebApi.Repository
{
    public class GrupoRepository
    {
        private T4LContext _context;
        public GrupoRepository(T4LContext context)
        {
            _context = context;
        }

        public async Task<GenericResult> GetAll()
        {
            try
            {
                var grupoProdutos = await _context.ProdutoGrupo.ToListAsync();

                if (grupoProdutos == null)
                {
                    return new GenericResult(false, "Grupos não encontrados", null);
                }

                return new GenericResult(true, "Grupos encontrados", grupoProdutos);
            }
            catch (Exception ex)
            {
                return new GenericResult(false, ex.Message);
            }
        }
    }
}
