using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class AdminProductProductURLChange : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Url")]
        [TestCategory("TS3")]
        [TestCategory("ChangeUrl")]
        public void adminProductProductURLChange()
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
            var FirstName = "Test" + GetRandomNumber();
            var LastName = "Tester" + GetRandomNumber();
            var Number = "12345678" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("AdminProductProductURLChange", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("AdminProductProductURLChange", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("AdminProductProductURLChange", "Goto User Admin Product >> Product  ");
                VisitOffice("products");

                executionLog.Log("AdminProductProductURLChange", "Click On any Product >> Product");
                products_ProductHelper.ClickElement("ClickProduct1");
                products_ProductHelper.WaitForWorkAround(1000);

                executionLog.Log("AdminProductProductURLChange", "Change the url with the url number of another office");
                VisitOffice("products/edit/21");
                products_ProductHelper.WaitForWorkAround(2000);

                executionLog.Log("AdminProductProductURLChange", "Verify Validation");
                products_ProductHelper.WaitForText("You don't have privilege.", 10);

            }
            catch (Exception e)
            {
                Console.WriteLine("ERRROROOR");
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("AdminProductProductURLChange");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Admin Product Product URL Change");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Admin Product Product URL Change", "Bug", "Medium", "Products Page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Admin Product Product URL Change");
                        TakeScreenshot("AdminProductProductURLChange");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AdminProductProductURLChange.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("AdminProductProductURLChange");
                        string id = loginHelper.getIssueID("Admin Product Product URL Change");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AdminProductProductURLChange.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Admin Product Product URL Change"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Admin Product Product URL Change");
                //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("AdminProductProductURLChange");
                executionLog.WriteInExcel("Admin Product Product URL Change", Status, JIRA, "Office Products");
            }
        }
    }
}
