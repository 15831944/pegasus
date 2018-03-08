using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class AddAgentCodeForPartnerAgent : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS4")]
        [TestCategory("NewSkinTaskJira")]
        public void addAgentCodeForPartnerAgent()
        {
            string[] username = null;
            string[] password = null;

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var agents_PartnerAgentsHelper = new Agents_PartnerAgentsHelper(GetWebDriver());

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Variable
            var Email = "P.Agent" + RandomNumber(1, 999) + "@yopmail.com";
            var name = "TestAgent" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("AddAgentCodeForPartnerAgent", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("AddAgentCodeForPartnerAgent", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("AddAgentCodeForPartnerAgent", "Click On Partner Agent");
                VisitOffice("partners/agents");

                executionLog.Log("AddAgentCodeForPartnerAgent", "Verify Page title");
                VerifyTitle("Partner Agents");

                executionLog.Log("AddAgentCodeForPartnerAgent", "Enter AgentName");
                agents_PartnerAgentsHelper.TypeText("AgentName", "Partner Agent Code Tester");
                agents_PartnerAgentsHelper.WaitForWorkAround(6000);

                executionLog.Log("AddAgentCodeForPartnerAgent", "Select adjustment status");
                agents_PartnerAgentsHelper.SelectByText("SelectStatusAdjtmnt", "Active");
                agents_PartnerAgentsHelper.WaitForWorkAround(4000);

                var loc = "//table[@id='list1']/tbody/tr[2]/td[@aria-describedby='list1_contactNm']/a";
                if (agents_PartnerAgentsHelper.IsElementPresent(loc))
                {
                    executionLog.Log("AddAgentCodeForPartnerAgent", "Click On Partner Agent Agent");
                    agents_PartnerAgentsHelper.ClickElement("PartnerAgent");

                    executionLog.Log("AddAgentCodeForPartnerAgent", "Click On Create button adjustment");
                    agents_PartnerAgentsHelper.ClickElement("Create");
                    agents_PartnerAgentsHelper.WaitForWorkAround(2000);

                    executionLog.Log("AddAgentCodeForPartnerAgent", "Enter agent code");
                    agents_PartnerAgentsHelper.TypeText("EnterCodep", "1009");

                    executionLog.Log("AddAgentCodeForPartnerAgent", "Enter revenue share.");
                    agents_PartnerAgentsHelper.TypeText("RevenueShare", "10");

                    executionLog.Log("AddAgentCodeForPartnerAgent", "Click on save button");
                    agents_PartnerAgentsHelper.ClickElement("Save");
                    agents_PartnerAgentsHelper.WaitForWorkAround(4000);

                }
                else
                {

                    executionLog.Log("AddAgentCodeForPartnerAgent", "Click On Create");
                    agents_PartnerAgentsHelper.ClickElement("Create");

                    executionLog.Log("AddAgentCodeForPartnerAgent", "Select Salutation");
                    agents_PartnerAgentsHelper.Select("SelectSalutation", "Mr");

                    executionLog.Log("AddAgentCodeForPartnerAgent", "Enter FirstNAME");
                    agents_PartnerAgentsHelper.TypeText("FirstName", "Partner Agent Code");

                    executionLog.Log("AddAgentCodeForPartnerAgent", "Enter LastName");
                    agents_PartnerAgentsHelper.TypeText("LastName", "Tester");

                    executionLog.Log("AddAgentCodeForPartnerAgent", "Enter Date Of Birth");
                    agents_PartnerAgentsHelper.TypeText("BirthDay", "03/02/1991");

                    executionLog.Log("AddAgentCodeForPartnerAgent", "Enter DBAName");
                    agents_PartnerAgentsHelper.TypeText("DBAName", "Test DBA");

                    executionLog.Log("AddAgentCodeForPartnerAgent", "Enter LinkedInUrl");
                    agents_PartnerAgentsHelper.TypeText("LinkedInUrl", "LinkedIn.con");

                    executionLog.Log("AddAgentCodeForPartnerAgent", "Enter FaceBook Url");
                    agents_PartnerAgentsHelper.TypeText("FaceBookUrl", "Facebook.com");

                    executionLog.Log("AddAgentCodeForPartnerAgent", "Enter TwitterURL");
                    agents_PartnerAgentsHelper.TypeText("TwitterURL", "Twitter.com");

                    executionLog.Log("AddAgentCodeForPartnerAgent", "Enter DBAName");
                    agents_PartnerAgentsHelper.Select("SelectLanguage", "English");

                    executionLog.Log("AddAgentCodeForPartnerAgent", "Enter eAddressType");
                    agents_PartnerAgentsHelper.Select("eAddressType", "E-Mail");

                    executionLog.Log("AddAgentCodeForPartnerAgent", "Enter eAddressLebel");
                    agents_PartnerAgentsHelper.Select("eAddressLebel", "Work");

                    executionLog.Log("AddAgentCodeForPartnerAgent", "Enter eAddressType");
                    agents_PartnerAgentsHelper.TypeText("eAddress", Email);

                    executionLog.Log("AddAgentCodeForPartnerAgent", "Enter SelectPhoneType");
                    agents_PartnerAgentsHelper.Select("SelectPhoneType", "Work");

                    executionLog.Log("AddAgentCodeForPartnerAgent", "Enter PhoneNumber");
                    agents_PartnerAgentsHelper.TypeText("PhoneNumber", "1212121212");

                    executionLog.Log("AddAgentCodeForPartnerAgent", "Select Address Type");
                    agents_PartnerAgentsHelper.Select("AddressType", "Office");

                    executionLog.Log("AddAgentCodeForPartnerAgent", "Enter AddressLine1");
                    agents_PartnerAgentsHelper.TypeText("AddressLine1", "FC 89");

                    executionLog.Log("AddAgentCodeForPartnerAgent", "Enter postal code.");
                    agents_PartnerAgentsHelper.TypeText("PostalCode", "60601");
                    agents_PartnerAgentsHelper.WaitForWorkAround(2000);

                    executionLog.Log("AddAgentCodeForPartnerAgent", "CLICK Save AGENT btn");
                    agents_PartnerAgentsHelper.ClickElement("ClickSave");

                    executionLog.Log("AddAgentCodeForPartnerAgent", "Verify Text");
                    agents_PartnerAgentsHelper.WaitForText("Partner Agent Created Successfully.", 10);

                    executionLog.Log("AddAgentCodeForPartnerAgent", "Click On Create btn Adjmnt");
                    agents_PartnerAgentsHelper.ClickElement("Create");
                    agents_PartnerAgentsHelper.WaitForWorkAround(2000);

                    executionLog.Log("AddAgentCodeForPartnerAgent", "SelectAdjustmentFor");
                    agents_PartnerAgentsHelper.TypeText("EnterCodep", "1009");

                    executionLog.Log("AddAgentCodeForPartnerAgent", "SelectAdjustmentFor");
                    agents_PartnerAgentsHelper.TypeText("RevenueShare", "10");

                    executionLog.Log("AddAgentCodeForPartnerAgent", "SaveAgentCode");
                    agents_PartnerAgentsHelper.ClickElement("Save");
                    agents_PartnerAgentsHelper.WaitForWorkAround(4000);

                }
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("AddAgentCodeForPartnerAgent");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Add Agent Code for Partner Agent");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Add Agent Code for Partner Agent", "Bug", "Medium", "Partner agent page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Add Agent Code for Partner Agent");
                        TakeScreenshot("HomeLink");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AddAgentCodeForPartnerAgent.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("AddAgentCodeForPartnerAgent");
                        string id = loginHelper.getIssueID("Add Agent Code for Partner Agent");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AddAgentCodeForPartnerAgent.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Add Agent Code for Partner Agent"), "This issue is still occurring");
                    }
                }

                JIRA = loginHelper.getIssueID("Add Agent Code for Partner Agent");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("AddAgentCodeForPartnerAgent");
                executionLog.WriteInExcel("Add Agent Code for Partner Agent", Status, JIRA, "Agent Portal");
            }
        }
    }
}