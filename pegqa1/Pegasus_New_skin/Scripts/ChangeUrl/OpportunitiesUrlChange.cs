using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class OpportunitiesUrlChange : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Url")]
        [TestCategory("TS4")]
        [TestCategory("ChangeUrl")]
        public void opportunitiesUrlChange()
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
            var office_OpportunitiesHelper = new Office_OpportunitiesHelper(GetWebDriver());

            // Variable
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("OpportunitiesUrlChange", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("OpportunitiesUrlChange", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("OpportunitiesUrlChange", "Goto User Opportunities");
                VisitOffice("opportunities");

                executionLog.Log("OpportunitiesUrlChange", "Click On Any Opportunity");
                office_OpportunitiesHelper.ClickElement("OpenOpportunity");
                office_OpportunitiesHelper.WaitForWorkAround(2000);

                executionLog.Log("OpportunitiesUrlChange", "Change the url with the url number of another office");
                VisitOffice("opportunities/view/63347");

                executionLog.Log("OpportunitiesUrlChange", "Verify Validation");
                office_OpportunitiesHelper.WaitForText("You don't have privileges to View this Opportunity.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("OpportunitiesUrlChange");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Opportunities Url Change");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Opportunities Url Change", "Bug", "Medium", "Opportunities page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Opportunities Url Change");
                        TakeScreenshot("OpportunitiesUrlChange");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\OpportunitiesUrlChange.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("OpportunitiesUrlChange");
                        string id = loginHelper.getIssueID("Opportunities Url Change");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\OpportunitiesUrlChange.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Opportunities Url Change"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Opportunities Url Change");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("OpportunitiesUrlChange");
                executionLog.WriteInExcel("Opportunities Url Change", Status, JIRA, "Office opportunities");
            }
        }
    }
}
