using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class ClientDocumentUrlChange : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Url")]
        [TestCategory("Fail")]
        [TestCategory("TS3")]
        [TestCategory("ChangeUrl")]
        public void clientDocumentUrlChange()
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
            var office_ClientsHelper = new Office_ClientsHelper(GetWebDriver());

            // Variable

            var FirstName = "Test" + GetRandomNumber();
            var LastName = "Tester" + GetRandomNumber();
            var Number = "12345678" + GetRandomNumber();

            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("ClientDocumentUrlChange", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("ClientDocumentUrlChange", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("ClientDocumentUrlChange", "Goto User Agent >> Clients");
                VisitOffice("clients");

                executionLog.Log("ClientDocumentUrlChange", "Click On Any Client");
                office_ClientsHelper.ClickElement("Client1");
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("ClientDocumentUrlChange", "Click On Add Document");
                office_ClientsHelper.ClickElement("AddDoc");

                var DocName = "Document Test" + GetRandomNumber();
                executionLog.Log("ClientDocumentUrlChange", "Enter Document Name");
                office_ClientsHelper.TypeText("DocName", DocName);

                var File = GetPathToFile() + "index.jpg";
                executionLog.Log("ClientDocumentUrlChange", "Upload File");
                office_ClientsHelper.Upload("BrowseFile", File);
                office_ClientsHelper.WaitForWorkAround(1000);

                executionLog.Log("ClientDocumentUrlChange", "Click Save");
                office_ClientsHelper.ClickElement("SaveDoc");
                office_ClientsHelper.WaitForWorkAround(1000);

                executionLog.Log("ClientDocumentUrlChange", "Click On Document ");
                office_ClientsHelper.PressEnter("ClientDoc1");
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("ClientDocumentUrlChange", "Change the url with the url number of another office");
                VisitOffice("documents/view/41");
                office_ClientsHelper.WaitForWorkAround(1000);

                executionLog.Log("ClientDocumentUrlChange", "Verify Validation");
                office_ClientsHelper.VerifyPageText("You don't have privilege.");
                office_ClientsHelper.WaitForWorkAround(4000);

                executionLog.Log("ClientDocumentUrlChange", "Redirect to Clients Page");
                VisitOffice("clients");

                executionLog.Log("ClientDocumentUrlChange", "Click On Any Client");
                office_ClientsHelper.ClickElement("Client1");
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("ClientDocumentUrlChange", "Click On Document ");
                office_ClientsHelper.PressEnter("ClientDoc1");
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("ClientDocumentUrlChange", "Click OnDelete icon");
                office_ClientsHelper.ClickElement("DeleteDoc");

                executionLog.Log("ClientDocumentUrlChange", "Accept alert message");
                office_ClientsHelper.AcceptAlert();

                executionLog.Log("ClientDocumentUrlChange", "Wait for delete message");
                office_ClientsHelper.WaitForText("Document deleted successfully.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("ClientDocumentUrlChange");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Client Document Url Change");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Client Document Url Change", "Bug", "Medium", "Client page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Client Document Url Change");
                        TakeScreenshot("ClientDocumentUrlChange");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ClientDocumentUrlChange.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("ClientDocumentUrlChange");
                        string id = loginHelper.getIssueID("Client Document Url Change");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ClientDocumentUrlChange.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Client Document Url Change"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Client Document Url Change");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("ClientDocumentUrlChange");
                executionLog.WriteInExcel("Client Document Url Change", Status, JIRA, "Client Management");
            }
        }
    }
}

