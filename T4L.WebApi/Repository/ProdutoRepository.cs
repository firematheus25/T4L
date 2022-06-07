using Microsoft.EntityFrameworkCore;
using T4L.Domain;
using T4L.Domain.Entities;
using T4L.WebApi.Context;

namespace T4L.WebApi.Repository
{
    public class ProdutoRepository //: IDisposable
    {
        private T4LContext _context;
        public ProdutoRepository(T4LContext context)
        {
            _context = context;
        }

        public async Task<GenericResult> GetAll()
        {
            try
            {
                var produtos = await _context.VwConsulta.ToListAsync();

                if (produtos.Count == 0)
                {
                    return new GenericResult(false, "Produtos não encontrados", null);
                }

                return new GenericResult(true, "Produtos encontrados", produtos);
            }
            catch (Exception ex)
            {
                return new GenericResult(false, ex.Message);
            }

        }

        public async Task<GenericResult> GetById(int id)
        {
            try
            {
                var produto = await _context.Produto.FirstOrDefaultAsync(x => x.Cod == id);

                if (produto == null)
                {
                    return new GenericResult(false, "Produto não encontrado", null);
                }

                return new GenericResult(true, "Produto encontrado", produto);
            }
            catch (Exception ex)
            {

                return new GenericResult(false, ex.Message);
            }
        }
        

        public async Task<GenericResult> GetByKeyWord(string keyWord)
        {
            try
            {
                //var produtos = await _context.Produto.Where(x => x.Descricao.Contains(keyWord)).ToListAsync();
                var produtos = await _context.VwConsulta.Where(x => x.Descricao.Contains(keyWord)).ToListAsync();

                if (produtos.Count == 0)
                {
                    return new GenericResult(false, "Produtos não encontrados", null);
                }

                return new GenericResult(true, "Produtos encontrados", produtos);
            }
            catch (Exception ex)
            {

                return new GenericResult(false, ex.Message);
            }
        }

        public async Task<GenericResult> Add(Produto produto)
        {
            try
            {
                await _context.Produto.AddAsync(produto);    
                _context.SaveChanges();

                return new GenericResult(true, "Inserido com sucesso.", produto);
            }
            catch (Exception ex)
            {
                return new GenericResult(false, ex.Message);                
            }            
        }

        public async Task<GenericResult> Edit(Produto produto)
        {
            try
            {
                _context.Produto.Update(produto);
                _context.SaveChanges();
                return new GenericResult(true, "Atualizado com sucesso.", produto);
            }
            catch (Exception ex)
            {
                return new GenericResult(false, ex.Message);
            }
        }


        public async Task<GenericResult> Delete(int id)
        {
            try
            {
                var produto = await GetById(id);
                var vendaProduto = await IsDeleteValid(id);
                if (vendaProduto != null)
                {
                    return new GenericResult(false, "Produto não pode ser excluído, pois já possui vendas vinculadas.");
                }
                _context.Produto.Remove((Produto)produto.Data);
                _context.SaveChanges();
                return new GenericResult(true, "Excluido com sucesso.", produto);
            }
            catch (Exception ex)
            {
                return new GenericResult(false, ex.Message);
            }
        }

        private async Task<VendaProduto> IsDeleteValid(int id)        
           => await _context.VendaProduto.FirstOrDefaultAsync(x => x.CodProduto == id);

        //public void Dispose()
        //{
        //    _context.SaveChanges();
        //}
    }
}
