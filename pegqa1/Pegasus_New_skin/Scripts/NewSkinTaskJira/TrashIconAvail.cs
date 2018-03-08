using System;
using System.IO;
using LinqToExcel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;


namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class TrashIconAvail : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS5")]
        [TestCategory("NewSkinTaskJira")]
        public void trashIconAvail()
        {
            string[] username = null;
            string[] password = null;
            string[] log = null;
            String JIRA = "";
            String Status = "Pass";

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");
            log = oXMLData.getData("settings/URL", "logout");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var officeActivities_DocumentHelper = new OfficeActivities_DocumentHelper(GetWebDriver());

            var Name = "DocTest" + RandomNumber(10, 500);

            try
            {
                executionLog.Log("TrashIconAvail", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("TrashIconAvail", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("TrashIconAvail", "Go to document page");
                VisitOffice("documents");
                officeActivities_DocumentHelper.WaitForWorkAround(3000);

                executionLog.Log("TrashIconAvail", "verify title");
                VerifyTitle("Documents");

                executionLog.Log("TrashIconAvail", "Open Docuemnt");
                officeActivities_DocumentHelper.ClickElement("OpenDoc");
                officeActivities_DocumentHelper.WaitForWorkAround(3000);

                executionLog.Log("TrashIconAvail", "verify title");
                VerifyTitle("Document View");

                executionLog.Log("TrashIconAvail", "Verify trash icon available");
                officeActivities_DocumentHelper.verifyElementPresent("Trash");

                executionLog.Log("TrashIconAvail", "Log out from the application");
                VisitOffice("logout");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("TrashIconAvail");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Trash Icon Avail");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Trash Icon Avail", "Bug", "Medium", "Documents page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Trash Icon Avail");
                        TakeScreenshot("TrashIconAvail");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\TrashIconAvail.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("TrashIconAvail");
                        string id = loginHelper.getIssueID("Trash Icon Avail");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\TrashIconAvail.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Trash Icon Avail"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Trash Icon Avail");
             //   executionLog.DeleteFile("Error");
                
                throw;

            }
            finally
            {
                executionLog.DeleteFile("TrashIconAvail");
                executionLog.WriteInExcel("Trash Icon Avail", Status, JIRA, "Office Activities");
            }
        }
    }
}

