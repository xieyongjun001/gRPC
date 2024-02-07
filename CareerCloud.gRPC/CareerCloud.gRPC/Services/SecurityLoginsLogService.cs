using Grpc.Core;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using CareerCloud.gRPC.Protos;
using Google.Protobuf.WellKnownTypes;

namespace CareerCloud.gRPC.Services
{
    public class SecurityLoginsLogService:SecurityLoginsLog.SecurityLoginsLogBase
    {
        private readonly SecurityLoginsLogLogic _SecurityLoginsLogLogic;
        private readonly CommonHelper commonHelper = new CommonHelper();


        public SecurityLoginsLogService(CareerCloudContext context)
        {
            var repo = new EFGenericRepository<SecurityLoginsLogPoco>(context);
            _SecurityLoginsLogLogic = new SecurityLoginsLogLogic(repo);

        }

        public override Task<SecurityLoginsLogEntity> GetSecurityLoginsLog(IdRequestSecurityLoginsLog request, ServerCallContext context)
        {
            var SecurityLoginsLog = _SecurityLoginsLogLogic.Get(Guid.Parse(request.Id));

            var response = fromPoco(SecurityLoginsLog);
            return Task.FromResult(response);
        }

        public override Task<SecurityLoginsLogStatusMessage> DeleteSecurityLoginsLog(IdRequestSecurityLoginsLog request, ServerCallContext context)
        {
            var SecurityLoginsLog = _SecurityLoginsLogLogic.Get(Guid.Parse(request.Id));

            if (SecurityLoginsLog != null)
            {
                _SecurityLoginsLogLogic.Delete(new SecurityLoginsLogPoco[] { SecurityLoginsLog });
                return Task.FromResult(new SecurityLoginsLogStatusMessage
                {
                    CommonResponse = commonHelper.BuildCommonResponse(true, "Deleted successfully")
                });
            }
            return Task.FromResult(new SecurityLoginsLogStatusMessage
            {
                CommonResponse = commonHelper.BuildCommonResponse(false, "Record not found")
            });


        }

        public override Task<SecurityLoginsLogStatusMessage> CreateSecurityLoginsLog(SecurityLoginsLogEntity request, ServerCallContext context)
        {
            var requestPoco = toPoco(request);
            try
            {
                _SecurityLoginsLogLogic.Add(new SecurityLoginsLogPoco[] { requestPoco });
                return Task.FromResult(new SecurityLoginsLogStatusMessage
                {
                    CommonResponse = commonHelper.BuildCommonResponse(true, "Created successfully")
                });
            }
            catch (AggregateException ex)
            {
                return Task.FromResult(new SecurityLoginsLogStatusMessage
                {
                    CommonResponse = commonHelper.BuildCommonResponse(false, "Created Fail")
                });
            }
        }


        public override Task<SecurityLoginsLogStatusMessage> UpdateSecurityLoginsLog(SecurityLoginsLogEntity request, ServerCallContext context)
        {

            var requestPoco = toPoco(request);

            var currequestPoco = _SecurityLoginsLogLogic.Get(Guid.Parse(request.Id));

            commonHelper.CopyProperties(requestPoco, currequestPoco);
            try
            {
                _SecurityLoginsLogLogic.Update(new SecurityLoginsLogPoco[] { currequestPoco });
                return Task.FromResult(new SecurityLoginsLogStatusMessage
                {
                    CommonResponse = commonHelper.BuildCommonResponse(true, "Updated successfully")
                });
            }
            catch (AggregateException ex)
            {
                return Task.FromResult(new SecurityLoginsLogStatusMessage
                {
                    CommonResponse = commonHelper.BuildCommonResponse(false, "Updated Fail")
                });
            }

        }


        private SecurityLoginsLogEntity fromPoco(SecurityLoginsLogPoco poco)
        {
            return new SecurityLoginsLogEntity()
            {
                Id = poco.Id.ToString(),
                Login = poco.Login.ToString(),
                SourceIP = poco.SourceIP,
                IsSuccesful = poco.IsSuccesful,
                LogonDate = poco.LogonDate == null ? null :
                    Timestamp.FromDateTime(DateTime.SpecifyKind((DateTime)poco.LogonDate, DateTimeKind.Utc)),
           
            };
        }


        private SecurityLoginsLogPoco toPoco(SecurityLoginsLogEntity reply)
        {
            return new SecurityLoginsLogPoco()
            {
                Id = Guid.Parse(reply.Id),
                Login = Guid.Parse(reply.Login),
                SourceIP = reply.SourceIP,
                IsSuccesful = reply.IsSuccesful,
                LogonDate = reply.LogonDate.ToDateTime()
            };
        }
    }
}