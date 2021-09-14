import { browser, by, element, ElementFinder } from 'protractor';

export class DepartmentListPage {
  async navigateTo(): Promise<unknown> {
    return browser.get('http://localhost:4200/department');
  }

  // Add department button as trigger modal.
  getAddDepButton(): ElementFinder {
    return element(by.css('.btn-float'));
  }

  async isAddDepButtonDisplayed(): Promise<boolean> {
    return (await this.getAddDepButton()).isDisplayed();
  }
}
