using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class BulkUpdateSalesManagerForClient : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("Fail")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void bulkUpdateSalesManagerForClient()
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
            var Office_ClientsHelper = new Office_ClientsHelper(GetWebDriver());

            // Variable

            var FirstName = "Test" + GetRandomNumber();
            var LastName = "Tester" + GetRandomNumber();
            var Number = "12345678" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("BulkUpdateSalesManagerForClient", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("BulkUpdateSalesManagerForClient", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("BulkUpdateSalesManagerForClient", "Go to clients page.");
                VisitOffice("clients");

                executionLog.Log("BulkUpdateSalesManagerForClient", "Verify page title.");
                VerifyTitle();

                executionLog.Log("BulkUpdateSalesManagerForClient", "Click On first Check Box");
                Office_ClientsHelper.ClickElement("ClickOnCheckBox");

                executionLog.Log("BulkUpdateSalesManagerForClient", "Click On Bulk Update");
                Office_ClientsHelper.ClickElement("ClickOnBulkUpdateClient");

                executionLog.Log("BulkUpdateSalesManagerForClient", " Click on Change Sale Manager");
                Office_ClientsHelper.ClickElement("BlukUpdateChangeSaleManager");

                executionLog.Log("BulkUpdateSalesManagerForClient", "Select Sales MANAGER");
                Office_ClientsHelper.SelectByText("SelectSaleManager", "Howard Tang");

                executionLog.Log("BulkUpdateSalesManagerForClient", "Click on Update button");
                Office_ClientsHelper.ClickDisplayed("//button[text()='Update']");

                executionLog.Log("BulkUpdateSalesManagerForClient", "Accept alert message.");
                Office_ClientsHelper.AcceptAlert();
                Office_ClientsHelper.WaitForText("records updated successfully", 30);

            }

            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                Console.WriteLine("Counter value is    " + counter);
                String Description = executionLog.GetAllTextFile("BulkUpdateSalesManagerForClient");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Bulk Update Sales Manager For Client");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Bulk Update Sales Manager For Client", "Bug", "Medium", "Client page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Bulk Update Sales Manager For Client");
                        TakeScreenshot("BulkUpdateSalesManagerForClient");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\BulkUpdateSalesManagerForClient.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("BulkUpdateSalesManagerForClient");
                        string id = loginHelper.getIssueID("Bulk Update Sales Manager For Client");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\BulkUpdateSalesManagerForClient.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Bulk Update Sales Manager For Client"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Bulk Update Sales Manager For Client");
                //     executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("BulkUpdateSalesManagerForClient");
                executionLog.WriteInExcel("Bulk Update Sales Manager For Client", Status, JIRA, "Client Management");
            }
        }
    }
}
