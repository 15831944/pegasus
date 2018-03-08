using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class RevenueAdjustmentSaleManagerEmployeeAddPercentage : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("Test")]
        [TestCategory("TS5")]
        [TestCategory("NewSkinTaskJira")]
        public void revenueAdjustmentSaleManagerEmployeeAddPercentage()
        {
            string[] username = null;
            string[] password = null;
            String JIRA = "";
            String Status = "Pass";

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var agents_EmployeesHelper = new Agents_EmployeesHelper(GetWebDriver());
            var residualIncome_MasterDataHelper = new ResidualIncome_MasterDataHelper(GetWebDriver());

            // VARIABLE
            var name = "TestEmployee" + GetRandomNumber();
            var Adjustment = "Adjustment" + GetRandomNumber();


            try
            {
                executionLog.Log("RevenueAdjustmentSaleManagerEmployeeAddPercentage", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("RevenueAdjustmentSaleManagerEmployeeAddPercentage", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("RevenueAdjustmentSaleManagerEmployeeAddPercentage", "Redirect to employee agents page.");
                VisitOffice("employees");
                agents_EmployeesHelper.WaitForWorkAround(3000);

                var loc = "//table[@id='list1']/tbody/tr[2]";
                agents_EmployeesHelper.WaitForElementPresent(loc, 10);
                if (agents_EmployeesHelper.IsElementPresent(loc))
                {

                    executionLog.Log("RevenueAdjustmentSaleManagerEmployeeAddPercentage", "Click On Sale Agent");
                    agents_EmployeesHelper.ClickElement("ClikOnEmployeeAgent");
                    agents_EmployeesHelper.WaitForWorkAround(3000);

                    executionLog.Log("RevenueAdjustmentSaleManagerEmployeeAddPercentage", "Click On Create btn Adjmnt");
                    agents_EmployeesHelper.ClickElement("ClickOnCreatebtnAdjmnt");

                    executionLog.Log("RevenueAdjustmentSaleManagerEmployeeAddPercentage", "Click on SaleAgent");
                    agents_EmployeesHelper.ClickElement("ClickSaleManager");

                    executionLog.Log("RevenueAdjustmentSaleManagerEmployeeAddPercentage", "Wait for element to present.");
                    agents_EmployeesHelper.WaitForElementPresent("EnterAdjustmentName", 10);

                    executionLog.Log("RevenueAdjustmentSaleManagerEmployeeAddPercentage", "EnterAdjustmentName");
                    agents_EmployeesHelper.TypeText("EnterAdjustmentName", Adjustment);

                    executionLog.Log("RevenueAdjustmentSaleManagerEmployeeAddPercentage", "SelectAdjustmentFor");
                    agents_EmployeesHelper.Select("SelectAdjustmentFor", "Agent");

                    executionLog.Log("RevenueAdjustmentSaleManagerEmployeeAddPercentage", "SelectAdjustmentFor");
                    agents_EmployeesHelper.Select("AdjustmentType", "Transaction");

                    executionLog.Log("RevenueAdjustmentSaleManagerEmployeeAddPercentage", "SelectAdjustmentFor");
                    agents_EmployeesHelper.Select("SelectReportingPeriod", "00");

                    executionLog.Log("RevenueAdjustmentSaleManagerEmployeeAddPercentage", "SelectProcessor");
                    agents_EmployeesHelper.Select("SelectProcessor", "Any");

                    executionLog.Log("RevenueAdjustmentSaleManagerEmployeeAddPercentage", "SelectAdjustmentFor");
                    agents_EmployeesHelper.Select("SelectRuleType", "1");

                    executionLog.Log("RevenueAdjustmentSaleManagerEmployeeAddPercentage", "Enter Amount");
                    agents_EmployeesHelper.TypeText("EnterAmount", "20");

                    executionLog.Log("RevenueAdjustmentSaleManagerEmployeeAddPercentage", "AddRemove");
                    agents_EmployeesHelper.Select("AddRemove", "Add");

                    executionLog.Log("RevenueAdjustmentSaleManagerEmployeeAddPercentage", "Click On Save Btn ");
                    agents_EmployeesHelper.ClickElement("ClickOnSaveBtnAdjustmnet");
                    agents_EmployeesHelper.WaitForWorkAround(3000);

                    executionLog.Log("RevenueAdjustmentSaleManagerEmployeeAddPercentage", "Verify message");
                    agents_EmployeesHelper.WaitForText("Master Adjustment Rules Created Successfully.", 10);

                    executionLog.Log("RevenueAdjustmentSaleManagerEmployeeAddPercentage", "Redirect at adjustment tools page.");
                    VisitOffice("rir/adjustments_tool");
                    agents_EmployeesHelper.WaitForWorkAround(3000);

                    executionLog.Log("RevenueAdjustmentSaleManagerEmployeeAddPercentage", "Verify page title.");
                    VerifyTitle("Adjustments Tool");

                    executionLog.Log("RevenueAdjustmentSaleManagerEmployeeAddPercentage", "Enter adjustment to be deleted.");
                    residualIncome_MasterDataHelper.TypeText("EnterAdjustmentNameSrch", Adjustment);
                    residualIncome_MasterDataHelper.WaitForWorkAround(3000);

                    executionLog.Log("RevenueAdjustmentSaleManagerEmployeeAddPercentage", "Click on delete icon.");
                    residualIncome_MasterDataHelper.ClickElement("DeleteAdjtmnt");

                    executionLog.Log("RevenueAdjustmentSaleManagerEmployeeAddPercentage", "Accept alert message.");
                    residualIncome_MasterDataHelper.AcceptAlert();

                    executionLog.Log("RevenueAdjustmentSaleManagerEmployeeAddPercentage", "Wait for delete success.");
                    residualIncome_MasterDataHelper.WaitForText("Ruleset deleted successfully.", 10);


                }
                else
                {

                    executionLog.Log("RevenueAdjustmentSaleManagerEmployeeAddPercentage", "Click On Create Employee Btn");
                    agents_EmployeesHelper.ClickElement("Create");

                    executionLog.Log("RevenueAdjustmentSaleManagerEmployeeAddPercentage", "Select Salutation");
                    agents_EmployeesHelper.Select("SelectSalutation", "Mr");

                    executionLog.Log("RevenueAdjustmentSaleManagerEmployeeAddPercentage", "Enter FirstNAME");
                    agents_EmployeesHelper.TypeText("FirstNAME", "Employee Agent");

                    executionLog.Log("RevenueAdjustmentSaleManagerEmployeeAddPercentage", "Enter LastName");
                    agents_EmployeesHelper.TypeText("LastName", "Tester");

                    executionLog.Log("RevenueAdjustmentSaleManagerEmployeeAddPercentage", "Enter Date Of Birth");
                    agents_EmployeesHelper.TypeText("BirthDay", "1991-03-02");

                    executionLog.Log("RevenueAdjustmentSaleManagerEmployeeAddPercentage", "Select eAddressType");
                    agents_EmployeesHelper.Select("eAddressType", "E-Mail");

                    executionLog.Log("RevenueAdjustmentSaleManagerEmployeeAddPercentage", "Select eAddressLebel");
                    agents_EmployeesHelper.Select("eAddressLebel", "Work");

                    executionLog.Log("RevenueAdjustmentSaleManagerEmployeeAddPercentage", "Enter eAddressType");
                    agents_EmployeesHelper.TypeText("eAddress", "Test@gmail.com");

                    executionLog.Log("RevenueAdjustmentSaleManagerEmployeeAddPercentage", "Select SelectPhoneType");
                    agents_EmployeesHelper.Select("SelectPhoneType", "Work");
                    agents_EmployeesHelper.WaitForWorkAround(3000);

                    executionLog.Log("RevenueAdjustmentSaleManagerEmployeeAddPercentage", "Enter PhoneNumber");
                    agents_EmployeesHelper.TypeText("PhoneNumber", "121212121");
                    agents_EmployeesHelper.WaitForWorkAround(3000);

                    executionLog.Log("RevenueAdjustmentSaleManagerEmployeeAddPercentage", "Select Address Type");
                    agents_EmployeesHelper.Select("AddressType", "Office");
                    agents_EmployeesHelper.WaitForWorkAround(3000);

                    executionLog.Log("RevenueAdjustmentSaleManagerEmployeeAddPercentage", "Enter AddressLine1");
                    agents_EmployeesHelper.TypeText("AddressLine1", "FC 89");
                    agents_EmployeesHelper.WaitForWorkAround(3000);

                    executionLog.Log("RevenueAdjustmentSaleManagerEmployeeAddPercentage", "Enter Postal code");
                    agents_EmployeesHelper.TypeText("PostalCode", "60601");
                    agents_EmployeesHelper.WaitForWorkAround(3000);

                    executionLog.Log("RevenueAdjustmentSaleManagerEmployeeAddPercentage", "Click on save button.");
                    agents_EmployeesHelper.ClickElement("ClickSaveNskin");
                    agents_EmployeesHelper.WaitForWorkAround(3000);

                    executionLog.Log("RevenueAdjustmentSaleManagerEmployeeAddPercentage", "Wait for success message");
                    agents_EmployeesHelper.WaitForText("The employee is successfully added", 10);

                    executionLog.Log("RevenueAdjustmentSaleManagerEmployeeAddPercentage", "Enter agent name to search");
                    agents_EmployeesHelper.TypeText("EnterAgentName", "Employee Agent Tester");
                    agents_EmployeesHelper.WaitForWorkAround(3000);

                    agents_EmployeesHelper.Select("SelectStatusAdjtmnt", "");
                    agents_EmployeesHelper.WaitForWorkAround(3000);

                    executionLog.Log("RevenueAdjustmentSaleManagerEmployeeAddPercentage", "Click On Sale Agent");
                    agents_EmployeesHelper.ClickElement("ClikOnEmployeeAgent");

                    executionLog.Log("RevenueAdjustmentSaleManagerEmployeeAddPercentage", "Click On Create btn Adjmnt");
                    agents_EmployeesHelper.ClickElement("ClickOnCreatebtnAdjmnt");

                    executionLog.Log("RevenueAdjustmentSaleManagerEmployeeAddPercentage", "Click on SaleAgent");
                    agents_EmployeesHelper.ClickElement("ClickSaleManager");

                    executionLog.Log("RevenueAdjustmentSaleManagerEmployeeAddPercentage", "EnterAdjustmentName");
                    agents_EmployeesHelper.TypeText("EnterAdjustmentName", "Employee Sale Manager");

                    executionLog.Log("RevenueAdjustmentSaleManagerEmployeeAddPercentage", "SelectAdjustmentFor");
                    agents_EmployeesHelper.Select("SelectAdjustmentFor", "Agent");

                    executionLog.Log("RevenueAdjustmentSaleManagerEmployeeAddPercentage", "SelectAdjustmentFor");
                    agents_EmployeesHelper.Select("AdjustmentType", "Transaction");

                    executionLog.Log("RevenueAdjustmentSaleManagerEmployeeAddPercentage", "SelectAdjustmentFor");
                    agents_EmployeesHelper.Select("SelectReportingPeriod", "00");

                    executionLog.Log("RevenueAdjustmentSaleManagerEmployeeAddPercentage", "SelectProcessor");
                    agents_EmployeesHelper.Select("SelectProcessor", "Any");

                    executionLog.Log("RevenueAdjustmentSaleManagerEmployeeAddPercentage", "SelectAdjustmentFor");
                    agents_EmployeesHelper.Select("SelectRuleType", "1");

                    executionLog.Log("RevenueAdjustmentSaleManagerEmployeeAddPercentage", "Enter Amount");
                    agents_EmployeesHelper.TypeText("EnterAmount", "20");

                    executionLog.Log("RevenueAdjustmentSaleManagerEmployeeAddPercentage", "AddRemove");
                    agents_EmployeesHelper.Select("AddRemove", "Add");

                    executionLog.Log("RevenueAdjustmentSaleManagerEmployeeAddPercentage", "ClickOnSaveBtnAdjustmnet");
                    agents_EmployeesHelper.ClickElement("ClickOnSaveBtnAdjustmnet");
                    agents_EmployeesHelper.WaitForWorkAround(3000);

                    executionLog.Log("RevenueAdjustmentSaleManagerEmployeeAddPercentage", "Verify Message");
                    agents_EmployeesHelper.VerifyPageText("Master Adjustment Rules Created Successfully.");

                    executionLog.Log("RevenueAdjustmentSaleManagerEmployeeAddPercentage", "Redirect at adjustment tools page.");
                    VisitOffice("rir/adjustments_tool");
                    agents_EmployeesHelper.WaitForWorkAround(3000);

                    executionLog.Log("RevenueAdjustmentSaleManagerEmployeeAddPercentage", "Verify page title.");
                    VerifyTitle("Adjustments Tool");

                    executionLog.Log("RevenueAdjustmentSaleManagerEmployeeAddPercentage", "Enter adjustment to be deleted.");
                    residualIncome_MasterDataHelper.TypeText("EnterAdjustmentNameSrch", Adjustment);
                    residualIncome_MasterDataHelper.WaitForWorkAround(3000);

                    executionLog.Log("RevenueAdjustmentSaleManagerEmployeeAddPercentage", "Click on delete icon.");
                    residualIncome_MasterDataHelper.ClickElement("DeleteAdjtmnt");

                    executionLog.Log("RevenueAdjustmentSaleManagerEmployeeAddPercentage", "Accept alert message.");
                    residualIncome_MasterDataHelper.AcceptAlert();

                    executionLog.Log("RevenueAdjustmentSaleManagerEmployeeAddPercentage", "Wait for delete success.");
                    residualIncome_MasterDataHelper.WaitForText("Ruleset deleted successfully.", 10);

                }
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("RevenueAdjustmentSaleManagerEmployeeAddPercentage");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Revenue Adjustment Sale Manager Employee Add Percentage");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Revenue Adjustment Sale Manager Employee Add Percentage", "Bug", "Medium", "Employee Agent page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Revenue Adjustment Sale Manager Employee Add Percentage");
                        TakeScreenshot("RevenueAdjustmentSaleManagerEmployeeAddPercentage");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\RevenueAdjustmentSaleManagerEmployeeAddPercentage.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("RevenueAdjustmentSaleManagerEmployeeAddPercentage");
                        string id = loginHelper.getIssueID("Revenue Adjustment Sale Manager Employee Add Percentage");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\RevenueAdjustmentSaleManagerEmployeeAddPercentage.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Revenue Adjustment Sale Manager Employee Add Percentage"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Revenue Adjustment Sale Manager Employee Add Percentage");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("RevenueAdjustmentSaleManagerEmployeeAddPercentage");
                executionLog.WriteInExcel("Revenue Adjustment Sale Manager Employee Add Percentage", Status, JIRA, "Agent Portal");
            }
        }
    }
}