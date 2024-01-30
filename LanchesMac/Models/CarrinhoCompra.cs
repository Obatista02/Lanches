using LanchesMac.Components;
using LanchesMac.Context;
using Microsoft.EntityFrameworkCore;

namespace LanchesMac.Models
{
    public class CarrinhoCompra
    {
        private readonly AppDbContext _context;
        

        public CarrinhoCompra(AppDbContext context)
        {
            _context = context;
        }
        public string CarrinhoCompraId { get; set; }
        public List<CarrionhoCompraItem> CarrionhoCompraItems { get; set; }
        public static CarrinhoCompra GetCarrinho (IServiceProvider services)
        {
            //define uma sessao
            ISession session = 
                services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            //obtem um servico do tipo do nosso contexto
            var context = services.GetService<AppDbContext>();

            //obtem ou gera o id do carrinho
            string carrinhoId = session.GetString("CarrinhoId")?? Guid.NewGuid().ToString();
            //atribui o id do carrinho na sessao
            session.SetString("CarrinhoId", carrinhoId);

            //retorna o carrinho com o contexto e o id atrbuido ou obtido
            return new CarrinhoCompra(context)
            {
                CarrinhoCompraId = carrinhoId,
            };
        }

        public void AdicionarAoCarrinho(Lanche lanche)
        {
            var carrinhoCompraItem = _context.CarrionhoCompraItens.SingleOrDefault(
                s => s.Lanche.LancheId == lanche.LancheId &&
                    s.CarrinhoCompraId == CarrinhoCompraId
                );
            if(carrinhoCompraItem == null)
            {
                carrinhoCompraItem = new CarrionhoCompraItem
                {
                    CarrinhoCompraId = CarrinhoCompraId,
                    Lanche = lanche,
                    Quantidade = 1
                };
                _context.CarrionhoCompraItens.Add( carrinhoCompraItem );
            }
            else
            {
                carrinhoCompraItem.Quantidade++;
            }
            _context.SaveChanges();
        }

        public int RemoverDoCarrinho(Lanche lanche)
        {
            var carrinhoCompraItem =
                _context.CarrionhoCompraItens.SingleOrDefault(
                      s => s.Lanche.LancheId == lanche.LancheId &&
                        s.CarrinhoCompraId == CarrinhoCompraId
                    );
            var quantidadeLocal = 0;

            if(carrinhoCompraItem != null)
            {
                if(carrinhoCompraItem.Quantidade > 1)
                {
                    carrinhoCompraItem.Quantidade--;
                    quantidadeLocal = carrinhoCompraItem.Quantidade;
                }
                else
                {
                    _context.CarrionhoCompraItens.Remove( carrinhoCompraItem );

                }
            }
            _context.SaveChanges();
            return quantidadeLocal;
        }

        public List<CarrionhoCompraItem> GetCarrionhoCompraItems()
        {
            return CarrionhoCompraItems ?? (CarrionhoCompraItems = _context.CarrionhoCompraItens
                .Where(c => c.CarrinhoCompraId == CarrinhoCompraId)
                .Include(s => s.Lanche)
                .ToList());
        }


        public void LimparCarrinho()
        {
            var carrinhoItens = _context.CarrionhoCompraItens
                                .Where(carrinho => carrinho.CarrinhoCompraId == CarrinhoCompraId);
            _context.CarrionhoCompraItens.RemoveRange( carrinhoItens );
            _context.SaveChanges();
        }
        public decimal GetCarrinhoCompraTotal()
        {
            var total = _context.CarrionhoCompraItens
                .Where(c => c.CarrinhoCompraId == CarrinhoCompraId)
                .Select(c => c.Lanche.Preco * c.Quantidade).Sum();
            return total;

        }

        public static implicit operator CarrinhoCompra(CarrinhoCompraResumo v)
        {
            throw new NotImplementedException();
        }
    }
}
