export class UserEntity {
    public id: string = undefined;
    public code: string = undefined;
    public imageUrl: string = './assets/img/Resources/img-user-default.png';
    public name: string = undefined;
    public lastName: string = undefined;
    public mail: string = undefined;
    public nuCellPhone: string = undefined;
    public codeDesblock: string = undefined;
    public dtCreation: Date = undefined;
    public dtLastUpdate: Date = undefined;
    public isBlock: boolean = false;
    public isActive: boolean = true;
}