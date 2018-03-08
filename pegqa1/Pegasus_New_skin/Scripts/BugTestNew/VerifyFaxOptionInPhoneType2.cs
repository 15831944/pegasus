using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class VerifyFaxOptionInPhoneType2 : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS3")]
        [TestCategory("BugTestNew")]
        public void verifyFaxOptionInPhoneType2()
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
            String JIRA = "";
            String Status = "Pass";

            //try
            //{

                executionLog.Log("VerifyFaxOptionInPhoneType2", "Login with valid credentials");
                Login(username[0], password[0]);

                executionLog.Log("VerifyFaxOptionInPhoneType2", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyFaxOptionInPhoneType2", "Reditect at contact page.");
                VisitOffice("contacts");

                executionLog.Log("VerifyFaxOptionInPhoneType2", "Verify title");
                office_ContactsHelper.VerifyText("VerifyContact", "Contacts");

                executionLog.Log("VerifyFaxOptionInPhoneType2", "Reditect at create contact page.");
                VisitOffice("contacts/create");

                executionLog.Log("VerifyFaxOptionInPhoneType2", "Click on Add Phone button");
                office_ContactsHelper.ClickElement("AddPhone");

                executionLog.Log("VerifyFaxOptionInPhoneType2", "Click on Phone Type-2 drop down");
                office_ContactsHelper.ClickElement("PhoneType2");

                executionLog.Log("VerifyFaxOptionInPhoneType2", "Click on Phone Type-2 drop down");
                office_ContactsHelper.IsElementVisible("//select[@id='ContactPhone1PhoneType']/option[@value='Fax']");

                

            //}
            //catch (Exception e)
            //{
                
            //}
            
        }
    }
}
