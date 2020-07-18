export class ProjectDto implements IProjectDto {

    static fromJS(data: any): ProjectDto {
        data = typeof data === 'object' ? data : {};
        const result = new ProjectDto();
        result.init(data);
        if (result.childProject != undefined) {
            ProjectDto.fromJS(result.childProject);
        }
        return result;
    }

    static fromJSArray(dataArray: any[]): ProjectDto[] {
        const array = [];
        dataArray.forEach(result => {
            const item = new ProjectDto();
            item.init(result);
            array.push(item);
        });
        return array;
    }
    id: number | undefined;
    name: string | undefined;
    progress: number | undefined;
    parent: number | undefined;
    remark: number | undefined;
    expirationTime: Date|undefined;
    childProject: ProjectDto|undefined;


    constructor(data?: IProjectDto) {
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
            this.progress = data['progress'];
            this.parent = data['parent'];
            this.remark = data['remark'];
            this.expirationTime = data['expirationTime'];
            this.childProject = data['childProject'];
        }
    }


    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data['id'] = this.id;
        data['name'] = this.name;
        data['progress'] = this.progress;
        data['parent'] = this.parent;
        data['remark'] = this.remark;
        data['expirationTime'] = this.expirationTime;
        return data;
    }

    clone(): ProjectDto {
        const json = this.toJSON();
        const result = new ProjectDto();
        result.init(json);
        return result;
    }
}

export interface IProjectDto {
    id: number | undefined;
    name: string | undefined;
    progress: number | undefined;
    parent: number | undefined;
    remark: number | undefined;
    expirationTime: Date|undefined;
    childProject: ProjectDto|undefined;
}
