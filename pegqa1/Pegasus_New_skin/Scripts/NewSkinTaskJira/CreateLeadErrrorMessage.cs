using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class CreateLeadErrrorMessage : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS4")]
        [TestCategory("NewSkinTaskJira")]
        public void createLeadErrrorMessage()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            //Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var office_LeadsHelper = new Office_LeadsHelper(GetWebDriver());
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("CreateLeadErrrorMessage", "Login with valid username and password");
                Login(username[0], password[0]);

                executionLog.Log("CreateLeadErrrorMessage", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("CreateLeadErrrorMessage", "Go to create lead page");
                VisitOffice("leads/create");

                executionLog.Log("CreateLeadErrrorMessage", "Verify title");
                VerifyTitle("Create a Lead");

                loginHelper.Click("//button[@title='Save']");
                loginHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateLeadErrrorMessage", "Verify no error message displayed");
                loginHelper.VerifyAlertNotPresent();
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CreateLeadErrrorMessage");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Create Lead Errror Message");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Create Lead Errror Message", "Bug", "Medium", "Lead page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Create Lead Errror Message");
                        TakeScreenshot("CreateLeadErrrorMessage");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateLeadErrrorMessage.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CreateLeadErrrorMessage");
                        string id = loginHelper.getIssueID("Create Lead Errror Message");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateLeadErrrorMessage.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Create Lead Errror Message"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Create Lead Errror Message");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("CreateLeadErrrorMessage");
                executionLog.WriteInExcel("Create Lead Errror Message", Status, JIRA, "Leads Management");
            }
        }
    }
}