using Grpc.Core;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using CareerCloud.gRPC.Protos;
using Google.Protobuf.WellKnownTypes;
namespace CareerCloud.gRPC.Services
{
    public class ApplicantJobApplicationService:ApplicantJobApplication.ApplicantJobApplicationBase
    {
        private readonly ApplicantJobApplicationLogic _ApplicantJobApplicationLogic;
        private readonly CommonHelper commonHelper = new CommonHelper();


        public ApplicantJobApplicationService(CareerCloudContext context)
        {
            var repo = new EFGenericRepository<ApplicantJobApplicationPoco>(context);
            _ApplicantJobApplicationLogic = new ApplicantJobApplicationLogic(repo);

        }

        public override Task<ApplicantJobApplicationEntity> GetApplicantJobApplication(IdRequestApplicantJobApplication request, ServerCallContext context)
        {
            var ApplicantJobApplication = _ApplicantJobApplicationLogic.Get(Guid.Parse(request.Id));

            var response = fromPoco(ApplicantJobApplication);
            return Task.FromResult(response);
        }

        public override Task<ApplicantJobApplicationStatusMessage> DeleteApplicantJobApplication(IdRequestApplicantJobApplication request, ServerCallContext context)
        {
            var ApplicantJobApplication = _ApplicantJobApplicationLogic.Get(Guid.Parse(request.Id));

            if (ApplicantJobApplication != null)
            {
                _ApplicantJobApplicationLogic.Delete(new ApplicantJobApplicationPoco[] { ApplicantJobApplication });
                return Task.FromResult(new ApplicantJobApplicationStatusMessage
                {
                    CommonResponse = commonHelper.BuildCommonResponse(true, "Deleted successfully")
                });
            }
            return Task.FromResult(new ApplicantJobApplicationStatusMessage
            {
                CommonResponse = commonHelper.BuildCommonResponse(false, "Record not found")
            });


        }

        public override Task<ApplicantJobApplicationStatusMessage> CreateApplicantJobApplication(ApplicantJobApplicationEntity request, ServerCallContext context)
        {
            var requestPoco = toPoco(request);
            try
            {
                _ApplicantJobApplicationLogic.Add(new ApplicantJobApplicationPoco[] { requestPoco });
                return Task.FromResult(new ApplicantJobApplicationStatusMessage
                {
                    CommonResponse = commonHelper.BuildCommonResponse(true, "Created successfully")
                });
            }
            catch (AggregateException ex)
            {
                return Task.FromResult(new ApplicantJobApplicationStatusMessage
                {
                    CommonResponse = commonHelper.BuildCommonResponse(false, "Created Fail")
                });
            }
        }


        public override Task<ApplicantJobApplicationStatusMessage> UpdateApplicantJobApplication(ApplicantJobApplicationEntity request, ServerCallContext context)
        {

            var requestPoco = toPoco(request);
            var currequestPoco = _ApplicantJobApplicationLogic.Get(Guid.Parse(request.Id));


            commonHelper.CopyProperties(requestPoco, currequestPoco);

            try
            {
                _ApplicantJobApplicationLogic.Update(new ApplicantJobApplicationPoco[] { currequestPoco });
                return Task.FromResult(new ApplicantJobApplicationStatusMessage
                {
                    CommonResponse = commonHelper.BuildCommonResponse(true, "Updated successfully")
                });
            }
            catch (AggregateException ex)
            {
                return Task.FromResult(new ApplicantJobApplicationStatusMessage
                {
                    CommonResponse = commonHelper.BuildCommonResponse(false, "Updated Fail")
                });
            }

        }


        private ApplicantJobApplicationEntity fromPoco(ApplicantJobApplicationPoco poco)
        {
            return new ApplicantJobApplicationEntity()
            {
                Id = poco.Id.ToString(),
                Applicant = poco.Applicant.ToString(),
                Job = poco.Job.ToString(),
                ApplicationDate = poco.ApplicationDate == null ? null :
                    Timestamp.FromDateTime(DateTime.SpecifyKind((DateTime)poco.ApplicationDate, DateTimeKind.Utc)),
               
            };
        }


        private ApplicantJobApplicationPoco toPoco(ApplicantJobApplicationEntity reply)
        {
            return new ApplicantJobApplicationPoco()
            {
                Id = Guid.Parse(reply.Id),
                Applicant = Guid.Parse(reply.Applicant),
                Job = Guid.Parse(reply.Job),
                ApplicationDate = reply.ApplicationDate.ToDateTime()
            };
        }
    }
}