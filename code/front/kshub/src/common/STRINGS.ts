class constSTRINGS {
    constructor() {
        
    }
    loginApi:string='/api/user/login';
    registerApi:string='/api/user/register';
    signoutApi: string = '/api/User/Signout';
    Roles={
      anonymous:'Anonymous',
      user:'User',
      admin:'Admin'
    }
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
    roles: string[] = [];
}
const STRINGS = new constSTRINGS();
export { UserInfo, RegDto, STRINGS };

export default STRINGS;