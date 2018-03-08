using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class LabelSelectforClient : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS2")]
        [TestCategory("BugTestNew")]
        public void labelSelectforClient()
        {
            string[] username = null;
            string[] password = null;

            XMLParse oXMLData = new XMLParse();
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

                executionLog.Log("LabelSelectforClient", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("LabelSelectforClient", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("LabelSelectforClient", "Redirect at clients page.");
                VisitOffice("clients");

                executionLog.Log("LabelSelectforClient", "Search the Company");
                office_ClientsHelper.TypeText("SearchClient", "Client");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("LabelSelectforClient", "Click on any Client");
                office_ClientsHelper.ClickElement("Client1");
                office_ClientsHelper.WaitForWorkAround(5000);

                executionLog.Log("LabelSelectforClient", "Verify Select for category");
                office_ClientsHelper.VerifyText("CategoryClient", "Select");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("LabelSelectforClient");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Label Select for Client");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Label Select for Client", "Bug", "Medium", "Client page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Label Select for Client");
                        TakeScreenshot("LabelSelectforClient");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\LabelSelectforClient.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("LabelSelectforClient");
                        string id = loginHelper.getIssueID("Label Select for Client");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\LabelSelectforClient.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Label Select for Client"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Label Select for Client");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("LabelSelectforClient");
                executionLog.WriteInExcel("Label Select for Client", Status, JIRA, "Clients Management");
            }
        }
    }
}