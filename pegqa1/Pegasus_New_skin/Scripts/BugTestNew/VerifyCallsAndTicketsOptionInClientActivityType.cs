using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class VerifyCallsAndTicketsOptionInClientActivityType : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("BugTestNew")]
        public void verifyCallsAndTicketsOptionInClientActivityType()
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

            // Variable random
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("VerifyCallsAndTicketsOptionInClientActivityType", "Login with valid username and password");
                Login(username[0], password[0]);

                executionLog.Log("VerifyCallsAndTicketsOptionInClientActivityType", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("VerifyCallsAndTicketsOptionInClientActivityType", "Redirect at All merchants page");
                VisitOffice("clients");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCallsAndTicketsOptionInClientActivityType", "Open any merchant");
                office_ClientsHelper.ClickElement("Client1");
                office_ClientsHelper.WaitForWorkAround(4000);

                executionLog.Log("VerifyCallsAndTicketsOptionInClientActivityType", "Click on Client History button");
                office_ClientsHelper.ClickElement("ClientHistory");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCallsAndTicketsOptionInClientActivityType", "Verify Calls and Tickets option present");
                office_ClientsHelper.VerifyText("ActivityType", "Calls");
                office_ClientsHelper.VerifyText("ActivityType", "Tickets");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyCallsAndTicketsOptionInClientActivityType");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Calls And Tickets Option In Client Activity Type");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Calls And Tickets Option In Client Activity Type", "Bug", "Medium", "Office Merchant page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Calls And Tickets Option In Client Activity Type");
                        TakeScreenshot("VerifyCallsAndTicketsOptionInClientActivityType");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyCallsAndTicketsOptionInClientActivityType.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyCallsAndTicketsOptionInClientActivityType");
                        string id = loginHelper.getIssueID("Verify Calls And Tickets Option In Client Activity Type");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyCallsAndTicketsOptionInClientActivityType.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Calls And Tickets Option In Client Activity Type"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Calls And Tickets Option In Client Activity Type");
                //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyCallsAndTicketsOptionInClientActivityType");
                executionLog.WriteInExcel("Verify Calls And Tickets Option In Client Activity Type", Status, JIRA, "Office Merchants");
            }
        }
    }
}
