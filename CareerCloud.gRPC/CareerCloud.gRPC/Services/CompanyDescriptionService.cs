using Grpc.Core;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using CareerCloud.gRPC.Protos;
using Google.Protobuf.WellKnownTypes;
namespace CareerCloud.gRPC.Services
{
    public class CompanyDescriptionService:CompanyDescription.CompanyDescriptionBase
    {
        private readonly CompanyDescriptionLogic _CompanyDescriptionLogic;
        private readonly CommonHelper commonHelper = new CommonHelper();


        public CompanyDescriptionService(CareerCloudContext context)
        {
            var repo = new EFGenericRepository<CompanyDescriptionPoco>(context);
            _CompanyDescriptionLogic = new CompanyDescriptionLogic(repo);

        }

        public override Task<CompanyDescriptionEntity> GetCompanyDescription(IdRequestCompanyDescription request, ServerCallContext context)
        {
            var CompanyDescription = _CompanyDescriptionLogic.Get(Guid.Parse(request.Id));

            var response = fromPoco(CompanyDescription);
            return Task.FromResult(response);
        }

        public override Task<CompanyDescriptionStatusMessage> DeleteCompanyDescription(IdRequestCompanyDescription request, ServerCallContext context)
        {
            var CompanyDescription = _CompanyDescriptionLogic.Get(Guid.Parse(request.Id));

            if (CompanyDescription != null)
            {
                _CompanyDescriptionLogic.Delete(new CompanyDescriptionPoco[] { CompanyDescription });
                return Task.FromResult(new CompanyDescriptionStatusMessage
                {
                    CommonResponse = commonHelper.BuildCommonResponse(true, "Deleted successfully")
                });
            }
            return Task.FromResult(new CompanyDescriptionStatusMessage
            {
                CommonResponse = commonHelper.BuildCommonResponse(false, "Record not found")
            });


        }

        public override Task<CompanyDescriptionStatusMessage> CreateCompanyDescription(CompanyDescriptionEntity request, ServerCallContext context)
        {
            var requestPoco = toPoco(request);
            try
            {
                _CompanyDescriptionLogic.Add(new CompanyDescriptionPoco[] { requestPoco });
                return Task.FromResult(new CompanyDescriptionStatusMessage
                {
                    CommonResponse = commonHelper.BuildCommonResponse(true, "Created successfully")
                });
            }
            catch (AggregateException ex)
            {
                return Task.FromResult(new CompanyDescriptionStatusMessage
                {
                    CommonResponse = commonHelper.BuildCommonResponse(false, "Created Fail")
                });
            }
        }


        public override Task<CompanyDescriptionStatusMessage> UpdateCompanyDescription(CompanyDescriptionEntity request, ServerCallContext context)
        {

            var requestPoco = toPoco(request);
            var currequestPoco = _CompanyDescriptionLogic.Get(Guid.Parse(request.Id));


            commonHelper.CopyProperties(requestPoco, currequestPoco);

            try
            {
                _CompanyDescriptionLogic.Update(new CompanyDescriptionPoco[] { currequestPoco });
                return Task.FromResult(new CompanyDescriptionStatusMessage
                {
                    CommonResponse = commonHelper.BuildCommonResponse(true, "Updated successfully")
                });
            }
            catch (AggregateException ex)
            {
                return Task.FromResult(new CompanyDescriptionStatusMessage
                {
                    CommonResponse = commonHelper.BuildCommonResponse(false, "Updated Fail")
                });
            }

        }


        private CompanyDescriptionEntity fromPoco(CompanyDescriptionPoco poco)
        {
            return new CompanyDescriptionEntity()
            {
                Id = poco.Id.ToString(),
                CompanyName = poco.CompanyName,
                CompanyDescription = poco.CompanyDescription,
                Company = poco.Company.ToString(),
                LanguageID = poco.LanguageId,
               
            };
        }

        private CompanyDescriptionPoco toPoco(CompanyDescriptionEntity reply)
        {
            return new CompanyDescriptionPoco()
            {
                Id = Guid.Parse(reply.Id),

                CompanyName = reply.CompanyName,
                CompanyDescription = reply.CompanyDescription,
                Company = Guid.Parse(reply.Company),
                LanguageId = reply.LanguageID,
            };
        }
    }
}