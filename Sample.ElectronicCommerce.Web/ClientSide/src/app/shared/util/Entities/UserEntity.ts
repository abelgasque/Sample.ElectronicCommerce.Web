import { DynamicEntity } from "src/app/shared/util/Entities/DynamicEntity";

export class UserEntity implements DynamicEntity {
    public id: string;
    public code: string;
    public imageUrl: string = './assets/img/Resources/img-user-default.png';
    public name: string;
    public lastName: string;
    public mail: string;
    public nuCellPhone: string;
    public codeDesblock: string;
    public dtCreation: Date;
    public dtLastUpdate: Date;
    public isBlock: boolean = false;
    public isActive: boolean = true;  
}