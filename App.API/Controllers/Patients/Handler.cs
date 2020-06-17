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

namespace App.API.Controllers.Patients
{
    public class PostHandler : IRequestHandler<PatientCommand, int>
    {
        private readonly IConfiguration _configuration;

        public PostHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> Handle(PatientCommand command, CancellationToken cancellationToken)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DBConnection")))
            {
                await connection.OpenAsync();
                var result = await connection.ExecuteScalarCommandAsync<int>("Patient_BasicManage_Sp", new
                {
                    command.Check,
                    command.PkPatientId,
                    command.Gender,
                    command.PasNumber,
                    command.ForeName,
                    command.SurName,
                    command.DateOfBirth,
                    command.HomeTelephoneNumber
                }, commandType: CommandType.StoredProcedure);

                return result;
            }
        }
    }
    public class GetListHandler : IRequestHandler<PatientQuery, List<PatientResponse>>
    {
        private readonly IConfiguration _configuration;

        public GetListHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<PatientResponse>> Handle(PatientQuery query, CancellationToken cancellationToken)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DBConnection")))
            {
                await connection.OpenAsync();

                var result = await connection.ExecuteAsync<PatientResponse>("Patient_BasicGetAll_Sp", null, commandType: CommandType.StoredProcedure);

                return result.ToList();
            }
        }
    }
    public class GetByIdHandler : IRequestHandler<PatientByIdQuery, PatientResponse>
    {
        private readonly IConfiguration _configuration;

        public GetByIdHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<PatientResponse> Handle(PatientByIdQuery query, CancellationToken cancellationToken)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DBConnection")))
            {
                await connection.OpenAsync();

                var result = await connection.FirstOrDefaultExecuteAsync<PatientResponse>("Patient_BasicGetById_Sp", new { query.PkPatientId }, commandType: CommandType.StoredProcedure);

                return result;
            }
        }
    }
    public class GetInfoByIdHandler : IRequestHandler<PatientInfoQuery, List<PatientInfoResponse>>
    {
        private readonly IConfiguration _configuration;

        public GetInfoByIdHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<PatientInfoResponse>> Handle(PatientInfoQuery query, CancellationToken cancellationToken)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DBConnection")))
            {
                await connection.OpenAsync();

                var result = await connection.ExecuteAsync<PatientInfoResponse>("Patient_InfoGetById_Sp", new { query.PkPatientId }, commandType: CommandType.StoredProcedure);


                return result.ToList();
            }
        }
    }
}
