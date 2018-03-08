using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class VerifyValidationForCategory : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin1")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Admin1")]
        public void verifyValidationForCategory()
        {
            string[] username = null;
            string[] password = null;

            XMLParse oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            //Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var masterData_CategoriesHelper = new MasterData_CategoriesHelper(GetWebDriver());

            // Variable 
            var name = "Category" + RandomNumber(1, 999);
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("VerifyValidationForCategory", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyValidationForCategory", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("VerifyValidationForCategory", "Click On  Admin");
                VisitOffice("admin");

                executionLog.Log("VerifyValidationForCategory", "Redirect To URL");
                VisitOffice("categories");

                executionLog.Log("VerifyValidationForCategory", "Verify page title as categories.");
                VerifyTitle("Categories");

                executionLog.Log("VerifyValidationForCategory", "Search category ny name.");
                masterData_CategoriesHelper.TypeText("SearchName", "Leads");
                masterData_CategoriesHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyValidationForCategory", "Click on searched category.");
                masterData_CategoriesHelper.ClickElement("Category1");

                executionLog.Log("VerifyValidationForCategory", "Click on create button.");
                masterData_CategoriesHelper.ClickElement("Create");

                executionLog.Log("VerifyValidationForCategory", "Enter category name.");
                masterData_CategoriesHelper.TypeText("Name", "test");

                executionLog.Log("VerifyValidationForCategory", "Click on save button.");
                masterData_CategoriesHelper.ClickElement("Save");

                executionLog.Log("VerifyValidationForCategory", "Wait for duplicate category text.");
                masterData_CategoriesHelper.WaitForText("test category already exists.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyValidationForCategory");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Validation For Category");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Validation For Category", "Bug", "Medium", "Create Categories", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Validation For Category");
                        TakeScreenshot("VerifyValidationForCategory");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyValidationForCategory.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyValidationForCategory");
                        string id = loginHelper.getIssueID("Verify Validation For Category");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyValidationForCategory.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Validation For Category"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Validation For Category");
           //     executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("VerifyValidationForCategory");
                executionLog.WriteInExcel("Verify Validation For Category", Status, JIRA, "Office Masterdate");
            }
        }
    }
}
