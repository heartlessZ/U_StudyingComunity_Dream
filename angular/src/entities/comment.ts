export class CommentDto implements ICommentDto {
    id:number | undefined;
    author: string | undefined;
    userDetailId: string | undefined;
    avatar: string | undefined;
    content: string | undefined;
    children:CommentDto[] | undefined;
    parent:number | undefined;

    constructor(data?: ICommentDto) {
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
            this.author = data["author"];
            this.userDetailId = data["userDetailId"];
            this.avatar = data["avatar"];
            this.content = data["content"];
            this.children = data["children"];
        }
    }

    static fromJS(data: any): CommentDto {
        data = typeof data === 'object' ? data : {};
        let result = new CommentDto();
        result.init(data);
        return result;
    }

    static fromJSArray(dataArray: any[]): CommentDto[] {
        let array = [];
        dataArray.forEach(result => {
            let item = new CommentDto();
            item.init(result);
            if(item.children.length > 0)
            {
                item.children = this.fromJSArray(item.children);
            }
            array.push(item);
        });
        return array;
    }


    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["author"] = this.author;
        data["userDetailId"] = this.userDetailId;
        data["content"] = this.content;
        data["parent"] = this.parent;
        return data; 
    }

    clone(): CommentDto {
        const json = this.toJSON();
        let result = new CommentDto();
        result.init(json);
        return result;
    }
}

export interface ICommentDto {
    id:number | undefined;
    author: string | undefined;
    userDetailId: string | undefined;
    avatar: string | undefined;
    content: string | undefined;
    children:CommentDto[] | undefined;
}


export class CommentCreate{
    id:number | undefined;
    userDetailId: string | undefined;
    content: string | undefined;
    parent:number | undefined;
    articleId:number | undefined;
    author: string | undefined;
    

    constructor(data?: CommentCreate) {
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
            this.userDetailId = data["userDetailId"];
            this.content = data["content"];
        }
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["userDetailId"] = this.userDetailId;
        data["content"] = this.content;
        data["parent"] = this.parent;
        data["articleId"] = this.articleId;
        return data; 
    }

    clone(): CommentCreate {
        const json = this.toJSON();
        let result = new CommentCreate();
        result.init(json);
        return result;
    }
}