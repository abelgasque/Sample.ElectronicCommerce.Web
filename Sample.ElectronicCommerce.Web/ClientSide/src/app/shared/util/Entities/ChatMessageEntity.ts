export class ChatMessageEntity {
    public id: string = null; 
    public code: string = null;
    public name: string = null;
    public dtCreation: Date = null;
    public dtLastUpdate: Date = null;
    public isActive: boolean = true;
    public idUserSender: string = null;    
    public idUserDestinatary: string = null;    
    public message: string = null;
}