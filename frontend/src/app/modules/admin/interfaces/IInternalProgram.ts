export interface IInternalProgram {
    internalProgramGuid: string;
    internalProgramName: string;
    internalProgramDescription: string;
    internalProgramReviewCycle: number;
    internalProgramActiveDays: number;
    internalProgramStartDate: Date;
    internalProgramEndDate: Date;
    isInternalProgramCategoryExists: boolean;
    creationDate: string;
    modificationDate: string;
    isActive:boolean;
}
