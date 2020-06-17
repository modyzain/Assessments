using App.Domain.DTOs;
using App.Infrastructure.Services;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace App.API.Controllers.Relationship
{
    public class GetListHandler : IRequestHandler<NokRelationshipQuery, List<NokRelationshipResponse>>
    {
        private readonly IConfiguration _configuration;

        public GetListHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<NokRelationshipResponse>> Handle(NokRelationshipQuery query, CancellationToken cancellationToken)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DBConnection")))
            {
                await connection.OpenAsync();

                var result = await connection.ExecuteAsync<NokRelationshipResponse>("NokRelationship_GetAll_Sp", null, commandType: CommandType.StoredProcedure);

                return result.ToList();
            }
        }
    }
}
