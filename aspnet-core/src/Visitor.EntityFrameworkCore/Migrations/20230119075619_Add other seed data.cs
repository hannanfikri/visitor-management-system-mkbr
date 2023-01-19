using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Visitor.Migrations
{
    public partial class Addotherseeddata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Towers",
                keyColumn: "Id",
                keyValue: new Guid("056c7b57-9c59-4fb3-b919-1d242a32f8af"));

            migrationBuilder.DeleteData(
                table: "Towers",
                keyColumn: "Id",
                keyValue: new Guid("39ec162d-ddf6-47a8-9e1e-ea7abac9cdd4"));

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "DepartmentName" },
                values: new object[,]
                {
                    { new Guid("2347f472-2023-4500-86a5-a3455ded2eb5"), "Network" },
                    { new Guid("3194773f-b3de-4195-b158-35f57144357b"), "Information Security" },
                    { new Guid("36cd3f01-4435-46fa-b193-789b4dc3983b"), "Information Systems" },
                    { new Guid("a2e34eb4-f8b9-4628-8c7d-e8ea723bb1d2"), "IT Solutions" },
                    { new Guid("e7e26f53-f8a2-4bf2-94d4-33623c0942fb"), "IT Administration" }
                });

            migrationBuilder.InsertData(
                table: "LevelAppointments",
                columns: new[] { "Id", "LevelBankRakyat" },
                values: new object[,]
                {
                    { new Guid("0121dce2-6533-4fc5-8b02-43739a117e35"), "2" },
                    { new Guid("044f1e53-5633-4106-ad98-c818016689fa"), "9" },
                    { new Guid("04b80398-c720-47f7-8617-5d37190dd155"), "18" },
                    { new Guid("05b7e1b8-9947-4f1b-9800-8391455c7aba"), "1" },
                    { new Guid("090a2267-9dbf-4891-ac82-66047a493da2"), "26" },
                    { new Guid("15b44837-5ecc-4a05-8566-12c15d39f97c"), "8" },
                    { new Guid("175c138e-e503-4194-8343-b3a384f83935"), "31" },
                    { new Guid("27812963-af4e-4a8c-9ede-e4a46524e2a2"), "35" },
                    { new Guid("347d92f2-e20b-4513-9cf7-7f0116049a83"), "17" },
                    { new Guid("380765a7-851b-4253-aa13-8e589e03a6bb"), "19" },
                    { new Guid("3d910523-6487-4fba-9a35-695825321ca3"), "21" },
                    { new Guid("47d3c895-d849-4f82-92a1-c329fb265e51"), "25" },
                    { new Guid("49e90414-fab3-4a58-ab51-e1306101da03"), "24" },
                    { new Guid("50ef3b84-23f7-4d2e-a113-8d66509033e6"), "16" },
                    { new Guid("686250e2-030a-4db0-ac84-030ff66f2765"), "4" },
                    { new Guid("6b633840-4de5-4dd2-ac24-53fd3f0299a4"), "20" },
                    { new Guid("7851e486-eb75-42ef-bf51-e8f27ded188b"), "38" },
                    { new Guid("7eebabff-ec74-43d3-a5e7-e32dd0cd5ac9"), "14" },
                    { new Guid("84547ee8-21b4-40a7-bbd1-babe99e19887"), "23" },
                    { new Guid("86283a67-6ce2-42ae-9011-00b248144e13"), "36" },
                    { new Guid("93fe53b0-1f53-43bb-8a8c-95ada3511c44"), "5" },
                    { new Guid("97080566-69b6-416f-b6b4-8ae15380ca84"), "6" },
                    { new Guid("9d6e0740-1c61-4bdf-a871-cf653419efa3"), "34" },
                    { new Guid("9fe516f2-09ff-4a28-aae0-f9a218b93abe"), "27" },
                    { new Guid("ac6b3790-1e9e-4c95-8d35-031a6573851f"), "7" },
                    { new Guid("b3d7abcd-a728-4c0d-9eb3-e2c8e2e3c2b1"), "15" },
                    { new Guid("c058eb1a-323b-4f21-bd2e-85451b3f9dee"), "28" },
                    { new Guid("c06f8af6-d8b7-4aa6-8984-6773d15de6bc"), "13" },
                    { new Guid("c478c23a-9633-41b1-8b57-30823f31c75a"), "37" },
                    { new Guid("c69fc78b-ca42-4576-9fc6-160ca31b7e99"), "3" },
                    { new Guid("d2847c23-f713-4ed9-aa17-a0b6108967d0"), "29" },
                    { new Guid("d6d02912-84a0-4bb3-ab1d-a96ea6b4dfa5"), "33" },
                    { new Guid("d8a34681-7ee9-4f19-8f2b-abd1d62dc5b6"), "12" },
                    { new Guid("e0721018-64f6-467f-a018-77f01c2c10fe"), "22" },
                    { new Guid("e286ac41-9269-43a0-badb-fd17f9824a97"), "11" },
                    { new Guid("e91d1a1f-4d98-4910-8e97-c81888935ea0"), "10" },
                    { new Guid("f2c96532-0a71-422c-971a-aefab56c5722"), "32" }
                });

            migrationBuilder.InsertData(
                table: "LevelAppointments",
                columns: new[] { "Id", "LevelBankRakyat" },
                values: new object[] { new Guid("f6de15d1-4f1b-4f3a-a70e-869a4f1bf98e"), "30" });

            migrationBuilder.InsertData(
                table: "PurposeOfVisitAppointments",
                columns: new[] { "Id", "PurposeOfVisitApp" },
                values: new object[,]
                {
                    { new Guid("2dfc18e1-0fc1-4b18-83a8-b682d4b7dfd3"), "Interview" },
                    { new Guid("4a36bbf8-a7f0-47ae-80c2-b2751cc5ef00"), "Meeting" },
                    { new Guid("6d315757-5fa0-4160-b69b-76b52a4c64af"), "Collect Cheque" },
                    { new Guid("6ea41212-4896-4422-a420-484ed8aa6517"), "Training" },
                    { new Guid("7add69b6-32b8-49d9-a080-6ff133a63ad8"), "Installation" },
                    { new Guid("89cb8563-44ac-4eae-b3d7-403ef4bd952a"), "Event" },
                    { new Guid("b9c8cdfa-aa84-423f-9587-82d900a4ac0b"), "Admission" },
                    { new Guid("bb4de330-2be1-4a21-b8a9-576544a1486e"), "Delivery" },
                    { new Guid("c7d4fcb8-0a8f-4db2-bcff-b1bb4c36b8f1"), "Visit" },
                    { new Guid("d799c225-f7a4-4472-aa7d-9b41c608afcf"), "Document Collection" },
                    { new Guid("dd047b80-3f21-4898-8cca-9af397fefb86"), "Event" },
                    { new Guid("f9d73081-344b-4cf4-a816-9a0a1d490a91"), "Vendor" },
                    { new Guid("fca33933-902b-4846-92b5-7704a4c27303"), "Discussion" }
                });

            migrationBuilder.InsertData(
                table: "Towers",
                columns: new[] { "Id", "TowerBankRakyat" },
                values: new object[,]
                {
                    { new Guid("3337cc54-122f-4347-b1dd-88861e13e042"), "Tower 2" },
                    { new Guid("b9b1547c-87a2-4638-bcbf-e930a85ab211"), "Tower 1" }
                });

            migrationBuilder.InsertData(
                table: "VisitorTitles",
                columns: new[] { "Id", "VisitorTitle" },
                values: new object[,]
                {
                    { new Guid("06fefa85-82c0-43b9-a029-3fa2feb12c13"), "Tan Sri" },
                    { new Guid("1d023354-dd25-46fb-bd32-50db08110a26"), "Mr" },
                    { new Guid("4016b884-e757-446e-af49-eb678de740c0"), "Ms" },
                    { new Guid("4292d77b-d529-4797-bba9-39ec7f780b7b"), "Sir" },
                    { new Guid("99f57da7-cdca-4a9a-a063-dc4f33cc269b"), "Dato Sri" },
                    { new Guid("9cf3266e-cc60-4658-8445-26007541e506"), "Mrs" },
                    { new Guid("a5ae3fd9-b377-461e-bdb0-b9a9927ae499"), "Puan Sri" }
                });

            migrationBuilder.InsertData(
                table: "companies",
                columns: new[] { "Id", "CompanyAddress", "CompanyEmail", "CompanyName", "OfficePhoneNumber" },
                values: new object[,]
                {
                    { new Guid("211b33a3-3e6c-4c48-a035-8487bf238cdd"), "No. 33, Jalan Rakyat, KL Sentral, 50740 Kuala Lumpur", "bankrakyat@bankrakyat.com", "Bank Rakyat", "0123456789" },
                    { new Guid("aad9dd05-41c5-4244-bbc7-91431f33d98c"), "No. 1, Blok C, Jalan Indah 2/6, Taman Indah, Batu 11, Cheras, 43200, Selangor, Darul Ehsan", "elaine@mieco.com", "Mieco", "0390759991" },
                    { new Guid("fc96341f-f3ff-40a0-aad2-8adf4a23c2b7"), "Lot 1327, Centre Point Commercial Centre, Jalan Melayu, 98007 Miri, Sarawak", "mecofurniture@yahoo.com", "Meco Furniture Trading Co.", "085437705" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("2347f472-2023-4500-86a5-a3455ded2eb5"));

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("3194773f-b3de-4195-b158-35f57144357b"));

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("36cd3f01-4435-46fa-b193-789b4dc3983b"));

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("a2e34eb4-f8b9-4628-8c7d-e8ea723bb1d2"));

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("e7e26f53-f8a2-4bf2-94d4-33623c0942fb"));

            migrationBuilder.DeleteData(
                table: "LevelAppointments",
                keyColumn: "Id",
                keyValue: new Guid("0121dce2-6533-4fc5-8b02-43739a117e35"));

            migrationBuilder.DeleteData(
                table: "LevelAppointments",
                keyColumn: "Id",
                keyValue: new Guid("044f1e53-5633-4106-ad98-c818016689fa"));

            migrationBuilder.DeleteData(
                table: "LevelAppointments",
                keyColumn: "Id",
                keyValue: new Guid("04b80398-c720-47f7-8617-5d37190dd155"));

            migrationBuilder.DeleteData(
                table: "LevelAppointments",
                keyColumn: "Id",
                keyValue: new Guid("05b7e1b8-9947-4f1b-9800-8391455c7aba"));

            migrationBuilder.DeleteData(
                table: "LevelAppointments",
                keyColumn: "Id",
                keyValue: new Guid("090a2267-9dbf-4891-ac82-66047a493da2"));

            migrationBuilder.DeleteData(
                table: "LevelAppointments",
                keyColumn: "Id",
                keyValue: new Guid("15b44837-5ecc-4a05-8566-12c15d39f97c"));

            migrationBuilder.DeleteData(
                table: "LevelAppointments",
                keyColumn: "Id",
                keyValue: new Guid("175c138e-e503-4194-8343-b3a384f83935"));

            migrationBuilder.DeleteData(
                table: "LevelAppointments",
                keyColumn: "Id",
                keyValue: new Guid("27812963-af4e-4a8c-9ede-e4a46524e2a2"));

            migrationBuilder.DeleteData(
                table: "LevelAppointments",
                keyColumn: "Id",
                keyValue: new Guid("347d92f2-e20b-4513-9cf7-7f0116049a83"));

            migrationBuilder.DeleteData(
                table: "LevelAppointments",
                keyColumn: "Id",
                keyValue: new Guid("380765a7-851b-4253-aa13-8e589e03a6bb"));

            migrationBuilder.DeleteData(
                table: "LevelAppointments",
                keyColumn: "Id",
                keyValue: new Guid("3d910523-6487-4fba-9a35-695825321ca3"));

            migrationBuilder.DeleteData(
                table: "LevelAppointments",
                keyColumn: "Id",
                keyValue: new Guid("47d3c895-d849-4f82-92a1-c329fb265e51"));

            migrationBuilder.DeleteData(
                table: "LevelAppointments",
                keyColumn: "Id",
                keyValue: new Guid("49e90414-fab3-4a58-ab51-e1306101da03"));

            migrationBuilder.DeleteData(
                table: "LevelAppointments",
                keyColumn: "Id",
                keyValue: new Guid("50ef3b84-23f7-4d2e-a113-8d66509033e6"));

            migrationBuilder.DeleteData(
                table: "LevelAppointments",
                keyColumn: "Id",
                keyValue: new Guid("686250e2-030a-4db0-ac84-030ff66f2765"));

            migrationBuilder.DeleteData(
                table: "LevelAppointments",
                keyColumn: "Id",
                keyValue: new Guid("6b633840-4de5-4dd2-ac24-53fd3f0299a4"));

            migrationBuilder.DeleteData(
                table: "LevelAppointments",
                keyColumn: "Id",
                keyValue: new Guid("7851e486-eb75-42ef-bf51-e8f27ded188b"));

            migrationBuilder.DeleteData(
                table: "LevelAppointments",
                keyColumn: "Id",
                keyValue: new Guid("7eebabff-ec74-43d3-a5e7-e32dd0cd5ac9"));

            migrationBuilder.DeleteData(
                table: "LevelAppointments",
                keyColumn: "Id",
                keyValue: new Guid("84547ee8-21b4-40a7-bbd1-babe99e19887"));

            migrationBuilder.DeleteData(
                table: "LevelAppointments",
                keyColumn: "Id",
                keyValue: new Guid("86283a67-6ce2-42ae-9011-00b248144e13"));

            migrationBuilder.DeleteData(
                table: "LevelAppointments",
                keyColumn: "Id",
                keyValue: new Guid("93fe53b0-1f53-43bb-8a8c-95ada3511c44"));

            migrationBuilder.DeleteData(
                table: "LevelAppointments",
                keyColumn: "Id",
                keyValue: new Guid("97080566-69b6-416f-b6b4-8ae15380ca84"));

            migrationBuilder.DeleteData(
                table: "LevelAppointments",
                keyColumn: "Id",
                keyValue: new Guid("9d6e0740-1c61-4bdf-a871-cf653419efa3"));

            migrationBuilder.DeleteData(
                table: "LevelAppointments",
                keyColumn: "Id",
                keyValue: new Guid("9fe516f2-09ff-4a28-aae0-f9a218b93abe"));

            migrationBuilder.DeleteData(
                table: "LevelAppointments",
                keyColumn: "Id",
                keyValue: new Guid("ac6b3790-1e9e-4c95-8d35-031a6573851f"));

            migrationBuilder.DeleteData(
                table: "LevelAppointments",
                keyColumn: "Id",
                keyValue: new Guid("b3d7abcd-a728-4c0d-9eb3-e2c8e2e3c2b1"));

            migrationBuilder.DeleteData(
                table: "LevelAppointments",
                keyColumn: "Id",
                keyValue: new Guid("c058eb1a-323b-4f21-bd2e-85451b3f9dee"));

            migrationBuilder.DeleteData(
                table: "LevelAppointments",
                keyColumn: "Id",
                keyValue: new Guid("c06f8af6-d8b7-4aa6-8984-6773d15de6bc"));

            migrationBuilder.DeleteData(
                table: "LevelAppointments",
                keyColumn: "Id",
                keyValue: new Guid("c478c23a-9633-41b1-8b57-30823f31c75a"));

            migrationBuilder.DeleteData(
                table: "LevelAppointments",
                keyColumn: "Id",
                keyValue: new Guid("c69fc78b-ca42-4576-9fc6-160ca31b7e99"));

            migrationBuilder.DeleteData(
                table: "LevelAppointments",
                keyColumn: "Id",
                keyValue: new Guid("d2847c23-f713-4ed9-aa17-a0b6108967d0"));

            migrationBuilder.DeleteData(
                table: "LevelAppointments",
                keyColumn: "Id",
                keyValue: new Guid("d6d02912-84a0-4bb3-ab1d-a96ea6b4dfa5"));

            migrationBuilder.DeleteData(
                table: "LevelAppointments",
                keyColumn: "Id",
                keyValue: new Guid("d8a34681-7ee9-4f19-8f2b-abd1d62dc5b6"));

            migrationBuilder.DeleteData(
                table: "LevelAppointments",
                keyColumn: "Id",
                keyValue: new Guid("e0721018-64f6-467f-a018-77f01c2c10fe"));

            migrationBuilder.DeleteData(
                table: "LevelAppointments",
                keyColumn: "Id",
                keyValue: new Guid("e286ac41-9269-43a0-badb-fd17f9824a97"));

            migrationBuilder.DeleteData(
                table: "LevelAppointments",
                keyColumn: "Id",
                keyValue: new Guid("e91d1a1f-4d98-4910-8e97-c81888935ea0"));

            migrationBuilder.DeleteData(
                table: "LevelAppointments",
                keyColumn: "Id",
                keyValue: new Guid("f2c96532-0a71-422c-971a-aefab56c5722"));

            migrationBuilder.DeleteData(
                table: "LevelAppointments",
                keyColumn: "Id",
                keyValue: new Guid("f6de15d1-4f1b-4f3a-a70e-869a4f1bf98e"));

            migrationBuilder.DeleteData(
                table: "PurposeOfVisitAppointments",
                keyColumn: "Id",
                keyValue: new Guid("2dfc18e1-0fc1-4b18-83a8-b682d4b7dfd3"));

            migrationBuilder.DeleteData(
                table: "PurposeOfVisitAppointments",
                keyColumn: "Id",
                keyValue: new Guid("4a36bbf8-a7f0-47ae-80c2-b2751cc5ef00"));

            migrationBuilder.DeleteData(
                table: "PurposeOfVisitAppointments",
                keyColumn: "Id",
                keyValue: new Guid("6d315757-5fa0-4160-b69b-76b52a4c64af"));

            migrationBuilder.DeleteData(
                table: "PurposeOfVisitAppointments",
                keyColumn: "Id",
                keyValue: new Guid("6ea41212-4896-4422-a420-484ed8aa6517"));

            migrationBuilder.DeleteData(
                table: "PurposeOfVisitAppointments",
                keyColumn: "Id",
                keyValue: new Guid("7add69b6-32b8-49d9-a080-6ff133a63ad8"));

            migrationBuilder.DeleteData(
                table: "PurposeOfVisitAppointments",
                keyColumn: "Id",
                keyValue: new Guid("89cb8563-44ac-4eae-b3d7-403ef4bd952a"));

            migrationBuilder.DeleteData(
                table: "PurposeOfVisitAppointments",
                keyColumn: "Id",
                keyValue: new Guid("b9c8cdfa-aa84-423f-9587-82d900a4ac0b"));

            migrationBuilder.DeleteData(
                table: "PurposeOfVisitAppointments",
                keyColumn: "Id",
                keyValue: new Guid("bb4de330-2be1-4a21-b8a9-576544a1486e"));

            migrationBuilder.DeleteData(
                table: "PurposeOfVisitAppointments",
                keyColumn: "Id",
                keyValue: new Guid("c7d4fcb8-0a8f-4db2-bcff-b1bb4c36b8f1"));

            migrationBuilder.DeleteData(
                table: "PurposeOfVisitAppointments",
                keyColumn: "Id",
                keyValue: new Guid("d799c225-f7a4-4472-aa7d-9b41c608afcf"));

            migrationBuilder.DeleteData(
                table: "PurposeOfVisitAppointments",
                keyColumn: "Id",
                keyValue: new Guid("dd047b80-3f21-4898-8cca-9af397fefb86"));

            migrationBuilder.DeleteData(
                table: "PurposeOfVisitAppointments",
                keyColumn: "Id",
                keyValue: new Guid("f9d73081-344b-4cf4-a816-9a0a1d490a91"));

            migrationBuilder.DeleteData(
                table: "PurposeOfVisitAppointments",
                keyColumn: "Id",
                keyValue: new Guid("fca33933-902b-4846-92b5-7704a4c27303"));

            migrationBuilder.DeleteData(
                table: "Towers",
                keyColumn: "Id",
                keyValue: new Guid("3337cc54-122f-4347-b1dd-88861e13e042"));

            migrationBuilder.DeleteData(
                table: "Towers",
                keyColumn: "Id",
                keyValue: new Guid("b9b1547c-87a2-4638-bcbf-e930a85ab211"));

            migrationBuilder.DeleteData(
                table: "VisitorTitles",
                keyColumn: "Id",
                keyValue: new Guid("06fefa85-82c0-43b9-a029-3fa2feb12c13"));

            migrationBuilder.DeleteData(
                table: "VisitorTitles",
                keyColumn: "Id",
                keyValue: new Guid("1d023354-dd25-46fb-bd32-50db08110a26"));

            migrationBuilder.DeleteData(
                table: "VisitorTitles",
                keyColumn: "Id",
                keyValue: new Guid("4016b884-e757-446e-af49-eb678de740c0"));

            migrationBuilder.DeleteData(
                table: "VisitorTitles",
                keyColumn: "Id",
                keyValue: new Guid("4292d77b-d529-4797-bba9-39ec7f780b7b"));

            migrationBuilder.DeleteData(
                table: "VisitorTitles",
                keyColumn: "Id",
                keyValue: new Guid("99f57da7-cdca-4a9a-a063-dc4f33cc269b"));

            migrationBuilder.DeleteData(
                table: "VisitorTitles",
                keyColumn: "Id",
                keyValue: new Guid("9cf3266e-cc60-4658-8445-26007541e506"));

            migrationBuilder.DeleteData(
                table: "VisitorTitles",
                keyColumn: "Id",
                keyValue: new Guid("a5ae3fd9-b377-461e-bdb0-b9a9927ae499"));

            migrationBuilder.DeleteData(
                table: "companies",
                keyColumn: "Id",
                keyValue: new Guid("211b33a3-3e6c-4c48-a035-8487bf238cdd"));

            migrationBuilder.DeleteData(
                table: "companies",
                keyColumn: "Id",
                keyValue: new Guid("aad9dd05-41c5-4244-bbc7-91431f33d98c"));

            migrationBuilder.DeleteData(
                table: "companies",
                keyColumn: "Id",
                keyValue: new Guid("fc96341f-f3ff-40a0-aad2-8adf4a23c2b7"));

            migrationBuilder.InsertData(
                table: "Towers",
                columns: new[] { "Id", "TowerBankRakyat" },
                values: new object[] { new Guid("056c7b57-9c59-4fb3-b919-1d242a32f8af"), "Tower 2" });

            migrationBuilder.InsertData(
                table: "Towers",
                columns: new[] { "Id", "TowerBankRakyat" },
                values: new object[] { new Guid("39ec162d-ddf6-47a8-9e1e-ea7abac9cdd4"), "Tower 1" });
        }
    }
}
