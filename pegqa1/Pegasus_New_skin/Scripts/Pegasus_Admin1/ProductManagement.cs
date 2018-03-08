using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Admin
{
    [TestClass]
    public class ProductManagement : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin1")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Admin1")]
        public void productManagement()
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
            var products_CategoryHelper = new Products_CategoryHelper(GetWebDriver());
            var products_ProductHelper = new Products_ProductHelper(GetWebDriver());
            var office_ClientsHelper = new Office_ClientsHelper(GetWebDriver());

            // Variable 
            string name = "Test" + RandomNumber(1, 999);
            var ProductName = "Product" + RandomNumber(1, 999);
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("ProductManagement", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("ProductManagement", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("ProductManagement", "Click On  Admin");
                VisitOffice("admin");

                executionLog.Log("ProductManagement", "Redirect To Product categories page.");
                VisitOffice("products/categories");

                executionLog.Log("ProductManagement", "Verify title");
                VerifyTitle("Product Categories");

                executionLog.Log("ProductManagement", " Click On Create");
                products_CategoryHelper.ClickElement("Create");

                executionLog.Log("ProductManagement", " Wait for save button to present.");
                products_CategoryHelper.WaitForElementPresent("Save", 10);

                executionLog.Log("ProductManagement", " Click on Save button.");
                products_CategoryHelper.ClickElement("Save");

                executionLog.Log("ProductManagement", "Verify Validation for Name");
                products_CategoryHelper.VerifyText("NameError", "This field is required.");

                executionLog.Log("ProductManagement", "Enter Name");
                products_CategoryHelper.TypeText("Name", name);

                executionLog.Log("ProductManagement", "Select default view.");
                products_CategoryHelper.Select("DefaultView", "0");

                executionLog.Log("ProductManagement", " Click on Save button.");
                products_CategoryHelper.ClickElement("Save");

                executionLog.Log("ProductManagement", "Wait for success message.n");
                products_CategoryHelper.WaitForText("Category Created Successfully", 10);
                products_CategoryHelper.WaitForWorkAround(5000);

                executionLog.Log("ProductManagement", "Redirect at products page.");
                VisitOffice("products");

                executionLog.Log("ProductManagement", "Verify title");
                VerifyTitle("Products");
                products_ProductHelper.WaitForWorkAround(3000);

                executionLog.Log("ProductManagement", " Click On Create");
                products_ProductHelper.ClickElement("Create");

                executionLog.Log("ProductManagement", "verify page title");
                VerifyTitle("Products");

                executionLog.Log("ProductManagement", "Enter product Name");
                products_ProductHelper.TypeText("Name", ProductName);

                executionLog.Log("ProductManagement", "Select product Category");
                products_ProductHelper.SelectByText("Category", name);

                executionLog.Log("ProductManagement", "Click on Save btn.");
                products_ProductHelper.ClickElement("Save");

                executionLog.Log("ProductManagement", "Click On  Admin");
                products_CategoryHelper.WaitForText("Product Created Successfully", 10);

                executionLog.Log("ProductManagement", "Redirect To Product categorie page.");
                VisitOffice("products/categories");

                executionLog.Log("ProductManagement", "Verify delete button is not displayed for product category.");
                products_CategoryHelper.VerifyDeleteButton(name);
                products_CategoryHelper.WaitForWorkAround(3000);

                executionLog.Log("ProductManagement", "Redirect To Products");
                VisitOffice("products");

                executionLog.Log("ProductManagement", "Verify title");
                VerifyTitle("Products");

                executionLog.Log("ProductManagement", "Enter Name to search");
                products_ProductHelper.TypeText("SearchProduct", ProductName);
                products_ProductHelper.WaitForWorkAround(2000);

                executionLog.Log("ProductManagement", "Delete the created product.");
                products_ProductHelper.ClickElement("DeleteProduct");

                executionLog.Log("ProductManagement", "Accept alert message.");
                products_ProductHelper.AcceptAlert();

                executionLog.Log("ProductManagement", "Wait for delete success message.");
                products_ProductHelper.WaitForText("Product Deleted Successfully", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("ProductManagement");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Product Management");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Product Management", "Bug", "Medium", "Product page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Product Management");
                        TakeScreenshot("ProductManagement");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ProductManagement.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("ProductManagement");
                        string id = loginHelper.getIssueID("Product Management");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ProductManagement.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Product Management"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Product Management");
             //   executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("ProductManagement");
                executionLog.WriteInExcel("Product Management", Status, JIRA, "Product Management");
            }
        }
    }
}