import { browser, logging } from 'protractor';
import { AppPage } from './app.po';

describe('workspace-project App', () => {
  let page: AppPage;

  beforeEach(() => {
    page = new AppPage();
  });

  it('should display title app as `Employee Management Portal`', async () => {
    await page.navigateTo();
    expect(await page.getTitleText()).toEqual('Employee Management Portal');
  });

  it('should display departments button on page', async () => {
    await page.navigateTo();
    expect(await page.getDepartmentsButtonDisplayed()).toBeTruthy('Departments button is display');
  });

  it('should display departments button title as `Departments`', async () => {
    await page.navigateTo();
    expect(await page.getDepartmentsButtonTitle()).toEqual('Departments');
  });

  it('should display employees button on page', async () => {
    await page.navigateTo();
    expect(await page.getEmployeesButtonDisplayed()).toBeTruthy('Employees button is display');
  });

  it('should display employees button title as `Employees`', async () => {
    await page.navigateTo();
    expect(await page.getEmployeesButtonTitle()).toEqual('Employees');
  });

  afterEach(async () => {
    // Assert that there are no errors emitted from the browser
    const logs = await browser.manage().logs().get(logging.Type.BROWSER);
    expect(logs).not.toContain(jasmine.objectContaining({
      level: logging.Level.SEVERE,
    } as logging.Entry));
  });
});
