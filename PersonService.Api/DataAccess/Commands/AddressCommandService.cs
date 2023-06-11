using System;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using PersonService.Api.Models;
using Microsoft.Extensions.Logging;

namespace PersonService.Api.DataAccess.Commands
{
    public class AddressCommandService : IAddressCommandService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AddressCommandService> _logger;
        public AddressCommandService(IUnitOfWork unitOfWork, ILogger<AddressCommandService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<int> Delete(long id, string uuid)
        {
            try
            {
                const string sql = @"
                    UPDATE [dbo].[Address] 
                    SET [Deleted] = 1
                    ,[ModifiedBy] = @modedBy
                    ,[ModifiedDate] = SYSDATETIMEOFFSET()
                     WHERE Id = @id";

                var rowCount = await _unitOfWork.Connection.ExecuteAsync(sql,
                    new
                    {
                        Id = id,
                        modedby = uuid
                    });

                if (rowCount == 0)
                {
                    _logger.LogError("ADDRESS_DELETE_FAIL", nameof(Delete));
                    throw new Exception("Issue with updating");
                }

                return rowCount;
            }
            catch (Exception e)
            {
                _logger.LogError(e, nameof(Delete));
                throw;
            }
        }

        public async Task<long> Insert(NewAddress address, string uuid)
        {
            try
            {
                const string sql = @"
                INSERT INTO [dbo].[Address] (
                    [Address]
                    ,[Address2]
                    ,[City]
                    ,[State]
                    ,[Deleted]
                    ,[CreatedBy]
                    ,[CreatedDate])
                VALUES (
                    @Address,
                    @Address2,
                    @City,
                    @State,
                    0,
                    @CreatedBy,
                    SYSDATETIMEOFFSET())
                
                SELECT CAST(SCOPE_IDENTITY() as int)";

                var result = await _unitOfWork.Connection.QueryAsync<long>(
                    sql,
                    new
                    {
                        address.Address,
                        address.Address2,
                        address.City,
                        address.ZipCode,
                        address.State,
                        uuid
                    });
                return result.Single();
            }
            catch (Exception e)
            {
                _logger.LogError(e, nameof(Insert));
                throw;
            }
        }

        public async Task<Address> Update(long id, Address address, string uuid)
        {
            try
            {
                const string sql = @"
                    UPDATE [dbo].[Address] 
                    SET [Address] = @address
                    ,[Address2] = @address2
                    ,[City] = @city
                    ,[ZipCode] = @zipcode
                    ,[State] = @state
                    ,[ModifiedBy] = @modedBy
                    ,[ModifiedDate] = SYSDATETIMEOFFSET()
                     WHERE Id = @id";

                var rowCount = await _unitOfWork.Connection.ExecuteAsync(sql,
                    new
                    {
                        Id = id,
                        address = address.Address,
                        address2 = address.Address2,
                        city = address.City,
                        zipcode = address.ZipCode,
                        state = address.State,
                        modedBy = uuid
                    });

                if (rowCount == 0)
                {
                    _logger.LogError("ADDRESS_UPDATE_FAIL", nameof(Update));
                    throw new Exception("Issue with updating");
                }

                return address;
            }
            catch (Exception e)
            {
                _logger.LogError(e, nameof(Update));
                throw;
            }
        }
    }
}
