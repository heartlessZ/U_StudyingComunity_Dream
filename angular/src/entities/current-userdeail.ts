export class CurrentUserDetailDto implements ICurrentUserDetailDto {
    name: string | undefined;
    surname: string | undefined;
    headPortraitUrl: string | undefined;
    userId: number | undefined;
    userDetailId:string | undefined;
    isAdmin:boolean | undefined;

    constructor(data?: ICurrentUserDetailDto) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(data?: any) {
        if (data) {
            this.name = data["name"];
            this.surname = data["surname"];
            this.headPortraitUrl = data["headPortraitUrl"];
            this.userId = data["userId"];
            this.userDetailId = data["userDetailId"];
            this.isAdmin = data["isAdmin"];
        }
    }

    static fromJS(data: any): CurrentUserDetailDto {
        data = typeof data === 'object' ? data : {};
        let result = new CurrentUserDetailDto();
        result.init(data);
        return result;
    }

    static fromJSArray(dataArray: any[]): CurrentUserDetailDto[] {
        let array = [];
        dataArray.forEach(result => {
            let item = new CurrentUserDetailDto();
            item.init(result);
            array.push(item);
        });
        return array;
    }


    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["name"] = this.name;
        data["surname"] = this.surname;
        data["headPortraitUrl"] = this.headPortraitUrl;
        data["userId"] = this.userId;
        data["userDetailId"] = this.userDetailId;
        data["isAdmin"] = this.isAdmin;
        return data; 
    }

    clone(): CurrentUserDetailDto {
        const json = this.toJSON();
        let result = new CurrentUserDetailDto();
        result.init(json);
        return result;
    }
}

export interface ICurrentUserDetailDto {
    name: string | undefined;
    surname: string | undefined;
    headPortraitUrl: string | undefined;
    userId: number | undefined;
    userDetailId:string | undefined;
    isAdmin:boolean | undefined;
}