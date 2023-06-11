using FluentMigrator;

namespace PersonService.Api.Migrations
{
    [Migration(202012291135)]
    public class AddAddressTable : Migration
    {
        public override void Up()
        {
            Create.Table("Address")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Address").AsString(1000)
                .WithColumn("Address2").AsString(1000)
                .WithColumn("City").AsString(100)
                .WithColumn("ZipCode").AsString(10)
                .WithColumn("State").AsString(2)
                .WithColumn("Deleted").AsByte()
                .WithColumn("CreatedDate").AsDateTimeOffset()
                .WithColumn("CreatedBy").AsString(250)
                .WithColumn("ModifiedDate").AsDateTimeOffset()
                .WithColumn("ModifiedBy").AsString(250);
        }

        public override void Down()
        {
            Delete.Table("Address");
        }
    }
}