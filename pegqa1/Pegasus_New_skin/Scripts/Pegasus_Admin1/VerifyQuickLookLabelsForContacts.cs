using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class VerifyQuickLookLabelsForContacts : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin1")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Admin1")]
        public void verifyQuickLookLabelsForContacts()
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

            executionLog.Log("VerifyQuickLookLabelsForContacts", "Login with valid credential  Username");
            Login(username[0], password[0]);

            executionLog.Log("VerifyQuickLookLabelsForContacts", "Verify Page title");
            VerifyTitle("Dashboard");
            Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyQuickLookLabelsForContacts", "Goto Create Contact");
            VisitOffice("contacts/create");
                office_ContactsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyQuickLookLabelsForContacts", "Click on Cancel Button");
            office_ContactsHelper.ClickElement("Cancelbtn");
                office_ContactsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuickLookLabelsForContacts", "Verify text on page.");
            office_ContactsHelper.VerifyText("VerifyContact", "Contacts");
                //office_ContactsHelper.WaitForWorkAround(3000);


                executionLog.Log("VerifyQuickLookLabelsForContacts", "Redirect at create contacts page.");
            VisitOffice("contacts/create");
                office_ContactsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyQuickLookLabelsForContacts", "Enter First name.");
            office_ContactsHelper.TypeText("FirstNAME", FName);

            executionLog.Log("VerifyQuickLookLabelsForContacts", "Enter Last Name");
            office_ContactsHelper.TypeText("LastName", LName);

            executionLog.Log("VerifyQuickLookLabelsForContacts", "Enter Company DBA Name");
            office_ContactsHelper.TypeText("CompanyName", CDBA);

            executionLog.Log("VerifyQuickLookLabelsForContacts", "Click on Save Contact");
            office_ContactsHelper.ClickElement("SaveContactN");
                office_ContactsHelper.WaitForWorkAround(3000);

            //    executionLog.Log("VerifyQuickLookLabelsForContacts", "Verify Confirmation");
            //office_ContactsHelper.WaitForText("A Contact has been created.", 30);

            executionLog.Log("VerifyQuickLookLabelsForContacts", "Click on any contact.");
            office_ContactsHelper.ClickElement("Contact1");
            office_ContactsHelper.WaitForWorkAround(3000);

            executionLog.Log("VerifyQuickLookLabelsForContacts", "Verify label for contact type.");
            office_ContactsHelper.VerifyText("Type", "Select Type");
            //office_ContactsHelper.WaitForWorkAround(2000);

            executionLog.Log("VerifyQuickLookLabelsForContacts", "Verify label for contact Status.");
            office_ContactsHelper.VerifyText("StatusV", "Active");
            //office_ContactsHelper.WaitForWorkAround(2000);

            executionLog.Log("VerifyQuickLookLabelsForContacts", "Verify label for contact source.");
            office_ContactsHelper.VerifyText("Source", "Select Source");
            //office_ContactsHelper.WaitForWorkAround(2000);

            executionLog.Log("VerifyQuickLookLabelsForContacts", "Verify label for contact category.");
            office_ContactsHelper.VerifyText("CategoryLabel", "Select Category");
            //office_ContactsHelper.WaitForWorkAround(2000);

            executionLog.Log("VerifyQuickLookLabelsForContacts", "Verify label for contact responsibility.");
            office_ContactsHelper.VerifyText("ResponsibilityV", "Howard Tang");
            //office_ContactsHelper.WaitForWorkAround(2000);

            executionLog.Log("VerifyQuickLookLabelsForContacts", "Verify label for contact sales manager.");
            office_ContactsHelper.VerifyText("SalesManagerLabel", "Select Sales Manager");
            //office_ContactsHelper.WaitForWorkAround(2000);

            executionLog.Log("VerifyQuickLookLabelsForContacts", "Click on edit button.");
            office_ContactsHelper.ClickElement("EditLink");
            office_ContactsHelper.WaitForWorkAround(3000);

            executionLog.Log("VerifyQuickLookLabelsForContacts", "Verify page title as edit contact");
            VerifyTitle("Edit Contact");
            office_ContactsHelper.WaitForWorkAround(2000);

            executionLog.Log("VerifyQuickLookLabelsForContacts", "Select contact type.");
            office_ContactsHelper.selectByText("SelectType", "Client Owner");
            //office_ContactsHelper.WaitForWorkAround(2000);

            executionLog.Log("VerifyQuickLookLabelsForContacts", "Select contact source.");
            office_ContactsHelper.selectByText("SelectSource", "Web Site");
            //office_ContactsHelper.WaitForWorkAround(2000);

            executionLog.Log("VerifyQuickLookLabelsForContacts", "Select contact category.");
            office_ContactsHelper.selectByText("SelectCategory", "Personal");
            //office_ContactsHelper.WaitForWorkAround(2000);

            executionLog.Log("VerifyQuickLookLabelsForContacts", "Select contact responsibility.");
            office_ContactsHelper.selectByText("Responsibility", "Howard Tang");
            //office_ContactsHelper.WaitForWorkAround(2000);

            executionLog.Log("VerifyQuickLookLabelsForContacts", "Select contact sales manager.");
            office_ContactsHelper.selectByText("SalesManager", "Howard Tang");
            //office_ContactsHelper.WaitForWorkAround(2000);

            executionLog.Log("VerifyQuickLookLabelsForContacts", "Click on save contact.");
            office_ContactsHelper.ClickElement("SaveContactN");
            office_ContactsHelper.WaitForWorkAround(3000);

            executionLog.Log("VerifyQuickLookLabelsForContacts", "Verify title as contacts.");
            VerifyTitle("Contacts");
                //office_ContactsHelper.WaitForWorkAround(3000);


                executionLog.Log("VerifyQuickLookLabelsForContacts", "Verify contact label for type.");
            office_ContactsHelper.VerifyText("Type", "Client Owner");
            //office_ContactsHelper.WaitForWorkAround(2000);

            executionLog.Log("VerifyQuickLookLabelsForContacts", "Verify contact label for status.");
            office_ContactsHelper.VerifyText("StatusV", "Active");
            //office_ContactsHelper.WaitForWorkAround(2000);

            executionLog.Log("VerifyQuickLookLabelsForContacts", "Verify contact label for source.");
            office_ContactsHelper.VerifyText("Source", "Web Site");
            //office_ContactsHelper.WaitForWorkAround(2000);

            executionLog.Log("VerifyQuickLookLabelsForContacts", "Verify contact label for category.");
            office_ContactsHelper.VerifyText("CategoryLabel", "Personal");
            //office_ContactsHelper.WaitForWorkAround(2000);

            executionLog.Log("VerifyQuickLookLabelsForContacts", "Verify contact label for responsibility.");
            office_ContactsHelper.VerifyText("ResponsibilityV", "Howard Tang");
            //office_ContactsHelper.WaitForWorkAround(2000);

            executionLog.Log("VerifyQuickLookLabelsForContacts", "Verify contact label for sales manager.");
            office_ContactsHelper.VerifyText("SalesManagerLabel", "Howard Tang");
            //office_ContactsHelper.WaitForWorkAround(3000);

            executionLog.Log("VerifyQuickLookLabelsForContacts", "Goto Contact");
            VisitOffice("contacts");
            office_ContactsHelper.WaitForWorkAround(3000);

            executionLog.Log("VerifyQuickLookLabelsForContacts", "Search Contact by company name");
            office_ContactsHelper.TypeText("SearchCompany", CDBA);
            office_ContactsHelper.WaitForWorkAround(2000);

            executionLog.Log("VerifyQuickLookLabelsForContacts", "Select 'All' in Responsbitiy field");
            office_ContactsHelper.SelectByText("ResponsibiltyField", "All");
                office_ContactsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuickLookLabelsForContacts", "Check the  First Checkbox");
            office_ContactsHelper.ClickElement("CheckTheFirstContact");
                //office_ContactsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyQuickLookLabelsForContacts", "Click on Delete Icon");
            office_ContactsHelper.ClickElement("ClickOnDeleteIcon");
            office_ContactsHelper.AcceptAlert();
                //office_ContactsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyQuickLookLabelsForContacts", "Confrimation");
            office_ContactsHelper.WaitForText("records deleted successfully", 10);

            executionLog.Log("VerifyQuickLookLabelsForContacts", "Goto Contact Recycle Bin");
            VisitOffice("contacts/recyclebin");

            executionLog.Log("VerifyQuickLookLabelsForContacts", "Delete Icon");
            office_ContactsHelper.ClickElement("ClickOnDeleteIconRCBin");
            office_ContactsHelper.AcceptAlert();

            executionLog.Log("VerifyQuickLookLabelsForContacts", "Confiration");
            office_ContactsHelper.WaitForText("Contact Permanently Deleted.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyQuickLookLabelsForContacts");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("VerifyQuickLookLabelsForContacts");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("VerifyQuickLookLabelsForContacts", "Bug", "Medium", "Contacts page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("VerifyQuickLookLabelsForContacts");
                        TakeScreenshot("VerifyQuickLookLabelsForContacts");
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
                        TakeScreenshot("VerifyQuickLookLabelsForContacts");
                        string id = loginHelper.getIssueID("VerifyQuickLookLabelsForContacts");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\Contact.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("VerifyQuickLookLabelsForContacts"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("VerifyQuickLookLabelsForContacts");
                //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyQuickLookLabelsForContacts");
                executionLog.WriteInExcel("VerifyQuickLookLabelsForContacts", Status, JIRA, "Contact Management");
            }
        }
    }
}

