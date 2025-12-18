using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EC.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EventCentri",
                columns: table => new
                {
                    EventCenterID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adresa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailAdresa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KontaktTelefon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SysDtCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SysDtUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventCentri", x => x.EventCenterID);
                });

            migrationBuilder.CreateTable(
                name: "MapeSedenja",
                columns: table => new
                {
                    IdMape = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NazivMape = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RasporedJSON = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kapacitet = table.Column<int>(type: "int", nullable: false),
                    EventCenterID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SysDtCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SysDtUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MapeSedenja", x => x.IdMape);
                    table.ForeignKey(
                        name: "FK_MapeSedenja_EventCentri_EventCenterID",
                        column: x => x.EventCenterID,
                        principalTable: "EventCentri",
                        principalColumn: "EventCenterID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Dogadjaji",
                columns: table => new
                {
                    IdDogadjaja = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NazivDogadjaja = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OpisDogadja = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatumDogadjaja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StatusDogadjaja = table.Column<int>(type: "int", nullable: false),
                    EventCenterID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdMape = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SysDtCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SysDtUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dogadjaji", x => x.IdDogadjaja);
                    table.ForeignKey(
                        name: "FK_Dogadjaji_EventCentri_EventCenterID",
                        column: x => x.EventCenterID,
                        principalTable: "EventCentri",
                        principalColumn: "EventCenterID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Dogadjaji_MapeSedenja_IdMape",
                        column: x => x.IdMape,
                        principalTable: "MapeSedenja",
                        principalColumn: "IdMape");
                });

            migrationBuilder.CreateTable(
                name: "TemplateoviStolova",
                columns: table => new
                {
                    IdTemplejtaStola = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BrojStola = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KapacitetStola = table.Column<int>(type: "int", nullable: false),
                    OblikStola = table.Column<int>(type: "int", nullable: false),
                    XKordinata = table.Column<double>(type: "float", nullable: false),
                    YKordinata = table.Column<double>(type: "float", nullable: false),
                    Rotacija = table.Column<double>(type: "float", nullable: false),
                    IdMape = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SysDtCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SysDtUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemplateoviStolova", x => x.IdTemplejtaStola);
                    table.ForeignKey(
                        name: "FK_TemplateoviStolova_MapeSedenja_IdMape",
                        column: x => x.IdMape,
                        principalTable: "MapeSedenja",
                        principalColumn: "IdMape",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Gosti",
                columns: table => new
                {
                    IdGosta = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InicijalSrednjegImena = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdDogadjaja = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SysDtCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SysDtUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gosti", x => x.IdGosta);
                    table.ForeignKey(
                        name: "FK_Gosti_Dogadjaji_IdDogadjaja",
                        column: x => x.IdDogadjaja,
                        principalTable: "Dogadjaji",
                        principalColumn: "IdDogadjaja",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Totemi",
                columns: table => new
                {
                    IdTotema = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Lokacija = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Aktivan = table.Column<bool>(type: "bit", nullable: false),
                    EventCenterID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdDogadjaja = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SysDtCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SysDtUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Totemi", x => x.IdTotema);
                    table.ForeignKey(
                        name: "FK_Totemi_Dogadjaji_IdDogadjaja",
                        column: x => x.IdDogadjaja,
                        principalTable: "Dogadjaji",
                        principalColumn: "IdDogadjaja");
                    table.ForeignKey(
                        name: "FK_Totemi_EventCentri_EventCenterID",
                        column: x => x.EventCenterID,
                        principalTable: "EventCentri",
                        principalColumn: "EventCenterID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stolovi",
                columns: table => new
                {
                    IdStola = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BrojStola = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KapacitetStola = table.Column<int>(type: "int", nullable: false),
                    OblikStola = table.Column<int>(type: "int", nullable: false),
                    XKordinata = table.Column<double>(type: "float", nullable: false),
                    YKordinata = table.Column<double>(type: "float", nullable: false),
                    Rotacija = table.Column<double>(type: "float", nullable: false),
                    IdTemplejtaStola = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IdDogadjaja = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SysDtCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SysDtUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stolovi", x => x.IdStola);
                    table.ForeignKey(
                        name: "FK_Stolovi_Dogadjaji_IdDogadjaja",
                        column: x => x.IdDogadjaja,
                        principalTable: "Dogadjaji",
                        principalColumn: "IdDogadjaja",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Stolovi_TemplateoviStolova_IdTemplejtaStola",
                        column: x => x.IdTemplejtaStola,
                        principalTable: "TemplateoviStolova",
                        principalColumn: "IdTemplejtaStola");
                });

            migrationBuilder.CreateTable(
                name: "TemplateoviStolica",
                columns: table => new
                {
                    IdTemplejtaStolice = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BrojStolice = table.Column<int>(type: "int", nullable: false),
                    PozicijaX = table.Column<double>(type: "float", nullable: false),
                    PozicijaY = table.Column<double>(type: "float", nullable: false),
                    Pravac = table.Column<double>(type: "float", nullable: false),
                    Zauzeta = table.Column<bool>(type: "bit", nullable: false),
                    IdTemplejtaStola = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SysDtCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SysDtUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemplateoviStolica", x => x.IdTemplejtaStolice);
                    table.ForeignKey(
                        name: "FK_TemplateoviStolica_TemplateoviStolova_IdTemplejtaStola",
                        column: x => x.IdTemplejtaStola,
                        principalTable: "TemplateoviStolova",
                        principalColumn: "IdTemplejtaStola",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stolice",
                columns: table => new
                {
                    IdStolice = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BrojStolice = table.Column<int>(type: "int", nullable: false),
                    PozicijaX = table.Column<double>(type: "float", nullable: false),
                    PozicijaY = table.Column<double>(type: "float", nullable: false),
                    Pravac = table.Column<double>(type: "float", nullable: false),
                    Zauzeta = table.Column<bool>(type: "bit", nullable: false),
                    IdStola = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdTemplejtaStolice = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SysDtCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SysDtUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stolice", x => x.IdStolice);
                    table.ForeignKey(
                        name: "FK_Stolice_Stolovi_IdStola",
                        column: x => x.IdStola,
                        principalTable: "Stolovi",
                        principalColumn: "IdStola",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Stolice_TemplateoviStolica_IdTemplejtaStolice",
                        column: x => x.IdTemplejtaStolice,
                        principalTable: "TemplateoviStolica",
                        principalColumn: "IdTemplejtaStolice");
                });

            migrationBuilder.CreateTable(
                name: "Rezervacije",
                columns: table => new
                {
                    IdRezervacije = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdGrupeKojaJeUsla = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DatumRezervacije = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdGosta = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdStolice = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SysDtCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SysDtUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rezervacije", x => x.IdRezervacije);
                    table.ForeignKey(
                        name: "FK_Rezervacije_Gosti_IdGosta",
                        column: x => x.IdGosta,
                        principalTable: "Gosti",
                        principalColumn: "IdGosta",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rezervacije_Stolice_IdStolice",
                        column: x => x.IdStolice,
                        principalTable: "Stolice",
                        principalColumn: "IdStolice");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dogadjaji_EventCenterID",
                table: "Dogadjaji",
                column: "EventCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_Dogadjaji_IdMape",
                table: "Dogadjaji",
                column: "IdMape");

            migrationBuilder.CreateIndex(
                name: "IX_Gosti_IdDogadjaja",
                table: "Gosti",
                column: "IdDogadjaja");

            migrationBuilder.CreateIndex(
                name: "IX_MapeSedenja_EventCenterID",
                table: "MapeSedenja",
                column: "EventCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_Rezervacije_IdGosta",
                table: "Rezervacije",
                column: "IdGosta");

            migrationBuilder.CreateIndex(
                name: "IX_Rezervacije_IdStolice",
                table: "Rezervacije",
                column: "IdStolice");

            migrationBuilder.CreateIndex(
                name: "IX_Stolice_IdStola",
                table: "Stolice",
                column: "IdStola");

            migrationBuilder.CreateIndex(
                name: "IX_Stolice_IdTemplejtaStolice",
                table: "Stolice",
                column: "IdTemplejtaStolice");

            migrationBuilder.CreateIndex(
                name: "IX_Stolovi_IdDogadjaja",
                table: "Stolovi",
                column: "IdDogadjaja");

            migrationBuilder.CreateIndex(
                name: "IX_Stolovi_IdTemplejtaStola",
                table: "Stolovi",
                column: "IdTemplejtaStola");

            migrationBuilder.CreateIndex(
                name: "IX_TemplateoviStolica_IdTemplejtaStola",
                table: "TemplateoviStolica",
                column: "IdTemplejtaStola");

            migrationBuilder.CreateIndex(
                name: "IX_TemplateoviStolova_IdMape",
                table: "TemplateoviStolova",
                column: "IdMape");

            migrationBuilder.CreateIndex(
                name: "IX_Totemi_EventCenterID",
                table: "Totemi",
                column: "EventCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_Totemi_IdDogadjaja",
                table: "Totemi",
                column: "IdDogadjaja");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rezervacije");

            migrationBuilder.DropTable(
                name: "Totemi");

            migrationBuilder.DropTable(
                name: "Gosti");

            migrationBuilder.DropTable(
                name: "Stolice");

            migrationBuilder.DropTable(
                name: "Stolovi");

            migrationBuilder.DropTable(
                name: "TemplateoviStolica");

            migrationBuilder.DropTable(
                name: "Dogadjaji");

            migrationBuilder.DropTable(
                name: "TemplateoviStolova");

            migrationBuilder.DropTable(
                name: "MapeSedenja");

            migrationBuilder.DropTable(
                name: "EventCentri");
        }
    }
}
