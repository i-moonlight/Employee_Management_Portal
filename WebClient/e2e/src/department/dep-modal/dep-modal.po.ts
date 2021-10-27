import { browser, by, element, ElementFinder } from 'protractor';

export class DepartmentModalPage {
  async navigateTo(): Promise<unknown> {
    return browser.get('http://localhost:4200/department');
  }

  // Add department button
  getAddDepartmentButton(): ElementFinder {
    return element(by.css('.btn-float'));
  }

  // Add department label
  async getAddDepartmentLabel(): Promise<string> {
    return element(by.css('.col-form-label')).getText();
  }
  // Edit department button
  getEditDepartmentButton(): ElementFinder {
    return element(by.css('.btn-green'));
  }

  // Department name input
  getDepartmentNameInput(): ElementFinder {
    return element(by.css('.form-control'));
  }

  async isDepartmentNameInputDisplayed(): Promise<boolean> {
    return (await this.getDepartmentNameInput()).isDisplayed();
  }

  async getDepartmentNameInputPlaceholder(): Promise<string> {
    return (await this.getDepartmentNameInput()).getAttribute('placeholder');
  }

  // Add department modal button
  getAddDepartmentModalButton(): ElementFinder {
    return element(by.id('addButton'));
  }

  async isAddModalButtonDisplayed(): Promise<boolean> {
    return (await this.getAddDepartmentModalButton()).isDisplayed();
  }

  async getAddDepModalLabel(): Promise<string> {
    return (await this.getAddDepartmentModalButton()).getText();
  }

  // Update department modal button
  getUpdateDepartmentModalButton(): ElementFinder {
    return element(by.id('updateButton'));
  }

  async isUpdateDepartmentButtonDisplayed(): Promise<boolean> {
    return (await this.getUpdateDepartmentModalButton()).isDisplayed();
  }

  async getUpdateDepartmentButtonName(): Promise<string> {
    return (await this.getUpdateDepartmentModalButton()).getText();
  }
}
