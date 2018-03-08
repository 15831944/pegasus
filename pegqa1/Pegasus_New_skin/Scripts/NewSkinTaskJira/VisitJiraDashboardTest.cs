using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using PegasusTests.PageHelper.Comm;
using System;
using System.Threading;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using Selenium;
using System.IO;

namespace Pegasus_Admin.Scripts
{

  //  [TestClass]

    public class VisitJiraDashboardTest
    {


        private IWebDriver Browser;

        public IWebDriver GetWebDriver()
        {
            return Browser;
        }

        [TestInitialize]
        public void Login()
        {
            //LoginHelper loginHelper = new LoginHelper(GetWebDriver());        
            Browser = GetWebDriver();
            //   Browser = new FirefoxDriver();
            Browser = new ChromeDriver(@"C:\\Webdrivers");

        }

        [TestMethod]
        [TestCategory("Dashboard")]
        [TestCategory("TS5")]
        public void zzzUploadToDashboard()
        {
            Browser.Navigate().GoToUrl("http://pegasuscrm.us/dashboard/index.php");
            Browser.Manage().Window.Maximize();
            Browser.FindElement(By.XPath("//input[@name='username']")).SendKeys("qa");
            Browser.FindElement(By.XPath("//input[@name='password']")).SendKeys("pegasus34!");
            Browser.FindElement(By.XPath("//button[@name='login']")).Click();
            Thread.Sleep(7000);
        }


    }

}
