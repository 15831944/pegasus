using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class ProductCategoryWithAlreadyExistingName : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("Fail")]
        [TestCategory("TS3")]
        [TestCategory("BugTestNew")]
        public void productCategoryWithAlreadyExistingName()
        {
            string[] username = null;
            string[] password = null;

            XMLParse oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var products_CategoryHelper = new Products_CategoryHelper(GetWebDriver());
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("ProductCategoryWithAlreadyExistingName", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("ProductCategoryWithAlreadyExistingName", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("ProductCategoryWithAlreadyExistingName", "Redirect at product categories page.");
                VisitOffice("products/categories");

                var loc = "//span[text()='Do not delete']";
                if (products_CategoryHelper.IsElementPresent(loc))
                {
                    products_CategoryHelper.WaitForWorkAround(3000);
                    executionLog.Log("ProductCategoryWithAlreadyExistingName", "Clcik on edit icon.");
                    products_CategoryHelper.ClickElement("Create");
                    products_CategoryHelper.WaitForWorkAround(5000);

                    executionLog.Log("ProductCategoryWithAlreadyExistingName", "Enter category name.");
                    products_CategoryHelper.TypeText("Name", "Do not delete");

                    executionLog.Log("ProductCategoryWithAlreadyExistingName", "Click on save button.");
                    products_CategoryHelper.ClickElement("Save");

                    executionLog.Log("ProductCategoryWithAlreadyExistingName", "Verify Select for category");
                    products_CategoryHelper.WaitForText("Category Already Existed", 10);

                }

                else
                {
                    products_CategoryHelper.WaitForWorkAround(3000);
                    executionLog.Log("ProductCategoryWithAlreadyExistingName", "Clcik on edit icon.");
                    products_CategoryHelper.ClickElement("Create");
                    products_CategoryHelper.WaitForWorkAround(5000);

                    executionLog.Log("ProductCategoryWithAlreadyExistingName", "Enter category name.");
                    products_CategoryHelper.TypeText("Name", "Do not delete");

                    executionLog.Log("ProductCategoryWithAlreadyExistingName", "Click on save button.");
                    products_CategoryHelper.ClickElement("Save");

                    executionLog.Log("ProductCategoryWithAlreadyExistingName", "Verify Select for category");
                    products_CategoryHelper.WaitForText("Category Updated Successfully", 10);

                    executionLog.Log("ProductCategoryWithAlreadyExistingName", "Click on edit icon.");
                    products_CategoryHelper.ClickElement("Create");
                    products_CategoryHelper.WaitForWorkAround(2000);

                    executionLog.Log("ProductCategoryWithAlreadyExistingName", "Enter category name.");
                    products_CategoryHelper.TypeText("Name", "Do not delete");

                    executionLog.Log("ProductCategoryWithAlreadyExistingName", "Click on save button.");
                    products_CategoryHelper.ClickElement("Save");

                    executionLog.Log("ProductCategoryWithAlreadyExistingName", "Verify Select for category");
                    products_CategoryHelper.WaitForText("Category Already Existed", 10);
                }
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("ProductCategoryWithAlreadyExistingName");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Product Category With Already Existing Name");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Product Category With Already Existing Name", "Bug", "Medium", "Products page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Product Category With Already Existing Name");
                        TakeScreenshot("ProductCategoryWithAlreadyExistingName");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ProductCategoryWithAlreadyExistingName.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("ProductCategoryWithAlreadyExistingName");
                        string id = loginHelper.getIssueID("Product Category With Already Existing Name");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ProductCategoryWithAlreadyExistingName.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Product Category With Already Existing Name"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Product Category With Already Existing Name");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("ProductCategoryWithAlreadyExistingName");
                executionLog.WriteInExcel("Product Category With Already Existing Name", Status, JIRA, "Admin Products");
            }
        }
    }
}