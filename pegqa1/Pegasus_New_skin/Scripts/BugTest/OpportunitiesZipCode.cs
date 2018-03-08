using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class OpportunitiesZipCode : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("BugTest")]
        public void opportunitiesZipCode()
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
                executionLog.Log("OpportunitiesZipCode", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("OpportunitiesZipCode", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("OpportunitiesZipCode", "Redirect To URL");
                VisitOffice("opportunities/create");

                executionLog.Log("OpportunitiesZipCode", "Verify page title");
                VerifyTitle("Create an Opportunity");

                executionLog.Log("OpportunitiesZipCode", "Enter Address Line1");
                office_OpportunitiesHelper.TypeText("AddressLine1Opp", "TEST ADDRESS LINE 1");

                executionLog.Log("OpportunitiesZipCode", "Enter Zip Code");
                office_OpportunitiesHelper.TypeText("ZipCodeOpp", "60601");
                office_OpportunitiesHelper.WaitForWorkAround(2000);

                executionLog.Log("OpportunitiesZipCode", "Select MailingAddressLine1");
                office_OpportunitiesHelper.TypeText("MailingAddLine1opp", "test");

                executionLog.Log("OpportunitiesZipCode", "Enter Mailing Zip Code");
                office_OpportunitiesHelper.TypeText("MailingZipCodeOpp", "30033");
                office_OpportunitiesHelper.WaitForWorkAround(3000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("OpportunitiesZipCode");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Opportunities Zip Code");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Opportunities Zip Code", "Bug", "Medium", "Opportunities page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Opportunities Zip Code");
                        TakeScreenshot("OpportunitiesZipCode");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\OpportunitiesZipCode.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("OpportunitiesZipCode");
                        string id = loginHelper.getIssueID("Opportunities Zip Code");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\OpportunitiesZipCode.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Opportunities Zip Code"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Opportunities Zip Code");
                executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("OpportunitiesZipCode");
                executionLog.WriteInExcel("Opportunities Zip Code", Status, JIRA, "Opportunities Management");
            }
        }
    }
}