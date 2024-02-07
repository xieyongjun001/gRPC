using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;

namespace CareerCloud.BusinessLogicLayer
{
    public class SystemLanguageCodeLogic
    {
        protected IDataRepository<SystemLanguageCodePoco> _repository;

        public SystemLanguageCodeLogic(IDataRepository<SystemLanguageCodePoco> repository)
        {
            _repository = repository;
        }


         public void Add(SystemLanguageCodePoco[] pocos)
         {
             Verify(pocos);

            _repository.Add(pocos);
        }

        public SystemLanguageCodePoco Get(string LanguageID)
        {
            return _repository.GetSingle(c => c.LanguageID == LanguageID);
        }

        public virtual List<SystemLanguageCodePoco>  GetAll()
        {
            return _repository.GetAll().ToList();
        }

        public  void Update(SystemLanguageCodePoco[] pocos)
         {
             Verify(pocos);
            _repository.Update(pocos);
         }

        protected  void Verify(SystemLanguageCodePoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();
            string[] requiredExtendedPasswordChars = new string[] { "$", "*", "#", "_", "@" };

            foreach (var poco in pocos)
            {
                if (string.IsNullOrEmpty(poco.LanguageID))
                {
                    exceptions.Add(new ValidationException(1000, $"LanguageID for SystemLanguageCode {poco.LanguageID} cannot be null"));
                }
              
                if (string.IsNullOrEmpty(poco.Name))
                {
                    exceptions.Add(new ValidationException(1001, $"Name for SystemLanguageCode {poco.LanguageID} cannot be null"));
                }
                
                if (string.IsNullOrEmpty(poco.NativeName))
                {
                    exceptions.Add(new ValidationException(1002, "NativeName for SystemLanguageCode {poco.Id} cannot be null."));
                }
                

            }

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }

        public void Delete(SystemLanguageCodePoco[] pocos)
        {
            _repository.Remove(pocos);
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
