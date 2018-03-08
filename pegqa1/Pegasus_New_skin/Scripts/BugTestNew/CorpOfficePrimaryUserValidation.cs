using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class CorpOfficePrimaryUserValidation : DriverTestCase
    {
        [TestMethod]
        [TestCategory("Corp")]
        [TestCategory("All")]
        [TestCategory("TS2")]
        [TestCategory("BugTestNew")]
        public void corpOfficePrimaryUserValidation()
        {
            string[] username1 = null;
            string[] password1 = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username1 = oXMLData.getData("settings/Credentials", "username_corp");
            password1 = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var corpOffice_OfficeHelper = new CorpOffice_OfficeHelper(GetWebDriver());

            // Variable random

            var username = "TESTUSER" + GetRandomNumber();
            var name = "Test" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("CorpOfficePrimaryUserValidation", "Login with valid username and password");
                Login("newthemecorp", "mynewpegasus");

                executionLog.Log("CorpOfficePrimaryUserValidation", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("CorpOfficePrimaryUserValidation", "Go to office page");
                VisitCorp("offices");
                corpOffice_OfficeHelper.WaitForWorkAround(4000);

                executionLog.Log("CorpOfficePrimaryUserValidation", "Verify title as offices");
                VerifyTitle("Offices");

                executionLog.Log("CorpOfficePrimaryUserValidation", "Click On Create button");
                corpOffice_OfficeHelper.ClickElement("Create");
                corpOffice_OfficeHelper.WaitForWorkAround(4000);

                executionLog.Log("CorpOfficePrimaryUserValidation", "Verify title as create an office.");
                VerifyTitle("Create an Office");

                executionLog.Log("CorpOfficePrimaryUserValidation", "Enter Name of the ofice");
                corpOffice_OfficeHelper.TypeText("Name", name);

                executionLog.Log("CorpOfficePrimaryUserValidation", "Enter DBAName");
                corpOffice_OfficeHelper.TypeText("DBAName", "TEST123");

                executionLog.Log("CorpOfficePrimaryUserValidation", "Enter Website");
                corpOffice_OfficeHelper.TypeText("Website", "TEST.COM");

                executionLog.Log("CorpOfficePrimaryUserValidation", "Enter OfficeCode");
                corpOffice_OfficeHelper.TypeText("OfficeCode", "12345");

                executionLog.Log("CorpOfficePrimaryUserValidation", "Select Address");
                corpOffice_OfficeHelper.Select("AddressType", "Office");

                executionLog.Log("CorpOfficePrimaryUserValidation", "Enter AddressLine1");
                corpOffice_OfficeHelper.TypeText("AddressLine1", "FC-89");

                executionLog.Log("CorpOfficePrimaryUserValidation", "Enter ZipCode");
                corpOffice_OfficeHelper.TypeText("ZIpCode", "");

                executionLog.Log("OfficeEditEmailPhoneValidation", " Click on do not create primary user checkbox.");
                corpOffice_OfficeHelper.ClickElement("PrimaryUser_ChkBox");
                corpOffice_OfficeHelper.WaitForWorkAround(3000);

                executionLog.Log("CorpOfficePrimaryUserValidation", " Click on save button");
                corpOffice_OfficeHelper.ClickElement("Save");
                corpOffice_OfficeHelper.WaitForWorkAround(3000);

                executionLog.Log("CorpOfficePrimaryUserValidation", "Enter ZipCode");
                corpOffice_OfficeHelper.TypeText("ZIpCode", "60601");

                executionLog.Log("CorpOfficePrimaryUserValidation", " Click on save button");
                corpOffice_OfficeHelper.ClickElement("Save");
                corpOffice_OfficeHelper.WaitForWorkAround(3000);

                executionLog.Log("CorpOfficePrimaryUserValidation", "Verify validation for primary user first name not present.");
                corpOffice_OfficeHelper.IsElementNotPresent("ValidationFirstName");

                executionLog.Log("CorpOfficePrimaryUserValidation", "Verify validation for primary user last name not present.");
                corpOffice_OfficeHelper.IsElementNotPresent("ValidationLastName");

                executionLog.Log("CorpOfficePrimaryUserValidation", "Verify validation for primary user email not present.");
                corpOffice_OfficeHelper.IsElementNotPresent("ValidationEmail");

                executionLog.Log("CorpOfficePrimaryUserValidation", "Verify text on the page");
                corpOffice_OfficeHelper.WaitForText("Office created successfully.", 40);

                executionLog.Log("CorpOfficePrimaryUserValidation", "Go to office page");
                VisitCorp("offices");
                corpOffice_OfficeHelper.WaitForWorkAround(5000);

                executionLog.Log("CorpOfficePrimaryUserValidation", "Verify title as offices");
                VerifyTitle("Offices");

                executionLog.Log("CorpOfficePrimaryUserValidation", "Enter Name to search");
                corpOffice_OfficeHelper.TypeText("EnterSelenium", name);
                corpOffice_OfficeHelper.WaitForWorkAround(5000);

                executionLog.Log("CorpOfficePrimaryUserValidation", "Click Delete btn  ");
                corpOffice_OfficeHelper.ClickElement("DeleteOffice");

                executionLog.Log("CorpOfficePrimaryUserValidation", "Verify page text");
                corpOffice_OfficeHelper.VerifyPageText("Are you sure want to delete the");

                executionLog.Log("CorpOfficePrimaryUserValidation", "Click Delete btn  ");
                corpOffice_OfficeHelper.ClickElement("ConfirmDelete");

                executionLog.Log("CorpOfficePrimaryUserValidation", "Accept alert message. ");
                corpOffice_OfficeHelper.AcceptAlert();
                corpOffice_OfficeHelper.WaitForWorkAround(5000);

                executionLog.Log("CorpOfficePrimaryUserValidation", "Wait for delete message. ");
                corpOffice_OfficeHelper.WaitForText("Office deleted successfully.", 40);

            }

            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CorpOfficePrimaryUserValidation");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Corp Office Primary User Validation");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Corp Office Primary User Validation", "Bug", "Medium", "Office page", "QA", "Log in as: " + username1[0] + " / " + password1[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Corp Office Primary User Validation");
                        TakeScreenshot("CorpOfficePrimaryUserValidation");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CorpOfficePrimaryUserValidation.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CorpOfficePrimaryUserValidation");
                        string id = loginHelper.getIssueID("Corp Office Primary User Validation");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CorpOfficePrimaryUserValidation.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Corp Office Primary User Validation"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Corp Office Primary User Validation");
                //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("CorpOfficePrimaryUserValidation");
                executionLog.WriteInExcel("Corp Office Primary User Validation", Status, JIRA, "Corp Office");
            }
        }
    }
}
