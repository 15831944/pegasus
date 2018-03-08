using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Admin
{
    [TestClass]
    public class CreateProductCategory : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin")]
        public void createProductCategory()
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

            // Variable 
            var name = "Test" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
            executionLog.Log("CreateProductCategory", "Login with valid username and password");
            Login(username[0], password[0]);
            Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

            executionLog.Log("CreateProductCategory", "Verify Page title");
            VerifyTitle("Dashboard");

            executionLog.Log("CreateProductCategory", "Click On  Admin");
            VisitOffice("admin");

            executionLog.Log("CreateProductCategory", "Redirect To Product");
            VisitOffice("products/categories");
            products_CategoryHelper.WaitForWorkAround(4000);
            
            executionLog.Log("CreateProductCategory", "Verify title");
            VerifyTitle("Product Categories");
                        
            executionLog.Log("CreateProductCategory", " Click On Create");
            products_CategoryHelper.ClickElement("Create");
            products_CategoryHelper.WaitForWorkAround(1000);

            executionLog.Log("CreateProductCategory", "Enter Name");
            products_CategoryHelper.TypeText("Name", name);

            executionLog.Log("CreateProductCategory", " Click on Save button   ");
            products_CategoryHelper.ClickElement("Save");

            executionLog.Log("CreateProductCategory", "wait for text");
            products_CategoryHelper.WaitForText("Category Created Successfully", 30);

            executionLog.Log("CreateProductCategory", "Search and click ");
            products_CategoryHelper.SearchAnclick(name);

        }
       catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CreateProductCategory");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Create Product Category");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Create Product Category", "Bug", "Medium", "Product page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Create Product Category");
                        TakeScreenshot("CreateProductCategory");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateProductCategory.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CreateProductCategory");
                        string id = loginHelper.getIssueID("Create Product Category");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateProductCategory.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Create Product Category"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Create Product Category");
             //   executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("CreateProductCategory");
                executionLog.WriteInExcel("Create Product Category", Status, JIRA, "Product Management");
            }
        }
    }
} 
