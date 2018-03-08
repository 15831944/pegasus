using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using PegasusTests.Locators;
using PegasusTests.PageHelper.Comm;
using System;

namespace PegasusTests.PageHelper
{
    public class Products_CategoryHelper : DriverHelper
    {
        public LocatorReader locatorReader;

        public Products_CategoryHelper(IWebDriver idriver)
            : base(idriver)
        {
            locatorReader = new LocatorReader("Products_Category.xml");
        }


        // Type into given xml node
        public void TypeText(string Field, string text)
        {
            var locator = locatorReader.ReadLocator(Field);
            WaitForElementPresent(locator, 20);
            SendKeys(locator, text);
        }

        // Search And edit the category
        public void SearchAndEdit(string name)
        {
            int x = XpathCount("//div[@class='dd']/ul[2]/li");
            for (int i = 1; i <= x; i++)
            {
                var Nloc = "//div[@class='dd']/ul[2]/li[" + i + "]/div/span[2]";
                var Edit = "//div[@class='dd']/ul[2]/li[" + i + "]/div/span[1]/a[2]/i";
                if (GetText(Nloc).Contains(name))
                    Click(Edit);
            }
        }

        // Verify text of given xml node
        public void VerifyText(string XmlNode, string text)
        {
            var locator = locatorReader.ReadLocator(XmlNode);
            var value = GetText(locator);
            Assert.IsTrue(value.Contains(text));
        }

        // Select by value
        public void Select(string xmlNode, string value)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            SelectDropDown(locator, value);
        }



        //Delete Product Category
        public void DeleteProductCategory()
        {
            var l = "//ul[@id='sortable']/li[20]/div/span[1]/a[3]/i";
            var cnt = XpathCount("//ul[@id='sortable']/li");

            for (int i = 20; i <= cnt; i++)
            {
                var ll = "//ul[@id='sortable']/li['" + i + "']/div/span[1]/a[3]/i";
                Click(ll);
                AcceptAlert();
                WaitForWorkAround(3000);
                WaitForElementPresent(ll, 10);
            }
        }

        public void EditPCategory(string namefield)
        {
            var count = XpathCount("//ul[@id='sortable']//li");
            WaitForWorkAround(3000);
            Console.WriteLine(count);
            for (int i = 1; i <= count; i++)

            {
                var text = "//ul[@id='sortable']//li[" + i + "]//span[2]";
                WaitForWorkAround(3000);
                Console.WriteLine("Text is" + text);
                if (GetText(text).Contains(namefield))
                    WaitForWorkAround(3000);
                WaitForWorkAround(3000);
                {
                    var LocToClick = "//ul[@id='sortable']//li[" + i + "]//a[@title='Edit Category']";
                    var VerifyLoc = "//ul[@id='sortable']//li[" + i + "]//span[4]";
                    Console.WriteLine("Verify Loc is " + VerifyLoc);
                    WaitForWorkAround(3000);
                    Assert.IsTrue(GetText(VerifyLoc).Contains("Expanded"));
                    WaitForWorkAround(3000);
                    WaitForWorkAround(1000);
                    WaitForWorkAround(3000);
                    Click(LocToClick);
                    WaitForWorkAround(3000);
                    Console.WriteLine("Loc To click is  " + LocToClick);
                    WaitForWorkAround(5000);
                    SelectDropDown("//*[@id='ProductCategoryCanCollapse']", "1");
                    WaitForWorkAround(2000);
                    WaitForWorkAround(3000);
                    Click("//div[@class='col-lg-12']//input[@title='Save']");
                    WaitForWorkAround(2000);
                    WaitForWorkAround(3000);
                    Assert.IsTrue(GetText(VerifyLoc).Contains("Collapsed"));
                    WaitForWorkAround(2000);
                }
            }

        }


        //Verify Delete Buttton not present for category.
        public void VerifyDeleteButton(string namefield)
        {
            var count = XpathCount("//ul[@id='sortable']//li");
            for (int i = 1; i <= count; i++)
            {
                var text = "//ul[@id='sortable']//li[" + i + "]//span[2]";
                if (GetText(text).Contains(namefield))
                {
                    var loc = "//ul[@id='sortable']//li[" + i + "]//a[@title='Delete Category']";
                    Assert.IsFalse(IsElementPresent(loc));
                    WaitForWorkAround(1000);

                }
            }
        }


        //Verify Delete Buttton
        public void DeleteCategory(string namefield)
        {
            var count = XpathCount("//ul[@id='sortable']//li");
            for (int i = 1; i <= count; i++)
            {
                var text = "//ul[@id='sortable']//li[" + i + "]//span[2]";
                if (GetText(text).Contains(namefield))
                    WaitForWorkAround(4000);
                {
                    var loc = "//ul[@id='sortable']//li[" + i + "]//a[@title='Delete Category']";
                    WaitForWorkAround(4000);
                    Click(loc);
                    WaitForWorkAround(2000);
                    AcceptAlert();
                    WaitForWorkAround(3000);
                }
            }
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
            WaitForElementPresent(loc, 20);
            ClickViaJavaScript(loc);
        }

        // Seach and click on given category via name.
        public void SearchAnclick(string name)
        {
            int x = XpathCount("//div[@id='list']/div[1]/div/ul/li");
            for (int i = 1; i <= x; i++)
            {
                var neloc = "//div[@id='list']/div[1]/div/ul/li[" + i + "]/a[2]";
                if (GetText(neloc).Contains(name))
                    Click(neloc);
            }
        }

        public void DeleteCat(string name)
        {
            int x = XpathCount("//div[@class='dd']/ul[2]/li");
            for (int i = 1; i <= x; i++)
            {
                var loc = "//div[@class='dd']/ul[2]/li[" + i + "]/div/span[2]";
                string nm = GetText(loc);
                if (name == nm)
                {
                    Click("//div[@class='dd']/ul[2]/li[" + i + "]/div/span[1]/a[3]");
                    AcceptAlert();
                }
            }
        }
                


    }
}



