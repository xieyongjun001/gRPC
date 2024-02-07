using System;
using Grpc.Core;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using CareerCloud.gRPC.Protos;
using Google.Protobuf.WellKnownTypes;

namespace CareerCloud.gRPC.Services
{
	public class ApplicantEducationService:ApplicantEducation.ApplicantEducationBase
	{
		private readonly ApplicantEducationLogic _applicantEducationLogic;
        private readonly CommonHelper commonHelper = new CommonHelper();


        public ApplicantEducationService(CareerCloudContext context)
		{
			var repo = new EFGenericRepository<ApplicantEducationPoco>(context);
			_applicantEducationLogic = new ApplicantEducationLogic(repo);

		}

		public override Task<ApplicantEducationEntity> GetApplicantEducation(IdRequestApplicantEducation request, ServerCallContext context)
		{
			var applicantEducation = _applicantEducationLogic.Get(Guid.Parse(request.Id));

			var response = fromPoco(applicantEducation);
			return Task.FromResult(response);
		}

		public override Task<ApplicantEducationStatusMessage> DeleteApplicantEducation(IdRequestApplicantEducation request, ServerCallContext context)
		{
			var applicantEducation = _applicantEducationLogic.Get(Guid.Parse(request.Id));

			if (applicantEducation != null)
			{
				_applicantEducationLogic.Delete(new ApplicantEducationPoco[] { applicantEducation });
                return Task.FromResult(new ApplicantEducationStatusMessage
                {
                    CommonResponse = commonHelper.BuildCommonResponse(true, "Deleted successfully")
                });
            }
           return Task.FromResult(new ApplicantEducationStatusMessage
           {
                CommonResponse = commonHelper.BuildCommonResponse(false, "Record not found")
            });


        }

        public override Task<ApplicantEducationStatusMessage> CreateApplicantEducation(ApplicantEducationEntity request, ServerCallContext context)
        {
            var requestPoco = toPoco(request);
            try
            {
                _applicantEducationLogic.Add(new ApplicantEducationPoco[] { requestPoco });
                return Task.FromResult(new ApplicantEducationStatusMessage
                {
                    CommonResponse = commonHelper.BuildCommonResponse(true, "Created successfully")
                });
            }
            catch (AggregateException ex)
            {
                return Task.FromResult(new ApplicantEducationStatusMessage
                {
                    CommonResponse = commonHelper.BuildCommonResponse(false, "Created Fail")
                });
            }          
        }


        public override Task<ApplicantEducationStatusMessage> UpdateApplicantEducation(ApplicantEducationEntity request, ServerCallContext context)
        {
           
            var requestPoco = toPoco(request);
            var currequestPoco = _applicantEducationLogic.Get(Guid.Parse(request.Id));


            commonHelper.CopyProperties(requestPoco, currequestPoco);
            try
            {
                _applicantEducationLogic.Update(new ApplicantEducationPoco[] { currequestPoco });
                return Task.FromResult(new ApplicantEducationStatusMessage
                {
                    CommonResponse = commonHelper.BuildCommonResponse(true, "Updated successfully")
                });
            }
            catch (AggregateException ex)
            {
                return Task.FromResult(new ApplicantEducationStatusMessage
                {
                    CommonResponse = commonHelper.BuildCommonResponse(false, "Updated Fail")
                });
            }

        }


        private ApplicantEducationEntity fromPoco(ApplicantEducationPoco poco)
		{
			return new ApplicantEducationEntity()
			{
				Id = poco.Id.ToString(),
				Applicant = poco.Applicant.ToString(),
				Major = poco.Major,
				CertificationDiploma = poco.CertificateDiploma,
				StartDate = poco.StartDate == null ? null :
					Timestamp.FromDateTime(DateTime.SpecifyKind((DateTime)poco.StartDate,DateTimeKind.Utc)),
				CompletionDate = poco.CompletionDate == null ? null :
                    Timestamp.FromDateTime(DateTime.SpecifyKind((DateTime)poco.CompletionDate, DateTimeKind.Utc)),
                CompletionPercent = (byte)poco.CompletionPercent!
			};
		}

        private ApplicantEducationPoco toPoco(ApplicantEducationEntity reply)
        {
            return new ApplicantEducationPoco()
            {
                Id = Guid.Parse(reply.Id),
                Applicant = Guid.Parse(reply.Applicant),
                Major = reply.Major,
                CertificateDiploma = reply.CertificationDiploma,
                StartDate = reply.StartDate.ToDateTime(),
                CompletionDate = reply.CompletionDate.ToDateTime(),
                CompletionPercent = (byte)reply.CompletionPercent
            };
        }
    }
}
/*
decimal float
bool bool
short int32*/