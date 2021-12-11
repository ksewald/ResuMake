using ResumMake;
using ResumMake.Business.Objects;
using System;

namespace ResuMakeConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //Instatiate settings object and populate from user defined settings.
            var settings = new Settings();

            //Can overwrite field values
            //var fieldVals = new Dictionary<string, string>()
            //{
            //    { DynamicFieldKeys.FirstName, "John" },
            //    { DynamicFieldKeys.LastName, "Smith" }
            //};
            //var person = new Person(settings, fieldVals);

            //Create a person
            var person = new Person(settings);

            var builder = new ResumeBuilder();
            builder.MakeFromTemplate(person);
        }
    }
}
