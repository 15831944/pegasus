using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class VerifyFieldsInheritedForNewOffice : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("BugTestNew")]
        public void verifyFieldsInheritedForNewOffice()
        {

            string[] username = null;
            string[] password = null;


            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_corp");
            password = oXMLData.getData("settings/Credentials", "password");

            

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var corp_Office_OfficeHelper = new CorpOffice_OfficeHelper(GetWebDriver());
            var yopmail_Helper = new YopMailHelper(GetWebDriver());
            var office_LeadsHelper = new Office_LeadsHelper(GetWebDriver());
            var office_FieldDictionary_FieldsHelper = new Office_FieldDictionary_FieldsHelper(GetWebDriver());


            //  Variable random
            
            String JIRA = "";
            String Status = "Pass";
            var officename = "Office" + RandomNumber(1, 999999999);
            var username1 = "user" + RandomNumber(1, 9999);

            try
            {
                executionLog.Log("VerifyFieldsInheritedForNewOffice", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyFieldsInheritedForNewOffice", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected to Dashboard");

                executionLog.Log("VerifyFieldsInheritedForNewOffice", "Create Office");
                VisitCorp("offices");
                Console.WriteLine("Redirected to All Office page");

                executionLog.Log("VerifyFieldsInheritedForNewOffice", "Click on Create button");
                corp_Office_OfficeHelper.ClickElement("Create");

                executionLog.Log("VerifyFieldsInheritedForNewOffice", "Verify page title");
                VerifyTitle("Create an Office");

                executionLog.Log("VerifyFieldsInheritedForNewOffice", "Enter Office name");
                corp_Office_OfficeHelper.TypeText("Name", officename);

                executionLog.Log("VerifyFieldsInheritedForNewOffice", "Enter Address Line 1");
                corp_Office_OfficeHelper.TypeText("AddressLine1", "Add 1");

                executionLog.Log("VerifyFieldsInheritedForNewOffice", "Enter Zip Code");
                corp_Office_OfficeHelper.TypeText("ZIpCode", "20001");
                corp_Office_OfficeHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyFieldsInheritedForNewOffice", "Deselect Auto generate password check box");
                corp_Office_OfficeHelper.ClickElement("AutoGenPassword");
                corp_Office_OfficeHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyFieldsInheritedForNewOffice", "Enter Username");
                corp_Office_OfficeHelper.TypeText("PrimaryUserName", username1);

                executionLog.Log("VerifyFieldsInheritedForNewOffice", "Enter Password");
                corp_Office_OfficeHelper.TypeText("PrimaryPassword", "123456");

                executionLog.Log("VerifyFieldsInheritedForNewOffice", "Enter First Name");
                corp_Office_OfficeHelper.TypeText("FirstName", "Test");

                executionLog.Log("VerifyFieldsInheritedForNewOffice", "Enter Last Name");
                corp_Office_OfficeHelper.TypeText("LastName", "User");

                executionLog.Log("VerifyFieldsInheritedForNewOffice", "Select eAddress label");
                corp_Office_OfficeHelper.Select("EaddressLabel", "Home");

                executionLog.Log("VerifyFieldsInheritedForNewOffice", "Enter eAddress");
                corp_Office_OfficeHelper.TypeText("eAddress", "aslam.pegasus@yopmail.com");
                Console.WriteLine("Entered eAddress");


                executionLog.Log("VerifyFieldsInheritedForNewOffice", "Click on Save");
                try
                {
                    corp_Office_OfficeHelper.ClickElement("Save");
                }
                catch (OpenQA.Selenium.WebDriverException)
                { }
                
                Console.WriteLine("Office Created");

                executionLog.Log("VerifyFieldsInheritedForNewOffice", "Go to yopmail.com");
                GetWebDriver().Navigate().GoToUrl("http://www.yopmail.com/en/");
                Console.WriteLine("Redirected to yopmail.com");

                executionLog.Log("VerifyFieldsInheritedForNewOffice", "Enter eAddress in yopmail");
                yopmail_Helper.TypeText("Yopmail", "aslam.pegasus");

                executionLog.Log("VerifyFieldsInheritedForNewOffice", "Check Inbox");
                yopmail_Helper.ClickElement("YopmailClick");

                yopmail_Helper.switchFrame("ifmail");

                yopmail_Helper.VerifyText("MailSecondPoint", username1);

                executionLog.Log("VerifyFieldsInheritedForNewOffice", "Click on Office Link");
                yopmail_Helper.ClickElement("OfficeLink");
                corp_Office_OfficeHelper.SwitchToWindow();

                yopmail_Helper.WaitForWorkAround(5000);

                executionLog.Log("VerifyFieldsInheritedForNewOffice", "Click on Profile Icon");
                 corp_Office_OfficeHelper.ClickElement("ProfileIcon");

                 executionLog.Log("VerifyFieldsInheritedForNewOffice", "Click on Logout");
                 corp_Office_OfficeHelper.ClickElement("Logout");
                 Console.WriteLine("Redirected to Login Screen");

                 executionLog.Log("VerifyFieldsInheritedForNewOffice", "Enter Username");
                 loginHelper.TypeText("Login/Username", username1);

                 executionLog.Log("VerifyFieldsInheritedForNewOffice", "Enter Last Name");
                 loginHelper.TypeText("Login/Password", "123456");

                 executionLog.Log("VerifyFieldsInheritedForNewOffice", "Verify Office");
                 loginHelper.ClickElement("Login/Enter");
                 Console.WriteLine("Logged into new office");

                 executionLog.Log("VerifyFieldsInheritedForNewOffice", "Verify Page title");
                 VerifyTitle("Dashboard");

                 executionLog.Log("VerifyFieldsInheritedForNewOffice", "Redirect at create Leads page");
                 GetWebDriver().Navigate().GoToUrl("https://www.mypegasuscrm.com/newthemecorp/"+username1+"/leads/create");
                 office_LeadsHelper.WaitForWorkAround(3000);

                 executionLog.Log("VerifyFieldsInheritedForNewOffice", "Verify Company Name field appearing");
                 office_LeadsHelper.verifyElementPresent("CompanyName");

                 executionLog.Log("VerifyFieldsInheritedForNewOffice", "Redirect at Field Properties");
                 GetWebDriver().Navigate().GoToUrl("https://www.mypegasuscrm.com/newthemecorp/" + username1 + "/fields");
                 office_LeadsHelper.WaitForWorkAround(3000);

                 executionLog.Log("VerifyFieldsInheritedForNewOffice", "Select Module");
                 office_FieldDictionary_FieldsHelper.SelectByText("FSModule", "Clients");
                 office_LeadsHelper.WaitForWorkAround(3000);

                 executionLog.Log("VerifyFieldsInheritedForNewOffice", "Verify fields available");
                 Assert.IsFalse(GetWebDriver().PageSource.Contains("No Fields Available."));
                 //office_LeadsHelper.WaitForWorkAround(3000);

            }

            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyFieldsInheritedForNewOffice");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Fields Inherited For New Office");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Fields Inherited For New Office", "Bug", "Medium", "Employee page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Fields Inherited For New Office");
                        TakeScreenshot("VerifyFieldsInheritedForNewOffice");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyFieldsInheritedForNewOffice.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyFieldsInheritedForNewOffice");
                        string id = loginHelper.getIssueID("Verify Fields Inherited For New Office");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyFieldsInheritedForNewOffice.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Fields Inherited For New Office"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Fields Inherited For New Office");
                //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyFieldsInheritedForNewOffice");
                executionLog.WriteInExcel("Verify Fields Inherited For New Office", Status, JIRA, "Corp Employees");
            }
            
        }
    }
}