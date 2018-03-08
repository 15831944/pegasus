using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class PartnerAgentWithUser : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void partnerAgentWithUser()
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
            var office_UserHelper = new Office_UserHelper(GetWebDriver());

            // Variable
            var name = "TestAgent" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("PartnerAgentWithUser", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("PartnerAgentWithUser", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("PartnerAgentWithUser", "Redirect to the URL");
                VisitOffice("partners/agent/create");

                executionLog.Log("PartnerAgentWithUser", "Verify page title.");
                VerifyTitle("Create a Partner Agent");

                executionLog.Log("PartnerAgentWithUser", "Select Salutation");
                agent_PartnerAgentHelper.Select("SelectSalutation", "Mr");

                executionLog.Log("PartnerAgentWithUser", "Enter FirstNAME");
                agent_PartnerAgentHelper.TypeText("FirstName", "Test Agent");

                executionLog.Log("PartnerAgentWithUser", "Enter LastName");
                agent_PartnerAgentHelper.TypeText("LastName", "Tester");

                executionLog.Log("PartnerAgentWithUser", "Enter DBAName");
                agent_PartnerAgentHelper.TypeText("DBAName", "Test DBA");

                executionLog.Log("PartnerAgentWithUser", "Select eAddressType");
                agent_PartnerAgentHelper.Select("eAddressType", "E-Mail");

                executionLog.Log("PartnerAgentWithUser", "Select eAddressLebel");
                agent_PartnerAgentHelper.Select("eAddressLebel", "Work");

                executionLog.Log("PartnerAgentWithUser", "Enter eAddressType");
                agent_PartnerAgentHelper.TypeText("eAddress", "12@gmail.com");

                executionLog.Log("PartnerAgentWithUser", "Select SelectPhoneType");
                agent_PartnerAgentHelper.Select("SelectPhoneType", "Work");

                executionLog.Log("PartnerAgentWithUser", "Select Address Type  ");
                agent_PartnerAgentHelper.Select("AddressType", "Office");

                executionLog.Log("PartnerAgentWithUser", "Enter AddressLine1");
                agent_PartnerAgentHelper.TypeText("AddressLine1", "FC 89");

                executionLog.Log("PartnerAgentWithUser", "Enter Zip code");
                agent_PartnerAgentHelper.TypeText("PostalCode", "60601");
                agent_PartnerAgentHelper.WaitForWorkAround(2000);

                executionLog.Log("PartnerAgentWithUser", "Click On Checkbox");
                agent_PartnerAgentHelper.ClickViaJavaScript("//*[@id='UserCreateUser']");

                executionLog.Log("PartnerAgentWithUser", "Enter UserName");
                agent_PartnerAgentHelper.TypeText("UserName", name);

                executionLog.Log("PartnerAgentWithUser", "Click On Avatar");
                agent_PartnerAgentHelper.ClickElement("ClickOnAvatar");
                agent_PartnerAgentHelper.WaitForWorkAround(1000);

                executionLog.Log("PartnerAgentWithUser", "CLICK Save AGENT btn");
                agent_PartnerAgentHelper.ClickElement("SaveAgent");
                agent_PartnerAgentHelper.WaitForWorkAround(1000);

                executionLog.Log("PartnerAgentWithUser", "Redirect To Admin");
                VisitOffice("admin");
                agent_PartnerAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("PartnerAgentWithUser", "Redirect To User ");
                VisitOffice("users");
                office_UserHelper.WaitForWorkAround(2000);

                executionLog.Log("PartnerAgentWithUser", "Select Status");
                office_UserHelper.Select("SelectStatus1", "");
                office_UserHelper.WaitForWorkAround(1000);

                executionLog.Log("PartnerAgentWithUser", "Enter email in email filter.");
                office_UserHelper.TypeText("EnterEmail", "12@gmail.com");
                office_UserHelper.WaitForWorkAround(6000);


            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";
                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("PartnerAgentWithUser");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Partner Agent With User");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Partner Agent With User", "Bug", "Medium", "Partner page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Partner Agent With User");
                        TakeScreenshot("PartnerAgentWithUser");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\PartnerAgentWithUser.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("PartnerAgentWithUser");
                        string id = loginHelper.getIssueID("Partner Agent With User");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\PartnerAgentWithUser.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Partner Agent With User"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Partner Agent With User");
                //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("PartnerAgentWithUser");
                executionLog.WriteInExcel("Partner Agent With User", Status, JIRA, "Agents Portal");
            }
        }
    }
}