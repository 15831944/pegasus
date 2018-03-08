using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class VerifyContactStatusBulkUpdate : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("BugTestNew")]
        public void verifyContactStatusBulkUpdate()
        {
            string[] username = null;
            string[] password = null;

            XMLParse oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            //Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var office_ContactsHelper = new Office_ContactsHelper(GetWebDriver());

            // Random Variable
            var last_name = "Contact" + GetRandomNumber();
            var comp = "Automation" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";


            try
            {
                executionLog.Log("VerifyContactStatusBulkUpdate", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyContactStatusBulkUpdate", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyContactStatusBulkUpdate", "Redirect at import contacts page.");
                VisitOffice("contacts/create");
                office_ContactsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyContactStatusBulkUpdate", "Enter First and Last name");
                office_ContactsHelper.TypeText("FirstNAME", "Automation");
                office_ContactsHelper.TypeText("LastName", last_name);

                executionLog.Log("VerifyContactStatusBulkUpdate", "Enter Company name");
                office_ContactsHelper.TypeText("CompanyName", comp);

                executionLog.Log("VerifyContactStatusBulkUpdate", "Select Status");
                office_ContactsHelper.selectByText("Status", "Active");

                executionLog.Log("VerifyContactStatusBulkUpdate", "Click on Save button");
                office_ContactsHelper.ClickElement("SaveContactN");
                office_ContactsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyContactStatusBulkUpdate", "Navigate to All Contacts page");
                VisitOffice("contacts");
                office_ContactsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyContactStatusBulkUpdate", "Search by contact name and company");
                var name = "Automation " + last_name;
                office_ContactsHelper.TypeText("SearchName", name);
                office_ContactsHelper.WaitForWorkAround(1000);
                office_ContactsHelper.TypeText("SearchCompanyName", comp);
                office_ContactsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyContactStatusBulkUpdate", "Select check box");
                office_ContactsHelper.ClickElement("SelectIstContact");

                executionLog.Log("VerifyContactStatusBulkUpdate", "Click on Bulk Update");
                office_ContactsHelper.ClickElement("BulkUpdate");
                office_ContactsHelper.Click("//div[@title='Bulk Update']/ul/li[3]/a");
                office_ContactsHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyContactStatusBulkUpdate", "Select Status");
                office_ContactsHelper.selectByText("BU_Status", "Inactive");

                executionLog.Log("VerifyContactStatusBulkUpdate", "Click on Update button");
                office_ContactsHelper.ClickElement("BU_StatusUpdate");
                office_ContactsHelper.AcceptAlert();
                office_ContactsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyContactStatusBulkUpdate", "Search by contact name and company");
                office_ContactsHelper.TypeText("SearchName", name);
                office_ContactsHelper.WaitForWorkAround(1000);
                office_ContactsHelper.TypeText("SearchCompanyName", comp);
                office_ContactsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyContactStatusBulkUpdate", "Open contact");
                office_ContactsHelper.ClickElement("Contact1");
                office_ContactsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyContactStatusBulkUpdate", "Verify Status updated");
                Assert.AreEqual(office_ContactsHelper.GetText("//div[@id='status']"),"Inactive");
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyContactStatusBulkUpdate");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Contact Status Bulk Update");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Contact Status Bulk Update", "Bug", "Medium", "Contacts page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Contact Status Bulk Update");
                        TakeScreenshot("VerifyContactStatusBulkUpdate");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyContactStatusBulkUpdate.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyContactStatusBulkUpdate");
                        string id = loginHelper.getIssueID("Verify Contact Status Bulk Update");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyContactStatusBulkUpdate.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Contact Status Bulk Update"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Contact Status Bulk Update");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyContactStatusBulkUpdate");
                executionLog.WriteInExcel("Verify Contact Status Bulk Update", Status, JIRA, "Contact Management");
            }
        }
    }
}