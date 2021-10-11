import { browser, by, element, ElementFinder } from 'protractor';

export class EmployeeListPage {
  async navigateTo(): Promise<unknown> {
    return browser.get('http://localhost:4200/employee');
  }

  // Add employee button as trigger modal.
  getAddEmployeeButton(): ElementFinder {
    return element(by.css('.btn-float'));
  }

  async isAddEmpButtonDisplayed(): Promise<boolean> {
    return (await this.getAddEmployeeButton()).isDisplayed();
  }

  async getAddEmpButtonTitle(): Promise<string> {
    return (await this.getAddEmployeeButton()).getText();
  }

  // Modal window.
  async isModalDisplayed(): Promise<boolean> {
    return element(by.id('exampleModal')).isDisplayed();
  }

  // Modal title.
  async getModalTitle(): Promise<string> {
    return element(by.className('modal-title')).getText();
  }

  // Close modal button.
  getCloseModalButton(): ElementFinder {
    return element(by.className('btn-close'));
  }

  async isCloseModalButtonDisplayed(): Promise<boolean> {
    return (await this.getCloseModalButton()).isDisplayed();
  }

  // Modal component
  async isComponentDisplayed(): Promise<boolean> {
    return element(by.tagName('app-emp-modal')).isDisplayed();
  }
}
