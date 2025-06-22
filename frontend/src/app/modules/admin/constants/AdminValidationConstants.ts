export class AdminValidationConstants {
    public static readonly REGEX_PATTERN_ALPHABETS: string = '^[A-Za-z_ ]+';
    public static readonly REGEX_PATTERN_ALPHABETS_BACKSLASH_SPACE: string = '^[A-Za-z_\/ ]+';
    public static readonly REGEX_PATTERN_ALPHANUMERICS: string = '^[a-zA-Z0-9.]*$';
    public static readonly REGEX_PATTERN_ALPHANUMERICS_SPACE: string = '^[a-zA-Z0-9_ .]*$';
    public static readonly REGEX_PATTERN_ALPHANUMERIC_BRACKETS_HYPHEN: string = "[A-Za-z0-9 ()']*";
    public static readonly REGEX_PATTERN_ALPHANUMERIC_UNDERSCORE_DASH_DOT: string = "^[a-zA-Z0-9._ -]*$";
    public static readonly REGEX_PATTERN_ALPHANUMERIC_UNDERSCORE_DASH_DOT_BACKSLASH: string = '^[a-zA-Z0-9._\/ -]*$';
    public static readonly REGEX_PATTERN_NO_BRACKETS: string = '[^<>()]*';
    public static readonly REGEX_PATTERN_NO_ANGELBRACKETS: string = '[^<>`]*';
    public static readonly REGEX_PATTERN_NO_BRACKETS_DIGITS: string = '^[^()<>0-9]*';
    public static readonly REGEX_PATTERN_DIGITS: string = '^[0-9]*$';
    public static readonly REGEX_PASSWORD: string = '^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$';
    public static readonly REQUIRED_VALIDATION_MSG: string = 'Input is required!';
    public static readonly REGEX_SKYPE: string = '^live:.cid.[a-zA-Z0-9.:]*$';
    public static readonly REGEX_URL: string = "https://{1}[^<>()]*";

}
