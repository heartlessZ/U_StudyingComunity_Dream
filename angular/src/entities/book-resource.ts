export class BookResourceDto implements IBookResourceDto {
    id:number | undefined;
    bookId: number | undefined;
    name: string | undefined;
    url: string | undefined;
    stutus:number|undefined;
    error:string | undefined;

    constructor(data?: IBookResourceDto) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(data?: any) {
        if (data) {
            this.id = data["id"];
            this.name = data["name"];
            this.bookId = data["bookId"];
            this.url = data["url"];
            this.stutus = data["stutus"];
        }
    }

    static fromJS(data: any): BookResourceDto {
        data = typeof data === 'object' ? data : {};
        let result = new BookResourceDto();
        result.init(data);
        return result;
    }

    static fromJSArray(dataArray: any[]): BookResourceDto[] {
        let array = [];
        dataArray.forEach(result => {
            let item = new BookResourceDto();
            item.init(result);
            array.push(item);
        });
        return array;
    }


    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["name"] = this.name;
        data["bookId"] = this.bookId;
        data["url"] = this.url;
        return data; 
    }

    clone(): BookResourceDto {
        const json = this.toJSON();
        let result = new BookResourceDto();
        result.init(json);
        return result;
    }
}

export interface IBookResourceDto {
    id:number | undefined;
    bookId: number | undefined;
    name: string | undefined;
    url: string | undefined;
}