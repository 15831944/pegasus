using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class OppourtunitiesVerifyCanadaCountry : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS5")]
        [TestCategory("NewSkinTaskJira")]
        public void oppourtunitiesVerifyCanadaCountry()
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

            // VARIABLE
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("OppourtunitiesVerifyCanadaCountry", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("OppourtunitiesVerifyCanadaCountry", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("OppourtunitiesVerifyCanadaCountry", "Redirect at create opportunities page.");
                VisitOffice("opportunities/create");
               
                executionLog.Log("OppourtunitiesVerifyCanadaCountry", "Select Mailing Country");
                office_OpportunitiesHelper.Select("SelectAddressCountryOpp", "Canada");
                office_OpportunitiesHelper.WaitForWorkAround(3000);

                executionLog.Log("OppourtunitiesVerifyCanadaCountry", "Select Location counrty");
                office_OpportunitiesHelper.Select("SelectMailingOppCountry", "Canada");
                office_OpportunitiesHelper.WaitForWorkAround(3000);

            }    
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("OppourtunitiesVerifyCanadaCountry");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Oppourtunities Verify Canada Country");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Oppourtunities Verify Canada Country", "Bug", "Medium", "Opportunities page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Oppourtunities Verify Canada Country");
                        TakeScreenshot("OppourtunitiesVerifyCanadaCountry");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\OppourtunitiesVerifyCanadaCountry.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("OppourtunitiesVerifyCanadaCountry");
                        string id = loginHelper.getIssueID("Oppourtunities Verify Canada Country");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\OppourtunitiesVerifyCanadaCountry.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Oppourtunities Verify Canada Country"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Oppourtunities Verify Canada Country");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("OppourtunitiesVerifyCanadaCountry");
                executionLog.WriteInExcel("Oppourtunities Verify Canada Country", Status, JIRA, "Opportunities Management");
            }
        }
    }
}