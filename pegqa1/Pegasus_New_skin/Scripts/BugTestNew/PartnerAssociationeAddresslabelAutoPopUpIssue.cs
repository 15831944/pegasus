using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class PartnerAssociationeAddresslabelAutoPopUpIssue : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("Temp")]
        public void partnerAssociationeAddresslabelAutoPopUpIssue()
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
                executionLog.Log("PartnerAssociationeAddresslabelAutoPopUpIssue", "Login with valid username and password");
                Login("testpartnerassociation", "test1qaz!QAZ");

                executionLog.Log("PartnerAssociationeAddresslabelAutoPopUpIssue", "Verify Page title");
                VerifyTitle("test association - Details");

                executionLog.Log("PartnerAssociationeAddresslabelAutoPopUpIssue", "Click on edit button.");
                agent_PartnerAssociationHelper.ClickElement("EditAsso");

                executionLog.Log("PartnerAssociationeAddresslabelAutoPopUpIssue", "Select eAddress type as E-mail");
                agent_PartnerAssociationHelper.Select("eAddressType", "E-Mail");

                executionLog.Log("PartnerAssociationeAddresslabelAutoPopUpIssue", "Click on save button.");
                agent_PartnerAssociationHelper.ClickElement("Save");

                executionLog.Log("PartnerAssociationeAddresslabelAutoPopUpIssue", "Click on edit button.");
                agent_PartnerAssociationHelper.ClickElement("EditAsso");

                executionLog.Log("PartnerAssociationeAddresslabelAutoPopUpIssue", "Verify eAddress label is work.");
                agent_PartnerAssociationHelper.VerifyeLabel("VerifyeLabel");

                executionLog.Log("PartnerAssociationeAddresslabelAutoPopUpIssue", "Click on save button..");
                agent_PartnerAssociationHelper.ClickElement("Save");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("PartnerAssociationeAddresslabelAutoPopUpIssue");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Partner Association eAddress label Auto PopUp Issue");
                if (!result)
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Partner Association eAddress label Auto PopUp Issue", "Bug", "Medium", "Partner Association page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Partner Association eAddress label Auto PopUp Issue");
                        TakeScreenshot("PartnerAssociationeAddresslabelAutoPopUpIssue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\PartnerAssociationeAddresslabelAutoPopUpIssue.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("PartnerAssociationeAddresslabelAutoPopUpIssue");
                        string id = loginHelper.getIssueID("Partner Association eAddress label Auto PopUp Issue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\PartnerAssociationeAddresslabelAutoPopUpIssue.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Partner Association eAddress label Auto PopUp Issue"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Partner Association eAddress label Auto PopUp Issue");
                executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("PartnerAssociationeAddresslabelAutoPopUpIssue");
                executionLog.WriteInExcel("Partner Association eAddress label Auto PopUp Issue", Status, JIRA, "Agents Portal");
            }
        }
    }
}