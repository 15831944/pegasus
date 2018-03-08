using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class OpportunitiesEmailUrlChange : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Url")]
        [TestCategory("Fail")]
        [TestCategory("TS4")]
        [TestCategory("ChangeUrl")]
        public void opportunitiesEmailUrlChange()
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
            var OfficeActivities_EmailHelper = new OfficeActivities_EmailsHelper(GetWebDriver());

            // Variable
            var SendTo = "Test" + GetRandomNumber() + "@yopmail.com";
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("OpportunitiesEmailUrlChange", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("OpportunitiesEmailUrlChange", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("OpportunitiesEmailUrlChange", "Goto User Opportunities");
                VisitOffice("opportunities");
                office_OpportunitiesHelper.WaitForWorkAround(3000);

                executionLog.Log("OpportunitiesEmailUrlChange", "Click On Any Opportunity");
                office_OpportunitiesHelper.ClickElement("OpenOpportunity");
                office_OpportunitiesHelper.WaitForWorkAround(3000);

                executionLog.Log("OpportunitiesEmailUrlChange", "Click On Send Email");
                office_OpportunitiesHelper.ClickElement("SendEmail");
                office_OpportunitiesHelper.WaitForWorkAround(3000);

                executionLog.Log("OpportunitiesEmailUrlChange", "Enter Email Id");
                OfficeActivities_EmailHelper.TypeText("Sendto", "Test@yopmal.com");
                OfficeActivities_EmailHelper.WaitForWorkAround(2000);

                executionLog.Log("OpportunitiesEmailUrlChange", "Enter  Subject");
                OfficeActivities_EmailHelper.TypeText("EmailName", SendTo);
                //OfficeActivities_EmailHelper.WaitForWorkAround(2000);

                executionLog.Log("OpportunitiesEmailUrlChange", "Click Save");
                OfficeActivities_EmailHelper.ClickElement("SendEmailActivity");
                OfficeActivities_EmailHelper.WaitForWorkAround(3000);

                executionLog.Log("OpportunitiesEmailUrlChange", "Select the active type");
                OfficeActivities_EmailHelper.SelectByText("SelectActivityType", "E-Mails");
                OfficeActivities_EmailHelper.WaitForWorkAround(2000);

                executionLog.Log("OpportunitiesEmailUrlChange", "Click On E-mail ");
                OfficeActivities_EmailHelper.ClickJS("ClickEmail1Oppo");
                OfficeActivities_EmailHelper.WaitForWorkAround(3000);

                executionLog.Log("OpportunitiesEmailUrlChange", "Change the url with the url number of another office");
                VisitOffice("mails/view/57");
                office_OpportunitiesHelper.WaitForWorkAround(1000);

                executionLog.Log("OpportunitiesEmailUrlChange", "Verify Validation");
                OfficeActivities_EmailHelper.WaitForText("You don't have privileges to view this E-Mail.", 10);
                OfficeActivities_EmailHelper.WaitForWorkAround(1000);

                executionLog.Log("OpportunitiesEmailUrlChange", "Redirect to URL");
                VisitOffice("mails/sent");
                OfficeActivities_EmailHelper.WaitForWorkAround(3000);

                executionLog.Log("OpportunitiesEmailUrlChange", "Verify page title");
                VerifyTitle("Sent");
                //OfficeActivities_EmailHelper.WaitForWorkAround(4000);

                executionLog.Log("OpportunitiesEmailUrlChange", "Enter Send to in search ");
                OfficeActivities_EmailHelper.TypeText("SearchMailInput", SendTo);
                OfficeActivities_EmailHelper.WaitForWorkAround(1000);

                executionLog.Log("OpportunitiesEmailUrlChange", "Click on search btn");
                OfficeActivities_EmailHelper.ClickElement("SearchBtn");
                office_OpportunitiesHelper.WaitForWorkAround(3000);

                executionLog.Log("OpportunitiesEmailUrlChange", "Select searched email.");
                OfficeActivities_EmailHelper.ClickJS("CheckBox1");
                //OfficeActivities_EmailHelper.WaitForWorkAround(4000);

                executionLog.Log("OpportunitiesEmailUrlChange", "Click on delete btn");
                OfficeActivities_EmailHelper.ClickElement("Delete");
                OfficeActivities_EmailHelper.WaitForWorkAround(3000);

                executionLog.Log("OpportunitiesEmailUrlChange", "Verify Email Deleted successfully");
                OfficeActivities_EmailHelper.WaitForText("E-Mail has been moved to the Recycle Bin.", 05);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("OpportunitiesEmailUrlChange");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Opportunities Email Url Change");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Opportunities Email Url Change", "Bug", "Medium", "Opportunities page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Opportunities Email Url Change");
                        TakeScreenshot("OpportunitiesEmailUrlChange");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\OpportunitiesEmailUrlChange.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("OpportunitiesEmailUrlChange");
                        string id = loginHelper.getIssueID("Opportunities Email Url Change");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\OpportunitiesEmailUrlChange.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Opportunities Email Url Change"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Opportunities Email Url Change");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("OpportunitiesEmailUrlChange");
                executionLog.WriteInExcel("Opportunities Email Url Change", Status, JIRA, "Office Opportunities");
            }
        }
    }
}

