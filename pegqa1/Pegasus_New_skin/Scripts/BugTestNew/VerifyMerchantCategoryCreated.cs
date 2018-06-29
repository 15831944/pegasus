using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class VerifyMerchantCategoryCreated : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin")]
        [TestCategory("TS2")]
        [TestCategory("BugTestNew")]
        public void verifyMerchantCategoryCreated()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var masterData_CategoryHelper = new MasterData_CategoryHelper(GetWebDriver());
            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Variable
            var CatName = "Test" + RandomNumber(99, 99999);
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("VerifyMerchantCategoryCreated", "Login with valid credentials");
                Login(username[0], password[0]);

                executionLog.Log("VerifyMerchantCategoryCreated", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyMerchantCategoryCreated", "Visit Craete Client");
                VisitOffice("categories");
                masterData_CategoryHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyMerchantCategoryCreated", "Search Cients");
                masterData_CategoryHelper.TypeText("SearchName", "Clients");
                masterData_CategoryHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyMerchantCategoryCreated", "Open category");
                masterData_CategoryHelper.ClickElement("Category1");
                masterData_CategoryHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyMerchantCategoryCreated", "Click on Create button");
                masterData_CategoryHelper.ClickElement("MerchantCreate");
                masterData_CategoryHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyMerchantCategoryCreated", "Enter Name of category");
                masterData_CategoryHelper.TypeText("CategoryName", CatName);

                executionLog.Log("VerifyMerchantCategoryCreated", "Click on Save button");
                masterData_CategoryHelper.ClickElement("SaveBtn");
                masterData_CategoryHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyMerchantCategoryCreated", "Verify creation message");
                masterData_CategoryHelper.WaitForText("Category saved successfully.", 05);
                masterData_CategoryHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyMerchantCategoryCreated", "Enter Merchant Category name to search");
                masterData_CategoryHelper.TypeText("SearchMerchant", CatName);
                masterData_CategoryHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyMerchantCategoryCreated", "Click on Delete icon");
                masterData_CategoryHelper.ClickElement("DeleteMerchant1");
                masterData_CategoryHelper.AcceptAlert();
                masterData_CategoryHelper.WaitForWorkAround(3000);
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyMerchantCategoryCreated");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Merchant Category Created");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Merchant Category Created", "Bug", "Medium", "Partner Agents", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Merchant Category Created");
                        TakeScreenshot("VerifyMerchantCategoryCreated");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyMerchantCategoryCreated.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyMerchantCategoryCreated");
                        string id = loginHelper.getIssueID("Verify Merchant Category Created");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyMerchantCategoryCreated.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Merchant Category Created"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Merchant Category Created");
                //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyMerchantCategoryCreated");
                executionLog.WriteInExcel("Verify Merchant Category Created", Status, JIRA, "Agents Portal");
            }
        }
    }
}