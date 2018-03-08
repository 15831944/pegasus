using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using PegasusTests.Locators;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.PageHelper
{
    public class LoginHelper : DriverHelper
    {
        public LocatorReader locatorReader;

        public LoginHelper(IWebDriver idriver)
            : base(idriver)
        {
            locatorReader = new LocatorReader("Login.xml");
        }

        public void EnterUserName(string username)
        {
            var locator = locatorReader.ReadLocator("Login/Username");
            SendKeys(locator, username);
        }

        public void EnterPassword(string password)
        {
            var locator = locatorReader.ReadLocator("Login/Password");
            SendKeys(locator, password);
        }

        public void ClickEnterButton()
        {
            var locator = locatorReader.ReadLocator("Login/Enter");
            Click(locator);
        }

        public void verifyErrorMessages(string msg)
        {
            GetWebDriver().PageSource.Contains(msg);
        }

        public void ClickUserIcon()
        {
            var locator = locatorReader.ReadLocator("Logout/UserIcon");
            Click(locator);
        }

        public void ClickLogOff()
        {
            var locator = locatorReader.ReadLocator("Logout/LogOff");
            Click(locator);

        }

        // Type into given xml node
        public void TypeText(string Field, string text)
        {
            var locator = locatorReader.ReadLocator(Field);
            WaitForElementPresent(locator, 20);
            SendKeys(locator, text);
        }

        // Click on xml node
        public void ClickElement(string xmlNode)
        {
            WaitForWorkAround(3000);
            var locator = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(locator, 50);
            Click(locator);
            WaitForWorkAround(3000);
        }
    }
}