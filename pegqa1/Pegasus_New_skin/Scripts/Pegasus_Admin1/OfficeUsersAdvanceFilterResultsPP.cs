using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class OfficeUsersAdvanceFilterResultsPP : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin1")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin1")]
        public void officeUsersAdvanceFilterResultsPP()
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

            // Variable
            var DocName = "Test Exe" + GetRandomNumber();
            var fileUpl = GetPathToFile() + "chrome.exe";
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("OfficeUsersAdvanceFilterResultsPP", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("OfficeUsersAdvanceFilterResultsPP", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("OfficeUsersAdvanceFilterResultsPP", "Redirect at employee page.");
                VisitOffice("users");
                office_UserHelper.WaitForWorkAround(3000);

                executionLog.Log("OfficeUsersAdvanceFilterResultsPP", "Click on advance filter.");
                office_UserHelper.ClickElement("AdvanceFilter");
                office_UserHelper.WaitForWorkAround(2000);

                executionLog.Log("OfficeUsersAdvanceFilterResultsPP", "Select number of records to 10.");
                office_UserHelper.SelectByText("ResultsPerPage", "10");
                office_UserHelper.WaitForWorkAround(3000);

                executionLog.Log("OfficeUsersAdvanceFilterResultsPP", "Click on Apply button.");
                office_UserHelper.ClickElement("Apply");
                office_UserHelper.WaitForWorkAround(3000);


                executionLog.Log("OfficeUsersAdvanceFilterResultsPP", "Verify number of records displayed.");
                office_UserHelper.ShowResult(10);
                // office_UserHelper.VerifyText("No.ofRecords", "Showing 1 - 10 of");
                office_UserHelper.WaitForWorkAround(3000);

                executionLog.Log("OfficeUsersAdvanceFilterResultsPP", "Click on advance filter.");
                office_UserHelper.ClickElement("AdvanceFilter");
                office_UserHelper.WaitForWorkAround(3000);

                executionLog.Log("OfficeUsersAdvanceFilterResultsPP", "Select number of records to 20.");
                office_UserHelper.SelectByText("ResultsPerPage", "20");
                office_UserHelper.WaitForWorkAround(3000);

                executionLog.Log("OfficeUsersAdvanceFilterResultsPP", "Click on Apply button.");
                office_UserHelper.ClickElement("Apply");
                office_UserHelper.WaitForWorkAround(3000);

                executionLog.Log("OfficeUsersAdvanceFilterResultsPP", "Verify number of records displayed.");
                office_UserHelper.ShowResult(20);
                // office_UserHelper.VerifyText("No.ofRecords", "Showing 1 - 20 of");
                office_UserHelper.WaitForWorkAround(3000);

                executionLog.Log("OfficeUsersAdvanceFilterResultsPP", "Click on advance filter.");
                office_UserHelper.ClickElement("AdvanceFilter");
                office_UserHelper.WaitForWorkAround(3000);

                executionLog.Log("OfficeUsersAdvanceFilterResultsPP", "Select number of records to 50.");
                office_UserHelper.SelectByText("ResultsPerPage", "50");
                office_UserHelper.WaitForWorkAround(3000);

                executionLog.Log("OfficeUsersAdvanceFilterResultsPP", "Click on Apply button.");
                office_UserHelper.ClickElement("Apply");
                office_UserHelper.WaitForWorkAround(3000);

                executionLog.Log("OfficeUsersAdvanceFilterResultsPP", "Verify number of records displayed.");
                office_UserHelper.ShowResult(50);
                //office_UserHelper.VerifyText("No.ofRecords", "Showing 1 - 50 of");
                office_UserHelper.WaitForWorkAround(3000);

                executionLog.Log("OfficeUsersAdvanceFilterResultsPP", "Click on advance filter.");
                office_UserHelper.ClickElement("AdvanceFilter");
                office_UserHelper.WaitForWorkAround(3000);

                executionLog.Log("OfficeUsersAdvanceFilterResultsPP", "Select number of records to 100.");
                office_UserHelper.SelectByText("ResultsPerPage", "100");
                office_UserHelper.WaitForWorkAround(3000);

                executionLog.Log("OfficeUsersAdvanceFilterResultsPP", "Click on Apply button.");
                office_UserHelper.ClickElement("Apply");
                office_UserHelper.WaitForWorkAround(3000);

                executionLog.Log("OfficeUsersAdvanceFilterResultsPP", "Verify number of records displayed.");
                office_UserHelper.ShowResult(100);
                //office_UserHelper.VerifyText("No.ofRecords", "Showing 1 - 100 of");
                office_UserHelper.WaitForWorkAround(3000);

                executionLog.Log("OfficeUsersAdvanceFilterResultsPP", "Logout from the application.");
                VisitOffice("logout");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("OfficeUsersAdvanceFilterResultsPP");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Office Users Advance Filter ResultsPP");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Office Users Advance Filter ResultsPP", "Bug", "Medium", "Opportunities page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Office Users Advance Filter ResultsPP");
                        TakeScreenshot("OfficeUsersAdvanceFilterResultsPP");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\OfficeUsersAdvanceFilterResultsPP.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("OfficeUsersAdvanceFilterResultsPP");
                        string id = loginHelper.getIssueID("Office Users Advance Filter ResultsPP");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\OfficeUsersAdvanceFilterResultsPP.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Office Users Advance Filter ResultsPP"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Office Users Advance Filter ResultsPP");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("OfficeUsersAdvanceFilterResultsPP");
                executionLog.WriteInExcel("Office Users Advance Filter ResultsPP", Status, JIRA, "Opportunities Management");
            }
        }
    }
}