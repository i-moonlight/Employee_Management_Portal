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

  afterEach(async () => {
    const logs = await browser.manage().logs().get(logging.Type.BROWSER);
    expect(logs).not.toContain(jasmine.objectContaining(
      {level: logging.Level.SEVERE} as logging.Entry))
  });
});
