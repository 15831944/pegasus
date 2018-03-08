using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using PegasusTests.Locators;
using PegasusTests.PageHelper.Comm;
using System;
using System.Collections.Generic;

namespace PegasusTests.PageHelper
{
    public class Eqiupment_EquipmentHelper : DriverHelper
    {
        public LocatorReader locatorReader;

        public Eqiupment_EquipmentHelper(IWebDriver idriver)
            : base(idriver)
        {
            locatorReader = new LocatorReader("Equipment_Equipment.xml");
        }

        // Select element by text
        public void SelectByText(string xmlNode, string text)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            SelectDropDownByText(locator, text);
        }

        //Verify the No of showing page
        public void ShowResult(int Value)
        {
            int el4 = XpathCount("//table[@id='list1']//tbody//tr");
            Console.WriteLine("el4 is  " + el4);
            if (Value < el4)
            {
                Assert.IsTrue(Value < el4);
            }
            else
            {
                Assert.IsFalse(Value < el4);
            }
        }

        // Scroll to the given locator
        public void ScrollToElement(string xmlNode)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(locator ,10);
            ScrollDown(locator);

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

        // Select by value
        public void Select(string xmlNode, string value)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            SelectDropDown(locator, value);
        }

        //Delete All Equipments
        public void DeleteEquipments()
        {
            var l = "//table[@id='list1']/tbody/tr[2]/td[3]/span[2]/a/i";
            var cnt = XpathCount("//table[@id='list1']/tbody/tr");
            Console.WriteLine("Xath Count is " + cnt);
            for (int i = 20; i < cnt; i++)
            {
                var ll = "//table[@id='list1']/tbody/tr[" + i + "]/td[3]/span[2]/a/i";
                Console.WriteLine("loc path is " + ll);
                Click(ll);
                AcceptAlert();
                WaitForWorkAround(3000);
            }
        }

        // Click on given xml node.
        public void ClickElement(string xmlNode)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(locator, 20);
            Click(locator);
        }

        // Click on given xml node.
        public void ClickOnDisplayed(string xmlNode)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(locator, 20);
            ClickDisplayed(locator);
        }



        // Click on given xml node via java script
        public void Clickjs(string xmlNode)
        {
            var locator = locatorReader.ReadLocator(xmlNode);
            WaitForElementPresent(locator, 20);
            ClickViaJavaScript(locator);
        }

        public void OptionPresentInDropdown(string optiontext)
        {
            int count = XpathCount("//*[@id='order']/option");
            for(int i =1;i<= count; i++)
            {
                var loc = _driver.FindElement(By.XPath("//*[@id='order']/option[" + i + "]"));
                Console.WriteLine("sds  "+ loc.Text);
               
                if (loc.Text.Contains(optiontext))
                {
                    Assert.IsTrue(loc.Text.Contains(optiontext));
                    break;
                }
               // break;

            }
        }



        //Clean Office
        public void sdsadsa()
        {
            var forcont = "//table[@id='list1']//tr";
         //   var loc = "//table[@id='list1']//tr[2]//td[3]";
            int sdsa = XpathCount(forcont);

            for (int i = 2; i <= 10000; i++)
            {
                Console.WriteLine("this is for " + i);
             //   SendKeys("//*[@id='gs_name']", "Test2");
             //   WaitForWorkAround(5000);
                var loc = "//table[@id='list1']//tr["+i+"]//td[3]";
                var locator = _driver.FindElement(By.XPath(loc));

                if (locator.Text.Contains("Test2"))
                {
                //    var sd = _driver.FindElement(By.XPath("//table[@id='list1']//tr[" + i + "]//td[2]/a[3]/span"));
                    ClickViaJavaScript("//table[@id='list1']//tr[" + i + "]//td[2]/a[3]/span");
                    WaitForWorkAround(2000);
                    ClickViaJavaScript("//button[@title='Delete']");
                    WaitForWorkAround(8000);
                }
                else
                {
                     //do nothing

                }
            }


        }



    }
}