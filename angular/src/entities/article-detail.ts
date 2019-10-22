export class ArticleDetailDto implements IArticleDetailDto {
    id:number | undefined;
    headline: string | undefined;
    content: string | undefined;
    praise: number | undefined;
    visitVolume: number | undefined;
    releaseStatus:number | undefined;
    userDetailId:string | undefined;
    categoryIds:number[] | undefined;

    constructor(data?: IArticleDetailDto) {
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
            this.headline = data["headline"];
            this.content = data["content"];
            this.praise = data["praise"];
            this.visitVolume = data["visitVolume"];
            this.releaseStatus = data["releaseStatus"];
            this.userDetailId = data["userDetailId"];
        }
    }

    static fromJS(data: any): ArticleDetailDto {
        data = typeof data === 'object' ? data : {};
        let result = new ArticleDetailDto();
        result.init(data);
        return result;
    }

    static fromJSArray(dataArray: any[]): ArticleDetailDto[] {
        let array = [];
        dataArray.forEach(result => {
            let item = new ArticleDetailDto();
            item.init(result);
            array.push(item);
        });
        return array;
    }


    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["headline"] = this.headline;
        data["content"] = this.content;
        data["praise"] = this.praise;
        data["visitVolume"] = this.visitVolume;
        data["releaseStatus"] = this.releaseStatus;
        data["userDetailId"] = this.userDetailId;
        data["categoryIds"] = this.categoryIds;
        return data; 
    }

    clone(): ArticleDetailDto {
        const json = this.toJSON();
        let result = new ArticleDetailDto();
        result.init(json);
        return result;
    }
}

export interface IArticleDetailDto {
    id:number | undefined;
    headline: string | undefined;
    content: string | undefined;
    praise: number | undefined;
    visitVolume: number | undefined;
    releaseStatus:number | undefined;
    userDetailId:string | undefined;
}