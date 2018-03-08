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
    public class AdminProductChangeUrl : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void adminProductChangeUrl()
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
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("AdminProductChangeUrl", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("AdminProductChangeUrl", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("AdminProductChangeUrl", "Redirect To Admin");
                VisitOffice("admin");
                products_ProductHelper.WaitForWorkAround(1000);

                executionLog.Log("AdminProductChangeUrl", "Goto Product >> Product");
                VisitOffice("products");

                executionLog.Log("AdminProductChangeUrl", "Click On Any Product");
                products_ProductHelper.ClickElement("ClickProduct1");
                products_ProductHelper.WaitForWorkAround(3000);

                executionLog.Log("AdminProductChangeUrl", "Change url of the page.");
                VisitOffice("products/edit/121");

                executionLog.Log("AdminProductChangeUrl", "Verify You don't have privilege.");
                products_ProductHelper.WaitForText("You don't have privilege.", 20);
                Console.WriteLine("Passed");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("AdminProductChangeUrl");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Admin Product Change Url");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Admin Product Change Url", "Bug", "Medium", "Office Admin", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Admin Product Change Url");
                        TakeScreenshot("AdminProductChangeUrl");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AdminProductChangeUrl.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("AdminProductChangeUrl");
                        string id = loginHelper.getIssueID("Admin Product Change Url");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AdminProductChangeUrl.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Admin Product Change Url"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Admin Product Change Url");
                // executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("AdminProductChangeUrl");
                executionLog.WriteInExcel("Admin Product Change Url", Status, JIRA, "Admin Products");
            }
        }
    }
}
