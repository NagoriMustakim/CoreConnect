export interface Request {
    userName: string,
    requestGuid: string,
    requestDescription: string,
    requestRejectionReason: string,
    requestType: number,
    requestStatus: number,
    noOfMembers: number,
    creationDate: string,
    modificationDate: string,
    requestDate: string
    users:User[]
  }
  interface User{
    id:string,
    firstName:string,
    lastName:string
  }
