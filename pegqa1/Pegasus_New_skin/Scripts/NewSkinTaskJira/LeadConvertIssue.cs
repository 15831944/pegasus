using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class LeadConvertIssue : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS5")]
        [TestCategory("NewSkinTaskJira")]
        public void leadConvertIssue()
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
            var office_LeadsHelper = new Office_LeadsHelper(GetWebDriver());

            // VARIABLE
            var name = "TestEmployee" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("LeadConvertIssue", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("LeadConvertIssue", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("LeadConvertIssue", "Redirect To leads page");
                VisitOffice("leads");

                executionLog.Log("LeadConvertIssue", "Verify title");
                VerifyTitle("Leads");

                executionLog.Log("LeadConvertIssue", "Open Lead");
                office_LeadsHelper.ClickElement("ClickAnyLead");

                executionLog.Log("LeadConvertIssue", "Verify title");
                VerifyTitle(" Details");

                executionLog.Log("LeadConvertIssue", "Click on Pdf tab");
                office_LeadsHelper.ClickElement("PDFTab");

                executionLog.Log("LeadConvertIssue", "Verify Title");
                VerifyTitle(" Pdfs");

                executionLog.Log("LeadConvertIssue", "Click on convert button");
                office_LeadsHelper.ClickElement("ClickOnConvert");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("LeadConvertIssue", "Verify convert button is working");
                office_LeadsHelper.verifyElementVisible("VerifyWork");

            }       
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("LeadConvertIssue");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Lead Convert Issue");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Lead Convert Issue", "Bug", "Medium", "Lead page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Lead Convert Issue");
                        TakeScreenshot("LeadConvertIssue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\LeadConvertIssue.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("LeadConvertIssue");
                        string id = loginHelper.getIssueID("Lead Convert Issue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\LeadConvertIssue.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Lead Convert Issue"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Lead Convert Issue");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("LeadConvertIssue");
                executionLog.WriteInExcel("Lead Convert Issue", Status, JIRA, "Leads Management");
            }
        }
    }
}
    