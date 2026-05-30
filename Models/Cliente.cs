using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace vendinhaPlena.Models
{
    public class Cliente
    {
        [Key]
        public int id {get; set;}
        [Required, StringLength(100, MinimumLength = 9)]
        public string nome {get; set;}
        [Required, StringLength(11)]
        public string cpf {get; set;}
        [Required]
        [Column(TypeName = "Date")]
        public DateTime DataNascimento {get; set;}
        public int Idade
        {
            get
            {
                var hoje = DateTime.Today;
                var anos = hoje.Year - DataNascimento.Year;
                var diaNascimento = hoje.AddYears(anos);
                if (DataNascimento > diaNascimento)
                {
                    anos--;
                }
                return anos;
            }
        }
        private string email;
        [Required]
        [EmailAddress]
        public string Email
        {
            get {return email;}
            set{email = value.ToLower();}
        }
        public List<Dividas> dividas { get; set; } = new();
    }
}