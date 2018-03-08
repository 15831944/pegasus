using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class ContactPrimaryPhoneAndExistingOverlay : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin")]
        public void contactPrimaryPhoneAndExistingOverlay()
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

            executionLog.Log("ContactPrimaryPhoneAndExistingOverlay", "Goto Contact");
            VisitOffice("contacts");

            executionLog.Log("ContactPrimaryPhoneAndExistingOverlay", "Verify title");
            office_ContactsHelper.VerifyText("VerifyContact", "Contacts");

            executionLog.Log("ContactPrimaryPhoneAndExistingOverlay", "Goto Create Contact");
            VisitOffice("contacts/create");

            executionLog.Log("ContactPrimaryPhoneAndExistingOverlay", "Click on Cancel Button");
            office_ContactsHelper.ClickElement("Cancelbtn");

            executionLog.Log("ContactPrimaryPhoneAndExistingOverlay", "Verify text on page.");
            office_ContactsHelper.VerifyText("VerifyContact", "Contacts");

            executionLog.Log("ContactPrimaryPhoneAndExistingOverlay", "Redirect at create contacts page.");
            VisitOffice("contacts/create");

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
          
            executionLog.Log("ContactPrimaryPhoneAndExistingOverlay", "Click on add phone.");
            office_ContactsHelper.ClickElement("AddPhone");
            office_ContactsHelper.WaitForWorkAround(1000);

            executionLog.Log("ContactPrimaryPhoneAndExistingOverlay", "Select phone type.");
            office_ContactsHelper.Select("PhoneType2", "Home");

            executionLog.Log("ContactPrimaryPhoneAndExistingOverlay", "Enter phone number.");
            office_ContactsHelper.TypeText("PhoneNumber2", "1222121211");
            office_ContactsHelper.WaitForWorkAround(1000);

            executionLog.Log("ContactPrimaryPhoneAndExistingOverlay", "Click on radio button primary.");
            office_ContactsHelper.ClickElement("PrimaryRadio2");
            office_ContactsHelper.WaitForWorkAround(1000);

            executionLog.Log("ContactPrimaryPhoneAndExistingOverlay","Click on remove icon to remove phone.");
            office_ContactsHelper.ClickElement("RemovePhone");
            office_ContactsHelper.WaitForWorkAround(1000);

            executionLog.Log("ContactPrimaryPhoneAndExistingOverlay", "Click on Save button.");
            office_ContactsHelper.ClickElement("SaveContactN");

            executionLog.Log("ContactPrimaryPhoneAndExistingOverlay", "Verify alert for primary phone");
            office_ContactsHelper.VerifyAlertText("please select a primary phone");
            office_ContactsHelper.WaitForWorkAround(1000);

            executionLog.Log("ContactPrimaryPhoneAndExistingOverlay", "Accept alert message.");
            office_ContactsHelper.AcceptAlert();
         
            executionLog.Log("ContactPrimaryPhoneAndExistingOverlay", "Verify that no exisitng contact overlay present.");
            Assert.IsFalse(office_ContactsHelper.IsElementPresent("ExistingContactOverlay"));

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
