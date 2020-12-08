using Microsoft.EntityFrameworkCore.Migrations;

namespace SdnMonitorData.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "sdnListPublshInformation",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Publish_Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Record_Count = table.Column<int>(type: "int", nullable: false),
                    Record_CountSpecified = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sdnListPublshInformation", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "sdnListSdnEntryVesselInfo",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    callSign = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    vesselType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    vesselFlag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    vesselOwner = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tonnage = table.Column<int>(type: "int", nullable: false),
                    tonnageSpecified = table.Column<bool>(type: "bit", nullable: false),
                    grossRegisteredTonnage = table.Column<int>(type: "int", nullable: false),
                    grossRegisteredTonnageSpecified = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sdnListSdnEntryVesselInfo", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "sdnLists",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    publshInformationid = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sdnLists", x => x.id);
                    table.ForeignKey(
                        name: "FK_sdnLists_sdnListPublshInformation_publshInformationid",
                        column: x => x.publshInformationid,
                        principalTable: "sdnListPublshInformation",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "sdnListSdnEntry",
                columns: table => new
                {
                    uid = table.Column<int>(type: "int", nullable: false),
                    firstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sdnType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InternalData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    vesselInfoid = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    sdnListid = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sdnListSdnEntry", x => x.uid);
                    table.ForeignKey(
                        name: "FK_sdnListSdnEntry_sdnLists_sdnListid",
                        column: x => x.sdnListid,
                        principalTable: "sdnLists",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_sdnListSdnEntry_sdnListSdnEntryVesselInfo_vesselInfoid",
                        column: x => x.vesselInfoid,
                        principalTable: "sdnListSdnEntryVesselInfo",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "sdnListSdnEntryAddress",
                columns: table => new
                {
                    uid = table.Column<int>(type: "int", nullable: false),
                    address1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    address2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    address3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    city = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    stateOrProvince = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    postalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sdnListSdnEntryuid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sdnListSdnEntryAddress", x => x.uid);
                    table.ForeignKey(
                        name: "FK_sdnListSdnEntryAddress_sdnListSdnEntry_sdnListSdnEntryuid",
                        column: x => x.sdnListSdnEntryuid,
                        principalTable: "sdnListSdnEntry",
                        principalColumn: "uid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "sdnListSdnEntryAka",
                columns: table => new
                {
                    uid = table.Column<int>(type: "int", nullable: false),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    firstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sdnListSdnEntryuid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sdnListSdnEntryAka", x => x.uid);
                    table.ForeignKey(
                        name: "FK_sdnListSdnEntryAka_sdnListSdnEntry_sdnListSdnEntryuid",
                        column: x => x.sdnListSdnEntryuid,
                        principalTable: "sdnListSdnEntry",
                        principalColumn: "uid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "sdnListSdnEntryCitizenship",
                columns: table => new
                {
                    uid = table.Column<int>(type: "int", nullable: false),
                    country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    mainEntry = table.Column<bool>(type: "bit", nullable: false),
                    sdnListSdnEntryuid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sdnListSdnEntryCitizenship", x => x.uid);
                    table.ForeignKey(
                        name: "FK_sdnListSdnEntryCitizenship_sdnListSdnEntry_sdnListSdnEntryuid",
                        column: x => x.sdnListSdnEntryuid,
                        principalTable: "sdnListSdnEntry",
                        principalColumn: "uid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "sdnListSdnEntryDateOfBirthItem",
                columns: table => new
                {
                    uid = table.Column<int>(type: "int", nullable: false),
                    dateOfBirth = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    mainEntry = table.Column<bool>(type: "bit", nullable: false),
                    sdnListSdnEntryuid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sdnListSdnEntryDateOfBirthItem", x => x.uid);
                    table.ForeignKey(
                        name: "FK_sdnListSdnEntryDateOfBirthItem_sdnListSdnEntry_sdnListSdnEntryuid",
                        column: x => x.sdnListSdnEntryuid,
                        principalTable: "sdnListSdnEntry",
                        principalColumn: "uid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "sdnListSdnEntryID",
                columns: table => new
                {
                    uid = table.Column<int>(type: "int", nullable: false),
                    idType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    idNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    idCountry = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    issueDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    expirationDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sdnListSdnEntryuid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sdnListSdnEntryID", x => x.uid);
                    table.ForeignKey(
                        name: "FK_sdnListSdnEntryID_sdnListSdnEntry_sdnListSdnEntryuid",
                        column: x => x.sdnListSdnEntryuid,
                        principalTable: "sdnListSdnEntry",
                        principalColumn: "uid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "sdnListSdnEntryNationality",
                columns: table => new
                {
                    uid = table.Column<int>(type: "int", nullable: false),
                    country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    mainEntry = table.Column<bool>(type: "bit", nullable: false),
                    sdnListSdnEntryuid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sdnListSdnEntryNationality", x => x.uid);
                    table.ForeignKey(
                        name: "FK_sdnListSdnEntryNationality_sdnListSdnEntry_sdnListSdnEntryuid",
                        column: x => x.sdnListSdnEntryuid,
                        principalTable: "sdnListSdnEntry",
                        principalColumn: "uid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "sdnListSdnEntryPlaceOfBirthItem",
                columns: table => new
                {
                    uid = table.Column<int>(type: "int", nullable: false),
                    placeOfBirth = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    mainEntry = table.Column<bool>(type: "bit", nullable: false),
                    sdnListSdnEntryuid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sdnListSdnEntryPlaceOfBirthItem", x => x.uid);
                    table.ForeignKey(
                        name: "FK_sdnListSdnEntryPlaceOfBirthItem_sdnListSdnEntry_sdnListSdnEntryuid",
                        column: x => x.sdnListSdnEntryuid,
                        principalTable: "sdnListSdnEntry",
                        principalColumn: "uid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_sdnLists_publshInformationid",
                table: "sdnLists",
                column: "publshInformationid");

            migrationBuilder.CreateIndex(
                name: "IX_sdnListSdnEntry_sdnListid",
                table: "sdnListSdnEntry",
                column: "sdnListid");

            migrationBuilder.CreateIndex(
                name: "IX_sdnListSdnEntry_vesselInfoid",
                table: "sdnListSdnEntry",
                column: "vesselInfoid");

            migrationBuilder.CreateIndex(
                name: "IX_sdnListSdnEntryAddress_sdnListSdnEntryuid",
                table: "sdnListSdnEntryAddress",
                column: "sdnListSdnEntryuid");

            migrationBuilder.CreateIndex(
                name: "IX_sdnListSdnEntryAka_sdnListSdnEntryuid",
                table: "sdnListSdnEntryAka",
                column: "sdnListSdnEntryuid");

            migrationBuilder.CreateIndex(
                name: "IX_sdnListSdnEntryCitizenship_sdnListSdnEntryuid",
                table: "sdnListSdnEntryCitizenship",
                column: "sdnListSdnEntryuid");

            migrationBuilder.CreateIndex(
                name: "IX_sdnListSdnEntryDateOfBirthItem_sdnListSdnEntryuid",
                table: "sdnListSdnEntryDateOfBirthItem",
                column: "sdnListSdnEntryuid");

            migrationBuilder.CreateIndex(
                name: "IX_sdnListSdnEntryID_sdnListSdnEntryuid",
                table: "sdnListSdnEntryID",
                column: "sdnListSdnEntryuid");

            migrationBuilder.CreateIndex(
                name: "IX_sdnListSdnEntryNationality_sdnListSdnEntryuid",
                table: "sdnListSdnEntryNationality",
                column: "sdnListSdnEntryuid");

            migrationBuilder.CreateIndex(
                name: "IX_sdnListSdnEntryPlaceOfBirthItem_sdnListSdnEntryuid",
                table: "sdnListSdnEntryPlaceOfBirthItem",
                column: "sdnListSdnEntryuid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sdnListSdnEntryAddress");

            migrationBuilder.DropTable(
                name: "sdnListSdnEntryAka");

            migrationBuilder.DropTable(
                name: "sdnListSdnEntryCitizenship");

            migrationBuilder.DropTable(
                name: "sdnListSdnEntryDateOfBirthItem");

            migrationBuilder.DropTable(
                name: "sdnListSdnEntryID");

            migrationBuilder.DropTable(
                name: "sdnListSdnEntryNationality");

            migrationBuilder.DropTable(
                name: "sdnListSdnEntryPlaceOfBirthItem");

            migrationBuilder.DropTable(
                name: "sdnListSdnEntry");

            migrationBuilder.DropTable(
                name: "sdnLists");

            migrationBuilder.DropTable(
                name: "sdnListSdnEntryVesselInfo");

            migrationBuilder.DropTable(
                name: "sdnListPublshInformation");
        }
    }
}
