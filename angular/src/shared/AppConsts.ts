export class AppConsts {

    static remoteServiceBaseUrl: string;
    static appBaseUrl: string;
    static appBaseHref: string; // returns angular's base-href parameter value if used during the publish

    static getFileUploadUrl(): string {
        return this.appBaseUrl + '/api/File/upload';
    }

    static localeMappings: any = [];

    static readonly userManagement = {
        defaultAdminUserName: 'admin'
    };

    static readonly localization = {
        defaultLocalizationSourceName: 'U_StudyingCommunity_Dream'
    };

    static readonly authorization = {
        encryptedAuthTokenName: 'enc_auth_token'
    };
    /**
   * 数据表格设置
   */
  // tslint:disable-next-line:member-ordering
  static readonly grid = {
    /**
     * 每页显示条目数
     */
    defaultPageSize: 10,
    /**
     * 每页显示条目数下拉框值
     */
    defaultPageSizes: [5, 10, 15, 20, 25, 30, 50, 80, 100],
  };
}
