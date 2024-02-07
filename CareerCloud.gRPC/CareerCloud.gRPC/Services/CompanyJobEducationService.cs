using Grpc.Core;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using CareerCloud.gRPC.Protos;
using Google.Protobuf.WellKnownTypes;
using System;

namespace CareerCloud.gRPC.Services
{
    public class CompanyJobEducationService:CompanyJobEducation.CompanyJobEducationBase
    {
        private readonly CompanyJobEducationLogic _CompanyJobEducationLogic;
        private readonly CommonHelper commonHelper = new CommonHelper();


        public CompanyJobEducationService(CareerCloudContext context)
        {
            var repo = new EFGenericRepository<CompanyJobEducationPoco>(context);
            _CompanyJobEducationLogic = new CompanyJobEducationLogic(repo);

        }

        public override Task<CompanyJobEducationEntity> GetCompanyJobEducation(IdRequestCompanyJobEducation request, ServerCallContext context)
        {
            var CompanyJobEducation = _CompanyJobEducationLogic.Get(Guid.Parse(request.Id));

            var response = fromPoco(CompanyJobEducation);
            return Task.FromResult(response);
        }

        public override Task<CompanyJobEducationStatusMessage> DeleteCompanyJobEducation(IdRequestCompanyJobEducation request, ServerCallContext context)
        {
            var CompanyJobEducation = _CompanyJobEducationLogic.Get(Guid.Parse(request.Id));

            if (CompanyJobEducation != null)
            {
                _CompanyJobEducationLogic.Delete(new CompanyJobEducationPoco[] { CompanyJobEducation });
                return Task.FromResult(new CompanyJobEducationStatusMessage
                {
                    CommonResponse = commonHelper.BuildCommonResponse(true, "Deleted successfully")
                });
            }
            return Task.FromResult(new CompanyJobEducationStatusMessage
            {
                CommonResponse = commonHelper.BuildCommonResponse(false, "Record not found")
            });


        }

        public override Task<CompanyJobEducationStatusMessage> CreateCompanyJobEducation(CompanyJobEducationEntity request, ServerCallContext context)
        {
            var requestPoco = toPoco(request);
            try
            {
                _CompanyJobEducationLogic.Add(new CompanyJobEducationPoco[] { requestPoco });
                return Task.FromResult(new CompanyJobEducationStatusMessage
                {
                    CommonResponse = commonHelper.BuildCommonResponse(true, "Created successfully")
                });
            }
            catch (AggregateException ex)
            {
                return Task.FromResult(new CompanyJobEducationStatusMessage
                {
                    CommonResponse = commonHelper.BuildCommonResponse(false, "Created Fail")
                });
            }
        }


        public override Task<CompanyJobEducationStatusMessage> UpdateCompanyJobEducation(CompanyJobEducationEntity request, ServerCallContext context)
        {

            var requestPoco = toPoco(request);
            var currequestPoco = _CompanyJobEducationLogic.Get(Guid.Parse(request.Id));


            commonHelper.CopyProperties(requestPoco, currequestPoco);

            try
            {
                _CompanyJobEducationLogic.Update(new CompanyJobEducationPoco[] { currequestPoco });
                return Task.FromResult(new CompanyJobEducationStatusMessage
                {
                    CommonResponse = commonHelper.BuildCommonResponse(true, "Updated successfully")
                });
            }
            catch (AggregateException ex)
            {
                return Task.FromResult(new CompanyJobEducationStatusMessage
                {
                    CommonResponse = commonHelper.BuildCommonResponse(false, "Updated Fail")
                });
            }

        }


        private CompanyJobEducationEntity fromPoco(CompanyJobEducationPoco poco)
        {
            return new CompanyJobEducationEntity()
            {
                Id = poco.Id.ToString(),
                Job = poco.Job.ToString(),
                Major = poco.Major,
                Importance = poco.Importance,
            };
        }


        

        private CompanyJobEducationPoco toPoco(CompanyJobEducationEntity reply)
        {
            return new CompanyJobEducationPoco()
            {
                Id = Guid.Parse(reply.Id),
                Job = Guid.Parse(reply.Job),
                Major = reply.Major,
                Importance = (short)reply.Importance
            };
        }
    }
}