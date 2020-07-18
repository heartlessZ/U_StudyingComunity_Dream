export class CurrentUserDetailDto implements ICurrentUserDetailDto {

    static fromJS(data: any): CurrentUserDetailDto {
        data = typeof data === 'object' ? data : {};
        const result = new CurrentUserDetailDto();
        result.init(data);
        return result;
    }

    static fromJSArray(dataArray: any[]): CurrentUserDetailDto[] {
        const array = [];
        dataArray.forEach(result => {
            const item = new CurrentUserDetailDto();
            item.init(result);
            array.push(item);
        });
        return array;
    }
    name: string | undefined;
    surname: string | undefined;
    headPortraitUrl: string | undefined;
    userId: number | undefined;
    userDetailId: string | undefined;
    isAdmin: boolean | undefined;

    constructor(data?: ICurrentUserDetailDto) {
        if (data) {
            for (const property in data) {
                if (data.hasOwnProperty(property)) {
                    (<any>this)[property] = (<any>data)[property];
                }
            }
        }
    }

    init(data?: any) {
        if (data) {
            this.name = data['name'];
            this.surname = data['surname'];
            this.headPortraitUrl = data['headPortraitUrl'];
            this.userId = data['userId'];
            this.userDetailId = data['userDetailId'];
            this.isAdmin = data['isAdmin'];
        }
    }


    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data['name'] = this.name;
        data['surname'] = this.surname;
        data['headPortraitUrl'] = this.headPortraitUrl;
        data['userId'] = this.userId;
        data['userDetailId'] = this.userDetailId;
        data['isAdmin'] = this.isAdmin;
        return data;
    }

    clone(): CurrentUserDetailDto {
        const json = this.toJSON();
        const result = new CurrentUserDetailDto();
        result.init(json);
        return result;
    }
}

export interface ICurrentUserDetailDto {
    name: string | undefined;
    surname: string | undefined;
    headPortraitUrl: string | undefined;
    userId: number | undefined;
    userDetailId: string | undefined;
    isAdmin: boolean | undefined;
}
