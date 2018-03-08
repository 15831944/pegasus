using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class PartnerAgentWithoutUserSuccessConfirm : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void partnerAgentWithoutUserSuccessConfirm()
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
                executionLog.Log("PartnerAgentWithoutUserSuccessConfirm", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("PartnerAgentWithoutUserSuccessConfirm", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("PartnerAgentWithoutUserSuccessConfirm", "Redirect to the URL");
                VisitOffice("partners/agent/create");

                executionLog.Log("PartnerAgentWithoutUserSuccessConfirm", "Verify page title.");
                VerifyTitle("Create a Partner Agent");

                executionLog.Log("PartnerAgentWithoutUserSuccessConfirm", "Select Salutation");
                agent_PartnerAgentHelper.Select("SelectSalutation", "Mr");

                executionLog.Log("PartnerAgentWithoutUserSuccessConfirm", "Enter FirstNAME");
                agent_PartnerAgentHelper.TypeText("FirstName", "Test Agent");

                executionLog.Log("PartnerAgentWithoutUserSuccessConfirm", "Enter LastName");
                agent_PartnerAgentHelper.TypeText("LastName", "Tester");

                executionLog.Log("PartnerAgentWithoutUserSuccessConfirm", "Enter Date Of Birth");
                agent_PartnerAgentHelper.TypeText("BirthDay", "02/01/1991");

                executionLog.Log("PartnerAgentWithoutUserSuccessConfirm", "Enter DBAName");
                agent_PartnerAgentHelper.TypeText("DBAName", "Test DBA");

                executionLog.Log("PartnerAgentWithoutUserSuccessConfirm", "Select Language");
                agent_PartnerAgentHelper.Select("SelectLanguage", "English");

                executionLog.Log("PartnerAgentWithoutUserSuccessConfirm", "Select eAddressType");
                agent_PartnerAgentHelper.Select("eAddressType", "E-Mail");

                executionLog.Log("PartnerAgentWithoutUserSuccessConfirm", "Select eAddressLebel");
                agent_PartnerAgentHelper.Select("eAddressLebel", "Work");

                executionLog.Log("PartnerAgentWithoutUserSuccessConfirm", "Select eAddress");
                agent_PartnerAgentHelper.TypeText("eAddress", "Test@gmail.com");

                executionLog.Log("PartnerAgentWithoutUserSuccessConfirm", "Select SelectPhoneType");
                agent_PartnerAgentHelper.Select("SelectPhoneType", "Work");

                executionLog.Log("PartnerAgentWithoutUserSuccessConfirm", "Select Address Type  ");
                agent_PartnerAgentHelper.Select("AddressType", "Office");

                executionLog.Log("PartnerAgentWithoutUserSuccessConfirm", "Enter AddressLine1");
                agent_PartnerAgentHelper.TypeText("AddressLine1", "FC 89");

                executionLog.Log("PartnerAgentWithoutUserSuccessConfirm", "Enter PhoneNumber");
                agent_PartnerAgentHelper.TypeText("PostalCode", "60601");
                agent_PartnerAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("PartnerAgentBirthDateVerifySave", "Select User Account Check Box");
                agent_PartnerAgentHelper.ClickElement("UserAccChkBox");
                agent_PartnerAgentHelper.WaitForWorkAround(1000);

                executionLog.Log("PartnerAgentBirthDateVerifySave", "Enter Username");
                agent_PartnerAgentHelper.TypeText("UserName", username1);

                executionLog.Log("PartnerAgentBirthDateVerifySave", "Select Avatar Check Box");
                agent_PartnerAgentHelper.ClickElement("ParnterUserAvatar");

                executionLog.Log("PartnerAgentWithoutUserSuccessConfirm", "CLICK Save AGENT btn");
                agent_PartnerAgentHelper.ClickElement("SaveAgent");

                executionLog.Log("PartnerAgentWithoutUserSuccessConfirm", "Verify Text");
                agent_PartnerAgentHelper.WaitForText("Partner Agent Created Successfully.", 10);

                executionLog.Log("PartnerAgentWithoutUserSuccessConfirm", "Click on Edit Partner agent");
                agent_PartnerAgentHelper.ClickElement("EditAgentDetails");
                agent_PartnerAgentHelper.WaitForWorkAround(1000);

                executionLog.Log("PartnerAgentBirthDateVerifySave", "Enter Username");
                agent_PartnerAgentHelper.TypeText("UserName", username1);

                executionLog.Log("PartnerAgentBirthDateVerifySave", "Select Avatar Check Box");
                agent_PartnerAgentHelper.ClickElement("ParnterUserAvatar");

                executionLog.Log("PartnerAgentWithoutUserSuccessConfirm", "Click Save");
                agent_PartnerAgentHelper.ClickElement("SaveAgent");

                executionLog.Log("PartnerAgentWithoutUserSuccessConfirm", "Verify Confirmation");
                agent_PartnerAgentHelper.WaitForText("Partner Agent Updated Successfully.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";
                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("PartnerAgentWithoutUserSuccessConfirm");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("PartnerAgent Without User Success Confirm");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("PartnerAgent Without User Success Confirm", "Bug", "Medium", "Partner page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("PartnerAgent Without User Success Confirm");
                        TakeScreenshot("PartnerAgentWithoutUserSuccessConfirm");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\PartnerAgentWithoutUserSuccessConfirm.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("PartnerAgentWithoutUserSuccessConfirm");
                        string id = loginHelper.getIssueID("PartnerAgent Without User Success Confirm");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\PartnerAgentWithoutUserSuccessConfirm.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("PartnerAgent Without User Success Confirm"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("PartnerAgent Without User Success Confirm");
         //       executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("PartnerAgentWithoutUserSuccessConfirm");
                executionLog.WriteInExcel("PartnerAgent Without User Success Confirm", Status, JIRA, "Agents Portal");
            }
        }
    }
}