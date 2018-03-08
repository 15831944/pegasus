using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class ProductMaxLength : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS5")]
        [TestCategory("NewSkinTaskJira")]
        public void productMaxLength()
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

            var Name = "QaProduct" + RandomNumber(1, 100);
            var prodcat = "ProdCat" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("ProductMaxLength", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("ProductMaxLength", "Verify Page title");
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

                executionLog.Log("CreateProducts", "Redirect To Profucts");
                VisitOffice("products/create");
                products_ProductHelper.WaitForWorkAround(4000);

                executionLog.Log("CreateProducts", "Enter the name of the product");
                products_ProductHelper.TypeText("Name", Name);

                executionLog.Log("CreateProducts", "Select the category of the product");
                products_ProductHelper.SelectByText("Category", prodcat);

                executionLog.Log("CreateProducts", " Click On Category");
                products_ProductHelper.ClickElement("AddCustomField");
                products_ProductHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateProducts", "Enter Filed name");
                products_ProductHelper.TypeText("FieldName", "Test");

                executionLog.Log("CreateProducts", "Select Type");
                products_ProductHelper.Select("Type", "textbox");

                executionLog.Log("CreateProducts", "Select Type");
                products_ProductHelper.Select("ContentType", "text");

                executionLog.Log("CreateProducts", "Enter Filed name");
                products_ProductHelper.TypeText("DataLength", "10");

                executionLog.Log("CreateProducts", "Select Data Validation");
                products_ProductHelper.Select("DataValidation", "email");

                executionLog.Log("CreateProducts", "Click on Required CheckBox");
                products_ProductHelper.ClickElement("Required");
                products_ProductHelper.WaitForWorkAround(2000);

                executionLog.Log("CreateProducts", "Enter Description");
                products_ProductHelper.TypeText("Description", "THIS IS TESTING DESCRIPTION");

                executionLog.Log("CreateProducts", "  Click on Save button");
                products_ProductHelper.ClickElement("Save1");
                products_ProductHelper.WaitForElementPresent("Save", 5);

                executionLog.Log("CreateProducts", "Click on Save btn.");
                products_ProductHelper.ClickJS("Save");
                products_ProductHelper.WaitForWorkAround(5000);

                executionLog.Log("CreateProducts", "Search the same product");
                products_ProductHelper.TypeText("SearchProduct", Name);
                products_ProductHelper.WaitForWorkAround(4000);

                executionLog.Log("ProductMaxLength", "Open first product");
                products_ProductHelper.ClickElement("ClickProduct1");
                products_ProductHelper.WaitForWorkAround(3000);

                executionLog.Log("ProductMaxLength", "Verify title");
                VerifyTitle("Products");

                executionLog.Log("ProductMaxLength", "Click on Add custom field");
                products_ProductHelper.ClickElement("AddCustomField");
                products_ProductHelper.WaitForWorkAround(3000);

                executionLog.Log("ProductMaxLength", "Select Text Box type");
                products_ProductHelper.SelectByText("CustomType", "Text Box");

                executionLog.Log("ProductMaxLength", "Enter value in the Data length field");
                products_ProductHelper.TypeText("EditDataLength", "30");

                executionLog.Log("ProductMaxLength", "Select Number as content type");
                products_ProductHelper.SelectByText("EditContenttypt", "Number");

                executionLog.Log("ProductMaxLength", "Verify Data length");
                Assert.IsTrue(products_ProductHelper.GetValue("//*[@id='ProductCustomFieldEditDataLength']").Contains("30"));

                executionLog.Log("ProductMaxLength", "Select text as content type");
                products_ProductHelper.SelectByText("EditContenttypt", "Text");

                executionLog.Log("ProductMaxLength", "Verify Data length");
                Assert.IsTrue(products_ProductHelper.GetValue("//*[@id='ProductCustomFieldEditDataLength']").Contains("30"));

                executionLog.Log("CreateProducts", "  Click on Save button");
                products_ProductHelper.ClickElement("Save1");
                products_ProductHelper.WaitForElementPresent("Save", 5);

                executionLog.Log("CreateProducts", "Click on Save btn.");
                products_ProductHelper.ClickJS("Save");
                products_ProductHelper.WaitForWorkAround(4000);

                executionLog.Log("CreateProducts", "Enter Name to search");
                products_ProductHelper.TypeText("SearchProduct", Name);
                products_ProductHelper.WaitForWorkAround(5000);

                executionLog.Log("CreateProducts", "Click Delete btn  ");
                products_ProductHelper.ClickElement("DeleteProduct");

                executionLog.Log("CreateProducts", "Accept alert message. ");
                products_ProductHelper.AcceptAlert();

                executionLog.Log("CreateProducts", "Wait for delete message. ");
                products_ProductHelper.WaitForText("Product Deleted Successfully", 10);

                executionLog.Log("ProductIpage", "Redirect To product categories");
                VisitOffice("products/categories");
                products_ProductHelper.WaitForWorkAround(3000);

                executionLog.Log("ProductIpage", "Delete category");
                products_CategoryHelper.DeleteCat(prodcat);

                executionLog.Log("ProductIpage", "Wait for success message.");
                products_CategoryHelper.WaitForText("Category Deleted Successfully", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("ProductMaxLength");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Product Max Length");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Product Max Length", "Bug", "Medium", "Product page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Product Max Length");
                        TakeScreenshot("ProductMaxLength");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ProductMaxLength.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("ProductMaxLength");
                        string id = loginHelper.getIssueID("Product Max Length");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ProductMaxLength.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Product Max Length"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Product Max Length");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("AdditionalFilter");
                executionLog.WriteInExcel("Product Max Length", Status, JIRA, "Product Management");
            }
        }
    }
}