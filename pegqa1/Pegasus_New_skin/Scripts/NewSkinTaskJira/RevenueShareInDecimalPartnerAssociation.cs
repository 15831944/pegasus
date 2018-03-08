using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class RevenueShareInDecimalPartnerAssociation : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("Test")]
        [TestCategory("Fail")]
        [TestCategory("TS5")]
        [TestCategory("NewSkinTaskJira")]

        public void revenueShareInDecimalPartnerAssociation()
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
            var Agentcode = "1" + RandomNumber(99, 999);
            var RevenueShare = "22." + RandomNumber(1, 99);


            try
            {
                executionLog.Log("RevenueShareInDecimalPartnerAssociation", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("RevenueShareInDecimalPartnerAssociation", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("RevenueShareInDecimalPartnerAssociation", "Click on Click On Partner Agent");
                VisitOffice("partners/associations");

                executionLog.Log("RevenueShareInDecimalPartnerAssociation", "verify title");
                VerifyTitle("Partner Associations");

                executionLog.Log("RevenueShareInDecimalPartnerAssociation", "ClickOnRevenueShare");
                agents_PartnerAssociationHelper.ClickElement("RevenueSahrnepartneragent");

                executionLog.Log("RevenueShareInDecimalPartnerAssociation", "Verify title");
                VerifyTitle("Partner Associations Revenue Shares");

                executionLog.Log("RevenueShareInDecimalPartnerAssociation", "Click on Revenue Share Partner Agnet");
                agents_PartnerAssociationHelper.ClickElement("RevenueSahrnepartnerasso");
                agents_PartnerAssociationHelper.WaitForWorkAround(5000);

                executionLog.Log("RevenueShareInDecimalPartnerAssociation", "SelectPartnerAgnetRS");
                agents_PartnerAssociationHelper.SelectByText("AgentNameSelect", "AssociationTester");

                executionLog.Log("RevenueShareInDecimalPartnerAssociation", "Select processor");
                agents_PartnerAssociationHelper.SelectByText("ProcessorSelect", "First Data Omaha");

                executionLog.Log("RevenueShareInDecimalPartnerAssociation", "EnterPartnerCode");
                agents_PartnerAssociationHelper.TypeText("ProcessorCodePAss", Agentcode);

                executionLog.Log("RevenueShareInDecimalPartnerAssociation", "EnterPartnerCode");
                agents_PartnerAssociationHelper.TypeText("RevenueShareps", RevenueShare);

                executionLog.Log("RevenueShareInDecimalPartnerAssociation", "ClickOnSaveRS");
                agents_PartnerAssociationHelper.ClickElement("SaveRS");
                agents_PartnerAssociationHelper.WaitForWorkAround(8000);

                executionLog.Log("RevenueShareInDecimalPartnerAssociation", "verify message Partner agent code & revenue share saved successfully.");
                // agents_PartnerAssociationHelper.ver("Partner association code & revenue share saved successfully.");

                executionLog.Log("RevenueShareInDecimalPartnerAssociation", "Enter Deciaml value");
                agents_PartnerAssociationHelper.TypeText("SearchDeciaml", RevenueShare);
                agents_PartnerAssociationHelper.WaitForWorkAround(4000);

                executionLog.Log("RevenueShareInDecimalPartnerAssociation", "Verify value oin decimal");
                agents_PartnerAssociationHelper.VerifyText("VerifyValueInDecimal", RevenueShare);
                agents_PartnerAssociationHelper.WaitForWorkAround(4000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("RevenueShareInDecimalPartnerAssociation");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Revenue Share In Decimal Partner Association");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Revenue Share In Decimal Partner Association", "Bug", "Medium", "Partner Association page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Revenue Share In Decimal Partner Association");
                        TakeScreenshot("RevenueShareInDecimalPartnerAssociation");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\RevenueShareInDecimalPartnerAssociation.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("RevenueShareInDecimalPartnerAssociation");
                        string id = loginHelper.getIssueID("Revenue Share In Decimal Partner Association");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\RevenueShareInDecimalPartnerAssociation.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Revenue Share In Decimal Partner Association"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Revenue Share In Decimal Partner Association");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("RevenueShareInDecimalPartnerAssociation");
                executionLog.WriteInExcel("Revenue Share In Decimal Partner Association", Status, JIRA, "Agent Portal");
            }
        }
    }
}