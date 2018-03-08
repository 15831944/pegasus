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
    public class VerifyContactCategorySalesMgrLabel : DriverTestCase
    {
        [TestMethod]
        public void verifyContactCategorySalesMgrLabel()
        {
            string[] username = null;
            string[] password = null;

            XMLParse oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var office_ContactsHelper = new Office_ContactsHelper(GetWebDriver());

            // Random variables
            var File = GetPathToFile() + "leadsamples.csv";
            String JIRA = "";
            String Status = "Pass";


            try
            {
                executionLog.Log("VerifyContactCategorySalesMgrLabel", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyContactCategorySalesMgrLabel", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyContactCategorySalesMgrLabel", "Redirect at contacts page.");
                VisitOffice("contacts");

                executionLog.Log("VerifyContactCategorySalesMgrLabel", "Verify page title as contacts.");
                VerifyTitle("Contacts");

                executionLog.Log("VerifyContactCategorySalesMgrLabel", "Click on any contacts.");
                office_ContactsHelper.ClickElement("Contact1");

                executionLog.Log("VerifyContactCategorySalesMgrLabel", "Verify label for contact category.");
                office_ContactsHelper.VerifyText("CategoryLabel", "Select Category");

                executionLog.Log("VerifyContactCategorySalesMgrLabel", "Verify label for contact sales manager.");
                office_ContactsHelper.VerifyText("SalesManagerLabel", "Select Sales Manager");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyContactCategorySalesMgrLabel");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Contact Category SalesMgr Label");
                if (!result)
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Contact Category SalesMgr Label", "Bug", "Medium", "Contacts page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Contact Category SalesMgr Label");
                        TakeScreenshot("VerifyContactCategorySalesMgrLabel");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyContactCategorySalesMgrLabel.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyContactCategorySalesMgrLabel");
                        string id = loginHelper.getIssueID("Verify Contact Category SalesMgr Label");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyContactCategorySalesMgrLabel.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Contact Category SalesMgr Label"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Contact Category SalesMgr Label");
                executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyContactCategorySalesMgrLabel");
                executionLog.WriteInExcel("Verify Contact Category SalesMgr Label", Status, JIRA, "Contacts Management");
            }
        }
    }
}