export interface INomination {
  creationDate: string;
  internalProgramTitle: string;
  internalProgramCategory: string;
  justificationComment: string;
  modificationDate: string;
  nominationGuid: string;
  nominatorName: string;
  nomineeName: string;
  nominationStatus: number;
  cycleIteration: number;
  nmsAttachments: [{ attachmentName: string }];
}
