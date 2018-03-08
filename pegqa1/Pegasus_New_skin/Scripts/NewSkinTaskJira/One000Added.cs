using System;
using System.IO;
using LinqToExcel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class One000Added : DriverTestCase
    {
        [TestMethod]
        [TestCategory("NewSkin_Task")]
        [TestCategory("All")]
        [TestCategory("TS5")]
        [TestCategory("NewSkinTaskJira")]
        public void one000Added()
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
            var office_ContactsHelper = new Office_ContactsHelper(GetWebDriver());
            var officeTickets_AllTicketsHelper = new OfficeTickets_AllTicketsHelper(GetWebDriver());

            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("One000Added", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("One000Added", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("One000Added", "Go to client page");
                VisitOffice("clients");

                executionLog.Log("One000Added", "Verify title");
                VerifyTitle();

                executionLog.Log("One000Added", "Click on Advance filter");
                office_ClientsHelper.ClickElement("Advance");

                executionLog.Log("One000Added", "Veirfy website is available under the filter");
                office_ClientsHelper.verifyElementPresent("PegResult");

                executionLog.Log("One000Added", "Go to contact page");
                VisitOffice("contacts");

                executionLog.Log("One000Added", "Verify title");
                VerifyTitle("Contacts");

                executionLog.Log("One000Added", "Click on Advance filter");
                office_ContactsHelper.ClickElement("Advance");

                executionLog.Log("One000Added", "Veirfy website is available under the filter");
                office_ContactsHelper.verifyElementPresent("PegResult");

                executionLog.Log("One000Added", "Go to Tickets page");
                VisitOffice("tickets");

                executionLog.Log("One000Added", "Verify title");
                VerifyTitle("Tickets");

                executionLog.Log("One000Added", "Click on Advance filter");
                officeTickets_AllTicketsHelper.ClickElement("Advance");

                executionLog.Log("One000Added", "Veirfy website is available under the filter");
                officeTickets_AllTicketsHelper.verifyElementPresent("PegResult");

                executionLog.Log("One000Added", "Logout from the application");
                VisitOffice("logout");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("One000Added");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("One 000 Added");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("One 000 Added", "Bug", "Medium", "Advance filter page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("One 000 Added");
                        TakeScreenshot("One000Added");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\One000Added.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("One000Added");
                        string id = loginHelper.getIssueID("One 000 Added");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\One000Added.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("One 000 Added"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("One 000 Added");
           //     executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("One000Added");
                executionLog.WriteInExcel("One 000 Added", Status, JIRA, "Client Management");
            }
        }
    }
}