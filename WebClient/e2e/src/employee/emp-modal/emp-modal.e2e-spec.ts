import { browser, logging } from 'protractor';
import { EmployeeModalPage } from './emp-modal.po';

describe('EmployeeModalPage', () => {
  let page: EmployeeModalPage;

  beforeEach(() =>
    page = new EmployeeModalPage());

  it('should display employee name title as `Employee Name` on page when click on button', async () => {
    await page.navigateTo();
    await page.getAddEmployeeButton().click();
    await browser.sleep(1000);
    expect(await page.getEmployeeNameTitle()).toEqual('Employee Name');
  });

  it('should display employee name input form on page', async () => {
    await page.navigateTo();
    await page.getAddEmployeeButton().click();
    await browser.sleep(1000);
    expect(await page.getEmployeeNameInputDisplayed()).toBeTruthy('Employee name input is display');
  });

  it('should display employee name placeholder value as `Enter Employee Name` on page', async () => {
    await page.navigateTo();
    await page.getAddEmployeeButton().click();
    await browser.sleep(1000);
    expect(await page.getEmployeeNamePlaceholder()).toEqual('Enter Employee Name');
  });

  it('should display department name select on modal window', async () => {
    await page.navigateTo();
    await page.getAddEmployeeButton().click();
    await browser.sleep(1000);
    expect(await page.getDepartmentTitle()).toEqual('Department');
  });

  it('should display selected department on select form when option selected', async () => {
    await page.navigateTo();
    await page.getAddEmployeeButton().click();
    await browser.sleep(1000);
    await page.getDepartmentOptions().then((options) => options[1].click());
    await browser.sleep(1000);
    expect(await page.isOptionSelectDisplayed()).toBeTruthy('Selected department is display');
  });

  it('should display date of joining title text as `Date` on page', async () => {
    await page.navigateTo();
    await page.getAddEmployeeButton().click();
    await browser.sleep(1000);
    expect(await page.getDateTitle()).toEqual('Date');
  });

  it('should display date of joining on form when date input', async () => {
    await page.navigateTo();
    await page.getAddEmployeeButton().click();
    await browser.sleep(1000);
    expect(await page.isDateInputDisplayed()).toBeTruthy('Date of joining input is display');
  });

  it('should display photo on modal window when click on add employee button', async () => {
    await page.navigateTo();
    await page.getAddEmployeeButton().click();
    await browser.sleep(1000);
    expect(await page.isPhotoDisplayed()).toBeTruthy('Photo is display');
  });

  afterEach(async () => {
    const logs = await browser.manage().logs().get(logging.Type.BROWSER);
    expect(logs).not.toContain(jasmine.objectContaining(
      {level: logging.Level.SEVERE} as logging.Entry))
  });
});

