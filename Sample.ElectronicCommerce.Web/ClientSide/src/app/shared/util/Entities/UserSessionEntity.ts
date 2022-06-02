import { DynamicEntity } from "src/app/shared/util/Entities/DynamicEntity";
import { UserEntity } from "src/app/shared/util/Entities/UserEntity";
import { UserRoleEntity } from "src/app/shared/util/Entities/UserRoleEntity";

export class  UserSessionEntity implements DynamicEntity {
    public id: string;
    public code: string;
    public name: string;
    public dtCreation: Date;
    public dtLastUpdate: Date;
    public isActive: boolean;
    public IdUser: string;
    public dtLastBlock: Date;
    public dtLastDesblock: Date;
    public accessToken: string;
    public version: string;
    public password: string;
    public nuRefreshToken: number;
    public nuAuthAttemptsToken: number;
    public nuSuccessToken: number;
    public nuFailsToken: number;
    public isTest: boolean  = false;
    public isLoggout: boolean = false;
    public user: UserEntity = null;
    public roles: UserRoleEntity[] = [];
}