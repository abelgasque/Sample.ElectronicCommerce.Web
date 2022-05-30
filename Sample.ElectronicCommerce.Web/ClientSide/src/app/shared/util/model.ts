export interface ReturnDTO {
    isSuccess: boolean;
    deMessage: string
    resultObject: any;
}

export class UserDTO {    
    public mail: string = '';
    public password: string = '';
}

export class TokenDTO {
    public idUserSession: string;
    public accessToken: string;
    public expiresIn: number;
}
   
export class Menu {
    public id: number = 0;
    public name: string = null;
    public isActive: boolean = true;
}

export class MenuItem {
    public id: number = 0;
    public name: string = null;
    public isActive: boolean = true;
}

export class UserRole {
    public id: string = null;
    public code: string = '';
    public name: string = '';
    public isActive: boolean = false;
}

export class User {
    public id: string = null;
    public dtCreation: Date = null;
    public dtLastUpdate: Date = null;
    public dtLastBlock: Date = null;
    public dtLastDesblock: Date = null;
    public imageUrl: string = './assets/img/Resources/img-user-default.png';
    public mail: string = null;
    public name: string = null;
    public lastName: string = null;
    public password: string = null;
    public provider: string = 'Comum'; 
    public codeDesblock: string = null;
    public nuCellPhone: string = null;
    public isBlock: boolean = false;
    public isActive: boolean = true;
    public roles: UserRole[] = [];
}

export class UserSession {
    public id: string = null;
    public idUser: string = null;
    public dtCreation: Date = null;
    public dtLastUpdate: Date = null;
    public dtAccessTokenExpiration: Date = null;
    public dtRefreshTokenExpiration: Date = null;
    public accessToken: string = '';
    public refreshToken: string = '';
    public deMessage: string = '';
    public nuVersion: string = '';
    public nuAuthenticationAttempts: number = 0;
    public nuRefreshToken: number = 0;
    public isSuccess: boolean = false;
    public isTest: boolean = false;
    public isActive: boolean = true;
    public user: User = new User();
}

export class LogApp {
    public id: number = 0;
    public dtCreation: Date = null;
    public dtLastUpdate: Date = null;
    public deMessage: string;
    public nuVersion: string;
    public isSuccess: boolean = false;
    public isTest: boolean = false;
    public isActive: boolean = true;
}

export class MailBroker {
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

export class Mail { 
    public id: number = 0;
    public dtCreation: Date = null;
    public dtLastUpdate: Date = null;    
    public name: string = '';
    public isActive: boolean = true;
}

export class MailMessage { 
    public id: number = 0;
    public dtCreation: Date = null;
    public dtLastUpdate: Date = null;    
    public name: string = '';
    public isActive: boolean = true;
}