export class ArticleCategoryDto implements IArticleCategoryDto {
    id: number | undefined;
    name: string | undefined;
    lable:string | undefined;
    value:string | undefined;

    constructor(data?: IArticleCategoryDto) {
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
            this.lable = data["name"];
            this.value = data["id"].toString();
        }
    }

    static fromJS(data: any): ArticleCategoryDto {
        data = typeof data === 'object' ? data : {};
        let result = new ArticleCategoryDto();
        result.init(data);
        return result;
    }

    static fromJSArray(dataArray: any[]): ArticleCategoryDto[] {
        let array = [];
        dataArray.forEach(result => {
            let item = new ArticleCategoryDto();
            item.init(result);
            array.push(item);
        });
        return array;
    }


    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["name"] = this.name;
        return data;
    }

    clone(): ArticleCategoryDto {
        const json = this.toJSON();
        let result = new ArticleCategoryDto();
        result.init(json);
        return result;
    }
}

export interface IArticleCategoryDto {
    id: number | undefined;
    name: string | undefined;
}




export class SelectArticleCategoryDto {
    lable:string | undefined;
    value:number | undefined;

    constructor(data?: IArticleCategoryDto) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(data?: any) {
        if (data) {
            this.value = data["id"];
            this.lable = data["name"];
        }
    }

    static fromJS(data: any): ArticleCategoryDto {
        data = typeof data === 'object' ? data : {};
        let result = new ArticleCategoryDto();
        result.init(data);
        return result;
    }

    static fromJSArray(dataArray: any[]): ArticleCategoryDto[] {
        let array = [];
        dataArray.forEach(result => {
            let item = new ArticleCategoryDto();
            item.init(result);
            array.push(item);
        });
        return array;
    }
}