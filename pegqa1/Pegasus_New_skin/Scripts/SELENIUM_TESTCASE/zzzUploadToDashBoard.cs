using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using PegasusTests.PageHelper.Comm;
using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using Selenium;
using System.IO;

namespace Pegasus_Admin.Scripts
{

    [TestClass]
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
            //LoginHelper loginHelper = new LoginHelper(GetWebDriver());
            //Browser = new ChromeDriver(@"C:\\Webdrivers");
            Browser = GetWebDriver();
            Browser = new FirefoxDriver();

        }

   //     [TestMethod]
        [TestCategory("Upload")]
        public void zzzUploadToDashboard()
        {
            Browser.Navigate().GoToUrl("http://pegasuscrm.us/dashboard/index2.php");
            //   upload(@"C:\\Aslam_Tests\\Pegasus_New_Skin\\Pegasus_New_skin\\Pegasus_New_skin\\bin\\Debug\\Execution_Result_File.csv");
            var locFile = GetPathToFile() + "Debug\\Execution_Result_File.csv";
            var updated_path = locFile.Replace("Files", "bin");
            Console.WriteLine(updated_path);
            upload(updated_path);
            Browser.FindElement(By.XPath("//input[@id=\"submitButton\"]")).Click();
            Thread.Sleep(7000);
        }
        /* Pegasus_Crop
        Browser.Navigate().GoToUrl("http://pegasuscrm.us/dashboard/index2.php");
        upload(@"C:\\Aslam_Tests\\Pegasus_Crop\\Pegasus_Crop\\bin\\Debug\\Execution_Result_File.csv");
        Browser.FindElement(By.XPath("//input[@id=\"submitButton\"]")).Click();
        Thread.Sleep(5000);

        Browser.Navigate().GoToUrl("http://pegasuscrm.us/dashboard/index2.php");
        upload(@"C:\\Aslam_Tests\\Pegasus_Change_URL\\Pegasus_Change_URL\\PegasusTests\\bin\Debug\\Execution_Result_File.csv");
        Browser.FindElement(By.XPath("//input[@id=\"submitButton\"]")).Click();
        Thread.Sleep(5000);

        Browser.Navigate().GoToUrl("http://pegasuscrm.us/dashboard/index2.php");
        upload(@"C:\\Aslam_Tests\\Pegasus_Bug\\Pegasus_Bug\\PegasusTests\\bin\\Debug\\Execution_Result_File.csv");
        Browser.FindElement(By.XPath("//input[@id=\"submitButton\"]")).Click();
        Thread.Sleep(5000);

        Browser.Navigate().GoToUrl("http://pegasuscrm.us/dashboard/index2.php");
        upload(@"C:\\Aslam_Tests\\Peg_Admin\\Pegasus_Admin\\Pegasus_Admin\\bin\Debug\\Execution_Result_File.csv");
        Browser.FindElement(By.XPath("//input[@id=\"submitButton\"]")).Click();
        Thread.Sleep(5000);
    }
    /*
    public string filePathString(string projectName)
    {
        \\Peg_Admin\\Pegasus_Admin\\Pegasus_Admin
        return "C:\\Users\\tmai\\Documents\\GitHub\\pegqa" + projectName + "\\bin\\Debug\\Execution_Result_File.csv";
    }
    */

        public void upload(string FileName)
        {
            // String locator = locatorReader.ReadLocator(Field);
            Console.WriteLine(FileName);
            GetWebDriver().FindElement(OpenQA.Selenium.By.XPath("//input[@id=\"chooseFile\"]")).SendKeys(FileName);
            Thread.Sleep(3000);
        }


    }

}
