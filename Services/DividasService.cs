using vendinhaPlena.Data;
using vendinhaPlena.Models;
using System.ComponentModel.DataAnnotations;
using vendinhaPlena.Data.Repositories;

namespace vendinhaPlena.Services
{
    public class DividaService{
        public bool Validar(Dividas d, out List<ValidationResult> erros)
        {
            var contexto = new ValidationContext(d);
            erros = new List<ValidationResult>();
            var objetoValido = Validator.
                TryValidateObject(
                    d,
                    contexto,
                    erros,
                    true
                );
            return objetoValido;
        }
        public bool CriarDivida(Dividas d)
        {
            using var context = new VendaDbContext();

            bool dividaAberta = context.Divida.Any(d => d.idCliente == d.idCliente && d.situacao == Enums.SituacaoDivida.naoPago);
            if (!Validar(d, out var erros))
            {
                return false;
            }else if (dividaAberta)
            {
                return false;
            }
            context.Divida.Add(d);
            context.SaveChanges();
            return true;
        }
        public List<Dividas> ListarDivida(int idCliente)
        {
            using var context = new VendaDbContext();
            var dividas = context.Divida.Where(d => d.idCliente == idCliente).ToList();
            return dividas;
        }
        public bool Pagar(int id_cliente)
        {
            using var context = new VendaDbContext();
            var divida = context.Divida.FirstOrDefault(d => d.idCliente == id_cliente && d.situacao == Enums.SituacaoDivida.naoPago);


            if (divida == null || divida.situacao == Enums.SituacaoDivida.pago)
                return false;

            divida.situacao = 0;
            context.SaveChanges();
            return true;
        }
    }
}