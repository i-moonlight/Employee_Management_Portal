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

  // Close modal button.
  getCloseModalButton(): ElementFinder {
    return element(by.className('btn-close'));
  }

  async getCloseModalButtonDisplayed(): Promise<boolean> {
    return (await this.getCloseModalButton()).isDisplayed();
  }

  // Modal component.
  async getComponentDisplayed(): Promise<boolean> {
    return element(by.tagName('app-emp-modal')).isDisplayed();
  }

  // Employee table.
  getTable(): ElementFinder {
    return element(by.tagName('table'));
  }

  async getTableDisplayed(): Promise<boolean> {
    return (await this.getTable()).isDisplayed();
  }

  // Employee list.
  async getListDisplayed(): Promise<boolean> {
    return element(by.css('app-show-emp tbody tr')).isDisplayed();
  }

  // Table Id filter.
  getIdFilter(): ElementFinder {
    return element(by.id('idFilter'));
  }

  async getIdFilterDisplayed(): Promise<boolean> {
    return (await this.getIdFilter()).isDisplayed();
  }

  async getIdFilterPlaceholder(): Promise<ElementFinder> {
    return (await this.getIdFilter()).getAttribute('placeholder');
  }

  // Table name filter.
  getNameFilter(): ElementFinder {
    return element(by.id('nameFilter'));
  }

  async getNameFilterDisplayed(): Promise<boolean> {
    return (await this.getNameFilter()).isDisplayed();
  }

  async getNameFilterPlaceholder(): Promise<ElementFinder> {
    return (await this.getNameFilter()).getAttribute('placeholder');
  }

  // Table department filter.
  getDepartmentFilter(): ElementFinder {
    return element(by.id('departmentFilter'));
  }

  async getDepInputFilterDisplayed(): Promise<boolean> {
    return (await this.getDepartmentFilter()).isDisplayed();
  }

  async getDepartmentFilterPlaceholder(): Promise<ElementFinder> {
    return (await this.getDepartmentFilter()).getAttribute('placeholder');
  }
}
