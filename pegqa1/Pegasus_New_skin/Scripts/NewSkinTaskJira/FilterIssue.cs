using System;
using System.IO;
using LinqToExcel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;


namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class FilterIssue : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS5")]
        [TestCategory("NewSkinTaskJira")]
        public void filterIssue()
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


            String Status = "Pass";
            String JIRA = "";
            try
            {
                executionLog.Log("FilterIssue", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("FilterIssue", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("FilterIssue", "Go to Client page");
                VisitOffice("clients");

                executionLog.Log("FilterIssue", "verify title");
                VerifyTitle();

                executionLog.Log("FilterIssue", "Click on Filter");
                office_ClientsHelper.ClickElement("Advance");

                executionLog.Log("FilterIssue", "Wait for text");
                office_ClientsHelper.WaitForText("Layout Options", 10);

                if (office_ClientsHelper.IsElementPresent("ProcessorOption"))
                {
                    executionLog.Log("FilterIssue", "Click on processor");
                    office_ClientsHelper.ClickElement("ProcessorOption");

                    executionLog.Log("FilterIssue", "Click on Add arrow");
                    office_ClientsHelper.ClickElement("AddArrow");
                }

                executionLog.Log("FilterIssue", "Click on apply button");
                office_ClientsHelper.ClickElement("Apply");

                executionLog.Log("FilterIssue", "Verify title");
                VerifyTitle();

                executionLog.Log("FilterIssue", "Log out from the application");
                VisitOffice("logout");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";
                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("FilterIssue");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Filter Issue");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Filter Issue", "Bug", "Medium", "Processor page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Filter Issue");
                        TakeScreenshot("FilterIssue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\FilterIssue.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("FilterIssue");
                        string id = loginHelper.getIssueID("Filter Issue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\FilterIssue.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Filter Issue"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Filter Issue");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("FilterIssue");
                executionLog.WriteInExcel("Filter Issue", Status, JIRA, "Client Management");
            }
        }
    }
}
