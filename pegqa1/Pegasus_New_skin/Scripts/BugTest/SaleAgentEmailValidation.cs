using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class SaleAgentEmailValidation : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("Temp")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void saleAgentEmailValidation()
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
            var agent_1099SaleAagentHelper = new Agent_1099SalesAgentHelper(GetWebDriver());

            // Variable random
            var name = "TESTCLIENT" + RandomNumber(1, 9999);
            String JIRA = "";
            String Status = "Pass";


            try
            {
                executionLog.Log("SaleAgentEmailValidation", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("SaleAgentEmailValidation", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("SaleAgentEmailValidation", "Redirect To Create Agent page");
                VisitOffice("sales_agents/create");

                executionLog.Log("SaleAgentEmailValidation", "Wait for element to present");
                agent_1099SaleAagentHelper.WaitForElementPresent("SelectSalutation", 10);

                executionLog.Log("SaleAgentEmailValidation", "Select Salutation");
                agent_1099SaleAagentHelper.Select("SelectSalutation", "Mr");

                executionLog.Log("SaleAgentEmailValidation", "Enter FirstName");
                agent_1099SaleAagentHelper.TypeText("FirstNAME", "Test Sale gent");

                executionLog.Log("SaleAgentEmailValidation", "Enter LastName");
                agent_1099SaleAagentHelper.TypeText("LastName", "Tester");

                executionLog.Log("SaleAgentEmailValidation", "Enter Date Of Birth");
                agent_1099SaleAagentHelper.TypeText("BirthDay", "1991-03-02");

                executionLog.Log("SaleAgentEmailValidation", "Select eAddressType");
                agent_1099SaleAagentHelper.Select("eAddressType", "E-Mail");

                executionLog.Log("SaleAgentEmailValidation", "Select eAddressLebel");
                agent_1099SaleAagentHelper.Select("eAddressLebel", "Work");

                executionLog.Log("SaleAgentEmailValidation", "Enter eAddress Type ");
                agent_1099SaleAagentHelper.TypeText("eAddress", "Testgmail.com");

                executionLog.Log("SaleAgentEmailValidation", "CLICK On Save");
                agent_1099SaleAagentHelper.ClickElement("SaveSaleAgent");

                agent_1099SaleAagentHelper.WaitForText("Please enter a valid email address.", 10);
                agent_1099SaleAagentHelper.WaitForWorkAround(3000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("SaleAgentEmailValidation");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Sale Agent Email Validation");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Sale Agent Email Validation", "Bug", "Medium", "Sale Agent page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Sale Agent Email Validation");
                        TakeScreenshot("SaleAgentEmailValidation");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\SaleAgentEmailValidation.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("SaleAgentEmailValidation");
                        string id = loginHelper.getIssueID("Sale Agent Email Validation");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\SaleAgentEmailValidation.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Sale Agent Email Validation"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Sale Agent Email Validation");
           //     executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("SaleAgentEmailValidation");
                executionLog.WriteInExcel("Sale Agent Email Validation", Status, JIRA, "Agents Portal");
            }
        }
    }
}