using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class CreatePartnerAgentAndUserAccount : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void createPartnerAgentAndUserAccount()
        {
            string[] username = null;
            string[] password = null;

            XMLParse oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var agent_PartnerAgentHelper = new Agents_PartnerAgentsHelper(GetWebDriver());

            // Variable
            var name = "TestAgent" + RandomNumber(1, 99);
            var user = "agentuser" + RandomNumber(111,99999);
            String JIRA = "";
            String Status = "Pass";


            try
            {
                executionLog.Log("CreatePartnerAgentAndUserAccount", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("CreatePartnerAgentAndUserAccount", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("CreatePartnerAgentAndUserAccount", "Go to Partner Agent page.");
                VisitOffice("partners/agents");

                executionLog.Log("CreatePartnerAgentAndUserAccount", "Verify page title.");
                VerifyTitle("Partner Agents");

                executionLog.Log("CreatePartnerAgentAndUserAccount", "Click On Create");
                agent_PartnerAgentHelper.ClickElement("Create");
                agent_PartnerAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("CreatePartnerAgentAndUserAccount", "Select Salutation");
                agent_PartnerAgentHelper.Select("SelectSalutation", "Mr");

                executionLog.Log("CreatePartnerAgentAndUserAccount", "Enter FirstNAME");
                agent_PartnerAgentHelper.TypeText("FirstName", "Test Agent");

                executionLog.Log("CreatePartnerAgentAndUserAccount", "Enter LastName");
                agent_PartnerAgentHelper.TypeText("LastName", "Tester");

                executionLog.Log("CreatePartnerAgentAndUserAccount", "Enter Date Of Birth");
                agent_PartnerAgentHelper.TypeText("BirthDay", "1991-03-02");

                executionLog.Log("CreatePartnerAgentAndUserAccount", "Enter DBAName");
                agent_PartnerAgentHelper.TypeText("DBAName", "Test DBA");

                executionLog.Log("CreatePartnerAgentAndUserAccount", "Enter LinkedInUrl");
                agent_PartnerAgentHelper.TypeText("LinkedInUrl", "LinkedIn.con");

                executionLog.Log("CreatePartnerAgentAndUserAccount", "Enter FaceBook Url");
                agent_PartnerAgentHelper.TypeText("FaceBookUrl", "Facebook.com");

                executionLog.Log("CreatePartnerAgentAndUserAccount", "Enter TwitterURL");
                agent_PartnerAgentHelper.TypeText("TwitterURL", "Twitter.com");

                executionLog.Log("CreatePartnerAgentAndUserAccount", "Select language.");
                agent_PartnerAgentHelper.Select("SelectLanguage", "English");

                executionLog.Log("CreatePartnerAgentAndUserAccount", "Select eAddressType");
                agent_PartnerAgentHelper.Select("eAddressType", "E-Mail");

                executionLog.Log("CreatePartnerAgentAndUserAccount", "Select eAddressLebel");
                agent_PartnerAgentHelper.Select("eAddressLebel", "Work");

                executionLog.Log("CreatePartnerAgentAndUserAccount", "Enter eAddressType");
                var Email = "P.Agent" + GetRandomNumber() + "@yopmail.com";
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
                agent_PartnerAgentHelper.WaitForWorkAround(2000);

                executionLog.Log("CreatePartnerAgentAndUserAccount", "Select User Account Check Box");
                agent_PartnerAgentHelper.ClickElement("UserAccChkBox");

                executionLog.Log("CreatePartnerAgentAndUserAccount", "Enter Username");
                agent_PartnerAgentHelper.TypeText("UserName", user);

                executionLog.Log("CreatePartnerAgentAndUserAccount", "Select PartnerUser Avatar Check Box");
                agent_PartnerAgentHelper.ClickElement("ParnterUserAvatar");

                executionLog.Log("CreatePartnerAgentAndUserAccount", "Click Save Agent btn");
                agent_PartnerAgentHelper.ClickElement("ClickSave");
                agent_PartnerAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("CreatePartnerAgentAndUserAccount", "Verify success message. ");
                agent_PartnerAgentHelper.WaitForText("Partner Agent Created Successfully.", 30);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CreatePartnerAgentAndUserAccount");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Create Partner Agent And User Account");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Create Partner Agent And Use rAccount", "Bug", "Medium", "Partner Agents", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Create Partner Agent And User Account");
                        TakeScreenshot("CreatePartnerAgentAndUserAccount");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreatePartnerAgentAndUserAccount.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CreatePartnerAgentAndUserAccount");
                        string id = loginHelper.getIssueID("Create Partner Agent And User Account");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreatePartnerAgentAndUserAccount.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Create Partner Agent And User Account"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Create Partner Agent And User Account");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("CreatePartnerAgentAndUserAccount");
                executionLog.WriteInExcel("Create Partner Agent And User Account", Status, JIRA, "Agents Portal");
            }
        }
    }
}