using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class OfficeEditEmailPhoneValidation : DriverTestCase
    {
        [TestMethod]
        [TestCategory("Corp")]
        [TestCategory("All")]
        [TestCategory("TS2")]
        [TestCategory("BugTestNew")]
        public void officeEditEmailPhoneValidation()
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

                executionLog.Log("OfficeEditEmailPhoneValidation", "Login with valid username and password");
                Login(username1[0], password1[0]);
                Console.WriteLine("Logged in as: " + username1[0] + " / " + password1[0]);

                executionLog.Log("OfficeEditEmailPhoneValidation", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("OfficeEditEmailPhoneValidation", "Go to office create page");
                VisitCorp("offices/create");
                corpOffice_OfficeHelper.WaitForWorkAround(2000);

                executionLog.Log("OfficeEditEmailPhoneValidation", "Verify title");
                VerifyTitle("Create an Office");

                executionLog.Log("OfficeEditEmailPhoneValidation", "Enter Name");
                corpOffice_OfficeHelper.TypeText("Name", name);
                corpOffice_OfficeHelper.WaitForWorkAround(3000);

                executionLog.Log("OfficeEditEmailPhoneValidation", "Enter DBAName");
                corpOffice_OfficeHelper.TypeText("DBAName", "TEST123");
                corpOffice_OfficeHelper.WaitForWorkAround(3000);

                executionLog.Log("OfficeEditEmailPhoneValidation", "Enter Website");
                corpOffice_OfficeHelper.TypeText("Website", "TEST.COM");
                corpOffice_OfficeHelper.WaitForWorkAround(3000);

                executionLog.Log("OfficeEditEmailPhoneValidation", "Enter OfficeCode");
                corpOffice_OfficeHelper.TypeText("OfficeCode", "12345");
                corpOffice_OfficeHelper.WaitForWorkAround(3000);

                executionLog.Log("OfficeEditEmailPhoneValidation", "Select Address");
                corpOffice_OfficeHelper.Select("AddressType", "Office");
                corpOffice_OfficeHelper.WaitForWorkAround(3000);

                executionLog.Log("OfficeEditEmailPhoneValidation", "Enter AddressLine1");
                corpOffice_OfficeHelper.TypeText("AddressLine1", "FC-89");
                corpOffice_OfficeHelper.WaitForWorkAround(3000);

                executionLog.Log("OfficeEditEmailPhoneValidation", "Enter ZipCode");
                corpOffice_OfficeHelper.TypeText("ZIpCode", "60601");
                corpOffice_OfficeHelper.WaitForWorkAround(3000);

                executionLog.Log("OfficeEditEmailPhoneValidation", " Click on do not create primary user checkbox.");
                corpOffice_OfficeHelper.ClickElement("PrimaryUser_ChkBox");
                corpOffice_OfficeHelper.WaitForWorkAround(3000);

                executionLog.Log("OfficeEditEmailPhoneValidation", " Click on save button");
                try
                {
                    corpOffice_OfficeHelper.ClickElement("Save");
                }
                catch (OpenQA.Selenium.WebDriverException)
                { }
                corpOffice_OfficeHelper.WaitForWorkAround(30000);
                

                executionLog.Log("OfficeEditEmailPhoneValidation", "Verify text on the page");
                corpOffice_OfficeHelper.WaitForText("Office created successfully.", 10);

                executionLog.Log("OfficeEditEmailPhoneValidation", "Go to office page");
                VisitCorp("offices");
                corpOffice_OfficeHelper.WaitForWorkAround(5000);

                executionLog.Log("OfficeEditEmailPhoneValidation", "Enter Name of the office to search");
                corpOffice_OfficeHelper.TypeText("EnterSelenium", name);
                corpOffice_OfficeHelper.WaitForWorkAround(5000);

                executionLog.Log("OfficeEditEmailPhoneValidation", "Click on edit icon to edit the office.");
                corpOffice_OfficeHelper.ClickElement("EditOffice");
                corpOffice_OfficeHelper.WaitForWorkAround(2000);

                executionLog.Log("OfficeEditEmailPhoneValidation", "Click on the save button.");
                try
                {
                    corpOffice_OfficeHelper.ClickElement("SaveEdit");
                }
                catch (OpenQA.Selenium.WebDriverException) { }
                corpOffice_OfficeHelper.WaitForWorkAround(2000);

                executionLog.Log("OfficeEditEmailPhoneValidation", "Verifies no alert present .");
                corpOffice_OfficeHelper.VerifyAlertNotPresent();

                executionLog.Log("OfficeEditEmailPhoneValidation", "Verify office updation success text.");
                corpOffice_OfficeHelper.WaitForText("Office updated successfully.", 30);

                executionLog.Log("OfficeEditEmailPhoneValidation", "Go to office page");
                VisitCorp("offices");
                corpOffice_OfficeHelper.WaitForWorkAround(5000);

                executionLog.Log("OfficeEditEmailPhoneValidation", "Enter Name to search");
                corpOffice_OfficeHelper.TypeText("EnterSelenium", name);
                corpOffice_OfficeHelper.WaitForWorkAround(5000);

                executionLog.Log("OfficeEditEmailPhoneValidation", "Click on Delete btn  ");
                corpOffice_OfficeHelper.ClickElement("DeleteOffice");

                executionLog.Log("OfficeEditEmailPhoneValidation", "Verify page text");
                corpOffice_OfficeHelper.VerifyPageText("Are you sure want to delete the");

                executionLog.Log("OfficeEditEmailPhoneValidation", "Click Delete btn  ");
                corpOffice_OfficeHelper.ClickElement("ConfirmDelete");

                executionLog.Log("OfficeEditEmailPhoneValidation", "Accept alert message. ");
                corpOffice_OfficeHelper.AcceptAlert();

                executionLog.Log("OfficeEditEmailPhoneValidation", "Wait for delete message. ");
                corpOffice_OfficeHelper.WaitForText("Office deleted successfully.", 40);

            }

            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("OfficeEditEmailPhoneValidation");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Office Edit Email Phone Validation");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Office Edit Email Phone Validation", "Bug", "Medium", "Office page", "QA", "Log in as: " + username1[0] + " / " + password1[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Office Edit Email Phone Validation");
                        TakeScreenshot("OfficeEditEmailPhoneValidation");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\OfficeEditEmailPhoneValidation.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("OfficeEditEmailPhoneValidation");
                        string id = loginHelper.getIssueID("Office Edit Email Phone Validation");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\OfficeEditEmailPhoneValidation.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Office Edit Email Phone Validation"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Office Edit Email Phone Validation");
                //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("OfficeEditEmailPhoneValidation");
                executionLog.WriteInExcel("Office Edit Email Phone Validation", Status, JIRA, "Corp Office");
            }
        }
    }
}
