export class LogAppEntity {
    public id: number = 0;
    public dtCreation: Date = null;
    public dtLastUpdate: Date = null;
    public deMessage: string;
    public nuVersion: string;
    public isSuccess: boolean = false;
    public isTest: boolean = false;
    public isActive: boolean = true;
}
