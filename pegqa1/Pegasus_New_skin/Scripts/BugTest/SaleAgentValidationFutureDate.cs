using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class SaleAgentValidationFutureDate : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void saleAgentValidationFutureDate()
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
            String JIRA = "";
            String Status = "Pass";


            try
            {
                executionLog.Log("SaleAgentValidationFutureDate", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("SaleAgentValidationFutureDate", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("SaleAgentValidationFutureDate", "Redirect To Create Agent");
                VisitOffice("sales_agents/create");

                executionLog.Log("SaleAgentValidationFutureDate", "Wait for elelment to present.");
                agent_1099SaleAagentHelper.WaitForElementPresent("SelectSalutation", 08);

                executionLog.Log("SaleAgentValidationFutureDate", "Select Salutation");
                agent_1099SaleAagentHelper.Select("SelectSalutation", "Mr");

                executionLog.Log("SaleAgentValidationFutureDate", "Enter FirstName");
                agent_1099SaleAagentHelper.TypeText("FirstNAME", "Test Sale gent");

                executionLog.Log("SaleAgentValidationFutureDate", "Enter LastName");
                agent_1099SaleAagentHelper.TypeText("LastName", "Tester");

                executionLog.Log("SaleAgentValidationFutureDate", "Enter Date Of Birth");
                agent_1099SaleAagentHelper.TypeText("BirthDay", "2018-03-02");

                executionLog.Log("SaleAgentValidationFutureDate", "Select eAddressType");
                agent_1099SaleAagentHelper.Select("eAddressType", "E-Mail");

                executionLog.Log("SaleAgentValidationFutureDate", "Select eAddressLebel");
                agent_1099SaleAagentHelper.Select("eAddressLebel", "Work");

                executionLog.Log("SaleAgentValidationFutureDate", "Enter eAddress");
                agent_1099SaleAagentHelper.TypeText("eAddress", "Test@gmail.com");

                executionLog.Log("SaleAgentValidationFutureDate", "Select SelectPhoneType");
                agent_1099SaleAagentHelper.Select("SelectPhoneType", "Work");

                executionLog.Log("SaleAgentValidationFutureDate", "Select Address Type ");
                agent_1099SaleAagentHelper.Select("AddressType", "Office");

                executionLog.Log("SaleAgentValidationFutureDate", "Enter AddressLine1");
                agent_1099SaleAagentHelper.TypeText("AddressLine1", "FC 89");

                executionLog.Log("SaleAgentValidationFutureDate", "Enter Postal code");
                agent_1099SaleAagentHelper.TypeText("PostalCode", "60601");

                executionLog.Log("SaleAgentValidationFutureDate", "CLICK On Save");
                agent_1099SaleAagentHelper.ClickElement("SaveSaleAgent");

                agent_1099SaleAagentHelper.WaitForText("Age should be greater than 18.", 10);
                agent_1099SaleAagentHelper.WaitForWorkAround(3000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("SaleAgentValidationFutureDate");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Sale Agent Validation Future Date");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Sale Agent Validation Future Date", "Bug", "Medium", "Sale Agent page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Sale Agent Validation Future Date");
                        TakeScreenshot("SaleAgentValidationFutureDate");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\SaleAgentValidationFutureDate.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("SaleAgentValidationFutureDate");
                        string id = loginHelper.getIssueID("Sale Agent Validation Future Date");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\SaleAgentValidationFutureDate.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Sale Agent Validation Future Date"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Sale Agent Validation Future Date");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("SaleAgentValidationFutureDate");
                executionLog.WriteInExcel("Sale Agent Validation Future Date", Status, JIRA, "Agents Portal");
            }
        }
    }
}