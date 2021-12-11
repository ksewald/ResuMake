namespace ResumMake.Business
{
    public static class Constants
    {

        public static class DynamicFieldKeys
        {
            public const string FirstName = "${FirstName}";
            public const string LastName = "${LastName}";
            public const string DOB = "${DOB}";
            public const string Address = "${Address}";
            public const string Phone = "${Phone}";
            public const string Email = "${Email}";
        }
    }

    public static class ISORegionCodes
    {
        public const string Australia = "AU";
        public const string NewZealand = "NZ";
        public const string UnitedStates = "US";
    }

    /// <summary>
    /// Phone number templates are used to specify the 
    /// rules for generating phone numbers based on the 
    /// Country/region. Different countries will have diffirent
    /// length/format phone numbers as well as different rules
    /// e.g. Australian mobile numbers start with 04.
    /// </summary>
    public static class PhoneNumberTemplates
    {
        //phone numbers can be defined by wrapping curly braces around a number range preceeded with a $.
        //A number will then be generated based on the template.
        //Examples:
        //--------------------------------------------
        //| Template              |   Example Output |
        //--------------------------------------------
        //| ${0-9}-{5-6}          | 3-5
        
        /// <summary>
        /// Example: (555) 555-555
        /// </summary>
        public const string UnitedStates = "(${2-9}${0-9}${0-9}) ${2-9}${0-9}${0-9}-${0-9}${0-9}${0-9}";

        /// <summary>
        /// Example: 0412 345 678
        /// </summary>
        public const string Australia = "${0-0}${4-4}${0-9}${0-9} {0-9}${0-9}{0-9} ${0-9}{0-9}${0-9}";
    }
}
