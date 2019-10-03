import { U_StudyingCommunity_DreamTemplatePage } from './app.po';

describe('U_StudyingCommunity_Dream App', function() {
  let page: U_StudyingCommunity_DreamTemplatePage;

  beforeEach(() => {
    page = new U_StudyingCommunity_DreamTemplatePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
