using Grpc.Core;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using CareerCloud.gRPC.Protos;
using Google.Protobuf.WellKnownTypes;

namespace CareerCloud.gRPC.Services
{
    public class SystemLanguageCodeService:SystemLanguageCode.SystemLanguageCodeBase
    {
        private readonly SystemLanguageCodeLogic _SystemLanguageCodeLogic;
        private readonly CommonHelper commonHelper = new CommonHelper();


        public SystemLanguageCodeService(CareerCloudContext context)
        {
            var repo = new EFGenericRepository<SystemLanguageCodePoco>(context);
            _SystemLanguageCodeLogic = new SystemLanguageCodeLogic(repo);

        }

        public override Task<SystemLanguageCodeEntity> GetSystemLanguageCode(IdRequestSystemLanguageCode request, ServerCallContext context)
        {
            var SystemLanguageCode = _SystemLanguageCodeLogic.Get(request.LanguageID);

            var response = fromPoco(SystemLanguageCode);
            return Task.FromResult(response);
        }

        public override Task<SystemLanguageCodeStatusMessage> DeleteSystemLanguageCode(IdRequestSystemLanguageCode request, ServerCallContext context)
        {
            var SystemLanguageCode = _SystemLanguageCodeLogic.Get(request.LanguageID);

            if (SystemLanguageCode != null)
            {
                _SystemLanguageCodeLogic.Delete(new SystemLanguageCodePoco[] { SystemLanguageCode });
                return Task.FromResult(new SystemLanguageCodeStatusMessage
                {
                    CommonResponse = commonHelper.BuildCommonResponse(true, "Deleted successfully")
                });
            }
            return Task.FromResult(new SystemLanguageCodeStatusMessage
            {
                CommonResponse = commonHelper.BuildCommonResponse(false, "Record not found")
            });


        }

        public override Task<SystemLanguageCodeStatusMessage> CreateSystemLanguageCode(SystemLanguageCodeEntity request, ServerCallContext context)
        {
            var requestPoco = toPoco(request);
            try
            {
                _SystemLanguageCodeLogic.Add(new SystemLanguageCodePoco[] { requestPoco });
                return Task.FromResult(new SystemLanguageCodeStatusMessage
                {
                    CommonResponse = commonHelper.BuildCommonResponse(true, "Created successfully")
                });
            }
            catch (AggregateException ex)
            {
                return Task.FromResult(new SystemLanguageCodeStatusMessage
                {
                    CommonResponse = commonHelper.BuildCommonResponse(false, "Created Fail")
                });
            }
        }


        public override Task<SystemLanguageCodeStatusMessage> UpdateSystemLanguageCode(SystemLanguageCodeEntity request, ServerCallContext context)
        {

            var requestPoco = toPoco(request);
            var currequestPoco = _SystemLanguageCodeLogic.Get(request.LanguageID);

            commonHelper.CopyProperties(requestPoco, currequestPoco);

            try
            {
                _SystemLanguageCodeLogic.Update(new SystemLanguageCodePoco[] { currequestPoco });
                return Task.FromResult(new SystemLanguageCodeStatusMessage
                {
                    CommonResponse = commonHelper.BuildCommonResponse(true, "Updated successfully")
                });
            }
            catch (AggregateException ex)
            {
                return Task.FromResult(new SystemLanguageCodeStatusMessage
                {
                    CommonResponse = commonHelper.BuildCommonResponse(false, "Updated Fail")
                });
            }

        }


        private SystemLanguageCodeEntity fromPoco(SystemLanguageCodePoco poco)
        {
            return new SystemLanguageCodeEntity()
            {
                LanguageID = poco.LanguageID.ToString(),
                Name = poco.Name,
                NativeName = poco.NativeName,
            };
        }

      

        private SystemLanguageCodePoco toPoco(SystemLanguageCodeEntity reply)
        {
            return new SystemLanguageCodePoco()
            {
                LanguageID = (reply.LanguageID),
                Name = reply.Name,
                NativeName = reply.NativeName,
            };
        }
    }
}