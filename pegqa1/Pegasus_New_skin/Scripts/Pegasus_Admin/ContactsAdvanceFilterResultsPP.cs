using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class ContactsAdvanceFilterResultsPP : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin")]
        public void contactsAdvanceFilterResultsPP()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var office_ContactsHelper = new Office_ContactsHelper(GetWebDriver());

            // Variable
            var DocName = "Test Exe" + GetRandomNumber();
            var fileUpl = GetPathToFile() + "chrome.exe";
            String JIRA = "";
            String Status = "Pass";

            try
            {
            executionLog.Log("ContactsAdvanceFilterResultsPP", "Login with valid username and password");
            Login(username[0], password[0]);
            Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

            executionLog.Log("ContactsAdvanceFilterResultsPP", "Verify Page title");
            VerifyTitle("Dashboard");
            Console.WriteLine("Redirected at Dashboard screen.");

            executionLog.Log("ContactsAdvanceFilterResultsPP", "Redirect To URL");
            VisitOffice("contacts");
            office_ContactsHelper.WaitForWorkAround(3000);

            executionLog.Log("ContactsAdvanceFilterResultsPP", "Verify page title.");
            VerifyTitle("Contacts");

            executionLog.Log("ContactsAdvanceFilterResultsPP", "Click on advance filter.");
            office_ContactsHelper.ClickElement("AdvanceFilter");
            office_ContactsHelper.WaitForWorkAround(2000);

            executionLog.Log("ContactsAdvanceFilterResultsPP", "Select number of records to 10.");
            office_ContactsHelper.SelectByText("ResultsPerPage", "10");
            //office_ContactsHelper.WaitForWorkAround(3000);

            executionLog.Log("ContactsAdvanceFilterResultsPP", "Click on apply button.");
            office_ContactsHelper.ClickElement("Apply");
            office_ContactsHelper.WaitForWorkAround(3000);

            executionLog.Log("ContactsAdvanceFilterResultsPP", "Verify number of records displayed.");
            office_ContactsHelper.VerifyText("No.ofResults", "Showing 1 - 10 of");
            //office_ContactsHelper.WaitForWorkAround(3000);

            executionLog.Log("ContactsAdvanceFilterResultsPP", "Click on advance filter.");
            office_ContactsHelper.ClickElement("AdvanceFilter");
            office_ContactsHelper.WaitForWorkAround(2000);

            executionLog.Log("ContactsAdvanceFilterResultsPP", "Select number of records to 20.");
            office_ContactsHelper.SelectByText("ResultsPerPage", "20");
            //office_ContactsHelper.WaitForWorkAround(3000);

            executionLog.Log("ContactsAdvanceFilterResultsPP", "Click on apply button.");
            office_ContactsHelper.ClickElement("Apply");
            office_ContactsHelper.WaitForWorkAround(3000);

            executionLog.Log("ContactsAdvanceFilterResultsPP", "Verify number of records displayed.");
            office_ContactsHelper.VerifyText("No.ofResults", "Showing 1 - 20 of");
            //office_ContactsHelper.WaitForWorkAround(3000);

            executionLog.Log("ContactsAdvanceFilterResultsPP", "Click on advance filter.");
            office_ContactsHelper.ClickElement("AdvanceFilter");
            office_ContactsHelper.WaitForWorkAround(2000);

            executionLog.Log("ContactsAdvanceFilterResultsPP", "Select number of records to 50.");
            office_ContactsHelper.SelectByText("ResultsPerPage", "50");
            //office_ContactsHelper.WaitForWorkAround(3000);

            executionLog.Log("ContactsAdvanceFilterResultsPP", "Click on apply button.");
            office_ContactsHelper.ClickElement("Apply");
            office_ContactsHelper.WaitForWorkAround(3000);

            executionLog.Log("ContactsAdvanceFilterResultsPP", "Verify number of records displayed.");
            office_ContactsHelper.VerifyText("No.ofResults", "Showing 1 - 50 of");
            //office_ContactsHelper.WaitForWorkAround(3000);

            executionLog.Log("ContactsAdvanceFilterResultsPP", "Click on advance filter.");
            office_ContactsHelper.ClickElement("AdvanceFilter");
            office_ContactsHelper.WaitForWorkAround(2000);

            executionLog.Log("ContactsAdvanceFilterResultsPP", "Select number of records to 100.");
            office_ContactsHelper.SelectByText("ResultsPerPage", "100");
            //office_ContactsHelper.WaitForWorkAround(3000);

            executionLog.Log("ContactsAdvanceFilterResultsPP", "Click on apply button.");
            office_ContactsHelper.ClickElement("Apply");
            office_ContactsHelper.WaitForWorkAround(3000);

            executionLog.Log("ContactsAdvanceFilterResultsPP", "Verify number of records displayed.");
            office_ContactsHelper.VerifyText("No.ofResults", "Showing 1 - 100 of");
            //office_ContactsHelper.WaitForWorkAround(3000);

            executionLog.Log("ContactsAdvanceFilterResultsPP", "Logout from the application.");
            VisitOffice("logout");

        }
      catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("ContactsAdvanceFilterResultsPP");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Clients Advance Filter ResultsPP");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Clients Advance Filter ResultsPP", "Bug", "Medium", "Opportunities page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Clients Advance Filter ResultsPP");
                        TakeScreenshot("ContactsAdvanceFilterResultsPP");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ContactsAdvanceFilterResultsPP.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("ContactsAdvanceFilterResultsPP");
                        string id = loginHelper.getIssueID("Clients Advance Filter ResultsPP");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ContactsAdvanceFilterResultsPP.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Clients Advance Filter ResultsPP"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Clients Advance Filter ResultsPP");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("ContactsAdvanceFilterResultsPP");
                executionLog.WriteInExcel("Clients Advance Filter ResultsPP", Status, JIRA, "Opportunities Management");
            }
        }
    }
}