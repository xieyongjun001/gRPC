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
     public class CompanyProfileLogic : BaseLogic<CompanyProfilePoco>
    {
        public CompanyProfileLogic(IDataRepository<CompanyProfilePoco> repository) : base(repository)
        {
        }

        // public bool Authenticate(string userName, string password)
        // {
        //     CompanyProfilePoco poco = base.GetAll().Where(s => s.Login == userName).FirstOrDefault();
        //     if (null == poco)
        //     {
        //         return false;
        //     }
        //     return VerifyHash(password, poco.Password);
        // }

         public override void Add(CompanyProfilePoco[] pocos)
         {
             Verify(pocos);
        //     foreach (CompanyProfilePoco poco in pocos)
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

         public override void Update(CompanyProfilePoco[] pocos)
         {
             Verify(pocos);
             base.Update(pocos);
         }

        protected override void Verify(CompanyProfilePoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();
            string[] requiredExtendedPasswordChars = new string[] { "$", "*", "#", "_", "@" };

            foreach (var poco in pocos)
            {
                
                string validExtensionsPattern = @"(\.ca|\.com|\.biz)$";

                if (string.IsNullOrEmpty(poco.CompanyWebsite))
                {
                    exceptions.Add(new ValidationException(600, "CompanyWebsite for CompanyProfile {poco.Id} is not a valid email address format."));
                }
                else if (!Regex.IsMatch(poco.CompanyWebsite, validExtensionsPattern, RegexOptions.IgnoreCase))
                {
                    exceptions.Add(new ValidationException(600, "Valid websites must end with the following extensions – '.ca','.com', '.biz'."));
                }


                string patternPhone = @"^\d{3}-\d{3}-\d{4}$";

                if (string.IsNullOrEmpty(poco.ContactPhone))
                {
                    exceptions.Add(new ValidationException(601, "ContactPhone for CompanyProfile {poco.Id} is not a valid email address format."));
                }
                else if (!Regex.IsMatch(poco.ContactPhone, patternPhone, RegexOptions.IgnoreCase))
                {
                    exceptions.Add(new ValidationException(601, "Must correspond to a valid phone number (e.g. 416-555-1234)"));
                }

                
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
