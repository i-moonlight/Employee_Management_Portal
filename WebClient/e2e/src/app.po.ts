import { browser, by, element, ElementFinder } from 'protractor';

export class AppPage {
  async navigateTo(): Promise<unknown> {
    return browser.get(browser.baseUrl);
  }

  async getTitleText(): Promise<string> {
    return element(by.css('app-root h3')).getText();
  }

 // Departments button.
  getDepartmentsButton(): ElementFinder {
    return element(by.css('[routerlink="department"]'));
  }

  async getDepartmentsButtonDisplayed(): Promise<boolean> {
    return (await this.getDepartmentsButton()).isDisplayed();
  }

  async getDepartmentsButtonTitle(): Promise<string> {
    return (await this.getDepartmentsButton()).getText();
  }

  // Employees button.
  getEmployeesButton(): ElementFinder {
    return element(by.css('[routerlink="employee"]'));
  }

  async getEmployeesButtonDisplayed(): Promise<boolean> {
    return (await this.getEmployeesButton()).isDisplayed();
  }
}
