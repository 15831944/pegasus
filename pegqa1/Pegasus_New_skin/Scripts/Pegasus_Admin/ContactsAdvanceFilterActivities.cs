using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class ContactsAdvanceFilterActivities : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("Fail")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin")]
        public void contactsAdvanceFilterActivities()
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

            //try
            //{
                executionLog.Log("ContactsAdvanceFilterActivities", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("ContactsAdvanceFilterActivities", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                // Verify leads with notes.

                executionLog.Log("ContactsAdvanceFilterActivities", "Redirect To URL");
                VisitOffice("contacts");
                office_ContactsHelper.WaitForWorkAround(3000);

                executionLog.Log("ContactsAdvanceFilterActivities", "Verify page title.");
                VerifyTitle("Contacts");

                executionLog.Log("ContactsAdvanceFilterActivities", "Click on advance filter.");
                office_ContactsHelper.ClickElement("AdvanceFilter");
                office_ContactsHelper.WaitForWorkAround(2000);

                executionLog.Log("ContactsAdvanceFilterActivities", "Click on show activiities button.");
                office_ContactsHelper.CheckAndClick("ShowActivities");
                office_ContactsHelper.WaitForWorkAround(1000);

                executionLog.Log("ContactsAdvanceFilterActivities", "Selct contact activity type.");
                office_ContactsHelper.ClickElement("ContactWithNotes");
                //office_ContactsHelper.WaitForWorkAround(3000);

                executionLog.Log("ContactsAdvanceFilterActivities", "Click on apply button.");
                office_ContactsHelper.ClickElement("Apply");
                office_ContactsHelper.WaitForWorkAround(3000);

                executionLog.Log("ContactsAdvanceFilterActivities", "Click on any contact.");
                office_ContactsHelper.ClickElement("Contact1");
                office_ContactsHelper.WaitForWorkAround(3000);

                executionLog.Log("ContactsAdvanceFilterActivities", "Select actitivity type as notes.");
                office_ContactsHelper.SelectByText("SelectActivityType", "Notes");
                office_ContactsHelper.WaitForWorkAround(2000);

                executionLog.Log("ContactsAdvanceFilterActivities", "Select All in created by field");
                office_ContactsHelper.selectByText("CreatedByField", "All");
                office_ContactsHelper.WaitForWorkAround(2000);

                executionLog.Log("ContactsAdvanceFilterActivities", "Verify notes present for contact.");
                office_ContactsHelper.WaitForElementVisible("NotesContacts", 10);


                // Verify contacts with open tasks.

                executionLog.Log("ContactsAdvanceFilterActivities", "Redirect To URL");
                VisitOffice("contacts");
                office_ContactsHelper.WaitForWorkAround(3000);

                executionLog.Log("ContactsAdvanceFilterActivities", "Verify page title.");
                VerifyTitle("Contacts");
                //office_ContactsHelper.WaitForWorkAround(1000);

                executionLog.Log("ContactsAdvanceFilterActivities", "Click on advance filter.");
                office_ContactsHelper.ClickElement("AdvanceFilter");
                office_ContactsHelper.WaitForWorkAround(2000);

                executionLog.Log("ContactsAdvanceFilterActivities", "Click on show activiities button.");
                office_ContactsHelper.CheckAndClick("ShowActivities");
                office_ContactsHelper.WaitForWorkAround(1000);

                executionLog.Log("ContactsAdvanceFilterActivities", "Selct contact activity type.");
                office_ContactsHelper.ClickElement("ContactWithTask");
                //office_ContactsHelper.WaitForWorkAround(3000);

                executionLog.Log("ContactsAdvanceFilterActivities", "Click on apply button.");
                office_ContactsHelper.ClickElement("Apply");
                office_ContactsHelper.WaitForWorkAround(3000);

                executionLog.Log("ContactsAdvanceFilterActivities", "Click on any contact.");
                office_ContactsHelper.ClickElement("Contact1");
                office_ContactsHelper.WaitForWorkAround(3000);

                executionLog.Log("ContactsAdvanceFilterActivities", "Select actitivity type as notes.");
                office_ContactsHelper.SelectByText("SelectActivityType", "Tasks");
                office_ContactsHelper.WaitForWorkAround(2000);

                executionLog.Log("ContactsAdvanceFilterActivities", "Select All in created by field");
                office_ContactsHelper.selectByText("CreatedByField", "All");
                office_ContactsHelper.WaitForWorkAround(2000);

                executionLog.Log("ContactsAdvanceFilterActivities", "Verify notes present for contact.");
                office_ContactsHelper.WaitForElementVisible("TasksContacts", 10);

                // Verify contacts with closed tasks.

                //executionLog.Log("ContactsAdvanceFilterActivities", "Redirect To URL");
                //VisitOffice("contacts");
                //office_ContactsHelper.WaitForWorkAround(3000);

                //executionLog.Log("ContactsAdvanceFilterActivities", "Verify page title.");
                //VerifyTitle("Contacts");

                //executionLog.Log("ContactsAdvanceFilterActivities", "Click on advance filter.");
                //office_ContactsHelper.ClickElement("AdvanceFilter");
                //office_ContactsHelper.WaitForWorkAround(2000);

                //executionLog.Log("ContactsAdvanceFilterActivities", "Click on show activiities button.");
                //office_ContactsHelper.CheckAndClick("ShowActivities");
                //office_ContactsHelper.WaitForWorkAround(1000);

                //executionLog.Log("ContactsAdvanceFilterActivities", "Selct contact activity type.");
                //office_ContactsHelper.ClickElement("ContactsWithCLosedTasks");
                ////office_ContactsHelper.WaitForWorkAround(3000);

                //executionLog.Log("ContactsAdvanceFilterActivities", "Click on apply button.");
                //office_ContactsHelper.ClickElement("Apply");
                //office_ContactsHelper.WaitForWorkAround(3000);

                //executionLog.Log("ContactsAdvanceFilterActivities", "Click on any contact.");
                //office_ContactsHelper.ClickElement("Contact1");
                //office_ContactsHelper.WaitForWorkAround(3000);

                //executionLog.Log("ContactsAdvanceFilterActivities", "Select actitivity type as notes.");
                //office_ContactsHelper.SelectByText("SelectActivityType", "Tasks");
                //office_ContactsHelper.WaitForWorkAround(2000);

                //executionLog.Log("ContactsAdvanceFilterActivities", "Select All in created by field");
                //office_ContactsHelper.selectByText("CreatedByField", "All");
                //office_ContactsHelper.WaitForWorkAround(2000);

                //executionLog.Log("ContactsAdvanceFilterActivities", "Verify notes present for contact.");
                //office_ContactsHelper.WaitForElementVisible("TasksContacts", 10);

                // Verify contacts with documents.

                executionLog.Log("ContactsAdvanceFilterActivities", "Redirect To URL");
                VisitOffice("contacts");
                office_ContactsHelper.WaitForWorkAround(3000);

                executionLog.Log("ContactsAdvanceFilterActivities", "Verify page title.");
                VerifyTitle("Contacts");
                //office_ContactsHelper.WaitForWorkAround(1000);

                executionLog.Log("ContactsAdvanceFilterActivities", "Click on advance filter.");
                office_ContactsHelper.ClickElement("AdvanceFilter");
                office_ContactsHelper.WaitForWorkAround(2000);

                executionLog.Log("ContactsAdvanceFilterActivities", "Click on show activiities button.");
                office_ContactsHelper.CheckAndClick("ShowActivities");
                office_ContactsHelper.WaitForWorkAround(4000);

                executionLog.Log("ContactsAdvanceFilterActivities", "Selct contact activity type.");
                office_ContactsHelper.ClickElement("ContactWithDocs");
                //office_ContactsHelper.WaitForWorkAround(3000);

                executionLog.Log("ContactsAdvanceFilterActivities", "Click on apply button.");
                office_ContactsHelper.ClickElement("Apply");
                office_ContactsHelper.WaitForWorkAround(3000);

                executionLog.Log("ContactsAdvanceFilterActivities", "Click on any contact.");
                office_ContactsHelper.ClickElement("Contact1");
                office_ContactsHelper.WaitForWorkAround(3000);

                executionLog.Log("ContactsAdvanceFilterActivities", "Select actitivity type as notes.");
                office_ContactsHelper.SelectByText("SelectActivityType", "Documents");
                office_ContactsHelper.WaitForWorkAround(2000);

                executionLog.Log("ContactsAdvanceFilterActivities", "Select All in created by field");
                office_ContactsHelper.selectByText("CreatedByField", "All");
                office_ContactsHelper.WaitForWorkAround(2000);

                executionLog.Log("ContactsAdvanceFilterActivities", "Verify notes present for contact.");
                office_ContactsHelper.WaitForElementVisible("DOcumentsContacts", 10);

                // Verify contacts with emails.

                executionLog.Log("ContactsAdvanceFilterActivities", "Redirect To URL");
                VisitOffice("contacts");
                office_ContactsHelper.WaitForWorkAround(3000);

                executionLog.Log("ContactsAdvanceFilterActivities", "Verify page title.");
                VerifyTitle("Contacts");

                executionLog.Log("ContactsAdvanceFilterActivities", "Click on advance filter.");
                office_ContactsHelper.ClickElement("AdvanceFilter");
                office_ContactsHelper.WaitForWorkAround(2000);

                executionLog.Log("ContactsAdvanceFilterActivities", "Click on show activiities button.");
                office_ContactsHelper.CheckAndClick("ShowActivities");
                office_ContactsHelper.WaitForWorkAround(1000);

                executionLog.Log("ContactsAdvanceFilterActivities", "Selct contact activity type.");
                office_ContactsHelper.ClickElement("ContactWithEmails");
                //office_ContactsHelper.WaitForWorkAround(3000);

                executionLog.Log("ContactsAdvanceFilterActivities", "Click on apply button.");
                office_ContactsHelper.ClickElement("Apply");
                office_ContactsHelper.WaitForWorkAround(3000);

                executionLog.Log("ContactsAdvanceFilterActivities", "Click on any contact.");
                office_ContactsHelper.ClickElement("Contact1");
                office_ContactsHelper.WaitForWorkAround(3000);

                executionLog.Log("ContactsAdvanceFilterActivities", "Select actitivity type as notes.");
                office_ContactsHelper.SelectByText("SelectActivityType", "E-Mails");
                office_ContactsHelper.WaitForWorkAround(2000);

                executionLog.Log("ContactsAdvanceFilterActivities", "Select All in created by field");
                office_ContactsHelper.selectByText("CreatedByField", "All");
                office_ContactsHelper.WaitForWorkAround(2000);

                executionLog.Log("ContactsAdvanceFilterActivities", "Verify notes present for contact.");
                office_ContactsHelper.WaitForElementVisible("EmailsContacts", 10);
                //office_ContactsHelper.WaitForWorkAround(3000);
            //}
            //catch (Exception e)
            //{
            //    executionLog.Log("Error", e.StackTrace);
            //    Status = "Fail";

            //    String counter = executionLog.readLastLine("counter");
            //    String Description = executionLog.GetAllTextFile("ContactsAdvanceFilterActivities");
            //    String Error = executionLog.GetAllTextFile("Error");
            //    Console.WriteLine(Error);
            //    if (counter == "")
            //    {
            //        counter = "0";
            //    }
            //    bool result = loginHelper.CheckExstingIssue("Contacts Advance Filter Activities");
            //    if (!result)
            //    {
            //        if (Int16.Parse(counter) < 9)
            //        {
            //            executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
            //            loginHelper.CreateIssue("Contacts Advance Filter Activities", "Bug", "Medium", "Opportunities page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
            //            string id = loginHelper.getIssueID("Contacts Advance Filter Activities");
            //            TakeScreenshot("ContactsAdvanceFilterActivities");
            //            string directoryName = loginHelper.GetnewDirectoryName(GetPath());
            //            var location = directoryName + "\\ContactsAdvanceFilterActivities.png";
            //            loginHelper.AddAttachment(location, id);
            //        }
            //    }
            //    else
            //    {
            //        if (Int16.Parse(counter) < 9)
            //        {
            //            executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
            //            TakeScreenshot("ContactsAdvanceFilterActivities");
            //            string id = loginHelper.getIssueID("Contacts Advance Filter Activities");
            //            string directoryName = loginHelper.GetnewDirectoryName(GetPath());
            //            var location = directoryName + "\\ContactsAdvanceFilterActivities.png";
            //            loginHelper.AddAttachment(location, id);
            //            loginHelper.AddComment(loginHelper.getIssueID("Contacts Advance Filter Activities"), "This issue is still occurring");
            //        }
            //    }
            //    JIRA = loginHelper.getIssueID("Contacts Advance Filter Activities");
            //    //    executionLog.DeleteFile("Error");
            //    throw;

            //}
            //finally
            //{
            //    executionLog.DeleteFile("ContactsAdvanceFilterActivities");
            //    executionLog.WriteInExcel("Contacts Advance Filter Activities", Status, JIRA, "Opportunities Management");
            //}
        }
    }
}
