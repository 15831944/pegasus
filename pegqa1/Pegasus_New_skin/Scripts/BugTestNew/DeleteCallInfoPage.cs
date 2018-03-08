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
    public class DeleteCallInfoPage : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("Fail")]
        [TestCategory("TS2")]
        [TestCategory("BugTestNew")]
        public void deleteCallInfoPage()
        {
            string[] username = null;
            string[] password = null;

            XMLParse oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            //Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var officeActivities_CallsHelper = new OfficeActivities_CallsHelper(GetWebDriver());

            // Random Variable
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("DeleteCallInfoPage", " Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("DeleteCallInfoPage", " Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("DeleteCallInfoPage", " Redirect at admin page.");
                VisitOffice("admin");

                executionLog.Log("DeleteCallInfoPage", "Redirect at calls page.");
                VisitOffice("calls");
                officeActivities_CallsHelper.WaitForWorkAround(4000);

                executionLog.Log("DeleteCallInfoPage", " verify title");
                VerifyTitle("Calls");
                officeActivities_CallsHelper.WaitForWorkAround(2000);

                executionLog.Log("DeleteCallInfoPage", "Click on create button.");
                officeActivities_CallsHelper.ClickElement("Create");
                officeActivities_CallsHelper.WaitForWorkAround(4000);

                executionLog.Log("DeleteCallInfoPage", "Select call type");
                officeActivities_CallsHelper.Select("CallType", "Personal");
                officeActivities_CallsHelper.WaitForWorkAround(2000);

                executionLog.Log("DeleteCallInfoPage", "Enter call from name.");
                officeActivities_CallsHelper.TypeText("CallFrom", "Howard Tang");

                executionLog.Log("DeleteCallInfoPage", " Enter call to name");
                officeActivities_CallsHelper.TypeText("CallToName", "Randy Jackson");

                executionLog.Log("DeleteCallInfoPage", " Enter call from number.");
                officeActivities_CallsHelper.TypeText("FromNumber", "1221221122");

                executionLog.Log("DeleteCallInfoPage", " Enter call to number.");
                officeActivities_CallsHelper.TypeText("CallTONumber", "1221221122");

                executionLog.Log("DeleteCallInfoPage", "Click on start button.");
                officeActivities_CallsHelper.ClickElement("Start");

                executionLog.Log("DeleteCallInfoPage", "Wait for some time.");
                officeActivities_CallsHelper.WaitForWorkAround(20000);

                executionLog.Log("DeleteCallInfoPage", "Click on stop button.");
                officeActivities_CallsHelper.ClickElement("Stop");

                executionLog.Log("DeleteCallInfoPage", "Click on save button.");
                officeActivities_CallsHelper.ClickElement("Save");
                officeActivities_CallsHelper.WaitForWorkAround(3000);

                executionLog.Log("DeleteCallInfoPage", "Wait for success text.");
                officeActivities_CallsHelper.WaitForText("Call logged successfully.", 15);

                executionLog.Log("DeleteCallInfoPage", "Click on call to be deleted.");
                officeActivities_CallsHelper.ClickForce("Call1");

                executionLog.Log("DeleteCallInfoPage", "Click on delete button.");
                officeActivities_CallsHelper.ClickElement("Delete");
                officeActivities_CallsHelper.WaitForWorkAround(1000);

                executionLog.Log("DeleteCallInfoPage", "Click on OK to accept alert message.");
                officeActivities_CallsHelper.AcceptAlert();

                executionLog.Log("DeleteCallInfoPage", "Wait for text call deleted.");
                officeActivities_CallsHelper.WaitForText("Call successfully deleted.", 20);

                executionLog.Log("DeleteCallInfoPage", "Redirect at recycle bin page.");
                VisitOffice("calls/recyclebin");
                officeActivities_CallsHelper.WaitForWorkAround(2000);

                executionLog.Log("DeleteCallInfoPage", "Verify page title.");
                VerifyTitle("Recycled Calls");
                officeActivities_CallsHelper.WaitForWorkAround(2000);

                executionLog.Log("DeleteCallInfoPage", "Click on delete icon.");
                officeActivities_CallsHelper.ClickElement("DeleteRecycle");
                officeActivities_CallsHelper.WaitForWorkAround(2000);

                executionLog.Log("DeleteCallInfoPage", "Accept alert message.");
                officeActivities_CallsHelper.AcceptAlert();

                executionLog.Log("DeleteCallInfoPage", "Wait success confirmation.");
                officeActivities_CallsHelper.WaitForText("Call Permanently Deleted.", 20);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("DeleteCallInfoPage");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Delete Call Info Page");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Delete Call Info Page", "Bug", "Medium", "Calls page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Delete Call Info Page");
                        TakeScreenshot("DeleteCallInfoPage");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\DeleteCallInfoPage.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("DeleteCallInfoPage");
                        string id = loginHelper.getIssueID("Delete Call Info Page");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\DeleteCallInfoPage.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Delete Call Info Page"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Delete Call Info Page");
               // executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("DeleteCallInfoPage");
                executionLog.WriteInExcel("Delete Call Info Page", Status, JIRA, "Office Activities");
            }
        }
    }
}