using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class VerifyDocumentAdvanceFilterTrackingFields : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin1")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Admin1")]
        public void verifyDocumentAdvanceFilterTrackingFields()
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
            var officeActivities_DocumentHelper = new OfficeActivities_DocumentHelper(GetWebDriver());

            // Variable Random
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("VerifyDocumentsAdvanceFilerColumnOrder", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyDocumentsAdvanceFilerColumnOrder", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyDocumentsAdvanceFilerColumnOrder", "Redirect To URL");
                VisitOffice("documents");
                officeActivities_DocumentHelper.WaitForWorkAround(5000);

                executionLog.Log("VerifyDocumentsAdvanceFilerColumnOrder", "Verify page title.");
                VerifyTitle("Documents");

                executionLog.Log("VerifyDocumentsAdvanceFilerColumnOrder", "Verify status column is visible on the page..");
                officeActivities_DocumentHelper.IsElementPresent("HeadStatus");
                officeActivities_DocumentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyDocumentsAdvanceFilerColumnOrder", "Verify created column is visible on the page.");
                officeActivities_DocumentHelper.IsElementPresent("HeadCreated");
                officeActivities_DocumentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyDocumentsAdvanceFilerColumnOrder", "Verify owner column is visible on the page.");
                officeActivities_DocumentHelper.IsElementPresent("HeadOwner");
                officeActivities_DocumentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyDocumentsAdvanceFilerColumnOrder", "Verify Modified column is visible on the page.");
                officeActivities_DocumentHelper.IsElementPresent("HeadModified");
                officeActivities_DocumentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyDocumentsAdvanceFilerColumnOrder", "Click on advance filter.");
                officeActivities_DocumentHelper.ClickElement("AdvanceFilter");
                officeActivities_DocumentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyDocumentsAdvanceFilerColumnOrder", "Select status in displayed columns.");
                officeActivities_DocumentHelper.SelectByText("DisplayedCols", "Status");
                officeActivities_DocumentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyDocumentsAdvanceFilerColumnOrder", "Click arrow to move column to avail cols.");
                officeActivities_DocumentHelper.ClickElement("RemoveCols");
                officeActivities_DocumentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyDocumentsAdvanceFilerColumnOrder", "Select created in displayed columns.");
                officeActivities_DocumentHelper.SelectByText("DisplayedCols", "Created");
                officeActivities_DocumentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyDocumentsAdvanceFilerColumnOrder", "Click arrow to move column to avail cols");
                officeActivities_DocumentHelper.ClickElement("RemoveCols");
                officeActivities_DocumentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyDocumentsAdvanceFilerColumnOrder", "Select owner in displayed columns.");
                officeActivities_DocumentHelper.SelectByText("DisplayedCols", "Owner");
                officeActivities_DocumentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyDocumentsAdvanceFilerColumnOrder", "Click arrow to move column to avail cols");
                officeActivities_DocumentHelper.ClickElement("RemoveCols");
                officeActivities_DocumentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyDocumentsAdvanceFilerColumnOrder", "Select Modified in displayed columns.");
                officeActivities_DocumentHelper.SelectByText("DisplayedCols", "Modified");
                officeActivities_DocumentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyDocumentsAdvanceFilerColumnOrder", "Click arrow to move column to avail cols");
                officeActivities_DocumentHelper.ClickElement("RemoveCols");
                officeActivities_DocumentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyDocumentsAdvanceFilerColumnOrder", "Click on apply button.");
                officeActivities_DocumentHelper.ClickElement("Apply");
                officeActivities_DocumentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyDocumentsAdvanceFilerColumnOrder", "Verify status not present on page.");
                officeActivities_DocumentHelper.IsElementNotPresent("HeadStatus");
                officeActivities_DocumentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyDocumentsAdvanceFilerColumnOrder", "Verify created not present on page.");
                officeActivities_DocumentHelper.IsElementNotPresent("HeadCreated");
                officeActivities_DocumentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyDocumentsAdvanceFilerColumnOrder", "Verify owner not present on page.");
                officeActivities_DocumentHelper.IsElementNotPresent("HeadOwner");
                officeActivities_DocumentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyDocumentsAdvanceFilerColumnOrder", "Verify Modified not present on page.");
                officeActivities_DocumentHelper.IsElementNotPresent("HeadModified");
                officeActivities_DocumentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyDocumentsAdvanceFilerColumnOrder", "Redirect at leads page.");
                VisitOffice("leads");

                executionLog.Log("VerifyDocumentsAdvanceFilerColumnOrder", "Verify page title as leads.");
                VerifyTitle("Leads");

                executionLog.Log("VerifyDocumentsAdvanceFilerColumnOrder", "Redirect at documents page.");
                VisitOffice("documents");
                officeActivities_DocumentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyDocumentsAdvanceFilerColumnOrder", "Verify page title as documents");
                VerifyTitle("Documents");
                officeActivities_DocumentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyDocumentsAdvanceFilerColumnOrder", "Verify default position of status column.");
                officeActivities_DocumentHelper.IsElementPresent("HeadStatus5");
                officeActivities_DocumentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyDocumentsAdvanceFilerColumnOrder", "Verify default position of owner column.");
                officeActivities_DocumentHelper.IsElementPresent("HeadOwner6");
                officeActivities_DocumentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyDocumentsAdvanceFilerColumnOrder", "Redirect at documents page.");
                VisitOffice("documents");
                officeActivities_DocumentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyDocumentsAdvanceFilerColumnOrder", "Click on advance filter.");
                officeActivities_DocumentHelper.ClickElement("AdvanceFilter");
                officeActivities_DocumentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyDocumentsAdvanceFilerColumnOrder", "Select status in displayed column.");
                officeActivities_DocumentHelper.SelectByText("DisplayedCols", "Status");
                officeActivities_DocumentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyDocumentsAdvanceFilerColumnOrder", "Move status 1 step up.");
                officeActivities_DocumentHelper.ClickElement("MoveUp");
                officeActivities_DocumentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyDocumentsAdvanceFilerColumnOrder", "Move status 1 step up.");
                officeActivities_DocumentHelper.ClickElement("MoveUp");
                officeActivities_DocumentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyDocumentsAdvanceFilerColumnOrder", "Move status 1 step up.");
                officeActivities_DocumentHelper.ClickElement("MoveUp");
                officeActivities_DocumentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyDocumentsAdvanceFilerColumnOrder", "Select owner in displayed column.");
                officeActivities_DocumentHelper.SelectByText("DisplayedCols", "Owner");
                officeActivities_DocumentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyDocumentsAdvanceFilerColumnOrder", "Move owner 1 step down.");
                officeActivities_DocumentHelper.ClickElement("MoveDown");
                officeActivities_DocumentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyDocumentsAdvanceFilerColumnOrder", "Click on apply button.");
                officeActivities_DocumentHelper.ClickElement("Apply");
                officeActivities_DocumentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyDocumentsAdvanceFilerColumnOrder", "Verify changed position of status column.");
                officeActivities_DocumentHelper.IsElementPresent("HeadStatus3");
                officeActivities_DocumentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyDocumentsAdvanceFilerColumnOrder", "Verify changed position of owner column.");
                officeActivities_DocumentHelper.IsElementPresent("HeadOwner7");
                officeActivities_DocumentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyDocumentsAdvanceFilerColumnOrder", "Logout from the application.");
                VisitCorp("logout");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyDocumentsAdvanceFilerColumnOrder");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Documents Advance Filer Column Order");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Documents Advance Filer Column Order", "Bug", "Medium", "Activities page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Documents Advance Filer Column Order");
                        TakeScreenshot("VerifyDocumentsAdvanceFilerColumnOrder");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyDocumentsAdvanceFilerColumnOrder.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyDocumentsAdvanceFilerColumnOrder");
                        string id = loginHelper.getIssueID("Verify Documents Advance Filer Column Order");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyDocumentsAdvanceFilerColumnOrder.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Documents Advance Filer Column Order"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Documents Advance Filer Column Order");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyDocumentsAdvanceFilerColumnOrder");
                executionLog.WriteInExcel("Verify Documents Advance Filer Column Order", Status, JIRA, "Tasks Management");
            }
        }
    }
}