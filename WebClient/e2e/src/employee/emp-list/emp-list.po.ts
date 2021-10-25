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

  // Modal component.
  async isComponentDisplayed(): Promise<boolean> {
    return element(by.tagName('app-emp-modal')).isDisplayed();
  }

  // Employee table.
  async getTableDisplayed(): Promise<boolean> {
    return element(by.tagName('table')).isDisplayed();
  }

  // Employee list.
  async isListDisplayed(): Promise<boolean> {
    return element(by.tagName('table tbody tr')).isDisplayed();
  }

  // Employees table columns.
  async getTableColumnName(): Promise<string> {
    return element.all(by.tagName('th')).getText();
  }

  // Table Id filter.
  getIdFilter(): ElementFinder {
    return element(by.id('idFilter'));
  }

  async isIdFilterDisplayed(): Promise<boolean> {
    return (await this.getIdFilter()).isDisplayed();
  }

  async getIdFilterPlaceholder(): Promise<ElementFinder> {
    return (await this.getIdFilter()).getAttribute('placeholder');
  }

  // Table name filter.
  getNameFilter(): ElementFinder {
    return element(by.id('nameFilter'));
  }

  async isNameFilterDisplayed(): Promise<boolean> {
    return (await this.getNameFilter()).isDisplayed();
  }

  async getNameFilterPlaceholder(): Promise<ElementFinder> {
    return (await this.getNameFilter()).getAttribute('placeholder');
  }

  // Table department filter.
  getDepartmentFilter(): ElementFinder {
    return element(by.id('departmentFilter'));
  }

  async isDepInputFilterDisplayed(): Promise<boolean> {
    return (await this.getDepartmentFilter()).isDisplayed();
  }

  async getDepartmentFilterPlaceholder(): Promise<ElementFinder> {
    return (await this.getDepartmentFilter()).getAttribute('placeholder');
  }

  // Table date filter.
  getDateFilter(): ElementFinder {
    return element(by.id('dateFilter'));
  }

  async isDateFilterDisplayed(): Promise<boolean> {
    return (await this.getDateFilter()).isDisplayed();
  }

  async getDateFilterPlaceholder(): Promise<ElementFinder> {
    return (await this.getDateFilter()).getAttribute('placeholder');
  }

  // Table sort button.
  async isSortButton(): Promise<boolean> {
    return element(by.css('.sort')).isDisplayed();
  }

  // Edit employee button as trigger modal.
  getEditButton(): ElementFinder {
    return element(by.css('.btn-green'));
  }

  async isEditButtonDisplayed(): Promise<boolean> {
    return (await this.getEditButton()).isDisplayed();
  }

  // Delete alert button.
  getDeleteAlertButton(): ElementFinder {
    return element(by.css('.btn-red'));
  }

  async isDeleteAlertButtonDisplayed(): Promise<boolean> {
    return (await this.getDeleteAlertButton()).isDisplayed();
  }
}
