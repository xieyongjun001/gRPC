using CareerCloud.Pocos;
using System;
using System.Linq;
using System.Text;
using CareerCloud.DataAccessLayer;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CareerCloud.BusinessLogicLayer
{
    public class ApplicantEducationLogic : BaseLogic<ApplicantEducationPoco>
	{


        public ApplicantEducationLogic(IDataRepository<ApplicantEducationPoco> repository) : base(repository)
        {
        }

        // public bool Authenticate(string userName, string password)
        // {
        //     ApplicantEducationPoco poco = base.GetAll().Where(s => s.Login == userName).FirstOrDefault();
        //     if (null == poco)
        //     {
        //         return false;
        //     }
        //     return VerifyHash(password, poco.Password);
        // }

         public override void Add(ApplicantEducationPoco[] pocos)
         {
             Verify(pocos);
        //     foreach (ApplicantEducationPoco poco in pocos)
        //     {
        //         poco.Password = ComputeHash(poco.Password, new byte[saltLengthLimit]);
        //         poco.Created = DateTime.Now.ToUniversalTime();
        //         poco.IsLocked = false;
        //         poco.IsInactive = false;
        //         poco.ForceChangePassword = true;
        //         poco.PasswordUpdate = poco.Created.AddDays(30);
        //     }
        base.Add(pocos);
         }

         public override void Update(ApplicantEducationPoco[] pocos)
         {
             Verify(pocos);
             base.Update(pocos);
         }

        protected override void Verify(ApplicantEducationPoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();
        
            foreach (var poco in pocos)
            {
                if (string.IsNullOrEmpty(poco.Major))
                {
                    exceptions.Add(new ValidationException(107, $"Major for ApplicationEducation {poco.Id} cannot be null"));
                }
                else if (poco.Major.Length < 3)
                {
                    exceptions.Add(new ValidationException(107, $"Major for ApplicationEducation {poco.Id} must be at least 3 characters."));
                }


               

                DateTime currentDate = DateTime.Now;
                if (poco.StartDate > currentDate)
                {
                    exceptions.Add(new ValidationException(108, $"StartDate cannot be greater than today"));
                }

                if (poco.StartDate > poco.CompletionDate)
                {
                    exceptions.Add(new ValidationException(109, $"CompletionDate cannot be earlier than StartDate"));
                }
                /*if (poco.Importance<0)
                {
                    exceptions.Add(new ValidationException(201, $"Importance for ApplicationEducation {poco.Id} cannot be less than 0"));
                }*/

            }

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }

        // private static byte[] GetSalt()
        // {
        //     return GetSalt(saltLengthLimit);
        // }

        // private static byte[] GetSalt(int maximumSaltLength)
        // {
        //     var salt = new byte[maximumSaltLength];
        //     using (var random = new RNGCryptoServiceProvider())
        //     {
        //         random.GetNonZeroBytes(salt);
        //     }
        //     return salt;
        // }

        // private string ComputeHash(string plainText, byte[] saltBytes)
        // {
        //     if (saltBytes == null)
        //     {
        //         saltBytes = GetSalt();
        //     }

        //     byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
        //     byte[] plainTextWithSaltBytes = new byte[plainTextBytes.Length + saltBytes.Length];
        //     for (int i = 0; i < plainTextBytes.Length; i++)
        //         plainTextWithSaltBytes[i] = plainTextBytes[i];

        //     for (int i = 0; i < saltBytes.Length; i++)
        //     {
        //         plainTextWithSaltBytes[plainTextBytes.Length + i] = saltBytes[i];
        //     }

        //     HashAlgorithm hash = new SHA512Managed();
        //     byte[] hashBytes = hash.ComputeHash(plainTextWithSaltBytes);
        //     byte[] hashWithSaltBytes = new byte[hashBytes.Length + saltBytes.Length];
        //     for (int i = 0; i < hashBytes.Length; i++)
        //     {
        //         hashWithSaltBytes[i] = hashBytes[i];
        //     }
        //     for (int i = 0; i < saltBytes.Length; i++)
        //     {
        //         hashWithSaltBytes[hashBytes.Length + i] = saltBytes[i];
        //     }


        //     return Convert.ToBase64String(hashWithSaltBytes);
        // }
        // private bool VerifyHash(string plainText, string hashValue)
        // {
        //     const int hashSizeInBytes = 64;

        //     byte[] hashWithSaltBytes = Convert.FromBase64String(hashValue);
        //     if (hashWithSaltBytes.Length < hashSizeInBytes)
        //         return false;
        //     byte[] saltBytes = new byte[hashWithSaltBytes.Length - hashSizeInBytes];
        //     for (int i = 0; i < saltBytes.Length; i++)
        //         saltBytes[i] = hashWithSaltBytes[hashSizeInBytes + i];
        //     string expectedHashString = ComputeHash(plainText, saltBytes);
        //     return (hashValue == expectedHashString);
        // }

		
	}
}
