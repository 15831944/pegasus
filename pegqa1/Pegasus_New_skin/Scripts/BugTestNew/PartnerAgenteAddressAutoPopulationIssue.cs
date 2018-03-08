using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class PartnerAgenteAddressAutoPopulationIssue : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("Temp")]
        public void partnerAgenteAddressAutoPopulationIssue()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username");
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
            executionLog.Log("PartnerAgenteAddressAutoPopulationIssue", "Login with valid username and password");
            Login("Tester.test", "1qaz!QAZ");

            executionLog.Log("PartnerAgenteAddressAutoPopulationIssue", "Wait for locator to present.");
            agent_PartnerAgentHelper.WaitForElementPresent("EditP.Agent", 10);

            executionLog.Log("PartnerAgenteAddressAutoPopulationIssue", "Click on edit button.");
            agent_PartnerAgentHelper.ClickElement("EditP.Agent");
            agent_PartnerAgentHelper.WaitForWorkAround(2000);

            executionLog.Log("PartnerAgenteAddressAutoPopulationIssue", "Select eaddress type as weblink.");
            agent_PartnerAgentHelper.Select("PAddressType", "Web Links");
            agent_PartnerAgentHelper.WaitForWorkAround(2000);

            executionLog.Log("PartnerAgenteAddressAutoPopulationIssue", "Click on save button");
            agent_PartnerAgentHelper.ClickElement("SaveAgent");
            agent_PartnerAgentHelper.WaitForWorkAround(2000);

            executionLog.Log("PartnerAgenteAddressAutoPopulationIssue", "Click on edit icon.");
            agent_PartnerAgentHelper.ClickElement("EditP.Agent");
            agent_PartnerAgentHelper.WaitForWorkAround(2000);

            executionLog.Log("PartnerAgenteAddressAutoPopulationIssue", "Verify eAddress label as web link");
            agent_PartnerAgentHelper.VerifyLabel("VerifySelectedLabel");
            agent_PartnerAgentHelper.WaitForWorkAround(2000);

            executionLog.Log("PartnerAgenteAddressAutoPopulationIssue", "Click on save button");
            agent_PartnerAgentHelper.ClickElement("SaveAgent");
            agent_PartnerAgentHelper.WaitForWorkAround(2000);

        }
       catch (Exception e)
          {
              executionLog.Log("Error", e.StackTrace);
              Status = "Fail";

              String counter = executionLog.readLastLine("counter");
              String Description = executionLog.GetAllTextFile("PartnerAgenteAddressAutoPopulationIssue");
              String Error = executionLog.GetAllTextFile("Error");
              Console.WriteLine(Error);
              if (counter == "")
              {
                  counter = "0";
              }
              bool result = loginHelper.CheckExstingIssue("Partner Agent eAddress Auto Population Issue");
              if (!result)
              {
                  if (Int16.Parse(counter) < 5)
                  {
                      executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                      loginHelper.CreateIssue("Partner Agent eAddress Auto Population Issue", "Bug", "Medium", "Partner page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                      string id = loginHelper.getIssueID("Partner Agent eAddress Auto Population Issue");
                      TakeScreenshot("PartnerAgenteAddressAutoPopulationIssue");
                      string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                      var location = directoryName + "\\PartnerAgenteAddressAutoPopulationIssue.png";
                      loginHelper.AddAttachment(location, id);
                  }
              }
              else
              {
                  if (Int16.Parse(counter) < 5)
                  {
                      executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                      TakeScreenshot("PartnerAgenteAddressAutoPopulationIssue");
                      string id = loginHelper.getIssueID("Partner Agent eAddress Auto Population Issue");
                      string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                      var location = directoryName + "\\PartnerAgenteAddressAutoPopulationIssue.png";
                      loginHelper.AddAttachment(location, id);
                      loginHelper.AddComment(loginHelper.getIssueID("Partner Agent eAddress Auto Population Issue"), "This issue is still occurring");
                  }
              }
              JIRA = loginHelper.getIssueID("Partner Agent eAddress Auto Population Issue");
              executionLog.DeleteFile("Error");
              throw;

          }
          finally
          {
              executionLog.DeleteFile("PartnerAgenteAddressAutoPopulationIssue");
              executionLog.WriteInExcel("Partner Agent eAddress Auto Population Issue", Status, JIRA, "Agents Portal");
          }
      }
  }
}