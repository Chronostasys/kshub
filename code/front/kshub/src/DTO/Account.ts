import STRINGS from "../../components/CommonUtils/ConstStrings"

export interface UserDTO{
    id: string,
    name: string,
    affiliation: string,
    email: string,
    keywords: string[],
    school: string[],
    biography: string,
    isSelf: boolean,
    avatarUrl: string,
    staredDesigns: number,
    awesomes: number,
    follows: number,
    followers: number,
    staredProjects: number,
    exp: number,
    projectNum: number,
    designNum: number,
}

export function getUserEmpty(){
    var user: UserDTO = {
        id: STRINGS.EmptyGuid,
        name: "",
        affiliation: "",
        email: "",
        keywords: [],
        school: [],
        biography: "",
        isSelf: false,
        avatarUrl: "",
        staredDesigns: 0,
        awesomes: 0,
        follows: 0,
        followers: 0,
        staredProjects: 0,
        exp: 0,
        projectNum: 0,
        designNum: 0, 
    }
}