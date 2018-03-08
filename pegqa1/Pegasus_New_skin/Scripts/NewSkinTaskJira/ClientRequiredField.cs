using System;
using System.IO;
using LinqToExcel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class ClientRequiredField : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS4")]
        [TestCategory("NewSkinTaskJira")]
        public void clientRequiredField()
        {
            string[] username = null;
            string[] password = null;
            string[] log = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");
            log = oXMLData.getData("settings/URL", "logout");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var office_ClientsHelper = new Office_ClientsHelper(GetWebDriver());
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("ClientRequiredField", "Login with valid username and password");
                Login(username[0], password[0]);

                executionLog.Log("ClientRequiredField", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("ClientRequiredField", "Go to create a client page");
                VisitOffice("clients/create");

                executionLog.Log("ClientRequiredField", "Verify title");
                VerifyTitle("Create a Client");

                executionLog.Log("ClientRequiredField", "Click on Save button without entering any details");
                office_ClientsHelper.ClickElement("Save");

                executionLog.Log("ClientRequiredField", "Verify error displayed for 'Status field'");
                office_ClientsHelper.verifyElementPresent("StatusError");
                office_ClientsHelper.verifyElementPresent("StatusMandatory");

                executionLog.Log("ClientRequiredField", "Logout from the application");
                VisitOffice("logout");
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("ClientRequiredField");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Client Required Field");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Client Required Field", "Bug", "Medium", "Client page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Client Required Field");
                        TakeScreenshot("ClientRequiredField");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ClientRequiredField.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("ClientRequiredField");
                        string id = loginHelper.getIssueID("Client Required Field");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ClientRequiredField.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Client Required Field"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Client Required Field");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("ClientRequiredField");
                executionLog.WriteInExcel("Client Required Field", Status, JIRA, "Client Management");
            }
        }
    }
}