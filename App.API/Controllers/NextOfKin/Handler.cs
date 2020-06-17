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

namespace App.API.Controllers.NextOfKin
{
    public class PostHandler : IRequestHandler<NextOfKinCommand, int>
    {
        private readonly IConfiguration _configuration;

        public PostHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> Handle(NextOfKinCommand command, CancellationToken cancellationToken)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DBConnection")))
            {
                await connection.OpenAsync();
                var result = await connection.ExecuteScalarCommandAsync<int>("Patient_NextOfKinManage_Sp", new
                {
                    command.Check,
                    command.PkNokId,
                    command.FkPatientId,
                    command.FkNokRelationshipId,
                    command.NokName,
                    command.NokAddressLine1,
                    command.NokAddressLine2,
                    command.NokAddressLine3,
                    command.NokAddressLine4,
                    command.NokPostcode
                }, commandType: CommandType.StoredProcedure);

                return result;
            }
        }
    }
    public class GetListHandler : IRequestHandler<NextOfKinQuery, List<NextOfKinResponse>>
    {
        private readonly IConfiguration _configuration;

        public GetListHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<NextOfKinResponse>> Handle(NextOfKinQuery query, CancellationToken cancellationToken)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DBConnection")))
            {
                await connection.OpenAsync();

                var result = await connection.ExecuteAsync<NextOfKinResponse>("Patient_NextOfKinGetAll_Sp", new { query.FkPatientId }, commandType: CommandType.StoredProcedure);

                return result.ToList();
            }
        }
    }
    public class GetByIdHandler : IRequestHandler<NextOfKinByIdQuery, NextOfKinResponse>
    {
        private readonly IConfiguration _configuration;

        public GetByIdHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<NextOfKinResponse> Handle(NextOfKinByIdQuery query, CancellationToken cancellationToken)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DBConnection")))
            {
                await connection.OpenAsync();

                var result = await connection.FirstOrDefaultExecuteAsync<NextOfKinResponse>("Patient_NextOfKinGetById_Sp", new { query.PkNokId }, commandType: CommandType.StoredProcedure);

                return result;
            }
        }
    }
}
