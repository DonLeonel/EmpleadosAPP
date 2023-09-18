using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DB.Models
{
    [Table("ciudades")]
    public class Ciudad
    {
        [Key]
        [Column("id_ciudad")]
        public Guid CiudadID { get; set; }

        [Column("nombre")]
        public string Nombre { get; set; }

        public virtual ICollection<Sucursal> Sucursales { get; set; }
    }
}
