using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class VerifyCallStartTimeOnEdit : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin")]
        [TestCategory("BugTestNew")]
        public void verifyCallStartTimeOnEdit()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            //Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var officeActivities_CallsHelper = new OfficeActivities_CallsHelper(GetWebDriver());

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");
            
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("VerifyCallStartTimeOnEdit", "Login with valid credentials");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyCallStartTimeOnEdit", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyCallStartTimeOnEdit", "Go to Create Call page");
                VisitOffice("calls/create");
                officeActivities_CallsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCallStartTimeOnEdit", "Verify page title.");
                VerifyTitle("Log A Call");

                executionLog.Log("VerifyCallStartTimeOnEdit", "Select Call Type >> Inbound");
                officeActivities_CallsHelper.SelectByText("CallType", "Inbound");
                officeActivities_CallsHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyCallStartTimeOnEdit", "Enter Call from Name");
                officeActivities_CallsHelper.TypeText("CallFromName", "ABCD");

                executionLog.Log("VerifyCallStartTimeOnEdit", "Enter Call to Name");
                officeActivities_CallsHelper.TypeText("CallToName", "WXYZ");

                executionLog.Log("VerifyCallStartTimeOnEdit", "Enter From Number");
                officeActivities_CallsHelper.TypeText("FromNumber", "1111111111");

                executionLog.Log("VerifyCallStartTimeOnEdit", "Enter To Number");
                officeActivities_CallsHelper.TypeText("CallTONumber", "2222222222");

                executionLog.Log("VerifyCallStartTimeOnEdit", "Start Timer");
                officeActivities_CallsHelper.ClickElement("Start");
                officeActivities_CallsHelper.WaitForWorkAround(10000);

                executionLog.Log("VerifyCallStartTimeOnEdit", "Stop Timer");
                officeActivities_CallsHelper.ClickElement("Stop");
                officeActivities_CallsHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyCallStartTimeOnEdit", "Get Call Start Time");
                string strt_hr = officeActivities_CallsHelper.GetSelectdTxt("CallStartHour");
                string strt_min = officeActivities_CallsHelper.GetSelectdTxt("CallStartMin");
                string strt_sec = officeActivities_CallsHelper.GetSelectdTxt("CallStartSec");
                string strt_ampm = officeActivities_CallsHelper.GetSelectdTxt("CallStartAMPM");

                executionLog.Log("VerifyCallStartTimeOnEdit", "Click on Save button");
                officeActivities_CallsHelper.ClickElement("Save");
                officeActivities_CallsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCallStartTimeOnEdit", "Wait For confirmation");
                officeActivities_CallsHelper.WaitForText("Call logged successfully.",05);
                officeActivities_CallsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyCallStartTimeOnEdit", "Edit created time");
                officeActivities_CallsHelper.ClickElement("Edit");
                officeActivities_CallsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCallStartTimeOnEdit", "Verify Call Start Time");
                officeActivities_CallsHelper.VerifySelectdTxt("CallStartHour", strt_hr);
                officeActivities_CallsHelper.VerifySelectdTxt("CallStartMin", strt_min);
                officeActivities_CallsHelper.VerifySelectdTxt("CallStartSec", strt_sec);
                officeActivities_CallsHelper.VerifySelectdTxt("CallStartAMPM", strt_ampm);
                officeActivities_CallsHelper.WaitForWorkAround(1000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyCallStartTimeOnEdit");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Call Start Time On Edit");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Call Start Time On Edit", "Bug", "Medium", "Calls page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Call Start Time On Edit");
                        TakeScreenshot("VerifyCallStartTimeOnEdit");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyCallStartTimeOnEdit.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyCallStartTimeOnEdit");
                        string id = loginHelper.getIssueID("Verify Call Start Time On Edit");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyCallStartTimeOnEdit.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Call Start Time On Edit"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Call Start Time On Edit");
                //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyCallStartTimeOnEdit");
                executionLog.WriteInExcel("Verify Call Start Time On Edit", Status, JIRA, "Office Activities Calls");
            }
        }
    }
} 