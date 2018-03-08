using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class AuthenticateWithGoogleCalendar : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("BugTestNew")]
        
        public void authenticateWithGoogleCalendar()
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
            var corpOffice_OfficeHelper = new CorpOffice_OfficeHelper(GetWebDriver());
            var yopmail_Helper = new YopMailHelper(GetWebDriver());
            var gmailHelper = new GmailHelper(GetWebDriver());
            var officeActivities_MeetingHelper = new OfficeActivities_MeetingHelper(GetWebDriver());

            // Variable random
            var username1 = "GoogleAuth" + RandomNumber(44, 99999);
            var name = "Test" + RandomNumber(999, 999999);
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("AuthenticateWithGoogleCalendar", "Login with valid username and password");
                Login(username[0], password[0]);

                executionLog.Log("AuthenticateWithGoogleCalendar", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("AuthenticateWithGoogleCalendar", "Visit Corp offices");
                VisitCorp("offices");
                corpOffice_OfficeHelper.WaitForWorkAround(3000);

                executionLog.Log("AuthenticateWithGoogleCalendar", "Click on create office button");
                corpOffice_OfficeHelper.ClickElement("Create");
                corpOffice_OfficeHelper.WaitForWorkAround(3000);

                executionLog.Log("AuthenticateWithGoogleCalendar", "Enter Name");
                corpOffice_OfficeHelper.TypeText("Name", name);

                executionLog.Log("AuthenticateWithGoogleCalendar", "Enter DBAName");
                corpOffice_OfficeHelper.TypeText("DBAName", "TESTDBA");

                executionLog.Log("AuthenticateWithGoogleCalendar", "Enter Website");
                corpOffice_OfficeHelper.TypeText("Website", "TEST.COM");

                executionLog.Log("AuthenticateWithGoogleCalendar", "Enter OfficeCode");
                corpOffice_OfficeHelper.TypeText("OfficeCode", "12345");

                executionLog.Log("AuthenticateWithGoogleCalendar", "Enter VenderName");
                corpOffice_OfficeHelper.TypeText("VendorName", "VenderTEST");

                executionLog.Log("AuthenticateWithGoogleCalendar", "Enter VenderName");
                corpOffice_OfficeHelper.TypeText("VendorCode", "1234");

                executionLog.Log("AuthenticateWithGoogleCalendar", "Enter VenderName");
                corpOffice_OfficeHelper.TypeText("OfficePhoneNumber", "1234567890");

                executionLog.Log("AuthenticateWithGoogleCalendar", "Enter VenderName");
                corpOffice_OfficeHelper.TypeText("BusinessPhoneNumber", "1234567890");

                executionLog.Log("AuthenticateWithGoogleCalendar", "Enter VenderName");
                corpOffice_OfficeHelper.TypeText("FaxNumber", "1234567890");

                executionLog.Log("AuthenticateWithGoogleCalendar", "Enter VenderName");
                corpOffice_OfficeHelper.TypeText("LinkedURL", "Linked.com");

                executionLog.Log("AuthenticateWithGoogleCalendar", "Enter VenderName");
                corpOffice_OfficeHelper.TypeText("FacebookURL", "Facebook.com");

                executionLog.Log("AuthenticateWithGoogleCalendar", "Enter TwitterURL");
                corpOffice_OfficeHelper.TypeText("TwitterURL", "Twitter.com");

                executionLog.Log("AuthenticateWithGoogleCalendar", "Select Address");
                corpOffice_OfficeHelper.Select("AddressType", "Office");

                executionLog.Log("AuthenticateWithGoogleCalendar", "Enter AddressLine1");
                corpOffice_OfficeHelper.TypeText("AddressLine1", "FC-89");

                executionLog.Log("AuthenticateWithGoogleCalendar", "Select Zip Code");
                corpOffice_OfficeHelper.TypeText("ZIpCode", "60601");
                corpOffice_OfficeHelper.WaitForWorkAround(3000);

                executionLog.Log("AuthenticateWithGoogleCalendar", "Enter PrimaryUserName");
                corpOffice_OfficeHelper.TypeText("PrimaryUserName", username1);

                executionLog.Log("AuthenticateWithGoogleCalendar", "Click on AutoGenPassword checkbox");
                corpOffice_OfficeHelper.ClickElement("AutoGenPassword");
                corpOffice_OfficeHelper.WaitForWorkAround(1000);

                executionLog.Log("AuthenticateWithGoogleCalendar", "Enter PrimaryPassword");
                corpOffice_OfficeHelper.TypeText("PrimaryPassword", "123456");

                executionLog.Log("AuthenticateWithGoogleCalendar", "Select Salutation");
                corpOffice_OfficeHelper.Select("Salutation", "Mr");
                //corpOffice_OfficeHelper.WaitForWorkAround(2000);

                executionLog.Log("AuthenticateWithGoogleCalendar", "Enter FirstName");
                corpOffice_OfficeHelper.TypeText("FirstName", "Test");

                executionLog.Log("AuthenticateWithGoogleCalendar", "Enter LastName");
                corpOffice_OfficeHelper.TypeText("LastName", "Tester");

                executionLog.Log("AuthenticateWithGoogleCalendar", "Enter eAddress");
                corpOffice_OfficeHelper.TypeText("eAddress", "aslam.pegasus@yopmail.com");

                executionLog.Log("AuthenticateWithGoogleCalendar", "Click on save button.");
                try
                {
                    corpOffice_OfficeHelper.ClickElement("Save");
                }
                catch (OpenQA.Selenium.WebDriverException)
                { }
                yopmail_Helper.WaitForWorkAround(7000);

                executionLog.Log("AuthenticateWithGoogleCalendar", "Go to yopmail.com");
                GetWebDriver().Navigate().GoToUrl("http://www.yopmail.com/en/");
                Console.WriteLine("Redirected to yopmail.com");

                executionLog.Log("AuthenticateWithGoogleCalendar", "Enter eAddress in yopmail");
                yopmail_Helper.TypeText("Yopmail", "aslam.pegasus");

                executionLog.Log("AuthenticateWithGoogleCalendar", "Check Inbox");
                yopmail_Helper.ClickElement("YopmailClick");

                yopmail_Helper.switchFrame("ifmail");

                yopmail_Helper.VerifyText("MailSecondPoint", username1);

                executionLog.Log("AuthenticateWithGoogleCalendar", "Click on Office Link");
                yopmail_Helper.ClickElement("OfficeLink");
                corpOffice_OfficeHelper.SwitchToWindow();

                yopmail_Helper.WaitForWorkAround(5000);

                executionLog.Log("AuthenticateWithGoogleCalendar", "Click on Profile Icon");
                VisitCorp("logout");
                corpOffice_OfficeHelper.WaitForWorkAround(3000);

                executionLog.Log("AuthenticateWithGoogleCalendar", "Login to office");
                Login(username1,"123456");

                executionLog.Log("AuthenticateWithGoogleCalendar", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("AuthenticateWithGoogleCalendar", "Redirect at My Calendar page");
                GetWebDriver().Navigate().GoToUrl("https://www.mypegasuscrm.com/newthemecorp/" + username1 + "/meetings/calendar/my");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("AuthenticateWithGoogleCalendar", "Click on Authenticate with Google button");
                officeActivities_MeetingHelper.ClickElement("GoogleAuth");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                if (gmailHelper.IsElementPresent("//div[@id='identifierLink']/div[2]/p"))
                {
                    executionLog.Log("AuthenticateWithGoogleCalendar", "Click on Use Another Account");
                    gmailHelper.ClickElement("UseAnotherAccBtn");
                    gmailHelper.WaitForWorkAround(1000);

                    executionLog.Log("AuthenticateWithGoogleCalendar", "Enter email address");
                    gmailHelper.TypeText("EnterEmail", "selenium311@gmail.com");

                    executionLog.Log("AuthenticateWithGoogleCalendar", "Click on Next button");
                    gmailHelper.ClickElement("NextBtn");
                    gmailHelper.WaitForWorkAround(1000);

                    executionLog.Log("AuthenticateWithGoogleCalendar", "Enter password address");
                    gmailHelper.TypeText("EnterPswrd", "selenium123456");

                    executionLog.Log("AuthenticateWithGoogleCalendar", "Click on Next button");
                    gmailHelper.ClickElement("NextBtn");
                    gmailHelper.WaitForWorkAround(1000);

                    executionLog.Log("AuthenticateWithGoogleCalendar", "Click on Allow button");
                    gmailHelper.ClickElement("AllowBtn");
                    gmailHelper.WaitForWorkAround(4000);
                }

                else
                {
                    executionLog.Log("AuthenticateWithGoogleCalendar", "Enter email address");
                    gmailHelper.TypeText("EnterEmail", "selenium311@gmail.com");

                    executionLog.Log("AuthenticateWithGoogleCalendar", "Click on Next button");
                    gmailHelper.ClickElement("NextBtn");
                    gmailHelper.WaitForWorkAround(1000);

                    executionLog.Log("AuthenticateWithGoogleCalendar", "Enter password address");
                    gmailHelper.TypeText("EnterPswrd", "selenium123456");

                    executionLog.Log("AuthenticateWithGoogleCalendar", "Click on Next button");
                    gmailHelper.ClickElement("NextBtn");
                    gmailHelper.WaitForWorkAround(1000);

                    executionLog.Log("AuthenticateWithGoogleCalendar", "Click on Allow button");
                    gmailHelper.ClickElement("AllowBtn");
                    gmailHelper.WaitForWorkAround(4000);
                }

                executionLog.Log("AuthenticateWithGoogleCalendar", "Verify successfully Google Calendar Synced");
                officeActivities_MeetingHelper.VerifyPageText("Last Sync Time");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("AuthenticateWithGoogleCalendar");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Authenticate With Google Calendar");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Authenticate With Google Calendar", "Bug", "Medium", "Create Office page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Authenticate With Google Calendar");
                        TakeScreenshot("AuthenticateWithGoogleCalendar");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AuthenticateWithGoogleCalendar.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("AuthenticateWithGoogleCalendar");
                        string id = loginHelper.getIssueID("Authenticate With Google Calendar");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AuthenticateWithGoogleCalendar.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Authenticate With Google Calendar"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Authenticate With Google Calendar");
                //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("AuthenticateWithGoogleCalendar");
                executionLog.WriteInExcel("Authenticate With Google Calendar", Status, JIRA, "Corp Office");
            }
        }
    }
}