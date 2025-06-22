export interface Request {
    requestGuid: string,
    requestDescription: string,
    requestRejectionReason: string,
    requestType: number,
    requestStatus: number,
    noOfMembers: number,
    creationDate: string,
    modificationDate: string
}