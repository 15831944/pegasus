using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class AddMultipleContact : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("NewSkinTaskJira")]
        public void addMultipleContact()
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
            var officeActivities_EmailsHelper = new OfficeActivities_EmailsHelper(GetWebDriver());
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("AddMultipleContact", "Login with valid username and password");
                Login(username[0], password[0]);

                executionLog.Log("AddMultipleContact", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("AddMultipleContact", "Go to compose email page");
                VisitOffice("mails/compose");

                executionLog.Log("AddMultipleContact", "Verify title");
                VerifyTitle("Compose");

                executionLog.Log("AddMultipleContact", "Click on select employee");
                officeActivities_EmailsHelper.ClickElement("ToAddress");

                executionLog.Log("AddMultipleContact", "Select 1st employee");
                officeActivities_EmailsHelper.ClickElement("FEmployee");

                executionLog.Log("AddMultipleContact", "Select 2nd employee");
                officeActivities_EmailsHelper.ClickElement("SEmployee");

                executionLog.Log("AddMultipleContact", "Select 3rd employee");
                officeActivities_EmailsHelper.ClickElement("TEmployee");

                executionLog.Log("AddMultipleContact", "Click on Add button");
                officeActivities_EmailsHelper.ClickElement("AddEmployee");
                officeActivities_EmailsHelper.WaitForWorkAround(4000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("AddMultipleContact");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Add Multiple Contact");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Add Multiple Contact", "Bug", "Medium", "Compose page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Add Multiple Contact");
                        TakeScreenshot("AddMultipleContact");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AddMultipleContact.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("AddMultipleContact");
                        string id = loginHelper.getIssueID("Add Multiple Contact");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AddMultipleContact.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Add Multiple Contact"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Add Multiple Contact");
                executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("AddMultipleContact");
                executionLog.WriteInExcel("Add Multiple Contact", Status, JIRA, "Office Activities");
            }
        }
    }
}