export class BookResourceDto implements IBookResourceDto {

    static fromJS(data: any): BookResourceDto {
        data = typeof data === 'object' ? data : {};
        const result = new BookResourceDto();
        result.init(data);
        return result;
    }

    static fromJSArray(dataArray: any[]): BookResourceDto[] {
        const array = [];
        dataArray.forEach(result => {
            const item = new BookResourceDto();
            item.init(result);
            array.push(item);
        });
        return array;
    }
    id: number | undefined;
    bookId: number | undefined;
    name: string | undefined;
    url: string | undefined;
    status: any|undefined;
    error: string | undefined;
    uploaderName: string | undefined;
    auditorName: string | undefined;
    uploader: string|undefined;
    auditor: string|undefined;

    constructor(data?: IBookResourceDto) {
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
            this.bookId = data['bookId'];
            this.url = data['url'];
            this.status = data['status'];
            this.uploaderName = data['uploaderName'];
            this.auditorName = data['auditorName'];
            this.uploader = data['uploader'];
            this.auditor = data['auditor'];
        }
    }


    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data['id'] = this.id;
        data['name'] = this.name;
        data['bookId'] = this.bookId;
        data['url'] = this.url;
        data['status'] = this.status;
        data['uploader'] = this.uploader;
        data['auditor'] = this.auditor;
        return data;
    }

    clone(): BookResourceDto {
        const json = this.toJSON();
        const result = new BookResourceDto();
        result.init(json);
        return result;
    }
}

export interface IBookResourceDto {
    id: number | undefined;
    bookId: number | undefined;
    name: string | undefined;
    url: string | undefined;
    status: any|undefined;
    error: string | undefined;
}
