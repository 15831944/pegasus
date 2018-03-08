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
    public class MergeLeadSelectAnyAndCancel : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("Fail")]
        [TestCategory("TS2")]
        [TestCategory("BugTestNew")]
        public void mergeLeadSelectAnyAndCancel()
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

            // Random Variable.
            var File = GetPathToFile() + "leadsamples.csv";
            String JIRA = "";
            String Status = "Pass";


            try
            {
                executionLog.Log("MergeLeadSelectAnyAndCancel", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("MergeLeadSelectAnyAndCancel", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("MergeLeadSelectAnyAndCancel", "Redirect at lead import page.");
                VisitOffice("leads/import");

                executionLog.Log("MergeLeadSelectAnyAndCancel", "Upload a csv file.");
                office_LeadsHelper.Upload("BrowseFile", File);

                executionLog.Log("MergeLeadSelectAnyAndCancel", "Click on import button");
                office_LeadsHelper.ClickElement("LeadImport");
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("MergeLeadSelectAnyAndCancel", "Select primary record");
                office_LeadsHelper.ClickElement("LeadCompyRadioBtn");

                executionLog.Log("MergeLeadSelectAnyAndCancel", "Scroll to merge button.");
                office_LeadsHelper.ScrollDown("//div[@class='row']/div/a[@title='Merge']");

                executionLog.Log("MergeLeadSelectAnyAndCancel", "Click on merge button");
                office_LeadsHelper.ClickElement("ClickOnMergeBtn");

                executionLog.Log("MergeLeadSelectAnyAndCancel", "Click cancel to decline alert.");
                office_LeadsHelper.DeclineAlert();

                executionLog.Log("MergeLeadSelectAnyAndCancel", "Click on merge button");
                office_LeadsHelper.ClickElement("ClickOnMergeBtn");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("MergeLeadSelectAnyAndCancel");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Merge Lead Select Any And Cancel");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Merge Lead Select Any And Cancel", "Bug", "Medium", "Leads page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Merge Lead Select Any And Cancel");
                        TakeScreenshot("MergeLeadSelectAnyAndCancel");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\MergeLeadSelectAnyAndCancel.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("MergeLeadSelectAnyAndCancel");
                        string id = loginHelper.getIssueID("Merge Lead Select Any And Cancel");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\MergeLeadSelectAnyAndCancel.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Merge Lead Select Any And Cancel"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Merge Lead Select Any And Cancel");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("MergeLeadSelectAnyAndCancel");
                executionLog.WriteInExcel("Merge Lead Select Any And Cancel", Status, JIRA, "Leads Management");
            }
        }
    }
}