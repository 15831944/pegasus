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



   // [TestClass]

    public class zzzUploadToDashBoard

    {

        private IWebDriver Browser;

        public string GetPathToFile()

        {

            var currentDirectory = Directory.GetCurrentDirectory();

            var ab = currentDirectory.Split(new[] { "bin" }, StringSplitOptions.None);

            var a = ab[0];

            var fPath = a + "Files\\";

            return fPath;

        }
        public IWebDriver GetWebDriver()

        {

            return Browser;

        }

        [TestInitialize]

        public void Login()

        {

            Browser = GetWebDriver();

            Browser = new ChromeDriver(@"C:\\Webdrivers");



        }

        [TestMethod]

        [TestCategory("Upload")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Admin1")]

        public void zzzUploadToDashboard()

        {
            Browser.Manage().Cookies.DeleteAllCookies();
            //http://pegasuscrm.us/dashboard/index2.php
            Browser.Navigate().GoToUrl("http://pegasuscrm.us/dashboard/index2.php");

            var locFile = GetPathToFile() + "Debug\\Execution_Result_File.csv";

            var updated_path = locFile.Replace("Files", "bin");

            Console.WriteLine(updated_path);

            upload(updated_path);

            Browser.FindElement(By.XPath("//input[@id=\"submitButton\"]")).Click();

            Thread.Sleep(7000);

        }

        public void upload(string FileName)

        {

            Console.WriteLine(FileName);

            GetWebDriver().FindElement(OpenQA.Selenium.By.XPath("//input[@id=\"chooseFile\"]")).SendKeys(FileName);

            Thread.Sleep(3000);

        }

    }

}