using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class DocumentsAdvanceFilterRelatedTo : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin")]
        public void documentsAdvanceFilterRelatedTo()
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
            var officeActivities_DocumentHelper = new OfficeActivities_DocumentHelper(GetWebDriver());

            // Variable
            var DocName = "Test Exe" + GetRandomNumber();
            var fileUpl = GetPathToFile() + "chrome.exe";
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("DocumentsAdvanceFilterRelatedTo", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("DocumentsAdvanceFilterRelatedTo", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                // Verify documents with clients.

                executionLog.Log("DocumentsAdvanceFilterRelatedTo", "Redirect To URL");
                VisitOffice("documents");
                officeActivities_DocumentHelper.WaitForWorkAround(3000);

                executionLog.Log("DocumentsAdvanceFilterRelatedTo", "Verify page title.");
                VerifyTitle("Documents");
                //officeActivities_DocumentHelper.WaitForElementVisible("AdvanceFilter", 5);

                executionLog.Log("DocumentsAdvanceFilterRelatedTo", "Click on advance filter.");
                officeActivities_DocumentHelper.ClickElement("AdvanceFilter");
                officeActivities_DocumentHelper.WaitForWorkAround(2000);

                executionLog.Log("DocumentsAdvanceFilterRelatedTo", "click document with activity type.");
                officeActivities_DocumentHelper.ClickForce("DocsWithClients");
                //officeActivities_DocumentHelper.WaitForWorkAround(3000);

                executionLog.Log("DocumentsAdvanceFilterRelatedTo", "Click on apply button.");
                officeActivities_DocumentHelper.ClickForce("Apply");
                officeActivities_DocumentHelper.WaitForWorkAround(3000);

                executionLog.Log("DocumentsAdvanceFilterRelatedTo", "Verify document present is related to clients");
                officeActivities_DocumentHelper.VerifyText("RelatedTo", "Merchants");

                //Verify documents with contacts.

                executionLog.Log("DocumentsAdvanceFilterRelatedTo", "Redirect To URL");
                VisitOffice("documents");
                officeActivities_DocumentHelper.WaitForWorkAround(3000);

                executionLog.Log("DocumentsAdvanceFilterRelatedTo", "Verify page title.");
                VerifyTitle("Documents");
                //officeActivities_DocumentHelper.WaitForElementVisible("AdvanceFilter", 5);

                executionLog.Log("DocumentsAdvanceFilterRelatedTo", "Click on advance filter.");
                officeActivities_DocumentHelper.ClickForce("AdvanceFilter");
                officeActivities_DocumentHelper.WaitForWorkAround(2000);

                executionLog.Log("DocumentsAdvanceFilterRelatedTo", "Selct document related to contacts");
                officeActivities_DocumentHelper.ClickForce("DocsWithContacts");
                //officeActivities_DocumentHelper.WaitForWorkAround(3000);

                executionLog.Log("DocumentsAdvanceFilterRelatedTo", "Click on apply button.");
                officeActivities_DocumentHelper.ClickForce("Apply");
                officeActivities_DocumentHelper.WaitForWorkAround(3000);

                executionLog.Log("DocumentsAdvanceFilterRelatedTo", "Verify document present is related to contacts.");
                officeActivities_DocumentHelper.VerifyText("RelatedTo", "Contact");

                //Verify document with Leads.

                executionLog.Log("DocumentsAdvanceFilterRelatedTo", "Redirect To URL");
                VisitOffice("documents");
                officeActivities_DocumentHelper.WaitForWorkAround(3000);

                executionLog.Log("DocumentsAdvanceFilterRelatedTo", "Verify page title.");
                VerifyTitle("Documents");
                //officeActivities_DocumentHelper.WaitForElementVisible("AdvanceFilter", 5);

                executionLog.Log("DocumentsAdvanceFilterRelatedTo", "Click on advance filter.");
                officeActivities_DocumentHelper.ClickForce("AdvanceFilter");
                officeActivities_DocumentHelper.WaitForWorkAround(2000);

                executionLog.Log("DocumentsAdvanceFilterRelatedTo", "click on document with activity type.");
                officeActivities_DocumentHelper.ClickForce("DocsWithLeads");
                //officeActivities_DocumentHelper.WaitForWorkAround(3000);

                executionLog.Log("DocumentsAdvanceFilterRelatedTo", "Click on apply button.");
                officeActivities_DocumentHelper.ClickForce("Apply");
                officeActivities_DocumentHelper.WaitForWorkAround(5000);

                executionLog.Log("DocumentsAdvanceFilterRelatedTo", "Verify document present is related to leads.");
                officeActivities_DocumentHelper.VerifyText("RelatedTo", "Lead");

                // Verify document with meetings .

                executionLog.Log("DocumentsAdvanceFilterRelatedTo", "Redirect To URL");
                VisitOffice("documents");
                officeActivities_DocumentHelper.WaitForWorkAround(3000);

                executionLog.Log("DocumentsAdvanceFilterRelatedTo", "Verify page title.");
                VerifyTitle("Documents");
                //officeActivities_DocumentHelper.WaitForElementVisible("AdvanceFilter", 5);

                executionLog.Log("DocumentsAdvanceFilterRelatedTo", "Click on advance filter.");
                officeActivities_DocumentHelper.ClickForce("AdvanceFilter");
                officeActivities_DocumentHelper.WaitForWorkAround(2000);

                executionLog.Log("DocumentsAdvanceFilterRelatedTo", "click on document  with meetings.");
                officeActivities_DocumentHelper.ClickForce("DocsWithMeetings");
                //officeActivities_DocumentHelper.WaitForWorkAround(3000);

                executionLog.Log("DocumentsAdvanceFilterRelatedTo", "Click on apply button.");
                officeActivities_DocumentHelper.ClickForce("Apply");
                officeActivities_DocumentHelper.WaitForWorkAround(3000);

                executionLog.Log("DocumentsAdvanceFilterRelatedTo", "Verify docuemnt present is related to meetings.");
                officeActivities_DocumentHelper.VerifyText("RelatedTo", "Meeting");

                // Verify document with Opportunities .

                executionLog.Log("DocumentsAdvanceFilterRelatedTo", "Redirect To URL");
                VisitOffice("documents");
                officeActivities_DocumentHelper.WaitForWorkAround(3000);

                executionLog.Log("DocumentsAdvanceFilterRelatedTo", "Verify page title.");
                VerifyTitle("Documents");
                //officeActivities_DocumentHelper.WaitForElementVisible("AdvanceFilter", 5);

                executionLog.Log("DocumentsAdvanceFilterRelatedTo", "Click on advance filter.");
                officeActivities_DocumentHelper.ClickForce("AdvanceFilter");
                officeActivities_DocumentHelper.WaitForWorkAround(2000);

                executionLog.Log("DocumentsAdvanceFilterRelatedTo", "click on document  with opportunities.");
                officeActivities_DocumentHelper.ClickForce("DocsWithOppo");
                //officeActivities_DocumentHelper.WaitForWorkAround(3000);

                executionLog.Log("DocumentsAdvanceFilterRelatedTo", "Click on apply button.");
                officeActivities_DocumentHelper.ClickForce("Apply");
                officeActivities_DocumentHelper.WaitForWorkAround(3000);

                executionLog.Log("DocumentsAdvanceFilterRelatedTo", "Verify document present is related to opportunities.");
                officeActivities_DocumentHelper.VerifyText("RelatedTo", "Opportunities");

                // Verify document with tasks .

                executionLog.Log("DocumentsAdvanceFilterRelatedTo", "Redirect To URL");
                VisitOffice("documents");
                officeActivities_DocumentHelper.WaitForWorkAround(3000);

                executionLog.Log("DocumentsAdvanceFilterRelatedTo", "Verify page title.");
                VerifyTitle("Documents");
                //officeActivities_DocumentHelper.WaitForElementVisible("AdvanceFilter", 5);

                executionLog.Log("DocumentsAdvanceFilterRelatedTo", "Click on advance filter.");
                officeActivities_DocumentHelper.ClickForce("AdvanceFilter");
                officeActivities_DocumentHelper.WaitForWorkAround(2000);

                executionLog.Log("DocumentsAdvanceFilterRelatedTo", "click on document  with tasks.");
                officeActivities_DocumentHelper.ClickForce("DOcsWithTasks");
                //officeActivities_DocumentHelper.WaitForWorkAround(3000);

                executionLog.Log("DocumentsAdvanceFilterRelatedTo", "Click on apply button.");
                officeActivities_DocumentHelper.ClickForce("Apply");
                officeActivities_DocumentHelper.WaitForWorkAround(3000);

                executionLog.Log("DocumentsAdvanceFilterRelatedTo", "Verify document present is related to tasks.");
                officeActivities_DocumentHelper.VerifyText("RelatedTo", "Task");
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("DocumentsAdvanceFilterRelatedTo");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("DocumentsAdvanceFilterRelatedTo");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("DocumentsAdvanceFilterRelatedTo", "Bug", "Medium", "Opportunities page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("DocumentsAdvanceFilterRelatedTo");
                        TakeScreenshot("DocumentsAdvanceFilterRelatedTo");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\DocumentsAdvanceFilterRelatedTo.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("DocumentsAdvanceFilterRelatedTo");
                        string id = loginHelper.getIssueID("DocumentsAdvanceFilterRelatedTo");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\DocumentsAdvanceFilterRelatedTo.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("DocumentsAdvanceFilterRelatedTo"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("DocumentsAdvanceFilterRelatedTo");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("DocumentsAdvanceFilterRelatedTo");
                executionLog.WriteInExcel("DocumentsAdvanceFilterRelatedTo", Status, JIRA, "Opportunities Management");
            }
        }
    }
}