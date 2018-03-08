
﻿using System;
using System.Linq;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System.IO;
using System.Collections.Generic;
using Atlassian.Jira;

namespace PegasusTests.PageHelper.Comm
{
    public abstract class DriverHelper
    {
        public readonly IWebDriver _driver;
        public static string[] jiraUsername;
        public static string[] jiraPassword;
        public static string[] jiraURL;
        public static Jira jiraConn;

        public DriverHelper(IWebDriver idriver)
        {
            _driver = idriver;
        }
        public IWebDriver GetWebDriver()
        {
            return _driver;
        }

        public By ByLocator(string locator)
        {
            By result = null;

            if (locator.StartsWith("//"))
            {
                result = By.XPath(locator);
            }
            else if (locator.StartsWith("xpath="))
            {
                result = By.XPath(locator.Replace("xpath=", ""));
            }
            else if (locator.StartsWith("css="))
            {
                result = By.CssSelector(locator.Replace("css=", ""));
            }
            else if (locator.StartsWith("#"))
            {
                result = By.Name(locator.Replace("#", ""));
            }
            else if (locator.StartsWith("link="))
            {
                result = By.LinkText(locator.Replace("link=", ""));
            }

            else
            {
                result = By.Id(locator);
            }

            return result;
        }

        public void SelectWindow(string title)
        {
            foreach (var item in _driver.WindowHandles.Where(item => _driver.SwitchTo().Window(item).Title.Equals(title)))
            {
                _driver.SwitchTo().Window(item);
                break;
            }
        }

        public void SelectWindowWithTitle(string title)
        {
            foreach (var item in _driver.WindowHandles.Where(item => _driver.SwitchTo().Window(item).Title.Contains(title)))
            {
                _driver.SwitchTo().Window(item);
                break;
            }
        }

        public void SwitchWindowWithSimilerTitle(string title, string Id)
        {
            foreach (var item in _driver.WindowHandles.Where(item => _driver.SwitchTo().Window(item).Title.Equals(title) && item != Id))
            {
                _driver.SwitchTo().Window(item);
                break;
            }
        }

        public Boolean IsElementPresent(string locator)
        {
            Boolean result;
            try
            {
                _driver.FindElement(ByLocator(locator));
                result = true;
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }

        //Verify element not present on page
        public Boolean IsElementNotPresent(string locator)
        {
            Boolean result;
            try
            {
                _driver.FindElement(ByLocator(locator));
                result = false;
            }
            catch (Exception)
            {
                result = true;
            }
            return result;
        }

        public int CheckboxCount()
        {
            return _driver.FindElements(By.XPath("//input[@type='checkbox']")).Count();
        }

        //Swicth Window No Title
        public void SwitchToWindow()
        {
            GetWebDriver().SwitchTo().Window(GetWebDriver().WindowHandles.Last());
            WaitForWorkAround(2000);
        }

        public void WaitForElementPresent(string locator, int timeout)
        {
            for (var i = 0; i < timeout * 10; ++i)
            {
                if (IsElementPresent(locator))
                {
                    break;
                }

                try
                {
                    Thread.Sleep(100);
                }
                catch (Exception)
                {
                    //e.printStackTrace();
                }
            }
        }

        public String getCurrentURL()

        {
            string URL = GetWebDriver().Url;
      //      IJavaScriptExecutor js = (IJavaScriptExecutor)GetWebDriver();

            //String a = js.executeScript("window.location.href");

        //    String URL = (String)js.ExecuteScript("window.document.URL.substr()");

            return URL;

        }

        public void WaitForElementEnabled(string locator, int timeout)
        {
            for (var i = 0; i < timeout * 10; ++i)
            {
                if (IsElementPresent(locator) && _driver.FindElement(ByLocator(locator)).Enabled)
                {
                    break;
                }

                try
                {
                    Thread.Sleep(100);
                }
                catch (Exception)
                {
                    // Ignore exception.
                }
            }
        }

        public void WaitForElementNotEnabled(string locator, int timeout)
        {
            for (var i = 0; i < timeout * 10; ++i)
            {
                if (IsElementPresent(locator) && !_driver.FindElement(ByLocator(locator)).Enabled)
                {
                    break;
                }

                try
                {
                    Thread.Sleep(100);
                }
                catch (Exception)
                {
                    // Ignore exception.
                }
            }
        }

        public void WaitForElementVisible(string locator, int timeout)
        {
            for (var i = 0; i < timeout * 10; ++i)
            {
                if (IsElementPresent(locator) && _driver.FindElement(ByLocator(locator)).Displayed)
                {
                    break;
                }

                try
                {
                    Thread.Sleep(100);
                }
                catch (Exception)
                {
                    // Ignore exception.
                }
            }
        }

        public void WaitForElementNotVisible(string locator, int timeout)
        {
            for (var i = 0; i < timeout * 10; ++i)
            {
                if (IsElementPresent(locator) && !_driver.FindElement(ByLocator(locator)).Displayed)
                {
                    break;
                }

                try
                {
                    Thread.Sleep(100);
                }
                catch (Exception)
                {
                    // Ignore exception.
                }
            }
        }

        public bool IsElementVisible(string locator)
        {
            WaitForWorkAround(5000);
            return _driver.FindElement(ByLocator(locator)).Displayed;
            WaitForWorkAround(5000);
        }

        public void SendKeys(string locator, string value)
        {
            WaitForElementPresent(locator, 20);
            Assert.IsTrue(IsElementPresent(locator));
            var el = GetWebDriver().FindElement(ByLocator(locator));
            el.Clear();
            el.SendKeys(value);
        }


        //Upload File 

        public void UploadFile(string locator, string value)
        {
            WaitForElementPresent(locator, 20);
            Assert.IsTrue(IsElementPresent(locator));
            var el = GetWebDriver().FindElement(ByLocator(locator));
            el.SendKeys(value);
        }

        //Click Via Enter key.
        public void ClickViaEnter(string Locator)
        {
            var Hdel = GetWebDriver().FindElement(ByLocator(Locator));
            Hdel.SendKeys(Keys.Enter);
        }

        public void Click(string locator)
        {
            WaitForElementPresent(locator, 20);
            Assert.IsTrue(IsElementPresent(locator));
            _driver.FindElement(ByLocator(locator)).Click();
        }

        public void SelectDropDown(string locator, string targetValue)
        {
            WaitForElementPresent(locator, 20);
            Assert.IsTrue(IsElementPresent(locator));
            var dropDownListBox = _driver.FindElement(ByLocator(locator));
            var clickThis = new SelectElement(dropDownListBox);
            clickThis.SelectByValue(targetValue);
         
        }

        public void OptionNotPresentInDropdown(string locator, string optionText)
        {
            IList<IWebElement> options = _driver.FindElements(By.XPath(locator));
            foreach (IWebElement option in options)          
            {
                if (option.Text.Contains(optionText))
                {
                    Assert.IsFalse(option.Text.Contains(optionText));
                }
                else {

                }

                
            }
        }

        public string GetText(string locator)
        {
            var value = "";
            WaitForElementPresent(locator, 20);
            Assert.IsTrue(IsElementPresent(locator));
            var textval = _driver.FindElement(ByLocator(locator));
            value = textval.Text;
            return value;
          
        }
        public string getInputText(string locator)
        {
            var value = "";
            WaitForElementPresent(locator, 20);
            Assert.IsTrue(IsElementPresent(locator));
            var textval = _driver.FindElement(ByLocator(locator));
            value = textval.GetAttribute("value");
            return value;
        }

        public string GetTextFromNonVisibleElement(string locator)
        {
            var value = "";
            WaitForElementPresent(locator, 20);
            Assert.IsTrue(IsElementPresent(locator));
            var textval = _driver.FindElement(ByLocator(locator));
            value = ((IJavaScriptExecutor)_driver).ExecuteScript("return arguments[0].innerHTML", textval).ToString();
            return value;
        }

        public void SelectAndClosePopUp(string title)
        {
            foreach (var item in _driver.WindowHandles.Where(item => _driver.SwitchTo().Window(item).Title.Equals(title)))
            {
                _driver.SwitchTo().Window(item);
                _driver.Close();
                break;
            }
        }

        public string GetValue(string locator)
        {
            var value = "";
            WaitForElementPresent(locator, 20);
            Assert.IsTrue(IsElementPresent(locator));
            var textval = _driver.FindElement(ByLocator(locator));
            value = textval.GetAttribute("value");
            return value;
        }

        public string GetAttributeValue2(string text)
        {
            return _driver.FindElement(
                    By.XPath("//select[@id='ctl00_ContentPlaceHolderBody_drpClass']/option[contains(text(), '" + text +
                             "')]")).GetAttribute("value");
        }

        public string GetAttributeValuep(string text, string id)
        {
            return _driver.FindElement(By.XPath("//select[@id='" + id + "']/option[contains(text(), '" + text + "')]"))
                    .GetAttribute("value");
        }

        public void verifyFieldText(string loc, string expectedtext)
        {
         var valueOfField   = _driver.FindElement(By.XPath(loc))
                    .GetAttribute("value");

            Assert.AreEqual(valueOfField, expectedtext);
        }

        public string GetAtrributeByXpath(string xpath, string attribute)
        {
            return _driver.FindElement(By.XPath(xpath)).GetAttribute(attribute);
        }

        public string GetTextByXpath(string xpath)
        {
            WaitForElementEnabled(xpath, 30);
            return _driver.FindElement(By.XPath(xpath)).Text;
        }

        public string GetAtrributeByLocator(string locator, string attribute)
        {
            return _driver.FindElement(ByLocator(locator)).GetAttribute(attribute);
        }



        public bool IsFieldDisabled(string locator, string attribute)
        {
            var bRetVal = false;

            try
            {
                bRetVal = Convert.ToBoolean(_driver.FindElement(ByLocator(locator)).GetAttribute(attribute));
            }
            catch (Exception)
            {
                // Ignore exception.
            }

            return bRetVal;
        }

        public void getSelectedText(string locator)
        {

            IWebElement selectElement = _driver.FindElement(ByLocator(locator));
            SelectElement selectedValue = new SelectElement(selectElement);
            string selectedText = selectedValue.SelectedOption.Text;
            Console.WriteLine("Text of selected option is  "+selectedText);

        }

        public string GetSelectedText(string locator)
        {

            IWebElement selectElement = _driver.FindElement(ByLocator(locator));
            SelectElement selectedValue = new SelectElement(selectElement);
            string selectedText = selectedValue.SelectedOption.Text;
            return selectedText;

        }


        public void SelectDropDownByText(string locator, string targetText)
        {
            WaitForElementPresent(locator, 20);
            Assert.IsTrue(IsElementPresent(locator));
            var dropDownListBox = _driver.FindElement(ByLocator(locator));
            var clickThis = new SelectElement(dropDownListBox);
            
            clickThis.SelectByText(targetText);
        }

        public void VerifySelectedOption(string locator, string Value)
        {
            var ss = _driver.FindElement(ByLocator(locator));
            var optionis = new SelectElement(ss).SelectedOption.Text;
            Console.WriteLine("seleted option is " + optionis);
            Assert.AreEqual(Value, optionis);

        }

        // Clear Text box Value.
        public void ClearTextBoxValue(string locator)
        {
            WaitForElementPresent(locator, 20);
            Assert.IsTrue(IsElementPresent(locator));
            var el = GetWebDriver().FindElement(ByLocator(locator));
            el.Clear();
        }

        // Count number Of Rows.
        public int XpathCount(string xPath)
        {
            var count = _driver.FindElements(By.XPath(xPath)).Count;
            return count;
        }

 
        public void ClickViaJavaScript(string locator)
        {
            WaitForElementPresent(locator, 20);
            //Assert.IsTrue(IsElementPresent(locator));
            var el = _driver.FindElement(ByLocator(locator));

            //OpenQA.Selenium.Interactions.Actions actions = new OpenQA.Selenium.Interactions.Actions(driver);
            //actions.MoveToElement(el).ClickAndHold();

            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click();", el);
        }

        /* dan - Not sure we need this or not
        //Is text Present
        public bool IsTextPresent(string locator, string sTextToFind)
        {
            bool bRetVal = false;
            IWebElement element = GetWebDriver().FindElement(ByLocator(locator));
            if (element.Text == sTextToFind)
            {
                bRetVal = true;
            }

            return bRetVal;
        }
        */

        public string GetIdByAtttribute(string locator)
        {
            var sRetVal = "";

            var we = _driver.FindElement(By.XPath(locator));
            sRetVal = we.GetAttribute("id");

            return sRetVal;
        }

        public void CloseSelectedWindow()
        {
            _driver.Close();
        }

        // Method to click on button using btn text
        public void ClickButtonText(string btnText)
        {
            var locator = "//span[@class='ui-button-text' and contains(text(), '" + btnText + "')]";
            WaitForElementPresent(locator, 20);
            Click(locator);
        }

        // Method to verify text in page source
        public void VerifyPageText(string text)
        {
            var result = GetWebDriver().PageSource.Contains(text);
            Assert.IsTrue(result, "Text String: " + text + "Not Found.");
        }

        // Method to verify text in page source
        public void VerifyTextNot(string text)
        {
            var result = GetWebDriver().PageSource.Contains(text);
            Assert.IsFalse(result, "Text String: " + text + "Not Found.");
        }

        // Wait
        public void WaitForWorkAround(int number)
        {
            Thread.Sleep(number);
        }

        public void ArrowDown(string locator)
        {
            Assert.IsTrue(IsElementPresent(locator));
            GetWebDriver().FindElement(ByLocator(locator)).SendKeys(Keys.ArrowDown);
        }

        public void MouseOver(string locator)
        {
            var el = GetWebDriver().FindElement(ByLocator(locator));

            var builder = new Actions(GetWebDriver());
            builder.MoveToElement(el).Build().Perform();
        }

        // Mouseover and wait
        public void MouseOverAndWait(string locator, int time)
        {
          
        }

        public void AcceptAlert()
        {
            WaitForWorkAround(2000);
            try
            {
                GetWebDriver().SwitchTo().Alert().Accept();
            }
            catch (NoAlertPresentException e)
            {
                Console.WriteLine("No Alert present");
            }
        }



        /*    public void drawSign()
             {
                 Actions builder = new Actions(GetWebDriver());
                 Action drawAction = builder.MoveToElement(//*[@id='clicknew']", 50, 100)
        
                     //signatureWebElement is the element that holds the signature element you have in the DOM
                           .ClickAndHold()
                           .moveByOffset(100, 50)
                           .moveByOffset(6, 7)
                           .release()
                           .build();
                  drawAction.Perform();    

             }    */


        public FileInfo Getnewfilename(DirectoryInfo directory)
        {
            Thread.Sleep(3000);
            return directory.GetFiles().Union(directory.GetDirectories().Select(d => Getnewfilename(d)))
                .OrderByDescending(f => (f == null ? DateTime.MinValue : f.LastWriteTime))
                .FirstOrDefault();
        }

        public string GetnewDirectoryName(string directory)
        {
            Thread.Sleep(3000);
            DateTime lastHigh = new DateTime(1900, 1, 1);
            string highDir = "";
            foreach (string subdir in Directory.GetDirectories(directory))
            {
                DirectoryInfo fi1 = new DirectoryInfo(subdir);
                DateTime created = fi1.LastWriteTime;
                if (created > lastHigh)
                {
                    highDir = subdir;
                    lastHigh = created;
                }
            }
            return highDir;
        }

        public string CurrentUser()
        {
            string username = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            string user = username.Substring(username.LastIndexOf('\\') + 1);
            Console.WriteLine(user);
            return user;
        }

        public void WaitForText(string text, int timeout)
        {
            Boolean flag = false;
            for (var i = 0; i < timeout; ++i)
            {
                if (GetWebDriver().PageSource.Contains(text))
                {
                   
                    flag = true;
                    break;
                }
                else
                {
                    Thread.Sleep(100);
                }
            }
            Assert.IsTrue(flag);
        }

        public void WaitForTextHide(String text, int timeout)
        {
            for (var i = 0; i < timeout; ++i)
            {
                if (GetWebDriver().PageSource.Contains(text))
                {
                    Thread.Sleep(1000);
                }
                else
                {
                    break;
                }
            }
        }

        public void VerifyTextNotAvailable(string text)
        {
            string locator = "//*[contains(text(),'" + text + "')]";
            Assert.IsFalse(IsElementVisible(locator));
        }


        public void VerifyTextAvailable(string text)
        {
            string locator = "//*[contains(text(),'" + text + "')]";
            Assert.IsTrue(IsElementVisible(locator));
        }


        public void VerifyTextNotPresent(string text)
        {
            string locator = "//*[contains(text(),'" + text + "')]";
            Assert.IsFalse(IsElementPresent(locator));


        }
        public bool SwitchNewWindow(String windowName)
        {

            var currentWindow = GetWebDriver().CurrentWindowHandle;
            var availableWindows = new List<string>(GetWebDriver().WindowHandles);

            foreach (string w in availableWindows)
            {
                if (w != currentWindow)
                {
                    GetWebDriver().SwitchTo().Window(w);
                    if (GetWebDriver().Title == windowName)
                        return true;
                    else
                    {
                        GetWebDriver().SwitchTo().Window(currentWindow);
                    }
                }
            }

            return false;
        }

        public void VerifyAlertText(String text)
        {
            WaitForWorkAround(2000);
            String AlertText = GetWebDriver().SwitchTo().Alert().Text;
            Console.WriteLine("Alert Text  = " + AlertText);
            Assert.IsTrue(AlertText.Contains(text));
        }

        public void VerifyAlertTextNotPresent(String text)
        {
            WaitForWorkAround(2000);
            String AlertText = GetWebDriver().SwitchTo().Alert().Text;
            Console.WriteLine("Alert Text  = " + AlertText);
            Assert.IsFalse(AlertText.Contains(text));
        }

        public bool ElementNotAvailable(String locator)
        {
            WaitForWorkAround(2000);
            bool result;
            try
            {
                _driver.FindElement(ByLocator(locator));
                result = false;
            }
            catch (Exception)
            {
                result = true;
            }
            return result;
        }

        public void DoubleClick(string locator)
        {
            Actions builder = new Actions(GetWebDriver());
            IWebElement el = GetWebDriver().FindElement(ByLocator(locator));
            Console.WriteLine("Locator = " + locator);
            Assert.IsTrue(IsElementPresent(locator));
            builder.DoubleClick(el).Build().Perform();
        }

        public void dragndrop(string slocator1, string dlocator2)
        {
            var el1 = GetWebDriver().FindElement(ByLocator(slocator1));
            var el2 = GetWebDriver().FindElement(ByLocator(dlocator2));

            Actions ac = new Actions(_driver);
            ac.MoveToElement(el1).Perform();
            ac.DragAndDrop(el1, el2).Perform();
            //ac.Build().Perform();
        }





        public void CreateIssue(string Summary, string issueType, string Priority, string component, string Assignee, string Descriprion)
        {
            XMLParse _oWaXmlData = new XMLParse();

            _oWaXmlData.LoadXML("../../Config/ApplicationSettings.xml");
            jiraURL = _oWaXmlData.getData("settings/JiraCredentials", "URL");
            jiraUsername = _oWaXmlData.getData("settings/JiraCredentials", "UserName");
            jiraPassword = _oWaXmlData.getData("settings/JiraCredentials", "Password");
            //   jiraConn = new Jira(jiraURL[0], jiraUsername[0], jiraPassword[0]);
            jiraConn = new Jira("http://crmpegasus.com/", "Akhan", "WelcometoJira!");
            Console.WriteLine("Login success create issue");
            Summary = "[Automated Test Script] Testing " + Summary + " Script Failure Test";
            Issue issue = jiraConn.CreateIssue("PEG");
            try
            {
                issue.Summary = Summary;
                issue.Type = issueType;
                issue.Priority = Priority;
                issue.Assignee = Assignee;
                issue.Description = Descriprion;
                issue.SaveChanges();
                Console.WriteLine("Issue Created Successfully");
                CheckExstingIssue(Summary);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }







        public void CreateIssueTest(string Summary, string issueType, string Priority, string component, string Descriprion)
        {
            XMLParse _oWaXmlData = new XMLParse();

            _oWaXmlData.LoadXML("../../Config/ApplicationSettings.xml");
            jiraURL = _oWaXmlData.getData("settings/JiraCredentials", "URL");
            jiraUsername = _oWaXmlData.getData("settings/JiraCredentials", "UserName");
            jiraPassword = _oWaXmlData.getData("settings/JiraCredentials", "Password");
            //   jiraConn = new Jira(jiraURL[0], jiraUsername[0], jiraPassword[0]);
            jiraConn = new Jira("http://crmpegasus.com/", "Akhan", "WelcometoJira!");
            Console.WriteLine("Login success create issue");
            Summary = "[Automated Test Script] Testing " + Summary + " Script Failure Test";
            Issue issue = jiraConn.CreateIssue("PEG");
            try
            {
                issue.Summary = Summary;
                issue.Type = issueType;
                issue.Priority = Priority;
          //      issue.Assignee = Assignee;
                issue.Description = Descriprion;
                issue.SaveChanges();
                Console.WriteLine("Issue Created Successfully");
                CheckExstingIssue(Summary);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void AddComment(string issueID, string Comment)
        {
            XMLParse _oWaXmlData = new XMLParse();

            _oWaXmlData.LoadXML("../../Config/ApplicationSettings.xml");
            jiraURL = _oWaXmlData.getData("settings/JiraCredentials", "URL");
            jiraUsername = _oWaXmlData.getData("settings/JiraCredentials", "UserName");
            jiraPassword = _oWaXmlData.getData("settings/JiraCredentials", "Password");
            jiraConn = new Jira(jiraURL[0], jiraUsername[0], jiraPassword[0]);
            Console.WriteLine("Login success");

            try
            {
                Issue issue = jiraConn.GetIssue(issueID);
                issue.AddComment(Comment);
                issue.SaveChanges();
                Console.WriteLine("Comment added to Issue - " + issueID + " Successfully");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public bool CheckExstingIssue(string issueTitle)
        {
            bool result = false;
            bool check = false;
            XMLParse _oWaXmlData = new XMLParse();

            _oWaXmlData.LoadXML("../../Config/ApplicationSettings.xml");
            jiraURL = _oWaXmlData.getData("settings/JiraCredentials", "URL");
            jiraUsername = _oWaXmlData.getData("settings/JiraCredentials", "UserName");
            jiraPassword = _oWaXmlData.getData("settings/JiraCredentials", "Password");
            // jiraConn = new Jira(jiraURL[0], jiraUsername[0], jiraPassword[0]);
            jiraConn = new Jira("http://crmpegasus.com/", "Akhan", "WelcometoJira!");
            Console.WriteLine("Login success 11 existing");
            issueTitle = "[Automated Test Script] Testing " + issueTitle + " Script Failure Test";
            Console.WriteLine("This is issue title" + issueTitle);
            try
            {
                string jqlString = "project = PegasusCRM AND issuetype = Bug";
                IEnumerable<Atlassian.Jira.Issue> jiraIssues = jiraConn.GetIssuesFromJql(jqlString);
                foreach (var issue in jiraIssues)
                {
                    Console.WriteLine("JIRA Issues: " + issue.Summary);
                    if (issue.Summary == issueTitle)
                    {
                        result = true;
                        check = true;
                        break;
                    }
                }
                Console.WriteLine("Show me the value of result and check: " + result + "and " + check);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            if (!check)
            {
                Console.WriteLine("Issue not found");
            }
            return result;
        }

        public void AddAttachment(string path, string issueID)
        {
            XMLParse _oWaXmlData = new XMLParse();

            _oWaXmlData.LoadXML("../../Config/ApplicationSettings.xml");
            jiraURL = _oWaXmlData.getData("settings/JiraCredentials", "URL");
            jiraUsername = _oWaXmlData.getData("settings/JiraCredentials", "UserName");
            jiraPassword = _oWaXmlData.getData("settings/JiraCredentials", "Password");
            //    jiraConn = new Jira(jiraURL[0], jiraUsername[0], jiraPassword[0]);
            jiraConn = new Jira("http://crmpegasus.com/", "Akhan", "WelcometoJira!");

            Console.WriteLine("Login success add attachment");

            string jqlString = "project = PegasusCRM AND issuetype = Bug";
            bool result = false;
            IEnumerable<Atlassian.Jira.Issue> jiraIssues = jiraConn.GetIssuesFromJql(jqlString);
            foreach (var issue in jiraIssues)
            {
                if (issue.Key.Value == issueID)
                {
                    Issue foundissue = jiraConn.GetIssue(issueID);
                    foundissue.AddAttachment(path);
                    Console.WriteLine("Attachment Added Successfully");
                    foundissue.SaveChanges();
                    result = true;
                    break;
                }
            }
            if (!result)
            {
                Console.WriteLine("Failed To Add Attachment");
            }
        }

        public string getIssueID(string issueTitle)
        {
            string result = "";

            XMLParse _oWaXmlData = new XMLParse();

            _oWaXmlData.LoadXML("../../Config/ApplicationSettings.xml");
            jiraURL = _oWaXmlData.getData("settings/JiraCredentials", "URL");
            jiraUsername = _oWaXmlData.getData("settings/JiraCredentials", "UserName");
            jiraPassword = _oWaXmlData.getData("settings/JiraCredentials", "Password");
            //       jiraConn = new Jira(jiraURL[0], jiraUsername[0], jiraPassword[0]);
            jiraConn = new Jira("http://crmpegasus.com/", "Akhan", "WelcometoJira!");
            Console.WriteLine("Get issue ID");

           
            try
            {
                string jqlString = "project = PegasusCRM AND issuetype = Bug";
                IEnumerable<Atlassian.Jira.Issue> jiraIssues = jiraConn.GetIssuesFromJql(jqlString);
                issueTitle = "[Automated Test Script] Testing " + issueTitle + " Script Failure Test";
                foreach (var issue in jiraIssues)
                {
                    if (issue.Summary == issueTitle)
                    {
                        Console.WriteLine("Title issue success");
                        result = issue.Key.Value;
                        break;
                        Console.WriteLine("Get issue ID Success");

                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return result;
        }

        public bool CloseIssue(string issueTitle)
        {
            Console.WriteLine("Close issue success");
            bool result = false;
            bool check = false;
            XMLParse _oWaXmlData = new XMLParse();

            _oWaXmlData.LoadXML("../../Config/ApplicationSettings.xml");
            jiraURL = _oWaXmlData.getData("settings/JiraCredentials", "URL");
            jiraUsername = _oWaXmlData.getData("settings/JiraCredentials", "UserName");
            jiraPassword = _oWaXmlData.getData("settings/JiraCredentials", "Password");
         //   jiraConn = new Jira(jiraURL[0], jiraUsername[0], jiraPassword[0]);
            jiraConn = new Jira("http://crmpegasus.com/", "Akhan", "WelcometoJira!");
            Console.WriteLine("Close suuu success");
            try
            {
                string jqlString = "project = PegasusCRM AND issuetype = Bug";
                IEnumerable<Atlassian.Jira.Issue> jiraIssues = jiraConn.GetIssuesFromJql(jqlString);
                foreach (var issue in jiraIssues)
                {
                    Console.WriteLine("Loop success");
                    if (issue.Summary == issueTitle)
                    {
                        result = true;
                        check = true;
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            if (!check)
            {
                Console.WriteLine("Issue not found");
            }
            return result;
        }

        public void ClickDisplayed(string locator)
        {
            IList<IWebElement> el = GetWebDriver().FindElements(ByLocator(locator));
            for (int i = 0; i < el.Count; i++)
            {
                if (el[i].Displayed)
                {
                    el[i].Click();
                    break;
                }
            }
        }

        public void ScrollDown(String locator)
        {
            IWebElement element = GetWebDriver().FindElement(ByLocator(locator));
            ((IJavaScriptExecutor)GetWebDriver()).ExecuteScript("arguments[0].scrollIntoView(true);", element);
            Thread.Sleep(500);
        }

        public bool VerifyAlertNotPresent()
        {
            bool result = false;
            WaitForWorkAround(2000);
            try
            {
                GetWebDriver().SwitchTo().Alert();
            }
            catch (NoAlertPresentException e)
            {
                result = true;
            }
            return result;
        }

        public bool VerifyAlertPresent()
        {
            bool result = true;
            WaitForWorkAround(2000);
            try
            {
                GetWebDriver().SwitchTo().Alert();
            }
            catch (NoAlertPresentException e)
            {
                result = false;
            }
            return result;
        }
        public void clickByText(string locator)
        {
            _driver.FindElement(By.LinkText(locator)).Click();
        }

    
        public void waitForLink(string locator, int timeout)
        {
            bool flag = false;
            for (var i = 0; i < timeout * 10; ++i)
            {
                try
                {
                    _driver.FindElement(By.LinkText(locator));
                    flag = true;
                    break;
                }
                catch (Exception)
                {

                }
            }
            Assert.IsTrue(flag);

        }

        public bool isChecked(string locator)
        {
            IWebElement chkBox = GetWebDriver().FindElement(ByLocator(locator));
            if (chkBox.Selected)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public int TableRowCount()
        {
            return _driver.FindElements(By.XPath("//table[@id='list1']/tbody/tr")).Count();
        }
        public int TableCount(string table)
        {
            return _driver.FindElements(By.XPath("table")).Count();
        }
        public int InputCount(string part)
        {
            return _driver.FindElements(By.XPath(part)).Count();
        }

        public String GetUrl()
        {
            return _driver.Url;
        }

        public void ClickByTitle(string locator)
        {
             _driver.FindElement(By.XPath("//*[@title='" + locator + "']")).Click();


        }
        public string getTitle()
        {
            string title = _driver.Title;
            return title;
        }

        //Below method use to execute the java script
        public void JavascriptExecutor(string script)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)GetWebDriver();
            js.ExecuteScript(script);
        }

        //Select Owner  
        public void selectOwner(string xmlnode)
        {
            SelectDropDownByText(xmlnode, "All");

        }
    }
}