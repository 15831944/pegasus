using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class ProductUpdateDate : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("SELENIUM_TESTCASE")]
        [TestCategory("TS8")]
        public void productUpdateDate()
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

            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("ProductUpdateDate", "Login with valid credential  Username");
                Login(username[0], password[0]);

                executionLog.Log("ProductUpdateDate", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("ProductUpdateDate", "Goto Product tab.");
                VisitOffice("products");
                products_ProductHelper.WaitForWorkAround(3000);

                executionLog.Log("ProductUpdateDate", "Select first checkbox");
                products_ProductHelper.ClickElement("SelectCheckBoxForFirst");
                products_ProductHelper.WaitForWorkAround(3000);

                products_ProductHelper.ClickElement("BulkUpdateButton");
                products_ProductHelper.WaitForWorkAround(3000);

                executionLog.Log("ProductUpdateDate", "Click on Change Default View button.");
                products_ProductHelper.ClickElement("ChangeDefaultView");
                products_ProductHelper.WaitForWorkAround(3000);

                executionLog.Log("ProductUpdateDate", "Select Collapse Default View.");
                products_ProductHelper.SelectByText("SelectDefaultView", "Collapse");

                executionLog.Log("ProductUpdateDate", "Click on Update button.");
                products_ProductHelper.ClickDisplayed("//button[text()='Update']");

                executionLog.Log("ProductUpdateDate", "Accept Alert.");
                products_ProductHelper.AcceptAlert();
                products_ProductHelper.WaitForWorkAround(3000);

                executionLog.Log("ProductUpdateDate", "Verify current date for updated product.");
                string date = System.DateTime.Today.ToString("dd/MM/yy");
                var loc22 = "//table[@id='list1']//tr[2]//td[contains(@title,'" + date + "')]";
                products_ProductHelper.IsElementPresent(loc22);
            }

            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("productUpdateDate");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("product Update Date");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("product Update Date", "Bug", "Medium", "Product page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("product Update Date");
                        TakeScreenshot("productUpdateDate");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\productUpdateDate.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("productUpdateDate");
                        string id = loginHelper.getIssueID("product Update Date");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\productUpdateDate.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("product Update Date"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("product Update Date");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("productUpdateDate");
                executionLog.WriteInExcel("product Update Date", Status, JIRA, "Corp Master Data");
            }
        }
    }
}

