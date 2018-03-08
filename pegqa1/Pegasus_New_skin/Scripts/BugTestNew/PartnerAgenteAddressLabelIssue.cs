using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class PartnerAgenteAddressLabelIssue : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS2")]
        [TestCategory("BugTestNew")]
        public void partnerAgenteAddressLabelIssue()
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
            var username1 = "testuser"+ RandomNumber(111,5555);
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("PartnerAgenteAddressLabelIssue", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("PartnerAgenteAddressLabelIssue", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("PartnerAgenteAddressLabelIssue", "Redirect to the URL");
                VisitOffice("partners/agents");
                agent_PartnerAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("PartnerAgenteAddressLabelIssue", "Click on any agent.");
                agent_PartnerAgentHelper.ClickElement("OpenAgent");
                agent_PartnerAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("PartnerAgenteAddressLabelIssue", "Click Edit button");
                agent_PartnerAgentHelper.ClickElement("EditAgentDetails");
                agent_PartnerAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("PartnerAgenteAddressLabelIssue", "Select eAddress type");
                agent_PartnerAgentHelper.SelectByText("eAddressType", "Web Links");

                executionLog.Log("PartnerAgenteAddressLabelIssue", "Enter eAddress");
                agent_PartnerAgentHelper.TypeText("eAddress", "Agent@gmail.com");

                executionLog.Log("PartnerAgentBirthDateVerifySave", "Enter Username");
                agent_PartnerAgentHelper.TypeText("UserName", username1);

                executionLog.Log("PartnerAgentBirthDateVerifySave", "Select Avatar Check Box");
                agent_PartnerAgentHelper.ClickElement("ParnterUserAvatar");

                executionLog.Log("PartnerAgenteAddressLabelIssue", "Click Save button");
                agent_PartnerAgentHelper.ClickElement("ClickSave");
                agent_PartnerAgentHelper.WaitForWorkAround(4000);

                executionLog.Log("PartnerAgenteAddressLabelIssue", "Click Edit button Again");
                agent_PartnerAgentHelper.ClickElement("EditAgentDetails");
                agent_PartnerAgentHelper.WaitForWorkAround(4000);

                executionLog.Log("PartnerAgenteAddressLabelIssue", "Verify eAddress Label as weblink.");
                agent_PartnerAgentHelper.VerifyText("eAddressLebel", "Web Link");
                agent_PartnerAgentHelper.WaitForWorkAround(4000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("PartnerAgenteAddressLabelIssue");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Partner Agent eAddress Label Issue");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Partner Agent eAddress Label Issue", "Bug", "Medium", "Partner page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Partner Agent eAddress Label Issue");
                        TakeScreenshot("PartnerAgenteAddressLabelIssue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\PartnerAgenteAddressLabelIssue.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("PartnerAgenteAddressLabelIssue");
                        string id = loginHelper.getIssueID("Partner Agent eAddress Label Issue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\PartnerAgenteAddressLabelIssue.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Partner Agent eAddress Label Issue"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Partner Agent eAddress Label Issue");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("PartnerAgenteAddressLabelIssue");
                executionLog.WriteInExcel("Partner Agent eAddress Label Issue", Status, JIRA, "Agents Portal");
            }
        }
    }
}
