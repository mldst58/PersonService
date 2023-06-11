using Dapper;
using Microsoft.Extensions.Logging;
using PersonService.Api.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PersonService.Api.DataAccess.Commands
{
    public class PersonCommandService : IPersonCommandService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AddressCommandService> _logger;
        public PersonCommandService(IUnitOfWork unitOfWork, ILogger<AddressCommandService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<int> Delete(long id, string uuid)
        {
            try
            {
                const string sql = @"UPDATE [dbo].[Person] 
                        SET Delete = 1,
                        ,[ModifiedBy] = @modedBy
                        ,[ModifiedDate] = SYSDATETIMEOFFSET()
                        WHERE Id=@Id";

                var rowCount = await _unitOfWork.Connection.ExecuteAsync(sql,
                    new
                    {
                        Id = id,
                        modedby = uuid
                    });

                if (rowCount == 0)
                {
                    _logger.LogError("PERSON_DELETE_FAIL", nameof(Delete));
                    throw new Exception("Issue with updating");
                }

                return rowCount;
            }
            catch (Exception e)
            {
                _logger.LogError(e, nameof(Update));
                throw;
            }
        }
        public async Task<long> Insert(NewPerson person, string uuid)
        {
            try
            {
                const string sql = @"
                    INSERT INTO [dbo].[Person] (
                        [AddressId]
                        ,[FirstName]
                        ,[LastName]
                        ,[PreferredName]
                        ,[Gender]
                        ,[Deleted]
                        ,[DateOfBirth]
                        ,[CreatedBy]
                        ,[CreatedDate])
                    VALUES (
                        @AddressId,
                        @FirstName,
                        @LastName,
                        @PreferredName,
                        @Gender,
                        @DateOfBirth,
                        @CreatedBy,
                        SYSDATETIMEOFFSET())
                        
                    SELECT CAST(SCOPE_IDENTITY() as int)";
                var result = await _unitOfWork.Connection.QueryAsync<long>(
                    sql,
                    new
                    {
                        person.AddressId,
                        person.FirstName,
                        person.LastName,
                        person.PreferredName,
                        person.Gender,
                        person.DateOfBirth,
                        person.CreatedBy
                    });
                return result.Single();
            }
            catch (Exception e)
            {
                _logger.LogError(e, nameof(Insert));
                throw;
            }
        }

        public async Task<Person> Update(long id, Person person, string uuid)
        {
            try
            {
                const string sql = @"
                    UPDATE [dbo].[Person] 
                    SET [AddressId] = @addressId
                    ,[FirstName] = @firstName
                    ,[LastName] = @lastName
                    ,[PreferredName] = @preferredName
                    ,[Gender] = @gender
                    ,[Deleted] = @deleted
                    ,[DateOfBirth] = @dateOfBirth
                    ,[ModifiedBy] = @modedBy
                    ,[ModifiedDate] = SYSDATETIMEOFFSET()
                       WHERE Id = @id";

                var rowCount = await _unitOfWork.Connection.ExecuteAsync(sql,
                    new
                    {
                        Id = id,
                        firstName = person.FirstName,
                        lastName = person.LastName,
                        addressId = person.AddressId,
                        preferredname = person.PreferredName,
                        dateofbirth = person.DateOfBirth,
                        gender = person.Gender,
                        deleted = person.Deleted,
                        modedBy = uuid
                    });

                if (rowCount == 0)
                {
                    _logger.LogError("PERSON_UPDATE_FAIL", nameof(Update));
                    throw new Exception("Issue with updating");
                }

                return person;
            }
            catch (Exception e)
            {
                _logger.LogError(e, nameof(Update));
                throw;
            }
        }
    }
}
