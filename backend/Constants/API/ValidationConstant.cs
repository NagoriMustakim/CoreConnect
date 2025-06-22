namespace LinkwayAPI.Constants.API
{
    public class ValidationConstant
    {
        public const string ALPHABETS_REGEX = "^[A-Za-z_ ]+";
        public const string DESCRIPTION_REGEX = "[^<>`]*";
        public const string NAME_REGEX_MESSAGE = "Enter valid value";
        public const string DESCRIPTION_REGEX_MESSAGE = "Enter valid Description";
        public const string BUSINESS_NAME_REGEX = "[A-Za-z0-9 ()']*";
        public const string REGEX_PATTERN_ALPHANUMERICS_SPACE = "^[a-zA-Z0-9_ .]*$";
        public const string PRONOUN_REGEX = "^[A-Za-z_\\/ ]+";
        public const string ALPHANUMERICS_REGEX = "^[a-zA-Z0-9._ -]*$";
        public const string PROFICIENCY_REGEX = "^[a-zA-Z0-9._ -]*$";
        public const string REGEX_PATTERN_DIGITS = "^[0-9]*$";
        public const string REGEX_PATTERN_ALPHABETS_SPACE_DOT_HYPHEN = "^[a-zA-Z\\s\\.-]*$";
        public const string REGEX_PATTERN_NO_BRACKETS = "[^<>()]*";
        public const string REGEX_PATTERN_NO_BRACKETS_DIGITS = "^[^()<>0-9]*";
    }
}
