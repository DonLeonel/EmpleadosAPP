using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DB.Migrations
{
    /// <inheritdoc />
    public partial class InitDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cargos",
                columns: table => new
                {
                    id_cargo = table.Column<Guid>(type: "uuid", nullable: false),
                    nombre = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cargos", x => x.id_cargo);
                });

            migrationBuilder.CreateTable(
                name: "ciudades",
                columns: table => new
                {
                    id_ciudad = table.Column<Guid>(type: "uuid", nullable: false),
                    nombre = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ciudades", x => x.id_ciudad);
                });

            migrationBuilder.CreateTable(
                name: "credenciales",
                columns: table => new
                {
                    id_credencial = table.Column<Guid>(type: "uuid", nullable: false),
                    usuario = table.Column<string>(type: "text", nullable: false),
                    contrasenia = table.Column<string>(type: "text", nullable: false),
                    avatar = table.Column<string>(type: "text", nullable: false),
                    creado_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    actualizado_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    activo = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_credenciales", x => x.id_credencial);
                });

            migrationBuilder.CreateTable(
                name: "sucursales",
                columns: table => new
                {
                    id_sucursal = table.Column<Guid>(type: "uuid", nullable: false),
                    nombre = table.Column<string>(type: "text", nullable: false),
                    id_ciudad = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sucursales", x => x.id_sucursal);
                    table.ForeignKey(
                        name: "FK_sucursales_ciudades_id_ciudad",
                        column: x => x.id_ciudad,
                        principalTable: "ciudades",
                        principalColumn: "id_ciudad",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "empleados",
                columns: table => new
                {
                    id_empleado = table.Column<Guid>(type: "uuid", nullable: false),
                    nombre = table.Column<string>(type: "text", nullable: false),
                    apellido = table.Column<string>(type: "text", nullable: false),
                    dni = table.Column<string>(type: "text", nullable: false),
                    fecha_alta = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    id_sucursal = table.Column<Guid>(type: "uuid", nullable: false),
                    id_cargo = table.Column<Guid>(type: "uuid", nullable: false),
                    id_jefe = table.Column<Guid>(type: "uuid", nullable: false),
                    activo = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_empleados", x => x.id_empleado);
                    table.ForeignKey(
                        name: "FK_empleados_cargos_id_cargo",
                        column: x => x.id_cargo,
                        principalTable: "cargos",
                        principalColumn: "id_cargo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_empleados_sucursales_id_sucursal",
                        column: x => x.id_sucursal,
                        principalTable: "sucursales",
                        principalColumn: "id_sucursal",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_empleados_id_cargo",
                table: "empleados",
                column: "id_cargo");

            migrationBuilder.CreateIndex(
                name: "IX_empleados_id_sucursal",
                table: "empleados",
                column: "id_sucursal");

            migrationBuilder.CreateIndex(
                name: "IX_sucursales_id_ciudad",
                table: "sucursales",
                column: "id_ciudad");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "credenciales");

            migrationBuilder.DropTable(
                name: "empleados");

            migrationBuilder.DropTable(
                name: "cargos");

            migrationBuilder.DropTable(
                name: "sucursales");

            migrationBuilder.DropTable(
                name: "ciudades");
        }
    }
}
