using Grpc.Core;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using CareerCloud.gRPC.Protos;
using Google.Protobuf.WellKnownTypes;

namespace CareerCloud.gRPC.Services
{
    public class CompanyJobService:CompanyJob.CompanyJobBase
    {
        private readonly CompanyJobLogic _CompanyJobLogic;
        private readonly CommonHelper commonHelper = new CommonHelper();


        public CompanyJobService(CareerCloudContext context)
        {
            var repo = new EFGenericRepository<CompanyJobPoco>(context);
            _CompanyJobLogic = new CompanyJobLogic(repo);

        }

        public override Task<CompanyJobEntity> GetCompanyJob(IdRequestCompanyJob request, ServerCallContext context)
        {
            var CompanyJob = _CompanyJobLogic.Get(Guid.Parse(request.Id));

            var response = fromPoco(CompanyJob);
            return Task.FromResult(response);
        }

        public override Task<CompanyJobStatusMessage> DeleteCompanyJob(IdRequestCompanyJob request, ServerCallContext context)
        {
            var CompanyJob = _CompanyJobLogic.Get(Guid.Parse(request.Id));

            if (CompanyJob != null)
            {
                _CompanyJobLogic.Delete(new CompanyJobPoco[] { CompanyJob });
                return Task.FromResult(new CompanyJobStatusMessage
                {
                    CommonResponse = commonHelper.BuildCommonResponse(true, "Deleted successfully")
                });
            }
            return Task.FromResult(new CompanyJobStatusMessage
            {
                CommonResponse = commonHelper.BuildCommonResponse(false, "Record not found")
            });


        }

        public override Task<CompanyJobStatusMessage> CreateCompanyJob(CompanyJobEntity request, ServerCallContext context)
        {
            var requestPoco = toPoco(request);
            try
            {
                _CompanyJobLogic.Add(new CompanyJobPoco[] { requestPoco });
                return Task.FromResult(new CompanyJobStatusMessage
                {
                    CommonResponse = commonHelper.BuildCommonResponse(true, "Created successfully")
                });
            }
            catch (AggregateException ex)
            {
                return Task.FromResult(new CompanyJobStatusMessage
                {
                    CommonResponse = commonHelper.BuildCommonResponse(false, "Created Fail")
                });
            }
        }


        public override Task<CompanyJobStatusMessage> UpdateCompanyJob(CompanyJobEntity request, ServerCallContext context)
        {

            var requestPoco = toPoco(request);
            var currequestPoco = _CompanyJobLogic.Get(Guid.Parse(request.Id));


            commonHelper.CopyProperties(requestPoco, currequestPoco);

            try
            {
                _CompanyJobLogic.Update(new CompanyJobPoco[] { currequestPoco });
                return Task.FromResult(new CompanyJobStatusMessage
                {
                    CommonResponse = commonHelper.BuildCommonResponse(true, "Updated successfully")
                });
            }
            catch (AggregateException ex)
            {
                return Task.FromResult(new CompanyJobStatusMessage
                {
                    CommonResponse = commonHelper.BuildCommonResponse(false, "Updated Fail")
                });
            }

        }


        private CompanyJobEntity fromPoco(CompanyJobPoco poco)
        {
            return new CompanyJobEntity()
            {
                Id = poco.Id.ToString(),
                Company = poco.Company.ToString(),
                IsInactive = poco.IsInactive,
                IsCompanyHidden = poco.IsCompanyHidden,
                ProfileCreated = poco.ProfileCreated == null ? null :
                    Timestamp.FromDateTime(DateTime.SpecifyKind((DateTime)poco.ProfileCreated, DateTimeKind.Utc)),
               
            };
        }
       
        private CompanyJobPoco toPoco(CompanyJobEntity reply)
        {
            return new CompanyJobPoco()
            {
                Id = Guid.Parse(reply.Id),
                Company = Guid.Parse(reply.Company),
                IsInactive = reply.IsInactive,
                IsCompanyHidden = reply.IsCompanyHidden,
                ProfileCreated = reply.ProfileCreated.ToDateTime()
            };
        }
    }
}