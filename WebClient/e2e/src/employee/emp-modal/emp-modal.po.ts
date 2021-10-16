import { browser, by, element, ElementArrayFinder, ElementFinder } from 'protractor';

export class EmployeeModalPage {
  async navigateTo(): Promise<unknown> {
    return browser.get('http://localhost:4200/employee');
  }

  // Add employee button
  getAddEmployeeButton(): ElementFinder {
    return element(by.css('.btn-float'));
  }

  getEditEmployeeButton(): ElementFinder {
    return element(by.css('.btn-green'));
  }

  getEditEmployeeCurrentButton(): any {
    const item = element.all(by.id('list')).get(1);
    item.element(by.css('.btn-green')).click();
  }

  // Employee name
  getEmployeeName(): ElementFinder {
    return element(by.id('name'));
  }

  async getEmployeeNameLabel(): Promise<string> {
    return (await this.getEmployeeName()).getText();
  }

  // Employee name input
  getEmployeeNameInput(): ElementFinder {
    return element(by.id('employeeName'));
  }

  async isEmployeeNameInputDisplayed(): Promise<ElementFinder> {
    return (await this.getEmployeeNameInput()).isDisplayed();
  }

  async getEmployeeNamePlaceholder(): Promise<ElementFinder> {
    return (await this.getEmployeeNameInput()).getAttribute('placeholder');
  }

  // Department label
  async getDepartmentLabel(): Promise<string> {
    return element(by.id('dep')).getText();
  }

  async getEmployeeNameInputDisplayed(): Promise<string> {
    return (await this.getEmployeeNameInput()).getAttribute('ng-reflect-model');
  }

  async getEmployeeNameInputValue(): Promise<string> {
    return (await this.getEmployeeNameInput()).getAttribute('value');
  }

  // Department option select.
  getDepartmentOptions(): ElementArrayFinder {
    return element.all(by.tagName('option'));
  }

  getOptionSelected(): ElementFinder {
    return element(by.id('department'));
  }

  async getOptionSelectDisplayed(): Promise<string> {
    return (await this.getOptionSelected()).getAttribute('ng-reflect-model');
  }

  async getOptionSelectValue(): Promise<string> {
    return (await this.getOptionSelected()).getAttribute('value');
  }

  // Date of joining title
  async getDateLabel(): Promise<string> {
    return element(by.id('date')).getText();
  }
}
