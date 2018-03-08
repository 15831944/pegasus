using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class SaleAgentWithoutUser : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("Fail")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void saleAgentWithoutUser()
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
                executionLog.Log("SaleAgentWithoutUser", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("SaleAgentWithoutUser", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("SaleAgentWithoutUser", "Redirect To Create Agent");
                VisitOffice("sales_agents/create");

                executionLog.Log("SaleAgentWithoutUser", "Wait for element present.");
                agent_1099SaleAagentHelper.WaitForElementPresent("SelectSalutation", 5);

                executionLog.Log("SaleAgentWithoutUser", "Select Salutation");
                agent_1099SaleAagentHelper.Select("SelectSalutation", "Mr");

                executionLog.Log("SaleAgentWithoutUser", "Enter FirstName");
                agent_1099SaleAagentHelper.TypeText("FirstNAME", "Test Sale gent");

                executionLog.Log("SaleAgentWithoutUser", "Enter LastName");
                agent_1099SaleAagentHelper.TypeText("LastName", "Tester");

                executionLog.Log("SaleAgentWithoutUser", "Select eAddressType");
                agent_1099SaleAagentHelper.Select("eAddressType", "E-Mail");

                executionLog.Log("SaleAgentWithoutUser", "Select eAddressLebel");
                agent_1099SaleAagentHelper.Select("eAddressLebel", "Work");

                executionLog.Log("SaleAgentWithoutUser", "Enter eAddress Type ");
                agent_1099SaleAagentHelper.TypeText("eAddress", "Test@gmail.com");

                executionLog.Log("SaleAgentWithoutUser", "Select SelectPhoneType");
                agent_1099SaleAagentHelper.Select("SelectPhoneType", "Work");

                executionLog.Log("SaleAgentWithoutUser", "Select Address Type ");
                agent_1099SaleAagentHelper.Select("AddressType", "Office");

                executionLog.Log("SaleAgentWithoutUser", "Enter AddressLine1");
                agent_1099SaleAagentHelper.TypeText("AddressLine1", "FC 89");

                executionLog.Log("SaleAgentWithoutUser", "Enter Postal code.");
                agent_1099SaleAagentHelper.TypeText("PostalCode", "60601");

                executionLog.Log("SaleAgentWithoutUser", "CLICK On Save");
                agent_1099SaleAagentHelper.ClickElement("SaveSaleAgent");

                agent_1099SaleAagentHelper.WaitForText("Please correct the errors.", 10);
                agent_1099SaleAagentHelper.WaitForWorkAround(3000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("SaleAgentWithoutUser");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Sale Agent Without User");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Sale Agent Without User", "Bug", "Medium", "Sale Agent page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Sale Agent Without User");
                        TakeScreenshot("SaleAgentWithoutUser");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\SaleAgentWithoutUser.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("SaleAgentWithoutUser");
                        string id = loginHelper.getIssueID("Sale Agent Without User");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\SaleAgentWithoutUser.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Sale Agent Without User"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Sale Agent Without User");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("SaleAgentWithoutUser");
                executionLog.WriteInExcel("Sale Agent Without User", Status, JIRA, "Agents Portal");
            }
        }
    }
}