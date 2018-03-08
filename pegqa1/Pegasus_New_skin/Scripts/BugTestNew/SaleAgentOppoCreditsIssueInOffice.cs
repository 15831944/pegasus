using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class SaleAgentOppoCreditsIssueInOffice : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        [TestCategory("TS3")]
        [TestCategory("BugTestNew")]
        public void saleAgentOppoCreditsIssueInOffice()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var office_OpportunitiesHelper = new Office_OpportunitiesHelper(GetWebDriver());
            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Variable
            var Oppname = "Test" + GetRandomNumber();
            var CDBA = "New" + RandomNumber(99, 99999);
            String JIRA = "";
            String Status = "Pass";

            try
            {
            executionLog.Log("SaleAgentOppoCreditsIssueInOffice", "Login with valid credentials");
            Login("aslamsalesuser", "123456");
            Console.WriteLine("Logged in as: aslamsalesuser / 123456");

            executionLog.Log("SaleAgentOppoCreditsIssueInOffice", "Create Opportunities");
            VisitOffice("opportunities/create");

            executionLog.Log("SaleAgentOppoCreditsIssueInOffice", "Click on Save");
            office_OpportunitiesHelper.ClickElement("SaveOpp");
            office_OpportunitiesHelper.WaitForWorkAround(2000);

            executionLog.Log("SaleAgentOppoCreditsIssueInOffice", "Verify text on page.");
            office_OpportunitiesHelper.VerifyText("RequiredFieldsOpp", "This field is required.");

            executionLog.Log("SaleAgentOppoCreditsIssueInOffice", "Enter Opportunity Name");
            office_OpportunitiesHelper.TypeText("Name", Oppname);

            executionLog.Log("SaleAgentOppoCreditsIssueInOffice", "Enter Company DBA Name");
            office_OpportunitiesHelper.TypeText("CompanyName", CDBA);

            executionLog.Log("SaleAgentOppoCreditsIssueInOffice", "Select Opp Status");
            office_OpportunitiesHelper.SelectByText("State", "New");

            executionLog.Log("SaleAgentOppoCreditsIssueInOffice", "Select Opp Responsibility");
            office_OpportunitiesHelper.SelectByText("Responsibility", "Aslam Sales");

            executionLog.Log("SaleAgentOppoCreditsIssueInOffice", "Click on Save");
            office_OpportunitiesHelper.ClickElement("SaveOpp");
            office_OpportunitiesHelper.WaitForWorkAround(3000);

            var loc = "//h3[text()='Existing Opportunities']";
            if (office_OpportunitiesHelper.IsElementPresent(loc))
            {

                executionLog.Log("SaleAgentOppoCreditsIssueInOffice", "Click Dublicate Button");
                office_OpportunitiesHelper.ClickOnDisplayed("ClickOnDubBtn");
                office_OpportunitiesHelper.WaitForWorkAround(3000);

                executionLog.Log("SaleAgentOppoCreditsIssueInOffice", "Wait for success message");
                office_OpportunitiesHelper.WaitForText("Opportunity saved successfully.", 10);

                executionLog.Log("SaleAgentOppoCreditsIssueInOffice", "Verify opportunity created by");
                office_OpportunitiesHelper.VerifyText("CreatedBy", "Aslam Khan");

                executionLog.Log("SaleAgentOppoCreditsIssueInOffice", "Verify opportunity modified by");
                office_OpportunitiesHelper.VerifyText("ModifiedBy", "Aslam Sales");

                executionLog.Log("SaleAgentOppoCreditsIssueInOffice", "logout from the sales agent");
                VisitOffice("logout");
                office_OpportunitiesHelper.WaitForWorkAround(3000);

                executionLog.Log("SaleAgentOppoCreditsIssueInOffice", "Login to the office using valid credentials");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("SaleAgentOppoCreditsIssueInOffice", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("SaleAgentOppoCreditsIssueInOffice", "Redirect at opportunities page");
                VisitOffice("opportunities");
                office_OpportunitiesHelper.WaitForWorkAround(3000);

                executionLog.Log("SaleAgentOppoCreditsIssueInOffice", "Verify page title as opportunties.");
                VerifyTitle("Opportunities");

                executionLog.Log("SaleAgentOppoCreditsIssueInOffice", "Search opportunity by company name.");
                office_OpportunitiesHelper.TypeText("SearchCompanyopp", CDBA);
                office_OpportunitiesHelper.WaitForWorkAround(3000);

                executionLog.Log("SaleAgentOppoCreditsIssueInOffice", "Click on edit icon to edit opportunity.");
                office_OpportunitiesHelper.ClickElement("EditIcon");
                office_OpportunitiesHelper.WaitForWorkAround(3000);

                executionLog.Log("SaleAgentOppoCreditsIssueInOffice", "Select opportunity status as verbal commit.");
                office_OpportunitiesHelper.SelectByText("State", "Verbal Commit");

                executionLog.Log("SaleAgentOppoCreditsIssueInOffice", "Click on save button.");
                office_OpportunitiesHelper.ClickElement("SaveOpp");
                office_OpportunitiesHelper.WaitForWorkAround(3000);

                executionLog.Log("SaleAgentOppoCreditsIssueInOffice", "Verify opportunity created by");
                office_OpportunitiesHelper.VerifyText("CreatedBy", "Aslam Sales");
                //office_OpportunitiesHelper.WaitForWorkAround(3000);

                executionLog.Log("SaleAgentOppoCreditsIssueInOffice", "Verify opportunity modified by");
                office_OpportunitiesHelper.VerifyText("ModifiedBy", "Howard Tang");
                //office_OpportunitiesHelper.WaitForWorkAround(3000);

                executionLog.Log("SaleAgentOppoCreditsIssueInOffice", "logout from the application.");
                VisitOffice("logout");
                office_OpportunitiesHelper.WaitForWorkAround(3000);

                executionLog.Log("SaleAgentOppoCreditsIssueInOffice", "Login with valid credentials");
                Login("aslamsalesuser", "123456");
                Console.WriteLine("Logged in as: aslamsalesuser / 123456");
                office_OpportunitiesHelper.WaitForWorkAround(3000);

                executionLog.Log("SaleAgentOppoCreditsIssueInOffice", "Verify page title as dashboard.");
                VerifyTitle("Dashboard");
                office_OpportunitiesHelper.WaitForWorkAround(3000);

                executionLog.Log("SaleAgentOppoCreditsIssueInOffice", "Redirect at Opportunities");
                VisitOffice("opportunities");
                office_OpportunitiesHelper.WaitForWorkAround(3000);

                executionLog.Log("SaleAgentOppoCreditsIssueInOffice", "Click on  1st Opportunities");
                office_OpportunitiesHelper.ClickElement("ClickOn1stOpp");
                office_OpportunitiesHelper.WaitForWorkAround(3000);

                executionLog.Log("SaleAgentOppoCreditsIssueInOffice", "Click On Delete Button");
                office_OpportunitiesHelper.ClickElement("DeleteLink");
                office_OpportunitiesHelper.AcceptAlert();
                //office_OpportunitiesHelper.WaitForWorkAround(3000);

                executionLog.Log("SaleAgentOppoCreditsIssueInOffice", "Wait for confirmation");
                office_OpportunitiesHelper.WaitForText("1 records deleted successfully", 10);

                executionLog.Log("SaleAgentOppoCreditsIssueInOffice", "Redirect at recyclebin Page.");
                VisitOffice("opportunities/recyclebin");
                office_OpportunitiesHelper.WaitForWorkAround(3000);

                executionLog.Log("SaleAgentOppoCreditsIssueInOffice", "Verify page title.");
                VerifyTitle("Recycled Opportunities");
                //office_OpportunitiesHelper.WaitForWorkAround(3000);

                executionLog.Log("SaleAgentOppoCreditsIssueInOffice", "Click on delete icon.");
                office_OpportunitiesHelper.ClickElement("DeletePermanaently");
                office_OpportunitiesHelper.AcceptAlert();
                //office_OpportunitiesHelper.WaitForWorkAround(3000);

                executionLog.Log("SaleAgentOppoCreditsIssueInOffice", "Wait For Confirmation");
                office_OpportunitiesHelper.WaitForText("Opportunity permanently deleted.", 10);


            }
            else
            {
                executionLog.Log("SaleAgentOppoCreditsIssueInOffice", "Wait for success message");
                office_OpportunitiesHelper.WaitForText("Opportunity saved successfully.", 10);

                executionLog.Log("SaleAgentOppoCreditsIssueInOffice", "Verify opportunity created by");
                office_OpportunitiesHelper.VerifyText("CreatedBy", "Aslam Sales");
                //office_OpportunitiesHelper.WaitForWorkAround(3000);

                executionLog.Log("SaleAgentOppoCreditsIssueInOffice", "Verify opportunity modified by");
                office_OpportunitiesHelper.VerifyText("ModifiedBy", "Aslam Sales");
                office_OpportunitiesHelper.WaitForWorkAround(3000);

                executionLog.Log("SaleAgentOppoCreditsIssueInOffice", "logout from the sales agent");
                VisitOffice("logout");
                office_OpportunitiesHelper.WaitForWorkAround(3000);

                executionLog.Log("SaleAgentOppoCreditsIssueInOffice", "Login to the office using valid credentials");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);
                office_OpportunitiesHelper.WaitForWorkAround(3000);

                executionLog.Log("SaleAgentOppoCreditsIssueInOffice", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("SaleAgentOppoCreditsIssueInOffice", "Redirect at opportunities page");
                VisitOffice("opportunities");
                office_OpportunitiesHelper.WaitForWorkAround(3000);

                executionLog.Log("SaleAgentOppoCreditsIssueInOffice", "Verify page title as opportunties.");
                VerifyTitle("Opportunities");

                executionLog.Log("SaleAgentOppoCreditsIssueInOffice", "Search opportunity by company name.");
                office_OpportunitiesHelper.TypeText("SearchCompanyopp", CDBA);
                //office_OpportunitiesHelper.WaitForWorkAround(3000);

                executionLog.Log("SaleAgentOppoCreditsIssueInOffice", "Click on edit icon to edit opportunity.");
                office_OpportunitiesHelper.ClickElement("EditIcon");
                office_OpportunitiesHelper.WaitForWorkAround(3000);

                executionLog.Log("SaleAgentOppoCreditsIssueInOffice", "Select opportunity status as verbal commit.");
                office_OpportunitiesHelper.SelectByText("State", "Verbal Commit");
                //office_OpportunitiesHelper.WaitForWorkAround(3000);

                executionLog.Log("SaleAgentOppoCreditsIssueInOffice", "Click on save button.");
                office_OpportunitiesHelper.ClickElement("SaveOpp");
                office_OpportunitiesHelper.WaitForWorkAround(3000);

                executionLog.Log("SaleAgentOppoCreditsIssueInOffice", "Verify opportunity created by");
                office_OpportunitiesHelper.VerifyText("CreatedBy", "Aslam Sales");

                executionLog.Log("SaleAgentOppoCreditsIssueInOffice", "Verify opportunity modified by");
                office_OpportunitiesHelper.VerifyText("ModifiedBy", "Howard Tang");

                executionLog.Log("SaleAgentOppoCreditsIssueInOffice", "logout from the application.");
                VisitOffice("logout");
                office_OpportunitiesHelper.WaitForWorkAround(3000);

            }
        }
     catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("SaleAgentOppoCreditsIssueInOffice");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("SaleAgentOppoCreditsIssueInOffice");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("SaleAgentOppoCreditsIssueInOffice", "Bug", "Medium", "Opportunity page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("SaleAgentOppoCreditsIssueInOffice");
                        TakeScreenshot("SaleAgentOppoCreditsIssueInOffice");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\Opportunities.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("SaleAgentOppoCreditsIssueInOffice");
                        string id = loginHelper.getIssueID("SaleAgentOppoCreditsIssueInOffice");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\Opportunities.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("SaleAgentOppoCreditsIssueInOffice"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("SaleAgentOppoCreditsIssueInOffice");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("SaleAgentOppoCreditsIssueInOffice");
                executionLog.WriteInExcel("SaleAgentOppoCreditsIssueInOffice", Status, JIRA, "Opportunity management");
            }
        }
    }
}