using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class VerifyPrimaryRadioBtnForDifferentEAddressTypePartnerAgent : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("BugTestNew")]
        public void verifyPrimaryRadioBtnForDifferentEAddressTypePartnerAgent()
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

            // VARIABLE
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("VerifyPrimaryRadioBtnForDifferentEAddressTypePartnerAgent", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyPrimaryRadioBtnForDifferentEAddressTypePartnerAgent", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("VerifyPrimaryRadioBtnForDifferentEAddressTypePartnerAgent", "Redirect To Create Partner Agent page");
                VisitOffice("partners/agent/create");
                agents_PartnerAgentsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyPrimaryRadioBtnForDifferentEAddressTypePartnerAgent", "Select eAddress Type >> E-mail");
                agents_PartnerAgentsHelper.SelectByText("eAddressType", "E-Mail");
                agents_PartnerAgentsHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyPrimaryRadioBtnForDifferentEAddressTypePartnerAgent", "Verify Primary radio button present");
                agents_PartnerAgentsHelper.IsElementPresent("//input[@name='data[PartnerAgentElectronicAddress][0][primary]']");

                executionLog.Log("VerifyPrimaryRadioBtnForDifferentEAddressTypePartnerAgent", "Select eAddress Type >> IM");
                agents_PartnerAgentsHelper.SelectByText("eAddressType", "IM");
                agents_PartnerAgentsHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyPrimaryRadioBtnForDifferentEAddressTypePartnerAgent", "Verify Primary radio button present");
                agents_PartnerAgentsHelper.IsElementPresent("//input[@name='data[PartnerAgentElectronicAddress][0][primary]']");

                executionLog.Log("VerifyPrimaryRadioBtnForDifferentEAddressTypePartnerAgent", "Select eAddress Type >> Social Media");
                agents_PartnerAgentsHelper.SelectByText("eAddressType", "Social Media");
                agents_PartnerAgentsHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyPrimaryRadioBtnForDifferentEAddressTypePartnerAgent", "Verify Primary radio button present");
                agents_PartnerAgentsHelper.IsElementPresent("//input[@name='data[PartnerAgentElectronicAddress][0][primary]']");

                executionLog.Log("VerifyPrimaryRadioBtnForDifferentEAddressTypePartnerAgent", "Select eAddress Type >> Web Links");
                agents_PartnerAgentsHelper.SelectByText("eAddressType", "Web Links");
                agents_PartnerAgentsHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyPrimaryRadioBtnForDifferentEAddressTypePartnerAgent", "Verify Primary radio button present");
                agents_PartnerAgentsHelper.IsElementPresent("//input[@name='data[PartnerAgentElectronicAddress][0][primary]']");


            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyPrimaryRadioBtnForDifferentEAddressTypePartnerAgent");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Primary Radio Btn For Different EAddress Type Partner Agent");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Primary Radio Btn For Different EAddress Type Partner Agent", "Bug", "Medium", "Partner Agent page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Primary Radio Btn For Different EAddress Type Partner Agent");
                        TakeScreenshot("VerifyPrimaryRadioBtnForDifferentEAddressTypePartnerAgent");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyPrimaryRadioBtnForDifferentEAddressTypePartnerAgent.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyPrimaryRadioBtnForDifferentEAddressTypePartnerAgent");
                        string id = loginHelper.getIssueID("Verify Primary Radio Btn For Different EAddress Type Partner Agent");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyPrimaryRadioBtnForDifferentEAddressTypePartnerAgent.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Primary Radio Btn For Different EAddress Type Partner Agent"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Primary Radio Btn For Different EAddress Type Partner Agent");
                //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyPrimaryRadioBtnForDifferentEAddressTypePartnerAgent");
                executionLog.WriteInExcel("Verify Primary Radio Btn For Different EAddress Type Partner Agent", Status, JIRA, "Office Partner Agent");
            }
           
        }
    }
}