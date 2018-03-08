using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class CallsAdvanceFilterRelatedTo : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin")]
        public void callsAdvanceFilterRelatedTo()
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
            var officeActivities_CallsHelper = new OfficeActivities_CallsHelper(GetWebDriver());

            // Variable
            var DocName = "Test Exe" + GetRandomNumber();
            var fileUpl = GetPathToFile() + "chrome.exe";
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("CallsAdvanceFilterRelatedTo", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("CallsAdvanceFilterRelatedTo", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                // Verify calls with clients.

                executionLog.Log("CallsAdvanceFilterRelatedTo", "Redirect To URL");
                VisitOffice("calls");
                officeActivities_CallsHelper.WaitForWorkAround(3000);

                executionLog.Log("CallsAdvanceFilterRelatedTo", "Verify page title.");
                VerifyTitle("Calls");

                executionLog.Log("CallsAdvanceFilterRelatedTo", "Click on advance filter.");
                officeActivities_CallsHelper.ClickForce("AdvanceFilter");
                officeActivities_CallsHelper.WaitForWorkAround(2000);

                executionLog.Log("CallsAdvanceFilterRelatedTo", "click calls with activity type.");
                officeActivities_CallsHelper.ClickForce("CallsWithClients");
                //officeActivities_CallsHelper.WaitForWorkAround(3000);

                executionLog.Log("CallsAdvanceFilterRelatedTo", "Click on apply button.");
                officeActivities_CallsHelper.ClickForce("Apply");
                officeActivities_CallsHelper.WaitForWorkAround(3000);

                executionLog.Log("CallsAdvanceFilterRelatedTo", "Verify calls present is related to clients");
                officeActivities_CallsHelper.VerifyTextEqual("Merchants");

                //Verify calls with contacts.

                executionLog.Log("CallsAdvanceFilterRelatedTo", "Redirect To URL");
                VisitOffice("calls");
                officeActivities_CallsHelper.WaitForWorkAround(3000);

                executionLog.Log("CallsAdvanceFilterRelatedTo", "Verify page title.");
                VerifyTitle("Calls");
                //officeActivities_CallsHelper.WaitForElementVisible("AdvanceFilter", 10);

                executionLog.Log("CallsAdvanceFilterRelatedTo", "Click on advance filter.");
                officeActivities_CallsHelper.ClickForce("AdvanceFilter");
                officeActivities_CallsHelper.WaitForWorkAround(2000);

                executionLog.Log("CallsAdvanceFilterRelatedTo", "Selct calls related to contacts");
                officeActivities_CallsHelper.ClickForce("CallsWithContacts");
                //officeActivities_CallsHelper.WaitForWorkAround(3000);

                executionLog.Log("CallsAdvanceFilterRelatedTo", "Click on apply button.");
                officeActivities_CallsHelper.ClickForce("Apply");
                officeActivities_CallsHelper.WaitForWorkAround(3000);

                executionLog.Log("CallsAdvanceFilterRelatedTo", "Verify calls present is related to contacts.");
                officeActivities_CallsHelper.VerifyTextEqual("Contacts");

                //Verify calls with Leads.

                executionLog.Log("CallsAdvanceFilterRelatedTo", "Redirect To URL");
                VisitOffice("calls");
                officeActivities_CallsHelper.WaitForWorkAround(3000);

                executionLog.Log("CallsAdvanceFilterRelatedTo", "Verify page title.");
                VerifyTitle("Calls");
                //officeActivities_CallsHelper.WaitForElementVisible("AdvanceFilter", 10);

                executionLog.Log("CallsAdvanceFilterRelatedTo", "Click on advance filter.");
                officeActivities_CallsHelper.ClickForce("AdvanceFilter");
                officeActivities_CallsHelper.WaitForWorkAround(2000);

                executionLog.Log("CallsAdvanceFilterRelatedTo", "click on calls with activity type.");
                officeActivities_CallsHelper.ClickForce("CallsWithLeads");
                //officeActivities_CallsHelper.WaitForWorkAround(3000);

                executionLog.Log("CallsAdvanceFilterRelatedTo", "Click on apply button.");
                officeActivities_CallsHelper.ClickForce("Apply");
                officeActivities_CallsHelper.WaitForWorkAround(3000);

                executionLog.Log("CallsAdvanceFilterRelatedTo", "Verify calls present is related to leads.");
                officeActivities_CallsHelper.VerifyTextEqual("Leads");

                // Verify calls with Opportunities .

                executionLog.Log("CallsAdvanceFilterRelatedTo", "Redirect To URL");
                VisitOffice("calls");
                officeActivities_CallsHelper.WaitForWorkAround(3000);

                executionLog.Log("CallsAdvanceFilterRelatedTo", "Verify page title.");
                VerifyTitle("Calls");
                //officeActivities_CallsHelper.WaitForElementVisible("AdvanceFilter", 10);

                executionLog.Log("CallsAdvanceFilterRelatedTo", "Click on advance filter.");
                officeActivities_CallsHelper.ClickForce("AdvanceFilter");
                officeActivities_CallsHelper.WaitForWorkAround(2000);

                executionLog.Log("CallsAdvanceFilterRelatedTo", "click on calls  with opportunities.");
                officeActivities_CallsHelper.ClickForce("CallsWithOppo");
                //officeActivities_CallsHelper.WaitForWorkAround(3000);

                executionLog.Log("CallsAdvanceFilterRelatedTo", "Click on apply button.");
                officeActivities_CallsHelper.ClickElement("Apply");
                officeActivities_CallsHelper.WaitForWorkAround(3000);

                executionLog.Log("CallsAdvanceFilterRelatedTo", "Verify calls present is related to opportunities.");
                officeActivities_CallsHelper.VerifyTextEqual("Opportunities");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CallsAdvanceFilterRelatedTo");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("CallsAdvanceFilterRelatedTo");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("CallsAdvanceFilterRelatedTo", "Bug", "Medium", "Opportunities page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("CallsAdvanceFilterRelatedTo");
                        TakeScreenshot("CallsAdvanceFilterRelatedTo");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CallsAdvanceFilterRelatedTo.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CallsAdvanceFilterRelatedTo");
                        string id = loginHelper.getIssueID("CallsAdvanceFilterRelatedTo");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CallsAdvanceFilterRelatedTo.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("CallsAdvanceFilterRelatedTo"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("CallsAdvanceFilterRelatedTo");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("CallsAdvanceFilterRelatedTo");
                executionLog.WriteInExcel("CallsAdvanceFilterRelatedTo", Status, JIRA, "Opportunities Management");
            }
        }
    }
}