import { browser, logging } from 'protractor';
import { EmployeeListPage } from './emp-list.po';

describe('EmployeeListPage ', () => {
  let page: EmployeeListPage;

  beforeEach(() =>
    page = new EmployeeListPage());

  it('should display add employee button on page', async () => {
    await page.navigateTo();
    expect(await page.isAddEmpButtonDisplayed()).toBeTruthy('Add button is display');
  });

  it('should display add employee button name as `Add Employee`', async () => {
    await page.navigateTo();
    expect(await page.getAddEmpButtonTitle()).toEqual('Add Employee');
  });

  it('should display open modal window on page when click on add button', async () => {
    await page.navigateTo();
    await page.getAddEmployeeButton().click();
    await browser.sleep(1000);
    expect(await page.isModalDisplayed()).toBeTruthy('Modal window is open');
  });

  it('should display modal title as `Add Employee` on page when click on add button', async () => {
    await page.navigateTo();
    await page.getAddEmployeeButton().click();
    await browser.sleep(1000);
    expect(await page.getModalTitle()).toEqual('Add Employee');
  });

  it('should display modal close button on page when click on add employee button', async () => {
    await page.navigateTo();
    await page.getAddEmployeeButton().click();
    await browser.sleep(1000);
    expect(await page.isCloseModalButtonDisplayed()).toBeTruthy('Add button is display');
  });

  it('should close modal window on page when click on close button', async () => {
    await page.navigateTo();
    await page.getAddEmployeeButton().click();
    await browser.sleep(1000);
    await page.getCloseModalButton().click();
    await browser.sleep(1000);
    expect(await page.isModalDisplayed()).toBeFalsy('Modal window is close');
  });

  it('should display component modal window on page when click on add button', async () => {
    await page.navigateTo();
    await page.getAddEmployeeButton().click();
    await browser.sleep(1000);
    expect(await page.isComponentDisplayed()).toBeTruthy('Modal component is display');
  });
}
