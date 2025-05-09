﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContaCerta.Infra.Migrations
{
    /// <inheritdoc />
    public partial class InicialV2Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nickname",
                table: "Users",
                newName: "NickName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NickName",
                table: "Users",
                newName: "Nickname");
        }
    }
}
