using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class ClientOwnerSave : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS4")]
        [TestCategory("NewSkinTaskJira")]
        public void clientOwnerSave()
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

            // VARIABLE
            var name = "TestEmployee" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("ClientOwnerSave", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("ClientOwnerSave", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("ClientOwnerSave", "Click on Client tab.");
                office_ClientsHelper.ClickElement("ClickOnClientTab");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("ClientOwnerSave", "Verify title");
                VerifyTitle("Clients");

                executionLog.Log("ClientOwnerSave", "Open client");
                office_ClientsHelper.ClickElement("Client1");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("ClientOwnerSave", "Click on Owner tab");
                office_ClientsHelper.ClickElement("OwnerTab");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("ClientOwnerSave", "Enter First Name");
                office_ClientsHelper.TypeText("OwnerFNameEdit", "Test");

                executionLog.Log("ClientOwnerSave", "Click on Save button");
                office_ClientsHelper.ClickElement("OwnerSave");

                executionLog.Log("ClientOwnerSave", "Veirfy validation message not displayed");
                office_ClientsHelper.VerifyTextNotPresent("You don't have privileges to edit this client");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("ClientOwnerSave");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Client Owner Save");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Client Owner Save", "Bug", "Medium", "Client page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Client Owner Save");
                        TakeScreenshot("ClientOwnerSave");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ClientOwnerSave.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("ClientOwnerSave");
                        string id = loginHelper.getIssueID("Client Owner Save");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ClientOwnerSave.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Client Owner Save"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Client Owner Save");
               // executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("ClientOwnerSave");
                executionLog.WriteInExcel("Client Owner Save", Status, JIRA, "Client Management");
            }
        }
    }
}
