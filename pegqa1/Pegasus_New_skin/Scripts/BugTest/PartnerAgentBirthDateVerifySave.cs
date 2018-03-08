using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class PartnerAgentBirthDateVerifySave : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("Fail")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void partnerAgentBirthDateVerifySave()
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
            var name = "TestAgent" + GetRandomNumber();
            var username1 = "testuser" + RandomNumber(111,99999);
            String JIRA = "";
            String Status = "Pass";

            try
            {
            executionLog.Log("PartnerAgentBirthDateVerifySave", "Login with valid username and password");
            Login(username[0], password[0]);
            Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

            executionLog.Log("PartnerAgentBirthDateVerifySave", "Verify Page title");
            VerifyTitle("Dashboard");

            executionLog.Log("PartnerAgentBirthDateVerifySave", "Redirect to the URL");
            VisitOffice("partners/agent/create");

            executionLog.Log("PartnerAgentBirthDateVerifySave", "Verify page title");
            VerifyTitle("Create a Partner Agent");

            executionLog.Log("PartnerAgentBirthDateVerifySave", "Select Salutation");
            agent_PartnerAgentHelper.Select("SelectSalutation", "Mr");

            executionLog.Log("PartnerAgentBirthDateVerifySave", "Enter FirstNAME");
            agent_PartnerAgentHelper.TypeText("FirstName", "Test Agent");

            executionLog.Log("PartnerAgentBirthDateVerifySave", "Enter LastName");
            agent_PartnerAgentHelper.TypeText("LastName", "Tester");

            executionLog.Log("PartnerAgentBirthDateVerifySave", "Enter Date Of Birth");
            agent_PartnerAgentHelper.TypeText("BirthDay", "01/01/1992");

            executionLog.Log("PartnerAgentBirthDateVerifySave", "Enter DBAName");
            agent_PartnerAgentHelper.TypeText("DBAName", "Test DBA");

            executionLog.Log("PartnerAgentBirthDateVerifySave", "Select Language");
            agent_PartnerAgentHelper.Select("SelectLanguage", "English");

            executionLog.Log("PartnerAgentBirthDateVerifySave", "Select eAddressType");
            agent_PartnerAgentHelper.Select("eAddressType", "E-Mail");

            executionLog.Log("PartnerAgentBirthDateVerifySave", "Select eAddressLebel");
            agent_PartnerAgentHelper.Select("eAddressLebel", "Work");

            executionLog.Log("PartnerAgentBirthDateVerifySave", "Enter eAddess");
            agent_PartnerAgentHelper.TypeText("eAddress", "Test@gmail.com");

            executionLog.Log("PartnerAgentBirthDateVerifySave", "Select SelectPhoneType");
            agent_PartnerAgentHelper.Select("SelectPhoneType", "Work");

            executionLog.Log("PartnerAgentBirthDateVerifySave", "Select Address Type  ");
            agent_PartnerAgentHelper.Select("AddressType", "Office");

            executionLog.Log("PartnerAgentBirthDateVerifySave", "Enter AddressLine1");
            agent_PartnerAgentHelper.TypeText("AddressLine1", "FC 89");

            executionLog.Log("PartnerAgentBirthDateVerifySave", "Enter PhoneNumber");
            agent_PartnerAgentHelper.TypeText("PostalCode", "60601");
            agent_PartnerAgentHelper.WaitForWorkAround(3000);

            executionLog.Log("PartnerAgentBirthDateVerifySave", "Select User Account Check Box");
            agent_PartnerAgentHelper.ClickElement("UserAccChkBox");
            agent_PartnerAgentHelper.WaitForWorkAround(1000);

            executionLog.Log("PartnerAgentBirthDateVerifySave", "Enter Username");
            agent_PartnerAgentHelper.TypeText("UserName", username1);

            executionLog.Log("PartnerAgentBirthDateVerifySave", "Select Avatar Check Box");
            agent_PartnerAgentHelper.ClickElement("ParnterUserAvatar");

            executionLog.Log("PartnerAgentBirthDateVerifySave", "CLICK Save AGENT btn");
            agent_PartnerAgentHelper.ClickElement("SaveAgent");

            executionLog.Log("PartnerAgentBirthDateVerifySave", "Verify Text");
            agent_PartnerAgentHelper.WaitForText("Partner Agent Created Successfully.", 10);

            executionLog.Log("PartnerAgentBirthDateVerifySave", "Verify Date");
            agent_PartnerAgentHelper.WaitForText("01/01/1992", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("PartnerAgentBirthDateVerifySave");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Partner Agent Birth Date Verify Save");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Partner Agent Birth Date Verify Save", "Bug", "Medium", "Partner page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Partner Agent Birth Date Verify Save");
                        TakeScreenshot("PartnerAgentBirthDateVerifySave");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\PartnerAgentBirthDateVerifySave.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("PartnerAgentBirthDateVerifySave");
                        string id = loginHelper.getIssueID("Partner Agent Birth Date Verify Save");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\PartnerAgentBirthDateVerifySave.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Partner Agent Birth Date Verify Save"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Partner Agent Birth Date Verify Save");
                //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("PartnerAgentBirthDateVerifySave");
                executionLog.WriteInExcel("Partner Agent Birth Date Verify Save", Status, JIRA, "Partner Portal");
            }
        }
    }
}