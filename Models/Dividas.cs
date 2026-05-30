using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using vendinhaPlena.Enums;

namespace vendinhaPlena.Models
{
    public class Dividas
    {
        [Key]
        [Column("id_divida")]
        public int id_divida { get; set; }

        [Required]
        public decimal valor { get; set; }

        [Required]
        public SituacaoDivida situacao { get; set; }

        [Required]
        [Column(TypeName = "Date")]
        public DateTime dataCriacao { get; set; }

        [Required]
        [Column(TypeName = "Date")]
        public DateTime dataPagamento { get; set; }

        [Required]
        [ForeignKey("Cliente")]
        [Column("id_cliente")]
        public int idCliente { get; set; }

        public Cliente Cliente { get; set; }
    }
}