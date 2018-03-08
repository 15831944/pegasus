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
    public class VerifyCallsCreatedAndModifiedByCredits : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin1")]
        [TestCategory("Fail")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Admin1")]
        public void verifyCallsCreatedAndModifiedByCredits()
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
            var officeActivities_CallsHelper = new OfficeActivities_CallsHelper(GetWebDriver());

            // Random Variable
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("VerifyCallsCreatedAndModifiedByCredits", " Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyCallsCreatedAndModifiedByCredits", " Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyCallsCreatedAndModifiedByCredits", " Redirect at admin page.");
                VisitOffice("admin");

                executionLog.Log("VerifyCallsCreatedAndModifiedByCredits", "Redirect at calls page.");
                VisitOffice("calls");
                officeActivities_CallsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCallsCreatedAndModifiedByCredits", " verify title");
                VerifyTitle("Calls");

                executionLog.Log("VerifyCallsCreatedAndModifiedByCredits", "Click on create button.");
                officeActivities_CallsHelper.ClickElement("Create");

                executionLog.Log("VerifyCallsCreatedAndModifiedByCredits", "Click on save button.");
                officeActivities_CallsHelper.ClickElement("Save");

                executionLog.Log("VerifyCallsCreatedAndModifiedByCredits", "verify validation for call type");
                officeActivities_CallsHelper.VerifyText("CallTypeError", "This field is required.");

                executionLog.Log("VerifyCallsCreatedAndModifiedByCredits", "verify validation for call name.");
                officeActivities_CallsHelper.VerifyText("CallNameError", "This field is required.");

                executionLog.Log("VerifyCallsCreatedAndModifiedByCredits", "verify validation for call to.");
                officeActivities_CallsHelper.VerifyText("CallToNameError", "This field is required.");

                executionLog.Log("VerifyCallsCreatedAndModifiedByCredits", "verify validation for from number.");
                officeActivities_CallsHelper.VerifyText("FromNumError", "This field is required.");

                executionLog.Log("VerifyCallsCreatedAndModifiedByCredits", "verify validation for to number.");
                officeActivities_CallsHelper.VerifyText("ToNumError", "This field is required.");

                executionLog.Log("VerifyCallsCreatedAndModifiedByCredits", "Select call type");
                officeActivities_CallsHelper.Select("CallType", "Personal");

                executionLog.Log("VerifyCallsCreatedAndModifiedByCredits", "Select call related to.");
                officeActivities_CallsHelper.Select("Relatedto", "20");

                executionLog.Log("VerifyCallsCreatedAndModifiedByCredits", "Click on find list icon.");
                officeActivities_CallsHelper.ClickElement("Findlist");
                officeActivities_CallsHelper.WaitForWorkAround(4000);

                executionLog.Log("VerifyCallsCreatedAndModifiedByCredits", "Click on first client.");
                officeActivities_CallsHelper.ClickElement("Client1");

                executionLog.Log("VerifyCallsCreatedAndModifiedByCredits", "Enter call from name.");
                officeActivities_CallsHelper.TypeText("CallFrom", "Howard Tang");

                executionLog.Log("VerifyCallsCreatedAndModifiedByCredits", " Enter call to name");
                officeActivities_CallsHelper.TypeText("CallToName", "Randy Jackson");

                executionLog.Log("VerifyCallsCreatedAndModifiedByCredits", " Enter call from number.");
                officeActivities_CallsHelper.TypeText("FromNumber", "1221221122");

                executionLog.Log("VerifyCallsCreatedAndModifiedByCredits", " Enter call to number.");
                officeActivities_CallsHelper.TypeText("CallTONumber", "1221221122");

                executionLog.Log("VerifyCallsCreatedAndModifiedByCredits", "Click on start button.");
                officeActivities_CallsHelper.ClickElement("Start");

                executionLog.Log("VerifyCallsCreatedAndModifiedByCredits", "Wait for some time.");
                officeActivities_CallsHelper.WaitForWorkAround(20000);

                executionLog.Log("VerifyCallsCreatedAndModifiedByCredits", "Click on stop button.");
                officeActivities_CallsHelper.ClickElement("Stop");

                executionLog.Log("VerifyCallsCreatedAndModifiedByCredits", "Click on save button.");
                officeActivities_CallsHelper.ClickElement("Save");

                executionLog.Log("VerifyCallsCreatedAndModifiedByCredits", "Wait for success text.");
                officeActivities_CallsHelper.WaitForText("Call logged successfully.", 10);

                executionLog.Log("VerifyCallsCreatedAndModifiedByCredits", "Redirect at calls page.");
                VisitOffice("calls");
                officeActivities_CallsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCallsCreatedAndModifiedByCredits", "Verify page title as calls.");
                VerifyTitle("Calls");

                executionLog.Log("VerifyCallsCreatedAndModifiedByCredits", "Click on created call.");
                officeActivities_CallsHelper.ClickElement("Call1");

                executionLog.Log("VerifyCallsCreatedAndModifiedByCredits", "Verify call created by credits.");
                officeActivities_CallsHelper.VerifyText("CreatedBy", "Howard Tang");

                executionLog.Log("VerifyCallsCreatedAndModifiedByCredits", "Verify call modified by credits.");
                officeActivities_CallsHelper.VerifyText("ModifiedBy", "Howard Tang");

                executionLog.Log("VerifyCallsCreatedAndModifiedByCredits", "Click on edit button.");
                officeActivities_CallsHelper.ClickElement("EditLink");

                executionLog.Log("VerifyCallsCreatedAndModifiedByCredits", "Select call related to.");
                officeActivities_CallsHelper.Select("Relatedto", "14");

                executionLog.Log("VerifyCallsCreatedAndModifiedByCredits", "Click on find list icon.");
                officeActivities_CallsHelper.ClickElement("Findlist");

                executionLog.Log("VerifyCallsCreatedAndModifiedByCredits", "Click on first lead.");
                officeActivities_CallsHelper.ClickElement("Client1");

                executionLog.Log("VerifyCallsCreatedAndModifiedByCredits", "Enter call from name.");
                officeActivities_CallsHelper.TypeText("CallFrom", "Howard Tang");

                executionLog.Log("VerifyCallsCreatedAndModifiedByCredits", " Enter call to name");
                officeActivities_CallsHelper.TypeText("CallToName", "Randy Jackson");

                executionLog.Log("VerifyCallsCreatedAndModifiedByCredits", " Enter call from number.");
                officeActivities_CallsHelper.TypeText("FromNumber", "1221221122");

                executionLog.Log("VerifyCallsCreatedAndModifiedByCredits", " Enter call to number.");
                officeActivities_CallsHelper.TypeText("CallTONumber", "1221221122");

                executionLog.Log("VerifyCallsCreatedAndModifiedByCredits", "Click on start button.");
                officeActivities_CallsHelper.ClickElement("Start");

                executionLog.Log("VerifyCallsCreatedAndModifiedByCredits", "Wait for some time.");
                officeActivities_CallsHelper.WaitForWorkAround(20000);

                executionLog.Log("VerifyCallsCreatedAndModifiedByCredits", "Click on stop button.");
                officeActivities_CallsHelper.ClickElement("Stop");

                executionLog.Log("VerifyCallsCreatedAndModifiedByCredits", "Click on save button.");
                officeActivities_CallsHelper.ClickElement("Save");

                executionLog.Log("VerifyCallsCreatedAndModifiedByCredits", "Wait for updation success message.");
                officeActivities_CallsHelper.WaitForText("Updated Successfully.", 10);

                executionLog.Log("VerifyCallsCreatedAndModifiedByCredits", "Redirect at calls page.");
                VisitOffice("calls");
                officeActivities_CallsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCallsCreatedAndModifiedByCredits", "Verify page title as calls.");
                VerifyTitle("Calls");
                officeActivities_CallsHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyCallsCreatedAndModifiedByCredits", "Click on created call.");
                officeActivities_CallsHelper.ClickElement("Call1");
                officeActivities_CallsHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyCallsCreatedAndModifiedByCredits", "Verify call created by credits.");
                officeActivities_CallsHelper.VerifyText("CreatedBy", "Howard Tang");
                officeActivities_CallsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyCallsCreatedAndModifiedByCredits", "Verify call modified by credits.");
                officeActivities_CallsHelper.VerifyText("ModifiedBy", "Howard Tang");
                officeActivities_CallsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyCallsCreatedAndModifiedByCredits", "Redirect at calls page.");
                VisitOffice("calls");
                officeActivities_CallsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCallsCreatedAndModifiedByCredits", " verify title");
                VerifyTitle("Calls");
                officeActivities_CallsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCallsCreatedAndModifiedByCredits", "Wait for next element to present.");
                officeActivities_CallsHelper.WaitForElementPresent("Call1", 10);

                executionLog.Log("VerifyCallsCreatedAndModifiedByCredits", "Click on first check box");
                officeActivities_CallsHelper.ClickElement("ChkBox1");

                executionLog.Log("VerifyCallsCreatedAndModifiedByCredits", "Click on delete button.");
                officeActivities_CallsHelper.ClickElement("Delete");

                executionLog.Log("VerifyCallsCreatedAndModifiedByCredits", "Click on OK to accept alert message.");
                officeActivities_CallsHelper.AcceptAlert();
                officeActivities_CallsHelper.WaitForWorkAround(5000);

                executionLog.Log("VerifyCallsCreatedAndModifiedByCredits", "Wait for text call deleted.");
                officeActivities_CallsHelper.WaitForText("Call deleted successfully.", 20);

                executionLog.Log("VerifyCallsCreatedAndModifiedByCredits", "Redirect at call recycle bin page.");
                VisitOffice("calls/recyclebin");

                executionLog.Log("VerifyCallsCreatedAndModifiedByCredits", "Verify page title..");
                VerifyTitle("Recycled Calls");
                officeActivities_CallsHelper.WaitForWorkAround(5000);

                executionLog.Log("VerifyCallsCreatedAndModifiedByCredits", "Click on delete icon.");
                officeActivities_CallsHelper.ClickElement("DeleteRecycle");
                officeActivities_CallsHelper.AcceptAlert();

                executionLog.Log("VerifyCallsCreatedAndModifiedByCredits", "Wait for deletion success text.");
                officeActivities_CallsHelper.WaitForText("Call Permanently Deleted.", 30);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyCallsCreatedAndModifiedByCredits");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Calls Created And Modified By Credits");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Calls Created And Modified By Credits", "Bug", "Medium", "Calls page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Calls Created And Modified By Credits");
                        TakeScreenshot("VerifyCallsCreatedAndModifiedByCredits");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyCallsCreatedAndModifiedByCredits.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyCallsCreatedAndModifiedByCredits");
                        string id = loginHelper.getIssueID("Verify Calls Created And Modified By Credits");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyCallsCreatedAndModifiedByCredits.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Calls Created And Modified By Credits"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Calls Created And Modified By Credits");
            //    executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("VerifyCallsCreatedAndModifiedByCredits");
                executionLog.WriteInExcel("Verify Calls Created And Modified By Credits", Status, JIRA, "Office Activities");
            }
        }
    }
}