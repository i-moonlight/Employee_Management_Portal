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

  afterEach(async () => {
    const logs = await browser.manage().logs().get(logging.Type.BROWSER);
    expect(logs).not.toContain(jasmine.objectContaining(
      {level: logging.Level.SEVERE} as logging.Entry))
  });
});
