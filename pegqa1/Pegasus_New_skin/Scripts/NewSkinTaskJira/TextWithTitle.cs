using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class TextWithTitle : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("Test")]
        [TestCategory("TS5")]
        [TestCategory("NewSkinTaskJira")]
        public void textWithTitle()
        {
            string[] username = null;
            string[] password = null;
            String JIRA = "";
            String Status = "Pass";

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

            try
            {
                executionLog.Log("TextWithTitle", "Login with valid username and password");
                Login(username[0], password[0]);

                executionLog.Log("TextWithTitle", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("TextWithTitle", "Go to client page");
                VisitOffice("clients");

                executionLog.Log("TextWithTitle", "Verify title clients");
                VerifyTitle();

                executionLog.Log("TextWithTitle", "Open a client");
                office_ClientsHelper.ClickElement("Client1");

                executionLog.Log("TextWithTitle", "Click on 'Owner' tab");
                office_ClientsHelper.ClickForce("OwnerTab");

                executionLog.Log("TextWithTitle", "Verify title owners");
                VerifyTitle("Owners");

                executionLog.Log("TextWithTitle", "Verify Text field in header not available");
                office_ClientsHelper.verifyElementNotPresent("NoText");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("TextWithTitle");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Text with Title");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Text with Title", "Bug", "Medium", "Clients page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Text with Title");
                        TakeScreenshot("TextWithTitle");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\TextWithTitle.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("TextWithTitle");
                        string id = loginHelper.getIssueID("Text with Title");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\TextWithTitle.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Text with Title"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Text with Title");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("TextWithTitle");
                executionLog.WriteInExcel("Text with Title", Status, JIRA, "Client Management");
            }
        }
    }
}