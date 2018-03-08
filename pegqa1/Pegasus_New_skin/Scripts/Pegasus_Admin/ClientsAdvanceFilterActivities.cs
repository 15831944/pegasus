using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class ClientsAdvanceFilterActivities : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin")]
        public void clientsAdvanceFilterActivities()
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

            // Variable
            var DocName = "Test Exe" + GetRandomNumber();
            var fileUpl = GetPathToFile() + "chrome.exe";
            var ClientNote = "Client Note" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
            executionLog.Log("ClientsAdvanceFilterActivities", "Login with valid username and password");
            Login(username[0], password[0]);
            Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

            executionLog.Log("ClientsAdvanceFilterActivities", "Verify Page title");
            VerifyTitle("Dashboard");
            Console.WriteLine("Redirected at Dashboard screen.");

            executionLog.Log("ClientsAdvanceFilterActivities", "Redirect To URL");
            VisitOffice("clients");
            office_ClientsHelper.WaitForWorkAround(3000);

            executionLog.Log("ClientsAdvanceFilterActivities", "Verify page title.");
            VerifyTitle("Clients");
            //office_ClientsHelper.WaitForWorkAround(3000);

            //Verify clients with notes.

            executionLog.Log("ClientsAdvanceFilterActivities", "Click on advance filter.");
            office_ClientsHelper.ClickForce("AdvanceFilter");
            office_ClientsHelper.WaitForWorkAround(2000);

            executionLog.Log("ClientsAdvanceFilterActivities", "Click on show activiities button.");
            office_ClientsHelper.ClickForce("ShowClientActivity");
            office_ClientsHelper.WaitForWorkAround(2000);

            executionLog.Log("ClientsAdvanceFilterActivities", "Selct client with activity type.");
            office_ClientsHelper.ClickForce("ClientWithNotes");
            //office_ClientsHelper.WaitForWorkAround(3000);

            executionLog.Log("ClientsAdvanceFilterActivities", "Click on apply button.");
            office_ClientsHelper.ClickForce("Apply");
            office_ClientsHelper.WaitForWorkAround(3000);

            executionLog.Log("ClientsAdvanceFilterActivities", "Click on any client.");
            office_ClientsHelper.ClickForce("Client1");
            office_ClientsHelper.WaitForWorkAround(3000);

            executionLog.Log("ClientsAdvanceFilterActivities", "Select actitivity type as notes.");
            office_ClientsHelper.SelectByText("SelectActivityType", "Notes");
            office_ClientsHelper.WaitForWorkAround(2000);

            executionLog.Log("ClientsAdvanceFilterActivities", "Select All in Created By field");
            office_ClientsHelper.SelectByText("CreatedByField", "All");
            office_ClientsHelper.WaitForWorkAround(2000);

            executionLog.Log("ClientsAdvanceFilterActivities", "Verify notes present for clients.");
            office_ClientsHelper.WaitForElementVisible("NoteClient",5);
            
            // Verifies client with open meetings.
            executionLog.Log("ClientsAdvanceFilterActivities", "Redirect To URL");
            VisitOffice("clients");
            office_ClientsHelper.WaitForWorkAround(3000);

            executionLog.Log("ClientsAdvanceFilterActivities", "Verify page title.");
            VerifyTitle("Clients");

            executionLog.Log("ClientsAdvanceFilterActivities", "Click on advance filter.");
            office_ClientsHelper.ClickForce("AdvanceFilter");
            office_ClientsHelper.WaitForWorkAround(2000);

            executionLog.Log("ClientsAdvanceFilterActivities", "Click on show activiities button.");
            office_ClientsHelper.ClickForce("ShowClientActivity");
            office_ClientsHelper.WaitForWorkAround(2000);

            executionLog.Log("ClientsAdvanceFilterActivities", "Selct client with activity type.");
            office_ClientsHelper.ClickForce("ClientOpenMeet");
            //office_ClientsHelper.WaitForWorkAround(3000);

            executionLog.Log("ClientsAdvanceFilterActivities", "Click on apply button.");
            office_ClientsHelper.ClickForce("Apply");
            office_ClientsHelper.WaitForWorkAround(3000);

            executionLog.Log("ClientsAdvanceFilterActivities", "Click on any client.");
            office_ClientsHelper.ClickForce("Client1");
            office_ClientsHelper.WaitForWorkAround(3000);

            executionLog.Log("ClientsAdvanceFilterActivities", "Select actitivity type as meetings.");
            office_ClientsHelper.SelectByText("SelectActivityType", "Meetings");
            office_ClientsHelper.WaitForWorkAround(2000);

            executionLog.Log("ClientsAdvanceFilterActivities", "Select All in Created By field");
            office_ClientsHelper.SelectByText("CreatedByField", "All");
            office_ClientsHelper.WaitForWorkAround(2000);

            executionLog.Log("ClientsAdvanceFilterActivities", "Verify meetings present for clients.");
            office_ClientsHelper.WaitForElementVisible("MeetingClient",5);

            // Verifies client with closed meetings.
            executionLog.Log("ClientsAdvanceFilterActivities", "Redirect To URL");
            VisitOffice("clients");
            office_ClientsHelper.WaitForWorkAround(5000);

            executionLog.Log("ClientsAdvanceFilterActivities", "Verify page title.");
            VerifyTitle();

            executionLog.Log("ClientsAdvanceFilterActivities", "Click on advance filter.");
            office_ClientsHelper.ClickForce("AdvanceFilter");
            office_ClientsHelper.WaitForWorkAround(3000);

            executionLog.Log("ClientsAdvanceFilterActivities", "Click on show activiities button.");
            office_ClientsHelper.ClickForce("ShowClientActivity");
            office_ClientsHelper.WaitForWorkAround(3000);

            executionLog.Log("ClientsAdvanceFilterActivities", "Selct client with activity type.");
            office_ClientsHelper.ClickForce("ClientCLoseMeet");
            office_ClientsHelper.WaitForWorkAround(3000);

            executionLog.Log("ClientsAdvanceFilterActivities", "Click on apply button.");
            office_ClientsHelper.ClickForce("Apply");
            office_ClientsHelper.WaitForWorkAround(3000);

            executionLog.Log("ClientsAdvanceFilterActivities", "Click on any client.");
            office_ClientsHelper.ClickForce("Client1");
            office_ClientsHelper.WaitForWorkAround(3000);

            executionLog.Log("ClientsAdvanceFilterActivities", "Select actitivity type as meetings.");
            office_ClientsHelper.SelectByText("SelectActivityType", "Meetings");
            office_ClientsHelper.WaitForWorkAround(3000);

            executionLog.Log("ClientsAdvanceFilterActivities", "Select All in Created By field");
            office_ClientsHelper.SelectByText("CreatedByField", "All");
            office_ClientsHelper.WaitForWorkAround(2000);

            executionLog.Log("ClientsAdvanceFilterActivities", "Verify meeting present for clients.");
           // office_ClientsHelper.WaitForElementVisible("MeetingClient",5);

            // Verifies client with open tasks.

            executionLog.Log("ClientsAdvanceFilterActivities", "Redirect To URL");
            VisitOffice("clients");
            office_ClientsHelper.WaitForWorkAround(3000);

            executionLog.Log("ClientsAdvanceFilterActivities", "Verify page title.");
            VerifyTitle("Clients");

            executionLog.Log("ClientsAdvanceFilterActivities", "Click on advance filter.");
            office_ClientsHelper.ClickForce("AdvanceFilter");
            office_ClientsHelper.WaitForWorkAround(2000);

            executionLog.Log("ClientsAdvanceFilterActivities", "Click on show activiities button.");
            office_ClientsHelper.ClickForce("ShowClientActivity");
            office_ClientsHelper.WaitForWorkAround(2000);

            executionLog.Log("ClientsAdvanceFilterActivities", "Selct client with activity type.");
            office_ClientsHelper.ClickForce("ClientOpenTask");
            //office_ClientsHelper.WaitForWorkAround(3000);

            executionLog.Log("ClientsAdvanceFilterActivities", "Click on apply button.");
            office_ClientsHelper.ClickForce("Apply");
            office_ClientsHelper.WaitForWorkAround(3000);

            executionLog.Log("ClientsAdvanceFilterActivities", "Click on any client.");
            office_ClientsHelper.ClickForce("Client1");
            office_ClientsHelper.WaitForWorkAround(3000);

            executionLog.Log("ClientsAdvanceFilterActivities", "Select actitivity type as tasks.");
            office_ClientsHelper.SelectByText("SelectActivityType", "Tasks");
            office_ClientsHelper.WaitForWorkAround(2000);

            executionLog.Log("ClientsAdvanceFilterActivities", "Select All in Created By field");
            office_ClientsHelper.SelectByText("CreatedByField", "All");
            office_ClientsHelper.WaitForWorkAround(2000);

            executionLog.Log("ClientsAdvanceFilterActivities", "Verify tasks present for clients.");
            office_ClientsHelper.WaitForElementVisible("TasksClient",5);

            // Verifies client with closed tasks.

          //  executionLog.Log("ClientsAdvanceFilterActivities", "Redirect To URL");
          //  VisitOffice("clients");
          //  office_ClientsHelper.WaitForWorkAround(5000);

          //  executionLog.Log("ClientsAdvanceFilterActivities", "Verify page title.");
          //  VerifyTitle();

          //  executionLog.Log("ClientsAdvanceFilterActivities", "Click on advance filter.");
          //  office_ClientsHelper.ClickForce("AdvanceFilter");
          //  office_ClientsHelper.WaitForWorkAround(3000);

          //  executionLog.Log("ClientsAdvanceFilterActivities", "Click on show activiities button.");
          //  office_ClientsHelper.ClickForce("ShowClientActivity");
          //  office_ClientsHelper.WaitForWorkAround(3000);

          //  executionLog.Log("ClientsAdvanceFilterActivities", "Selct client with activity type.");
          //  office_ClientsHelper.ClickForce("ClientCLoseTask");
          //  office_ClientsHelper.WaitForWorkAround(3000);

          //  executionLog.Log("ClientsAdvanceFilterActivities", "Click on apply button.");
          //  office_ClientsHelper.ClickForce("Apply");
          //  office_ClientsHelper.WaitForWorkAround(3000);

          //  executionLog.Log("ClientsAdvanceFilterActivities", "Click on any client.");
          //  office_ClientsHelper.ClickForce("Client1");
          //  office_ClientsHelper.WaitForWorkAround(3000);

          //  executionLog.Log("ClientsAdvanceFilterActivities", "Select actitivity type as tasks.");
          //  office_ClientsHelper.SelectByText("SelectActivityType", "Tasks");
          //  office_ClientsHelper.WaitForWorkAround(2000);

          //  executionLog.Log("ClientsAdvanceFilterActivities", "Select All in Created By field");
          //  office_ClientsHelper.SelectByText("CreatedByField", "All");
          //  office_ClientsHelper.WaitForWorkAround(2000);

          //  executionLog.Log("ClientsAdvanceFilterActivities", "Verify tasks present for clients.");
          ////  office_ClientsHelper.WaitForElementVisible("TasksClient",5);

            // Verifies client with documents.

            executionLog.Log("ClientsAdvanceFilterActivities", "Redirect To URL");
            VisitOffice("clients");
            office_ClientsHelper.WaitForWorkAround(3000);

            executionLog.Log("ClientsAdvanceFilterActivities", "Verify page title.");
            VerifyTitle("Clients");

            executionLog.Log("ClientsAdvanceFilterActivities", "Click on advance filter.");
            office_ClientsHelper.ClickForce("AdvanceFilter");
            office_ClientsHelper.WaitForWorkAround(2000);

            executionLog.Log("ClientsAdvanceFilterActivities", "Click on show activiities button.");
            office_ClientsHelper.ClickForce("ShowClientActivity");
            office_ClientsHelper.WaitForWorkAround(2000);

            executionLog.Log("ClientsAdvanceFilterActivities", "Selct client with activity type.");
            office_ClientsHelper.ClickForce("ClientWithDoc");
            //office_ClientsHelper.WaitForWorkAround(3000);

            executionLog.Log("ClientsAdvanceFilterActivities", "Click on apply button.");
            office_ClientsHelper.ClickForce("Apply");
            office_ClientsHelper.WaitForWorkAround(3000);

            executionLog.Log("ClientsAdvanceFilterActivities", "Click on any client.");
            office_ClientsHelper.ClickForce("Client1");
            office_ClientsHelper.WaitForWorkAround(3000);

            executionLog.Log("ClientsAdvanceFilterActivities", "Select actitivity type as documents.");
            office_ClientsHelper.SelectByText("SelectActivityType", "Documents");
            office_ClientsHelper.WaitForWorkAround(2000);

            executionLog.Log("ClientsAdvanceFilterActivities", "Select All in Created By field");
            office_ClientsHelper.SelectByText("CreatedByField", "All");
            office_ClientsHelper.WaitForWorkAround(2000);

            executionLog.Log("ClientsAdvanceFilterActivities", "Verify documents present for clients.");
            office_ClientsHelper.WaitForElementVisible("DocsClient",5);
            
            // Verifies client with emails.

            executionLog.Log("ClientsAdvanceFilterActivities", "Redirect To URL");
            VisitOffice("clients");
            office_ClientsHelper.WaitForWorkAround(5000);

            executionLog.Log("ClientsAdvanceFilterActivities", "Verify page title.");
            VerifyTitle("Clients");

            executionLog.Log("ClientsAdvanceFilterActivities", "Click on advance filter.");
            office_ClientsHelper.ClickForce("AdvanceFilter");
            office_ClientsHelper.WaitForWorkAround(2000);

            executionLog.Log("ClientsAdvanceFilterActivities", "Click on show activiities button.");
            office_ClientsHelper.ClickForce("ShowClientActivity");
            office_ClientsHelper.WaitForWorkAround(2000);

            executionLog.Log("ClientsAdvanceFilterActivities", "Selct client with activity type.");
            office_ClientsHelper.ClickForce("ClientsWithEmail");
            //office_ClientsHelper.WaitForWorkAround(3000);

            executionLog.Log("ClientsAdvanceFilterActivities", "Click on apply button.");
            office_ClientsHelper.ClickForce("Apply");
            office_ClientsHelper.WaitForWorkAround(3000);

            executionLog.Log("ClientsAdvanceFilterActivities", "Click on any client.");
            office_ClientsHelper.ClickForce("Client1");
            office_ClientsHelper.WaitForWorkAround(3000);

            executionLog.Log("ClientsAdvanceFilterActivities", "Select actitivity type as emails.");
            office_ClientsHelper.SelectByText("SelectActivityType", "E-Mails");
            office_ClientsHelper.WaitForWorkAround(2000);

            executionLog.Log("ClientsAdvanceFilterActivities", "Select All in Created By field");
            office_ClientsHelper.SelectByText("CreatedByField", "All");
            office_ClientsHelper.WaitForWorkAround(2000);

            executionLog.Log("ClientsAdvanceFilterActivities", "Verify email present for clients.");
            office_ClientsHelper.WaitForElementVisible("EmailClient",5);

        }
      catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("ClientsAdvanceFilterActivities");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Clients Advance Filter Activities");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Clients Advance Filter Activities", "Bug", "Medium", "Opportunities page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Clients Advance Filter Activities");
                        TakeScreenshot("ClientsAdvanceFilterActivities");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ClientsAdvanceFilterActivities.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("ClientsAdvanceFilterActivities");
                        string id = loginHelper.getIssueID("Clients Advance Filter Activities");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ClientsAdvanceFilterActivities.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Clients Advance Filter Activities"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Clients Advance Filter Activities");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("ClientsAdvanceFilterActivities");
                executionLog.WriteInExcel("Clients Advance Filter Activities", Status, JIRA, "Opportunities Management");
            }
        }
    }
} 