using NUnit.Framework;
using ResumMake.Business;
using ResumMake.Business.Objects;
using System;
using System.ComponentModel.DataAnnotations;

namespace ResuMake.Tests
{
    public class DataWorkerTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GenerateRandomPhoneNumberFromTemplateReturnsValidResults()
        {
            //Arrange
            var forceNumberTemplate = "${7-7}";
            var numberInRangeTemplate = "${0-5}";
            var specialCharsTemplate = "+(${6-6}${1-1}) ${0-0}${4-4}${0-9}${0-9} ${0-9}${0-9}${0-9} ${0-9}${0-9}${0-9}";

            //Act
            var forceNumberResult = DataWorker.GenerateRandomPhoneNumberFromTemplate(forceNumberTemplate);
            var numberInRangeResult = DataWorker.GenerateRandomPhoneNumberFromTemplate(numberInRangeTemplate);
            var specialCharsResult = DataWorker.GenerateRandomPhoneNumberFromTemplate(specialCharsTemplate);

            //Assert
            Assert.AreEqual(forceNumberResult, "7");
            Assert.True(int.TryParse(numberInRangeResult, out var numberInRange) && numberInRange >= 0 && numberInRange <= 5);
            Assert.True(specialCharsResult.StartsWith("+(61) 04"));
            Assert.True(specialCharsResult.Length == 18);
        }

        [Test]
        public void SelectRandomFirstNameReturnsResult()
        {
            Assert.True(!string.IsNullOrWhiteSpace(DataWorker.SelectRandomFirstName()));
        }

        [Test]
        public void SelectRandomLastNameReturnsResult()
        {
            Assert.True(!string.IsNullOrWhiteSpace(DataWorker.SelectRandomLastName()));
        }

        [Test]
        public void SelectRandomGenerateRandomEmailReturnsValidResult()
        {
            //Arrange
            var settings = new Settings();
            var person = new Person(settings);

            //Act
            var email = DataWorker.GenerateRandomEmail(person);

            //Assert
            Assert.True(!string.IsNullOrWhiteSpace(email));
            Assert.True(new EmailAddressAttribute().IsValid(email));
        }

        [Test]
        public void SelectRandomDateOfBirthReturnsValidResults()
        {
            //Arrange

            //Act
            var dob18 = DataWorker.SelectRandomDateOfBirth(new Settings(18, 18));
            var dob18Or19 = DataWorker.SelectRandomDateOfBirth(new Settings(18, 19));

            //Assert
            var today = DateTime.Today;
            Assert.True(dob18.Year == today.Year - 18);
            Assert.Contains(dob18Or19.Year, new int[] { today.Year - 18, today.Year - 19 });
        }
    }
}