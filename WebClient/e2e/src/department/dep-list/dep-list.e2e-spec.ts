import { DepartmentListPage } from './dep-list.po';
import { browser, logging } from 'protractor';

describe('DepartmentListPage', () => {
  let page: DepartmentListPage;

  beforeEach(() =>
    page = new DepartmentListPage());

  it('should display add department button on page', async () => {
    await page.navigateTo();
    expect(await page.isAddDepButtonDisplayed()).toBeTruthy('Add button is display');
  });

  afterEach(async () => {
    const logs = await browser.manage().logs().get(logging.Type.BROWSER);
    expect(logs).not.toContain(jasmine.objectContaining(
      {level: logging.Level.SEVERE} as logging.Entry));
  });
})
