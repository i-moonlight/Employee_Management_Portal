import { browser, by, element, ElementFinder } from 'protractor';

export class DepartmentModalPage {
  async navigateTo(): Promise<unknown> {
    return browser.get('http://localhost:4200/department');
  }

  // Add department button.
  getAddDepartmentButton(): ElementFinder {
    return element(by.css('.btn-float'));
  }

  // Add department label.
  async getAddDepartmentLabel(): Promise<string> {
    return element(by.css('.col-form-label')).getText();
  }
}
