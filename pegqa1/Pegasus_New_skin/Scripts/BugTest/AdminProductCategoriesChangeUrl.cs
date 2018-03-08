using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class AdminProductCategoriesChangeUrl : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void adminProductCategoriesChangeUrl()
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
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("AdminProductCategoriesChangeUrl", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("AdminProductCategoriesChangeUrl", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("AdminProductCategoriesChangeUrl", "Redirect To Admin");
                VisitOffice("admin");
                products_CategoryHelper.WaitForWorkAround(1000);

                executionLog.Log("AdminProductCategoriesChangeUrl", "Goto product Categories");
                VisitOffice("products/categories");
                products_CategoryHelper.WaitForWorkAround(3000);

                executionLog.Log("AdminProductCategoriesChangeUrl", "Click On Edit icon category");
                products_CategoryHelper.ClickElement("Edit");
                products_CategoryHelper.WaitForWorkAround(3000);

                executionLog.Log("AdminProductCategoriesChangeUrl", "Change url of the page.");
                VisitOffice("products/category_edit/12");

                executionLog.Log("AdminProductCategoriesChangeUrl", "Wait for text");
                products_CategoryHelper.WaitForText("You don't have privilege.", 20);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("AdminProductCategoriesChangeUrl");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Admin Product Category Change Url");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Admin Product Category Change Url", "Bug", "Medium", "Office Admin", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Admin Product Category Change Url");
                        TakeScreenshot("AdminProductCategoriesChangeUrl");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AdminProductCategoriesChangeUrl.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("AdminProductCategoriesChangeUrl");
                        string id = loginHelper.getIssueID("Admin Product Category Change Url");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AdminProductCategoriesChangeUrl.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Admin Product Category Change Url"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Admin Product Category Change Url");
                //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("AdminProductCategoriesChangeUrl");
                executionLog.WriteInExcel("Admin Product Category Change Url", Status, JIRA, "Admin Products");
            }
        }
    }
}
