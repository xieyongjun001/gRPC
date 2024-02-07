using Grpc.Core;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using CareerCloud.gRPC.Protos;
using Google.Protobuf.WellKnownTypes;
namespace CareerCloud.gRPC.Services
{
    public class ApplicantProfileService:ApplicantProfile.ApplicantProfileBase
    {
        private readonly ApplicantProfileLogic _ApplicantProfileLogic;
        private readonly CommonHelper commonHelper = new CommonHelper();


        public ApplicantProfileService(CareerCloudContext context)
        {
            var repo = new EFGenericRepository<ApplicantProfilePoco>(context);
            _ApplicantProfileLogic = new ApplicantProfileLogic(repo);

        }

        public override Task<ApplicantProfileEntity> GetApplicantProfile(IdRequestApplicantProfile request, ServerCallContext context)
        {
            var ApplicantProfile = _ApplicantProfileLogic.Get(Guid.Parse(request.Id));

            var response = fromPoco(ApplicantProfile);
            return Task.FromResult(response);
        }

        public override Task<ApplicantProfileStatusMessage> DeleteApplicantProfile(IdRequestApplicantProfile request, ServerCallContext context)
        {
            var ApplicantProfile = _ApplicantProfileLogic.Get(Guid.Parse(request.Id));

            if (ApplicantProfile != null)
            {
                _ApplicantProfileLogic.Delete(new ApplicantProfilePoco[] { ApplicantProfile });
                return Task.FromResult(new ApplicantProfileStatusMessage
                {
                    CommonResponse = commonHelper.BuildCommonResponse(true, "Deleted successfully")
                });
            }
            return Task.FromResult(new ApplicantProfileStatusMessage
            {
                CommonResponse = commonHelper.BuildCommonResponse(false, "Record not found")
            });


        }

        public override Task<ApplicantProfileStatusMessage> CreateApplicantProfile(ApplicantProfileEntity request, ServerCallContext context)
        {
            var requestPoco = toPoco(request);
            try
            {
                _ApplicantProfileLogic.Add(new ApplicantProfilePoco[] { requestPoco });
                return Task.FromResult(new ApplicantProfileStatusMessage
                {
                    CommonResponse = commonHelper.BuildCommonResponse(true, "Created successfully")
                });
            }
            catch (AggregateException ex)
            {
                return Task.FromResult(new ApplicantProfileStatusMessage
                {
                    CommonResponse = commonHelper.BuildCommonResponse(false, "Created Fail")
                });
            }
        }


        public override Task<ApplicantProfileStatusMessage> UpdateApplicantProfile(ApplicantProfileEntity request, ServerCallContext context)
        {

            var requestPoco = toPoco(request);

            var currequestPoco = _ApplicantProfileLogic.Get(Guid.Parse(request.Id));


            commonHelper.CopyProperties(requestPoco, currequestPoco);
            try
            {
                _ApplicantProfileLogic.Update(new ApplicantProfilePoco[] { currequestPoco });
                return Task.FromResult(new ApplicantProfileStatusMessage
                {
                    CommonResponse = commonHelper.BuildCommonResponse(true, "Updated successfully")
                });
            }
            catch (AggregateException ex)
            {
                return Task.FromResult(new ApplicantProfileStatusMessage
                {
                    CommonResponse = commonHelper.BuildCommonResponse(false, "Updated Fail")
                });
            }

        }


        private ApplicantProfileEntity fromPoco(ApplicantProfilePoco poco)
        {
            return new ApplicantProfileEntity()
            {
                Id = poco.Id.ToString(),
                CurrentSalary = (float)poco.CurrentSalary,
                CurrentRate = (float)poco.CurrentRate,
                Country = poco.Country,
                Province = poco.Province,
                Street = poco.Street,
                City = poco.City,
                PostalCode = poco.PostalCode,
                Login = poco.Login.ToString(),
            };
        }

        private ApplicantProfilePoco toPoco(ApplicantProfileEntity reply)
        {
            return new ApplicantProfilePoco()
            {
                Id = Guid.Parse(reply.Id),
                CurrentSalary = (decimal?)reply.CurrentSalary,
                CurrentRate = (decimal?)reply.CurrentRate,
                Country = reply.Country,
                Province = reply.Province,
                Street = reply.Street,
                City = reply.City,
                PostalCode = reply.PostalCode,
                Login = Guid.Parse(reply.Login),
            };
        }
    }
}