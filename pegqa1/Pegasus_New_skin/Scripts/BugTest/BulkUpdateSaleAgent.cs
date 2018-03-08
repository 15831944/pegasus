using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class BulkUpdateSaleAgent : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void bulkUpdateSaleAgent()
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
            var filePath = GetPathToFile() + "AgentImport.xlsx";
            var name = "TESTCLIENT" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("BulkUpdateSaleAgent", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("BulkUpdateSaleAgent", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("BulkUpdateSaleAgent", "Go to Sales agent page.");
                VisitOffice("sales_agents");
                agent_1099SaleAagentHelper.WaitForWorkAround(3000);

                executionLog.Log("BulkUpdateSaleAgent", "Verify title");
                VerifyTitle("Sales Agents");

                executionLog.Log("BulkUpdateSaleAgent", "Click On Sale Agnet ChkBox");
                agent_1099SaleAagentHelper.ClickElement("ClickOnSaleAgnetChkBox");

                executionLog.Log("BulkUpdateSaleAgent", "ClickOnBulkUpdate");
                agent_1099SaleAagentHelper.ClickJs("ClickOnBulkUpdate");
                agent_1099SaleAagentHelper.WaitForWorkAround(2000);

                executionLog.Log("BulkUpdateSaleAgent", "Click on change department");
                agent_1099SaleAagentHelper.ClickElement("ChangeDepartment");
                agent_1099SaleAagentHelper.WaitForWorkAround(2000);

                executionLog.Log("BulkUpdateSaleAgent", "Select Department");
                agent_1099SaleAagentHelper.SelectByText("SelectDepartmentSaleAgnet", "Information Technology");

                executionLog.Log("BulkUpdateSaleAgent", "ClickOnBulkUpdate");
                agent_1099SaleAagentHelper.ClickDisplayed("//a[@title='Update']");
                agent_1099SaleAagentHelper.AcceptAlert();
                agent_1099SaleAagentHelper.WaitForWorkAround(4000);

                executionLog.Log("BulkUpdateSaleAgent", "ClickOnBulkUpdate");
                agent_1099SaleAagentHelper.ClickElement("ClickOnSaleAgnetChkBox");
                agent_1099SaleAagentHelper.ClickJs("ClickOnBulkUpdate");
                agent_1099SaleAagentHelper.WaitForWorkAround(2000);

                executionLog.Log("BulkUpdateSaleAgent", "ClickOnChange role");
                agent_1099SaleAagentHelper.ClickElement("ChangeRole");
                agent_1099SaleAagentHelper.WaitForWorkAround(2000);

                executionLog.Log("BulkUpdateSaleAgent", "ClickOnBulkUpdate");
                agent_1099SaleAagentHelper.Click("//*[@id='EmpRoleUpdateSalesUsersForm']/div[3]/a");
                agent_1099SaleAagentHelper.AcceptAlert();
                agent_1099SaleAagentHelper.WaitForWorkAround(4000);

                executionLog.Log("BulkUpdateSaleAgent", "Click on first check box");
                agent_1099SaleAagentHelper.ClickElement("ClickOnSaleAgnetChkBox");

                executionLog.Log("BulkUpdateSaleAgent", "ClickOnBulkUpdate");
                agent_1099SaleAagentHelper.ClickJs("ClickOnBulkUpdate");
                agent_1099SaleAagentHelper.WaitForWorkAround(3000);

                executionLog.Log("BulkUpdateSaleAgent", "Change Team");
                agent_1099SaleAagentHelper.ClickElement("ChangeTeam");
                agent_1099SaleAagentHelper.WaitForWorkAround(2000);

                executionLog.Log("BulkUpdateSaleAgent", "Click on update button");
                agent_1099SaleAagentHelper.ClickViaJavaScript("//*[@id='EmpTeamUpdateSalesUsersForm']//a");
                agent_1099SaleAagentHelper.AcceptAlert();
                agent_1099SaleAagentHelper.WaitForWorkAround(4000);

                executionLog.Log("BulkUpdateSaleAgent", "Click On first check box");
                agent_1099SaleAagentHelper.ClickElement("ClickOnSaleAgnetChkBox");

                executionLog.Log("BulkUpdateSaleAgent", "ClickOnBulkUpdate");
                agent_1099SaleAagentHelper.ClickJs("ClickOnBulkUpdate");
                agent_1099SaleAagentHelper.WaitForWorkAround(2000);

                executionLog.Log("BulkUpdateSaleAgent", "Change Status");
                agent_1099SaleAagentHelper.ClickElement("ChangeStatus");
                agent_1099SaleAagentHelper.WaitForWorkAround(2000);

                executionLog.Log("BulkUpdateSaleAgent", "Select Department");
                agent_1099SaleAagentHelper.Select("SelectStatus", "Active");

                executionLog.Log("BulkUpdateSaleAgent", "Click On Bulk Update");
                agent_1099SaleAagentHelper.ClickDisplayed("//a[@title='Update']");

                executionLog.Log("BulkUpdateSaleAgent", "Accept alert message.");
                agent_1099SaleAagentHelper.AcceptAlert();
                agent_1099SaleAagentHelper.WaitForWorkAround(1000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("BulkUpdateSaleAgent");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Bulk Update Sale Agent");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Bulk Update Sale Agent", "Bug", "Medium", "Sale Agent", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Bulk Update Sale Agent");
                        TakeScreenshot("BulkUpdateSaleAgent");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\BulkUpdateSaleAgent.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("BulkUpdateSaleAgent");
                        string id = loginHelper.getIssueID("Bulk Update Sale Agent");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\BulkUpdateSaleAgent.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Bulk Update Sale Agent"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Bulk Update Sale Agent");
                //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("BulkUpdateSaleAgent");
                executionLog.WriteInExcel("Bulk Update Sale Agent", Status, JIRA, "Agents Portal");
            }
        }
    }
}
