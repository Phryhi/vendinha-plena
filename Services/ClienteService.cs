using vendinhaPlena.Data;
using vendinhaPlena.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using vendinhaPlena.Data.Repositories;

namespace vendinhaPlena.Services
{
    public class ClienteService
    {
        public bool Validar(Cliente c, out List<ValidationResult> erros)
        {
            var contexto = new ValidationContext(c);
            erros = new List<ValidationResult>();
            var objetoValido = Validator.
                TryValidateObject(
                    c,
                    contexto,
                    erros,
                    true
                );
            return objetoValido;
        }
        public List<Cliente> Listar(int pag)
        {
            using var context = new VendaDbContext();
            return context.Cliente.Include(c => c.dividas).OrderByDescending(c => c.dividas.Sum(d => d.valor)).Skip((pag - 1) * 10).Take(10).ToList();
        }
        public bool Criar(Cliente c)
        {
            if (!Validar(c, out var erros))
            {
                return false;
            }
            using var context = new VendaDbContext();
            bool cpfExiste = context.Cliente.Any(cliente => cliente.cpf == c.cpf);
            if (cpfExiste)
            {
                return false;
            }
            context.Cliente.Add(c);
            context.SaveChanges();
            return true;
        }
        public bool Excluir(Cliente c)
        {
            using var context = new VendaDbContext();
            var cliente = context.Cliente.Find(c.id);

            if(cliente == null){
                return false;
            }
            context.Cliente.Remove(cliente);
            context.SaveChanges();
            return true;
        }
        public bool Editar(Cliente c, out List<ValidationResult> erros)
        {
            using var context = new VendaDbContext();
            var registroExistente = context.Cliente.FirstOrDefault(
                x => x.id == c.id
                );

            if (registroExistente == null)
            {
                erros = new List<ValidationResult>();
                erros.Add(new ValidationResult("Categoria não encontrada"));
                return false;
            }

            registroExistente.nome = c.nome;
            registroExistente.cpf = c.cpf;
            registroExistente.DataNascimento = c.DataNascimento;
            registroExistente.Email = c.Email;

            if (!Validar(registroExistente, out erros))
            {
                return false;
            }
            context.SaveChanges();
            return true;
        }
        public List<Cliente> Buscar(string nomeCliente)
        {
            using var context = new VendaDbContext();
            return context.Cliente.Include(c => c.dividas).Where(c => c.nome.Contains(nomeCliente)).ToList();
        }
    }
}