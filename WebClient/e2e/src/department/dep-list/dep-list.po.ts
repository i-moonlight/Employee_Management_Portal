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

  async getAddDepartmentButtonName(): Promise<string> {
    return (await this.getAddDepButton()).getText();
  }

  // Modal window.
  getModal(): ElementFinder {
    return element(by.id('exampleModal'));
  }

  async isModalDisplayed(): Promise<boolean> {
    return (await this.getModal()).isDisplayed();
  }

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
}
