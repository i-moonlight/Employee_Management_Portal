import { browser, logging } from 'protractor';
import { EmployeeModalPage } from './emp-modal.po';

describe('EmployeeModalPage', () => {
  let page: EmployeeModalPage;

  beforeEach(() =>
    page = new EmployeeModalPage());

  it('should display employee name label as `Employee Name` on page when click on add employee button', async () => {
    await page.navigateTo();
    await page.getAddEmployeeButton().click();
    await browser.sleep(1000);
    expect(await page.getEmployeeNameLabel()).toEqual('Employee Name');
  });

  it('should display employee name input form on page when click on add employee button', async () => {
    await page.navigateTo();
    await page.getAddEmployeeButton().click();
    await browser.sleep(1000);
    expect(await page.isEmployeeNameInputDisplayed()).toBeTruthy('Employee name input is display');
  });

  it('should display employee name placeholder as `Enter Employee Name` on page when click on add employee button', async () => {
    await page.navigateTo();
    await page.getAddEmployeeButton().click();
    await browser.sleep(1000);
    expect(await page.getEmployeeNamePlaceholder()).toEqual('Enter Employee Name');
  });

  it('should display department name select label as `Department` on page when click on add employee button', async () => {
    await page.navigateTo();
    await page.getAddEmployeeButton().click();
    await browser.sleep(1000);
    expect(await page.getDepartmentLabel()).toEqual('Department');
  });

  it('should display selected option default value as `--Select--` when click on add employee button', async () => {
    await page.navigateTo();
    await page.getAddEmployeeButton().click();
    await browser.sleep(1000);
    expect(await page.getDepartmentOptions().first().getText()).toEqual('--Select--');
  });

 it('should display selected department on form when option selected when click on add employee button', async () => {
    await page.navigateTo();
    await page.getAddEmployeeButton().click();
    await browser.sleep(1000);
    await page.getDepartmentOptions().then((options) => options[1].click());
    await browser.sleep(1000);
    expect(await page.getOptionSelectDisplayed()).toEqual(await page.getOptionSelectValue());
  });

  it('should display date of joining label as `Date of joining` on page when click on add employee button', async () => {
    await page.navigateTo();
    await page.getAddEmployeeButton().click();
    await browser.sleep(1000);
    expect(await page.getDateLabel()).toEqual('Date of joining');
  });

  it('should display date of joining input form on page when click on add employee button', async () => {
    await page.navigateTo();
    await page.getAddEmployeeButton().click();
    await browser.sleep(1000);
    expect(await page.isDateInputDisplayed()).toBeTruthy('Date of joining input is display');
  });

  it('should display photo on page when click on add employee button', async () => {
    await page.navigateTo();
    await page.getAddEmployeeButton().click();
    await browser.sleep(1000);
    expect(await page.isPhotoDisplayed()).toBeTruthy('Photo is display');
  });

  it('should display photo file input on page when click on add employee button', async () => {
    await page.navigateTo();
    await page.getAddEmployeeButton().click();
    await browser.sleep(1000);
    expect(await page.isPhotoFileInputDisplayed()).toBeTruthy('Photo input is display');
  });

  it('should display add button on page when click on add employee button', async () => {
    await page.navigateTo();
    await page.getAddEmployeeButton().click();
    await browser.sleep(1000);
    expect(await page.isAddButtonDisplayed()).toBeTruthy('Add button is display');
  });

  it('should display employee name label as `Employee Name` on page when click on edit employee button', async () => {
    await page.navigateTo();
    await page.getEditEmployeeButton().click();
    await browser.sleep(1000);
    expect(await page.getEmployeeNameLabel()).toEqual('Employee Name');
  });

  it('should display employee name as input value on page when click on edit employee button', async () => {
    await page.navigateTo();
    await page.getEditEmployeeCurrentButton();
    await browser.sleep(1000);
    expect(await page.getEmployeeNameInputDisplayed()).toEqual(await page.getEmployeeNameInputValue());
  });

  it('should display photo on page when click on edit employee button', async () => {
    await page.navigateTo();
    await page.getEditEmployeeButton().click();
    await browser.sleep(1000);
    expect(await page.isPhotoDisplayed()).toBeTruthy('Photo is display');
  });

  it('should display photo path on page when click on edit employee button', async () => {
    await page.navigateTo();
    await page.getEditEmployeeButton().click();
    await browser.sleep(1000);
    expect(await page.isPhotoPathPresent()).toBeTruthy('Photo file path is display');
  });

  it('should display photo file input on page when click on edit employee button', async () => {
    await page.navigateTo();
    await page.getEditEmployeeButton().click();
    await browser.sleep(1000);
    expect(await page.isPhotoFileInputDisplayed()).toBeTruthy('Photo input is display');
  });

  it('should display add button is disable on page when click on add employee button', async () => {
    await page.navigateTo();
    await page.getAddEmployeeButton().click();
    await browser.sleep(1000);
    expect(await page.isAddButtonDisabled()).toBeTruthy('Add button is disable');
  });

  it('should display add button name as `ADD` on page when click on add employee button', async () => {
    await page.navigateTo();
    await page.getAddEmployeeButton().click();
    await browser.sleep(1000);
    expect(await page.getAddButton().getText()).toEqual('ADD');
  });

  it('should display update employee modal button on page when click on update button', async () => {
    await page.navigateTo();
    await page.getEditEmployeeButton().click();
    await browser.sleep(1000);
    expect(await page.isUpdateButtonDisplayed()).toBeTruthy('Update button is display');
  });

  afterEach(async () => {
    const logs = await browser.manage().logs().get(logging.Type.BROWSER);
    expect(logs).not.toContain(jasmine.objectContaining(
      {level: logging.Level.SEVERE} as logging.Entry))
  });
});

