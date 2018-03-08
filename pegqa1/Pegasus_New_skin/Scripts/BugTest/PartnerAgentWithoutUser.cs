using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class PartnerAgentWithoutUser : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void partnerAgentWithoutUser()
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
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("PartnerAgentWithoutUser", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("PartnerAgentWithoutUser", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("PartnerAgentWithoutUser", "Redirect to the URL");
                VisitOffice("partners/agent/create");

                executionLog.Log("PartnerAgentWithoutUser", "Verify page title.");
                VerifyTitle("Create a Partner Agent");

                executionLog.Log("PartnerAgentWithoutUser", "Select Salutation");
                agent_PartnerAgentHelper.Select("SelectSalutation", "Mr");

                executionLog.Log("PartnerAgentWithoutUser", "Enter FirstNAME");
                agent_PartnerAgentHelper.TypeText("FirstName", "Test Agent");

                executionLog.Log("PartnerAgentWithoutUser", "Enter LastName");
                agent_PartnerAgentHelper.TypeText("LastName", "Tester");

                executionLog.Log("PartnerAgentWithoutUser", "Enter Date Of Birth");
                agent_PartnerAgentHelper.TypeText("BirthDay", "1991-03-02");

                executionLog.Log("PartnerAgentWithoutUser", "Enter DBAName");
                agent_PartnerAgentHelper.TypeText("DBAName", "Test DBA");

                executionLog.Log("PartnerAgentWithoutUser", "Select Language");
                agent_PartnerAgentHelper.Select("SelectLanguage", "English");

                executionLog.Log("PartnerAgentWithoutUser", "Select eAddressType");
                agent_PartnerAgentHelper.Select("eAddressType", "E-Mail");

                executionLog.Log("PartnerAgentWithoutUser", "Select eAddressLebel");
                agent_PartnerAgentHelper.Select("eAddressLebel", "Work");

                executionLog.Log("PartnerAgentWithoutUser", "Enter eAddressType");
                agent_PartnerAgentHelper.TypeText("eAddress", "Test@gmail.com");

                executionLog.Log("PartnerAgentWithoutUser", "Select SelectPhoneType");
                agent_PartnerAgentHelper.Select("SelectPhoneType", "Work");

                executionLog.Log("PartnerAgentWithoutUser", "Select Address Type  ");
                agent_PartnerAgentHelper.Select("AddressType", "Office");

                executionLog.Log("PartnerAgentWithoutUser", "Enter AddressLine1");
                agent_PartnerAgentHelper.TypeText("AddressLine1", "FC 89");

                executionLog.Log("PartnerAgentWithoutUser", "Enter PhoneNumber");
                agent_PartnerAgentHelper.TypeText("PostalCode", "60601");
                agent_PartnerAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("PartnerAgentWithoutUser", "CLICK Save AGENT btn");
                agent_PartnerAgentHelper.ClickElement("SaveAgent");

                executionLog.Log("PartnerAgentWithoutUser", "Verify Text");
                agent_PartnerAgentHelper.VerifyPageText("Partner Agents");
                agent_PartnerAgentHelper.WaitForWorkAround(3000);


            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";
                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("PartnerAgentWithoutUser");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Partner Agent Without User");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Partner Agent Without User", "Bug", "Medium", "Partner page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Partner Agent Without User");
                        TakeScreenshot("PartnerAgentWithoutUser");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\PartnerAgentWithoutUser.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("PartnerAgentWithoutUser");
                        string id = loginHelper.getIssueID("Partner Agent Without User");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\PartnerAgentWithoutUser.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Partner Agent Without User"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Partner Agent Without User");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("PartnerAgentWithoutUser");
                executionLog.WriteInExcel("Partner Agent Without User", Status, JIRA, "Agents Portal");
            }
        }
    }
}