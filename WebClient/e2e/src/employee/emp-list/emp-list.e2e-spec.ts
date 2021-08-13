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

  afterEach(async () => {
    const logs = await browser.manage().logs().get(logging.Type.BROWSER);
    expect(logs).not.toContain(jasmine.objectContaining(
      {level: logging.Level.SEVERE} as logging.Entry))
  });
});

