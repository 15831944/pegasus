using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class SaleAgentWithUser : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("Temp")]
        [TestCategory("Fail")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void saleAgentWithUser()
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
            var office_UserHelper = new Office_UserHelper(GetWebDriver());

            // Variable random
            var name = "TESTCLIENT" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("SaleAgentWithUser", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("SaleAgentWithUser", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("SaleAgentWithUser", "Redirect To Sale Agent");
                VisitOffice("sales_agents/create");

                executionLog.Log("SaleAgentWithUser", "Wait for element to present.");
                agent_1099SaleAagentHelper.WaitForElementPresent("SelectSalutation", 5);

                executionLog.Log("SaleAgentWithUser", "Select Salutation");
                agent_1099SaleAagentHelper.Select("SelectSalutation", "Mr");

                executionLog.Log("SaleAgentWithUser", "Enter FirstNAME");
                agent_1099SaleAagentHelper.TypeText("FirstNAME", "Test Sale gent");

                executionLog.Log("SaleAgentWithUser", "Enter LastName");
                agent_1099SaleAagentHelper.TypeText("LastName", "Tester");

                executionLog.Log("SaleAgentWithUser", "Enter Date Of Birth");
                agent_1099SaleAagentHelper.TypeText("BirthDay", "1991-03-02");

                executionLog.Log("SaleAgentWithUser", "Select eAddressType");
                agent_1099SaleAagentHelper.Select("eAddressType", "E-Mail");

                executionLog.Log("SaleAgentWithUser", "Select eAddressLebel");
                agent_1099SaleAagentHelper.Select("eAddressLebel", "Work");

                executionLog.Log("SaleAgentWithUser", "Enter eAddressType");
                var Email = "Sale" + RandomNumber(111, 9999) + "@yopmail.com";
                agent_1099SaleAagentHelper.TypeText("eAddress", Email);
                Console.WriteLine("Email is" + Email);

                executionLog.Log("SaleAgentWithUser", "Select SelectPhoneType");
                agent_1099SaleAagentHelper.Select("SelectPhoneType", "Work");

                executionLog.Log("SaleAgentWithUser", "Select Address Type   ");
                agent_1099SaleAagentHelper.Select("AddressType", "Office");

                executionLog.Log("SaleAgentWithUser", "Enter AddressLine1");
                agent_1099SaleAagentHelper.TypeText("AddressLine1", "FC 89");

                executionLog.Log("SaleAgentWithUser", "Enter City");
                agent_1099SaleAagentHelper.TypeText("City", "Test City");

                executionLog.Log("SaleAgentWithUser", "Enter Zip code");
                agent_1099SaleAagentHelper.TypeText("PostalCode", "60601");
                agent_1099SaleAagentHelper.WaitForWorkAround(2000);

                executionLog.Log("SaleAgentWithUser", "Check create a user account for agent field open or not");
                agent_1099SaleAagentHelper.ClickAndCheck("CollapseDetails");
                agent_1099SaleAagentHelper.WaitForWorkAround(2000);

                executionLog.Log("SaleAgentWithUser", "Enter UserName");
                agent_1099SaleAagentHelper.TypeText("UserName", name);
                agent_1099SaleAagentHelper.WaitForWorkAround(1000);

                executionLog.Log("SaleAgentWithUser", "Click On Avatar");
                agent_1099SaleAagentHelper.ClickElement("ClickOnSAvatarBtn");
                agent_1099SaleAagentHelper.WaitForWorkAround(2000);

                executionLog.Log("SaleAgentWithUser", "Click on Save");
                agent_1099SaleAagentHelper.ClickElement("SaveSaleAgent");
                agent_1099SaleAagentHelper.WaitForWorkAround(2000);

                executionLog.Log("SaleAgentWithUser", "Redirect To Admin");
                VisitOffice("admin");

                executionLog.Log("SaleAgentWithUser", "Redirect To User");
                VisitOffice("users");
                agent_1099SaleAagentHelper.WaitForWorkAround(2000);

                executionLog.Log("SaleAgentWithUser", "Select user type as sale agent. ");
                office_UserHelper.Select("SearchUserType", "1099 Sales Agent");
                office_UserHelper.WaitForWorkAround(4000);

                executionLog.Log("SaleAgentWithUser", "Enter Email ");
                office_UserHelper.TypeText("EnterEmail", Email);
                office_UserHelper.WaitForWorkAround(30000);

                executionLog.Log("SaleAgentWithUser", "Verify Email ");
                //       office_UserHelper.VerifyPageText(Email);
                office_UserHelper.WaitForWorkAround(2000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("SaleAgentWithUser");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Sale Agent With User");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Sale Agent With User", "Bug", "Medium", "Sale agent page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Sale Agent With User");
                        TakeScreenshot("SaleAgentWithUser");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\SaleAgentWithUser.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("SaleAgentWithUser");
                        string id = loginHelper.getIssueID("Sale Agent With User");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\SaleAgentWithUser.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Sale Agent With User"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Sale Agent With User");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("SaleAgentWithUser");
                executionLog.WriteInExcel("Sale Agent With User", Status, JIRA, "Agents Portal");
            }
        }
    }
}