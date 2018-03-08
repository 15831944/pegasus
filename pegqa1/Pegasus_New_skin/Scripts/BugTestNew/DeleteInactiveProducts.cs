using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Admin
{
    [TestClass]
    public class DeleteInactiveProducts : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS2")]
        [TestCategory("BugTestNew")]
        public void deleteInactiveProducts()
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
            var products_ProductHelper = new Products_ProductHelper(GetWebDriver());

            // Variable
            var name = "Test" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("Delete Inactive Products", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("Delete Inactive Products", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("Delete Inactive Products", "Click On  Admin");
                VisitOffice("admin");

                executionLog.Log("Delete Inactive Products", "Redirect To Profucts");
                VisitOffice("products");

                executionLog.Log("Delete Inactive Products", "Verify title");
                VerifyTitle("Products");

                executionLog.Log("Delete Inactive Products", " Click On Create");
                products_ProductHelper.ClickElement("Create");

                executionLog.Log("Delete Inactive Products", "verify title");
                VerifyTitle("Products");

                executionLog.Log("Delete Inactive Products", "Enter Name");
                products_ProductHelper.TypeText("Name", name);

                executionLog.Log("Delete Inactive Products", "Select Category");
                products_ProductHelper.ClickElement("Category");

                executionLog.Log("Delete Inactive Products", " Click On Category");
                products_ProductHelper.ClickElement("AddCustomField");
                products_ProductHelper.WaitForWorkAround(3000);

                executionLog.Log("Delete Inactive Products", "Enter Filed name");
                products_ProductHelper.TypeText("FieldName", "Test");

                executionLog.Log("Delete Inactive Products", "Select Type");
                products_ProductHelper.Select("Type", "textbox");

                executionLog.Log("Delete Inactive Products", "Select Type");
                products_ProductHelper.Select("ContentType", "text");

                executionLog.Log("Delete Inactive Products", "Enter Filed name");
                products_ProductHelper.TypeText("DataLength", "10");

                executionLog.Log("Delete Inactive Products", "Select Data Validation");
                products_ProductHelper.Select("DataValidation", "email");

                executionLog.Log("Delete Inactive Products", "Click on Required CheckBox");
                products_ProductHelper.ClickElement("Required");
                products_ProductHelper.WaitForWorkAround(1000);

                executionLog.Log("Delete Inactive Products", "Enter Description");
                products_ProductHelper.TypeText("Description", "THIS IS TESTING DESCRIPTION");

                executionLog.Log("Delete Inactive Products", "  Click on Save button");
                products_ProductHelper.ClickElement("Save1");
                products_ProductHelper.WaitForWorkAround(2000);

                executionLog.Log("Delete Inactive Products", "Wait for save button to present.");
                products_ProductHelper.WaitForElementPresent("Save", 10);

                executionLog.Log("Delete Inactive Products", "Click on Save btn.");
                products_ProductHelper.ClickElement("Save");
                products_ProductHelper.WaitForWorkAround(1000);

                executionLog.Log("Delete Inactive Products", "Redirect To Profucts");
                VisitOffice("products");

                executionLog.Log("Delete Inactive Products", "Verify title");
                VerifyTitle("Products");

                executionLog.Log("Delete Inactive Products", "Enter Name to search");
                products_ProductHelper.TypeText("SearchProduct", name);
                products_ProductHelper.WaitForWorkAround(2000);

                executionLog.Log("Delete Inactive Products", "Click on edit icon");
                products_ProductHelper.ClickElement("Edit");

                executionLog.Log("Delete Inactive Products", "Select status as inactive.");
                products_ProductHelper.Select("Status", "0");

                executionLog.Log("Delete Inactive Products", "Click on save button.");
                products_ProductHelper.ClickElement("Save");

                executionLog.Log("Delete Inactive Products", "Enter Name to search");
                products_ProductHelper.TypeText("SearchProduct", name);
                products_ProductHelper.WaitForWorkAround(2000);

                executionLog.Log("Delete Inactive Products", "Click Delete btn  ");
                products_ProductHelper.ClickElement("DeleteProduct");

                executionLog.Log("Delete Inactive Products", "Accept alert message. ");
                products_ProductHelper.AcceptAlert();

                executionLog.Log("Delete Inactive Products", "Wait for delete message. ");
                products_ProductHelper.WaitForText("Product Deleted Successfully", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("Delete Inactive Products");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Delete Inactive Products");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Delete Inactive Products", "Bug", "Medium", "Products page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Delete Inactive Products");
                        TakeScreenshot("Delete Inactive Products");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\Delete Inactive Products.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("Delete Inactive Products");
                        string id = loginHelper.getIssueID("Delete Inactive Products");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\Delete Inactive Products.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Delete Inactive Products"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Delete Inactive Products");
               // executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("Delete Inactive Products");
                executionLog.WriteInExcel("Delete Inactive Products", Status, JIRA, "Admin Products");
            }
        }
    }
}
