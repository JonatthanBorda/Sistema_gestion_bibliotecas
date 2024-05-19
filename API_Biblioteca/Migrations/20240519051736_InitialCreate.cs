using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Biblioteca.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "TipoDocto",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Tipo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK__TipoDoct__3214EC07C8EDAE96", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Autor",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
            //        Apellido = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
            //        IdTipoDocto = table.Column<int>(type: "int", nullable: false),
            //        NumDocto = table.Column<int>(type: "int", nullable: false),
            //        FechaNacimiento = table.Column<DateOnly>(type: "date", nullable: false),
            //        Bibliografia = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        FechaCreacion = table.Column<DateTime>(type: "datetime", nullable: false),
            //        UsuarioCreacion = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
            //        FechaModificacion = table.Column<DateTime>(type: "datetime", nullable: true),
            //        UsuarioModificacion = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK__Autor__3214EC0706C7FCCA", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK__Autor__IdTipoDoc__267ABA7A",
            //            column: x => x.IdTipoDocto,
            //            principalTable: "TipoDocto",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Libro",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Titulo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
            //        NumPaginas = table.Column<int>(type: "int", nullable: false),
            //        FechaPublicacion = table.Column<DateOnly>(type: "date", nullable: false),
            //        Disponible = table.Column<bool>(type: "bit", nullable: false),
            //        IdAutor = table.Column<int>(type: "int", nullable: false),
            //        FechaCreacion = table.Column<DateTime>(type: "datetime", nullable: false),
            //        UsuarioCreacion = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
            //        FechaModificacion = table.Column<DateTime>(type: "datetime", nullable: true),
            //        UsuarioModificacion = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK__Libro__3214EC074A03F95E", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK__Libro__IdAutor__29572725",
            //            column: x => x.IdAutor,
            //            principalTable: "Autor",
            //            principalColumn: "Id");
            //    });

            migrationBuilder.CreateIndex(
                name: "IX_Autor_IdTipoDocto",
                table: "Autor",
                column: "IdTipoDocto");

            migrationBuilder.CreateIndex(
                name: "IX_Libro_IdAutor",
                table: "Libro",
                column: "IdAutor");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Libro");

            migrationBuilder.DropTable(
                name: "Autor");

            migrationBuilder.DropTable(
                name: "TipoDocto");
        }
    }
}
