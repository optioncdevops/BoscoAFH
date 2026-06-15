using Newtonsoft.Json;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace BoscoAFH.Common
{
    public static class CommonMethods
    {
        // Example AES Key and IV (You should generate and store them securely)
        private static readonly string EncryptionKey = "1a2b3c4d5e6f7a8B9c0d1E2f3a4b5c6D"; // 32 bytes for AES-256

        private static readonly string IV = "5F4D3c2b1a0X9d8c"; // 16 bytes for AES-128
        private static readonly Random random = new(); // Random number generator
        private static readonly TimeZoneInfo IstZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");

        /// <summary>
        /// /GemBox (Excel Export) Details
        /// </summary>
        //GemBox (Excel Export) Details
        public static class GemBoxSettings
        {
            public const string LicenseKey = "EDWG-UK8O-D78A-OMUQ";
        }

        /// <summary>
        /// Serializes the object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objectValue">The object value.</param>
        /// <returns></returns>
        public static string SerializeObject<T>(T objectValue)
        {
            return JsonConvert.SerializeObject(objectValue);
        }

        /// <summary>
        /// Deserializes the string to intended object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objectValue">The object value.</param>
        /// <returns>Serialized target object</returns>
        public static T? DeserializeObject<T>(string? objectValue)
        {
            if (string.IsNullOrEmpty(objectValue))
            {
                return default; // Returns null for reference types or default value for value types
            }

            return JsonConvert.DeserializeObject<T>(objectValue);
        }

        // Convert to Base64Url (for safe URL transmission)
        public static string ToBase64Url(string base64)
        {
            //The characters ==> { ?, &, #, and % } are not part of the Base64 character set by design.
            return base64.Replace('+', '-').Replace('/', '_').Replace("=", "");
        }

        public static string EncryptValue(string plainText)
        {
            using Aes aesAlg = Aes.Create();
            aesAlg.Key = Encoding.UTF8.GetBytes(EncryptionKey); // Ensure 32 bytes (256-bit key)
            aesAlg.IV = Encoding.UTF8.GetBytes(IV); // Ensure 16 bytes IV

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
            using MemoryStream msEncrypt = new MemoryStream();
            using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
            using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
            {
                swEncrypt.Write(plainText);
            }
            // Convert to Base64 string, then apply Base64Url encoding
            string base64 = Convert.ToBase64String(msEncrypt.ToArray());
            return ToBase64Url(base64); // Apply Base64Url encoding
        }

        // Convert back from Base64Url to Base64
        public static string FromBase64Url(string base64Url)
        {
            base64Url = base64Url.Replace('-', '+').Replace('_', '/');
            return string.Concat(base64Url, "==".AsSpan(0, (4 - base64Url.Length % 4) % 4)); // Padding to correct length
        }

        /// <summary>
        /// accepts an encrypted string and returns the string as plain text
        /// </summary>
        /// <param name="Msg"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string DecryptValue(string base64UrlCipherText)
        {
            if (string.IsNullOrEmpty(base64UrlCipherText))
            {
                return "0";
            }
            try
            {
                // Convert from Base64Url to Base64
                string base64CipherText = FromBase64Url(base64UrlCipherText);

                using Aes aesAlg = Aes.Create();
                aesAlg.Key = Encoding.UTF8.GetBytes(EncryptionKey); // Ensure 32 bytes (256-bit key)
                aesAlg.IV = Encoding.UTF8.GetBytes(IV); // Ensure 16 bytes IV

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                using MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(base64CipherText)); // Convert Base64 back to byte array
                using CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
                using StreamReader srDecrypt = new StreamReader(csDecrypt);
                return srDecrypt.ReadToEnd(); // Return decrypted plaintext
            }
            catch (Exception)
            {
                return "0";
            }
        }

        public static string ReadFile(string path)
        {
            var fileData = string.Empty;

            if (File.Exists(path))
            {
                fileData = File.ReadAllText(path);
            }

            return fileData;
        }

        public static string GenerateLicenseKey(int totalLength = 25, int segmentLength = 5)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string newKey;
            var numberOfSegments = (totalLength + 1) / (segmentLength + 1); // Calculate the number of segments

            newKey = string.Join("-", Enumerable.Range(0, numberOfSegments)
                .Select(_ => new string([.. Enumerable.Repeat(chars, segmentLength).Select(s => s[random.Next(s.Length)])])));

            return newKey;
        }

        /// <summary>
        /// To generate unique File name
        /// </summary>
        /// <param name="fileExtension">File extention</param>
        /// <returns>generated file name</returns>
        public static string GenerateUniqueFileName(string fileExtension)
        {
            var uniqueFileName = Guid.NewGuid().ToString();
            var timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            return $"{uniqueFileName}_{timestamp}{fileExtension}";
        }

        /// <summary>
        /// To Generate random OTP
        /// </summary>
        /// <param name="digits">Number of digits to generate</param>
        /// <returns>OTP as string</returns>
        /// <exception cref="ArgumentException">the number of digits must be greater than 0</exception>
        public static string GenerateOTP(int digits)
        {
            if (digits <= 0)
            {
                throw new ArgumentException("The number of digits must be greater than 0.", nameof(digits));
            }
            var rng = RandomNumberGenerator.Create();
            var randomNumber = new byte[4];
            rng.GetBytes(randomNumber);
            var otp = Math.Abs(BitConverter.ToInt32(randomNumber, 0));
            var maxValue = (int)Math.Pow(10, digits);
            otp %= maxValue;
            return otp.ToString($"D{digits}");
        }

        public static string GetRandomNumber()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz".ToCharArray();
            var password = new StringBuilder();  // Use StringBuilder for better performance
            var random = new Random();

            while (password.Length < 8)
            {
                var x = random.Next(0, chars.Length); // Change 1 to 0 to include first character
                var character = chars[x].ToString();

                if (!password.ToString().Contains(character))  // Convert StringBuilder to string
                {
                    password.Append(character);  // Append character instead of string concatenation
                }
            }

            return password.ToString();
        }

        /// <summary>
        /// Get Active Status for nullable boolean.
        /// </summary>
        /// <param name="status">The status as a nullable boolean.</param>
        /// <returns>Returns "Active" if true, "Inactive" if false, and "Unknown" if null.</returns>
        public static string GetActiveStatus(bool? status)
        {
            if (status.HasValue)
            {
                return status.Value ? "Active" : "Inactive";
            }
            else
            {
                return "Unknown";
            }
        }

        /// <summary>
        /// Converts DateTime DOB to string format
        /// </summary>
        /// <param name="birthDate"></param>
        /// <returns>returns age</returns>
        public static string ConvertDateTimeToAge(DateTime? birthDate)
        {
            if (birthDate == null)
            {
                return string.Empty;
            }

            DateTime currentDate = DateTime.Today;

            int years = currentDate.Year - birthDate.Value.Year;
            int months = currentDate.Month - birthDate.Value.Month;
            int days = currentDate.Day - birthDate.Value.Day;

            // Adjust for negative months or days
            if (months < 0 || (months == 0 && days < 0))
            {
                years--;
                months = (months + 12) % 12;
                days += DateTime.DaysInMonth(currentDate.Year, currentDate.Month);
            }

            return $"{years}Y {Math.Abs(months)}M {Math.Abs(days)}D";
        }

        /// <summary>
        /// Converts DateTime DOB to string format
        /// </summary>
        /// <param name="birthDate">date of birth</param>
        /// <param name="isSingleLetter">Letter</param>
        /// <returns>returns age</returns>
        public static string ConvertDateTimeToAge(DateTime? birthDate, bool isSingleLetter = false)
        {
            if (birthDate == null)
            {
                return string.Empty;
            }

            DateTime currentDate = DateTime.Today;

            int years = currentDate.Year - birthDate.Value.Year;
            int months = currentDate.Month - birthDate.Value.Month;
            int days = currentDate.Day - birthDate.Value.Day;

            // Adjust for negative months or days
            if (months < 0 || (months == 0 && days < 0))
            {
                years--;
                months = (months + 12) % 12;
                days += DateTime.DaysInMonth(currentDate.Year, currentDate.Month);
            }

            return isSingleLetter ? $"{years}Y {Math.Abs(months)}M {Math.Abs(days)}D" : $"{years} Years {Math.Abs(months)} Months {Math.Abs(days)} Days";
        }

        public static int CalculateAge(DateTime birthDate)
        {
            DateTime currentDate = DateTime.Now;
            int age = currentDate.Year - birthDate.Year;

            // If the birthdate hasn't occurred yet this year, subtract 1 from the age
            if (currentDate.Month < birthDate.Month || (currentDate.Month == birthDate.Month && currentDate.Day < birthDate.Day))
            {
                age--;
            }

            return age;
        }
         
        public static string GetNextLetterCombination(string current)
        {
            var firstChar = current[0];
            var secondChar = current[1];

            if (secondChar == 'Z')
            {
                if (firstChar == 'Z')
                {
                    return "AA"; // Reset to "AA" after "ZZ"
                }
                else
                {
                    return $"{(char)(firstChar + 1)}A"; // Increment first character and reset second to "A"
                }
            }
            else
            {
                return $"{firstChar}{(char)(secondChar + 1)}"; // Increment second character
            }
        }

        public static string ConvertDateTimeToAgeYear(DateTime? birthDate)
        {
            if (birthDate == null)
            {
                return string.Empty;
            }

            DateTime currentDate = DateTime.Today;

            int years = currentDate.Year - birthDate.Value.Year;
            int months = currentDate.Month - birthDate.Value.Month;
            int days = currentDate.Day - birthDate.Value.Day;

            // Adjust for negative months or days
            if (months < 0 || (months == 0 && days < 0))
            {
                years--;
                months = (months + 12) % 12;
                days += DateTime.DaysInMonth(currentDate.Year, currentDate.Month);
            }

            return $"{years}";
        }

        /// <summary>
        /// Combines a given date and time (assumed to be IST) and converts it to UTC.
        /// </summary>
        /// <param name="date">The date part (IST)</param>
        /// <param name="time">The time part (IST). Defaults to 00:00:00 if null for FromDate, 23:59:59 if null for ToDate.</param>
        /// <param name="isEndOfDay">If true, defaults time to 23:59:59; else 00:00:00.</param>
        public static DateTime? CombineToUtc(DateTime? date, TimeOnly? time = null, bool isEndOfDay = false)
        {
            if (date == null)
            {
                return null;
            }
            DateTime? dateOnly = date?.Date; // set time as 00:00:00
            var defaultTime = time ?? (isEndOfDay ? new TimeOnly(23, 59, 59) : new TimeOnly(0, 0, 0));
            var istDateTime = dateOnly != null ? dateOnly.Value.Add(defaultTime.ToTimeSpan()) : DateTime.UtcNow.Date;

            // Convert IST → UTC
            return TimeZoneInfo.ConvertTimeToUtc(DateTime.SpecifyKind(istDateTime, DateTimeKind.Unspecified), IstZone);
        }

        public static string CheckMobileNumberFormat(string mobile)
        {
            string tmpMobile = mobile.Trim();

            if (tmpMobile.Length > 13 || tmpMobile.Length < 10)
            {
                throw new ArgumentException("The mobile number format is wrong either should be +918888888888 (or) 8888888888 (or) 918888888888.");
            }

            if (tmpMobile.Length == 10)
            {
                return "+91" + tmpMobile;
            }

            return tmpMobile;
        }

        public static string AgeOfPatientInHospitalFormat(int? ageYears, int? ageMonths, int? ageDays)
        {
            return $"{ageYears ?? 0}Y {ageMonths ?? 0}M {ageDays ?? 0}D";
        }

        public static string AgeOfPatientInHospitalFormat(DateTime? birthDateUtc)
        {
            if (birthDateUtc == null)
            {
                return string.Empty;
            }

            var birth = birthDateUtc.Value;
            var now = DateTime.UtcNow;

            if (birth > now)
            {
                return string.Empty;
            }

            int years = now.Year - birth.Year;
            int months = now.Month - birth.Month;
            int days = now.Day - birth.Day;

            // Fix negative days by borrowing from previous month
            if (days < 0)
            {
                var prevMonth = now.AddMonths(-1);
                days += DateTime.DaysInMonth(prevMonth.Year, prevMonth.Month);
                months--;
            }

            // Fix negative months by borrowing a year
            if (months < 0)
            {
                months += 12;
                years--;
            }

            return AgeOfPatientInHospitalFormat(years, months, days);
        }

        /// <summary>
        /// Generates a random password
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GetRandomPassword(int length = 12)
        {
            const string upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string lower = "abcdefghijklmnopqrstuvwxyz";
            const string digits = "0123456789";
            const string special = "!@#$%^&*()-_=+<>?";

            string allChars = upper + lower + digits + special;

            var random = new Random();
            var password = new StringBuilder();

            // Ensure at least one from each category
            password.Append(upper[random.Next(upper.Length)]);
            password.Append(lower[random.Next(lower.Length)]);
            password.Append(digits[random.Next(digits.Length)]);
            password.Append(special[random.Next(special.Length)]);

            // Fill remaining length
            for (int i = password.Length; i < length; i++)
            {
                password.Append(allChars[random.Next(allChars.Length)]);
            }

            // Shuffle password
            return new string([.. password.ToString().OrderBy(x => random.Next())]);
        }

        public static string GetPatientGender(int? gender)
        {
            gender ??= 0;

            return gender switch
            {
                1 => "Male",
                2 => "Female",
                _ => "UnKnown"
            };
        }

        public static class ObjectStringTrimmer
        {
            // cache of compiled actions per Type
            private static readonly ConcurrentDictionary<Type, Action<object>> _cache = new();

            public static void TrimAllStrings(object obj)
            {
                if (obj == null)
                {
                    return;
                }

                var type = obj.GetType();
                var action = _cache.GetOrAdd(type, CreateTrimAction);
                action(obj);
            }

            private static Action<object> CreateTrimAction(Type type)
            {
                var param = Expression.Parameter(typeof(object), "obj");
                var casted = Expression.Variable(type, "typedObj");
                var assigns = new List<Expression>
        {
            Expression.Assign(casted, Expression.Convert(param, type))
        };

                // Generate expressions for each writable string property
                foreach (var prop in type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                             .Where(p => p.CanRead && p.CanWrite && p.PropertyType == typeof(string)))
                {
                    var propExp = Expression.Property(casted, prop);
                    var assignTrim = Expression.Assign(
                        propExp,
                        Expression.Condition(
                            Expression.Equal(propExp, Expression.Constant(null, typeof(string))),
                            Expression.Constant(null, typeof(string)),
                            Expression.Call(propExp, nameof(string.Trim), Type.EmptyTypes)
                        )
                    );

                    assigns.Add(assignTrim);
                }

                var body = Expression.Block([casted], assigns);
                return Expression.Lambda<Action<object>>(body, param).Compile();
            }
        }
    }

    public static partial class TemplateKeyExtractor
    {
        [GeneratedRegex("\"([^\"]*)\"")]
        private static partial Regex QuotedValueRegex();

        public static List<string> ExtractTemplateKeys(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return new();
            }

            return
            [
                .. QuotedValueRegex()
                .Matches(input)
                .Select(m => m.Groups[1].Value)
                .Where(v => !string.IsNullOrWhiteSpace(v))
            ];
        }
    }
}
