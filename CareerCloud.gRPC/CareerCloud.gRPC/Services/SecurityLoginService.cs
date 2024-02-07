using Grpc.Core;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using CareerCloud.gRPC.Protos;
using Google.Protobuf.WellKnownTypes;

namespace CareerCloud.gRPC.Services
{
    public class SecurityLoginService:SecurityLogin.SecurityLoginBase
    {
        private readonly SecurityLoginLogic _SecurityLoginLogic;
        private readonly CommonHelper commonHelper = new CommonHelper();


        public SecurityLoginService(CareerCloudContext context)
        {
            var repo = new EFGenericRepository<SecurityLoginPoco>(context);
            _SecurityLoginLogic = new SecurityLoginLogic(repo);

        }

        public override Task<SecurityLoginEntity> GetSecurityLogin(IdRequestSecurityLogin request, ServerCallContext context)
        {
            var SecurityLogin = _SecurityLoginLogic.Get(Guid.Parse(request.Id));

            var response = fromPoco(SecurityLogin);
            return Task.FromResult(response);
        }

        public override Task<SecurityLoginStatusMessage> DeleteSecurityLogin(IdRequestSecurityLogin request, ServerCallContext context)
        {
            var SecurityLogin = _SecurityLoginLogic.Get(Guid.Parse(request.Id));

            if (SecurityLogin != null)
            {
                _SecurityLoginLogic.Delete(new SecurityLoginPoco[] { SecurityLogin });
                return Task.FromResult(new SecurityLoginStatusMessage
                {
                    CommonResponse = commonHelper.BuildCommonResponse(true, "Deleted successfully")
                });
            }
            return Task.FromResult(new SecurityLoginStatusMessage
            {
                CommonResponse = commonHelper.BuildCommonResponse(false, "Record not found")
            });


        }

        public override Task<SecurityLoginStatusMessage> CreateSecurityLogin(SecurityLoginEntity request, ServerCallContext context)
        {
            var requestPoco = toPoco(request);
            try
            {
                _SecurityLoginLogic.Add(new SecurityLoginPoco[] { requestPoco });
                return Task.FromResult(new SecurityLoginStatusMessage
                {
                    CommonResponse = commonHelper.BuildCommonResponse(true, "Created successfully")
                });
            }
            catch (AggregateException ex)
            {
                return Task.FromResult(new SecurityLoginStatusMessage
                {
                    CommonResponse = commonHelper.BuildCommonResponse(false, "Created Fail")
                });
            }
        }


        public override Task<SecurityLoginStatusMessage> UpdateSecurityLogin(SecurityLoginEntity request, ServerCallContext context)
        {

            var requestPoco = toPoco(request);
            var currequestPoco = _SecurityLoginLogic.Get(Guid.Parse(request.Id));


            commonHelper.CopyProperties(requestPoco, currequestPoco);

            try
            {
                _SecurityLoginLogic.Update(new SecurityLoginPoco[] { currequestPoco });
                return Task.FromResult(new SecurityLoginStatusMessage
                {
                    CommonResponse = commonHelper.BuildCommonResponse(true, "Updated successfully")
                });
            }
            catch (AggregateException ex)
            {
                return Task.FromResult(new SecurityLoginStatusMessage
                {
                    CommonResponse = commonHelper.BuildCommonResponse(false, "Updated Fail")
                });
            }

        }


        private SecurityLoginEntity fromPoco(SecurityLoginPoco poco)
        {
            return new SecurityLoginEntity()
            {
                Id = poco.Id.ToString(),
                Login = poco.Login.ToString(),
                Password = poco.Password,
                Created = poco.Created == null ? null :
                    Timestamp.FromDateTime(DateTime.SpecifyKind((DateTime)poco.Created, DateTimeKind.Utc)),
                PasswordUpdate = poco.PasswordUpdate == null ? null :
                    Timestamp.FromDateTime(DateTime.SpecifyKind((DateTime)poco.PasswordUpdate, DateTimeKind.Utc)),
                AgreementAccepted = poco.AgreementAccepted == null ? null :
                    Timestamp.FromDateTime(DateTime.SpecifyKind((DateTime)poco.AgreementAccepted, DateTimeKind.Utc)),
                IsLocked = poco.IsLocked,
                IsInactive = poco.IsInactive,
                EmailAddress = poco.EmailAddress,
                PhoneNumber = poco.PhoneNumber,
                FullName = poco.FullName,
                ForceChangePassword = poco.ForceChangePassword,
                PrefferredLanguage = poco.PrefferredLanguage,

            };
        }

        private SecurityLoginPoco toPoco(SecurityLoginEntity reply)
        {
            return new SecurityLoginPoco()
            {
                Id = Guid.Parse(reply.Id),
                Login =reply.Login,
                Password = reply.Password,
                Created = reply.Created.ToDateTime(),
                PasswordUpdate = reply.PasswordUpdate.ToDateTime(),
                AgreementAccepted = reply.AgreementAccepted.ToDateTime(),
                IsLocked = reply.IsLocked,
                IsInactive = reply.IsInactive,
                EmailAddress = reply.EmailAddress,
                PhoneNumber = reply.PhoneNumber,
                FullName = reply.FullName,
                ForceChangePassword = reply.ForceChangePassword,
                PrefferredLanguage = reply.PrefferredLanguage,
            };
        }
    }
}