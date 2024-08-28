using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DATN.Core.Migrations
{
    /// <inheritdoc />
    public partial class ud2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Attribute_EAV",
                columns: table => new
                {
                    AttributeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AttributeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attribute_EAV", x => x.AttributeId);
                });

            migrationBuilder.CreateTable(
                name: "Product_EAV",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product_EAV", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "AttributeValue_EAV",
                columns: table => new
                {
                    AttributeValueId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AttributeId = table.Column<int>(type: "int", nullable: false),
                    ValueText = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeValue_EAV", x => x.AttributeValueId);
                    table.ForeignKey(
                        name: "FK_AttributeValue_EAV_Attribute_EAV_AttributeId",
                        column: x => x.AttributeId,
                        principalTable: "Attribute_EAV",
                        principalColumn: "AttributeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Variant",
                columns: table => new
                {
                    VariantId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    VariantName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PuscharPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SalePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AfterDiscountPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Variant", x => x.VariantId);
                    table.ForeignKey(
                        name: "FK_Variant_Product_EAV_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product_EAV",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VariantAttribute",
                columns: table => new
                {
                    VariantAttributeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VariantId = table.Column<int>(type: "int", nullable: false),
                    AttributeValueId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VariantAttribute", x => x.VariantAttributeId);
                    table.ForeignKey(
                        name: "FK_VariantAttribute_AttributeValue_EAV_AttributeValueId",
                        column: x => x.AttributeValueId,
                        principalTable: "AttributeValue_EAV",
                        principalColumn: "AttributeValueId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VariantAttribute_Variant_VariantId",
                        column: x => x.VariantId,
                        principalTable: "Variant",
                        principalColumn: "VariantId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("00bb44d1-f674-49f6-bdae-afb143ab9749"),
                columns: new[] { "ConcurrencyStamp", "LastLoginTime", "PasswordHash" },
                values: new object[] { "ecc351d4-a604-4565-98ff-a356cd91d660", new DateTime(2024, 8, 28, 4, 19, 16, 715, DateTimeKind.Utc).AddTicks(5650), "AQAAAAIAAYagAAAAEBARpaiG0S2T4MWEfG+Ni5XAwgPRL/Uwm+hjJBS9kmVINo85HXUG9b7K6COsJFcMPQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("2753c921-2304-4f8d-b8d5-75229d3b20d6"),
                columns: new[] { "ConcurrencyStamp", "LastLoginTime", "PasswordHash" },
                values: new object[] { "4b74952a-1443-46e5-8ea0-8a7055800e29", new DateTime(2024, 8, 28, 4, 19, 16, 644, DateTimeKind.Utc).AddTicks(9825), "AQAAAAIAAYagAAAAEFAbqE+yr+NNKVPygsFAbRfXMfbbAi1/0BvT9vaAAM9cMmTMzhQA+YMg7daTNY9Wvw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("aa7c5218-4f1e-4ac6-a3b4-08dcb162e29e"),
                columns: new[] { "ConcurrencyStamp", "LastLoginTime", "PasswordHash" },
                values: new object[] { "99360d21-d839-400b-b63d-0f2855480db1", new DateTime(2024, 8, 28, 4, 19, 16, 782, DateTimeKind.Utc).AddTicks(3106), "AQAAAAIAAYagAAAAEFXgqBJPj71jPHh2HaoCOBkAwvUHk3tRBX0HuThP1fQf3Q4bbj7SMgtxA8XCI/MLxA==" });

            migrationBuilder.InsertData(
                table: "Attribute_EAV",
                columns: new[] { "AttributeId", "AttributeName" },
                values: new object[,]
                {
                    { 1, "Màu sắc" },
                    { 2, "Kích thước" },
                    { 3, "Dung tích" }
                });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(3833), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(3844), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(3844) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(3848), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(3849), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(3849) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(3941), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(3941), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(3942) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(3943), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(3943), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(3944) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(3945), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(3945), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(3946) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(3947), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(3947), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(3947) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(3949), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(3949), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(3949) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(3950), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(3951), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(3951) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(3952), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(3953), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(3953) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(3964), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(3965), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(3973) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(3986), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(3986), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(3986) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(3988), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(3988), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(3988) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(3989), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(3990), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(3990) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(3996), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(3996), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(3997) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(3998), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(3998), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(3999) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4000), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4001), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4001) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4002), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4003), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4003) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4004), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4005), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4005) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4006), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4007), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4007) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4008), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4009), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4010) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4011), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4011), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4012) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4014), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4014), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4014) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4016), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4016), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4016) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4018), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4018), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4018) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4020), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4020), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4020) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4022), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4022), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4022) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4024), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4024), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4024) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4026), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4026), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4026) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4028), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4028), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4029) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4030), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4030), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4031) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4032), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4032), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4033) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4034), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4034), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4035) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4036), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4036), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4037) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4038), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4038), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4039) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4040), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4040), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4040) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4042), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4042), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4042) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4043), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4044), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4044) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4045), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4045), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4046) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4050), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4050), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4050) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4059), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4060), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4060) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 41,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4061), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4062), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4062) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4064), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4064), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4064) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 43,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4065), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4066), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4066) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 44,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4067), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4068), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4068) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 45,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4069), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4070), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4070) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 46,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4071), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4072), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4072) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 47,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4073), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4074), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4074) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 48,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4075), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4075), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4076) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 49,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4077), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4078), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4078) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 50,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4079), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4080), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4080) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 51,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4081), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4081), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4082) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 52,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4083), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4083), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4084) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 53,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4085), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4085), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4086) });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(6235));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(6262));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 3,
                column: "Date",
                value: new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(6265));

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "InvoiceId",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(6315));

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "InvoiceId",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(6320));

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "InvoiceId",
                keyValue: 3,
                column: "CreateDate",
                value: new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(6321));

            migrationBuilder.UpdateData(
                table: "Language",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 576, DateTimeKind.Local).AddTicks(9288), new DateTime(2024, 8, 28, 11, 19, 16, 576, DateTimeKind.Local).AddTicks(9288), new DateTime(2024, 8, 28, 11, 19, 16, 576, DateTimeKind.Local).AddTicks(9289) });

            migrationBuilder.UpdateData(
                table: "Language",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 576, DateTimeKind.Local).AddTicks(9290), new DateTime(2024, 8, 28, 11, 19, 16, 576, DateTimeKind.Local).AddTicks(9290), new DateTime(2024, 8, 28, 11, 19, 16, 576, DateTimeKind.Local).AddTicks(9291) });

            migrationBuilder.UpdateData(
                table: "Magazines",
                keyColumn: "MagazineId",
                keyValue: 1,
                column: "CreateAt",
                value: new DateTime(2024, 8, 29, 11, 19, 16, 576, DateTimeKind.Local).AddTicks(9266));

            migrationBuilder.UpdateData(
                table: "Magazines",
                keyColumn: "MagazineId",
                keyValue: 2,
                column: "CreateAt",
                value: new DateTime(2024, 8, 30, 11, 19, 16, 576, DateTimeKind.Local).AddTicks(9272));

            migrationBuilder.UpdateData(
                table: "Magazines",
                keyColumn: "MagazineId",
                keyValue: 3,
                column: "CreateAt",
                value: new DateTime(2024, 8, 31, 11, 19, 16, 576, DateTimeKind.Local).AddTicks(9274));

            migrationBuilder.UpdateData(
                table: "Magazines",
                keyColumn: "MagazineId",
                keyValue: 4,
                column: "CreateAt",
                value: new DateTime(2024, 9, 1, 11, 19, 16, 576, DateTimeKind.Local).AddTicks(9276));

            migrationBuilder.UpdateData(
                table: "Origins",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4188), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4186), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4187) });

            migrationBuilder.UpdateData(
                table: "Origins",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4190), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4189), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4190) });

            migrationBuilder.UpdateData(
                table: "Origins",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4193), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4192), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4192) });

            migrationBuilder.UpdateData(
                table: "Origins",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4195), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4194), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4194) });

            migrationBuilder.UpdateData(
                table: "Origins",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4198), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4196), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4196) });

            migrationBuilder.UpdateData(
                table: "Origins",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4200), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4199), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4199) });

            migrationBuilder.UpdateData(
                table: "Origins",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4202), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4201), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4201) });

            migrationBuilder.UpdateData(
                table: "Origins",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4204), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4203), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4203) });

            migrationBuilder.UpdateData(
                table: "Origins",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4207), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4205), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4205) });

            migrationBuilder.UpdateData(
                table: "Origins",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4209), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4208), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4208) });

            migrationBuilder.InsertData(
                table: "Product_EAV",
                columns: new[] { "ProductId", "ProductName" },
                values: new object[,]
                {
                    { 1, "Samsung Smart TV QLED QA55Q70C" },
                    { 2, "Tủ lạnh LG Inverter Multi Door GR-B50BL" }
                });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateAt", "CreateBy", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4273), new Guid("6d396541-fc1f-4a6c-80b3-5bc0626a8418"), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4274) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreateAt", "CreateBy", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4286), new Guid("87917bef-cbaf-4d5a-9c65-c2866df54258"), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4287) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreateAt", "CreateBy", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4291), new Guid("a7f7ec3f-a32a-4002-8207-e2016abf57f1"), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4291) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreateAt", "CreateBy", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4295), new Guid("7d01cd62-20db-4f30-beda-230e3e57f9f3"), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4295) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreateAt", "CreateBy", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4298), new Guid("b3eb32e5-fef0-4fdf-bf45-39c9f400ebfb"), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4298) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreateAt", "CreateBy", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4301), new Guid("0c9fe7a0-5519-413c-990d-8dc2816c5a01"), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4302) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreateAt", "CreateBy", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4305), new Guid("e9ff68e3-7614-4347-966a-3f5d4db8aa8c"), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4305) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreateAt", "CreateBy", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4308), new Guid("f04a0b8c-279d-495b-919e-cb6a3b1d496b"), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4309) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreateAt", "CreateBy", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4312), new Guid("5492b7eb-43b9-4149-b65c-aad9d05d4014"), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4312) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreateAt", "CreateBy", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4327), new Guid("4957e61b-e547-41c1-a1ec-33496d4d450f"), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4328) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreateAt", "CreateBy", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4331), new Guid("fbfe9a27-b8e6-4009-a495-8d69b18ee062"), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4331) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreateAt", "CreateBy", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4335), new Guid("4ad187b1-076a-4409-9a79-60fc98000a63"), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(4335) });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateAt", "DeleteAt", "From", "To", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 576, DateTimeKind.Local).AddTicks(8993), new DateTime(2024, 8, 28, 11, 19, 16, 576, DateTimeKind.Local).AddTicks(9007), new DateTime(2024, 8, 28, 11, 19, 16, 576, DateTimeKind.Local).AddTicks(9013), new DateTime(2024, 9, 28, 11, 19, 16, 576, DateTimeKind.Local).AddTicks(9015), new DateTime(2024, 8, 28, 11, 19, 16, 576, DateTimeKind.Local).AddTicks(9011) });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreateAt", "DeleteAt", "From", "To", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 576, DateTimeKind.Local).AddTicks(9022), new DateTime(2024, 8, 28, 11, 19, 16, 576, DateTimeKind.Local).AddTicks(9022), new DateTime(2024, 8, 28, 11, 19, 16, 576, DateTimeKind.Local).AddTicks(9023), new DateTime(2024, 10, 28, 11, 19, 16, 576, DateTimeKind.Local).AddTicks(9024), new DateTime(2024, 8, 28, 11, 19, 16, 576, DateTimeKind.Local).AddTicks(9022) });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreateAt", "DeleteAt", "From", "To", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 576, DateTimeKind.Local).AddTicks(9025), new DateTime(2024, 8, 28, 11, 19, 16, 576, DateTimeKind.Local).AddTicks(9026), new DateTime(2024, 8, 28, 11, 19, 16, 576, DateTimeKind.Local).AddTicks(9027), new DateTime(2024, 11, 28, 11, 19, 16, 576, DateTimeKind.Local).AddTicks(9028), new DateTime(2024, 8, 28, 11, 19, 16, 576, DateTimeKind.Local).AddTicks(9026) });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreateAt", "DeleteAt", "From", "To", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 576, DateTimeKind.Local).AddTicks(9029), new DateTime(2024, 8, 28, 11, 19, 16, 576, DateTimeKind.Local).AddTicks(9029), new DateTime(2024, 8, 28, 11, 19, 16, 576, DateTimeKind.Local).AddTicks(9031), new DateTime(2024, 12, 28, 11, 19, 16, 576, DateTimeKind.Local).AddTicks(9031), new DateTime(2024, 8, 28, 11, 19, 16, 576, DateTimeKind.Local).AddTicks(9029) });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreateAt", "DeleteAt", "From", "To", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 576, DateTimeKind.Local).AddTicks(9032), new DateTime(2024, 8, 28, 11, 19, 16, 576, DateTimeKind.Local).AddTicks(9032), new DateTime(2024, 8, 28, 11, 19, 16, 576, DateTimeKind.Local).AddTicks(9034), new DateTime(2025, 1, 28, 11, 19, 16, 576, DateTimeKind.Local).AddTicks(9035), new DateTime(2024, 8, 28, 11, 19, 16, 576, DateTimeKind.Local).AddTicks(9033) });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreateAt", "DeleteAt", "From", "To", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 576, DateTimeKind.Local).AddTicks(9036), new DateTime(2024, 8, 28, 11, 19, 16, 576, DateTimeKind.Local).AddTicks(9036), new DateTime(2024, 8, 28, 11, 19, 16, 576, DateTimeKind.Local).AddTicks(9038), new DateTime(2025, 2, 28, 11, 19, 16, 576, DateTimeKind.Local).AddTicks(9039), new DateTime(2024, 8, 28, 11, 19, 16, 576, DateTimeKind.Local).AddTicks(9037) });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreateAt", "DeleteAt", "From", "To", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 576, DateTimeKind.Local).AddTicks(9040), new DateTime(2024, 8, 28, 11, 19, 16, 576, DateTimeKind.Local).AddTicks(9040), new DateTime(2024, 8, 28, 11, 19, 16, 576, DateTimeKind.Local).AddTicks(9042), new DateTime(2025, 3, 28, 11, 19, 16, 576, DateTimeKind.Local).AddTicks(9042), new DateTime(2024, 8, 28, 11, 19, 16, 576, DateTimeKind.Local).AddTicks(9041) });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreateAt", "DeleteAt", "From", "To", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 576, DateTimeKind.Local).AddTicks(9043), new DateTime(2024, 8, 28, 11, 19, 16, 576, DateTimeKind.Local).AddTicks(9043), new DateTime(2024, 8, 28, 11, 19, 16, 576, DateTimeKind.Local).AddTicks(9045), new DateTime(2025, 4, 28, 11, 19, 16, 576, DateTimeKind.Local).AddTicks(9045), new DateTime(2024, 8, 28, 11, 19, 16, 576, DateTimeKind.Local).AddTicks(9044) });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreateAt", "DeleteAt", "From", "To", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 576, DateTimeKind.Local).AddTicks(9046), new DateTime(2024, 8, 28, 11, 19, 16, 576, DateTimeKind.Local).AddTicks(9046), new DateTime(2024, 8, 28, 11, 19, 16, 576, DateTimeKind.Local).AddTicks(9048), new DateTime(2025, 5, 28, 11, 19, 16, 576, DateTimeKind.Local).AddTicks(9048), new DateTime(2024, 8, 28, 11, 19, 16, 576, DateTimeKind.Local).AddTicks(9047) });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreateAt", "DeleteAt", "From", "To", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 576, DateTimeKind.Local).AddTicks(9049), new DateTime(2024, 8, 28, 11, 19, 16, 576, DateTimeKind.Local).AddTicks(9049), new DateTime(2024, 8, 28, 11, 19, 16, 576, DateTimeKind.Local).AddTicks(9051), new DateTime(2025, 6, 28, 11, 19, 16, 576, DateTimeKind.Local).AddTicks(9052), new DateTime(2024, 8, 28, 11, 19, 16, 576, DateTimeKind.Local).AddTicks(9050) });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreateAt", "DeleteAt", "From", "To", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 576, DateTimeKind.Local).AddTicks(9054), new DateTime(2024, 8, 28, 11, 19, 16, 576, DateTimeKind.Local).AddTicks(9054), new DateTime(2024, 8, 28, 11, 19, 16, 576, DateTimeKind.Local).AddTicks(9055), new DateTime(2025, 6, 28, 11, 19, 16, 576, DateTimeKind.Local).AddTicks(9056), new DateTime(2024, 8, 28, 11, 19, 16, 576, DateTimeKind.Local).AddTicks(9054) });

            migrationBuilder.UpdateData(
                table: "ShippingOrders",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(6603), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(6599), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(6599) });

            migrationBuilder.UpdateData(
                table: "VoucherUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateAt", "DeleteAt", "From", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(6562), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(6562), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(6567), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(6563) });

            migrationBuilder.UpdateData(
                table: "VoucherUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreateAt", "DeleteAt", "From", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(6569), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(6569), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(6571), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(6569) });

            migrationBuilder.UpdateData(
                table: "VoucherUsers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreateAt", "DeleteAt", "From", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(6571), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(6572), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(6573), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(6572) });

            migrationBuilder.UpdateData(
                table: "Vouchers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(6533), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(6525), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(6526) });

            migrationBuilder.UpdateData(
                table: "Vouchers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(6537), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(6535), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(6535) });

            migrationBuilder.UpdateData(
                table: "Vouchers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(6541), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(6538), new DateTime(2024, 8, 28, 11, 19, 16, 782, DateTimeKind.Local).AddTicks(6538) });

            migrationBuilder.InsertData(
                table: "AttributeValue_EAV",
                columns: new[] { "AttributeValueId", "AttributeId", "ValueText" },
                values: new object[,]
                {
                    { 1, 1, "Đen" },
                    { 2, 1, "Trắng" },
                    { 3, 2, "50 inch" },
                    { 4, 2, "60 inch" },
                    { 5, 3, "40 lít" },
                    { 6, 3, "50 lít" }
                });

            migrationBuilder.InsertData(
                table: "Variant",
                columns: new[] { "VariantId", "AfterDiscountPrice", "ProductId", "PuscharPrice", "Quantity", "SalePrice", "VariantName" },
                values: new object[,]
                {
                    { 1, 70000m, 1, 50000m, 100, 75000m, "Đen/50 inch" },
                    { 2, 71000m, 1, 52000m, 50, 78000m, "Đen/60 inch" },
                    { 3, 70500m, 1, 48000m, 75, 73000m, "Trắng/50 inch" },
                    { 4, 71000m, 1, 49000m, 80, 74000m, "Trắng/60 inch" },
                    { 5, 71000m, 2, 49000m, 0, 74000m, "Đen/40 lít" },
                    { 6, 71000m, 2, 49000m, 80, 74000m, "Đen/50 lít" }
                });

            migrationBuilder.InsertData(
                table: "VariantAttribute",
                columns: new[] { "VariantAttributeId", "AttributeValueId", "VariantId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 3, 1 },
                    { 3, 1, 2 },
                    { 4, 4, 2 },
                    { 5, 2, 3 },
                    { 6, 3, 3 },
                    { 7, 2, 4 },
                    { 8, 4, 4 },
                    { 9, 1, 5 },
                    { 10, 5, 5 },
                    { 11, 1, 6 },
                    { 12, 6, 6 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AttributeValue_EAV_AttributeId",
                table: "AttributeValue_EAV",
                column: "AttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_Variant_ProductId",
                table: "Variant",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_VariantAttribute_AttributeValueId",
                table: "VariantAttribute",
                column: "AttributeValueId");

            migrationBuilder.CreateIndex(
                name: "IX_VariantAttribute_VariantId",
                table: "VariantAttribute",
                column: "VariantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VariantAttribute");

            migrationBuilder.DropTable(
                name: "AttributeValue_EAV");

            migrationBuilder.DropTable(
                name: "Variant");

            migrationBuilder.DropTable(
                name: "Attribute_EAV");

            migrationBuilder.DropTable(
                name: "Product_EAV");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("00bb44d1-f674-49f6-bdae-afb143ab9749"),
                columns: new[] { "ConcurrencyStamp", "LastLoginTime", "PasswordHash" },
                values: new object[] { "a80581bb-afdf-4656-be23-767dc9553fea", new DateTime(2024, 8, 28, 1, 49, 16, 323, DateTimeKind.Utc).AddTicks(1415), "AQAAAAIAAYagAAAAEPjc5f4SB4JLmr3njiMdO2vBz3ino8w2lEaLOjB2YsmZvMEJmZI+bryhCtRMiZvP3w==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("2753c921-2304-4f8d-b8d5-75229d3b20d6"),
                columns: new[] { "ConcurrencyStamp", "LastLoginTime", "PasswordHash" },
                values: new object[] { "c2dc530c-06dc-4a7b-9026-6b829442deb9", new DateTime(2024, 8, 28, 1, 49, 16, 244, DateTimeKind.Utc).AddTicks(6943), "AQAAAAIAAYagAAAAEDllB8DpyX/ojuckII3mwnRoLhCS+20EMpMeDPJ+Edpul+1upfZ4dZQ3r7BK8FtIFg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("aa7c5218-4f1e-4ac6-a3b4-08dcb162e29e"),
                columns: new[] { "ConcurrencyStamp", "LastLoginTime", "PasswordHash" },
                values: new object[] { "5a89deff-e03e-4c54-879c-de91a2eb0d74", new DateTime(2024, 8, 28, 1, 49, 16, 417, DateTimeKind.Utc).AddTicks(8130), "AQAAAAIAAYagAAAAEPw7D7mMF0Gh0+Zr/lP9IghJXh/eT0btm/HKrtoWTCk/rjGmjWoNA5PdYhNPdEk9/g==" });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(8990), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9009), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9011) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9015), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9015), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9016) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9121), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9121), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9122) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9123), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9123), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9124) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9125), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9125), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9126) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9127), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9127), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9127) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9128), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9129), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9129) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9131), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9131), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9131) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9132), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9133), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9133) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9145), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9145), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9152) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9167), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9168), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9168) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9169), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9169), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9170) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9171), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9171), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9172) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9177), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9177), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9177) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9178), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9179), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9179) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9183), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9183), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9184) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9185), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9185), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9186) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9187), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9188), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9188) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9190), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9191), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9191) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9192), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9193), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9193) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9194), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9195), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9195) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9196), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9197), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9197) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9198), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9199), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9199) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9200), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9201), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9201) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9202), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9203), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9203) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9204), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9205), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9205) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9206), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9207), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9207) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9208), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9209), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9209) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9210), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9211), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9211) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9212), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9213), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9213) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9258), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9258), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9259) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9261), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9261), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9262) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9263), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9263), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9264) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9265), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9265), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9266) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9267), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9267), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9267) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9269), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9270), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9270) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9271), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9272), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9272) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9273), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9273), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9274) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9278), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9278), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9279) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9280), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9280), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9281) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 41,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9282), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9282), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9283) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9284), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9284), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9284) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 43,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9286), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9286), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9286) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 44,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9287), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9288), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9288) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 45,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9289), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9290), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9290) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 46,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9291), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9292), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9292) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 47,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9293), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9293), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9294) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 48,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9295), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9295), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9296) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 49,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9297), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9297), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9298) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 50,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9299), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9300), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9300) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 51,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9301), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9302), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9302) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 52,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9303), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9304), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9304) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 53,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9305), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9305), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9306) });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2024, 8, 28, 8, 49, 16, 418, DateTimeKind.Local).AddTicks(2131));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2024, 8, 28, 8, 49, 16, 418, DateTimeKind.Local).AddTicks(2170));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 3,
                column: "Date",
                value: new DateTime(2024, 8, 28, 8, 49, 16, 418, DateTimeKind.Local).AddTicks(2174));

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "InvoiceId",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2024, 8, 28, 8, 49, 16, 418, DateTimeKind.Local).AddTicks(2282));

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "InvoiceId",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2024, 8, 28, 8, 49, 16, 418, DateTimeKind.Local).AddTicks(2289));

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "InvoiceId",
                keyValue: 3,
                column: "CreateDate",
                value: new DateTime(2024, 8, 28, 8, 49, 16, 418, DateTimeKind.Local).AddTicks(2292));

            migrationBuilder.UpdateData(
                table: "Language",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 175, DateTimeKind.Local).AddTicks(8689), new DateTime(2024, 8, 28, 8, 49, 16, 175, DateTimeKind.Local).AddTicks(8689), new DateTime(2024, 8, 28, 8, 49, 16, 175, DateTimeKind.Local).AddTicks(8690) });

            migrationBuilder.UpdateData(
                table: "Language",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 175, DateTimeKind.Local).AddTicks(8691), new DateTime(2024, 8, 28, 8, 49, 16, 175, DateTimeKind.Local).AddTicks(8691), new DateTime(2024, 8, 28, 8, 49, 16, 175, DateTimeKind.Local).AddTicks(8692) });

            migrationBuilder.UpdateData(
                table: "Magazines",
                keyColumn: "MagazineId",
                keyValue: 1,
                column: "CreateAt",
                value: new DateTime(2024, 8, 29, 8, 49, 16, 175, DateTimeKind.Local).AddTicks(8665));

            migrationBuilder.UpdateData(
                table: "Magazines",
                keyColumn: "MagazineId",
                keyValue: 2,
                column: "CreateAt",
                value: new DateTime(2024, 8, 30, 8, 49, 16, 175, DateTimeKind.Local).AddTicks(8670));

            migrationBuilder.UpdateData(
                table: "Magazines",
                keyColumn: "MagazineId",
                keyValue: 3,
                column: "CreateAt",
                value: new DateTime(2024, 8, 31, 8, 49, 16, 175, DateTimeKind.Local).AddTicks(8672));

            migrationBuilder.UpdateData(
                table: "Magazines",
                keyColumn: "MagazineId",
                keyValue: 4,
                column: "CreateAt",
                value: new DateTime(2024, 9, 1, 8, 49, 16, 175, DateTimeKind.Local).AddTicks(8673));

            migrationBuilder.UpdateData(
                table: "Origins",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9419), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9417), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9418) });

            migrationBuilder.UpdateData(
                table: "Origins",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9422), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9421), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9421) });

            migrationBuilder.UpdateData(
                table: "Origins",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9424), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9423), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9423) });

            migrationBuilder.UpdateData(
                table: "Origins",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9426), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9425), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9426) });

            migrationBuilder.UpdateData(
                table: "Origins",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9429), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9427), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9428) });

            migrationBuilder.UpdateData(
                table: "Origins",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9431), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9430), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9430) });

            migrationBuilder.UpdateData(
                table: "Origins",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9433), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9432), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9433) });

            migrationBuilder.UpdateData(
                table: "Origins",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9435), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9434), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9435) });

            migrationBuilder.UpdateData(
                table: "Origins",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9437), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9436), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9437) });

            migrationBuilder.UpdateData(
                table: "Origins",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9440), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9438), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9439) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateAt", "CreateBy", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9569), new Guid("26589066-8148-46a9-9b9f-03613e7e1889"), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9569) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreateAt", "CreateBy", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9574), new Guid("d266840f-3023-4579-ba7b-d7371637fa91"), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9574) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreateAt", "CreateBy", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9577), new Guid("ce56a074-98fb-4399-ae86-7ea6b023a9ba"), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9578) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreateAt", "CreateBy", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9581), new Guid("2e6cd13f-c04d-4915-9972-7acd31207aa9"), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9581) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreateAt", "CreateBy", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9584), new Guid("8748e688-fb34-42e4-aead-48a0c0fe1ab8"), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9585) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreateAt", "CreateBy", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9588), new Guid("0123da8f-7dfa-48a2-909c-5fa1b681b9f3"), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9588) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreateAt", "CreateBy", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9591), new Guid("d2cef5ad-4595-4979-8e18-c8b7aa5ca961"), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9592) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreateAt", "CreateBy", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9595), new Guid("ec4664e8-4fb8-4fad-8e26-79295c5a60e6"), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9595) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreateAt", "CreateBy", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9601), new Guid("50590da3-27f3-47bb-94c2-fc8565987f7a"), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9602) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreateAt", "CreateBy", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9605), new Guid("e20a6e1f-3430-4dc4-ad50-6eb8ae2957b9"), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9606) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreateAt", "CreateBy", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9608), new Guid("80bec1ca-e6b9-413a-aa49-c62bbf042ebd"), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9609) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreateAt", "CreateBy", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9612), new Guid("1ccea9ab-7acf-4c26-9dbc-b9a1806b9d90"), new DateTime(2024, 8, 28, 8, 49, 16, 417, DateTimeKind.Local).AddTicks(9613) });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateAt", "DeleteAt", "From", "To", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 175, DateTimeKind.Local).AddTicks(8354), new DateTime(2024, 8, 28, 8, 49, 16, 175, DateTimeKind.Local).AddTicks(8363), new DateTime(2024, 8, 28, 8, 49, 16, 175, DateTimeKind.Local).AddTicks(8370), new DateTime(2024, 9, 28, 8, 49, 16, 175, DateTimeKind.Local).AddTicks(8371), new DateTime(2024, 8, 28, 8, 49, 16, 175, DateTimeKind.Local).AddTicks(8367) });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreateAt", "DeleteAt", "From", "To", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 175, DateTimeKind.Local).AddTicks(8378), new DateTime(2024, 8, 28, 8, 49, 16, 175, DateTimeKind.Local).AddTicks(8378), new DateTime(2024, 8, 28, 8, 49, 16, 175, DateTimeKind.Local).AddTicks(8379), new DateTime(2024, 10, 28, 8, 49, 16, 175, DateTimeKind.Local).AddTicks(8380), new DateTime(2024, 8, 28, 8, 49, 16, 175, DateTimeKind.Local).AddTicks(8378) });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreateAt", "DeleteAt", "From", "To", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 175, DateTimeKind.Local).AddTicks(8381), new DateTime(2024, 8, 28, 8, 49, 16, 175, DateTimeKind.Local).AddTicks(8381), new DateTime(2024, 8, 28, 8, 49, 16, 175, DateTimeKind.Local).AddTicks(8383), new DateTime(2024, 11, 28, 8, 49, 16, 175, DateTimeKind.Local).AddTicks(8383), new DateTime(2024, 8, 28, 8, 49, 16, 175, DateTimeKind.Local).AddTicks(8382) });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreateAt", "DeleteAt", "From", "To", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 175, DateTimeKind.Local).AddTicks(8384), new DateTime(2024, 8, 28, 8, 49, 16, 175, DateTimeKind.Local).AddTicks(8385), new DateTime(2024, 8, 28, 8, 49, 16, 175, DateTimeKind.Local).AddTicks(8386), new DateTime(2024, 12, 28, 8, 49, 16, 175, DateTimeKind.Local).AddTicks(8386), new DateTime(2024, 8, 28, 8, 49, 16, 175, DateTimeKind.Local).AddTicks(8385) });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreateAt", "DeleteAt", "From", "To", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 175, DateTimeKind.Local).AddTicks(8387), new DateTime(2024, 8, 28, 8, 49, 16, 175, DateTimeKind.Local).AddTicks(8388), new DateTime(2024, 8, 28, 8, 49, 16, 175, DateTimeKind.Local).AddTicks(8389), new DateTime(2025, 1, 28, 8, 49, 16, 175, DateTimeKind.Local).AddTicks(8389), new DateTime(2024, 8, 28, 8, 49, 16, 175, DateTimeKind.Local).AddTicks(8388) });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreateAt", "DeleteAt", "From", "To", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 175, DateTimeKind.Local).AddTicks(8390), new DateTime(2024, 8, 28, 8, 49, 16, 175, DateTimeKind.Local).AddTicks(8391), new DateTime(2024, 8, 28, 8, 49, 16, 175, DateTimeKind.Local).AddTicks(8392), new DateTime(2025, 2, 28, 8, 49, 16, 175, DateTimeKind.Local).AddTicks(8393), new DateTime(2024, 8, 28, 8, 49, 16, 175, DateTimeKind.Local).AddTicks(8391) });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreateAt", "DeleteAt", "From", "To", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 175, DateTimeKind.Local).AddTicks(8394), new DateTime(2024, 8, 28, 8, 49, 16, 175, DateTimeKind.Local).AddTicks(8394), new DateTime(2024, 8, 28, 8, 49, 16, 175, DateTimeKind.Local).AddTicks(8395), new DateTime(2025, 3, 28, 8, 49, 16, 175, DateTimeKind.Local).AddTicks(8396), new DateTime(2024, 8, 28, 8, 49, 16, 175, DateTimeKind.Local).AddTicks(8394) });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreateAt", "DeleteAt", "From", "To", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 175, DateTimeKind.Local).AddTicks(8437), new DateTime(2024, 8, 28, 8, 49, 16, 175, DateTimeKind.Local).AddTicks(8438), new DateTime(2024, 8, 28, 8, 49, 16, 175, DateTimeKind.Local).AddTicks(8440), new DateTime(2025, 4, 28, 8, 49, 16, 175, DateTimeKind.Local).AddTicks(8440), new DateTime(2024, 8, 28, 8, 49, 16, 175, DateTimeKind.Local).AddTicks(8439) });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreateAt", "DeleteAt", "From", "To", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 175, DateTimeKind.Local).AddTicks(8442), new DateTime(2024, 8, 28, 8, 49, 16, 175, DateTimeKind.Local).AddTicks(8442), new DateTime(2024, 8, 28, 8, 49, 16, 175, DateTimeKind.Local).AddTicks(8443), new DateTime(2025, 5, 28, 8, 49, 16, 175, DateTimeKind.Local).AddTicks(8444), new DateTime(2024, 8, 28, 8, 49, 16, 175, DateTimeKind.Local).AddTicks(8443) });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreateAt", "DeleteAt", "From", "To", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 175, DateTimeKind.Local).AddTicks(8445), new DateTime(2024, 8, 28, 8, 49, 16, 175, DateTimeKind.Local).AddTicks(8445), new DateTime(2024, 8, 28, 8, 49, 16, 175, DateTimeKind.Local).AddTicks(8446), new DateTime(2025, 6, 28, 8, 49, 16, 175, DateTimeKind.Local).AddTicks(8447), new DateTime(2024, 8, 28, 8, 49, 16, 175, DateTimeKind.Local).AddTicks(8446) });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreateAt", "DeleteAt", "From", "To", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 175, DateTimeKind.Local).AddTicks(8448), new DateTime(2024, 8, 28, 8, 49, 16, 175, DateTimeKind.Local).AddTicks(8449), new DateTime(2024, 8, 28, 8, 49, 16, 175, DateTimeKind.Local).AddTicks(8450), new DateTime(2025, 6, 28, 8, 49, 16, 175, DateTimeKind.Local).AddTicks(8450), new DateTime(2024, 8, 28, 8, 49, 16, 175, DateTimeKind.Local).AddTicks(8449) });

            migrationBuilder.UpdateData(
                table: "ShippingOrders",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 418, DateTimeKind.Local).AddTicks(2870), new DateTime(2024, 8, 28, 8, 49, 16, 418, DateTimeKind.Local).AddTicks(2861), new DateTime(2024, 8, 28, 8, 49, 16, 418, DateTimeKind.Local).AddTicks(2861) });

            migrationBuilder.UpdateData(
                table: "VoucherUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateAt", "DeleteAt", "From", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 418, DateTimeKind.Local).AddTicks(2766), new DateTime(2024, 8, 28, 8, 49, 16, 418, DateTimeKind.Local).AddTicks(2767), new DateTime(2024, 8, 28, 8, 49, 16, 418, DateTimeKind.Local).AddTicks(2781), new DateTime(2024, 8, 28, 8, 49, 16, 418, DateTimeKind.Local).AddTicks(2768) });

            migrationBuilder.UpdateData(
                table: "VoucherUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreateAt", "DeleteAt", "From", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 418, DateTimeKind.Local).AddTicks(2783), new DateTime(2024, 8, 28, 8, 49, 16, 418, DateTimeKind.Local).AddTicks(2783), new DateTime(2024, 8, 28, 8, 49, 16, 418, DateTimeKind.Local).AddTicks(2786), new DateTime(2024, 8, 28, 8, 49, 16, 418, DateTimeKind.Local).AddTicks(2784) });

            migrationBuilder.UpdateData(
                table: "VoucherUsers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreateAt", "DeleteAt", "From", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 418, DateTimeKind.Local).AddTicks(2787), new DateTime(2024, 8, 28, 8, 49, 16, 418, DateTimeKind.Local).AddTicks(2787), new DateTime(2024, 8, 28, 8, 49, 16, 418, DateTimeKind.Local).AddTicks(2790), new DateTime(2024, 8, 28, 8, 49, 16, 418, DateTimeKind.Local).AddTicks(2788) });

            migrationBuilder.UpdateData(
                table: "Vouchers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 418, DateTimeKind.Local).AddTicks(2705), new DateTime(2024, 8, 28, 8, 49, 16, 418, DateTimeKind.Local).AddTicks(2697), new DateTime(2024, 8, 28, 8, 49, 16, 418, DateTimeKind.Local).AddTicks(2699) });

            migrationBuilder.UpdateData(
                table: "Vouchers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 418, DateTimeKind.Local).AddTicks(2711), new DateTime(2024, 8, 28, 8, 49, 16, 418, DateTimeKind.Local).AddTicks(2709), new DateTime(2024, 8, 28, 8, 49, 16, 418, DateTimeKind.Local).AddTicks(2709) });

            migrationBuilder.UpdateData(
                table: "Vouchers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreateAt", "DeleteAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 8, 28, 8, 49, 16, 418, DateTimeKind.Local).AddTicks(2716), new DateTime(2024, 8, 28, 8, 49, 16, 418, DateTimeKind.Local).AddTicks(2712), new DateTime(2024, 8, 28, 8, 49, 16, 418, DateTimeKind.Local).AddTicks(2712) });
        }
    }
}
