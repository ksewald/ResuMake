using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using NPOI.XWPF.UserModel;
using ResumMake.Business;
using ResumMake.Business.Objects;

namespace ResumMake
{
    public class ResumeBuilder
    {
        public void MakeFromTemplate(Person person)
        {
            var templateFile = GetRandomResumeTemplate();

            if (string.IsNullOrWhiteSpace(templateFile))
            {
                MakeFromScratch(person);
                return;
            }

            XWPFDocument doc = null;
            using (FileStream file = new FileStream(templateFile, FileMode.Open, FileAccess.Read))
            {
                doc = new XWPFDocument(file);
            }

            foreach (var prop in person.GetType().GetProperties())
            {
                var dynamicFieldKey = prop.GetCustomAttribute<DynamicFieldKey>();

                if (dynamicFieldKey == null || string.IsNullOrWhiteSpace(dynamicFieldKey.KeyValue)) continue;

                var valueToSet = string.Empty;
                if (prop.PropertyType == typeof(string))
                    valueToSet = (string)prop.GetValue(person);
                else if (prop.PropertyType == typeof(DateTime))
                    valueToSet = ((DateTime)prop.GetValue(person)).ToString("d MMMM yyyy");

                //Avoid endless loops
                if (dynamicFieldKey.KeyValue == valueToSet) continue;

                foreach (var paragraph in doc.Paragraphs)
                    while (paragraph.Text.Contains(dynamicFieldKey.KeyValue))
                        paragraph.ReplaceText(dynamicFieldKey.KeyValue, valueToSet);
            }

            using (var fileStream = File.Create("C:\\dev\\resume.docx"))
            {
                doc.Write(fileStream);
            }
        }

        public void MakeFromScratch(Person person)
        {
            XWPFDocument doc = new XWPFDocument();

            XWPFParagraph p1 = doc.CreateParagraph();
            p1.Alignment = ParagraphAlignment.CENTER;

            XWPFRun r1 = p1.CreateRun();
            r1.SetText("Test paragraph one");

            XWPFParagraph p2 = doc.CreateParagraph();
            p2.Alignment = ParagraphAlignment.LEFT;

            XWPFRun r2 = p2.CreateRun();
            r2.SetText("Test paragraph two");
            r2.FontSize = 16;
            r2.IsBold = true;


            using (var fileStream = File.Create("C:\\dev\\resume.docx"))
            {
                doc.Write(fileStream);
            }
        }

        public Dictionary<string, string> GetDynamicValuesDictionary()
        {
            return new Dictionary<string, string>()
            {
                { Constants.DynamicFieldKeys.FirstName, "" },
                { Constants.DynamicFieldKeys.LastName, "" },
                { Constants.DynamicFieldKeys.DOB, "" },
                { Constants.DynamicFieldKeys.Address, "" },
            };
        }

        private string GetRandomResumeTemplate()
        {
            var parentDirectory = FileUtils.GetRootPath();

            if (string.IsNullOrWhiteSpace(parentDirectory)) return null;

            var templateFiles = Directory.GetFiles(@$"{parentDirectory}/Templates", "*.docx");

            if (templateFiles.Length == 0) return null;

            return DataWorker.SelectRandomArrayEntry(templateFiles);
        }
    }
}
