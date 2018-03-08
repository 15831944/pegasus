using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class LeadsDocumentUrlChange : DriverTestCase
    {
        [TestMethod]
        [TestCategory("Url")]
        [TestCategory("All")]
        [TestCategory("Fail")]
        [TestCategory("TS3")]
        [TestCategory("ChangeUrl")]
        public void leadsDocumentUrlChange()
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
            var office_LeadsHelper = new Office_LeadsHelper(GetWebDriver());

            // Variable
            var File = GetPathToFile() + "leadslist.csv";
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("LeadsDocumentUrlChange", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("LeadsDocumentUrlChange", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("LeadsDocumentUrlChange", "Go to All Leads");
                VisitOffice("leads");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("LeadsDocumentUrlChange", "Click On Any lead");
                office_LeadsHelper.ClickElement("ClickAnyLead");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("LeadsDocumentUrlChange", "Click On Add Document");
                office_LeadsHelper.ClickElement("AddDocument");
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("LeadsDocumentUrlChange", "Enter Document Name");
                office_LeadsHelper.TypeText("EnterDocuName", "Document Test");

                executionLog.Log("LeadsDocumentUrlChange", "Upload File");
                office_LeadsHelper.UploadFile("//*[@id='DocumentFiles']", File);
                //office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("LeadsDocumentUrlChange", "Click Save");
                office_LeadsHelper.ClickDisplayed("//*[@id='CreateDocumentForm']/div[4]/button[1]");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("LeadsDocumentUrlChange", "Select Activity >> Document");
                office_LeadsHelper.Select("SelectActivityType", "Documents");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("LeadsDocumentUrlChange", "select All in created by field");
                office_LeadsHelper.SelectByText("CreatedByField", "All");
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("LeadsDocumentUrlChange", "Click On Document");
                office_LeadsHelper.ClickJS("ClickNotes1");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("LeadsDocumentUrlChange", "Change the url with the url number of another office");
                VisitOffice("documents/view/41");

                executionLog.Log("LeadsDocumentUrlChange", "Verify Validation");
                office_LeadsHelper.WaitForText("You don't have privilege.", 10);

                executionLog.Log("LeadsDocumentUrlChange", "Redirect to Clients Page");
                VisitOffice("leads");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("LeadsDocumentUrlChange", "Click On Any lead");
                office_LeadsHelper.ClickElement("ClickAnyLead");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("LeadsDocumentUrlChange", "Select the Document in activity type");
                office_LeadsHelper.SelectByText("SelectActivityType", "Documents");
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("LeadsDocumentUrlChange", "Click On Document ");
                office_LeadsHelper.PressEnter("ClickNotes1");
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("LeadsDocumentUrlChange", "Click OnDelete icon");
                office_LeadsHelper.ClickElement("DeleteDoc");

                executionLog.Log("LeadsDocumentUrlChange", "Accept alert message");
                office_LeadsHelper.AcceptAlert();

                executionLog.Log("LeadsDocumentUrlChange", "Wait for delete message");
                office_LeadsHelper.WaitForText("Document deleted successfully.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("LeadsDocumentUrlChange");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Leads Document Url Change");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Leads Document Url Change", "Bug", "Medium", "Leads page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Leads Document Url Change");
                        TakeScreenshot("LeadsDocumentUrlChange");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\LeadsDocumentUrlChange.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("LeadsDocumentUrlChange");
                        string id = loginHelper.getIssueID("Leads Document Url Change");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\LeadsDocumentUrlChange.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Leads Document Url Change"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Leads Document Url Change");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("LeadsDocumentUrlChange");
                executionLog.WriteInExcel("Leads Document Url Change", Status, JIRA, "Leads Management");
            }
        }
    }
}
