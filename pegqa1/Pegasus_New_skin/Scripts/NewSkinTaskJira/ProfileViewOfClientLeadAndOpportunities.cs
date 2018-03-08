using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class ProfileViewOfClientLeadAndOpportunities : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS5")]
        [TestCategory("NewSkinTaskJira")]
        public void profileViewOfClientLeadAndOpportunities()
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
            var office_ClientsHelper = new Office_ClientsHelper(GetWebDriver());
            var office_LeadsHelper = new Office_LeadsHelper(GetWebDriver());
            var office_OpportunitiesHelper = new Office_OpportunitiesHelper(GetWebDriver());

            // VARIABLE
            var name = "TestEmployee" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("ProfileViewOfClientLeadAndOpportunities", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("ProfileViewOfClientLeadAndOpportunities", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("ProfileViewOfClientLeadAndOpportunities", " Redirect To Clients");
                VisitOffice("clients");

                executionLog.Log("ProfileViewOfClientLeadAndOpportunities", "Click on any client.");
                office_ClientsHelper.ClickElement("ClickOnAnyClient");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("ProfileViewOfClientLeadAndOpportunities", "Verify View text on page");
                office_ClientsHelper.VerifyPageText("View");
                
                executionLog.Log("ProfileViewOfClientLeadAndOpportunities", " Redirect To Leads");
                VisitOffice("leads");
                office_ClientsHelper.WaitForWorkAround(4000);

                executionLog.Log("ProfileViewOfClientLeadAndOpportunities", "Click on any lead.");
                office_LeadsHelper.ClickElement("ClickAnyLead");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("ProfileViewOfClientLeadAndOpportunities", "Verify view text on page");
                office_LeadsHelper.VerifyPageText("View");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("ProfileViewOfClientLeadAndOpportunities", " Redirect To Opportunities page.");
                VisitOffice("opportunities");
               
                executionLog.Log("ProfileViewOfClientLeadAndOpportunities", "Click on any opportunity.");
                office_OpportunitiesHelper.ClickElement("Opportunities1");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("ProfileViewOfClientLeadAndOpportunities", "Verify view text present on page.");
                office_OpportunitiesHelper.VerifyPageText("View");
                
            }        
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("ProfileViewOfClientLeadAndOpportunities");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Profile View Of Client Lead And Opportunities");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Profile View Of Client Lead And Opportunities", "Bug", "Medium", "CLient/Leads page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Profile View Of Client Lead And Opportunities");
                        TakeScreenshot("ProfileViewOfClientLeadAndOpportunities");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ProfileViewOfClientLeadAndOpportunities.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("ProfileViewOfClientLeadAndOpportunities");
                        string id = loginHelper.getIssueID("Profile View Of Client Lead And Opportunities");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ProfileViewOfClientLeadAndOpportunities.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Profile View Of Client Lead And Opportunities"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Profile View Of Client Lead And Opportunities");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("ProfileViewOfClientLeadAndOpportunities");
                executionLog.WriteInExcel("Profile View Of Client Lead And Opportunities", Status, JIRA, "Leads/Client Management");
            }
        }
    }
}
		