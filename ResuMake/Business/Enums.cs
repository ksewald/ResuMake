using System;
using System.Collections.Generic;
using System.Text;

namespace ResumMake.Business
{
    class Enums
    {
        public enum EmailFormats
        {
            /// <summary>
            /// E.g. John.Smith@gmail.com
            /// </summary>
            FirstNameDotLastname,

            /// <summary>
            /// E.g. J.Smith@gmail.com
            /// </summary>
            FirstInitialDotLastName,

            /// <summary>
            /// E.g. John.Smith978@gmail.com
            /// </summary>
            FirstNameDotLastNameWithRandomEnd,

            /// <summary>
            /// E.g. John.Smith93@gmail.com
            /// </summary>
            FirstNameDotLastNameWithYearOfBirth
        }
    }
}
