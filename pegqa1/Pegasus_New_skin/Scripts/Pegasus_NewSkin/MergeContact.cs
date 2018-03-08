using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class MergeContact : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin")]
        [TestCategory("TS8")]
        [TestCategory("Pegasus_NewSkin")]
        public void mergeContact()
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
            var CDBA = "New" + RandomNumber(99, 99999);
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("MergeContact", "Login with valid credential  Username");
                Login(username[0], password[0]);

                executionLog.Log("MergeContact", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("MergeContact", "Goto Contact");
                VisitOffice("contacts");

                executionLog.Log("MergeContact", "Veify Title Contact");
                office_ContactsHelper.VerifyText("VerifyContact", "Contacts");

                executionLog.Log("MergeContact", "Goto Create Contact page.");
                VisitOffice("contacts/create");

                executionLog.Log("MergeContact", "Click on Cancel");
                office_ContactsHelper.ClickElement("Cancelbtn");

                executionLog.Log("MergeContact", "Verify Title Contact");
                office_ContactsHelper.VerifyText("VerifyContact", "Contacts");
                office_ContactsHelper.WaitForWorkAround(1000);

                executionLog.Log("MergeContact", "Create Contact");
                VisitOffice("contacts/create");

                executionLog.Log("MergeContact", "Enter First Name");
                office_ContactsHelper.TypeText("FirstNAME", FName);

                executionLog.Log("MergeContact", "Enter Last Name");
                office_ContactsHelper.TypeText("LastName", LName);

                executionLog.Log("MergeContact", "Enter Company DBA Name");
                office_ContactsHelper.TypeText("CompanyName", CDBA);

                executionLog.Log("MergeContact", "Click Save Button");
                office_ContactsHelper.ClickElement("SaveContactN");

                executionLog.Log("MergeContact", "Wait for Confirmation");
                office_ContactsHelper.WaitForText("A Contact has been created.", 10);

                executionLog.Log("MergeContact", "Goto Create Contact");
                VisitOffice("contacts/create");

                executionLog.Log("MergeContact", "Enter First Name");
                office_ContactsHelper.TypeText("FirstNAME", FName);

                executionLog.Log("MergeContact", "Enter Last Name");
                office_ContactsHelper.TypeText("LastName", LName);

                executionLog.Log("MergeContact", "Enter Company DBA Name");
                office_ContactsHelper.TypeText("CompanyName", CDBA);

                executionLog.Log("MergeContact", "Save Contact");
                office_ContactsHelper.ClickElement("SaveContactN");
                office_ContactsHelper.WaitForWorkAround(3000);

                if (office_ContactsHelper.IsElementPresent("//h3[text()='Existing Contacts']"))
                {

                    executionLog.Log("MergeContact", "Click on Dublicate");
                    office_ContactsHelper.ClickElement("ClickOnDuplicate");

                    executionLog.Log("MergeContact", "Confirmation");
                    office_ContactsHelper.WaitForText("A Contact has been created.", 10);

                    executionLog.Log("MergeContact", "Visit Contact");
                    VisitOffice("contacts");

                    executionLog.Log("MergeContact", "Select 1st contact");
                    office_ContactsHelper.ClickElement("SelectIstContact");
                    office_ContactsHelper.WaitForWorkAround(1000);

                    executionLog.Log("MergeContact", "Select 2nd contact");
                    office_ContactsHelper.ClickElement("Select2ndContact");
                    office_ContactsHelper.WaitForWorkAround(1000);

                    executionLog.Log("MergeContact", "Click on merge records.");
                    office_ContactsHelper.ClickElement("MergeRecordContact");
                    office_ContactsHelper.WaitForWorkAround(2000);

                    executionLog.Log("MergeContact", "Select Primary Contact.");
                    office_ContactsHelper.ClickElement("SelectCompContact");
                    office_ContactsHelper.WaitForWorkAround(2000);

                    executionLog.Log("MergeContact", "Click On Merge");
                    office_ContactsHelper.ClickElement("ClickOnMergeBtn");
                    office_ContactsHelper.AcceptAlert();

                    executionLog.Log("MergeContact", "Wait for success message.");
                    office_ContactsHelper.WaitForText("Merging Contact(s) Completed Successfully.", 10);

                }
                else
                {
                    executionLog.Log("MergeContact", "Wait for Confirmatoion");
                    office_ContactsHelper.WaitForText("A Contact has been created.", 10);

                    executionLog.Log("MergeContact", "Visist Contact");
                    VisitOffice("contacts");

                    executionLog.Log("MergeContact", "Select 1st Contact");
                    office_ContactsHelper.ClickElement("SelectIstContact");

                    executionLog.Log("MergeContact", "Select 2nd Contact");
                    office_ContactsHelper.ClickElement("Select2ndContact");

                    executionLog.Log("MergeContact", "Merge Record Contact");
                    office_ContactsHelper.ClickElement("MergeRecordContact");

                    executionLog.Log("MergeContact", "Select Comp Contact");
                    office_ContactsHelper.ClickElement("SelectCompContact");

                    executionLog.Log("MergeContact", "Click on Merge Button");
                    office_ContactsHelper.ClickElement("ClickOnMergeBtn");
                    office_ContactsHelper.AcceptAlert();

                    executionLog.Log("MergeContact", "Wait for Confirmation");
                    office_ContactsHelper.WaitForText("Merging Contact(s) Completed Successfully.", 10);

                }
            }

            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("MergeContact");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("MergeContact");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("MergeContact", "Bug", "Medium", "Contacts page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("MergeContact");
                        TakeScreenshot("MergeContact");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\Merge.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("MergeContact");
                        string id = loginHelper.getIssueID("MergeContact");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\Merge.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("MergeContact"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("MergeContact");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("MergeContact");
                executionLog.WriteInExcel("MergeContact", Status, JIRA, "Office Contact");
            }
        }
    }
}
