using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class ContactEaddressLabelAndTypeIssue : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin")]
        public void contactEaddressLabelAndTypeIssue()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            //Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var office_ContactsHelper = new Office_ContactsHelper(GetWebDriver());

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Variable
            var FName = "Fname" + RandomNumber(1, 999);
            var LName = "Lname" + RandomNumber(1, 999);
            var CDBA = "Comp" + RandomNumber(99, 99999);

            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("ContactPrimaryPhoneAndExistingOverlay", "Login with valid credential  Username");
                Login(username[0], password[0]);

                executionLog.Log("ContactPrimaryPhoneAndExistingOverlay", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("ContactPrimaryPhoneAndExistingOverlay", "Goto Contact page");
                VisitOffice("contacts");

                executionLog.Log("ContactPrimaryPhoneAndExistingOverlay", "Verify title");
                office_ContactsHelper.VerifyText("VerifyContact", "Contacts");

                executionLog.Log("ContactPrimaryPhoneAndExistingOverlay", "Goto Create Contact");
                VisitOffice("contacts/create");

                executionLog.Log("ContactPrimaryPhoneAndExistingOverlay", "Verify Page title.");
                VerifyTitle("Create a Contact");

                executionLog.Log("ContactPrimaryPhoneAndExistingOverlay", "Enter First Name");
                office_ContactsHelper.TypeText("FirstNAME", FName);

                executionLog.Log("ContactPrimaryPhoneAndExistingOverlay", "Enter Last Name");
                office_ContactsHelper.TypeText("LastName", LName);

                executionLog.Log("ContactPrimaryPhoneAndExistingOverlay", "Enter Company DBA Name");
                office_ContactsHelper.TypeText("CompanyName", CDBA);

                executionLog.Log("ContactPrimaryPhoneAndExistingOverlay", "Select phone type");
                office_ContactsHelper.Select("PhoneType1", "Work");

                executionLog.Log("ContactPrimaryPhoneAndExistingOverlay", "Enter phone number.");
                office_ContactsHelper.TypeText("PhoneNumber1", "1222121211");

                executionLog.Log("ContactPrimaryPhoneAndExistingOverlay", "Enter first email address.");
                office_ContactsHelper.TypeText("Eaddress", "Contact@yopmail.com");

                executionLog.Log("ContactPrimaryPhoneAndExistingOverlay", "Click on add email.");
                office_ContactsHelper.ClickElement("AddEmail");
                office_ContactsHelper.WaitForWorkAround(1000);

                executionLog.Log("ContactPrimaryPhoneAndExistingOverlay", "Select e adress type as email.");
                office_ContactsHelper.Select("EaddressType2", "E-Mail");

                executionLog.Log("ContactPrimaryPhoneAndExistingOverlay", "Enter second email.");
                office_ContactsHelper.TypeText("Eaddress2", "Test@gmail.com");
                office_ContactsHelper.WaitForWorkAround(1000);

                executionLog.Log("ContactPrimaryPhoneAndExistingOverlay", "Click on save button.");
                office_ContactsHelper.ClickElement("SaveContactN");

                // Verifiews if existing overlay present then create duplicate contact.

                var duplicate = "//h3[text()='Existing Contacts']";
                if (office_ContactsHelper.IsElementPresent(duplicate))
                {
                    office_ContactsHelper.WaitForWorkAround(2000);
                    office_ContactsHelper.ClickElement("DuplicateRadio");
                    office_ContactsHelper.WaitForWorkAround(2000);

                    office_ContactsHelper.ClickElement("ClickOnDuplicate");
                    office_ContactsHelper.WaitForWorkAround(2000);

                }

                executionLog.Log("ContactPrimaryPhoneAndExistingOverlay", "Wait for success message.");
                //            office_ContactsHelper.WaitForText("A Contact has been created. . ", 10);

                executionLog.Log("ContactPrimaryPhoneAndExistingOverlay", "Redirect at contacts page.");
                VisitOffice("contacts");

                executionLog.Log("ContactPrimaryPhoneAndExistingOverlay", "Search created contact by name.");
                office_ContactsHelper.TypeText("SearchCompany", CDBA);
                office_ContactsHelper.WaitForWorkAround(2000);

                executionLog.Log("ContactPrimaryPhoneAndExistingOverlay", "Click on edit icon.");
                office_ContactsHelper.ClickElement("EditContactNewSkin");
                office_ContactsHelper.WaitForWorkAround(2000);

                executionLog.Log("ContactPrimaryPhoneAndExistingOverlay", "Verify page title.");
                VerifyTitle("Edit Contact");
                office_ContactsHelper.WaitForWorkAround(2000);

                executionLog.Log("ContactPrimaryPhoneAndExistingOverlay", "Verify dropdown label not changed.");
                office_ContactsHelper.VerifyDropdownLabel();
                office_ContactsHelper.WaitForWorkAround(4000);

                executionLog.Log("ContactPrimaryPhoneAndExistingOverlay", "Verify dropdown type not changed.");
                office_ContactsHelper.VerifyDropdownType();
                office_ContactsHelper.WaitForWorkAround(2000);

                executionLog.Log("ContactPrimaryPhoneAndExistingOverlay", "Click on save button.");
                office_ContactsHelper.ClickElement("SaveContactN");

                executionLog.Log("ContactPrimaryPhoneAndExistingOverlay", "Wait for success message.");
                //            office_ContactsHelper.WaitForText("A Contact has been updated.", 10);

                executionLog.Log("ContactPrimaryPhoneAndExistingOverlay", "Redirect at contacts page.");
                VisitOffice("contacts");
                office_ContactsHelper.WaitForWorkAround(2000);

                executionLog.Log("ContactPrimaryPhoneAndExistingOverlay", "Wait for locator to present.");
                office_ContactsHelper.WaitForElementPresent("SearchCompany", 10);
                office_ContactsHelper.WaitForWorkAround(2000);

                executionLog.Log("ContactPrimaryPhoneAndExistingOverlay", "Enter company name to searched.");
                office_ContactsHelper.TypeText("SearchCompany", CDBA);
                office_ContactsHelper.WaitForWorkAround(2000);

                executionLog.Log("ContactPrimaryPhoneAndExistingOverlay", "Select first contact.");
                office_ContactsHelper.ClickElement("CheckTheFirstContact");
                office_ContactsHelper.WaitForWorkAround(2000);

                executionLog.Log("ContactPrimaryPhoneAndExistingOverlay", "Click on delete icon.");
                office_ContactsHelper.ClickElement("ClickOnDeleteIcon");
                office_ContactsHelper.WaitForWorkAround(2000);

                executionLog.Log("ContactPrimaryPhoneAndExistingOverlay", "Accept alert message.");
                office_ContactsHelper.AcceptAlert();
                office_ContactsHelper.WaitForWorkAround(2000);

                executionLog.Log("ContactPrimaryPhoneAndExistingOverlay", "Wait for success message.");
                //         office_ContactsHelper.WaitForText("1 records deleted successfully", 10);
                office_ContactsHelper.WaitForWorkAround(2000);

                executionLog.Log("ContactPrimaryPhoneAndExistingOverlay", "Redirect at recyclebin page");
                VisitOffice("contacts/recyclebin");
                office_ContactsHelper.WaitForWorkAround(2000);

                executionLog.Log("ContactPrimaryPhoneAndExistingOverlay", "Verify page title.");
                VerifyTitle("Recycled Contacts");
                office_ContactsHelper.WaitForWorkAround(2000);

                executionLog.Log("ContactPrimaryPhoneAndExistingOverlay", "Enter company name to be searched.");
                //       office_ContactsHelper.TypeText("SearchCompany", CDBA);
                office_ContactsHelper.WaitForWorkAround(2000);

                executionLog.Log("ContactPrimaryPhoneAndExistingOverlay", "Click on delete icon.");
                office_ContactsHelper.ClickElement("ClickOnDeleteIconRCBin");
                office_ContactsHelper.WaitForWorkAround(2000);

                executionLog.Log("ContactPrimaryPhoneAndExistingOverlay", "Acccept alert message");
                office_ContactsHelper.AcceptAlert();
                office_ContactsHelper.WaitForWorkAround(2000);

                executionLog.Log("ContactPrimaryPhoneAndExistingOverlay", "wait for successa message.");
                //           office_ContactsHelper.WaitForText("Contact Permanently Deleted.", 10);
                office_ContactsHelper.WaitForWorkAround(2000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("ContactPrimaryPhoneAndExistingOverlay");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("ContactPrimaryPhoneAndExistingOverlay");
                if (!result)
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("ContactPrimaryPhoneAndExistingOverlay", "Bug", "Medium", "Contacts page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("ContactPrimaryPhoneAndExistingOverlay");
                        TakeScreenshot("ContactPrimaryPhoneAndExistingOverlay");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\Contact.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("ContactPrimaryPhoneAndExistingOverlay");
                        string id = loginHelper.getIssueID("ContactPrimaryPhoneAndExistingOverlay");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\Contact.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("ContactPrimaryPhoneAndExistingOverlay"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("ContactPrimaryPhoneAndExistingOverlay");
                executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("ContactPrimaryPhoneAndExistingOverlay");
                executionLog.WriteInExcel("ContactPrimaryPhoneAndExistingOverlay", Status, JIRA, "Contact Management");
            }
        }
    }
}
