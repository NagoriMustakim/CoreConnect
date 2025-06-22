using LinkwayAPI.Constants.ClaimsStore;
using System.Security.Claims;

namespace LinkwayAPI.Models
{
    public static class ClaimsStore
    {
        //Admin claims
        public static List<Claim> AdminClaims = new List<Claim>()
        {
            new Claim(ClaimsConstant.CREATE_BUSSINESS_UNIT, ClaimsConstant.CREATE_BUSSINESS_UNIT),
            new Claim(ClaimsConstant.VIEW_BUSSINESS_UNIT, ClaimsConstant.VIEW_BUSSINESS_UNIT),
            new Claim(ClaimsConstant.EDIT_BUSSINESS_UNIT, ClaimsConstant.EDIT_BUSSINESS_UNIT),
            new Claim(ClaimsConstant.DELETE_BUSSINESS_UNIT, ClaimsConstant.DELETE_BUSSINESS_UNIT),

            new Claim(ClaimsConstant.CREATE_EMPLOYMENT_TYPE, ClaimsConstant.CREATE_EMPLOYMENT_TYPE),
            new Claim(ClaimsConstant.VIEW_EMPLOYMENT_TYPE, ClaimsConstant.VIEW_EMPLOYMENT_TYPE),
            new Claim(ClaimsConstant.EDIT_EMPLOYMENT_TYPE, ClaimsConstant.EDIT_EMPLOYMENT_TYPE),
            new Claim(ClaimsConstant.DELETE_EMPLOYMENT_TYPE, ClaimsConstant.DELETE_EMPLOYMENT_TYPE),

            new Claim(ClaimsConstant.VIEW_GIFT_APPLICATION, ClaimsConstant.VIEW_GIFT_APPLICATION),
            new Claim(ClaimsConstant.APPROVE_GIFT_APPLICATION, ClaimsConstant.APPROVE_GIFT_APPLICATION),
            new Claim(ClaimsConstant.REJECT_GIFT_APPLICATION, ClaimsConstant.REJECT_GIFT_APPLICATION),
            new Claim(ClaimsConstant.DELETE_GIFT_APPLICATION, ClaimsConstant.DELETE_GIFT_APPLICATION),

            new Claim(ClaimsConstant.CREATE_INTERNAL_PROGRAM, ClaimsConstant.CREATE_INTERNAL_PROGRAM),
            new Claim(ClaimsConstant.VIEW_INTERNAL_PROGRAM, ClaimsConstant.VIEW_INTERNAL_PROGRAM),
            new Claim(ClaimsConstant.EDIT_INTERNAL_PROGRAM, ClaimsConstant.EDIT_INTERNAL_PROGRAM),
            new Claim(ClaimsConstant.DELETE_INTERNAL_PROGRAM, ClaimsConstant.DELETE_INTERNAL_PROGRAM),

            new Claim(ClaimsConstant.CREATE_LOCATION_TYPE, ClaimsConstant.CREATE_LOCATION_TYPE),
            new Claim(ClaimsConstant.VIEW_LOCATION_TYPE, ClaimsConstant.VIEW_LOCATION_TYPE),
            new Claim(ClaimsConstant.EDIT_LOCATION_TYPE, ClaimsConstant.EDIT_LOCATION_TYPE),
            new Claim(ClaimsConstant.DELETE_LOCATION_TYPE, ClaimsConstant.DELETE_LOCATION_TYPE),

            new Claim(ClaimsConstant.CREATE_NOMINATION, ClaimsConstant.CREATE_NOMINATION),
            new Claim(ClaimsConstant.VIEW_NOMINATION, ClaimsConstant.VIEW_NOMINATION),
            new Claim(ClaimsConstant.EDIT_NOMINATION, ClaimsConstant.EDIT_NOMINATION),
            new Claim(ClaimsConstant.DELETE_NOMINATION, ClaimsConstant.DELETE_NOMINATION),

            new Claim(ClaimsConstant.CREATE_PROFICIENCY, ClaimsConstant.CREATE_PROFICIENCY),
            new Claim(ClaimsConstant.VIEW_PROFICIENCY, ClaimsConstant.VIEW_PROFICIENCY),
            new Claim(ClaimsConstant.EDIT_PROFICIENCY, ClaimsConstant.EDIT_PROFICIENCY),
            new Claim(ClaimsConstant.DELETE_PROFICIENCY, ClaimsConstant.DELETE_PROFICIENCY),

            new Claim(ClaimsConstant.CREATE_PRONOUN, ClaimsConstant.CREATE_PRONOUN),
            new Claim(ClaimsConstant.VIEW_PRONOUN, ClaimsConstant.VIEW_PRONOUN),
            new Claim(ClaimsConstant.EDIT_PRONOUN, ClaimsConstant.EDIT_PRONOUN),
            new Claim(ClaimsConstant.DELETE_PRONOUN, ClaimsConstant.DELETE_PRONOUN),

            new Claim(ClaimsConstant.VIEW_REQUEST, ClaimsConstant.VIEW_REQUEST),
            new Claim(ClaimsConstant.APPROVE_REQUEST, ClaimsConstant.APPROVE_REQUEST),
            new Claim(ClaimsConstant.REJECT_REQUEST, ClaimsConstant.REJECT_REQUEST),
            new Claim(ClaimsConstant.DELETE_REQUEST, ClaimsConstant.DELETE_REQUEST),

            new Claim(ClaimsConstant.CREATE_TRAINING_TYPE, ClaimsConstant.CREATE_TRAINING_TYPE),
            new Claim(ClaimsConstant.VIEW_TRAINING_TYPE, ClaimsConstant.VIEW_TRAINING_TYPE),
            new Claim(ClaimsConstant.EDIT_TRAINING_TYPE, ClaimsConstant.EDIT_TRAINING_TYPE),
            new Claim(ClaimsConstant.DELETE_TRAINING_TYPE, ClaimsConstant.DELETE_TRAINING_TYPE),

            new Claim(ClaimsConstant.CREATE_USER, ClaimsConstant.CREATE_USER),
            new Claim(ClaimsConstant.VIEW_USER, ClaimsConstant.VIEW_USER),
            new Claim(ClaimsConstant.EDIT_USER, ClaimsConstant.EDIT_USER),
            new Claim(ClaimsConstant.DELETE_USER, ClaimsConstant.DELETE_USER),

            new Claim(ClaimsConstant.CREATE_USER_AWARD, ClaimsConstant.CREATE_USER_AWARD),
            new Claim(ClaimsConstant.VIEW_USER_AWARD, ClaimsConstant.VIEW_USER_AWARD),
            new Claim(ClaimsConstant.EDIT_USER_AWARD, ClaimsConstant.EDIT_USER_AWARD),
            new Claim(ClaimsConstant.DELETE_USER_AWARD, ClaimsConstant.DELETE_USER_AWARD),

            new Claim(ClaimsConstant.CREATE_USER_CERTIFICATE, ClaimsConstant.CREATE_USER_CERTIFICATE),
            new Claim(ClaimsConstant.VIEW_USER_CERTIFICATE, ClaimsConstant.VIEW_USER_CERTIFICATE),
            new Claim(ClaimsConstant.EDIT_USER_CERTIFICATE, ClaimsConstant.EDIT_USER_CERTIFICATE),
            new Claim(ClaimsConstant.DELETE_USER_CERTIFICATE, ClaimsConstant.DELETE_USER_CERTIFICATE),

            new Claim(ClaimsConstant.CREATE_USER_COMMENT, ClaimsConstant.CREATE_USER_COMMENT),
            new Claim(ClaimsConstant.VIEW_USER_COMMENT, ClaimsConstant.VIEW_USER_COMMENT),
            new Claim(ClaimsConstant.EDIT_USER_COMMENT, ClaimsConstant.EDIT_USER_COMMENT),
            new Claim(ClaimsConstant.DELETE_USER_COMMENT, ClaimsConstant.DELETE_USER_COMMENT),

            new Claim(ClaimsConstant.CREATE_USER_EDUCATION, ClaimsConstant.CREATE_USER_EDUCATION),
            new Claim(ClaimsConstant.VIEW_USER_EDUCATION, ClaimsConstant.VIEW_USER_EDUCATION),
            new Claim(ClaimsConstant.EDIT_USER_EDUCATION, ClaimsConstant.EDIT_USER_EDUCATION),
            new Claim(ClaimsConstant.DELETE_USER_EDUCATION, ClaimsConstant.DELETE_USER_EDUCATION),

            new Claim(ClaimsConstant.CREATE_USER_EXPERIENCE, ClaimsConstant.CREATE_USER_EXPERIENCE),
            new Claim(ClaimsConstant.VIEW_USER_EXPERIENCE, ClaimsConstant.VIEW_USER_EXPERIENCE),
            new Claim(ClaimsConstant.EDIT_USER_EXPERIENCE, ClaimsConstant.EDIT_USER_EXPERIENCE),
            new Claim(ClaimsConstant.DELETE_USER_EXPERIENCE, ClaimsConstant.DELETE_USER_EXPERIENCE),

            new Claim(ClaimsConstant.CREATE_USER_KRA, ClaimsConstant.CREATE_USER_KRA),
            new Claim(ClaimsConstant.VIEW_USER_KRA, ClaimsConstant.VIEW_USER_KRA),
            new Claim(ClaimsConstant.EDIT_USER_KRA, ClaimsConstant.EDIT_USER_KRA),
            new Claim(ClaimsConstant.DELETE_USER_KRA, ClaimsConstant.DELETE_USER_KRA),

            new Claim(ClaimsConstant.CREATE_USER_LANGUAGE, ClaimsConstant.CREATE_USER_LANGUAGE),
            new Claim(ClaimsConstant.VIEW_USER_LANGUAGE, ClaimsConstant.VIEW_USER_LANGUAGE),
            new Claim(ClaimsConstant.EDIT_USER_LANGUAGE, ClaimsConstant.EDIT_USER_LANGUAGE),
            new Claim(ClaimsConstant.DELETE_USER_LANGUAGE, ClaimsConstant.DELETE_USER_LANGUAGE),

            new Claim(ClaimsConstant.CREATE_USER_PROJECT, ClaimsConstant.CREATE_USER_PROJECT),
            new Claim(ClaimsConstant.VIEW_USER_PROJECT, ClaimsConstant.VIEW_USER_PROJECT),
            new Claim(ClaimsConstant.EDIT_USER_PROJECT, ClaimsConstant.EDIT_USER_PROJECT),
            new Claim(ClaimsConstant.DELETE_USER_PROJECT, ClaimsConstant.DELETE_USER_PROJECT),

            new Claim(ClaimsConstant.CREATE_USER_SKILL, ClaimsConstant.CREATE_USER_SKILL),
            new Claim(ClaimsConstant.VIEW_USER_SKILL, ClaimsConstant.VIEW_USER_SKILL),
            new Claim(ClaimsConstant.EDIT_USER_SKILL, ClaimsConstant.EDIT_USER_SKILL),
            new Claim(ClaimsConstant.DELETE_USER_SKILL, ClaimsConstant.DELETE_USER_SKILL),

            new Claim(ClaimsConstant.CREATE_USER_TRAINING, ClaimsConstant.CREATE_USER_TRAINING),
            new Claim(ClaimsConstant.VIEW_USER_TRAINING, ClaimsConstant.VIEW_USER_TRAINING),
            new Claim(ClaimsConstant.EDIT_USER_TRAINING, ClaimsConstant.EDIT_USER_TRAINING),
            new Claim(ClaimsConstant.DELETE_USER_TRAINING, ClaimsConstant.DELETE_USER_TRAINING),
        };

        //Manager claims
        public static List<Claim> ManagerClaims = new List<Claim>()
        {
            new Claim(ClaimsConstant.VIEW_BUSSINESS_UNIT, ClaimsConstant.VIEW_BUSSINESS_UNIT),

            new Claim(ClaimsConstant.VIEW_EMPLOYMENT_TYPE, ClaimsConstant.VIEW_EMPLOYMENT_TYPE),

            new Claim(ClaimsConstant.VIEW_INTERNAL_PROGRAM, ClaimsConstant.VIEW_INTERNAL_PROGRAM),

            new Claim(ClaimsConstant.VIEW_LOCATION_TYPE, ClaimsConstant.VIEW_LOCATION_TYPE),

            new Claim(ClaimsConstant.VIEW_PROFICIENCY, ClaimsConstant.VIEW_PROFICIENCY),

            new Claim(ClaimsConstant.VIEW_PRONOUN, ClaimsConstant.VIEW_PRONOUN),

            new Claim(ClaimsConstant.VIEW_TRAINING_TYPE, ClaimsConstant.VIEW_TRAINING_TYPE),

            new Claim(ClaimsConstant.VIEW_GIFT_APPLICATION, ClaimsConstant.VIEW_GIFT_APPLICATION),
            new Claim(ClaimsConstant.APPROVE_GIFT_APPLICATION, ClaimsConstant.APPROVE_GIFT_APPLICATION),
            new Claim(ClaimsConstant.REJECT_GIFT_APPLICATION, ClaimsConstant.REJECT_GIFT_APPLICATION),

            new Claim(ClaimsConstant.CREATE_NOMINATION, ClaimsConstant.CREATE_NOMINATION),
            new Claim(ClaimsConstant.VIEW_NOMINATION, ClaimsConstant.VIEW_NOMINATION),
            new Claim(ClaimsConstant.EDIT_NOMINATION, ClaimsConstant.EDIT_NOMINATION),
            new Claim(ClaimsConstant.DELETE_NOMINATION, ClaimsConstant.DELETE_NOMINATION),

            new Claim(ClaimsConstant.CREATE_REQUEST, ClaimsConstant.CREATE_REQUEST),
            new Claim(ClaimsConstant.EDIT_REQUEST, ClaimsConstant.EDIT_REQUEST),
            new Claim(ClaimsConstant.VIEW_REQUEST, ClaimsConstant.VIEW_REQUEST),
            new Claim(ClaimsConstant.DELETE_REQUEST, ClaimsConstant.DELETE_REQUEST),

            new Claim(ClaimsConstant.CREATE_USER_COMMENT, ClaimsConstant.CREATE_USER_COMMENT),
            new Claim(ClaimsConstant.VIEW_USER_COMMENT, ClaimsConstant.VIEW_USER_COMMENT),
            new Claim(ClaimsConstant.EDIT_USER_COMMENT, ClaimsConstant.EDIT_USER_COMMENT),
            new Claim(ClaimsConstant.DELETE_USER_COMMENT, ClaimsConstant.DELETE_USER_COMMENT),

            new Claim(ClaimsConstant.CREATE_USER_AWARD, ClaimsConstant.CREATE_USER_AWARD),
            new Claim(ClaimsConstant.VIEW_USER_AWARD, ClaimsConstant.VIEW_USER_AWARD),
            new Claim(ClaimsConstant.EDIT_USER_AWARD, ClaimsConstant.EDIT_USER_AWARD),
            new Claim(ClaimsConstant.DELETE_USER_AWARD, ClaimsConstant.DELETE_USER_AWARD),

            new Claim(ClaimsConstant.CREATE_USER_CERTIFICATE, ClaimsConstant.CREATE_USER_CERTIFICATE),
            new Claim(ClaimsConstant.VIEW_USER_CERTIFICATE, ClaimsConstant.VIEW_USER_CERTIFICATE),
            new Claim(ClaimsConstant.EDIT_USER_CERTIFICATE, ClaimsConstant.EDIT_USER_CERTIFICATE),
            new Claim(ClaimsConstant.DELETE_USER_CERTIFICATE, ClaimsConstant.DELETE_USER_CERTIFICATE),

            new Claim(ClaimsConstant.CREATE_USER_EDUCATION, ClaimsConstant.CREATE_USER_EDUCATION),
            new Claim(ClaimsConstant.VIEW_USER_EDUCATION, ClaimsConstant.VIEW_USER_EDUCATION),
            new Claim(ClaimsConstant.EDIT_USER_EDUCATION, ClaimsConstant.EDIT_USER_EDUCATION),
            new Claim(ClaimsConstant.DELETE_USER_EDUCATION, ClaimsConstant.DELETE_USER_EDUCATION),

            new Claim(ClaimsConstant.CREATE_USER_EXPERIENCE, ClaimsConstant.CREATE_USER_EXPERIENCE),
            new Claim(ClaimsConstant.VIEW_USER_EXPERIENCE, ClaimsConstant.VIEW_USER_EXPERIENCE),
            new Claim(ClaimsConstant.EDIT_USER_EXPERIENCE, ClaimsConstant.EDIT_USER_EXPERIENCE),
            new Claim(ClaimsConstant.DELETE_USER_EXPERIENCE, ClaimsConstant.DELETE_USER_EXPERIENCE),

            new Claim(ClaimsConstant.CREATE_USER_KRA, ClaimsConstant.CREATE_USER_KRA),
            new Claim(ClaimsConstant.VIEW_USER_KRA, ClaimsConstant.VIEW_USER_KRA),
            new Claim(ClaimsConstant.EDIT_USER_KRA, ClaimsConstant.EDIT_USER_KRA),
            new Claim(ClaimsConstant.DELETE_USER_KRA, ClaimsConstant.DELETE_USER_KRA),

            new Claim(ClaimsConstant.CREATE_USER_LANGUAGE, ClaimsConstant.CREATE_USER_LANGUAGE),
            new Claim(ClaimsConstant.VIEW_USER_LANGUAGE, ClaimsConstant.VIEW_USER_LANGUAGE),
            new Claim(ClaimsConstant.EDIT_USER_LANGUAGE, ClaimsConstant.EDIT_USER_LANGUAGE),
            new Claim(ClaimsConstant.DELETE_USER_LANGUAGE, ClaimsConstant.DELETE_USER_LANGUAGE),

            new Claim(ClaimsConstant.CREATE_USER_PROJECT, ClaimsConstant.CREATE_USER_PROJECT),
            new Claim(ClaimsConstant.VIEW_USER_PROJECT, ClaimsConstant.VIEW_USER_PROJECT),
            new Claim(ClaimsConstant.EDIT_USER_PROJECT, ClaimsConstant.EDIT_USER_PROJECT),
            new Claim(ClaimsConstant.DELETE_USER_PROJECT, ClaimsConstant.DELETE_USER_PROJECT),

            new Claim(ClaimsConstant.CREATE_USER_SKILL, ClaimsConstant.CREATE_USER_SKILL),
            new Claim(ClaimsConstant.VIEW_USER_SKILL, ClaimsConstant.VIEW_USER_SKILL),
            new Claim(ClaimsConstant.EDIT_USER_SKILL, ClaimsConstant.EDIT_USER_SKILL),
            new Claim(ClaimsConstant.DELETE_USER_SKILL, ClaimsConstant.DELETE_USER_SKILL),

            new Claim(ClaimsConstant.CREATE_USER_TRAINING, ClaimsConstant.CREATE_USER_TRAINING),
            new Claim(ClaimsConstant.VIEW_USER_TRAINING, ClaimsConstant.VIEW_USER_TRAINING),
            new Claim(ClaimsConstant.EDIT_USER_TRAINING, ClaimsConstant.EDIT_USER_TRAINING),
            new Claim(ClaimsConstant.DELETE_USER_TRAINING, ClaimsConstant.DELETE_USER_TRAINING),
        };

        //RMG claims
        public static List<Claim> RMGClaims = new List<Claim>()
        {
            new Claim(ClaimsConstant.VIEW_BUSSINESS_UNIT, ClaimsConstant.VIEW_BUSSINESS_UNIT),

            new Claim(ClaimsConstant.VIEW_EMPLOYMENT_TYPE, ClaimsConstant.VIEW_EMPLOYMENT_TYPE),

            new Claim(ClaimsConstant.VIEW_GIFT_APPLICATION, ClaimsConstant.VIEW_GIFT_APPLICATION),

            new Claim(ClaimsConstant.VIEW_INTERNAL_PROGRAM, ClaimsConstant.VIEW_INTERNAL_PROGRAM),

            new Claim(ClaimsConstant.VIEW_LOCATION_TYPE, ClaimsConstant.VIEW_LOCATION_TYPE),

            new Claim(ClaimsConstant.VIEW_PROFICIENCY, ClaimsConstant.VIEW_PROFICIENCY),

            new Claim(ClaimsConstant.VIEW_PRONOUN, ClaimsConstant.VIEW_PRONOUN),

            new Claim(ClaimsConstant.VIEW_TRAINING_TYPE, ClaimsConstant.VIEW_TRAINING_TYPE),

            new Claim(ClaimsConstant.CREATE_NOMINATION, ClaimsConstant.CREATE_NOMINATION),
            new Claim(ClaimsConstant.VIEW_NOMINATION, ClaimsConstant.VIEW_NOMINATION),
            new Claim(ClaimsConstant.EDIT_NOMINATION, ClaimsConstant.EDIT_NOMINATION),
            new Claim(ClaimsConstant.DELETE_NOMINATION, ClaimsConstant.DELETE_NOMINATION),

            new Claim(ClaimsConstant.CREATE_REQUEST, ClaimsConstant.CREATE_REQUEST),
            new Claim(ClaimsConstant.EDIT_REQUEST, ClaimsConstant.EDIT_REQUEST),
            new Claim(ClaimsConstant.VIEW_REQUEST, ClaimsConstant.VIEW_REQUEST),
            new Claim(ClaimsConstant.DELETE_REQUEST, ClaimsConstant.DELETE_REQUEST),

            new Claim(ClaimsConstant.CREATE_USER, ClaimsConstant.CREATE_USER),

            new Claim(ClaimsConstant.CREATE_USER_COMMENT, ClaimsConstant.CREATE_USER_COMMENT),
            new Claim(ClaimsConstant.VIEW_USER_COMMENT, ClaimsConstant.VIEW_USER_COMMENT),
            new Claim(ClaimsConstant.EDIT_USER_COMMENT, ClaimsConstant.EDIT_USER_COMMENT),
            new Claim(ClaimsConstant.DELETE_USER_COMMENT, ClaimsConstant.DELETE_USER_COMMENT),

            new Claim(ClaimsConstant.CREATE_USER_AWARD, ClaimsConstant.CREATE_USER_AWARD),
            new Claim(ClaimsConstant.VIEW_USER_AWARD, ClaimsConstant.VIEW_USER_AWARD),
            new Claim(ClaimsConstant.EDIT_USER_AWARD, ClaimsConstant.EDIT_USER_AWARD),
            new Claim(ClaimsConstant.DELETE_USER_AWARD, ClaimsConstant.DELETE_USER_AWARD),

            new Claim(ClaimsConstant.CREATE_USER_CERTIFICATE, ClaimsConstant.CREATE_USER_CERTIFICATE),
            new Claim(ClaimsConstant.VIEW_USER_CERTIFICATE, ClaimsConstant.VIEW_USER_CERTIFICATE),
            new Claim(ClaimsConstant.EDIT_USER_CERTIFICATE, ClaimsConstant.EDIT_USER_CERTIFICATE),
            new Claim(ClaimsConstant.DELETE_USER_CERTIFICATE, ClaimsConstant.DELETE_USER_CERTIFICATE),

            new Claim(ClaimsConstant.CREATE_USER_EDUCATION, ClaimsConstant.CREATE_USER_EDUCATION),
            new Claim(ClaimsConstant.VIEW_USER_EDUCATION, ClaimsConstant.VIEW_USER_EDUCATION),
            new Claim(ClaimsConstant.EDIT_USER_EDUCATION, ClaimsConstant.EDIT_USER_EDUCATION),
            new Claim(ClaimsConstant.DELETE_USER_EDUCATION, ClaimsConstant.DELETE_USER_EDUCATION),

            new Claim(ClaimsConstant.CREATE_USER_EXPERIENCE, ClaimsConstant.CREATE_USER_EXPERIENCE),
            new Claim(ClaimsConstant.VIEW_USER_EXPERIENCE, ClaimsConstant.VIEW_USER_EXPERIENCE),
            new Claim(ClaimsConstant.EDIT_USER_EXPERIENCE, ClaimsConstant.EDIT_USER_EXPERIENCE),
            new Claim(ClaimsConstant.DELETE_USER_EXPERIENCE, ClaimsConstant.DELETE_USER_EXPERIENCE),

            new Claim(ClaimsConstant.CREATE_USER_KRA, ClaimsConstant.CREATE_USER_KRA),
            new Claim(ClaimsConstant.VIEW_USER_KRA, ClaimsConstant.VIEW_USER_KRA),
            new Claim(ClaimsConstant.EDIT_USER_KRA, ClaimsConstant.EDIT_USER_KRA),
            new Claim(ClaimsConstant.DELETE_USER_KRA, ClaimsConstant.DELETE_USER_KRA),

            new Claim(ClaimsConstant.CREATE_USER_LANGUAGE, ClaimsConstant.CREATE_USER_LANGUAGE),
            new Claim(ClaimsConstant.VIEW_USER_LANGUAGE, ClaimsConstant.VIEW_USER_LANGUAGE),
            new Claim(ClaimsConstant.EDIT_USER_LANGUAGE, ClaimsConstant.EDIT_USER_LANGUAGE),
            new Claim(ClaimsConstant.DELETE_USER_LANGUAGE, ClaimsConstant.DELETE_USER_LANGUAGE),

            new Claim(ClaimsConstant.CREATE_USER_PROJECT, ClaimsConstant.CREATE_USER_PROJECT),
            new Claim(ClaimsConstant.VIEW_USER_PROJECT, ClaimsConstant.VIEW_USER_PROJECT),
            new Claim(ClaimsConstant.EDIT_USER_PROJECT, ClaimsConstant.EDIT_USER_PROJECT),
            new Claim(ClaimsConstant.DELETE_USER_PROJECT, ClaimsConstant.DELETE_USER_PROJECT),

            new Claim(ClaimsConstant.CREATE_USER_SKILL, ClaimsConstant.CREATE_USER_SKILL),
            new Claim(ClaimsConstant.VIEW_USER_SKILL, ClaimsConstant.VIEW_USER_SKILL),
            new Claim(ClaimsConstant.EDIT_USER_SKILL, ClaimsConstant.EDIT_USER_SKILL),
            new Claim(ClaimsConstant.DELETE_USER_SKILL, ClaimsConstant.DELETE_USER_SKILL),

            new Claim(ClaimsConstant.CREATE_USER_TRAINING, ClaimsConstant.CREATE_USER_TRAINING),
            new Claim(ClaimsConstant.VIEW_USER_TRAINING, ClaimsConstant.VIEW_USER_TRAINING),
            new Claim(ClaimsConstant.EDIT_USER_TRAINING, ClaimsConstant.EDIT_USER_TRAINING),
            new Claim(ClaimsConstant.DELETE_USER_TRAINING, ClaimsConstant.DELETE_USER_TRAINING),
        };

        //HR claims
        public static List<Claim> HRClaims = new List<Claim>()
        {
            new Claim(ClaimsConstant.VIEW_BUSSINESS_UNIT, ClaimsConstant.VIEW_BUSSINESS_UNIT),

            new Claim(ClaimsConstant.VIEW_EMPLOYMENT_TYPE, ClaimsConstant.VIEW_EMPLOYMENT_TYPE),

            new Claim(ClaimsConstant.VIEW_GIFT_APPLICATION, ClaimsConstant.VIEW_GIFT_APPLICATION),

            new Claim(ClaimsConstant.VIEW_INTERNAL_PROGRAM, ClaimsConstant.VIEW_INTERNAL_PROGRAM),

            new Claim(ClaimsConstant.VIEW_LOCATION_TYPE, ClaimsConstant.VIEW_LOCATION_TYPE),

            new Claim(ClaimsConstant.VIEW_PROFICIENCY, ClaimsConstant.VIEW_PROFICIENCY),

            new Claim(ClaimsConstant.VIEW_PRONOUN, ClaimsConstant.VIEW_PRONOUN),

            new Claim(ClaimsConstant.VIEW_TRAINING_TYPE, ClaimsConstant.VIEW_TRAINING_TYPE),

            new Claim(ClaimsConstant.CREATE_NOMINATION, ClaimsConstant.CREATE_NOMINATION),
            new Claim(ClaimsConstant.VIEW_NOMINATION, ClaimsConstant.VIEW_NOMINATION),
            new Claim(ClaimsConstant.EDIT_NOMINATION, ClaimsConstant.EDIT_NOMINATION),
            new Claim(ClaimsConstant.DELETE_NOMINATION, ClaimsConstant.DELETE_NOMINATION),

            new Claim(ClaimsConstant.CREATE_REQUEST, ClaimsConstant.CREATE_REQUEST),
            new Claim(ClaimsConstant.EDIT_REQUEST, ClaimsConstant.EDIT_REQUEST),
            new Claim(ClaimsConstant.VIEW_REQUEST, ClaimsConstant.VIEW_REQUEST),
            new Claim(ClaimsConstant.DELETE_REQUEST, ClaimsConstant.DELETE_REQUEST),

            new Claim(ClaimsConstant.CREATE_USER, ClaimsConstant.CREATE_USER),

            new Claim(ClaimsConstant.CREATE_USER_COMMENT, ClaimsConstant.CREATE_USER_COMMENT),
            new Claim(ClaimsConstant.VIEW_USER_COMMENT, ClaimsConstant.VIEW_USER_COMMENT),
            new Claim(ClaimsConstant.EDIT_USER_COMMENT, ClaimsConstant.EDIT_USER_COMMENT),
            new Claim(ClaimsConstant.DELETE_USER_COMMENT, ClaimsConstant.DELETE_USER_COMMENT),

            new Claim(ClaimsConstant.CREATE_USER_AWARD, ClaimsConstant.CREATE_USER_AWARD),
            new Claim(ClaimsConstant.VIEW_USER_AWARD, ClaimsConstant.VIEW_USER_AWARD),
            new Claim(ClaimsConstant.EDIT_USER_AWARD, ClaimsConstant.EDIT_USER_AWARD),
            new Claim(ClaimsConstant.DELETE_USER_AWARD, ClaimsConstant.DELETE_USER_AWARD),

            new Claim(ClaimsConstant.CREATE_USER_CERTIFICATE, ClaimsConstant.CREATE_USER_CERTIFICATE),
            new Claim(ClaimsConstant.VIEW_USER_CERTIFICATE, ClaimsConstant.VIEW_USER_CERTIFICATE),
            new Claim(ClaimsConstant.EDIT_USER_CERTIFICATE, ClaimsConstant.EDIT_USER_CERTIFICATE),
            new Claim(ClaimsConstant.DELETE_USER_CERTIFICATE, ClaimsConstant.DELETE_USER_CERTIFICATE),

            new Claim(ClaimsConstant.CREATE_USER_EDUCATION, ClaimsConstant.CREATE_USER_EDUCATION),
            new Claim(ClaimsConstant.VIEW_USER_EDUCATION, ClaimsConstant.VIEW_USER_EDUCATION),
            new Claim(ClaimsConstant.EDIT_USER_EDUCATION, ClaimsConstant.EDIT_USER_EDUCATION),
            new Claim(ClaimsConstant.DELETE_USER_EDUCATION, ClaimsConstant.DELETE_USER_EDUCATION),

            new Claim(ClaimsConstant.CREATE_USER_EXPERIENCE, ClaimsConstant.CREATE_USER_EXPERIENCE),
            new Claim(ClaimsConstant.VIEW_USER_EXPERIENCE, ClaimsConstant.VIEW_USER_EXPERIENCE),
            new Claim(ClaimsConstant.EDIT_USER_EXPERIENCE, ClaimsConstant.EDIT_USER_EXPERIENCE),
            new Claim(ClaimsConstant.DELETE_USER_EXPERIENCE, ClaimsConstant.DELETE_USER_EXPERIENCE),

            new Claim(ClaimsConstant.CREATE_USER_KRA, ClaimsConstant.CREATE_USER_KRA),
            new Claim(ClaimsConstant.VIEW_USER_KRA, ClaimsConstant.VIEW_USER_KRA),
            new Claim(ClaimsConstant.EDIT_USER_KRA, ClaimsConstant.EDIT_USER_KRA),
            new Claim(ClaimsConstant.DELETE_USER_KRA, ClaimsConstant.DELETE_USER_KRA),

            new Claim(ClaimsConstant.CREATE_USER_LANGUAGE, ClaimsConstant.CREATE_USER_LANGUAGE),
            new Claim(ClaimsConstant.VIEW_USER_LANGUAGE, ClaimsConstant.VIEW_USER_LANGUAGE),
            new Claim(ClaimsConstant.EDIT_USER_LANGUAGE, ClaimsConstant.EDIT_USER_LANGUAGE),
            new Claim(ClaimsConstant.DELETE_USER_LANGUAGE, ClaimsConstant.DELETE_USER_LANGUAGE),

            new Claim(ClaimsConstant.CREATE_USER_PROJECT, ClaimsConstant.CREATE_USER_PROJECT),
            new Claim(ClaimsConstant.VIEW_USER_PROJECT, ClaimsConstant.VIEW_USER_PROJECT),
            new Claim(ClaimsConstant.EDIT_USER_PROJECT, ClaimsConstant.EDIT_USER_PROJECT),
            new Claim(ClaimsConstant.DELETE_USER_PROJECT, ClaimsConstant.DELETE_USER_PROJECT),

            new Claim(ClaimsConstant.CREATE_USER_SKILL, ClaimsConstant.CREATE_USER_SKILL),
            new Claim(ClaimsConstant.VIEW_USER_SKILL, ClaimsConstant.VIEW_USER_SKILL),
            new Claim(ClaimsConstant.EDIT_USER_SKILL, ClaimsConstant.EDIT_USER_SKILL),
            new Claim(ClaimsConstant.DELETE_USER_SKILL, ClaimsConstant.DELETE_USER_SKILL),

            new Claim(ClaimsConstant.CREATE_USER_TRAINING, ClaimsConstant.CREATE_USER_TRAINING),
            new Claim(ClaimsConstant.VIEW_USER_TRAINING, ClaimsConstant.VIEW_USER_TRAINING),
            new Claim(ClaimsConstant.EDIT_USER_TRAINING, ClaimsConstant.EDIT_USER_TRAINING),
            new Claim(ClaimsConstant.DELETE_USER_TRAINING, ClaimsConstant.DELETE_USER_TRAINING),
        };

        //Candidate claims
        public static List<Claim> CandidateClaims = new List<Claim>()
        {
            new Claim(ClaimsConstant.VIEW_BUSSINESS_UNIT, ClaimsConstant.VIEW_BUSSINESS_UNIT),

            new Claim(ClaimsConstant.VIEW_EMPLOYMENT_TYPE, ClaimsConstant.VIEW_EMPLOYMENT_TYPE),

            new Claim(ClaimsConstant.CREATE_GIFT_APPLICATION, ClaimsConstant.CREATE_GIFT_APPLICATION),
            new Claim(ClaimsConstant.VIEW_GIFT_APPLICATION, ClaimsConstant.VIEW_GIFT_APPLICATION),
            new Claim(ClaimsConstant.UPDATE_GIFT_APPLICATION, ClaimsConstant.UPDATE_GIFT_APPLICATION),
            new Claim(ClaimsConstant.DELETE_GIFT_APPLICATION, ClaimsConstant.DELETE_GIFT_APPLICATION),

            new Claim(ClaimsConstant.VIEW_LOCATION_TYPE, ClaimsConstant.VIEW_LOCATION_TYPE),

            new Claim(ClaimsConstant.VIEW_PROFICIENCY, ClaimsConstant.VIEW_PROFICIENCY),

            new Claim(ClaimsConstant.VIEW_PRONOUN, ClaimsConstant.VIEW_PRONOUN),

            new Claim(ClaimsConstant.VIEW_TRAINING_TYPE, ClaimsConstant.VIEW_TRAINING_TYPE),

            new Claim(ClaimsConstant.CREATE_USER_AWARD, ClaimsConstant.CREATE_USER_AWARD),
            new Claim(ClaimsConstant.VIEW_USER_AWARD, ClaimsConstant.VIEW_USER_AWARD),
            new Claim(ClaimsConstant.EDIT_USER_AWARD, ClaimsConstant.EDIT_USER_AWARD),
            new Claim(ClaimsConstant.DELETE_USER_AWARD, ClaimsConstant.DELETE_USER_AWARD),

            new Claim(ClaimsConstant.CREATE_USER_CERTIFICATE, ClaimsConstant.CREATE_USER_CERTIFICATE),
            new Claim(ClaimsConstant.VIEW_USER_CERTIFICATE, ClaimsConstant.VIEW_USER_CERTIFICATE),
            new Claim(ClaimsConstant.EDIT_USER_CERTIFICATE, ClaimsConstant.EDIT_USER_CERTIFICATE),
            new Claim(ClaimsConstant.DELETE_USER_CERTIFICATE, ClaimsConstant.DELETE_USER_CERTIFICATE),

            new Claim(ClaimsConstant.CREATE_USER_EDUCATION, ClaimsConstant.CREATE_USER_EDUCATION),
            new Claim(ClaimsConstant.VIEW_USER_EDUCATION, ClaimsConstant.VIEW_USER_EDUCATION),
            new Claim(ClaimsConstant.EDIT_USER_EDUCATION, ClaimsConstant.EDIT_USER_EDUCATION),
            new Claim(ClaimsConstant.DELETE_USER_EDUCATION, ClaimsConstant.DELETE_USER_EDUCATION),

            new Claim(ClaimsConstant.CREATE_USER_EXPERIENCE, ClaimsConstant.CREATE_USER_EXPERIENCE),
            new Claim(ClaimsConstant.VIEW_USER_EXPERIENCE, ClaimsConstant.VIEW_USER_EXPERIENCE),
            new Claim(ClaimsConstant.EDIT_USER_EXPERIENCE, ClaimsConstant.EDIT_USER_EXPERIENCE),
            new Claim(ClaimsConstant.DELETE_USER_EXPERIENCE, ClaimsConstant.DELETE_USER_EXPERIENCE),

            new Claim(ClaimsConstant.VIEW_USER_KRA, ClaimsConstant.VIEW_USER_KRA),

            new Claim(ClaimsConstant.CREATE_USER_LANGUAGE, ClaimsConstant.CREATE_USER_LANGUAGE),
            new Claim(ClaimsConstant.VIEW_USER_LANGUAGE, ClaimsConstant.VIEW_USER_LANGUAGE),
            new Claim(ClaimsConstant.EDIT_USER_LANGUAGE, ClaimsConstant.EDIT_USER_LANGUAGE),
            new Claim(ClaimsConstant.DELETE_USER_LANGUAGE, ClaimsConstant.DELETE_USER_LANGUAGE),

            new Claim(ClaimsConstant.CREATE_USER_PROJECT, ClaimsConstant.CREATE_USER_PROJECT),
            new Claim(ClaimsConstant.VIEW_USER_PROJECT, ClaimsConstant.VIEW_USER_PROJECT),
            new Claim(ClaimsConstant.EDIT_USER_PROJECT, ClaimsConstant.EDIT_USER_PROJECT),
            new Claim(ClaimsConstant.DELETE_USER_PROJECT, ClaimsConstant.DELETE_USER_PROJECT),

            new Claim(ClaimsConstant.CREATE_USER_SKILL, ClaimsConstant.CREATE_USER_SKILL),
            new Claim(ClaimsConstant.VIEW_USER_SKILL, ClaimsConstant.VIEW_USER_SKILL),
            new Claim(ClaimsConstant.EDIT_USER_SKILL, ClaimsConstant.EDIT_USER_SKILL),
            new Claim(ClaimsConstant.DELETE_USER_SKILL, ClaimsConstant.DELETE_USER_SKILL),

            new Claim(ClaimsConstant.CREATE_USER_TRAINING, ClaimsConstant.CREATE_USER_TRAINING),
            new Claim(ClaimsConstant.VIEW_USER_TRAINING, ClaimsConstant.VIEW_USER_TRAINING),
            new Claim(ClaimsConstant.EDIT_USER_TRAINING, ClaimsConstant.EDIT_USER_TRAINING),
            new Claim(ClaimsConstant.DELETE_USER_TRAINING, ClaimsConstant.DELETE_USER_TRAINING),
        };

    }
}
