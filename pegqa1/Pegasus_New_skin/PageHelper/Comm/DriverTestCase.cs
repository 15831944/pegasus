using System;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Threading;
using Atlassian.Jira;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using Selenium;
using OpenQA.Selenium.Interactions;

namespace PegasusTests.PageHelper.Comm
{
    public abstract class DriverTestCase
    {
        private static bool _isFileDeletedFirst;
        private static bool _isExecutedFirst;
        private static string _oldfname;
        private static string _name;
        private readonly XMLParse _oWaXmlData = new XMLParse();
        private IWebDriver _driver;
        private ISelenium _selenium;
        private string _url;
        public string BrowserType;
        public DriverHelper DriverHelper;
        public LoginHelper LoginHelper;
        protected string LogoutUrl;
        public StringBuilder VerificationErrors;
        public string _office;
        public string _corp;
        public string _urlJiraDashboard;
        

        [TestInitialize]
        public void SetupTest()
        {
        //    DeleteFile();
            CreateFolder();
            CreateExeFolder();
            _oWaXmlData.LoadXML("../../Config/ApplicationSettings.xml");

            _url = _oWaXmlData.getNodeValue("settings/URL/application");
           _urlJiraDashboard = _oWaXmlData.getNodeValue("settings/URLTest/JIRA_Dashboard");
            LogoutUrl = _oWaXmlData.getNodeValue("settings/URL/logout");
            _office = _oWaXmlData.getNodeValue("settings/URL/application");
            _corp = _oWaXmlData.getNodeValue("settings/URL/Corp");

            BrowserType = _oWaXmlData.getNodeValue("settings/browserdata/browser");

            if (BrowserType.ToLower().Equals("firefox") || BrowserType.ToLower().Equals("ff"))
            {

   
                _driver = new FirefoxDriver();

         
            }
            else if (BrowserType.ToLower().Equals("internet explorer") || BrowserType.ToLower().Equals("ie"))
            {
                _driver = new InternetExplorerDriver();
            }
            else if (BrowserType.ToLower().Equals("chrome"))
            {
                _driver = new ChromeDriver(@"C:\\Webdrivers");
            }
            else
            {
                _driver = new FirefoxDriver();
            }

            _driver.Manage().Window.Maximize();

            _selenium = new WebDriverBackedSelenium(_driver, _url);

            _driver.Navigate().GoToUrl(_url);
        }

        public IWebDriver GetWebDriver()
        {
            return _driver;
        }

        public ISelenium GetSelenium()
        {
            return _selenium;
        }

        [TestCleanup]
        public void TeardownTest()
        {
            _driver.Quit();
        }


        //Taking Screenshot
 /*       public void TakeScreenshot(string saveLocation)
        {
            var location = GetPath() + _name + "\\" + saveLocation + ".png";
            Thread.Sleep(10000);
            Screenshot ss = ((ITakesScreenshot)_driver).GetScreenshot();
            Thread.Sleep(10000);
            ss.SaveAsFile(location, ImageFormat.Png);
            Thread.Sleep(10000);
        }
        */

            // take screen shot of screen when test fails.
            public void TakeScreenshot(string saveLocation)
        {
            Thread.Sleep(10000);
            var loc = GetPath() + _name + "\\" + saveLocation + ".png";
            Thread.Sleep(10000);
            Screenshot ss = ((ITakesScreenshot)_driver).GetScreenshot();
            Thread.Sleep(10000);
                   ss.SaveAsFile(loc ,ImageFormat.Png);
            Thread.Sleep(10000);
        }





        public string GetRandomNumber()
        {
            var number = DateTime.Now.ToString("yyyyMMddHHmmssffff");
            return number;
        }

        // Verify the page title
        public void VerifyTitle(string title)
        {
            var containsTitle = false;

            for (var i = 0; i < 2; ++i)
            {
                containsTitle = GetWebDriver().Title.Contains(title);
                if (containsTitle)
                {
                    break;
                }

            }

            Assert.IsTrue(containsTitle);
        }

        public void VerifyTitle()
        {
            var title = GetWebDriver().Title;
            Console.WriteLine("Title of page is " + title);

            if (title.Contains("Merchants"))
            {
                Assert.IsTrue(title.Contains("Merchants"));
            }

            else  {
                Assert.IsTrue(title.Contains("Clients"));
            }
         
        }

        public void VerifyTitleMerchantClient()
        {
            var title = GetWebDriver().Title;
            Console.WriteLine("Title of page is " + title);

            if (title.Contains("Create a Client"))
            {
                Assert.IsTrue(title.Contains("Create a Client"));
            }

            else
            {
                Assert.IsTrue(title.Contains("Create a Merchant"));
            }

        }

        // Refresh the current web page
        public void RefreshPage()
        {
            GetWebDriver().Navigate().Refresh();
        }

        // Login into the application
        public void Login(string userName, string password)
        {
            LoginHelper = new LoginHelper(GetWebDriver());

            LoginHelper.EnterUserName(userName);
            LoginHelper.EnterPassword(password);
            LoginHelper.ClickEnterButton();
        }

        // Logout from the application
        public void Logout()
        {
            LoginHelper = new LoginHelper(GetWebDriver());
            LoginHelper.ClickUserIcon();
            LoginHelper.ClickLogOff();
        }

        // Get path
        public string GetPath()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var ab = currentDirectory.Split(new[] {"bin"}, StringSplitOptions.None);
            var a = ab[0];
            var fPath = a + "Screenshots\\";
            return fPath;
        }
        public string GetPathToFile()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var ab = currentDirectory.Split(new[] { "bin" }, StringSplitOptions.None);
            var a = ab[0];
            var fPath = a + "Files\\";
            Console.Write("file path: " + fPath);
            return fPath;          
        }
        // Create file _name
        public void CreateFolder()
        {
            if (_isExecutedFirst) return;

            _name = string.Format("{0:ddMMyy_hhmmss}", DateTime.Now);
            var fname = GetPath() + _name;
            _oldfname = fname;
            Directory.CreateDirectory(_oldfname);
            _isExecutedFirst = true;
        }

        public int RandomNumber(int n1, int n2)
        {
            var rnd = new Random();
            return rnd.Next(n1, n2);
        }

        public void ChangeTheme(string theme)
        {
            Thread.Sleep(2000);
            GetWebDriver().Navigate().GoToUrl("https://www.mypegasuscrm.com/newthemecorp/newthemeoffice/themes");
            LoginHelper loginHelper = new LoginHelper(GetWebDriver());
            VerifyTitle("Themes");
            if (theme == "Old")
            {
                if (loginHelper.IsElementPresent("//*[@id='menu']/li[1]/a"))
                {
                    Console.WriteLine("New theme");
                    loginHelper.WaitForElementVisible("//table[@id='list1']/tbody/tr[3]/td[5]/a[1]/i", 30);
                    loginHelper.Click("//table[@id='list1']/tbody/tr[3]/td[5]/a[1]/i");
                    loginHelper.WaitForText("Theme is successfully changed as default theme.", 30);
                }

            }
            else
            {
                if (!loginHelper.IsElementPresent("//*[@id='menu']/li[1]/a"))
                {
                    Console.WriteLine("Old theme");
                    loginHelper.WaitForElementVisible("//table[@id='list1']/tbody/tr[3]/td[5]/a[1]/img",30);
                    loginHelper.Click("//table[@id='list1']/tbody/tr[3]/td[5]/a[1]/img");
                    loginHelper.WaitForText("Theme is successfully changed as default theme.", 30);
                }
            }
            VerifyTitle("Themes");
            GetWebDriver().Navigate().GoToUrl("https://www.pegasus-test.com/newthemecorp/pegasustestoffice");
        }


        public void VisitOffice(String URL)
        {
            GetWebDriver().Navigate().GoToUrl(_office + URL);
        }

        public void VisitJira(String URL)
        {
            GetWebDriver().Navigate().GoToUrl(URL);
        }


        public void VisitCorp(String URL)
        {
            GetWebDriver().Navigate().GoToUrl(_corp + URL);
        }

        public void CreateExeFolder()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var ab = currentDirectory.Split(new[] { "bin" }, StringSplitOptions.None);
            var a = ab[0] + "\\ExecutionLog";
            if (Directory.Exists(a))
            {
                Console.WriteLine("Already Created");
            }
            else
            {
                Directory.CreateDirectory(a);
                Console.WriteLine("New Created");
            }

        }

       

          // Delete Existing File
  /*      public void DeleteFile()
        {
            if (_isFileDeletedFirst) return;

            string filePath = "Execution_Result_File.csv";
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            _isFileDeletedFirst = true;
        }
    */    
    }
}