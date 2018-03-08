using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class ValidationPartnerAssociationRevenueShare : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void validationPartnerAssociationRevenueShare()
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
            var agent_PartnerAssociationHelper = new Agents_PartnerAssociationHelper(GetWebDriver());

            // Variable
            var name = "TestAgent" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("ValidationPartnerAssociationRevenueShare", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("ValidationPartnerAssociationRevenueShare", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("ValidationPartnerAssociationRevenueShare", "Redirect to create partner agent");
                VisitOffice("partners/associations");

                executionLog.Log("ValidationPartnerAssociationRevenueShare", "Click On Partner Association");
                agent_PartnerAssociationHelper.ClickElement("ClickOnPartnerAssociation");
                agent_PartnerAssociationHelper.WaitForWorkAround(1000);

                executionLog.Log("ValidationPartnerAssociationRevenueShare", "Click AddNew Agent Code");
                agent_PartnerAssociationHelper.ClickElement("ClickAddNewAgentCode");
                agent_PartnerAssociationHelper.WaitForWorkAround(1000);

                executionLog.Log("ValidationPartnerAssociationRevenueShare", "Enter Sale Code");
                agent_PartnerAssociationHelper.TypeText("EnterSaleCode", "2");

                executionLog.Log("ValidationPartnerAssociationRevenueShare", "Enter Revenue Share");
                agent_PartnerAssociationHelper.TypeText("EnterRevenueShare", "3");

                executionLog.Log("ValidationPartnerAssociationRevenueShare", "Click On save Button");
                agent_PartnerAssociationHelper.ClickElement("ClickRevenueShareSaveBtn");
                agent_PartnerAssociationHelper.WaitForWorkAround(3000);

                executionLog.Log("ValidationPartnerAssociationRevenueShare", "Click On Edidt Agent Code");
                agent_PartnerAssociationHelper.ClickElement("ClickOnEditAgentCodeRS");

                executionLog.Log("ValidationPartnerAssociationRevenueShare", "Enter Revenue Share");
                agent_PartnerAssociationHelper.TypeText("EnterRevenueShareValue", "h");

                executionLog.Log("ValidationPartnerAssociationRevenueShare", "Click Save");
                agent_PartnerAssociationHelper.ClickElement("ClickSaveRS");
                agent_PartnerAssociationHelper.WaitForWorkAround(1000);

                executionLog.Log("ValidationPartnerAssociationRevenueShare", "Verify");
                agent_PartnerAssociationHelper.VerifyText("VerifyRsValidation", "Revenue Share: Please enter a valid number");
                agent_PartnerAssociationHelper.WaitForWorkAround(3000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("ValidationPartnerAssociationRevenueShare");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("ValidationPartner Association Revenue Share");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("ValidationPartner Association Revenue Share", "Bug", "Medium", "Partner Association page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("ValidationPartner Association Revenue Share");
                        TakeScreenshot("ValidationPartnerAssociationRevenueShare");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ValidationPartnerAssociationRevenueShare.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("ValidationPartnerAssociationRevenueShare");
                        string id = loginHelper.getIssueID("ValidationPartner Association Revenue Share");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ValidationPartnerAssociationRevenueShare.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("ValidationPartner Association Revenue Share"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("ValidationPartner Association Revenue Share");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("ValidationPartnerAssociationRevenueShare");
                executionLog.WriteInExcel("ValidationPartner Association Revenue Share", Status, JIRA, "Agents Portal");
            }
        }
    }
}