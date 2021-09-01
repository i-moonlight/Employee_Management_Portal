import { browser, by, element, ElementFinder } from 'protractor';

export class EmployeeModalPage {
  async navigateTo(): Promise<unknown> {
    return browser.get('http://localhost:4200/employee');
  }

  // Add employee button.
  getAddEmployeeButton(): ElementFinder {
    return element(by.css('.btn-float'));
  }

  // Employee name.
  getEmployeeName(): ElementFinder {
    return element(by.id('name'));
  }

  async getEmployeeNameTitle(): Promise<string> {
    return (await this.getEmployeeName()).getText();
  }

  // Employee name input.
  getEmployeeNameInput(): ElementFinder {
    return element(by.id('employeeName'));
  }

  async getEmployeeNameInputDisplayed(): Promise<ElementFinder> {
    return (await this.getEmployeeNameInput()).isDisplayed();
  }

  async getEmployeeNamePlaceholder(): Promise<ElementFinder> {
    return (await this.getEmployeeNameInput()).getAttribute('placeholder');
  }

  // Department title.
  getDepartment(): ElementFinder {
    return element(by.id('dep'));
  }

  async getDepartmentTitle(): Promise<string> {
    return (await this.getDepartment()).getText();
  }
}
