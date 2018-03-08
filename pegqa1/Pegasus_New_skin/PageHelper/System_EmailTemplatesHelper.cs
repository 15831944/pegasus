using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using PegasusTests.Locators;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.PageHelper
{
    public class System_PicklistsHelper : DriverHelper
    {
        public LocatorReader locatorReader;

        public System_PicklistsHelper(IWebDriver idriver)
            : base(idriver)
        {
            locatorReader = new LocatorReader("System_Picklists.xml");
        }

        // Verify presence of the element.
        public void verifyElementPresent(string field)
        {
            var locator = locatorReader.ReadLocator(field);
            WaitForElementPresent(locator, 30);
            Assert.IsTrue(IsElementPresent(locator));
        }

        // Click on last picklist inacticve button.
        public void PicInactive(string name)
        {
            int x = XpathCount("//table[@class='table table-bordered']/tbody/tr");
            for (int i = 1; i <= x; i++)
            {
                var newloc = "//table[@class='table table-bordered']/tbody/tr[" + i + "]/td[1]/span";
                var ActiveBtn = "//table[@class='table table-bordered']/tbody/tr[" + i + "]/td[3]/span/button";
                if (GetText(newloc).Contains(name))
                    Click(ActiveBtn);
            }
        }

        // Verify availability of element.
        public void verifyElementAvailable(string field)
        {
            var locator = locatorReader.ReadLocator(field);
            WaitForElementVisible(locator, 20);
            Assert.IsTrue(IsElementVisible(locator));
        }

        // Type into given xml node
        public void TypeText(string Field, string text)
        {
            var locator = locatorReader.ReadLocator(Field);
            WaitForElementPresent(locator, 20);
            SendKeys(locator, text);
        }

        // Verify text of given xml node
        public void VerifyText(string XmlNode, string text)
        {
            var locator = locatorReader.ReadLocator(XmlNode);
            var value = GetText(locator);
            Assert.IsTrue(value.Contains(text));
        }

        public void DeletePickList(string text)
        {
            var loc = "//*[@id='PickListValueDeleteItems']/option[text()='"+ text + "']";
            WaitForWorkAround(5000);
            Click(loc);
            WaitForWorkAround(2000);

        }


        // Select by value
        public void Select(string xmlNode, string value)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            SelectDropDown(locator, value);
        }

        // Click on given xml node.
        public void ClickElement(string xmlNode)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(locator, 20);
            Click(locator);
        }

        // Click the element via Java Script
        public void ClickJs(string xmlNode)
        {
            var loc = locatorReader.ReadLocator(xmlNode);
            ClickViaJavaScript(loc);
        }

        // Select element by text.        
        public void SelectText(string xmlNode, string text)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(locator, 20);
            SelectDropDownByText(locator, text);
        }

        // Search the picklist
        public void CheckAndCreate()
        {          
            for(int i =1; i < 20; i++)
            {
                var loc = "//table[@class='table table-bordered']//tr['+ i +']//td[1]/span";
                if (loc.Contains("Personal"))
                {
                    break;
                }
                else
                {
                    // Do nothing
                }
            }
        }
    }
}