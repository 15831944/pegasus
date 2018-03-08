using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class PartnerAgentWithUserNewSkin : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS5")]
        [TestCategory("NewSkinTaskJira")]
        public void partnerAgentWithUserNewSkin()
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
            var agents_PartnerAgentsHelper = new Agents_PartnerAgentsHelper(GetWebDriver());

            // Variable
            var name = "TestAgent" + GetRandomNumber();
            var Email = "P.Agent" + RandomNumber(1, 999) + "@yopmail.com";
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("PartnerAgentWithUserNewSkin", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("PartnerAgentWithUserNewSkin", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("PartnerAgentWithUserNewSkin", "Redirect at create agents page.");
                VisitOffice("partners/agent/create");

                executionLog.Log("PartnerAgentWithUserNewSkin", "Verify title");
                VerifyTitle("Create a Partner Agent");

                executionLog.Log("PartnerAgentWithUserNewSkin", "Select Salutation");
                agents_PartnerAgentsHelper.Select("SelectSalutation", "Mr");

                executionLog.Log("PartnerAgentWithUserNewSkin", "Enter FirstNAME");
                agents_PartnerAgentsHelper.TypeText("FirstName", "Test Agent");

                executionLog.Log("PartnerAgentWithUserNewSkin", "Enter LastName");
                agents_PartnerAgentsHelper.TypeText("LastName", "Tester");

                executionLog.Log("PartnerAgentWithUserNewSkin", "Enter Date Of Birth");
                agents_PartnerAgentsHelper.TypeText("BirthDay", "1991-03-02");

                executionLog.Log("PartnerAgentWithUserNewSkin", "Enter DBAName");
                agents_PartnerAgentsHelper.TypeText("DBAName", "Test DBA");

                executionLog.Log("PartnerAgentWithUserNewSkin", "Enter LinkedInUrl");
                agents_PartnerAgentsHelper.TypeText("LinkedInUrl", "LinkedIn.con");

                executionLog.Log("PartnerAgentWithUserNewSkin", "Select eAddressType");
                agents_PartnerAgentsHelper.Select("eAddressType", "E-Mail");

                executionLog.Log("PartnerAgentWithUserNewSkin", "Select eAddressLebel");
                agents_PartnerAgentsHelper.Select("eAddressLebel", "Work");

                executionLog.Log("PartnerAgentWithUserNewSkin", "Enter eAddressType");
                agents_PartnerAgentsHelper.TypeText("eAddress", Email);

                executionLog.Log("PartnerAgentWithUserNewSkin", "Select SelectPhoneType");
                agents_PartnerAgentsHelper.Select("SelectPhoneType", "Work");

                executionLog.Log("PartnerAgentWithUserNewSkin", "Enter PhoneNumber");
                agents_PartnerAgentsHelper.TypeText("PhoneNumber", "1212121212");

                executionLog.Log("PartnerAgentWithUserNewSkin", "Select Address Type  ");
                agents_PartnerAgentsHelper.Select("AddressType", "Office");

                executionLog.Log("PartnerAgentWithUserNewSkin", "Enter AddressLine1");
                agents_PartnerAgentsHelper.TypeText("AddressLine1", "FC 89");

                executionLog.Log("PartnerAgentWithUserNewSkin", "Enter Postal code.");
                agents_PartnerAgentsHelper.TypeText("PostalCode", "60601");
                agents_PartnerAgentsHelper.WaitForWorkAround(3000);

                executionLog.Log("PartnerAgentWithUserNewSkin", "Click On create user Checkbox");
                agents_PartnerAgentsHelper.ClickViaJavaScript("//input[@id='UserCreateUser' and @type='checkbox']");
                agents_PartnerAgentsHelper.WaitForWorkAround(2000);

                executionLog.Log("PartnerAgentWithUserNewSkin", "Enter UserName");
                agents_PartnerAgentsHelper.TypeText("UserName", name);

                executionLog.Log("PartnerAgentWithUserNewSkin", "Click On Avatar");
                agents_PartnerAgentsHelper.ClickElement("ClickOnAvatar");

                executionLog.Log("PartnerAgentWithUserNewSkin", "CLICK Save AGENT btn");
                agents_PartnerAgentsHelper.ClickElement("SaveAgent");

                executionLog.Log("PartnerAgentWithUserNewSkin", "Verify message");
                agents_PartnerAgentsHelper.WaitForText("Partner Agent Created Successfully", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("PartnerAgentWithUserNewSkin");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Partner Agent With User New Skin");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Partner Agent With User New Skin", "Bug", "Medium", "Partner page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Partner Agent With User New Skin");
                        TakeScreenshot("PartnerAgentWithUserNewSkin");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\PartnerAgentWithUserNewSkin.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("PartnerAgentWithUserNewSkin");
                        string id = loginHelper.getIssueID("Partner Agent With User New Skin");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\PartnerAgentWithUserNewSkin.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Partner Agent With User New Skin"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Partner Agent With User New Skin");
           //     executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("PartnerAgentWithUserNewSkin");
                executionLog.WriteInExcel("Partner Agent With User New Skin", Status, JIRA, "Partner Portal");
            }
        }
    }
}