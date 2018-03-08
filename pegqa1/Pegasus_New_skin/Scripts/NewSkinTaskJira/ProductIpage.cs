using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class ProductIpage : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS5")]
        [TestCategory("NewSkinTaskJira")]
        public void productIpage()
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
            var products_CategoryHelper = new Products_CategoryHelper(GetWebDriver());

            // VARIABLE
            var product = "ProductName" + RandomNumber(10, 500);
            var prodcat = "ProdCat" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";


            try
            {
                executionLog.Log("ProductIpage", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("ProductIpage", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("ProductIpage", "Redirect To product categories");
                VisitOffice("products/categories");
                products_ProductHelper.WaitForWorkAround(3000);

                executionLog.Log("ProductIpage", "Click on Create button");
                products_CategoryHelper.ClickElement("Create");
                products_CategoryHelper.WaitForWorkAround(1000);

                executionLog.Log("ProductIpage", "Click on Create button");
                products_CategoryHelper.TypeText("Name", prodcat);

                executionLog.Log("ProductIpage", "Click on Save button");
                products_CategoryHelper.ClickElement("Save");
                products_CategoryHelper.WaitForWorkAround(3000);

                executionLog.Log("ProductIpage", "Redirect To product");
                VisitOffice("products/create");
                products_ProductHelper.WaitForWorkAround(3000);

                executionLog.Log("ProductIpage", "Enter Product Name");
                products_ProductHelper.TypeText("Name", product);

                executionLog.Log("ProductIpage", "Select Category Prod");
                products_ProductHelper.SelectByText("Category", prodcat);

                executionLog.Log("ProductIpage", "Click on Add Custom field");
                products_ProductHelper.ClickElement("AddCustomField");
                products_ProductHelper.WaitForWorkAround(1000);

                executionLog.Log("ProductIpage", "Enter field name");
                products_ProductHelper.TypeText("FieldName", "FieldSelect");

                executionLog.Log("ProductIpage", "SelectFieldType");
                products_ProductHelper.SelectByText("Type", "Text Box");

                executionLog.Log("ProductIpage", "SelectContentType");
                products_ProductHelper.SelectByText("ContentType", "Text");

                executionLog.Log("ProductIpage", "TypeDataLength");
                products_ProductHelper.TypeText("DataLength", "12");

                executionLog.Log("ProductIpage", "ClickSaveProdBtn");
                products_ProductHelper.ClickElement("Save1");
                products_ProductHelper.WaitForWorkAround(3000);

                executionLog.Log("ProductIpage", "Click Save Btn");
                products_ProductHelper.ClickElement("Save");

                executionLog.Log("ProductIpage", "Wait for success message.");
                products_ProductHelper.WaitForText("Product Created Successfully", 05);

                executionLog.Log("ProductIpage", "Redirect To Products");
                VisitOffice("products");
                products_ProductHelper.WaitForWorkAround(3000);

                executionLog.Log("ProductIpage", "Enter Name to search");
                products_ProductHelper.TypeText("SearchProduct", product);
                products_ProductHelper.WaitForWorkAround(2000);

                executionLog.Log("ProductIpage", "Click Delete btn  ");
                products_ProductHelper.ClickElement("DeleteProduct");

                executionLog.Log("ProductIpage", "Accept alert message. ");
                products_ProductHelper.AcceptAlert();

                executionLog.Log("ProductIpage", "Wait for delete message. ");
                products_ProductHelper.WaitForText("Product Deleted Successfully", 10);

                executionLog.Log("ProductIpage", "Redirect To product categories");
                VisitOffice("products/categories");
                products_ProductHelper.WaitForWorkAround(3000);

                executionLog.Log("ProductIpage", "Delete category");
                products_CategoryHelper.DeleteCat(prodcat);

                executionLog.Log("ProductIpage", "Wait for success message.");
                products_CategoryHelper.WaitForText("Category Deleted Successfully", 10);
                //products_CategoryHelper.WaitForWorkAround(3000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("ProductIpage");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Product I page");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Product I page", "Bug", "Medium", "Product page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Product I page");
                        TakeScreenshot("ProductIpage");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ProductIpage.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("ProductIpage");
                        string id = loginHelper.getIssueID("Product I page");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ProductIpage.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Product I page"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Product I page");
                //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("ProductIpage");
                executionLog.WriteInExcel("Product I page", Status, JIRA, "Product Management");
            }
        }
    }
}
