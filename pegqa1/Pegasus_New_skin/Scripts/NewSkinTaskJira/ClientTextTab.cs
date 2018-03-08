using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class ClientTextTab : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS4")]
        [TestCategory("NewSkinTaskJira")]
        public void clientTextTab()
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
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("ClientTextTab", "Login with valid username and password");
                Login(username[0], password[0]);

                executionLog.Log("ClientTextTab", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("ClientTextTab", "Go to client page");
                VisitOffice("clients");
                
                executionLog.Log("ClientTextTab", "Verify title");
                VerifyTitle();

                executionLog.Log("ClientTextTab", "Open a client");
                office_ClientsHelper.ClickElement("Client1");

                executionLog.Log("ClientTextTab", "Verify title");
                VerifyTitle(" Details");

                executionLog.Log("ClientTextTab", "Click on 'Owner' tab");
                office_ClientsHelper.ClickElement("OwnerTab");

                executionLog.Log("ClientTextTab", "Verify title");
                VerifyTitle(" Owners");

                executionLog.Log("ClientTextTab", "Verify text displayed properly");
                office_ClientsHelper.verifyElementVisible("TextOwnwerInfo");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("ClientTextTab");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Client Text Tab");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Client Text Tab", "Bug", "Medium", "Client page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Client Text Tab");
                        TakeScreenshot("ClientTextTab");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ClientTextTab.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("ClientTextTab");
                        string id = loginHelper.getIssueID("Client Text Tab");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ClientTextTab.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Client Text Tab"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Client Text Tab");
                executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("ClientTextTab");
                executionLog.WriteInExcel("Client Text Tab", Status, JIRA, "Client Management");
            }
        }
    }
}