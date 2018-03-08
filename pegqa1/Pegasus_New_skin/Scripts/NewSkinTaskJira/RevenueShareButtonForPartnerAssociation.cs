using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class RevenueShareButtonForPartnerAssociation : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS5")]
        [TestCategory("NewSkinTaskJira")]
        public void revenueShareButtonForPartnerAssociation()
        {
            string[] username = null;
            string[] password = null;
            String JIRA = "";
            String Status = "Pass";

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var agents_PartnerAssociationHelper = new Agents_PartnerAssociationHelper(GetWebDriver());

            // Variable
            var name = "TestAgent" + GetRandomNumber();

            try
            {
                executionLog.Log("RevenueShareButtonForPartnerAssociation", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("RevenueShareButtonForPartnerAssociation", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("RevenueShareButtonForPartnerAssociation", "Redirect at partner association page.");
                VisitOffice("partners/associations");

                executionLog.Log("RevenueShareButtonForPartnerAssociation", "Click On Revenue Share button");
                agents_PartnerAssociationHelper.ClickElement("RevenueSahrnepartneragent");
                agents_PartnerAssociationHelper.WaitForWorkAround(3000);

                executionLog.Log("RevenueShareButtonForPartnerAssociation", "Verify  partner association available");
                agents_PartnerAssociationHelper.VerifyPageText("Partner Associations");
                agents_PartnerAssociationHelper.WaitForWorkAround(3000);

            }

            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("RevenueShareButtonForPartnerAssociation");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Revenue Share Button For Partner Association");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Revenue Share Button For Partner Association", "Bug", "Medium", "Partner Association page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Revenue Share Button For Partner Association");
                        TakeScreenshot("RevenueShareButtonForPartnerAssociation");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\RevenueShareButtonForPartnerAssociation.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("RevenueShareButtonForPartnerAssociation");
                        string id = loginHelper.getIssueID("Revenue Share Button For Partner Association");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\RevenueShareButtonForPartnerAssociation.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Revenue Share Button For Partner Association"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Revenue Share Button For Partner Association");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("RevenueShareButtonForPartnerAssociation");
                executionLog.WriteInExcel("Revenue Share Button For Partner Association", Status, JIRA, "Agent Portal");
            }
        }
    }
}