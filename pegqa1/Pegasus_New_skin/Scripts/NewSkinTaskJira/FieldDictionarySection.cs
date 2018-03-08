using System;
using System.IO;
using LinqToExcel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class FieldDictionarySection : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS5")]
        [TestCategory("NewSkinTaskJira")]
        public void fieldDictionarySection()
        {
            string[] username = null;
            string[] password = null;
            string[] log = null;
            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");
            log = oXMLData.getData("settings/URL", "logout");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var office_FieldDictionary_SectionsHelper = new Office_FieldDictionary_SectionsHelper(GetWebDriver());

            String Status = "Pass";
            String JIRA = "";

            try
            {
                executionLog.Log("FieldDictionarySection", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("FieldDictionarySection", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("FieldDictionarySection", "Go to Field Dictionary Section page");
                VisitOffice("sections");

                executionLog.Log("FieldDictionarySection", "Verify Title");
                VerifyTitle("Section Management");

                executionLog.Log("FieldDictionarySection", "Click on Change tab button");
                office_FieldDictionary_SectionsHelper.ClickElement("FSChangeTab");
                office_FieldDictionary_SectionsHelper.WaitForWorkAround(3000);

                executionLog.Log("FieldDictionarySection", "Verify error not displayed");
                office_FieldDictionary_SectionsHelper.VerifyTextNotPresent("An Internal Error Has Occurred");
                
                executionLog.Log("FieldDictionarySection", "Click on Save button");
                office_FieldDictionary_SectionsHelper.ClickOnDisplayed("EditSave");
                office_FieldDictionary_SectionsHelper.WaitForWorkAround(2000);

                executionLog.Log("FieldDictionarySection", "Accept alert");
                office_FieldDictionary_SectionsHelper.AcceptAlert();

                executionLog.Log("FieldDictionarySection", "Verify error not displayed");
                office_FieldDictionary_SectionsHelper.VerifyTextNotPresent("An Internal Error Has Occurred");

                executionLog.Log("FieldDictionarySection", "Log out from the application");
                VisitOffice("logout");

            }
            catch (Exception e)
             {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";
                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("FieldDictionarySection");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Field Dictionary Section");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Field Dictionary Section", "Bug", "Medium", "Field Section page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Field Dictionary Section");
                        TakeScreenshot("FieldDictionarySection");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\FieldDictionarySection.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("FieldDictionarySection");
                        string id = loginHelper.getIssueID("Field Dictionary Section");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\FieldDictionarySection.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Field Dictionary Section"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Field Dictionary Section");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("FieldDictionarySection");
                executionLog.WriteInExcel("Field Dictionary Section", Status, JIRA, "Field Management");
            }
        }
    }
}   