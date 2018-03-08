using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class VerifyContactCreatedAndModifiedByCredits : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin1")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Admin1")]
        public void verifyContactCreatedAndModifiedByCredits()
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
            var FName = "Test" + RandomNumber(99, 99999);
            var LName = "Test" + RandomNumber(99, 99999);
            var CDBA = "New" + RandomNumber(99, 99999);
            var ContactImport = GetPathToFile() + "contactsamples.csv";

            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("VerifyContactCreatedAndModifiedByCredits", "Login with valid credential  Username");
                Login(username[0], password[0]);

                executionLog.Log("VerifyContactCreatedAndModifiedByCredits", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyContactCreatedAndModifiedByCredits", "Goto Contact");
                VisitOffice("contacts");

                executionLog.Log("VerifyContactCreatedAndModifiedByCredits", "Verify title");
                office_ContactsHelper.VerifyText("VerifyContact", "Contacts");

                executionLog.Log("VerifyContactCreatedAndModifiedByCredits", "Goto Create Contact");
                VisitOffice("contacts/create");

                executionLog.Log("VerifyContactCreatedAndModifiedByCredits", "Click on Cancel Button");
                office_ContactsHelper.ClickElement("Cancelbtn");

                executionLog.Log("VerifyContactCreatedAndModifiedByCredits", "Verify text on page.");
                office_ContactsHelper.VerifyText("VerifyContact", "Contacts");

                executionLog.Log("VerifyContactCreatedAndModifiedByCredits", "Create Contact");
                VisitOffice("contacts/create");

                executionLog.Log("VerifyContactCreatedAndModifiedByCredits", "Enter First Name");
                office_ContactsHelper.TypeText("FirstNAME", FName);

                executionLog.Log("VerifyContactCreatedAndModifiedByCredits", "Enter Last Name");
                office_ContactsHelper.TypeText("LastName", LName);

                executionLog.Log("VerifyContactCreatedAndModifiedByCredits", "Enter Company DBA Name");
                office_ContactsHelper.TypeText("CompanyName", CDBA);

                executionLog.Log("VerifyContactCreatedAndModifiedByCredits", "Click on Save Contact");
                office_ContactsHelper.ClickElement("SaveContactN");
                office_ContactsHelper.WaitForWorkAround(3000);

                if (office_ContactsHelper.IsElementPresent("//h3[text()='Existing Contacts']"))
                {

                    executionLog.Log("VerifyContactCreatedAndModifiedByCredits", "Click on Dublicate button");
                    office_ContactsHelper.ClickElement("ClickOnDuplicate");
                    office_ContactsHelper.WaitForWorkAround(3000);

                    executionLog.Log("VerifyContactCreatedAndModifiedByCredits", "Confirmation");
                    office_ContactsHelper.WaitForText("A Contact has been created.", 10);

                    executionLog.Log("VerifyContactCreatedAndModifiedByCredits", "Redirect at contacts page.");
                    VisitOffice("contacts");

                    executionLog.Log("VerifyContactCreatedAndModifiedByCredits", "Verify page title.");
                    VerifyTitle("Contacts");

                    executionLog.Log("VerifyContactCreatedAndModifiedByCredits", "Search Contact by company name");
                    office_ContactsHelper.TypeText("SearchCompany", CDBA);
                    office_ContactsHelper.WaitForWorkAround(3000);

                    executionLog.Log("VerifyContactCreatedAndModifiedByCredits", "Select All in resposibility field");
                    office_ContactsHelper.selectByText("ResponsibilityField", "All");
                    office_ContactsHelper.WaitForWorkAround(3000);

                    executionLog.Log("VerifyContactCreatedAndModifiedByCredits", "Clcik on searched contact.");
                    office_ContactsHelper.ClickElement("Contact1");

                    executionLog.Log("VerifyContactCreatedAndModifiedByCredits", "Verify contact created by credits.");
                    office_ContactsHelper.VerifyText("CreatedBy", "Howard Tang");

                    executionLog.Log("VerifyContactCreatedAndModifiedByCredits", "Verify contact modified by credits.");
                    office_ContactsHelper.VerifyText("ModifiedBy", "Howard Tang");

                    executionLog.Log("VerifyContactCreatedAndModifiedByCredits", "Click on edit button.");
                    office_ContactsHelper.ClickElement("EditLink");

                    executionLog.Log("VerifyContactCreatedAndModifiedByCredits", "Click on Save button.");
                    office_ContactsHelper.ClickElement("SaveContactN");
                    office_ContactsHelper.WaitForWorkAround(3000);

                    executionLog.Log("VerifyContactCreatedAndModifiedByCredits", "Wait for updation success message.");
                    office_ContactsHelper.WaitForText("Contact has been updated.", 10);

                    executionLog.Log("VerifyContactCreatedAndModifiedByCredits", "Verify contact created by credits.");
                    office_ContactsHelper.VerifyText("CreatedBy", "Howard Tang");

                    executionLog.Log("VerifyContactCreatedAndModifiedByCredits", "Verify contact modified by credits.");
                    office_ContactsHelper.VerifyText("ModifiedBy", "Howard Tang");

                    executionLog.Log("VerifyContactCreatedAndModifiedByCredits", "Goto Contact");
                    VisitOffice("contacts");
                    office_ContactsHelper.WaitForWorkAround(3000);

                    executionLog.Log("VerifyContactCreatedAndModifiedByCredits", "Search Contact by company name");
                    office_ContactsHelper.TypeText("SearchCompany", CDBA);
                    office_ContactsHelper.WaitForWorkAround(3000);

                    executionLog.Log("VerifyContactCreatedAndModifiedByCredits", "Select All in resposibility field");
                    office_ContactsHelper.selectByText("ResponsibilityField", "All");
                    office_ContactsHelper.WaitForWorkAround(3000);

                    executionLog.Log("VerifyContactCreatedAndModifiedByCredits", "Check the first contact checkbox");
                    office_ContactsHelper.ClickElement("Contact1");

                    executionLog.Log("VerifyContactCreatedAndModifiedByCredits", "Click delete icon");
                    office_ContactsHelper.ClickElement("ClickOnDeleteIcon");
                    office_ContactsHelper.AcceptAlert();

                    executionLog.Log("VerifyContactCreatedAndModifiedByCredits", "Wait for deleted text message");
                    office_ContactsHelper.WaitForText("records deleted successfully", 10);

                    executionLog.Log("VerifyContactCreatedAndModifiedByCredits", "Redirect to recycled contacts.");
                    VisitOffice("contacts/recyclebin");

                    executionLog.Log("VerifyContactCreatedAndModifiedByCredits", "Click restore link");
                    office_ContactsHelper.ClickElement("ClickOnContactRCRestore");
                    office_ContactsHelper.WaitForWorkAround(3000);

                    executionLog.Log("VerifyContactCreatedAndModifiedByCredits", "Wait for restored message");
                    office_ContactsHelper.WaitForText("Contact Restored Successfully.", 10);

                    executionLog.Log("VerifyContactCreatedAndModifiedByCredits", "Goto Contact");
                    VisitOffice("contacts");
                    office_ContactsHelper.WaitForWorkAround(3000);

                    executionLog.Log("VerifyContactCreatedAndModifiedByCredits", "Search Contact by company name");
                    office_ContactsHelper.TypeText("SearchCompany", CDBA);
                    office_ContactsHelper.WaitForWorkAround(3000);

                    executionLog.Log("VerifyContactCreatedAndModifiedByCredits", "Select All in resposibility field");
                    office_ContactsHelper.selectByText("ResponsibilityField", "All");
                    office_ContactsHelper.WaitForWorkAround(3000);

                    executionLog.Log("VerifyContactCreatedAndModifiedByCredits", "Check the  First Checkbox");
                    office_ContactsHelper.ClickElement("Contact1");

                    executionLog.Log("VerifyContactCreatedAndModifiedByCredits", "Click on Delete Icon");
                    office_ContactsHelper.ClickElement("ClickOnDeleteIcon");
                    office_ContactsHelper.AcceptAlert();

                    executionLog.Log("VerifyContactCreatedAndModifiedByCredits", "Confrimation");
                    office_ContactsHelper.WaitForText("records deleted successfully", 10);

                    executionLog.Log("VerifyContactCreatedAndModifiedByCredits", "Goto Contact Recycle Bin");
                    VisitOffice("contacts/recyclebin");

                    executionLog.Log("VerifyContactCreatedAndModifiedByCredits", "Delete Icon");
                    office_ContactsHelper.ClickElement("ClickOnDeleteIconRCBin");
                    office_ContactsHelper.AcceptAlert();

                    executionLog.Log("VerifyContactCreatedAndModifiedByCredits", "Confiration");
                    office_ContactsHelper.WaitForText("Contact Permanently Deleted.", 10);

                }
                else
                {
                    executionLog.Log("VerifyContactCreatedAndModifiedByCredits", "Wait for save Confirmation");
                    office_ContactsHelper.WaitForText("A Contact has been created.", 20);
                    office_ContactsHelper.WaitForWorkAround(3000);

                    executionLog.Log("VerifyContactCreatedAndModifiedByCredits", "Redirect at contacts page.");
                    VisitOffice("contacts");
                    office_ContactsHelper.WaitForWorkAround(2000);

                    executionLog.Log("VerifyContactCreatedAndModifiedByCredits", "Verify page title.");
                    VerifyTitle("Contacts");

                    executionLog.Log("VerifyContactCreatedAndModifiedByCredits", "Search Contact by company name");
                    office_ContactsHelper.TypeText("SearchCompany", CDBA);
                    office_ContactsHelper.WaitForWorkAround(3000);

                    executionLog.Log("VerifyContactCreatedAndModifiedByCredits", "Select All in resposibility field");
                    office_ContactsHelper.selectByText("ResponsibilityField", "All");
                    office_ContactsHelper.WaitForWorkAround(3000);

                    executionLog.Log("VerifyContactCreatedAndModifiedByCredits", "Click on searched contact.");
                    office_ContactsHelper.ClickElement("Contact1");

                    executionLog.Log("VerifyContactCreatedAndModifiedByCredits", "Verify contact created by credits.");
                    office_ContactsHelper.VerifyText("CreatedBy", "Howard Tang");

                    executionLog.Log("VerifyContactCreatedAndModifiedByCredits", "Verify contact modified by credits.");
                    office_ContactsHelper.VerifyText("ModifiedBy", "Howard Tang");

                    executionLog.Log("VerifyContactCreatedAndModifiedByCredits", "Click on edit button.");
                    office_ContactsHelper.ClickElement("EditLink");

                    executionLog.Log("VerifyContactCreatedAndModifiedByCredits", "Click on Save button.");
                    office_ContactsHelper.ClickElement("SaveContactN");
                    office_ContactsHelper.WaitForWorkAround(3000);

                    executionLog.Log("VerifyContactCreatedAndModifiedByCredits", "Wait for updation success message.");
                    office_ContactsHelper.WaitForText("Contact has been updated.", 10);

                    executionLog.Log("VerifyContactCreatedAndModifiedByCredits", "Verify contact created by credits.");
                    office_ContactsHelper.VerifyText("CreatedBy", "Howard Tang");

                    executionLog.Log("VerifyContactCreatedAndModifiedByCredits", "Verify contact modified by credits.");
                    office_ContactsHelper.VerifyText("ModifiedBy", "Howard Tang");

                    executionLog.Log("VerifyContactCreatedAndModifiedByCredits", "Goto Contact");
                    VisitOffice("contacts");
                    office_ContactsHelper.WaitForWorkAround(3000);

                    executionLog.Log("VerifyContactCreatedAndModifiedByCredits", "Search Contact by company name");
                    office_ContactsHelper.TypeText("SearchCompany", CDBA);
                    office_ContactsHelper.WaitForWorkAround(3000);

                    executionLog.Log("VerifyContactCreatedAndModifiedByCredits", "Select All in resposibility field");
                    office_ContactsHelper.selectByText("ResponsibilityField", "All");
                    office_ContactsHelper.WaitForWorkAround(3000);

                    executionLog.Log("VerifyContactCreatedAndModifiedByCredits", "Check the first contact checkbox");
                    office_ContactsHelper.ClickElement("Contact1");

                    executionLog.Log("VerifyContactCreatedAndModifiedByCredits", "Click delete icon");
                    office_ContactsHelper.ClickJs("ClickOnDeleteIcon");
                    office_ContactsHelper.AcceptAlert();
                    office_ContactsHelper.WaitForWorkAround(5000);

                    executionLog.Log("VerifyContactCreatedAndModifiedByCredits", "Wait for deleted text message");
                    office_ContactsHelper.WaitForText("Contact deleted Successfully.", 10);

                    executionLog.Log("VerifyContactCreatedAndModifiedByCredits", "Redirect to recycled contacts.");
                    VisitOffice("contacts/recyclebin");
                    office_ContactsHelper.WaitForWorkAround(5000);

                    executionLog.Log("VerifyContactCreatedAndModifiedByCredits", "Click restore link");
                    office_ContactsHelper.ClickElement("ClickOnContactRCRestore");
                    office_ContactsHelper.WaitForWorkAround(3000);

                    executionLog.Log("VerifyContactCreatedAndModifiedByCredits", "Wait for restored message");
                    office_ContactsHelper.WaitForText("Contact Restored Successfully.", 10);

                    executionLog.Log("VerifyContactCreatedAndModifiedByCredits", "Goto Contact");
                    VisitOffice("contacts");
                    office_ContactsHelper.WaitForWorkAround(3000);

                    executionLog.Log("VerifyContactCreatedAndModifiedByCredits", "Search Contact by company name");
                    office_ContactsHelper.TypeText("SearchCompany", CDBA);
                    office_ContactsHelper.WaitForWorkAround(3000);

                    executionLog.Log("VerifyContactCreatedAndModifiedByCredits", "Select All in resposibility field");
                    office_ContactsHelper.selectByText("ResponsibilityField", "All");
                    office_ContactsHelper.WaitForWorkAround(3000);

                    executionLog.Log("VerifyContactCreatedAndModifiedByCredits", "Check the  First Checkbox");
                    office_ContactsHelper.ClickElement("Contact1");

                    executionLog.Log("VerifyContactCreatedAndModifiedByCredits", "Click on Delete Icon");
                    office_ContactsHelper.ClickElement("ClickOnDeleteIcon");
                    office_ContactsHelper.AcceptAlert();

                    executionLog.Log("VerifyContactCreatedAndModifiedByCredits", "Confrimation");
                    office_ContactsHelper.WaitForText("Contact deleted Successfully.", 10);

                    executionLog.Log("VerifyContactCreatedAndModifiedByCredits", "Goto Contact Recycle Bin");
                    VisitOffice("contacts/recyclebin");

                    executionLog.Log("VerifyContactCreatedAndModifiedByCredits", "Delete Icon");
                    office_ContactsHelper.ClickElement("ClickOnDeleteIconRCBin");
                    office_ContactsHelper.AcceptAlert();

                    executionLog.Log("VerifyContactCreatedAndModifiedByCredits", "Confiration");
                    office_ContactsHelper.WaitForText("Contact Permanently Deleted.", 10);

                }
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyContactCreatedAndModifiedByCredits");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("VerifyContactCreatedAndModifiedByCredits");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("VerifyContactCreatedAndModifiedByCredits", "Bug", "Medium", "Contacts page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("VerifyContactCreatedAndModifiedByCredits");
                        TakeScreenshot("VerifyContactCreatedAndModifiedByCredits");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\Contact.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyContactCreatedAndModifiedByCredits");
                        string id = loginHelper.getIssueID("VerifyContactCreatedAndModifiedByCredits");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\Contact.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("VerifyContactCreatedAndModifiedByCredits"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("VerifyContactCreatedAndModifiedByCredits");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyContactCreatedAndModifiedByCredits");
                executionLog.WriteInExcel("VerifyContactCreatedAndModifiedByCredits", Status, JIRA, "Contact Management");
            }
        }
    }
}