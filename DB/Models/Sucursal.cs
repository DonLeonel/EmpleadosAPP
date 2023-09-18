using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DB.Models
{
    [Table("sucursales")]
    public class Sucursal
    {
        [Key]
        [Column(name: "id_sucursal")]
        public Guid SucursalID { get; set; }

        [Column("nombre")]
        public string Nombre { get; set; }

        [Column("id_ciudad")]
        public Guid CiudadID { get; set; }
        [ForeignKey("CiudadID")]
        public virtual Ciudad Ciudad { get; set; }

        public virtual ICollection<Empleado> Empleados { get; set; }
    }
}
