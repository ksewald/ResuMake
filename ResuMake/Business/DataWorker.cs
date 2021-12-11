using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using ResumMake.Business.Objects;

namespace ResumMake.Business
{
    public static class DataWorker
    {

        const string FirstNameDataListFileName = "FirstNames.txt";
        const string LastNameDataListFileName = "LastNames.txt";
        const string EmailDomainsDataListFileName = "EmailDomains.txt";

        public static void PopulateDynamicFields(Settings settings, Dictionary<string, string> dynamicFields)
        {

            //foreach(var key in dynamicFields.Keys.ToList())
            //{
            //    //Do not overwrite values that are already set.
            //    if (!string.IsNullOrWhiteSpace(dynamicFields[key])) continue;

            //    if (key.Equals(Business.DynamicFieldKeys.FirstName))
            //        dynamicFields[key] = SelectRandomFirstName();
            //    else if (key.Equals(Business.DynamicFieldKeys.LastName))
            //        dynamicFields[key] = SelectRandomLastName();
            //    else if (key.Equals(Business.DynamicFieldKeys.DOB))
            //        dynamicFields[key] = GenerateRandomDob(settings);
            //}

        }

        public static string SelectRandomFirstName()
        {
            return SelectRandomArrayEntry(FileUtils.ReadFromDataList(FirstNameDataListFileName));
        }

        public static string SelectRandomLastName()
        {
            return SelectRandomArrayEntry(FileUtils.ReadFromDataList(LastNameDataListFileName));
        }

        public static string GenerateRandomEmail(Person person)
        {
            Array values = Enum.GetValues(typeof(Enums.EmailFormats));
            Random random = new Random();
            var emailFormat = (Enums.EmailFormats) values.GetValue(random.Next(values.Length));

            var email = string.Empty;
            switch(emailFormat)
            {
                case Enums.EmailFormats.FirstNameDotLastname:
                    email = $"{person.FirstName}.{person.LastName}";
                    break;
                case Enums.EmailFormats.FirstInitialDotLastName:
                    email = $"{person.FirstName.First()}.{person.LastName}";
                    break;
                case Enums.EmailFormats.FirstNameDotLastNameWithRandomEnd:
                    email = $"{person.FirstName}.{person.LastName}{random.Next(1000)}";
                    break;
                case Enums.EmailFormats.FirstNameDotLastNameWithYearOfBirth:
                    email = $"{person.FirstName}.{person.LastName}{person.DateofBirth.Year}";
                    break;
                default:
                    email = $"{person.FirstName}.{person.LastName}";
                    break;
            }

            var emailDomain = SelectRandomArrayEntry(FileUtils.ReadFromDataList(EmailDomainsDataListFileName));

            return $"{email}@{emailDomain}";
        }

        public static DateTime SelectRandomDateOfBirth(Settings settings)
        {
            var rand = new Random();
            var yearsOld = rand.Next(settings.MinimumAge, settings.MaximumAge);

            var daysOld = yearsOld * 365 + rand.Next(0, 365);
            return DateTime.Today.AddDays(-daysOld);
        }

        public static string GenerateRandomPhoneNumberFromTemplate(string template)
        {
            var phoneNumber = template;
            var rand = new Random();
            while(phoneNumber.IndexOf("${") != -1 && phoneNumber.IndexOf("}") != -1)
            {
                var startIndex = phoneNumber.IndexOf("${");
                var endIndex = phoneNumber.IndexOf("}");
                var sectionToReplace = phoneNumber.Substring(startIndex, endIndex - startIndex + 1);
                var numberRange = sectionToReplace.Replace("${", "").Replace("}", "");

                var nums = numberRange.Split("-");

                var generatedNumber = rand.Next(int.Parse(nums[0]), int.Parse(nums[1]) + 1);
                phoneNumber = phoneNumber.Remove(startIndex, endIndex - startIndex + 1).Insert(startIndex, generatedNumber.ToString());
            }

            return phoneNumber;
        }

        internal static string SelectRandomArrayEntry(string[] array)
        {
            if (array.Length == 0) return null;

            Random r = new Random();
            return array[r.Next(0, array.Length - 1)];
        }

        internal static void NormaliseNameDataLists()
        {
            var destination = @"C:/dev/";
            NormaliseNameDataList(destination, LastNameDataListFileName);
            NormaliseNameDataList(destination, FirstNameDataListFileName);
        }

        private static void NormaliseNameDataList(string destinationPath, string fileName)
        {
            var root = FileUtils.GetRootPath();

            var filePath = $"{root}/DataLists/{fileName}";
            var x = File.ReadAllLines(filePath);

            var namesList = new List<string>(x);
            var currentCulture = System.Threading.Thread.CurrentThread.CurrentCulture;
            namesList = namesList.ConvertAll(r => currentCulture.TextInfo.ToTitleCase(r.ToLower().Trim()));

            File.WriteAllLines(@$"{destinationPath}{fileName}", namesList);
        }
    }
}