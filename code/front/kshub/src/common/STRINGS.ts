class constSTRINGS {
    constructor() {
        
    }
    loginApi:string='/api/kshubuser/login';
    registerApi:string='/api/kshubuser/adduser';
}
class UserInfo {
    name: string;
    studentId: string;
    schoolName: string;
    introduction: string;
    email: string;
    role: string;
}

export { UserInfo };

const STRINGS = new constSTRINGS();
export default STRINGS;
