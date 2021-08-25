import { browser, logging } from 'protractor';
import { EmployeeListPage } from './emp-list.po';

describe('EmployeeListPage ', () => {
  let page: EmployeeListPage;

  beforeEach(() =>
    page = new EmployeeListPage());

  it('should display add employee button on page', async () => {
    await page.navigateTo();
    expect(await page.getAddEmpButtonDisplayed()).toBeTruthy('Add button is display');
  });

  it('should display add employee button title as `Add Employee`', async () => {
    await page.navigateTo();
    expect(await page.getAddEmpButtonTitle()).toEqual('Add Employee');
  });

  it('should display open modal window on page when click on add button', async () => {
    await page.navigateTo();
    await page.getAddEmployeeButton().click();
    await browser.sleep(1000);
    expect(await page.getModalDisplayed()).toBeTruthy('Modal window is open');
  });

  it('should display modal title on page as `Add Employee` when click on add button', async () => {
    await page.navigateTo();
    await page.getAddEmployeeButton().click();
    await browser.sleep(1000);
    expect(await page.getModalTitleText()).toEqual('Add Employee');
  });

  it('should display modal close button on page when click on add employee button', async () => {
    await page.navigateTo();
    await page.getAddEmployeeButton().click();
    await browser.sleep(1000);
    expect(await page.getCloseModalButtonDisplayed()).toBeTruthy('Add button is display');
  });

  it('should close modal window on page when click on close button', async () => {
    await page.navigateTo();
    await page.getAddEmployeeButton().click();
    await browser.sleep(1000);
    await page.getCloseModalButton().click();
    await browser.sleep(1000);
    expect(await page.getModalDisplayed()).toBeFalsy('Modal window is close');
  });

  it('should display component modal window on page when click on add button', async () => {
    await page.navigateTo();
    await page.getAddEmployeeButton().click();
    await browser.sleep(1000);
    expect(await page.getComponentDisplayed()).toBeTruthy('Modal component is display');
  });

  it('should display employee table on page', async () => {
    await page.navigateTo();
    expect(await page.getTableDisplayed()).toBeTruthy('Employee table is display');
  });

  it('should display employee list on page', async () => {
    await page.navigateTo();
    expect(await page.getListDisplayed()).toBeTruthy("Employee list is display");
  });

  it('should display table id filter on page', async () => {
    await page.navigateTo();
    expect(await page.getIdFilterDisplayed()).toBeTruthy('Id filter is display');
  });

  it('should display table id filter placeholder value as `Filter ID` on page', async () => {
    await page.navigateTo();
    expect(await page.getIdFilterPlaceholder()).toEqual('Filter ID');
  });

  it('should display table name filter on page', async () => {
    await page.navigateTo();
    expect(await page.getNameFilterDisplayed()).toBeTruthy('Name filter is display');
  });

  it('should display table name filter placeholder value as `Filter name` on page', async () => {
    await page.navigateTo();
    expect(await page.getNameFilterPlaceholder()).toEqual('Filter name');
  });

  it('should display table department filter on page', async () => {
    await page.navigateTo();
    expect(await page.getDepInputFilterDisplayed()).toBeTruthy('Department filter is display');
  });

  it('should display table department filter placeholder value as `Filter department` on page', async () => {
    await page.navigateTo();
    expect(await page.getDepartmentFilterPlaceholder()).toEqual('Filter department');
  });

  it('should display table date of joining filter on page', async () => {
    await page.navigateTo();
    expect(await page.getDateFilterDisplayed()).toBeTruthy('Date of joining filter is display');
  });

  it('should display table date of joining filter placeholder value as `Filter date` on page', async () => {
    await page.navigateTo();
    expect(await page.getDateFilterPlaceholder()).toEqual('Filter date');
  });

  it('should display table sort button on page', async () => {
    await page.navigateTo();
    expect(await page.getSortButton()).toBeTruthy('Sort button is display');
  });

  it('should display edit button on page', async () => {
    await page.navigateTo();
    expect(await page.getEditButtonDisplayed()).toBeTruthy('Edit button is display');
  });

  it('should display open edit modal on page when click on edit button', async () => {
    await page.navigateTo();
    await page.getEditButton().click();
    await browser.sleep(1000);
    expect(await page.getModalDisplayed()).toBeTruthy('Modal window is open');
  });

  afterEach(async () => {
    const logs = await browser.manage().logs().get(logging.Type.BROWSER);
    expect(logs).not.toContain(jasmine.objectContaining(
      {level: logging.Level.SEVERE} as logging.Entry))
  });
});

