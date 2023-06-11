using FluentMigrator;

namespace PersonService.Api.Migrations
{
    [Migration(202012291530)]
    public class AddPersonTable : Migration
    {
        public override void Up()
        {
            Create.Table("Person")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("AddressId").AsInt64()
                .WithColumn("FirstName").AsString(250)
                .WithColumn("LastName").AsString(250)
                .WithColumn("PreferredName").AsString(500)
                .WithColumn("Gender").AsByte()
                .WithColumn("Deleted").AsByte()
                .WithColumn("DateOfBirth").AsDateTimeOffset()
                .WithColumn("CreatedDate").AsDateTimeOffset()
                .WithColumn("CreatedBy").AsString(250)
                .WithColumn("ModifiedDate").AsDateTimeOffset()
                .WithColumn("ModifiedBy").AsString(250);

            Create.ForeignKey("fk_Person_AddressId_Address_Id")
                .FromTable("Address").ForeignColumn("AddressId")
                .ToTable("Address").PrimaryColumn("Id");
        }

        public override void Down()
        {
            Delete.Table("Person");
        }
    }
}