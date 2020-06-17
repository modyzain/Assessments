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

namespace App.API.Controllers.GpDetails
{
    public class PostHandler : IRequestHandler<GpDetailsCommand, int>
    {
        private readonly IConfiguration _configuration;

        public PostHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> Handle(GpDetailsCommand command, CancellationToken cancellationToken)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DBConnection")))
            {
                await connection.OpenAsync();
                var result = await connection.ExecuteScalarCommandAsync<int>("Patient_GpDetailsManage_Sp", new
                {
                    command.Check,
                    command.PkGpDetailsId,
                    command.FkPatientId,
                    command.GpCode,
                    command.GpSurName,
                    command.GpInitials,
                    command.GpPhone
                }, commandType: CommandType.StoredProcedure);

                return result;
            }
        }
    }
    public class GetListHandler : IRequestHandler<GpDetailsQuery, List<GpDetailsResponse>>
    {
        private readonly IConfiguration _configuration;

        public GetListHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<GpDetailsResponse>> Handle(GpDetailsQuery query, CancellationToken cancellationToken)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DBConnection")))
            {
                await connection.OpenAsync();

                var result = await connection.ExecuteAsync<GpDetailsResponse>("Patient_GpDetailsGetAll_Sp", new { query.FkPatientId }, commandType: CommandType.StoredProcedure);

                return result.ToList();
            }
        }
    }
    public class GetByIdHandler : IRequestHandler<GpDetailsByIdQuery, GpDetailsResponse>
    {
        private readonly IConfiguration _configuration;

        public GetByIdHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<GpDetailsResponse> Handle(GpDetailsByIdQuery query, CancellationToken cancellationToken)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DBConnection")))
            {
                await connection.OpenAsync();

                var result = await connection.FirstOrDefaultExecuteAsync<GpDetailsResponse>("Patient_GpDetailsGetById_Sp", new { query.PkGpDetailsId }, commandType: CommandType.StoredProcedure);

                return result;
            }
        }
    }
}
