export class BookCategoryDto implements IBookCategoryDto {
    parent:number | undefined;
    key: number | undefined;
    title:string | undefined;
    children:BookCategoryDto[] | undefined;
    isLeaf:boolean | undefined;
    isSelected:boolean | undefined;

    constructor(data?: IBookCategoryDto) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(data?: any) {
        if (data) {
            this.parent = data["parent"];
            this.key = data["key"];
            this.title = data["title"];
            this.children = data["children"];
        }
    }

    static fromJS(data: any): BookCategoryDto {
        data = typeof data === 'object' ? data : {};
        let result = new BookCategoryDto();
        result.init(data);
        return result;
    }

    static fromJSArray(dataArray: any[]): BookCategoryDto[] {
        let array = [];
        dataArray.forEach(result => {
            let item = new BookCategoryDto();
            item.init(result);
            if(item.children.length > 0)
            {
                item.children.forEach(e => {
                    let i = new BookCategoryDto();
                    i.init(e);
                    if (i.children.length == 0){
                        i.isLeaf=false;
                    }
                });
            }
            array.push(item);
        });
        return array;
    }


    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["parent"] = this.parent;
        data["key"] = this.key;
        data["title"] = this.title;
        data["children"] = this.children;
        return data; 
    }

    clone(): BookCategoryDto {
        const json = this.toJSON();
        let result = new BookCategoryDto();
        result.init(json);
        return result;
    }
}

export interface IBookCategoryDto {
    parent:number | undefined;
    key: number | undefined;
    title:string | undefined;
    children:BookCategoryDto[] | undefined;
}