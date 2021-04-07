using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Redpier.Infrastructure.Persistence.Migrations
{
    public partial class removeRequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DockerEndpoints",
                keyColumn: "Id",
                keyValue: new Guid("87b18a25-8581-48b0-baae-4c65b0a056ad"));

            migrationBuilder.DeleteData(
                table: "DockerEndpoints",
                keyColumn: "Id",
                keyValue: new Guid("a01cb5ea-1716-4fec-b3a8-61c3474c8f5c"));

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "DockerEndpoints",
                type: "TEXT",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 128);

            migrationBuilder.InsertData(
                table: "DockerEndpoints",
                columns: new[] { "Id", "Created", "CreatedBy", "LastModified", "LastModifiedBy", "Name", "Type", "Uri" },
                values: new object[] { new Guid("fece90eb-9efe-4f04-99c1-34ddf4efee86"), new DateTime(2021, 4, 3, 12, 48, 40, 62, DateTimeKind.Local).AddTicks(2198), null, null, null, "Docker Desktop for Windows", "socket", "npipe://./pipe/docker_engine" });

            migrationBuilder.InsertData(
                table: "DockerEndpoints",
                columns: new[] { "Id", "Created", "CreatedBy", "LastModified", "LastModifiedBy", "Name", "Type", "Uri" },
                values: new object[] { new Guid("bdf653f8-d28d-4aa1-9285-fbca5572627b"), new DateTime(2021, 4, 3, 12, 48, 40, 63, DateTimeKind.Local).AddTicks(5989), null, null, null, "Local Docker Engine on Linux", "socket", "/var/run/docker.sock" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DockerEndpoints",
                keyColumn: "Id",
                keyValue: new Guid("bdf653f8-d28d-4aa1-9285-fbca5572627b"));

            migrationBuilder.DeleteData(
                table: "DockerEndpoints",
                keyColumn: "Id",
                keyValue: new Guid("fece90eb-9efe-4f04-99c1-34ddf4efee86"));

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "DockerEndpoints",
                type: "TEXT",
                maxLength: 128,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "DockerEndpoints",
                columns: new[] { "Id", "Created", "CreatedBy", "LastModified", "LastModifiedBy", "Name", "Type", "Uri" },
                values: new object[] { new Guid("a01cb5ea-1716-4fec-b3a8-61c3474c8f5c"), new DateTime(2021, 3, 27, 17, 59, 31, 493, DateTimeKind.Local).AddTicks(5243), null, null, null, "Docker Desktop for Windows", "socket", "npipe://./pipe/docker_engine" });

            migrationBuilder.InsertData(
                table: "DockerEndpoints",
                columns: new[] { "Id", "Created", "CreatedBy", "LastModified", "LastModifiedBy", "Name", "Type", "Uri" },
                values: new object[] { new Guid("87b18a25-8581-48b0-baae-4c65b0a056ad"), new DateTime(2021, 3, 27, 17, 59, 31, 494, DateTimeKind.Local).AddTicks(9004), null, null, null, "Local Docker Engine on Linux", "socket", "/var/run/docker.sock" });
        }
    }
}
