import { browser, by, element, ElementArrayFinder, ElementFinder } from 'protractor';

export class EmployeeModalPage {
  async navigateTo(): Promise<unknown> {
    return browser.get('http://localhost:4200/employee');
  }

  // Add employee button.
  getAddEmployeeButton(): ElementFinder {
    return element(by.css('.btn-float'));
  }

  getEditEmployeeButton(): ElementFinder {
    return element(by.css('.btn-green'));
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

  // Department option select.
  getDepartmentOptions(): ElementArrayFinder {
    return element.all(by.tagName('option'));
  }

  getOptionSelected(): ElementFinder {
    return element(by.id('department'));
  }

  async isOptionSelectDisplayed(): Promise<boolean> {
    return (await this.getOptionSelected()).getAttribute('ng-reflect-model').isDisplayed();
  }

  // Date of joining title.
  async getDateTitle(): Promise<string> {
    return element(by.id('date')).getText();
  }

  // Date of joining input.
  getDateSelected(): ElementFinder {
    return element(by.id('dateOfJoining'));
  }

  async isDateInputDisplayed(): Promise<boolean> {
    return (await this.getDateSelected()).getAttribute('ng-reflect-model').isDisplayed();
  }

  // Photo file.
  getPhoto(): ElementFinder {
    return element(by.className('photo'));
  }

  async isPhotoDisplayed(): Promise<boolean> {
    return (await this.getPhoto()).isDisplayed();
  }

  async isPhotoPathPresent(): Promise<boolean> {
    return (await this.getPhoto()).getAttribute('src').isPresent();
  }

  // Photo file input.
  getPhotoFileInput(): ElementFinder {
    return element(by.className('input'));
  }

  async isPhotoFileInputDisplayed(): Promise<boolean> {
    return (await this.getPhotoFileInput()).isDisplayed();
  }

  // Employee add button.
  getAddButton(): ElementFinder {
    return element(by.css('.add'));
  }

  async isAddButtonDisplayed(): Promise<boolean> {
    return (await this.getAddButton()).isDisplayed();
  }
}
