import { Skill } from "./Skill";

export interface IProject {
    userProjectGuid: string;
    projectTitle: string;
    projectGuid: string;
    projectStartDate: string;
    projectEndDate: string;
    isProjectActive: boolean;
    projectDescription: string;
    skills:Skill[]
}
