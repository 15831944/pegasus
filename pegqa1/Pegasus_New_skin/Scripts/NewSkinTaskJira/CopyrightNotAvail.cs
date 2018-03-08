using System;
using System.IO;
using LinqToExcel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;


namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class CopyrightNotAvail : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS4")]
        [TestCategory("NewSkinTaskJira")]
        public void copyrightNotAvail()
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
                executionLog.Log("CopyrightNotAvail", "Login with valid username and password");
                Login(username[0], password[0]);

                executionLog.Log("CopyrightNotAvail", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("CopyrightNotAvail", "Go to client page");
                VisitOffice("clients");

                executionLog.Log("CopyrightNotAvail", "verify title");
                VerifyTitle();

                executionLog.Log("CopyrightNotAvail", "Open client");
                office_ClientsHelper.ClickElement("Client1");

                executionLog.Log("CopyrightNotAvail", "verify title");
                VerifyTitle(" - Details");

                executionLog.Log("CopyrightNotAvail", "Verify copyright text not available");
                office_ClientsHelper.VerifyTextNotPresent("Copyright @");

                executionLog.Log("CopyrightNotAvail", "Log out from the application");
                VisitOffice("logout");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CopyrightNotAvail");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Copy right Not Avail");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Copy right Not Avail", "Bug", "Medium", "Client page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Copy right Not Avail");
                        TakeScreenshot("CopyrightNotAvail");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CopyrightNotAvail.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CopyrightNotAvail");
                        string id = loginHelper.getIssueID("Copy right Not Avail");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CopyrightNotAvail.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Copy right Not Avail"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Copy right Not Avail");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("CopyrightNotAvail");
                executionLog.WriteInExcel("Copy right Not Avail", Status, JIRA,"Client Management");
            }
        }
    }
}