using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class ValidationforContactPrimaryEmail : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("Fail")]
        [TestCategory("TS3")]
        [TestCategory("BugTestNew")]


        public void validationforContactPrimaryEmail()
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
            var contact_Helper = new Office_ContactsHelper(GetWebDriver());

            // Variable
            var FirstName = "Test" + GetRandomNumber();
            var LastName = "Tester" + GetRandomNumber();
            var Number = "12345678" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
            executionLog.Log("ValidationforContactPrimaryEmail", "Login with valid username and password");
            Login(username[0], password[0]);
            Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

            executionLog.Log("ValidationforContactPrimaryEmail", "Verify Page title");
            VerifyTitle("Dashboard");
            Console.WriteLine("Redirected at Dashboard screen.");

            executionLog.Log("ValidationforContactPrimaryEmail", "Go to contacts page");
            VisitOffice("contacts");
            contact_Helper.WaitForWorkAround(3000);

            //contact_Helper.TypeText("SearchName", "aslam ");
            //contact_Helper.WaitForWorkAround(4000);

            executionLog.Log("ValidationforContactPrimaryEmail", "Click On Any  Contact");
            contact_Helper.ClickElement("CheckTheFirstContact");
            contact_Helper.WaitForWorkAround(2000);

            executionLog.Log("ValidationforContactPrimaryEmail", "Click On Edit icon");
            contact_Helper.ClickElement("EditContactNewSkin");
            contact_Helper.WaitForWorkAround(3000);

            executionLog.Log("ValidationforContactPrimaryEmail", "Click On Save Button");
            contact_Helper.ClickElement("SaveContactN");

            executionLog.Log("ValidationforContactPrimaryEmail", "Wait for success message");
            contact_Helper.WaitForText("Contact has been updated.", 10);

        }
  catch (Exception e)
    {
        executionLog.Log("Error", e.StackTrace);
        Status = "Fail";

        String counter = executionLog.readLastLine("counter");
        String Description = executionLog.GetAllTextFile("ValidationforContactPrimaryEmail");
        String Error = executionLog.GetAllTextFile("Error");
        if (counter == "")
        {
            counter = "0";
        }
        bool result = loginHelper.CheckExstingIssue("Validation for Contact Primary Email");
        if (!result)
        {
            if (Int16.Parse(counter) < 9)
            {
                executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                loginHelper.CreateIssue("Validation for Contact Primary Email", "Bug", "Medium", "Contact page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                string id = loginHelper.getIssueID("Validation for Contact Primary Email");
                TakeScreenshot("ValidationforContactPrimaryEmail");
                string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                var location = directoryName + "\\ValidationforContactPrimaryEmail.png";
                loginHelper.AddAttachment(location, id);
            }
        }
        else
        {
            if (Int16.Parse(counter) < 9)
            {
                executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                TakeScreenshot("ValidationforContactPrimaryEmail");
                string id = loginHelper.getIssueID("Validation for Contact Primary Email");
                string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                var location = directoryName + "\\ValidationforContactPrimaryEmail.png";
                loginHelper.AddAttachment(location, id);
                loginHelper.AddComment(loginHelper.getIssueID("Validation for Contact Primary Email"), "This issue is still occurring");
            }
        }
        JIRA = loginHelper.getIssueID("Validation for Contact Primary Email");
     //   executionLog.DeleteFile("Error");
        throw;

    }
    finally
    {
        executionLog.DeleteFile("ValidationforContactPrimaryEmail");
        executionLog.WriteInExcel("Validation for Contact Primary Email", Status, JIRA, "Contact Management");
    }
}
}
} 
