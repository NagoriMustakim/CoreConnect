using LinkwayAPI.Constants.ClaimsStore;
using LinkwayAPI.Constants.Permission;

namespace LinkwayAPI.Configurations
{
    public static class Policies
    {
        public static void AddPolicies(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(PolicyStrings.CREATE_NOMINATION_POLICY, policy => policy.RequireClaim(ClaimsConstant.CREATE_NOMINATION));
                options.AddPolicy(PolicyStrings.VIEW_NOMINATION_POLICY, policy => policy.RequireClaim(ClaimsConstant.VIEW_NOMINATION));
                options.AddPolicy(PolicyStrings.UPDATE_NOMINATION_POLICY, policy => policy.RequireClaim(ClaimsConstant.EDIT_NOMINATION));
                options.AddPolicy(PolicyStrings.DELETE_NOMINATION_POLICY, policy => policy.RequireClaim(ClaimsConstant.DELETE_NOMINATION));

                options.AddPolicy(PolicyStrings.CREATE_BUSSINESS_UNIT, policy => policy.RequireClaim(ClaimsConstant.CREATE_BUSSINESS_UNIT));
                options.AddPolicy(PolicyStrings.VIEW_BUSSINESS_UNIT, policy => policy.RequireClaim(ClaimsConstant.VIEW_BUSSINESS_UNIT));
                options.AddPolicy(PolicyStrings.EDIT_BUSSINESS_UNIT, policy => policy.RequireClaim(ClaimsConstant.EDIT_BUSSINESS_UNIT));
                options.AddPolicy(PolicyStrings.DELETE_BUSSINESS_UNIT, policy => policy.RequireClaim(ClaimsConstant.DELETE_BUSSINESS_UNIT));

                options.AddPolicy(PolicyStrings.CREATE_EMPLOYMENT_TYPE, policy => policy.RequireClaim(ClaimsConstant.CREATE_EMPLOYMENT_TYPE));
                options.AddPolicy(PolicyStrings.VIEW_EMPLOYMENT_TYPE, policy => policy.RequireClaim(ClaimsConstant.VIEW_EMPLOYMENT_TYPE));
                options.AddPolicy(PolicyStrings.EDIT_EMPLOYMENT_TYPE, policy => policy.RequireClaim(ClaimsConstant.EDIT_EMPLOYMENT_TYPE));
                options.AddPolicy(PolicyStrings.DELETE_EMPLOYMENT_TYPE, policy => policy.RequireClaim(ClaimsConstant.DELETE_EMPLOYMENT_TYPE));

                options.AddPolicy(PolicyStrings.CREATE_GIFT_APPLICATION, policy => policy.RequireClaim(ClaimsConstant.CREATE_GIFT_APPLICATION));
                options.AddPolicy(PolicyStrings.VIEW_GIFT_APPLICATION, policy => policy.RequireClaim(ClaimsConstant.VIEW_GIFT_APPLICATION));
                options.AddPolicy(PolicyStrings.UPDATE_GIFT_APPLICATION, policy => policy.RequireClaim(ClaimsConstant.UPDATE_GIFT_APPLICATION));
                options.AddPolicy(PolicyStrings.DELETE_GIFT_APPLICATION, policy => policy.RequireClaim(ClaimsConstant.DELETE_GIFT_APPLICATION));
                options.AddPolicy(PolicyStrings.APPROVE_GIFT_APPLICATION, policy => policy.RequireClaim(ClaimsConstant.APPROVE_GIFT_APPLICATION));
                options.AddPolicy(PolicyStrings.REJECT_GIFT_APPLICATION, policy => policy.RequireClaim(ClaimsConstant.REJECT_GIFT_APPLICATION));

                options.AddPolicy(PolicyStrings.CREATE_INTERNAL_PROGRAM, policy => policy.RequireClaim(ClaimsConstant.CREATE_INTERNAL_PROGRAM));
                options.AddPolicy(PolicyStrings.VIEW_INTERNAL_PROGRAM, policy => policy.RequireClaim(ClaimsConstant.VIEW_INTERNAL_PROGRAM));
                options.AddPolicy(PolicyStrings.EDIT_INTERNAL_PROGRAM, policy => policy.RequireClaim(ClaimsConstant.EDIT_INTERNAL_PROGRAM));
                options.AddPolicy(PolicyStrings.DELETE_INTERNAL_PROGRAM, policy => policy.RequireClaim(ClaimsConstant.DELETE_INTERNAL_PROGRAM));

                options.AddPolicy(PolicyStrings.CREATE_LOCATION_TYPE, policy => policy.RequireClaim(ClaimsConstant.CREATE_LOCATION_TYPE));
                options.AddPolicy(PolicyStrings.VIEW_LOCATION_TYPE, policy => policy.RequireClaim(ClaimsConstant.VIEW_LOCATION_TYPE));
                options.AddPolicy(PolicyStrings.EDIT_LOCATION_TYPE, policy => policy.RequireClaim(ClaimsConstant.EDIT_LOCATION_TYPE));
                options.AddPolicy(PolicyStrings.DELETE_LOCATION_TYPE, policy => policy.RequireClaim(ClaimsConstant.DELETE_LOCATION_TYPE));

                options.AddPolicy(PolicyStrings.CREATE_PROFICIENCY, policy => policy.RequireClaim(ClaimsConstant.CREATE_PROFICIENCY));
                options.AddPolicy(PolicyStrings.VIEW_PROFICIENCY, policy => policy.RequireClaim(ClaimsConstant.VIEW_PROFICIENCY));
                options.AddPolicy(PolicyStrings.EDIT_PROFICIENCY, policy => policy.RequireClaim(ClaimsConstant.EDIT_PROFICIENCY));
                options.AddPolicy(PolicyStrings.DELETE_PROFICIENCY, policy => policy.RequireClaim(ClaimsConstant.DELETE_PROFICIENCY));

                options.AddPolicy(PolicyStrings.CREATE_PRONOUN, policy => policy.RequireClaim(ClaimsConstant.CREATE_PRONOUN));
                options.AddPolicy(PolicyStrings.VIEW_PRONOUN, policy => policy.RequireClaim(ClaimsConstant.VIEW_PRONOUN));
                options.AddPolicy(PolicyStrings.EDIT_PRONOUN, policy => policy.RequireClaim(ClaimsConstant.EDIT_PRONOUN));
                options.AddPolicy(PolicyStrings.DELETE_PRONOUN, policy => policy.RequireClaim(ClaimsConstant.DELETE_PRONOUN));

                options.AddPolicy(PolicyStrings.VIEW_REQUEST, policy => policy.RequireClaim(ClaimsConstant.VIEW_REQUEST));
                options.AddPolicy(PolicyStrings.APPROVE_REQUEST, policy => policy.RequireClaim(ClaimsConstant.APPROVE_REQUEST));
                options.AddPolicy(PolicyStrings.REJECT_REQUEST, policy => policy.RequireClaim(ClaimsConstant.REJECT_REQUEST));
                options.AddPolicy(PolicyStrings.DELETE_REQUEST, policy => policy.RequireClaim(ClaimsConstant.DELETE_REQUEST));
                options.AddPolicy(PolicyStrings.CREATE_REQUEST, policy => policy.RequireClaim(ClaimsConstant.CREATE_REQUEST));
                options.AddPolicy(PolicyStrings.EDIT_REQUEST, policy => policy.RequireClaim(ClaimsConstant.EDIT_REQUEST));

                options.AddPolicy(PolicyStrings.CREATE_TRAINING_TYPE, policy => policy.RequireClaim(ClaimsConstant.CREATE_TRAINING_TYPE));
                options.AddPolicy(PolicyStrings.VIEW_TRAINING_TYPE, policy => policy.RequireClaim(ClaimsConstant.VIEW_TRAINING_TYPE));
                options.AddPolicy(PolicyStrings.EDIT_TRAINING_TYPE, policy => policy.RequireClaim(ClaimsConstant.EDIT_TRAINING_TYPE));
                options.AddPolicy(PolicyStrings.DELETE_TRAINING_TYPE, policy => policy.RequireClaim(ClaimsConstant.DELETE_TRAINING_TYPE));

                options.AddPolicy(PolicyStrings.CREATE_USER, policy => policy.RequireClaim(ClaimsConstant.CREATE_USER));
                options.AddPolicy(PolicyStrings.VIEW_USER, policy => policy.RequireClaim(ClaimsConstant.VIEW_USER));
                options.AddPolicy(PolicyStrings.EDIT_USER, policy => policy.RequireClaim(ClaimsConstant.EDIT_USER));
                options.AddPolicy(PolicyStrings.DELETE_USER, policy => policy.RequireClaim(ClaimsConstant.DELETE_USER));

                options.AddPolicy(PolicyStrings.CREATE_USER_AWARD, policy => policy.RequireClaim(ClaimsConstant.CREATE_USER_AWARD));
                options.AddPolicy(PolicyStrings.VIEW_USER_AWARD, policy => policy.RequireClaim(ClaimsConstant.VIEW_USER_AWARD));
                options.AddPolicy(PolicyStrings.EDIT_USER_AWARD, policy => policy.RequireClaim(ClaimsConstant.EDIT_USER_AWARD));
                options.AddPolicy(PolicyStrings.DELETE_USER_AWARD, policy => policy.RequireClaim(ClaimsConstant.DELETE_USER_AWARD));

                options.AddPolicy(PolicyStrings.CREATE_USER_CERTIFICATE, policy => policy.RequireClaim(ClaimsConstant.CREATE_USER_CERTIFICATE));
                options.AddPolicy(PolicyStrings.VIEW_USER_CERTIFICATE, policy => policy.RequireClaim(ClaimsConstant.VIEW_USER_CERTIFICATE));
                options.AddPolicy(PolicyStrings.EDIT_USER_CERTIFICATE, policy => policy.RequireClaim(ClaimsConstant.EDIT_USER_CERTIFICATE));
                options.AddPolicy(PolicyStrings.DELETE_USER_CERTIFICATE, policy => policy.RequireClaim(ClaimsConstant.DELETE_USER_CERTIFICATE));

                options.AddPolicy(PolicyStrings.CREATE_USER_COMMENT, policy => policy.RequireClaim(ClaimsConstant.CREATE_USER_COMMENT));
                options.AddPolicy(PolicyStrings.VIEW_USER_COMMENT, policy => policy.RequireClaim(ClaimsConstant.VIEW_USER_COMMENT));
                options.AddPolicy(PolicyStrings.EDIT_USER_COMMENT, policy => policy.RequireClaim(ClaimsConstant.EDIT_USER_COMMENT));
                options.AddPolicy(PolicyStrings.DELETE_USER_COMMENT, policy => policy.RequireClaim(ClaimsConstant.DELETE_USER_COMMENT));

                options.AddPolicy(PolicyStrings.CREATE_USER_EDUCATION, policy => policy.RequireClaim(ClaimsConstant.CREATE_USER_EDUCATION));
                options.AddPolicy(PolicyStrings.VIEW_USER_EDUCATION, policy => policy.RequireClaim(ClaimsConstant.VIEW_USER_EDUCATION));
                options.AddPolicy(PolicyStrings.EDIT_USER_EDUCATION, policy => policy.RequireClaim(ClaimsConstant.EDIT_USER_EDUCATION));
                options.AddPolicy(PolicyStrings.DELETE_USER_EDUCATION, policy => policy.RequireClaim(ClaimsConstant.DELETE_USER_EDUCATION));

                options.AddPolicy(PolicyStrings.CREATE_USER_EXPERIENCE, policy => policy.RequireClaim(ClaimsConstant.CREATE_USER_EXPERIENCE));
                options.AddPolicy(PolicyStrings.VIEW_USER_EXPERIENCE, policy => policy.RequireClaim(ClaimsConstant.VIEW_USER_EXPERIENCE));
                options.AddPolicy(PolicyStrings.EDIT_USER_EXPERIENCE, policy => policy.RequireClaim(ClaimsConstant.EDIT_USER_EXPERIENCE));
                options.AddPolicy(PolicyStrings.DELETE_USER_EXPERIENCE, policy => policy.RequireClaim(ClaimsConstant.DELETE_USER_EXPERIENCE));

                options.AddPolicy(PolicyStrings.CREATE_USER_KRA, policy => policy.RequireClaim(ClaimsConstant.CREATE_USER_KRA));
                options.AddPolicy(PolicyStrings.VIEW_USER_KRA, policy => policy.RequireClaim(ClaimsConstant.VIEW_USER_KRA));
                options.AddPolicy(PolicyStrings.EDIT_USER_KRA, policy => policy.RequireClaim(ClaimsConstant.EDIT_USER_KRA));
                options.AddPolicy(PolicyStrings.DELETE_USER_KRA, policy => policy.RequireClaim(ClaimsConstant.DELETE_USER_KRA));

                options.AddPolicy(PolicyStrings.CREATE_USER_LANGUAGE, policy => policy.RequireClaim(ClaimsConstant.CREATE_USER_LANGUAGE));
                options.AddPolicy(PolicyStrings.VIEW_USER_LANGUAGE, policy => policy.RequireClaim(ClaimsConstant.VIEW_USER_LANGUAGE));
                options.AddPolicy(PolicyStrings.EDIT_USER_LANGUAGE, policy => policy.RequireClaim(ClaimsConstant.EDIT_USER_LANGUAGE));
                options.AddPolicy(PolicyStrings.DELETE_USER_LANGUAGE, policy => policy.RequireClaim(ClaimsConstant.DELETE_USER_LANGUAGE));

                options.AddPolicy(PolicyStrings.CREATE_USER_PROJECT, policy => policy.RequireClaim(ClaimsConstant.CREATE_USER_PROJECT));
                options.AddPolicy(PolicyStrings.VIEW_USER_PROJECT, policy => policy.RequireClaim(ClaimsConstant.VIEW_USER_PROJECT));
                options.AddPolicy(PolicyStrings.EDIT_USER_PROJECT, policy => policy.RequireClaim(ClaimsConstant.EDIT_USER_PROJECT));
                options.AddPolicy(PolicyStrings.DELETE_USER_PROJECT, policy => policy.RequireClaim(ClaimsConstant.DELETE_USER_PROJECT));

                options.AddPolicy(PolicyStrings.CREATE_USER_SKILL, policy => policy.RequireClaim(ClaimsConstant.CREATE_USER_SKILL));
                options.AddPolicy(PolicyStrings.VIEW_USER_SKILL, policy => policy.RequireClaim(ClaimsConstant.VIEW_USER_SKILL));
                options.AddPolicy(PolicyStrings.EDIT_USER_SKILL, policy => policy.RequireClaim(ClaimsConstant.EDIT_USER_SKILL));
                options.AddPolicy(PolicyStrings.DELETE_USER_SKILL, policy => policy.RequireClaim(ClaimsConstant.DELETE_USER_SKILL));

                options.AddPolicy(PolicyStrings.CREATE_USER_TRAINING, policy => policy.RequireClaim(ClaimsConstant.CREATE_USER_TRAINING));
                options.AddPolicy(PolicyStrings.VIEW_USER_TRAINING, policy => policy.RequireClaim(ClaimsConstant.VIEW_USER_TRAINING));
                options.AddPolicy(PolicyStrings.EDIT_USER_TRAINING, policy => policy.RequireClaim(ClaimsConstant.EDIT_USER_TRAINING));
                options.AddPolicy(PolicyStrings.DELETE_USER_TRAINING, policy => policy.RequireClaim(ClaimsConstant.DELETE_USER_TRAINING));
            });
        }
    }
}
