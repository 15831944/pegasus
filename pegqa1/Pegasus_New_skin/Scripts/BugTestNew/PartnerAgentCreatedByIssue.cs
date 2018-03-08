using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class PartnerAgentCreatedByIssue : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS2")]
        [TestCategory("BugTestNew")]
        public void partnerAgentCreatedByIssue()
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
            var agent_PartnerAgentHelper = new Agents_PartnerAgentsHelper(GetWebDriver());

            // Variable
            var Email = "P.Agent" + GetRandomNumber() + "@yopmail.com";
            var name = "TestAgent" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("PartnerAgentCreatedByIssue", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("PartnerAgentCreatedByIssue", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("PartnerAgentCreatedByIssue", "Redirect at partner agents page.");
                VisitOffice("partners/agents");

                executionLog.Log("CreatePartnerAgentAndUserAccount", "Click On Create");
                agent_PartnerAgentHelper.ClickElement("Create");

                executionLog.Log("CreatePartnerAgentAndUserAccount", "Select Salutation");
                agent_PartnerAgentHelper.Select("SelectSalutation", "Mr");

                executionLog.Log("CreatePartnerAgentAndUserAccount", "Enter FirstName");
                agent_PartnerAgentHelper.TypeText("FirstName", "Test Agent");

                executionLog.Log("CreatePartnerAgentAndUserAccount", "Enter LastName");
                agent_PartnerAgentHelper.TypeText("LastName", "Tester");

                executionLog.Log("CreatePartnerAgentAndUserAccount", "Enter Date Of Birth");
                agent_PartnerAgentHelper.TypeText("BirthDay", "1991-03-02");

                executionLog.Log("CreatePartnerAgentAndUserAccount", "Enter DBAName");
                agent_PartnerAgentHelper.TypeText("DBAName", "Test DBA");

                executionLog.Log("CreatePartnerAgentAndUserAccount", "Enter LinkedInUrl");
                agent_PartnerAgentHelper.TypeText("LinkedInUrl", "LinkedIn.com");

                executionLog.Log("CreatePartnerAgentAndUserAccount", "Enter FaceBook Url");
                agent_PartnerAgentHelper.TypeText("FaceBookUrl", "Facebook.com");

                executionLog.Log("CreatePartnerAgentAndUserAccount", "Enter TwitterURL");
                agent_PartnerAgentHelper.TypeText("TwitterURL", "Twitter.com");

                executionLog.Log("CreatePartnerAgentAndUserAccount", "Select DBAName");
                agent_PartnerAgentHelper.Select("SelectLanguage", "English");

                executionLog.Log("CreatePartnerAgentAndUserAccount", "Select eAddressType");
                agent_PartnerAgentHelper.Select("eAddressType", "E-Mail");

                executionLog.Log("CreatePartnerAgentAndUserAccount", "Select eAddressLebel");
                agent_PartnerAgentHelper.Select("eAddressLebel", "Work");

                executionLog.Log("CreatePartnerAgentAndUserAccount", "Enter eAddressType");
                agent_PartnerAgentHelper.TypeText("eAddress", Email);

                executionLog.Log("CreatePartnerAgentAndUserAccount", "Select SelectPhoneType");
                agent_PartnerAgentHelper.Select("SelectPhoneType", "Work");

                executionLog.Log("CreatePartnerAgentAndUserAccount", "Enter PhoneNumber");
                agent_PartnerAgentHelper.TypeText("PhoneNumber", "1212121212");

                executionLog.Log("CreatePartnerAgentAndUserAccount", "Select Address Type  ");
                agent_PartnerAgentHelper.Select("AddressType", "Office");

                executionLog.Log("CreatePartnerAgentAndUserAccount", "Enter AddressLine1");
                agent_PartnerAgentHelper.TypeText("AddressLine1", "FC 89");

                executionLog.Log("CreatePartnerAgentAndUserAccount", "Enter Postal code");
                agent_PartnerAgentHelper.TypeText("PostalCode", "60601");
                agent_PartnerAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("CreatePartnerAgentAndUserAccount", "Click On Checkbox");
                agent_PartnerAgentHelper.ClickViaJavaScript("//*[@id='UserCreateUser']");

                executionLog.Log("CreatePartnerAgentAndUserAccount", "Enter UserName");
                agent_PartnerAgentHelper.TypeText("UserName", name);

                executionLog.Log("CreatePartnerAgentAndUserAccount", "Click On Avatar");
                agent_PartnerAgentHelper.ClickElement("ClickOnAvatar");
                agent_PartnerAgentHelper.WaitForWorkAround(2000);

                executionLog.Log("CreatePartnerAgentAndUserAccount", "Click on save button btn");
                agent_PartnerAgentHelper.ClickElement("ClickSave");

                executionLog.Log("CreatePartnerAgentAndUserAccount", "Verify success message. ");
                agent_PartnerAgentHelper.WaitForText("Partner Agent Created Successfully.", 10);

                executionLog.Log("CreatePartnerAgentAndUserAccount", "Verify created by credentials.");
                agent_PartnerAgentHelper.VerifyText("CreatedBy", "By Howard Tang");
                agent_PartnerAgentHelper.WaitForWorkAround(3000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("PartnerAgentCreatedByIssue");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Partner Agent Created By Issue");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Partner Agent Created By Issue", "Bug", "Medium", "Partner page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Partner Agent Created By Issue");
                        TakeScreenshot("PartnerAgentCreatedByIssue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\PartnerAgentCreatedByIssue.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("PartnerAgentCreatedByIssue");
                        string id = loginHelper.getIssueID("Partner Agent Created By Issue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\PartnerAgentCreatedByIssue.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Partner Agent Created By Issue"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Partner Agent Created By Issue");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("PartnerAgentCreatedByIssue");
                executionLog.WriteInExcel("Partner Agent Created By Issue", Status, JIRA, "Agents Portal");
            }
        }
    }
}

