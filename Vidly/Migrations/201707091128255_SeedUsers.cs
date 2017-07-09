namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'7ea6fb26-ad35-4459-81b0-8c36fd34652f', N'guest@guest.com', 0, N'ABc5GEUh0stz891gb3vMBIR6kGpjfKXz8C0LPBwVkdqpqtB6MGFmvLG1NKQvFC6KIw==', N'a594932b-156b-4d4b-8cc2-01acd923a4ed', NULL, 0, 0, NULL, 1, 0, N'guest@guest.com')
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'b389770c-ca70-48af-8377-ff830e4fc24b', N'admin@admin.com', 0, N'APiryULZ+wSq4Pd9Ddl7ai/NPOPCvQe7PJIsNFGdGB++G8Id59p49p6BoXFf8GNgyg==', N'6385e683-b4a1-44a3-a6ab-91c740156e2c', NULL, 0, 0, NULL, 1, 0, N'admin@admin.com')
                INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'83e3e5ce-c0e0-4aae-b824-f64a42547fa9', N'CanManageMovies')
                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'b389770c-ca70-48af-8377-ff830e4fc24b', N'83e3e5ce-c0e0-4aae-b824-f64a42547fa9')");
        }
        
        public override void Down()
        {
        }
    }
}
