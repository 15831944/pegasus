using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class EditPartnerAgentNewSkin : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS4")]
        [TestCategory("NewSkinTaskJira")]
        public void editPartnerAgentNewSkin()
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
            var Email = "P.Agent" + RandomNumber(1, 999) + "@yopmail.com";
            var name = "TestAgent" + GetRandomNumber();
            String Status = "Pass";
            String JIRA = "";

            try
            {

                executionLog.Log("EditPartnerAgentNewSkin", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("EditPartnerAgentNewSkin", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("EditPartnerAgentNewSkin", "Redirect at Partner Agent page.");
                VisitOffice("partners/agents");

                executionLog.Log("EditPartnerAgentNewSkin", "Search Partner Agent");
                agents_PartnerAgentsHelper.TypeText("SearchAgent", "Test Agent Tester");
                agents_PartnerAgentsHelper.WaitForWorkAround(2000);

                var Loc = "//table[@id='list1']/tbody/tr[2]/td[8]/a";
                if (agents_PartnerAgentsHelper.IsElementPresent(Loc))
                {

                    executionLog.Log("EditPartnerAgentNewSkin", "Click On Edit");
                    agents_PartnerAgentsHelper.ClickElement("EditAgent");

                    executionLog.Log("EditPartnerAgentNewSkin", "Enter LastName");
                    agents_PartnerAgentsHelper.TypeText("LastName", "Tester");

                    executionLog.Log("EditPartnerAgentNewSkin", "Enter DBAName");
                    agents_PartnerAgentsHelper.TypeText("DBAName", "Test DBA");

                    executionLog.Log("EditPartnerAgentNewSkin", "Select eAddressType");
                    agents_PartnerAgentsHelper.Select("eAddressType", "E-Mail");

                    executionLog.Log("EditPartnerAgentNewSkin", "Select eAddressLebel");
                    agents_PartnerAgentsHelper.Select("eAddressLebel", "Work");

                    executionLog.Log("EditPartnerAgentNewSkin", "Select SelectPhoneType");
                    agents_PartnerAgentsHelper.Select("SelectPhoneType", "Work");

                    executionLog.Log("EditPartnerAgentNewSkin", "Enter PhoneNumber");
                    agents_PartnerAgentsHelper.TypeText("PhoneNumber", "1212121212");

                    executionLog.Log("EditPartnerAgentNewSkin", "Select Address Type    ");
                    agents_PartnerAgentsHelper.Select("AddressType", "Office");

                    executionLog.Log("EditPartnerAgentNewSkin", "Enter AddressLine1");
                    agents_PartnerAgentsHelper.TypeText("AddressLine1", "FC 89");

                    executionLog.Log("EditPartnerAgentNewSkin", "Enter Postal code");
                    agents_PartnerAgentsHelper.TypeText("PostalCode", "60601");
                    agents_PartnerAgentsHelper.WaitForWorkAround(2000);

                    executionLog.Log("EditPartnerAgentNewSkin", "Click on Save button.");
                    agents_PartnerAgentsHelper.ClickElement("SaveAgent");
                    agents_PartnerAgentsHelper.WaitForWorkAround(4000);

                }
                else
                {

                    executionLog.Log("EditPartnerAgentNewSkin", "Click On Create");
                    agents_PartnerAgentsHelper.ClickElement("Create");

                    executionLog.Log("EditPartnerAgentNewSkin", "Select Salutation");
                    agents_PartnerAgentsHelper.Select("SelectSalutation", "Mr");

                    executionLog.Log("EditPartnerAgentNewSkin", "Enter FirstNAME");
                    agents_PartnerAgentsHelper.TypeText("FirstName", "Test Agent");

                    executionLog.Log("EditPartnerAgentNewSkin", "Enter LastName");
                    agents_PartnerAgentsHelper.TypeText("LastName", "Tester");

                    executionLog.Log("EditPartnerAgentNewSkin", "Enter DBAName");
                    agents_PartnerAgentsHelper.TypeText("DBAName", "Test DBA");

                    executionLog.Log("EditPartnerAgentNewSkin", "Enter LinkedInUrl");
                    agents_PartnerAgentsHelper.TypeText("LinkedInUrl", "LinkedIn.con");

                    executionLog.Log("EditPartnerAgentNewSkin", "Enter FaceBook Url");
                    agents_PartnerAgentsHelper.TypeText("FaceBookUrl", "Facebook.com");

                    executionLog.Log("EditPartnerAgentNewSkin", "Enter TwitterURL");
                    agents_PartnerAgentsHelper.TypeText("TwitterURL", "Twitter.com");

                    executionLog.Log("EditPartnerAgentNewSkin", "Select DBAName");
                    agents_PartnerAgentsHelper.Select("SelectLanguage", "English");

                    executionLog.Log("EditPartnerAgentNewSkin", "Select eAddressType");
                    agents_PartnerAgentsHelper.Select("eAddressType", "E-Mail");

                    executionLog.Log("EditPartnerAgentNewSkin", "Select eAddressLebel");
                    agents_PartnerAgentsHelper.Select("eAddressLebel", "Work");

                    executionLog.Log("EditPartnerAgentNewSkin", "Enter eAddressType");
                    agents_PartnerAgentsHelper.TypeText("eAddress", Email);

                    executionLog.Log("EditPartnerAgentNewSkin", "Select SelectPhoneType");
                    agents_PartnerAgentsHelper.Select("SelectPhoneType", "Work");

                    executionLog.Log("EditPartnerAgentNewSkin", "Enter PhoneNumber");
                    agents_PartnerAgentsHelper.TypeText("PhoneNumber", "1212121212");

                    executionLog.Log("EditPartnerAgentNewSkin", "Select Address Type    ");
                    agents_PartnerAgentsHelper.Select("AddressType", "Office");

                    executionLog.Log("EditPartnerAgentNewSkin", "Enter AddressLine1");
                    agents_PartnerAgentsHelper.TypeText("AddressLine1", "FC 89");

                    executionLog.Log("EditPartnerAgentNewSkin", "Enter Postal code.");
                    agents_PartnerAgentsHelper.TypeText("PostalCode", "60601");
                    agents_PartnerAgentsHelper.WaitForWorkAround(2000);

                    executionLog.Log("EditPartnerAgentNewSkin", "Click Save AGENT btn");
                    agents_PartnerAgentsHelper.ClickElement("SaveAgent");
                    agents_PartnerAgentsHelper.WaitForWorkAround(2000);


                }
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";
                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("EditPartnerAgentNewSkin");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Edit Partner Agent New Skin");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Edit Partner Agent New Skin", "Bug", "Medium", "Partner page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Edit Partner Agent New Skin");
                        TakeScreenshot("EditPartnerAgentNewSkin");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditPartnerAgentNewSkin.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("EditPartnerAgentNewSkin");
                        string id = loginHelper.getIssueID("Edit Partner Agent New Skin");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditPartnerAgentNewSkin.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Edit Partner Agent New Skin"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Edit Partner Agent New Skin");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("EditPartnerAgentNewSkin");
                executionLog.WriteInExcel("Edit Partner Agent New Skin", Status, JIRA, "Partner Portal");
            }
        }
    }
}