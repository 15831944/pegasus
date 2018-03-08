using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class ProductManagement : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin")]
        [TestCategory("TS8")]
        [TestCategory("Pegasus_NewSkin")]
        public void productManagement()
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
            var products_CategoryHelper = new Products_CategoryHelper(GetWebDriver());
            var products_ProductHelper = new Products_ProductHelper(GetWebDriver());


            // Variable
            var name = "TestAgent" + GetRandomNumber();


            try
            {
                executionLog.Log("ProductManagement", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("ProductManagement", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("ProductManagement", "Redirect at Admin");
                VisitOffice("admin");

                executionLog.Log("ProductManagement", "Redirect To Product");
                VisitOffice("products/categories");

                executionLog.Log("ProductManagement", "Verify title");
                VerifyTitle("Product Categories");

                executionLog.Log("ProductManagement", " Click On Create");
                products_CategoryHelper.ClickElement("Create");
                products_CategoryHelper.WaitForWorkAround(3000);

                executionLog.Log("ProductManagement", "Enter Name");
                products_CategoryHelper.TypeText("Name", name);

                executionLog.Log("ProductManagement", "Click on Save button");
                products_CategoryHelper.ClickElement("Save");

                executionLog.Log("ProductManagement", "wait for text");
                products_CategoryHelper.WaitForText("Category Created Successfully", 10);

                executionLog.Log("ProductManagement", "Search and click ");
                products_CategoryHelper.SearchAnclick(name);

                executionLog.Log("ProductManagement", "Redirect at Admin");
                VisitOffice("admin");

                executionLog.Log("ProductManagement", "Redirect To Product page");
                VisitOffice("products");

                executionLog.Log("ProductManagement", "Verify title");
                VerifyTitle("Products");

                executionLog.Log("ProductManagement", " Click On Create");
                products_ProductHelper.ClickElement("Create");
                products_ProductHelper.WaitForWorkAround(2000);

                executionLog.Log("ProductManagement", "Verify title");
                VerifyTitle("Products");

                executionLog.Log("ProductManagement", "Enter Name");
                products_ProductHelper.TypeText("Name", name);

                executionLog.Log("ProductManagement", "  Click on Save button");
                products_ProductHelper.ClickElement("Save");
                products_ProductHelper.WaitForWorkAround(2000);

                executionLog.Log("ProductManagement", "Wait for text");
                products_ProductHelper.WaitForText("Product Created Successfully", 10);

                executionLog.Log("ProductManagement", "Verify title");
                VerifyTitle("Products");

                executionLog.Log("ProductManagement", "Enter name");
                products_ProductHelper.TypeText("SearchProduct", name);
                products_ProductHelper.WaitForWorkAround(4000);

                executionLog.Log("ProductManagement", "Click on Edit");
                products_ProductHelper.ClickElement("Edit");
                products_ProductHelper.WaitForWorkAround(2000);

                executionLog.Log("ProductManagement", "Verify title");
                VerifyTitle("Products");

                executionLog.Log("ProductManagement", "Enter Name");
                products_ProductHelper.TypeText("Name", name + "1");

                executionLog.Log("ProductManagement", "Save Create");
                products_ProductHelper.ClickElement("SaveEdit");
                products_ProductHelper.WaitForWorkAround(3000);

                executionLog.Log("ProductManagement", "Redirect To Profucts");
                VisitOffice("products");

                executionLog.Log("ProductManagement", "Verify title");
                VerifyTitle("Products");
                products_ProductHelper.WaitForWorkAround(3000);

                executionLog.Log("ProductManagement", "Enter Name to search");
                products_ProductHelper.TypeText("SearchProduct", name + "1");
                products_ProductHelper.WaitForWorkAround(5000);

                products_ProductHelper.WaitForElementPresent("DeleteProduct", 10);
                executionLog.Log("ProductManagement", "Click Delete btn  ");
                products_ProductHelper.ClickElement("DeleteProduct");

                executionLog.Log("ProductManagement", "Accept alert message. ");
                products_ProductHelper.AcceptAlert();

                executionLog.Log("ProductManagement", "Wait for delete message. ");
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
                        loginHelper.CreateIssue("Product Management", "Bug", "Medium", "Products page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
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
              //  executionLog.DeleteFile("Error");
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