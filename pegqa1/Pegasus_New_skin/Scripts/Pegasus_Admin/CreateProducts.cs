using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Admin
{
    [TestClass]
    public class CreateProducts : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin")]
        public void createProducts()
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
                executionLog.Log("CreateProducts", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("CreateProducts", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("CreateProducts", "Redirect To Profucts");
                VisitOffice("products");
                products_ProductHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateProducts", "Enter Name");
                products_ProductHelper.TypeText("SearchProduct", "Apple");
                products_ProductHelper.WaitForWorkAround(2000);

                var loc = "//table[@id='list1']//tr[2]//td[5]/a";
                if (products_ProductHelper.IsElementPresent(loc))
                {
                    executionLog.Log("CreateProducts", "Redirect To Profucts");
                    VisitOffice("products/create");
                    products_ProductHelper.WaitForWorkAround(4000);

                    executionLog.Log("CreateProducts", "Enter the name of the product");
                    products_ProductHelper.TypeText("Name", name);

                    executionLog.Log("CreateProducts", "Select the category of the product");
                    products_ProductHelper.SelectByText("Category", "Broken");

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
                    products_ProductHelper.WaitForWorkAround(1000);

                    executionLog.Log("CreateProducts", "Enter Description");
                    products_ProductHelper.TypeText("Description", "THIS IS TESTING DESCRIPTION");

                    executionLog.Log("CreateProducts", "  Click on Save button");
                    products_ProductHelper.ClickElement("Save1");
                    products_ProductHelper.WaitForElementPresent("Save", 5);

                    executionLog.Log("CreateProducts", "Click on Save btn.");
                    products_ProductHelper.ClickJS("Save");
                    products_ProductHelper.WaitForWorkAround(3000);

                    executionLog.Log("CreateProducts", "Redirect To Profucts");
                    VisitOffice("products");
                    products_ProductHelper.WaitForWorkAround(3000);

                    executionLog.Log("CreateProducts", "Enter Name to search");
                    products_ProductHelper.TypeText("SearchProduct", name);
                    products_ProductHelper.WaitForWorkAround(3000);

                    executionLog.Log("CreateProducts", "Click Delete btn  ");
                    products_ProductHelper.ClickElement("DeleteProduct");

                    executionLog.Log("CreateProducts", "Accept alert message. ");
                    products_ProductHelper.AcceptAlert();

                    executionLog.Log("CreateProducts", "Wait for delete message. ");
                    products_ProductHelper.WaitForText("Product Deleted Successfully", 10);

                }
                else
                {
                    executionLog.Log("CreateProducts", "Redirect To Profucts");
                    VisitOffice("products/create");
                    products_ProductHelper.WaitForWorkAround(4000);

                    executionLog.Log("CreateProducts", "Enter the product Name");
                    products_ProductHelper.TypeText("Name", "Apple");

                    executionLog.Log("CreateProducts", "Select the Category");
                    products_ProductHelper.SelectByText("Category", "Broken");

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
                    products_ProductHelper.WaitForWorkAround(1000);

                    executionLog.Log("CreateProducts", "Enter Description");
                    products_ProductHelper.TypeText("Description", "THIS IS TESTING DESCRIPTION");

                    executionLog.Log("CreateProducts", "  Click on Save button");
                    products_ProductHelper.ClickElement("Save1");
                    products_ProductHelper.WaitForElementPresent("Save", 5);

                    executionLog.Log("CreateProducts", "Save the product");
                    products_ProductHelper.ClickJS("Save");
                    products_ProductHelper.WaitForWorkAround(6000);

                    products_ProductHelper.VerifyPageText("Product Created Successfully");
                    products_ProductHelper.WaitForWorkAround(2000);

                }

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CreateProducts");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Create Products");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Create Products", "Bug", "Medium", "Products page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Create Products");
                        TakeScreenshot("CreateProducts");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateProducts.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CreateProducts");
                        string id = loginHelper.getIssueID("Create Products");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateProducts.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Create Products"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Create Products");
                //    executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("CreateProducts");
                executionLog.WriteInExcel("Create Products", Status, JIRA, "Product Management");
            }
        }
    }
}
