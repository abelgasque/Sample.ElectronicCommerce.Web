export class MailBrokerEntity {
    public id: number = 0;
    public dtCreation: Date = null;
    public dtLastUpdate: Date = null;    
    public name: string = '';
    public server: string = '';
    public userName: string = '';
    public password: string = '';    
    public code: string = '';
    public port: number = null;
    public isEnabledSsl: boolean = true;
    public isActive: boolean = true;
}