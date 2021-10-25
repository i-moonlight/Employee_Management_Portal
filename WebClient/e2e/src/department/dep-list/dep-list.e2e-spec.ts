import { DepartmentListPage } from './dep-list.po';
import { browser, logging } from 'protractor';

describe('DepartmentListPage', () => {
  let page: DepartmentListPage;

  beforeEach(() =>
    page = new DepartmentListPage());

  it('should display add department button on page', async () => {
    await page.navigateTo();
    expect(await page.isAddDepButtonDisplayed()).toBeTruthy('Add button is display');
  });

  it('should display add department button name as `Add Department` on page', async () => {
    await page.navigateTo();
    expect(await page.getAddDepartmentButtonName()).toEqual('Add Department');
  });

  it('should display modal window on page when click on add department button', async () => {
    await page.navigateTo();
    await page.getAddDepButton().click();
    await browser.sleep(1000);
    expect(await page.isModalDisplayed()).toBeTruthy('Modal window is open');
  });

  it('should display modal title as `Add Department` on page when click on add department button', async () => {
    await page.navigateTo();
    await page.getAddDepButton().click();
    await browser.sleep(1000);
    expect(await page.getModalTitle()).toEqual('Add Department');
  });

  it('should display close modal button when click on add department button', async () => {
    await page.navigateTo();
    await page.getAddDepButton().click();
    await browser.sleep(1000);
    expect(await page.isCloseModalButtonDisplayed()).toBeTruthy('Close button is display');
  });

  it('should close modal window when click on close button', async () => {
    await page.navigateTo();
    await page.getAddDepButton().click();
    await browser.sleep(1000);
    await page.getCloseModalButton().click();
    await browser.sleep(1000);
    expect(await page.isModalDisplayed()).toBeFalsy('Modal window is close');
  });

  it('should display modal component when click on add department button', async () => {
    await page.navigateTo();
    await page.getAddDepButton().click();
    await browser.sleep(1000);
    expect(await page.isModalComponentDisplayed()).toBeTruthy('Modal component is display');
  });

  it('should display departments table on page', async () => {
    await page.navigateTo();
    expect(await page.isDepartmentsTableDisplayed()).toBeTruthy('Departments table is display');
  });

  it('should display departments list on page', async () => {
    await page.navigateTo();
    expect(await page.isListDisplayed()).toBeTruthy("Employees list is display");
  });

  it('should display department id column name as `ID` on page', async () => {
    await page.navigateTo();
    expect(await page.getTableColumnName().then((columns) => columns[0])).toEqual('ID');
  });

  it('should display department column name as `Department` on page', async () => {
    await page.navigateTo();
    expect(await page.getTableColumnName().then((columns) => columns[1])).toEqual('Department');
  });

  it('should display department table id filter on page', async () => {
    await page.navigateTo();
    expect(await page.isIdFilterDisplayed()).toBeTruthy('Id filter is display');
  });

  it('should display table id filter placeholder as `Filter ID` on page', async () => {
    await page.navigateTo();
    expect(await page.getFilterIdPlaceholder()).toEqual('Filter ID');
  });

  it('should display department table name filter on page', async () => {
    await page.navigateTo();
    expect(await page.isNameFilterDisplayed()).toBeTruthy('Name filter is display');
  });

  it('should display table name filter placeholder as `Filter Name` on page', async () => {
    await page.navigateTo();
    expect(await page.getNameFilterPlaceholder()).toEqual('Filter Name');
  });

  it('should display department table sort button on page', async () => {
    await page.navigateTo();
    expect(await page.isSortButton()).toBeTruthy('Sort button is display');
  });

  it('should display department edit button on page', async () => {
    await page.navigateTo();
    expect(await page.isEditEmpButtonDisplayed()).toBeTruthy('Edit button is display');
  });

  it('should display modal window when click on edit button', async () => {
    await page.navigateTo();
    await page.getEditEmpButton().click();
    await browser.sleep(1000);
    expect(await page.isModalDisplayed()).toBeTruthy('Modal window is open');
  });

  it('should display modal title as `Edit Department` on page when click on edit button', async () => {
    await page.navigateTo();
    await page.getEditEmpButton().click();
    await browser.sleep(1000);
    expect(await page.getModalTitle()).toEqual('Edit Department');
  });

  it('should display delete department alert button on page', async () => {
    await page.navigateTo();
    expect(await page.isDeleteAlertButtonDisplayed()).toBeTruthy('Delete alert button is display');
  });

  it('should display alert dialog when click on delete department alert button', async () => {
    await page.navigateTo();
    await page.getDeleteAlertButton().click();
    await expect(browser.switchTo().alert()).toBeTruthy('Alert dialog is display');
    await browser.restart();
  });

  it('should display alert dialog title `Are you sure??` when click on delete department alert button', async () => {
    await page.navigateTo();
    await page.getDeleteAlertButton().click();
    await expect(browser.switchTo().alert().getText()).toContain('Are you sure??');
    await browser.restart();
  });

  afterEach(async () => {
    const logs = await browser.manage().logs().get(logging.Type.BROWSER);
    expect(logs).not.toContain(jasmine.objectContaining(
      {level: logging.Level.SEVERE} as logging.Entry));
  });
})
