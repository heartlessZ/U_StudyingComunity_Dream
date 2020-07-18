export class UserProjectDto implements IUserProjectDto {

    static fromJS(data: any): UserProjectDto {
        data = typeof data === 'object' ? data : {};
        const result = new UserProjectDto();
        result.init(data);
        return result;
    }

    static fromJSArray(dataArray: any[]): UserProjectDto[] {
        const array = [];
        dataArray.forEach(result => {
            const item = new UserProjectDto();
            item.init(result);
            array.push(item);
        });
        return array;
    }
    id: number | undefined;
    userId: string | undefined;
    projectId: number | undefined;
    tagName: string | undefined;
    isPublic: boolean|undefined;
    creationTime: Date|undefined;
    praise: number|undefined;
    progress: number|undefined;


    constructor(data?: IUserProjectDto) {
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
            this.userId = data['userId'];
            this.isPublic = data['isPublic'];
            this.projectId = data['projectId'];
            this.praise = data['praise'];
            this.tagName = data['tagName'];
            this.creationTime = data['creationTime'];
            this.progress = data['progress'];
        }
    }


    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data['id'] = this.id;
        data['userId'] = this.userId;
        data['isPublic'] = this.isPublic;
        data['projectId'] = this.projectId;
        data['praise'] = this.praise;
        data['tagName'] = this.tagName;
        data['creationTime'] = this.creationTime;
        return data;
    }

    clone(): UserProjectDto {
        const json = this.toJSON();
        const result = new UserProjectDto();
        result.init(json);
        return result;
    }
}

export interface IUserProjectDto {
    id: number | undefined;
    userId: string | undefined;
    projectId: number | undefined;
    tagName: string | undefined;
    isPublic: boolean|undefined;
    creationTime: Date|undefined;
    praise: number|undefined;
    progress: number|undefined;
}
