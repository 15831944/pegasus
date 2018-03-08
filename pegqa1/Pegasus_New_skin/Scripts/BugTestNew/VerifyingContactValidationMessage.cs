using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class VerifyingContactValidationMessage : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS3")]
        [TestCategory("BugTestNew")]
        public void verifyingContactValidationMessage()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var office_ContactsHelper = new Office_ContactsHelper(GetWebDriver());

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Variable
            var FName = "Test" + RandomNumber(99, 99999);
            var LName = "Test" + RandomNumber(99, 99999);
            var CDBA = "NewComp" + RandomNumber(1, 99999);
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("VerifyingContactValidationMessage", "Login with valid credential  Username");
                Login(username[0], password[0]);

                executionLog.Log("VerifyingContactValidationMessage", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyingContactValidationMessage", "Reditect at contact page.");
                VisitOffice("contacts");

                executionLog.Log("VerifyingContactValidationMessage", "Verify title");
                office_ContactsHelper.VerifyText("VerifyContact", "Contacts");

                executionLog.Log("VerifyingContactValidationMessage", "Reditect at create contact page.");
                VisitOffice("contacts/create");

                executionLog.Log("VerifyingContactValidationMessage", "First Name");
                office_ContactsHelper.TypeText("FirstNAME", FName);

                executionLog.Log("VerifyingContactValidationMessage", "Last Name");
                office_ContactsHelper.TypeText("LastName", LName);

                executionLog.Log("VerifyingContactValidationMessage", "Company DBA Name");
                office_ContactsHelper.TypeText("CompanyName", CDBA);

                executionLog.Log("VerifyingContactValidationMessage", "Select Eadress type");
                office_ContactsHelper.Select("EaddressType", "E-Mail");

                executionLog.Log("VerifyingContactValidationMessage", "Enter Eadress");
                office_ContactsHelper.TypeText("Eaddress", "test@yopmail.com");

                executionLog.Log("VerifyingContactValidationMessage", "Cliclk Add email button");
                office_ContactsHelper.ClickElement("AddEmail");

                executionLog.Log("VerifyingContactValidationMessage", "Select EAddress type");
                office_ContactsHelper.Select("EaddressType2", "E-Mail");

                executionLog.Log("VerifyingContactValidationMessage", "Enter eAddress");
                office_ContactsHelper.TypeText("Eaddress2", "test@yopmail.com");

                executionLog.Log("VerifyingContactValidationMessage", "Select Second email as primary");
                office_ContactsHelper.Click("//input[@name='data[ContactElectronicAddress][1][primary]']");

                executionLog.Log("VerifyingContactValidationMessage", "Remove Second Email");
                office_ContactsHelper.ClickElement("RemoveEmailIcon");

                executionLog.Log("VerifyingContactValidationMessage", "Click on savw button");
                office_ContactsHelper.ClickElement("SaveContactN");
                office_ContactsHelper.WaitForWorkAround(4000);

                executionLog.Log("VerifyingContactValidationMessage", "Verify the validation message");
                office_ContactsHelper.VerifyAlertText("please select a primary email");

                executionLog.Log("VerifyingContactValidationMessage", "Accept alert");
                office_ContactsHelper.AcceptAlert();

                executionLog.Log("VerifyingContactValidationMessage", " Verify user is redirected to create page");
                VerifyTitle("Create a Contact");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyingContactValidationMessage");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("VerifyingContactValidationMessage");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("VerifyingContactValidationMessage", "Bug", "Medium", "Create Contact page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("VerifyingContactValidationMessage");
                        TakeScreenshot("VerifyingContactValidationMessage");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyingContactValidationMessage.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyingContactValidationMessage");
                        string id = loginHelper.getIssueID("VerifyingContactValidationMessage");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyingContactValidationMessage.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("VerifyingContactValidationMessage"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("VerifyingContactValidationMessage");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyingContactValidationMessage");
                executionLog.WriteInExcel("VerifyingContactValidationMessage", Status, JIRA, "Contact Management");
            }
        }
    }
}
