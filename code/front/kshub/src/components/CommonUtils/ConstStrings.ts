class ConstStrings{
    EmptyGuid: string = "00000000-0000-0000-0000-000000000000";
    PlaceHolder:Object = {
        title:"Title",
        text:"Text"
    };
    ProfilePath(id: string): string {
        return "/profile" + '/' + id;
    };
    homePath(): string {
        return "/";
    }
    getDraftAPI(id: string, editor:  boolean): string {
        return "/api/Design" + '/' + id + "?editor=" +(editor ? "true" : "false");
    };
    DesignImageAPI(id: string | void): string {
        return "/api/Design/UploadDesignImage" + (id ? '?id=' + id : "");
    };
}
const STRINGS = new ConstStrings();
export default STRINGS;