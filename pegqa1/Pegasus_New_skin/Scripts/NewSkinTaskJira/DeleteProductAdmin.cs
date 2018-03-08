using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class DeleteProductAdmin : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS4")]
        [TestCategory("NewSkinTaskJira")]
        public void deleteProductAdmin()
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

            // VARIABLE
            var name = "Delete " + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {

            executionLog.Log("DeleteProductAdmin", "Login with valid username and password");
            Login(username[0], password[0]);
            Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

            executionLog.Log("DeleteProductAdmin", "Verify Page title");
            VerifyTitle("Dashboard");
            Console.WriteLine("Redirected at Dashboard screen.");

            executionLog.Log("DeleteProductAdmin", "Redirect To Product page");
            VisitOffice("products");
            products_ProductHelper.WaitForWorkAround(3000);

            executionLog.Log("DeleteProductAdmin", "ClickOnCreate");
            products_ProductHelper.ClickElement("Create");
            products_ProductHelper.WaitForWorkAround(3000);

            executionLog.Log("DeleteProductAdmin", "Enter ProductName");
            products_ProductHelper.TypeText("Name", name);

            executionLog.Log("DeleteProductAdmin", "ClickSaveProdBtn");
            products_ProductHelper.ClickElement("Save");

            executionLog.Log("DeleteProductAdmin", "Wait for success message.");
            products_ProductHelper.WaitForText("Product Created Successfully", 10);

            executionLog.Log("DeleteProductAdmin", "Redirect to product page");
            VisitOffice("products");
            products_ProductHelper.WaitForWorkAround(3000);

            executionLog.Log("DeleteProductAdmin", "Search Product");
            products_ProductHelper.TypeText("SearchProduct", name);
            products_ProductHelper.WaitForWorkAround(2000);

            executionLog.Log("DeleteProductAdmin", "Click on delete product");
            products_ProductHelper.ClickElement("DeleteProduct");

            executionLog.Log("DeleteProductAdmin", "Accept Alert messsage.");
            products_ProductHelper.AcceptAlert();

            executionLog.Log("DeleteProductAdmin", "Wait for success message.");
            products_ProductHelper.WaitForText("Product Deleted Successfully", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("DeleteProductAdmin");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Delete Product Admin");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Delete Product Admin", "Bug", "Medium", "Product page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Delete Product Admin");
                        TakeScreenshot("DeleteProductAdmin");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\DeleteProductAdmin.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("DeleteProductAdmin");
                        string id = loginHelper.getIssueID("Delete Product Admin");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\DeleteProductAdmin.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Delete Product Admin"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Delete Product Admin");
                //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("DeleteProductAdmin");
                executionLog.WriteInExcel("Delete Product Admin", Status, JIRA, "Product Management");
            }
        }
    }
} 