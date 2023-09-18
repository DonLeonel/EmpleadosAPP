using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DB.Models
{
    [Table("empleados")]
    public class Empleado
    {
        [Key]
        [Column("id_empleado")]
        public Guid EmpleadoID { get; set; }

        [Column("nombre")]
        public string Nombre { get; set; }

        [Column("apellido")]
        public string Apellido { get; set; }

        [Column("dni")]
        public string Dni { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Column("fecha_alta")]
        public DateTime FechaAlta { get; set; }

        [Column("id_sucursal")]
        public Guid SucursalID { get; set; }
        [ForeignKey("SucursalID")]        
        public virtual Sucursal Sucursal { get; set; }

        [Column("id_cargo")]
        public Guid CargoID { get; set; }
        [ForeignKey("CargoID")]
        public virtual Cargo Cargo { get; set; }
                
        [Column("id_jefe")]
        public Guid JefeID { get; set; }

        [Column("activo")]
        public int Activo { get; set; }

    }
}
