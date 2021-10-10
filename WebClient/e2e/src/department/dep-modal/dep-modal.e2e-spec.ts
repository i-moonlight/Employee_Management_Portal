import {browser, logging} from 'protractor';
import { DepartmentModalPage } from './dep-modal.po';

describe('DepartmentModalPage', () => {
  let page: DepartmentModalPage;

  beforeEach(() =>
    page = new DepartmentModalPage());

  it('should display department name label as `Department Name` on page', async () => {
    await page.navigateTo();
    await page.getAddDepartmentButton().click();
    await browser.sleep(1000);
    expect(await page.getAddDepartmentLabel()).toEqual('Department Name');
  });

  it('should display department name input on page', async () => {
    await page.navigateTo();
    await page.getAddDepartmentButton().click();
    await browser.sleep(1000);
    expect(await page.isDepartmentNameInputDisplayed()).toBeTruthy('Department name input is display');
  });

  it('should display department name placeholder as `Enter Department Name` on page', async () => {
    await page.navigateTo();
    await page.getAddDepartmentButton().click();
    await browser.sleep(1000);
    expect(await page.getDepNameInputPlaceholder()).toEqual('Enter Department Name');
  });

  it('should display add department modal button on page when click on button', async () => {
    await page.navigateTo();
    await page.getAddDepartmentButton().click();
    await browser.sleep(1000);
    expect(await page.isAddDepModalButtonDisplayed()).toBeTruthy('Add department modal button is display');
  });

  it('should display add department modal button label as `ADD` on page when click on button', async () => {
    await page.navigateTo();
    await page.getAddDepartmentButton().click();
    await browser.sleep(1000);
    expect(await page.getAddDepModalLabel()).toEqual('ADD');
  });

  it('should display department name label as `Department Name` on page when click on edit button', async () => {
    await page.navigateTo();
    await page.getEditDepartmentButton().click();
    await browser.sleep(1000);
    expect(await page.getAddDepartmentLabel()).toEqual('Department Name');
  });

  it('should display department name input on page when click on edit button', async () => {
    await page.navigateTo();
    await page.getEditDepartmentButton().click();
    await browser.sleep(1000);
    expect(await page.isDepartmentNameInputDisplayed()).toBeTruthy('Department name input is display');
  });

  it('should display department name placeholder as `Enter Department Name` on page when click on edit button', async () => {
    await page.navigateTo();
    await page.getEditDepartmentButton().click();
    await browser.sleep(1000);
    expect(await page.getDepartmentNameInputPlaceholder()).toEqual('Enter Department Name');
  });

  afterEach(async () => {
    const logs = await browser.manage().logs().get(logging.Type.BROWSER);
    expect(logs).not.toContain(jasmine.objectContaining(
      {level: logging.Level.SEVERE} as logging.Entry))
  });
});
