using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class PartnerAgentCodeInQuickLook : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("SELENIUM_TESTCASE")]
        [TestCategory("TS8")]
        public void partnerAgentCodeInQuickLook()
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

                executionLog.Log("PartnerAgentCodeInQuickLook", "Login with valid credential  Username");
                Login(username[0], password[0]);

                executionLog.Log("PartnerAgentCodeInQuickLook", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("PartnerAgentCodeInQuickLook", "Goto Opportinuties");
                VisitOffice("opportunities");

                executionLog.Log("PartnerAgentCodeInQuickLook", "Open Opportunity");
                office_OpportunitiesHelper.ClickElement("Opportunities1");
                office_OpportunitiesHelper.WaitForWorkAround(5000);

                executionLog.Log("PartnerAgentCodeInQuickLook", "Edit Opportunity");
                office_OpportunitiesHelper.clickJS("OppEdit");
                office_OpportunitiesHelper.WaitForWorkAround(3000);
                executionLog.Log("PartnerAgentCodeInQuickLook", "Enter Partner Agent Code.");
                office_OpportunitiesHelper.TypeText("PartnerAgentCode_Field", "233");

                executionLog.Log("PartnerAgentCodeInQuickLook", "Select the status");
                office_OpportunitiesHelper.SelectByText("State", "New");

                executionLog.Log("PartnerAgentCodeInQuickLook", "Click On Save Button");
                office_OpportunitiesHelper.clickJS("ClickSaveClient");
                office_OpportunitiesHelper.WaitForWorkAround(9000);

                executionLog.Log("PartnerAgentCodeInQuickLook", "Verify added code in Quick Look.");
                office_OpportunitiesHelper.VerifyPageText("223");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("AmexRateCorp");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Amex Rate Corp");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Amex Rate Corp", "Bug", "Medium", "Amex page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Amex Rate Corp");
                        TakeScreenshot("AmexRateCorp");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AmexRateCorp.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("AmexRateCorp");
                        string id = loginHelper.getIssueID("Amex Rate Corp");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AmexRateCorp.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Amex Rate Corp"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Amex Rate Corp");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("AmexRateCorp");
                executionLog.WriteInExcel("Amex Rate Corp", Status, JIRA, "Corp Master Data");
            }
        }
    }
}
