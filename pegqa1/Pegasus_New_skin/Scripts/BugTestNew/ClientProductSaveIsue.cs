using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class ClientProductSaveIsue : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        public void clientProductSaveIsue()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var office_ClientsHelper = new Office_ClientsHelper(GetWebDriver());

            // Variable
            var data = "Product" + RandomNumber(444, 999);
            String JIRA = "";
            String Status = "Pass";

            //        try
            //      {
            executionLog.Log("ClientProductSaveIsue", "Login with valid username and password");
            Login(username[0], password[0]);
            Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

            executionLog.Log("ClientProductSaveIsue", "Verify Page title");
            VerifyTitle("Dashboard");
            Console.WriteLine("Redirected at Dashboard screen.");

            executionLog.Log("ClientProductSaveIsue", "Redirect at clients page.");
            VisitOffice("clients");

            executionLog.Log("ClientProductSaveIsue", "Verify page title as clients.");
            VerifyTitle("Clients");

            executionLog.Log("ClientProductSaveIsue", "Click on any client.");
            office_ClientsHelper.ClickElement("Client1");

            executionLog.Log("ClientProductSaveIsue", "Click on products tab.");
            office_ClientsHelper.ClickElement("Products");

            executionLog.Log("ClientProductSaveIsue", "Select first product to edit.");
            office_ClientsHelper.ClickElement("Product1Chk");

            executionLog.Log("ClientProductSaveIsue", "Wait for locator to present.");
            office_ClientsHelper.WaitForElementPresent("ProductText", 10);

            executionLog.Log("ClientProductSaveIsue", "Enter data in first product.");
            office_ClientsHelper.TypeText("ProductText", data);

            executionLog.Log("ClientProductSaveIsue", "Select second product to edit.");
            office_ClientsHelper.ClickElement("Product2Chk");

            executionLog.Log("ClientProductSaveIsue", "Enter a valid email in second product.");
            office_ClientsHelper.TypeText("ProductText2", "test@gmail.com");

            executionLog.Log("ClientProductSaveIsue", "Click on save button.");
            office_ClientsHelper.ClickElement("SaveButtonByTitle");

            executionLog.Log("ClientProductSaveIsue", "Wait for success text.");
            office_ClientsHelper.WaitForText("Client data updated successfully. ", 10);

            executionLog.Log("ClientProductSaveIsue", "verify saved data not removed.");
            office_ClientsHelper.ProductData(data);

        }
    } }
       /*     catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("ClientProductSaveIsue");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Client Product Save Issue");
                if (!result)
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Client Product Save Issue", "Bug", "Medium", "Client page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Client Product Save Issue");
                        TakeScreenshot("ClientProductSaveIsue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ClientProductSaveIsue.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("ClientProductSaveIsue");
                        string id = loginHelper.getIssueID("Client Product Save Issue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ClientProductSaveIsue.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Client Product Save Issue"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Client Product Save Issue");
                executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("ClientProductSaveIsue");
                executionLog.WriteInExcel("Client Product Save Issue", Status, JIRA, "Client Management");
            }
        }
    }
}
*/