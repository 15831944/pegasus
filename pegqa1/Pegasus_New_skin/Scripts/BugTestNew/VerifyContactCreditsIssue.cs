using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class VerifyContactCreditsIssue : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin")]
        [TestCategory("TS3")]
        [TestCategory("BugTestNew")]
        public void verifyContactCreditsIssue()
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

            executionLog.Log("VerifyContactCreditsIssue", "Login with valid credential  Username");
            Login(username[0], password[0]);

            executionLog.Log("VerifyContactCreditsIssue", "Verify Page title");
            VerifyTitle("Dashboard");
            Console.WriteLine("Redirected at Dashboard screen.");

            executionLog.Log("VerifyContactCreditsIssue", "Goto Contact");
            VisitOffice("contacts");
            office_ContactsHelper.WaitForWorkAround(5000);

            executionLog.Log("VerifyContactCreditsIssue", "Verify title");
            office_ContactsHelper.VerifyText("VerifyContact", "Contacts");

            executionLog.Log("VerifyContactCreditsIssue", "Goto Create Contact");
            VisitOffice("contacts/create");

            executionLog.Log("VerifyContactCreditsIssue", "Click on Cancel Button");
            office_ContactsHelper.ClickElement("Cancelbtn");

            executionLog.Log("VerifyContactCreditsIssue", "Verify text on page.");
            office_ContactsHelper.VerifyText("VerifyContact", "Contacts");

            executionLog.Log("VerifyContactCreditsIssue", "Create Contact");
            VisitOffice("contacts/create");

            executionLog.Log("VerifyContactCreditsIssue", "Enter First Name");
            office_ContactsHelper.TypeText("FirstNAME", FName);

            executionLog.Log("VerifyContactCreditsIssue", "Enter Last Name");
            office_ContactsHelper.TypeText("LastName", LName);

            executionLog.Log("VerifyContactCreditsIssue", "Enter Company DBA Name");
            office_ContactsHelper.TypeText("CompanyName", CDBA);

            executionLog.Log("VerifyContactCreditsIssue", "Click on Save Contact");
            office_ContactsHelper.ClickElement("SaveContactN");
            office_ContactsHelper.WaitForWorkAround(3000);

/*
            if (office_ContactsHelper.IsElementPresent("//h3[text()='Existing Contacts']"))
            {

                executionLog.Log("VerifyContactCreditsIssue", "Click on Dublicate button");
                office_ContactsHelper.ClickElement("ClickOnDuplicate");
                office_ContactsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyContactCreditsIssue", "Confirmation");
                office_ContactsHelper.WaitForText("A Contact has been created.", 10);

                executionLog.Log("VerifyContactCreditsIssue", "Redirect at contacts page.");
                VisitOffice("contacts");

                executionLog.Log("VerifyContactCreditsIssue", "Verify page title.");
                VerifyTitle("Contacts");

                executionLog.Log("VerifyContactCreditsIssue", "Search Contact by company name");
                office_ContactsHelper.TypeText("SearchCompany", CDBA);
                office_ContactsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyContactCreditsIssue", "Clcik on searched contact.");
                office_ContactsHelper.ClickElement("Contact1");

                executionLog.Log("VerifyContactCreditsIssue", "Verify contact created by credits.");
                office_ContactsHelper.VerifyText("CreatedBy", "Howard Tang");

                executionLog.Log("VerifyContactCreditsIssue", "Verify contact modified by credits.");
                office_ContactsHelper.VerifyText("ModifiedBy", "Howard Tang");

                executionLog.Log("VerifyContactCreditsIssue", "Goto Contact");
                VisitOffice("contacts");
                office_ContactsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyContactCreditsIssue", "Search Contact by company name");
                office_ContactsHelper.TypeText("SearchCompany", CDBA);
                office_ContactsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyContactCreditsIssue", "Check the first contact checkbox");
                office_ContactsHelper.ClickElement("CheckTheFirstContact");

                executionLog.Log("VerifyContactCreditsIssue", "Click delete icon");
                office_ContactsHelper.ClickElement("ClickOnDeleteIcon");
                office_ContactsHelper.AcceptAlert();

                executionLog.Log("VerifyContactCreditsIssue", "Wait for deleted text message");
                office_ContactsHelper.WaitForText("records deleted successfully", 10);

                executionLog.Log("VerifyContactCreditsIssue", "Redirect to recycled contacts.");
                VisitOffice("contacts/recyclebin");

                executionLog.Log("VerifyContactCreditsIssue", "Click restore link");
                office_ContactsHelper.ClickElement("ClickOnContactRCRestore");
                office_ContactsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyContactCreditsIssue", "Wait for restored message");
                office_ContactsHelper.WaitForText("Contact Restored Successfully.", 10);

                executionLog.Log("VerifyContactCreditsIssue", "Goto Contact");
                VisitOffice("contacts");
                office_ContactsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyContactCreditsIssue", "Search Contact by company name");
                office_ContactsHelper.TypeText("SearchCompany", CDBA);
                office_ContactsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyContactCreditsIssue", "Check the  First Checkbox");
                office_ContactsHelper.ClickElement("CheckTheFirstContact");

                executionLog.Log("VerifyContactCreditsIssue", "Click on Delete Icon");
                office_ContactsHelper.ClickElement("ClickOnDeleteIcon");
                office_ContactsHelper.AcceptAlert();

                executionLog.Log("VerifyContactCreditsIssue", "Confrimation");
                office_ContactsHelper.WaitForText("records deleted successfully", 10);

                executionLog.Log("VerifyContactCreditsIssue", "Goto Contact Recycle Bin");
                VisitOffice("contacts/recyclebin");

                executionLog.Log("VerifyContactCreditsIssue", "Delete Icon");
                office_ContactsHelper.ClickElement("ClickOnDeleteIconRCBin");
                office_ContactsHelper.AcceptAlert();

                executionLog.Log("VerifyContactCreditsIssue", "Confiration");
                office_ContactsHelper.WaitForText("Contact Permanently Deleted.", 10);

            }
            else
            {   */
                executionLog.Log("VerifyContactCreditsIssue", "Wait for save Confirmation");
                office_ContactsHelper.WaitForText("A Contact has been created.", 20);
                office_ContactsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyContactCreditsIssue", "Redirect at contacts page.");
                VisitOffice("contacts");

                executionLog.Log("VerifyContactCreditsIssue", "Verify page title.");
                VerifyTitle("Contacts");

                executionLog.Log("VerifyContactCreditsIssue", "Search Contact by company name");
                office_ContactsHelper.TypeText("SearchCompany", CDBA);
                office_ContactsHelper.WaitForWorkAround(3000);

                office_ContactsHelper.SelectDropDownByText("//select[@name='owner']", "All");
                office_ContactsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyContactCreditsIssue", "Click on searched contact.");
                office_ContactsHelper.ClickElement("Contact1");

                executionLog.Log("VerifyContactCreditsIssue", "Verify contact created by credits.");
                office_ContactsHelper.VerifyText("CreatedBy", "Howard Tang");

                executionLog.Log("VerifyContactCreditsIssue", "Verify contact modified by credits.");
                office_ContactsHelper.VerifyText("ModifiedBy", "Howard Tang");

                executionLog.Log("VerifyContactCreditsIssue", "Goto Contact");
                VisitOffice("contacts");
                office_ContactsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyContactCreditsIssue", "Search Contact by company name");
                office_ContactsHelper.TypeText("SearchCompany", CDBA);
                office_ContactsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyContactCreditsIssue", "Check the first contact checkbox");
                office_ContactsHelper.ClickElement("CheckTheFirstContact");

                executionLog.Log("VerifyContactCreditsIssue", "Click delete icon");
                office_ContactsHelper.ClickElement("ClickOnDeleteIcon");
                office_ContactsHelper.AcceptAlert();

                executionLog.Log("VerifyContactCreditsIssue", "Wait for deleted text message");
                office_ContactsHelper.WaitForText("records deleted successfully", 10);

                executionLog.Log("VerifyContactCreditsIssue", "Redirect to recycled contacts.");
                VisitOffice("contacts/recyclebin");

                executionLog.Log("VerifyContactCreditsIssue", "Click restore link");
                office_ContactsHelper.ClickElement("ClickOnContactRCRestore");
                office_ContactsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyContactCreditsIssue", "Wait for restored message");
                office_ContactsHelper.WaitForText("Contact Restored Successfully.", 10);

                executionLog.Log("VerifyContactCreditsIssue", "Goto Contact");
                VisitOffice("contacts");
                office_ContactsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyContactCreditsIssue", "Search Contact by company name");
                office_ContactsHelper.TypeText("SearchCompany", CDBA);
                office_ContactsHelper.WaitForWorkAround(5000);

                executionLog.Log("VerifyContactCreditsIssue", "Check the  First Checkbox");
                office_ContactsHelper.ClickElement("CheckTheFirstContact");

                executionLog.Log("VerifyContactCreditsIssue", "Click on Delete Icon");
                office_ContactsHelper.ClickElement("ClickOnDeleteIcon");
                office_ContactsHelper.AcceptAlert();

                executionLog.Log("VerifyContactCreditsIssue", "Confrimation");
                office_ContactsHelper.WaitForText("records deleted successfully", 10);

                executionLog.Log("VerifyContactCreditsIssue", "Goto Contact Recycle Bin");
                VisitOffice("contacts/recyclebin");

                executionLog.Log("VerifyContactCreditsIssue", "Delete Icon");
                office_ContactsHelper.ClickElement("ClickOnDeleteIconRCBin");
                office_ContactsHelper.AcceptAlert();

                executionLog.Log("VerifyContactCreditsIssue", "Confiration");
                office_ContactsHelper.WaitForText("Contact Permanently Deleted.", 10);

            }
         catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyContactCreditsIssue");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("VerifyContactCreditsIssue");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("VerifyContactCreditsIssue", "Bug", "Medium", "Contacts page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("VerifyContactCreditsIssue");
                        TakeScreenshot("VerifyContactCreditsIssue");
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
                        TakeScreenshot("VerifyContactCreditsIssue");
                        string id = loginHelper.getIssueID("VerifyContactCreditsIssue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\Contact.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("VerifyContactCreditsIssue"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("VerifyContactCreditsIssue");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyContactCreditsIssue");
                executionLog.WriteInExcel("VerifyContactCreditsIssue", Status, JIRA, "Contact Management");
            }
        }
    }
} 