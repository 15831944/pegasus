using System;
using LinqToExcel;
using System.Linq;
using LinqToExcel.Query;
using LinqToExcel.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using PegasusTests.Locators;
using PegasusTests.PageHelper.Comm;
using System.IO;
using OpenQA.Selenium.Interactions;


namespace PegasusTests.PageHelper
{
    public class DeleteDataHelper : DriverHelper
    {
        public LocatorReader locatorReader;

        public DeleteDataHelper(IWebDriver idriver)
            : base(idriver)
        {
            locatorReader = new LocatorReader("ActivitiesEmails_EmailAccount.xml");
        }


        // Clean the Default Master Pricing Plan
        public void CleanMasterPricing()
        {
            var count = "//table[@id='list1']//tr";
            int aa = XpathCount(count);
            for (int j = 2; j <= 1000; ++j)
            {
                for (int i = 2; i <= 1000; i++)
                {
                    Console.WriteLine("this is for" + i);
                    var loc = "//table[@id='list1']//tr[" + i + "]//td[3]/a";
                    var locator = _driver.FindElement(By.XPath(loc));
                    if (locator.Text.Contains("Test"))
                    {
                        ClickViaJavaScript("//table[@id='list1']//tr[" + i + "]//td[2]/a[2]/i");
                        AcceptAlert();
                        WaitForWorkAround(18000);
                        break;
                    }
                    else
                    {
                        //do nothing
                    }
                }
            }
        }

        //Clean default crop processor 

        public void CleanCropProcessor()
        {
            var count = "//table[@id='list1']//tr";
            int aa = XpathCount(count);
            for (int a = 2; a <= 1000; ++a)
            {
                for (int i = 2; i <= 1000; i++)
                {
                    Console.WriteLine("this is for " + i);
                    var loc = "//table[@id='list1']//tr[" + i + "]//td[4]/a";
                    GetText(loc);
                    if (GetText(loc).Contains("EditedTest") || GetText(loc).Contains("Test"))
                    {
                        ClickViaJavaScript("//table[@id='list1']//tr[" + i + "]//td[3]/a[2]/i");
                        AcceptAlert();
                        WaitForWorkAround(15000);
                        break;
                    }
                    else
                    {
                        // Do Nothing
                    }
                }
            }
        }

        
        

    }
}

