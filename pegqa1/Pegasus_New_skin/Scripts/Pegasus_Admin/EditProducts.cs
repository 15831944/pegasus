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
    public class EditProducts : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin")]
        public void editProducts()
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
            var products_ProductHelper = new Products_ProductHelper(GetWebDriver());

            // Variable
            var name = "Test" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("EditProducts", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("EditProducts", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("EditProducts", "Click On  Admin");
                VisitOffice("admin");
                
                executionLog.Log("EditProducts", "Redirect To Product page");
                VisitOffice("products");
               
                executionLog.Log("EditProducts", "Verify title");
                VerifyTitle("Products");

                executionLog.Log("EditProducts", " Click On Create");
                products_ProductHelper.ClickElement("Create");
                products_ProductHelper.WaitForWorkAround(2000);

                executionLog.Log("EditProducts", "Verify title");
                VerifyTitle("Products");

                executionLog.Log("EditProducts", "Enter Name");
                products_ProductHelper.TypeText("Name", name);

                executionLog.Log("EditProducts", "  Click on Save button");
                products_ProductHelper.ClickElement("Save");
                products_ProductHelper.WaitForWorkAround(2000);

                executionLog.Log("EditProducts", "Wait for text");
                products_ProductHelper.WaitForText("Product Created Successfully", 10);

                executionLog.Log("EditProducts", "Verify title");
                VerifyTitle("Products");

                executionLog.Log("EditProducts", "Enter name");
                products_ProductHelper.TypeText("SearchProduct", name);

                executionLog.Log("EditProducts", "Click on Edit");
                products_ProductHelper.ClickElement("Edit");
               
                executionLog.Log("EditProducts", "Verify title");
                VerifyTitle("Products");
               
                executionLog.Log("EditProducts", "Enter Name");
                products_ProductHelper.TypeText("Name", name + "1");

                executionLog.Log("EditProducts", "Save Create");
                products_ProductHelper.ClickElement("SaveEdit");
                products_ProductHelper.WaitForWorkAround(1000);

                executionLog.Log("EditProducts", "Redirect To Products");
                VisitOffice("products");

                executionLog.Log("EditProducts", "Verify title");
                VerifyTitle("Products");

                executionLog.Log("EditProducts", "Enter Name to search");
                products_ProductHelper.TypeText("SearchProduct", name + "1");
                products_ProductHelper.WaitForWorkAround(2000);

                executionLog.Log("EditProducts", "Click Delete btn  ");
                products_ProductHelper.ClickElement("DeleteProduct");

                executionLog.Log("EditProducts", "Accept alert message. ");
                products_ProductHelper.AcceptAlert();

                executionLog.Log("EditProducts", "Wait for delete message. ");
                products_ProductHelper.WaitForText("Product Deleted Successfully", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("EditProducts");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Edit Products");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Edit Products", "Bug", "Medium", "Product page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Edit Products");
                        TakeScreenshot("EditProducts");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditProducts.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("EditProducts");
                        string id = loginHelper.getIssueID("Edit Products");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditProducts.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Edit Products"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Edit Products");
             //   executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("EditProducts");
                executionLog.WriteInExcel("Edit Products", Status, JIRA, "Product Management");
            }
        }
    }
}