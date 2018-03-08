using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class PartnerAssoLeadEaddressIssue : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("Temp")]
        public void partnerAssoLeadEaddressIssue()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var agent_PartnerAssociationHelper = new Agents_PartnerAssociationHelper(GetWebDriver());

            //Variable
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("PartnerAssoLeadEaddressIssue", "Login with valid username and password");
                Login("testpartnerassociation", "test1qaz!QAZ");

                executionLog.Log("PartnerAssoLeadEaddressIssue", "Verify Page title");
                VerifyTitle("test association - Details");

                executionLog.Log("PartnerAssoLeadEaddressIssue", "Click on lead tab.");
                agent_PartnerAssociationHelper.ClickElement("LeadTab");

                executionLog.Log("PartnerAssoLeadEaddressIssue", "Clci on creat lead button.");
                agent_PartnerAssociationHelper.ClickElement("CreateLead");

                executionLog.Log("PartnerAssoLeadEaddressIssue", "Select e address type as e-mail.");
                agent_PartnerAssociationHelper.Select("LeadEType", "E-Mail");

                executionLog.Log("PartnerAssoLeadEaddressIssue", "Verify no alert message present.");
                agent_PartnerAssociationHelper.VerifyAlertNotPresent();


            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("PartnerAssoLeadEaddressIssue");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Partner Asso Lead Eaddress Issue");
                if (!result)
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Partner Asso Lead Eaddress Issue", "Bug", "Medium", "Partner Association page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Partner Asso Lead Eaddress Issue");
                        TakeScreenshot("PartnerAssoLeadEaddressIssue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\PartnerAssoLeadEaddressIssue.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("PartnerAssoLeadEaddressIssue");
                        string id = loginHelper.getIssueID("Partner Asso Lead Eaddress Issue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\PartnerAssoLeadEaddressIssue.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Partner Asso Lead Eaddress Issue"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Partner Asso Lead Eaddress Issue");
                executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("PartnerAssoLeadEaddressIssue");
                executionLog.WriteInExcel("Partner Asso Lead Eaddress Issue", Status, JIRA, "Agents Portal");
            }
        }
    }
}