using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class VerifySelectedUserGroup : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("SELENIUM_TESTCASE")]
        [TestCategory("TS8")]
        public void verifySelectedUserGroup()
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
            var verifySelectedUserGroupHelper = new VerifySelectedUserGroupHelper(GetWebDriver());

            // Variable

            String JIRA = "";
            String Status = "Pass";

            var FirstName = "ContactQA" + RandomNumber(1, 100);
            var CompanyName = "QACompany" + RandomNumber(1, 100);

            try
            {

                executionLog.Log("VerifySelectedUserGroup", "Login with valid credential  Username");
                Login(username[0], password[0]);

                executionLog.Log("VerifySelectedUserGroup", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                VisitOffice("contacts");
                verifySelectedUserGroupHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifySelectedUserGroup", "Click on Create Button");
                verifySelectedUserGroupHelper.ClickElement("CreateBtn");
                verifySelectedUserGroupHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifySelectedUserGroup", "Enter the first Name");
                verifySelectedUserGroupHelper.TypeText("FirstName", FirstName);

                executionLog.Log("VerifySelectedUserGroup", "Enter the Last Name");
                verifySelectedUserGroupHelper.TypeText("LastName", "Tester");

                executionLog.Log("VerifySelectedUserGroup", "Enter the Company Name");
                verifySelectedUserGroupHelper.TypeText("CompanyName", CompanyName);

                executionLog.Log("VerifySelectedUserGroup", "Select the status");
                verifySelectedUserGroupHelper.SelectByText("Status", "Active");

                executionLog.Log("VerifySelectedUserGroup", "Select the responsiblity");
                verifySelectedUserGroupHelper.SelectByText("Responsibity", "Howard Tang");

                executionLog.Log("VerifySelectedUserGroup", "Select the User Group");
                verifySelectedUserGroupHelper.ClickElement("UserGroup");
                verifySelectedUserGroupHelper.ClickElement("UserGroupValue");

                executionLog.Log("VerifySelectedUserGroup", "Click on Save Button");
                verifySelectedUserGroupHelper.ClickElement("SaveBtn");
                verifySelectedUserGroupHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifySelectedUserGroup", "Verify the Duplicate contact button");
                verifySelectedUserGroupHelper.VerifyDuplicateBtn("CreateDuplicateBtn", "SaveBtn");
                verifySelectedUserGroupHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifySelectedUserGroup", "Search the Same company");
                verifySelectedUserGroupHelper.TypeText("CompanySearchField", CompanyName);
                verifySelectedUserGroupHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifySelectedUserGroup", "Select All in responsibity field");
                verifySelectedUserGroupHelper.SelectByText("ResponsibityField", "All");
                verifySelectedUserGroupHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifySelectedUserGroup", "Click on Same contact");
                verifySelectedUserGroupHelper.ClickElement("ClickContact");
                verifySelectedUserGroupHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifySelectedUserGroup", "Click on Edit");
                verifySelectedUserGroupHelper.ClickElement("EditIcon");
                verifySelectedUserGroupHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifySelectedUserGroup", "Verify the default option is not visbile in User Group field.");
                verifySelectedUserGroupHelper.VerifySelectedOption("//*[@id='ContactAssignedUserGroupId']", "Primary Group");
                //verifySelectedUserGroupHelper.WaitForWorkAround(5000);

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
                executionLog.DeleteFile("Error");
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
