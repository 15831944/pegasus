using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class EditOfficeSaveInvalidWebLink : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        public void editOfficeSaveInvalidWebLink()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");
            username = oXMLData.getData("settings/Credentials", "username_corp");
            password = oXMLData.getData("settings/Credentials", "password2");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var corpOffice_OfficeHelper = new CorpOffice_OfficeHelper(GetWebDriver());

            // Variable random
            var usernme = "Sysprins" + RandomNumber(44, 777);
            var name = "Test" + RandomNumber(99, 999);
            String JIRA = "";
            String Status = "Pass";

            //          try
            //        {

            executionLog.Log("EditOfficeSaveInvalidWebLink", "Login with valid username and password");
            Login(username[0], password[0]);

            executionLog.Log("EditOfficeSaveInvalidWebLink", "Verify Page title");
            VerifyTitle("Dashboard");

            executionLog.Log("EditOfficeSaveInvalidWebLink", "Redirect to office page.");
            VisitCorp("offices");

            executionLog.Log("EditOfficeSaveInvalidWebLink", "Enter Name To Search Office");
            corpOffice_OfficeHelper.TypeText("EnterSelenium","Aslam Office");
            corpOffice_OfficeHelper.WaitForWorkAround(2000);

            executionLog.Log("EditOfficeSaveInvalidWebLink", "Click on edit office icon");
            corpOffice_OfficeHelper.ClickElement("EditOffice");

            executionLog.Log("EditOfficeSaveInvalidWebLink", "Select eAddress Type as web link");
            corpOffice_OfficeHelper.Select("EaddressType", "Web Links");

            executionLog.Log("EditOfficeSaveInvalidWebLink", "Enter an invalid web link");
            corpOffice_OfficeHelper.TypeText("eAddress", "Test");

            executionLog.Log("EditOfficeSaveInvalidWebLink", "Save updated details");
            corpOffice_OfficeHelper.ClickElement("SaveEdit");

            executionLog.Log("EditOfficeSaveInvalidWebLink", "Verify validation for invalid eAddress.");
            corpOffice_OfficeHelper.VerifyText("WeblinkValidation", "Please enter a valid email address.");

            executionLog.Log("EditOfficeSaveInvalidWebLink", "Enter a valid eaddress.");
            corpOffice_OfficeHelper.TypeText("eAddress", "Test@gmail.com");

            executionLog.Log("EditOfficeSaveInvalidWebLink", "Save updated details");
            corpOffice_OfficeHelper.ClickElement("SaveEdit");

            executionLog.Log("EditOfficeSaveInvalidWebLink", "Verify validation for invalid eAddress.");
            corpOffice_OfficeHelper.VerifyText("WeblinkValidation", "Please enter a valid email address.");

            executionLog.Log("EditOfficeSaveInvalidWebLink", "Enter a valid web link.");
            corpOffice_OfficeHelper.TypeText("eAddress", "https://www.google.com");

            executionLog.Log("EditOfficeSaveInvalidWebLink", "Save updated details");
            corpOffice_OfficeHelper.ClickElement("SaveEdit");

            executionLog.Log("EditOfficeSaveInvalidWebLink", "Wait for success message.");
            corpOffice_OfficeHelper.WaitForText("Office updated successfully.", 10);

        }
    } }
       /*     catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("EditOfficeSaveInvalidWebLink");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Edit Office Save Invalid WebLink");
                if (!result)
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Edit Office Save Invalid WebLink", "Bug", "Medium", "Corp Office page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Edit Office Save Invalid WebLink");
                        TakeScreenshot("EditOfficeSaveInvalidWebLink");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditOfficeSaveInvalidWebLink.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("EditOfficeSaveInvalidWebLink");
                        string id = loginHelper.getIssueID("Edit Office Save Invalid WebLink");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditOfficeSaveInvalidWebLink.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Edit Office Save Invalid WebLink"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Edit Office Save Invalid WebLink");
                executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("EditOfficeSaveInvalidWebLink");
                executionLog.WriteInExcel("Edit Office Save Invalid WebLink", Status, JIRA, "Corp Office");
            }
        }
    }
}*/