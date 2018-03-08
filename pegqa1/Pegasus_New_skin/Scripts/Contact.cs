using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class Contact : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin")]
        public void contact()
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

            executionLog.Log("Contact", "Login with valid credential  Username");
            Login(username[0], password[0]);

            executionLog.Log("Contact", "Verify Page title");
            VerifyTitle("Dashboard");
            Console.WriteLine("Redirected at Dashboard screen.");

            executionLog.Log("Contact", "Goto Contact");
            VisitOffice("contacts");

            executionLog.Log("Contact", "Verify title");
            office_ContactsHelper.VerifyText("VerifyContact", "Contacts");

            executionLog.Log("Contact", "Goto Create Contact");
            VisitOffice("contacts/create");

            executionLog.Log("Contact", "Click on Cancel Button");
            office_ContactsHelper.ClickElement("Cancelbtn");

            executionLog.Log("Contact", "Verify text on page.");
            office_ContactsHelper.VerifyText("VerifyContact", "Contacts");

            executionLog.Log("Contact", "Create Contact");
            VisitOffice("contacts/create");

            executionLog.Log("Contact", "Enter First Name");
            office_ContactsHelper.TypeText("FirstNAME", FName);

            executionLog.Log("Contact", "Enter Last Name");
            office_ContactsHelper.TypeText("LastName", LName);

            executionLog.Log("Contact", "Enter Company DBA Name");
            office_ContactsHelper.TypeText("CompanyName", CDBA);

            executionLog.Log("Contact", "Click on Save Contact");
            office_ContactsHelper.ClickElement("SaveContactN");

            executionLog.Log("Contact", "Verify Confirmation");
            office_ContactsHelper.WaitForText("A Contact has been created.", 10);

            executionLog.Log("Contact", "Search Contact by company name");
            office_ContactsHelper.TypeText("SearchCompany", CDBA);
            office_ContactsHelper.WaitForWorkAround(3000);

            executionLog.Log("Contact", "Click on Edit");
            office_ContactsHelper.ClickElement("EditContactNewSkin");

            executionLog.Log("Contact", "Click on Save button.");
            office_ContactsHelper.ClickElement("SaveContactN");
            office_ContactsHelper.WaitForWorkAround(3000);

            executionLog.Log("Contact", "Confirmation");
            office_ContactsHelper.WaitForText("Contact has been updated.", 10);

            executionLog.Log("Contact", "Crate contact");
            VisitOffice("contacts/create");

            executionLog.Log("Contact", "First Name");
            office_ContactsHelper.TypeText("FirstNAME", FName);

            executionLog.Log("Contact", "Last NAME");
            office_ContactsHelper.TypeText("LastName", LName);

            executionLog.Log("Contact", "Company DBA Name");
            office_ContactsHelper.TypeText("CompanyName", CDBA);

            executionLog.Log("Contact", "Save Contact");
            office_ContactsHelper.ClickElement("SaveContactN");
            office_ContactsHelper.WaitForWorkAround(3000);

            if (office_ContactsHelper.IsElementPresent("//h3[text()='Existing Contacts']"))
            {

                executionLog.Log("Contact", "Click on Dublicate button");
                office_ContactsHelper.ClickElement("ClickOnDuplicate");
                office_ContactsHelper.WaitForWorkAround(3000);

                executionLog.Log("Contact", "Confirmation");
                office_ContactsHelper.WaitForText("A Contact has been created.", 10);

            }
            else
            {
                executionLog.Log("Contact", "Confirmation");
                office_ContactsHelper.WaitForText("A Contact has been created.", 10);
                office_ContactsHelper.WaitForWorkAround(3000);

            }

            executionLog.Log("Contact", "Import Contact");
            VisitOffice("contacts/import");

            executionLog.Log("Contact", "Upload File");
            office_ContactsHelper.UploadFile("//*[@id='vcard_file']", ContactImport);

            executionLog.Log("Contact", "Click On Import button");
            office_ContactsHelper.ClickElement("ImportBtn");
            office_ContactsHelper.WaitForWorkAround(3000);

            executionLog.Log("Contact", "Confirmation");
            office_ContactsHelper.VerifyText("VerifyDublicateContact", "Duplicate records has been found");
            office_ContactsHelper.WaitForWorkAround(3000);

            executionLog.Log("Contact", "Goto Contact");
            VisitOffice("contacts");
            office_ContactsHelper.WaitForWorkAround(3000);

            executionLog.Log("Contact", "Search Contact by company name");
            office_ContactsHelper.TypeText("SearchCompany", CDBA);
            office_ContactsHelper.WaitForWorkAround(3000);

            executionLog.Log("Contact", "Check the first contact checkbox");
            office_ContactsHelper.ClickElement("CheckTheFirstContact");

            executionLog.Log("Contact", "Click delete icon");
            office_ContactsHelper.ClickElement("ClickOnDeleteIcon");
            office_ContactsHelper.AcceptAlert();

            executionLog.Log("Contact", "Wait for deleted text message");
            office_ContactsHelper.WaitForText("records deleted successfully", 10);

            executionLog.Log("Contact", "Redirect to recycled contacts.");
            VisitOffice("contacts/recyclebin");

            executionLog.Log("Contact", "Click restore link");
            office_ContactsHelper.ClickElement("ClickOnContactRCRestore");
            office_ContactsHelper.WaitForWorkAround(3000);

            executionLog.Log("Contact", "Wait for restored message");
            office_ContactsHelper.WaitForText("Contact Restored Successfully.", 10);

            executionLog.Log("Contact", "Goto Contact");
            VisitOffice("contacts");
            office_ContactsHelper.WaitForWorkAround(3000);

            executionLog.Log("Contact", "Search Contact by company name");
            office_ContactsHelper.TypeText("SearchCompany", CDBA);
            office_ContactsHelper.WaitForWorkAround(3000);

            executionLog.Log("Contact", "Check the  First Checkbox");
            office_ContactsHelper.ClickElement("CheckTheFirstContact");

            executionLog.Log("Contact", "Click on Delete Icon");
            office_ContactsHelper.ClickElement("ClickOnDeleteIcon");
            office_ContactsHelper.AcceptAlert();

            executionLog.Log("Contact", "Confrimation");
            office_ContactsHelper.WaitForText("records deleted successfully", 10);

            executionLog.Log("Contact", "Goto Contact Recycle Bin");
            VisitOffice("contacts/recyclebin");

            executionLog.Log("Contact", "Delete Icon");
            office_ContactsHelper.ClickElement("ClickOnDeleteIconRCBin");
            office_ContactsHelper.AcceptAlert();

            executionLog.Log("Contact", "Confiration");
            office_ContactsHelper.WaitForText("Contact Permanently Deleted.", 10);

        }
         catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("Contact");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Contact");
                if (!result)
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Contact", "Bug", "Medium", "Contacts page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Contact");
                        TakeScreenshot("Contact");
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
                        TakeScreenshot("Contact");
                        string id = loginHelper.getIssueID("Contact");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\Contact.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Contact"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Contact");
                executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("Contact");
                executionLog.WriteInExcel("Contact", Status, JIRA, "Contact Management");
            }
        }
    }
}
