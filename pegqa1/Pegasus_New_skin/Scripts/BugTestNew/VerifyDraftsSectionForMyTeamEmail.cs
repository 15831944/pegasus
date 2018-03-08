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
    public class VerifyDraftsSectionForMyTeamEmail : DriverTestCase
    {
        [TestMethod]
        [TestCategory("Bug")]
        [TestCategory("BugTestNew")]
        public void verifyDraftsSectionForMyTeamEmail()
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
            var officeActivities_EmailsHelper = new OfficeActivities_EmailsHelper(GetWebDriver());

            // Random Variable.
            String JIRA = "";
            String Status = "Pass";
            try
            {

                executionLog.Log("VerifyDraftsSectionForMyTeamEmail", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyDraftsSectionForMyTeamEmail", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyDraftsSectionForMyTeamEmail", "Redirect at Emails page");
                VisitOffice("mails");
                officeActivities_EmailsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyDraftsSectionForMyTeamEmail", "Select My Team Emails");
                officeActivities_EmailsHelper.SelectByText("EmailAccount", "My Team E-Mails");
                officeActivities_EmailsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyDraftsSectionForMyTeamEmail", "Verify Drafts section present");
                officeActivities_EmailsHelper.VerifyText("SecondTile", "Drafts");
                //officeActivities_EmailsHelper.WaitForWorkAround(3000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyDraftsSectionForMyTeamEmail");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Drafts Section For My Team Email");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Drafts Section For My Team Email", "Bug", "Medium", "Email page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Drafts Section For My Team Email");
                        TakeScreenshot("VerifyDraftsSectionForMyTeamEmail");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyDraftsSectionForMyTeamEmail.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyDraftsSectionForMyTeamEmail");
                        string id = loginHelper.getIssueID("Verify Drafts Section For My Team Email");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyDraftsSectionForMyTeamEmail.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Drafts Section For My Team Email"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Drafts Section For My Team Email");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyDraftsSectionForMyTeamEmail");
                executionLog.WriteInExcel("Verify Drafts Section For My Team Email", Status, JIRA, "Emails");
            }
        }
    }
}