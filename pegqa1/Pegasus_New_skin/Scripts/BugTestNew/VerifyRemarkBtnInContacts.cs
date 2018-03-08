using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class VerifyRemarkBtnInContacts : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("BugTestNew")]
        public void verifyRemarkBtnInContacts()
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

            //try
            //{

                executionLog.Log("VerifyRemarkBtnInContacts", "Login with valid credentials");
                Login(username[0], password[0]);

                executionLog.Log("VerifyRemarkBtnInContacts", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyRemarkBtnInContacts", "Redirect to contact page.");
                VisitOffice("contacts");

                executionLog.Log("VerifyRemarkBtnInContacts", "Verify title");
                office_ContactsHelper.VerifyText("VerifyContact", "Contacts");

                executionLog.Log("VerifyRemarkBtnInContacts", "Reditect to create contact page.");
                VisitOffice("contacts/create");

                executionLog.Log("VerifyRemarkBtnInContacts", "First Name");
                office_ContactsHelper.TypeText("FirstNAME", FName);

                executionLog.Log("VerifyRemarkBtnInContacts", "Last Name");
                office_ContactsHelper.TypeText("LastName", LName);

                executionLog.Log("VerifyRemarkBtnInContacts", "Company DBA Name");
                office_ContactsHelper.TypeText("CompanyName", CDBA);

                executionLog.Log("VerifyRemarkBtnInContacts", "Click on Add Remark button");
                office_ContactsHelper.ClickElement("AddRemarkPhnBtn1");
                office_ContactsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyRemarkBtnInContacts", "Enter Remark");
                office_ContactsHelper.TypeText("AddRemarkPhnArea1", "THIS IS PHONE REMARK");

                executionLog.Log("VerifyRemarkBtnInContacts", "Click Add Remark button");
                office_ContactsHelper.ClickElement("AddRemarkEmailBtn1");
                office_ContactsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyRemarkBtnInContacts", "Enter Remark");
                office_ContactsHelper.TypeText("AddRemarkEmailArea1", "THIS IS EMAIL REMARK");

                executionLog.Log("VerifyRemarkBtnInContacts", "Click on Save button");
                office_ContactsHelper.ClickElement("SaveContactN");
                office_ContactsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyRemarkBtnInContacts", "Enter Contact Name in Search Box");
                office_ContactsHelper.TypeText("SearchName", FName);
                office_ContactsHelper.WaitForWorkAround(4000);

                executionLog.Log("VerifyRemarkBtnInContacts", "Click on Edit icon");
                office_ContactsHelper.ClickElement("FirstEditIcon");
                office_ContactsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyRemarkBtnInContacts", "Verify Show Remark text for Phone");
                office_ContactsHelper.VerifyText("AddRemarkPhnBtn1", "Show remark");
                Console.WriteLine("Show Remark is appearing for Phone");

                executionLog.Log("VerifyRemarkBtnInContacts", "Verify Show Remark text for Email");
                office_ContactsHelper.VerifyText("AddRemarkEmailBtn1", "Show remark");
                Console.WriteLine("Show Remark is appearing for Email");

                executionLog.Log("VerifyRemarkBtnInContacts", "Redirect to contact page.");
                VisitOffice("contacts");

                executionLog.Log("VerifyRemarkBtnInContacts", "Enter Contact Name in Search Box");
                office_ContactsHelper.TypeText("SearchName", FName);
                office_ContactsHelper.WaitForWorkAround(4000);

                executionLog.Log("VerifyRemarkBtnInContacts", "Click on check box");
                office_ContactsHelper.ClickElement("SelectIstContact");

                executionLog.Log("VerifyRemarkBtnInContacts", "Click on Delete button");
                office_ContactsHelper.ClickElement("ClickOnDeleteIcon");



                

            //}
            //catch (Exception e)
            //{
                
            //}
            
        }
    }
}
