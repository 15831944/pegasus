using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class OpportunitiesnDocumentUrlChange : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Url")]
        [TestCategory("Fail")]
        [TestCategory("TS4")]
        [TestCategory("ChangeUrl")]
        public void opportunitiesnDocumentUrlChange()
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
            var office_OpportunitiesHelper = new Office_OpportunitiesHelper(GetWebDriver());
            var officeActivities_DocumentHelper = new OfficeActivities_DocumentHelper(GetWebDriver());

            // Variable
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("OpportunitiesnDocumentUrlChange", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("OpportunitiesnDocumentUrlChange", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("OpportunitiesnDocumentUrlChange", "Goto User Opportunities");
                VisitOffice("opportunities");
                office_OpportunitiesHelper.WaitForWorkAround(4000);

                executionLog.Log("OpportunitiesnDocumentUrlChange", "Click On Any Opportunity");
                office_OpportunitiesHelper.ClickElement("OpenOpportunity");
                office_OpportunitiesHelper.WaitForWorkAround(2000);

                executionLog.Log("OpportunitiesnDocumentUrlChange", "Click On Add Document");
                office_OpportunitiesHelper.ClickElement("AddDocument");
                office_OpportunitiesHelper.WaitForWorkAround(2000);

                executionLog.Log("OpportunitiesnDocumentUrlChange", "Enter Document Name");
                officeActivities_DocumentHelper.TypeText("Name", "Document Test");

                var File = GetPathToFile() + "index.jpg";
                executionLog.Log("OpportunitiesnDocumentUrlChange", "Upload File");
                officeActivities_DocumentHelper.Upload("BrowseFile", File);
                officeActivities_DocumentHelper.WaitForWorkAround(1000);

                executionLog.Log("OpportunitiesnDocumentUrlChange", "Click Save");
                officeActivities_DocumentHelper.ClickOnDisplayed("SaveDocOppo");
                officeActivities_DocumentHelper.WaitForWorkAround(2000);

                executionLog.Log("OpportunitiesnDocumentUrlChange", "Wait for success message.");
                officeActivities_DocumentHelper.WaitForText("Documents successfully Added.", 10);

                executionLog.Log("OpportunitiesnDocumentUrlChange", "Open the document");
                officeActivities_DocumentHelper.PressEnter("ClickDocument1");
                officeActivities_DocumentHelper.WaitForWorkAround(1000);

                executionLog.Log("OpportunitiesnDocumentUrlChange", "Change the url with the url number of another office");
                VisitOffice("documents/view/41");

                executionLog.Log("OpportunitiesnDocumentUrlChange", "Verify Validation");
                officeActivities_DocumentHelper.WaitForText("You don't have privilege.", 10);

                executionLog.Log("OpportunitiesnDocumentUrlChange", "Redirect to opportunity Page");
                VisitOffice("opportunities");
                officeActivities_DocumentHelper.WaitForWorkAround(2000);

                executionLog.Log("OpportunitiesnDocumentUrlChange", "Click On Any Opportunity");
                office_OpportunitiesHelper.ClickElement("OpenOpportunity");
                office_OpportunitiesHelper.WaitForWorkAround(2000);

                executionLog.Log("OpportunitiesnDocumentUrlChange", "Click On Document ");
                officeActivities_DocumentHelper.PressEnter("ClickDocument1");
                officeActivities_DocumentHelper.WaitForWorkAround(2000);

                executionLog.Log("OpportunitiesnDocumentUrlChange", "Click OnDelete icon");
                officeActivities_DocumentHelper.ClickElement("DeleteDocumentopp");

                executionLog.Log("OpportunitiesnDocumentUrlChange", "Accept alert message");
                officeActivities_DocumentHelper.AcceptAlert();
                officeActivities_DocumentHelper.WaitForWorkAround(4000);

                executionLog.Log("OpportunitiesnDocumentUrlChange", "Wait for delete message");
                officeActivities_DocumentHelper.WaitForText("Document deleted successfully.", 5);

            }

            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("OpportunitiesnDocumentUrlChange");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Opportunities Document Url Change");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Opportunities Document Url Change", "Bug", "Medium", "Opportunities page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Opportunities Document Url Change");
                        TakeScreenshot("OpportunitiesnDocumentUrlChange");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\OpportunitiesnDocumentUrlChange.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("OpportunitiesnDocumentUrlChange");
                        string id = loginHelper.getIssueID("Opportunities Document Url Change");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\OpportunitiesnDocumentUrlChange.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Opportunities Document Url Change"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Opportunities Document Url Change");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("OpportunitiesnDocumentUrlChange");
                executionLog.WriteInExcel("Opportunities Document Url Change", Status, JIRA, "Office Opportunities");
            }
        }
    }
}
