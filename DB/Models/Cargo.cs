using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DB.Models
{
    [Table("cargos")]
    public class Cargo
    {
        [Key]
        [Column(name: "id_cargo")]
        public Guid CargoID { get; set; }

        [Column("nombre")]
        public string Nombre { get; set; }

        public virtual ICollection<Empleado> Empleados { get; set; }
    }
}
