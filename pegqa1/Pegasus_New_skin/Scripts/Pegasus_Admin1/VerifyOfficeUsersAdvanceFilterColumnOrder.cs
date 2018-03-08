using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class VerifyOfficeUsersAdvanceFilterColumnOrder : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin1")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Admin1")]
        public void verifyOfficeUsersAdvanceFilterColumnOrder()
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
            var office_UserHelper = new Office_UserHelper(GetWebDriver());

            // Variable Random
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("VerifyOfficeUsersAdvanceFilterColumnOrder", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyOfficeUsersAdvanceFilterColumnOrder", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyOfficeUsersAdvanceFilterColumnOrder", "Redirect To URL");
                VisitOffice("users");
                office_UserHelper.WaitForWorkAround(4000);

                executionLog.Log("VerifyOfficeUsersAdvanceFilterColumnOrder", "Verify status column is visible on the page..");
                office_UserHelper.IsElementPresent("HeadStatus");
                //office_UserHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyOfficeUsersAdvanceFilterColumnOrder", "Verify email column is visible on the page.");
                office_UserHelper.IsElementPresent("HeadEmail");
                //office_UserHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyOfficeUsersAdvanceFilterColumnOrder", "Verify phone column is visible on the page.");
                office_UserHelper.IsElementPresent("HeadPhone");
                office_UserHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyOfficeUsersAdvanceFilterColumnOrder", "Verify modified column is visible on the page.");
                office_UserHelper.IsElementPresent("HeadModified");
                //office_UserHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyOfficeUsersAdvanceFilterColumnOrder", "Click on advance filter.");
                office_UserHelper.ClickElement("AdvanceFilter");
                office_UserHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyOfficeUsersAdvanceFilterColumnOrder", "Select status in displayed columns.");
                office_UserHelper.SelectByText("DisplayedCols", "Status");
                //office_UserHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyOfficeUsersAdvanceFilterColumnOrder", "Click arrow to move column to avail cols.");
                office_UserHelper.ClickElement("RemoveCols");
                //office_UserHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyOfficeUsersAdvanceFilterColumnOrder", "Select email in displayed columns.");
                office_UserHelper.SelectByText("DisplayedCols", "E-Mail");
                //office_UserHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyOfficeUsersAdvanceFilterColumnOrder", "Click arrow to move column to avail cols");
                office_UserHelper.ClickElement("RemoveCols");
                //office_UserHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyOfficeUsersAdvanceFilterColumnOrder", "Select phone in displayed columns.");
                office_UserHelper.SelectByText("DisplayedCols", "Phone");
                //office_UserHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyOfficeUsersAdvanceFilterColumnOrder", "Click arrow to move column to avail cols");
                office_UserHelper.ClickElement("RemoveCols");
                //office_UserHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyOfficeUsersAdvanceFilterColumnOrder", "Select modified in displayed columns.");
                office_UserHelper.SelectByText("DisplayedCols", "Modified");
                //office_UserHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyOfficeUsersAdvanceFilterColumnOrder", "Click arrow to move column to avail cols");
                office_UserHelper.ClickElement("RemoveCols");
                //office_UserHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyOfficeUsersAdvanceFilterColumnOrder", "Click on Apply button.");
                office_UserHelper.ClickElement("Apply");
                office_UserHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyOfficeUsersAdvanceFilterColumnOrder", "Verify status not present on page.");
                office_UserHelper.IsElementNotPresent("HeadStatus");
                //office_UserHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyOfficeUsersAdvanceFilterColumnOrder", "Verify email not present on page.");
                office_UserHelper.IsElementNotPresent("HeadEmail");
                //office_UserHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyOfficeUsersAdvanceFilterColumnOrder", "Verify phone not present on page.");
                office_UserHelper.IsElementNotPresent("HeadPhone");
                //office_UserHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyOfficeUsersAdvanceFilterColumnOrder", "Verify modified not present on page.");
                office_UserHelper.IsElementNotPresent("HeadModified");
                //office_UserHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyOfficeUsersAdvanceFilterColumnOrder", "Redirect at leads page.");
                VisitOffice("leads");
                office_UserHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyOfficeUsersAdvanceFilterColumnOrder", "Verify page title as leads.");
                VerifyTitle("Leads");

                executionLog.Log("VerifyOfficeUsersAdvanceFilterColumnOrder", "Redirect To user page.");
                VisitOffice("users");
                office_UserHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyOfficeUsersAdvanceFilterColumnOrder", "Verify default position of email column.");
                office_UserHelper.IsElementPresent("HeadEmail5");
                //office_UserHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyOfficeUsersAdvanceFilterColumnOrder", "Verify default position of phone column.");
                office_UserHelper.IsElementPresent("HeadPhone6");
                //office_UserHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyOfficeUsersAdvanceFilterColumnOrder", "Redirect at user page again.");
                VisitOffice("users");
                office_UserHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyOfficeUsersAdvanceFilterColumnOrder", "Click on advance filter.");
                office_UserHelper.ClickElement("AdvanceFilter");
                office_UserHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyOfficeUsersAdvanceFilterColumnOrder", "Select email in displayed column.");
                office_UserHelper.SelectByText("DisplayedCols", "E-Mail");
                //office_UserHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyOfficeUsersAdvanceFilterColumnOrder", "Move email 1 step up.");
                office_UserHelper.ClickElement("MoveUp");
                //office_UserHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyOfficeUsersAdvanceFilterColumnOrder", "Move email 1 step up.");
                office_UserHelper.ClickElement("MoveUp");
                //office_UserHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyOfficeUsersAdvanceFilterColumnOrder", "Move email 1 step up.");
                office_UserHelper.ClickElement("MoveUp");
                //office_UserHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyOfficeUsersAdvanceFilterColumnOrder", "Select phone in displayed column.");
                office_UserHelper.SelectByText("DisplayedCols", "Phone");
                //office_UserHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyOfficeUsersAdvanceFilterColumnOrder", "Move phone 1 step down.");
                office_UserHelper.ClickElement("MoveDown");
                //office_UserHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyOfficeUsersAdvanceFilterColumnOrder", "Click on Apply button.");
                office_UserHelper.ClickElement("Apply");
                office_UserHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyOfficeUsersAdvanceFilterColumnOrder", "Verify changed position of email column.");
                office_UserHelper.IsElementPresent("HeadEmail3");
                //office_UserHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyOfficeUsersAdvanceFilterColumnOrder", "Verify changed position of phone column.");
                office_UserHelper.IsElementPresent("HeadPhone7");
                //office_UserHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyOfficeUsersAdvanceFilterColumnOrder", "Logout from the application.");
                VisitCorp("logout");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyOfficeUsersAdvanceFilterColumnOrder");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Office Users Advance Filter Column Order");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Office Users Advance Filter Column Order", "Bug", "Medium", "Activities page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Office Users Advance Filter Column Order");
                        TakeScreenshot("VerifyOfficeUsersAdvanceFilterColumnOrder");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyOfficeUsersAdvanceFilterColumnOrder.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyOfficeUsersAdvanceFilterColumnOrder");
                        string id = loginHelper.getIssueID("Verify Office Users Advance Filter Column Order");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyOfficeUsersAdvanceFilterColumnOrder.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Office Users Advance Filter Column Order"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Office Users Advance Filter Column Order");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyOfficeUsersAdvanceFilterColumnOrder");
                executionLog.WriteInExcel("Verify Office Users Advance Filter Column Order", Status, JIRA, "Meetings Management");
            }
        }
    }
}