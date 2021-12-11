using System;
using System.Collections.Generic;
using ResumMake.Utils;

namespace ResumMake.Business.Objects
{
    public class Person : DynamicFieldBase
    {

        #region properties

        [DynamicFieldKey(Constants.DynamicFieldKeys.FirstName)]
        public string FirstName { get; set; }

        [DynamicFieldKey(Constants.DynamicFieldKeys.LastName)]
        public string LastName { get; set; }

        [DynamicFieldKey(Constants.DynamicFieldKeys.DOB)]
        public DateTime DateofBirth { get; set; }

        [DynamicFieldKey(Constants.DynamicFieldKeys.Phone)]
        public string Phone { get; set; }

        [DynamicFieldKey(Constants.DynamicFieldKeys.Email)]
        public string Email { get; set; }

        #endregion

        #region Constructors

        public Person() : this(new Settings(), new Dictionary<string, string>()) { }

        public Person(Settings settings) : this(settings, new Dictionary<string, string>()) { }

        public Person(Settings settings, Dictionary<string, string> dynamicFieldKeyValuePairs)
        {
            FirstName = TryGetDynamicFieldKey(typeof(Person), nameof(FirstName), out var firstNameKey) && dynamicFieldKeyValuePairs.ContainsKey(firstNameKey)
                ? dynamicFieldKeyValuePairs[firstNameKey]
                : DataWorker.SelectRandomFirstName();

            LastName = TryGetDynamicFieldKey(typeof(Person), nameof(LastName), out var lastNameKey) && dynamicFieldKeyValuePairs.ContainsKey(lastNameKey) 
                ? dynamicFieldKeyValuePairs[lastNameKey]
                : DataWorker.SelectRandomLastName();

            DateofBirth = TryGetDynamicFieldKey(typeof(Person), nameof(DateofBirth), out var dobKey) && dynamicFieldKeyValuePairs.TryGetValue(dobKey, out var dobValueStr) && DateTime.TryParse(dobValueStr, out var dobValue)
                ? dobValue
                : DataWorker.SelectRandomDateOfBirth(settings);

            Phone = TryGetDynamicFieldKey(typeof(Person), nameof(Phone), out var phoneKey) && dynamicFieldKeyValuePairs.ContainsKey(phoneKey)
                ? dynamicFieldKeyValuePairs[phoneKey]
                : DataWorker.GenerateRandomPhoneNumberFromTemplate("(${2-9}${0-9}${0-9}) ${2-9}${0-9}${0-9}-${0-9}${0-9}${0-9}");

            Email = TryGetDynamicFieldKey(typeof(Person), nameof(Email), out var emailKey) && dynamicFieldKeyValuePairs.ContainsKey(emailKey)
                ? dynamicFieldKeyValuePairs[emailKey]
                : DataWorker.GenerateRandomEmail(this);
        }

        #endregion
    }
}
