import { browser, element, by } from 'protractor';

export class U_StudyingCommunity_DreamTemplatePage {
  navigateTo() {
    return browser.get('/');
  }

  getParagraphText() {
    return element(by.css('app-root h1')).getText();
  }
}
