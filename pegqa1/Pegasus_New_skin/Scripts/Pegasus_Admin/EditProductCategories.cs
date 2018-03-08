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
    public class EditProductCategories : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        [TestCategory("Fail")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin")]
        public void editProductCategories()
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


            // Variable 
            String name = "Test" + RandomNumber(100, 999);
            var EditName = "EditTest" + RandomNumber(100, 999);
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("EditProductCategories", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("EditProductCategories", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("EditProductCategories", "Click On  Admin");
                VisitOffice("admin");

                executionLog.Log("EditProductCategories", "Redirect To Product categories page");
                VisitOffice("products/categories");
                products_CategoryHelper.WaitForWorkAround(4000);

                executionLog.Log("EditProductCategories", "Verify title");
                VerifyTitle("Product Categories");

                executionLog.Log("EditProductCategories", " Click On Create");
                products_CategoryHelper.ClickElement("Create");
                products_CategoryHelper.WaitForWorkAround(3000);

                executionLog.Log("EditProductCategories", "Enter Name");
                products_CategoryHelper.TypeText("Name", name);

                executionLog.Log("EditProductCategories", " Click on Save button   ");
                products_CategoryHelper.ClickElement("Save");
                products_CategoryHelper.WaitForWorkAround(5000);

                executionLog.Log("EditProductCategories", " Click On Edit");
                products_CategoryHelper.ClickJs("Edit");
                products_CategoryHelper.WaitForWorkAround(3000);

                executionLog.Log("EditProductCategories", "Enter Product name");
                products_CategoryHelper.TypeText("Name", EditName);

                executionLog.Log("EditProductCategories", " Click On save button");
                products_CategoryHelper.ClickElement("SaveEdit");
                products_CategoryHelper.WaitForWorkAround(6000);

                executionLog.Log("EditProductCategories", "Verify text");
                products_CategoryHelper.VerifyPageText("Category Updated Successfully");

                executionLog.Log("EditProductCategories", "Delete the same product");
                products_CategoryHelper.ClickJs("DeleteIcon");
                products_CategoryHelper.WaitForWorkAround(1000);
                products_CategoryHelper.AcceptAlert();
                products_CategoryHelper.WaitForWorkAround(6000);

                executionLog.Log("EditProductCategories", "Verify the confirmation message");
                products_CategoryHelper.VerifyPageText("Category Deleted Successfully");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("EditProductCategories");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Edit Product Categories");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Edit Product Categories", "Bug", "Medium", "Product categories page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Edit Product Categories");
                        TakeScreenshot("EditProductCategories");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditProductCategories.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("EditProductCategories");
                        string id = loginHelper.getIssueID("Edit Product Categories");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditProductCategories.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Edit Product Categories"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Edit Product Categories");
             //   executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("EditProductCategories");
                executionLog.WriteInExcel("Edit Product Categories", Status, JIRA, "Product Management");
            }
        }
    }
}
