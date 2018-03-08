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
    public class MergeLeadVerifyConfirmation : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS2")]
        [TestCategory("BugTestNew")]
        public void mergeLeadVerifyConfirmation()
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
            var office_LeadsHelper = new Office_LeadsHelper(GetWebDriver());

            // Random Variables.
            String JIRA = "";
            String Status = "Pass";
            var File = GetPathToFile() + "leadsamples.csv";

            try
            {

                executionLog.Log("MergeLeadVerifyConfirmation", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("MergeLeadVerifyConfirmation", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("MergeLeadVerifyConfirmation", "Redirect at leads import page");
                VisitOffice("leads/import");

                executionLog.Log("MergeLeadVerifyConfirmation", "Upload a csv file.");
                office_LeadsHelper.UploadFile("//*[@id='vcard_file']", File);

                executionLog.Log("MergeLeadVerifyConfirmation", "Click on import button.");
                office_LeadsHelper.ClickElement("LeadImport");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("MergeLeadVerifyConfirmation", "Scroll to merge button.");
                office_LeadsHelper.ScrollDown("//button[@title='Merge']");

                executionLog.Log("MergeLeadVerifyConfirmation", "Click on  merge button");
                office_LeadsHelper.ClickElement("MergeDuplicate");
                office_LeadsHelper.WaitForWorkAround(1000);

                executionLog.Log("MergeLeadVerifyConfirmation", "Accept alert message.");
                office_LeadsHelper.AcceptAlert();
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("MergeLeadVerifyConfirmation", "Verify text on page.");
                office_LeadsHelper.VerifyPageText("No records are merged");


            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("MergeLeadVerifyConfirmation");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Merge Lead Verify Confirmation");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Merge Lead Verify Confirmation", "Bug", "Medium", "Leads page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Merge Lead Verify Confirmation");
                        TakeScreenshot("MergeLeadVerifyConfirmation");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\MergeLeadVerifyConfirmation.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("MergeLeadVerifyConfirmation");
                        string id = loginHelper.getIssueID("Merge Lead Verify Confirmation");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\MergeLeadVerifyConfirmation.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Merge Lead Verify Confirmation"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Merge Lead Verify Confirmation");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("MergeLeadVerifyConfirmation");
                executionLog.WriteInExcel("Merge Lead Verify Confirmation", Status, JIRA, "Leads Management");
            }
        }
    }
}