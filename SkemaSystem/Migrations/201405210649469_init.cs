namespace SkemaSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClassModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClassName = c.String(nullable: false),
                        Education_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Educations", t => t.Education_Id, cascadeDelete: true)
                .Index(t => t.Education_Id);
            
            CreateTable(
                "dbo.Schemes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClassModel_Id = c.Int(nullable: false),
                        Semester_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ClassModels", t => t.ClassModel_Id, cascadeDelete: true)
                .ForeignKey("dbo.Semesters", t => t.Semester_Id, cascadeDelete: true)
                .Index(t => t.ClassModel_Id)
                .Index(t => t.Semester_Id);
            
            CreateTable(
                "dbo.Semesters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.Int(nullable: false),
                        Education_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Educations", t => t.Education_Id)
                .Index(t => t.Education_Id);
            
            CreateTable(
                "dbo.SemesterSubjectBlocks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BlocksCount = c.Int(nullable: false),
                        Subject_Id = c.Int(nullable: false),
                        Semester_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Subjects", t => t.Subject_Id, cascadeDelete: true)
                .ForeignKey("dbo.Semesters", t => t.Semester_Id)
                .Index(t => t.Subject_Id)
                .Index(t => t.Semester_Id);
            
            CreateTable(
                "dbo.Subjects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SubjectDistBlocks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BlocksCount = c.Int(nullable: false),
                        Subject_Id = c.Int(nullable: false),
                        Teacher_Id = c.Int(nullable: false),
                        Scheme_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Subjects", t => t.Subject_Id, cascadeDelete: true)
                .ForeignKey("dbo.Teachers", t => t.Teacher_Id, cascadeDelete: true)
                .ForeignKey("dbo.Schemes", t => t.Scheme_Id)
                .Index(t => t.Subject_Id)
                .Index(t => t.Teacher_Id)
                .Index(t => t.Scheme_Id);
            
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Educations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EducationTeachers",
                c => new
                    {
                        Education_Id = c.Int(nullable: false),
                        Teacher_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Education_Id, t.Teacher_Id })
                .ForeignKey("dbo.Educations", t => t.Education_Id, cascadeDelete: true)
                .ForeignKey("dbo.Teachers", t => t.Teacher_Id, cascadeDelete: true)
                .Index(t => t.Education_Id)
                .Index(t => t.Teacher_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ClassModels", "Education_Id", "dbo.Educations");
            DropForeignKey("dbo.SubjectDistBlocks", "Scheme_Id", "dbo.Schemes");
            DropForeignKey("dbo.SubjectDistBlocks", "Teacher_Id", "dbo.Teachers");
            DropForeignKey("dbo.EducationTeachers", "Teacher_Id", "dbo.Teachers");
            DropForeignKey("dbo.EducationTeachers", "Education_Id", "dbo.Educations");
            DropForeignKey("dbo.Semesters", "Education_Id", "dbo.Educations");
            DropForeignKey("dbo.SubjectDistBlocks", "Subject_Id", "dbo.Subjects");
            DropForeignKey("dbo.Schemes", "Semester_Id", "dbo.Semesters");
            DropForeignKey("dbo.SemesterSubjectBlocks", "Semester_Id", "dbo.Semesters");
            DropForeignKey("dbo.SemesterSubjectBlocks", "Subject_Id", "dbo.Subjects");
            DropForeignKey("dbo.Schemes", "ClassModel_Id", "dbo.ClassModels");
            DropIndex("dbo.ClassModels", new[] { "Education_Id" });
            DropIndex("dbo.SubjectDistBlocks", new[] { "Scheme_Id" });
            DropIndex("dbo.SubjectDistBlocks", new[] { "Teacher_Id" });
            DropIndex("dbo.EducationTeachers", new[] { "Teacher_Id" });
            DropIndex("dbo.EducationTeachers", new[] { "Education_Id" });
            DropIndex("dbo.Semesters", new[] { "Education_Id" });
            DropIndex("dbo.SubjectDistBlocks", new[] { "Subject_Id" });
            DropIndex("dbo.Schemes", new[] { "Semester_Id" });
            DropIndex("dbo.SemesterSubjectBlocks", new[] { "Semester_Id" });
            DropIndex("dbo.SemesterSubjectBlocks", new[] { "Subject_Id" });
            DropIndex("dbo.Schemes", new[] { "ClassModel_Id" });
            DropTable("dbo.EducationTeachers");
            DropTable("dbo.Educations");
            DropTable("dbo.Teachers");
            DropTable("dbo.SubjectDistBlocks");
            DropTable("dbo.Subjects");
            DropTable("dbo.SemesterSubjectBlocks");
            DropTable("dbo.Semesters");
            DropTable("dbo.Schemes");
            DropTable("dbo.ClassModels");
        }
    }
}
