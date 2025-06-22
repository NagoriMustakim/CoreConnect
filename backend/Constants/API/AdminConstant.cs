namespace LinkwayAPI.Constants.API
{
    public class AdminConstant
    {
        public const string ADMIN_USERID = "admin/{userId}";
        public const string ADMIN = "admin";
        public const string CONFIRM_PASSWORD = "ConfirmPassword";

        public const string ADMIN_EMPLOYMENT_TYPE = "api/employment-types";
        public const string ADMIN_EMPLOYMENT_TYPE_ID = "{employmentTypeId}";

        public const string ADMIN_INTERNAL_PROGRAM_CATEGORY = "api/internal-programs-categories";
        public const string ADMIN_UPDATE_INTERNAL_PROGRAM_CATEGORY_ID = "update/{internalProgramCategoryId}";
        public const string ADMIN_INTERNAL_PROGRAM_CATEGORY_ID = "{internalProgramCategoryId}";

        public const string ADMIN_LOCATION_TYPE = "api/work-models";
        public const string ADMIN_LOCATION_TYPE_ID = "{locationTypeId}";

        public const string ADMIN_TRAINING_TYPE = "api/trainings";
        public const string ADMIN_TRAINING_TYPE_ID = "{trainingTypeId}";

        public const string ADMIN_USERID_TRAININGID = "admin/{userId}/{trainingId}";
        public const string ADMIN_USERID_SKILLID = "admin/{userId}/{skillId}";
        public const string ADMIN_USERID_PROJECTID = "admin/{userId}/{projectId}";
        public const string USERID_PROJECTID = "{userId}/{projectId}";
        public const string ADMIN_USERID_LANGUAGEID = "admin/{userId}/{languageId}";
        public const string ADMIN_UPDATE_SKILL = "admin/update/{userId}/{skillId}";
        public const string ADMIN_UPDATE_TRAINING = "admin/update/{userId}/{trainingId}";
        public const string ADMIN_UPDATE_PROJECT = "admin/update/{userId}/{projectId}";
        public const string ADMIN_UPDATE_LANGUAGE = "admin/update/{userId}/{languageId}";
        public const string ADMIN_DELETE_TRAINING = "admin/delete/{userId}/{trainingId}";
        public const string ADMIN_DELETE_SKILL = "admin/delete/{userId}/{skillId}";
        public const string ADMIN_DELETE_PROJECT = "admin/delete/{userId}/{projectId}";
        public const string ADMIN_DELETE_LANGUAGE = "admin/delete/{userId}/{languageId}";

        public const string ADMIN_USERID_KRAID = "admin/{userId}/{kraId}";
        public const string ADMIN_UPDATE_KRA = "admin/update/{userId}/{kraId}";
        public const string ADMIN_DELETE_KRA = "admin/delete/{userId}/{kraId}";

        public const string ADMIN_USERID_EXPRIENCEID = "admin/{userId}/{experienceId}";
        public const string ADMIN_USERID_EDUCATIONID = "admin/{userId}/{educationId}";
        public const string ADMIN_USERID_CERTIFICATEID = "admin/{userId}/{certificateId}";
        public const string ADMIN_USERID_BASICDETAILID = "admin/{userId}";
        public const string ADMIN_BASICDETAIID = "admin/{userId}";
        public const string ADMIN_BASICDETAI_USERID = "admin/{userId}";

        public const string API_PRONOUNS = "api/pronouns";
        public const string PRONOUNID = "{pronounId}";

        public const string API_PROFICIENCY = "api/proficiency";
        public const string PROFICIENCYID = "{proficiencyId}";

        public const string API_NOMINATION = "api/nominations";
        public const string GET_ALL_NOMINATION = "get-all/{internalProgramId}";
        public const string NOMINATIONID = "{nominationId}";

        public const string API_INTERNALPROGRAM = "api/internalprogram";
        public const string INTERNALPROGRAMID = "{internalProgramId}";

        public const string ADMIN_UPDATE_GIFT = "admin/update/{userId}/{giftProgramId}";
        public const string ADMIN_GET_GIFT_BY_USERID = "admin/{userId}/{giftProgramId}";
        public const string ADMIN_UPDATE_GIFT_STATUS = "{userId}/{giftProgramId}";

        public const string API_BUSINESSUNIT = "api/business-units";
        public const string BUSINESS_UNIT_GUIID = "{businessUnitGuid}";

        public const string API_TRAINING = "api/admin/trainings";
        public const string TRAINING_GUID = "{trainingGuid}";

        public const string NOMINATION_APPROVE = "approve/{nominationId}";
        public const string NOMINATION_REJECT = "reject/{nominationId}";
        public const string GIFT = "GIFT";
        public const string COMMA = ",";
    }
}
