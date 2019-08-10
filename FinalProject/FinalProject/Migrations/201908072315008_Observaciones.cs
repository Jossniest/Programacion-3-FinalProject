namespace FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Observaciones : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Observaciones",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Empleado = c.Int(nullable: false),
                        Observacion = c.String(nullable: false, maxLength: 100),
                        Fecha = c.DateTime(nullable: false),
                        Comentarios = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Empleados", t => t.Empleado, cascadeDelete: true)
                .Index(t => t.Empleado);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Observaciones", "Empleado", "dbo.Empleados");
            DropIndex("dbo.Observaciones", new[] { "Empleado" });
            DropTable("dbo.Observaciones");
        }
    }
}
