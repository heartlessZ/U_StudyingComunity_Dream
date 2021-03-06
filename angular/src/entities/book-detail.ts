export class BookDetailDto implements IBookDetailDto {

    static fromJS(data: any): BookDetailDto {
        data = typeof data === 'object' ? data : {};
        const result = new BookDetailDto();
        result.init(data);
        return result;
    }

    static fromJSArray(dataArray: any[]): BookDetailDto[] {
        const array = [];
        dataArray.forEach(result => {
            const item = new BookDetailDto();
            item.init(result);
            array.push(item);
        });
        return array;
    }
    id: number | undefined;
    name: string | undefined;
    author: string | undefined;
    description: string | undefined;
    coverUrl: string | undefined;
    otherUrls: string | undefined;
    categoryId: number | undefined;
    categoryName: string | undefined;
    status: any | undefined;
    birthday: Date | undefined;
    praise: number | undefined;

    constructor(data?: IBookDetailDto) {
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
            this.id = data['id'];
            this.name = data['name'];
            this.author = data['author'];
            this.description = data['description'];
            this.coverUrl = data['coverUrl'];
            this.otherUrls = data['otherUrls'];
            this.categoryId = data['categoryId'];
            this.status = data['status'];
            this.birthday = data['birthday'];
            this.praise = data['praise'];
            this.categoryName = data['categoryName'];
        }
    }


    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data['id'] = this.id;
        data['name'] = this.name;
        data['author'] = this.author;
        data['description'] = this.description;
        data['coverUrl'] = this.coverUrl;
        data['otherUrls'] = this.otherUrls;
        data['categoryId'] = this.categoryId;
        data['status'] = this.status;
        data['birthday'] = this.birthday;
        return data;
    }

    clone(): BookDetailDto {
        const json = this.toJSON();
        const result = new BookDetailDto();
        result.init(json);
        return result;
    }
}

export interface IBookDetailDto {
    id: number | undefined;
    name: string | undefined;
    author: string | undefined;
    description: string | undefined;
    coverUrl: string | undefined;
    otherUrls: string | undefined;
    categoryId: number | undefined;
    status: any | undefined;
    birthday: Date | undefined;
}
