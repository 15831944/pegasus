using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;
using OpenQA.Selenium;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class ClientNotesUrlChange : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Url")]
        [TestCategory("Fail")]
        [TestCategory("TS3")]
        [TestCategory("ChangeUrl")]
        public void clientNotesUrlChange()
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
            var officeActivities_NotesHelper = new OfficeActivities_NotesHelper(GetWebDriver());

            // Variable

            var Subject = "Subject" + RandomNumber(1, 999);
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("ClientNotesUrlChange", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("ClientNotesUrlChange", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("ClientNotesUrlChange", "Go to Clients");
                VisitOffice("clients");
                office_ClientsHelper.WaitForWorkAround(3000);

                //executionLog.Log("ClientNotesUrlChange", "Search client by company name.");
                //office_ClientsHelper.TypeText("SearchClient", "Chy");
                //officeActivities_NotesHelper.WaitForWorkAround(6000);

                executionLog.Log("ClientNotesUrlChange", "Click On Advanced Filter");
                office_ClientsHelper.ClickElement("AdvanceFilter");
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("ClientNotesUrlChange", "Click On Show Activities");
                office_ClientsHelper.ClickElement("ShowClientActivity");
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("ClientNotesUrlChange", "Click On Client with notes");
                office_ClientsHelper.ClickElement("ClientWithNotes");
                //office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("ClientNotesUrlChange", "Click On Apply Filter");
                office_ClientsHelper.ClickElement("Apply");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("ClientNotesUrlChange", "Click On Any Client");
                office_ClientsHelper.ClickElement("ClickOnAnyClient");
                office_ClientsHelper.WaitForWorkAround(3000);

                //VerifyTitle("Chy Company - Details");

                office_ClientsHelper.SelectByText("SelectActivityType", "Notes");
                office_ClientsHelper.WaitForWorkAround(2000);

                office_ClientsHelper.SelectByText("CreatedByField", "All");
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("ClientNotesUrlChange", "Click On Notes ");
                officeActivities_NotesHelper.ClickViaJavaScript("//table[@id='list1']//td[14]/a");
                officeActivities_NotesHelper.WaitForWorkAround(2000);

                executionLog.Log("ClientNotesUrlChange", "Change the url with the url number of another office");
                VisitOffice("notes/view/765513");
                officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("ClientNotesUrlChange", "Verify Validation");
                officeActivities_NotesHelper.VerifyPageText("You don't have privileges to view this note.");
                officeActivities_NotesHelper.WaitForWorkAround(1000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("ClientNotesUrlChange");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Client Notes Url Change");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Client Notes Url Change", "Bug", "Medium", "Client page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Client Notes Url Change");
                        TakeScreenshot("ClientNotesUrlChange");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ClientNotesUrlChange.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("ClientNotesUrlChange");
                        string id = loginHelper.getIssueID("Client Notes Url Change");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ClientNotesUrlChange.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Client Notes Url Change"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Client Notes Url Change");
                //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("ClientNotesUrlChange");
                executionLog.WriteInExcel("Client Notes Url Change", Status, JIRA, "Client Management");
            }
        }
    }
}