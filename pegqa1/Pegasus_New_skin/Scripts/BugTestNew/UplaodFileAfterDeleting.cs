using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class UplaodFileAfterDeleting : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS3")]
        [TestCategory("BugTestNew")]
        public void uplaodFileAfterDeleting()
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
            var office_MyProfileHelper = new Office_MyProfileHelper(GetWebDriver());

            // Variable
            var name = "Testing Subject" + GetRandomNumber();
            var email = "Test" + GetRandomNumber() + "@gmail.com";
            String JIRA = "";
            String Status = "Pass";
            try
            {

                executionLog.Log("UplaodFileAfterDeleting", " Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("UplaodFileAfterDeleting", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("UplaodFileAfterDeleting", "Click On  Admin");
                VisitOffice("admin");

                executionLog.Log("UplaodFileAfterDeleting", "Redirect to My corp page.");
                VisitOffice("mycorp");
                office_MyProfileHelper.WaitForWorkAround(5000);

                executionLog.Log("UplaodFileAfterDeleting", "Click on add document link.");
                office_MyProfileHelper.ClickJS("AddDoc");
                office_MyProfileHelper.WaitForWorkAround(4000);

                executionLog.Log("UplaodFileAfterDeleting", "Enter Document name");
                office_MyProfileHelper.TypeText("DocName", "Test Document");

                executionLog.Log("UplaodFileAfterDeleting", "Upload File ");
                String Filename = GetPathToFile() + "index.jpg";
                office_MyProfileHelper.Upload("BrowseFile", Filename);

                executionLog.Log("UplaodFileAfterDeleting", "Delete uploaded file.");
                office_MyProfileHelper.ClickJS("RemoveFile");

                executionLog.Log("UplaodFileAfterDeleting", "Click on save button.");
                office_MyProfileHelper.ClickJS("SaveDoc");
                office_MyProfileHelper.WaitForWorkAround(6000);

                executionLog.Log("UplaodFileAfterDeleting", "Verify validation is displayed for attacment.");
                office_MyProfileHelper.VerifyText("VerifyValidation", "This field is required.");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("UplaodFileAfterDeleting");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Uplaod File After Deleting");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Uplaod File After Deleting", "Bug", "Medium", "Document page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Uplaod File After Deleting");
                        TakeScreenshot("UplaodFileAfterDeleting");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\UplaodFileAfterDeleting.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("UplaodFileAfterDeleting");
                        string id = loginHelper.getIssueID("Uplaod File After Deleting");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\UplaodFileAfterDeleting.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Uplaod File After Deleting"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Uplaod File After Deleting");
             //   executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("UplaodFileAfterDeleting");
                executionLog.WriteInExcel("Uplaod File After Deleting", Status, JIRA, "Admin Corporate");
            }
        }
    }
}