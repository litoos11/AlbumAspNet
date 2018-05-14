namespace Albums.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class toListRelation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FilePaths",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        TipoArchivo = c.Int(nullable: false),
                        Categoria_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categorias", t => t.Categoria_Id, cascadeDelete: true)
                .Index(t => t.Categoria_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FilePaths", "Categoria_Id", "dbo.Categorias");
            DropIndex("dbo.FilePaths", new[] { "Categoria_Id" });
            DropTable("dbo.FilePaths");
        }
    }
}
