using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class OpportunititySubjectFiledValidateBlank : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS5")]
        [TestCategory("NewSkinTaskJira")]
        public void opportunititySubjectFiledValidateBlank()
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
            var office_OpportunitiesHelper = new Office_OpportunitiesHelper(GetWebDriver());

            // VARIABLE
            var name = "TestEmployee" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("OpportunititySubjectFiledValidateBlank", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("OpportunititySubjectFiledValidateBlank", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("OpportunititySubjectFiledValidateBlank", "Redirect at opportunities page.");
                VisitOffice("opportunities");
                office_OpportunitiesHelper.WaitForWorkAround(2000);

                executionLog.Log("OpportunititySubjectFiledValidateBlank", "Click TO View Opp");
                office_OpportunitiesHelper.ClickElement("Opportunities1");

                executionLog.Log("OpportunititySubjectFiledValidateBlank", "Click on add note");
                office_OpportunitiesHelper.ClickElement("AddNotes");
                office_OpportunitiesHelper.WaitForWorkAround(2000);

                executionLog.Log("OpportunititySubjectFiledValidateBlank", "Leave note subject blank.");
                office_OpportunitiesHelper.TypeText("EnterNoteName", "    ");

                executionLog.Log("OpportunititySubjectFiledValidateBlank", "Click Save Note Button");
                office_OpportunitiesHelper.ClickElement("SaveNote");
                office_OpportunitiesHelper.WaitForWorkAround(3000);

                executionLog.Log("OpportunititySubjectFiledValidateBlank", "Verify validation message.");
                office_OpportunitiesHelper.VerifyText("VerifyTextNoteValidation", "This field is required.");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("OpportunititySubjectFiledValidateBlank");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Opportunitity Subject Filed Validate Blank");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Opportunitity Subject Filed Validate Blank", "Bug", "Medium", "Add Note page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Opportunitity Subject Filed Validate Blank");
                        TakeScreenshot("OpportunititySubjectFiledValidateBlank");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\OpportunititySubjectFiledValidateBlank.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("OpportunititySubjectFiledValidateBlank");
                        string id = loginHelper.getIssueID("Opportunitity Subject Filed Validate Blank");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\OpportunititySubjectFiledValidateBlank.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Opportunitity Subject Filed Validate Blank"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Opportunitity Subject Filed Validate Blank");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("OpportunititySubjectFiledValidateBlank");
                executionLog.WriteInExcel("Opportunitity Subject Filed Validate Blank", Status, JIRA, "Opportunities Management");
            }
        }
    }
}