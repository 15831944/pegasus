using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class ContactResponsibilityIssue : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Url")]
        [TestCategory("Temp")]
        public void contactResponsibilityIssue()
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
            var contact_Helper = new Office_ContactsHelper(GetWebDriver());

            // Variable
            var FirstName = "Test" + GetRandomNumber();
            var LastName = "Tester" + GetRandomNumber();
            var Number = "12345678" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("ContactResponsibilityIssue", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("ContactResponsibilityIssue", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("ContactResponsibilityIssue", "Goto User contacts");
                VisitOffice("contacts");

                executionLog.Log("ContactResponsibilityIssue", "Click On Any  Contact");
                contact_Helper.ClickElement("Contact1");
                contact_Helper.WaitForWorkAround(2000);

                executionLog.Log("ContactResponsibilityIssue", "Click On responsibility.");
                contact_Helper.DobleClick("ResponsibilityV");
                contact_Helper.WaitForWorkAround(3000);

                executionLog.Log("ContactResponsibilityIssue", "Select agent.");
                contact_Helper.selectByText("SelectResp", "Agent Sale");

                executionLog.Log("ContactResponsibilityIssue", "Click On Save Button");
                contact_Helper.ClickElement("Savetext");
                contact_Helper.WaitForWorkAround(3000);

                executionLog.Log("ContactResponsibilityIssue", "Click on edit button..");
                contact_Helper.ClickElement("EditLink");
                contact_Helper.WaitForWorkAround(3000);

                executionLog.Log("ContactResponsibilityIssue", "Click on save button.");
                contact_Helper.ClickElement("SaveContactN");
                contact_Helper.WaitForWorkAround(3000);

                executionLog.Log("ContactResponsibilityIssue", "Wait for locator to present.");
                contact_Helper.WaitForElementPresent("ResponsibilityV", 10);

                executionLog.Log("ContactResponsibilityIssue", "Verify responsibility not changed on edit..");
                contact_Helper.VerifyText("ResponsibilityV", "Agent Sale");
                contact_Helper.WaitForWorkAround(3000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("ContactResponsibilityIssue");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Contact Responsibility Issue");
                if (!result)
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Contact Responsibility Issue", "Bug", "Medium", "Contact page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Contact Responsibility Issue");
                        TakeScreenshot("ContactResponsibilityIssue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ContactResponsibilityIssue.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("ContactResponsibilityIssue");
                        string id = loginHelper.getIssueID("Contact Responsibility Issue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ContactResponsibilityIssue.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Contact Responsibility Issue"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Contact Responsibility Issue");
                executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("ContactResponsibilityIssue");
                executionLog.WriteInExcel("Contact Responsibility Issue", Status, JIRA, "Office contacts");
            }
        }
    }
}
