import { DynamicEntity } from "src/app/shared/util/Entities/DynamicEntity";

export class UserRoleEntity implements DynamicEntity {
    public id: string;
    public code: string;
    public name: string;
    public dtCreation: Date;
    public dtLastUpdate: Date;
    public isActive: boolean;
}