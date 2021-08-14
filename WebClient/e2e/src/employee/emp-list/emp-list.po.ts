import { browser, by, element, ElementFinder } from 'protractor';

export class EmployeeListPage {
  async navigateTo(): Promise<unknown> {
    return browser.get('http://localhost:4200/employee');
  }

  // Add employee button as trigger modal.
  getAddEmployeeButton(): ElementFinder {
    return element(by.css('.btn-float'));
  }

  async getAddEmpButtonDisplayed(): Promise<boolean> {
    return (await this.getAddEmployeeButton()).isDisplayed();
  }

  async getAddEmpButtonTitle(): Promise<string> {
    return (await this.getAddEmployeeButton()).getText();
  }

  // Modal window.
  getModal(): ElementFinder {
    return element(by.id('exampleModal'));
  }

  async getModalDisplayed(): Promise<boolean> {
    return (await this.getModal()).isDisplayed();
  }

  // Modal title.
  getModalTitle(): ElementFinder {
    return element(by.className('modal-title'));
  }

  async getModalTitleText(): Promise<string> {
    return (await this.getModalTitle()).getText();
  }
}
