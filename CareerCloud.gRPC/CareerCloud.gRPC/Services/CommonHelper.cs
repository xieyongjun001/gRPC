using CareerCloud.gRPC.Protos;
using System.Reflection;
using System;

namespace CareerCloud.gRPC.Services
{
    public class CommonHelper
    {
        /*public StatusMessage BuildCommonResponse(bool Success, string Message)
        {
            var commonResponse = new CommonResponse
            {
                Success = Success,
                Message = Message
            };

            var statusMessage = new StatusMessage
            {
          
                CommonResponse = commonResponse
            };

            return statusMessage;
        }*/


        public CommonResponse BuildCommonResponse(bool Success, string Message)
        {
            var commonResponse = new CommonResponse
            {
                Success = Success,
                Message = Message
            };


            return commonResponse;
        }



        public  void CopyProperties(object source, object destination)
        {
            if (source == null || destination == null)
            {
                throw new ArgumentNullException("source or destination can't be null");
            }

            Type sourceType = source.GetType();
            Type destinationType = destination.GetType();

            PropertyInfo[] sourceProperties = sourceType.GetProperties();
            PropertyInfo[] destinationProperties = destinationType.GetProperties();

            foreach (var sourceProperty in sourceProperties)
            {
                // 确保目标类型也有相同的属性
                var destinationProperty = Array.Find(destinationProperties, p => p.Name == sourceProperty.Name && p.PropertyType == sourceProperty.PropertyType);

                if (destinationProperty != null && destinationProperty.CanWrite)
                {
                    object value = sourceProperty.GetValue(source);
                    destinationProperty.SetValue(destination, value);
                }
            }
        }

    }
}
