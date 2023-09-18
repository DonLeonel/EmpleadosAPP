using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DB.Models
{
    [Table("credenciales")]
    public class Credencial
    {
        [Key]
        [Column("id_credencial")]
        public Guid CredencialID { get; set; }

        [Column("usuario")]
        public string Usuario { get; set; }

        [Column("contrasenia")]
        public string Contrasenia { get; set; }

        //[Column("email")]
        //public string Email { get; set; }    me falto esto!

        [Column("avatar")]
        public string Avatar { get; set; }

        [Column("creado_en")]
        public DateTime CreadoEn { get; set; }

        [Column("actualizado_en")]
        public DateTime ActualidoEn { get; set; }

        [Column("activo")]
        public int Activo { get; set; }
    }
}
