using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class ImportClientsFromCSV : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void importClientsFromCSV()
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
                executionLog.Log("ImportClientsFromCSV", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("ImportClientsFromCSV", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("ImportClientsFromCSV", "Click on Clients tab");
                VisitOffice("clients");

                executionLog.Log("ImportClientsFromCSV", "Wait for import to present.");
                office_ClientsHelper.WaitForElementPresent("ClickOnImport", 10);

                executionLog.Log("ImportClientsFromCSV", "Click On Import");
                office_ClientsHelper.ClickElement("ClickOnImport");
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("ImportClientsFromCSV", "Upload file");
                var Path = GetPathToFile() + "clientsamples(2).csv";
                office_ClientsHelper.Upload("SelectFile", Path);
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("ImportClientsFromCSV", "Click On Import");
                office_ClientsHelper.ClickElement("ClickOnImportClint");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("ImportClientsFromCSV", "Click on import process.");
                office_ClientsHelper.ClickElement("ClickImprtProcess");
                office_ClientsHelper.WaitForWorkAround(20000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("ImportClientsFromCSV");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Import Clients From CSV");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Import Clients From CSV", "Bug", "Medium", "Client page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Import Clients From CSV");
                        TakeScreenshot("ImportClientsFromCSV");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ImportClientsFromCSV.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("ImportClientsFromCSV");
                        string id = loginHelper.getIssueID("Import Clients From CSV");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ImportClientsFromCSV.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Import Clients From CSV"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Import Clients From CSV");
               // executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("ImportClientsFromCSV");
                executionLog.WriteInExcel("Import Clients From CSV", Status, JIRA, "Client Management");
            }
        }
    }
}