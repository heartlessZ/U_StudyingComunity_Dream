export class UserDetailDto implements IUserDetailDto {
    name: string | undefined;
    surname: string | undefined;
    description: string | undefined;
    headPortraitUrl: string | undefined;
    userId: number | undefined;
    gender:number | undefined;
    birthday:Date | undefined;
    site: string | undefined;
    occupation: string | undefined;
    phoneNumber: string | undefined;
    email: string | undefined;
    id:string | undefined;

    constructor(data?: IUserDetailDto) {
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
            this.description = data["description"];
            this.headPortraitUrl = data["headPortraitUrl"];
            this.userId = data["userId"];
            this.gender = data["gender"];
            this.birthday = data["birthday"];
            this.site = data["site"];
            this.id = data["id"];
            this.occupation = data["occupation"];
            this.phoneNumber = data["phoneNumber"];
            this.email = data["email"];
        }
    }

    static fromJS(data: any): UserDetailDto {
        data = typeof data === 'object' ? data : {};
        let result = new UserDetailDto();
        result.init(data);
        return result;
    }

    static fromJSArray(dataArray: any[]): UserDetailDto[] {
        let array = [];
        dataArray.forEach(result => {
            let item = new UserDetailDto();
            item.init(result);
            array.push(item);
        });
        return array;
    }


    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["name"] = this.name;
        data["surname"] = this.surname;
        data["description"] = this.description;
        data["headPortraitUrl"] = this.headPortraitUrl;
        data["userId"] = this.userId;
        data["gender"] = this.gender;
        data["birthday"] = this.birthday;
        data["site"] = this.site;
        data["id"] = this.id;
        data["occupation"] = this.occupation;
        data["phoneNumber"] = this.phoneNumber;
        data["email"] = this.email;
        return data; 
    }

    clone(): UserDetailDto {
        const json = this.toJSON();
        let result = new UserDetailDto();
        result.init(json);
        return result;
    }
}

export interface IUserDetailDto {
    name: string | undefined;
    surname: string | undefined;
    description: string | undefined;
    headPortraitUrl: string | undefined;
    userId: number | undefined;
    gender:number | undefined;
    birthday:Date | undefined;
    site: string | undefined;
    occupation: string | undefined;
    phoneNumber: string | undefined;
    email: string | undefined;
}