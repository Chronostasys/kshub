class constSTRINGS {
    constructor() {
        
    }
    loginApi:string='/api/user/login';
    registerApi:string='/api/user/register';
}
class RegDto{
  name: string;
  userId: string;
  password: string;
  schoolName: string;
  introduction: string;
  email: string;
  rememberme:boolean;
}
class UserInfo {
    name: string;
    userId: string;
    schoolName: string;
    introduction: string;
    email: string;
    roles: [
      string
    ]
}

export { UserInfo, RegDto };

const STRINGS = new constSTRINGS();
export default STRINGS;