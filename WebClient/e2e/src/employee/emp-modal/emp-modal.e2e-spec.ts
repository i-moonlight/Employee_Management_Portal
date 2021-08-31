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

  afterEach(async () => {
    const logs = await browser.manage().logs().get(logging.Type.BROWSER);
    expect(logs).not.toContain(jasmine.objectContaining(
      {level: logging.Level.SEVERE} as logging.Entry))
  });
});

