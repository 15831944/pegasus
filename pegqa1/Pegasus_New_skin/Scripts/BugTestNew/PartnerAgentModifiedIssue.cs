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
    public class PartnerAgentModifiedIssue : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("Temp")]
        public void partnerAgentModifiedIssue()
        {
            string[] username = null;
            string[] password = null;

            XMLParse oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var agent_PartnerAgentHelper = new Agents_PartnerAgentsHelper(GetWebDriver());

            // Variable
            var name = "TestAgent" + RandomNumber(1, 99);
            String JIRA = "";
            String Status = "Pass";


         try
            {
            executionLog.Log("PartnerAgentModifiedIssue", "Login with valid username and password");
            Login(username[0], password[0]);
            Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

            executionLog.Log("PartnerAgentModifiedIssue", "Verify Page title");
            VerifyTitle("Dashboard");

            executionLog.Log("PartnerAgentModifiedIssue", "Go to Partner Agent page.");
            VisitOffice("partners/agents");

            executionLog.Log("PartnerAgentModifiedIssue", "Verify page title.");
            VerifyTitle("Partner Agents");

            executionLog.Log("PartnerAgentModifiedIssue", "Click On Create");
            agent_PartnerAgentHelper.ClickElement("Create");
            agent_PartnerAgentHelper.WaitForWorkAround(3000);

            executionLog.Log("PartnerAgentModifiedIssue", "Select Salutation");
            agent_PartnerAgentHelper.Select("SelectSalutation", "Mr");

            executionLog.Log("PartnerAgentModifiedIssue", "Enter FirstNAME");
            agent_PartnerAgentHelper.TypeText("FirstName", "Test Agent");

            executionLog.Log("PartnerAgentModifiedIssue", "Enter LastName");
            agent_PartnerAgentHelper.TypeText("LastName", "Tester");

            executionLog.Log("PartnerAgentModifiedIssue", "Enter Date Of Birth");
            agent_PartnerAgentHelper.TypeText("BirthDay", "1991-03-02");

            executionLog.Log("PartnerAgentModifiedIssue", "Enter DBAName");
            agent_PartnerAgentHelper.TypeText("DBAName", "Test DBA");

            executionLog.Log("PartnerAgentModifiedIssue", "Enter LinkedInUrl");
            agent_PartnerAgentHelper.TypeText("LinkedInUrl", "LinkedIn.con");

            executionLog.Log("PartnerAgentModifiedIssue", "Enter FaceBook Url");
            agent_PartnerAgentHelper.TypeText("FaceBookUrl", "Facebook.com");

            executionLog.Log("PartnerAgentModifiedIssue", "Enter TwitterURL");
            agent_PartnerAgentHelper.TypeText("TwitterURL", "Twitter.com");

            executionLog.Log("PartnerAgentModifiedIssue", "Select language.");
            agent_PartnerAgentHelper.Select("SelectLanguage", "English");

            executionLog.Log("PartnerAgentModifiedIssue", "Select eAddressType");
            agent_PartnerAgentHelper.Select("eAddressType", "E-Mail");

            executionLog.Log("PartnerAgentModifiedIssue", "Select eAddressLebel");
            agent_PartnerAgentHelper.Select("eAddressLebel", "Work");

            executionLog.Log("PartnerAgentModifiedIssue", "Enter eAddressType");
            var Email = "P.Agent" + GetRandomNumber() + "@yopmail.com";
            agent_PartnerAgentHelper.TypeText("eAddress", Email);

            executionLog.Log("PartnerAgentModifiedIssue", "Select SelectPhoneType");
            agent_PartnerAgentHelper.Select("SelectPhoneType", "Work");

            executionLog.Log("PartnerAgentModifiedIssue", "Enter PhoneNumber");
            agent_PartnerAgentHelper.TypeText("PhoneNumber", "1212121212");
          
            executionLog.Log("PartnerAgentModifiedIssue", "Click Save Agent btn");
            agent_PartnerAgentHelper.ClickElement("ClickSave");

            executionLog.Log("PartnerAgentModifiedIssue", "Verify success message. ");
            agent_PartnerAgentHelper.WaitForText("Partner Agent Created Successfully.", 30);

            executionLog.Log("PartnerAgentModifiedIssue", "Verify modified by name for agent");
            agent_PartnerAgentHelper.VerifyText("ModiCredits", "Howard Tang");
            agent_PartnerAgentHelper.WaitForWorkAround(2000);

        }
         catch (Exception e)
            {
                 executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("PartnerAgentModifiedIssue");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Partner Agent Modified Issue");
                if (!result)
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Create Partner Agent And Use rAccount", "Bug", "Medium", "Partner Agents", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Partner Agent Modified Issue");
                        TakeScreenshot("PartnerAgentModifiedIssue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\PartnerAgentModifiedIssue.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("PartnerAgentModifiedIssue");
                        string id = loginHelper.getIssueID("Partner Agent Modified Issue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\PartnerAgentModifiedIssue.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Partner Agent Modified Issue"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Partner Agent Modified Issue");
                executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("PartnerAgentModifiedIssue");
                executionLog.WriteInExcel("Partner Agent Modified Issue", Status, JIRA, "Agents Portal");
            }
        }
    }
}