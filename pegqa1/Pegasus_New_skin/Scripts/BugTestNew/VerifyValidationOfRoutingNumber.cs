using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class VerifyValidationOfRoutingNumber : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin")]
        [TestCategory("Fail")]
        [TestCategory("BugTestNew")]
        public void verifyValidationOfRoutingNumber()
        {
            string[] username = null;
            string[] password = null;

            

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var office_LeadsHelper = new Office_LeadsHelper(GetWebDriver());


            var Company = "Company" + RandomNumber(1, 50000);
            var First = "Test" + RandomNumber(1, 50000);
            var Last = "Lead" + RandomNumber(1, 50000);

            // Variable random
            String JIRA = "";
            String Status = "Pass";

            //try
            //{

                executionLog.Log("VerifyValidationOfRoutingNumber", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in with " +username[0]+ " / " +password[0]);

                executionLog.Log("VerifyValidationOfRoutingNumber", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Dashboard Title Verified");

                executionLog.Log("VerifyValidationOfRoutingNumber", "Create a lead");
                VisitOffice("leads/create");
                office_LeadsHelper.WaitForWorkAround(4000);
                Console.WriteLine("Redirected to Create Lead page");

                executionLog.Log("VerifyValidationOfRoutingNumber", "Enter Company name");
                office_LeadsHelper.TypeText("CompanyName", Company);

                executionLog.Log("VerifyValidationOfRoutingNumber", "Enter First Name");
                office_LeadsHelper.TypeText("FirstNameLead", First);

                executionLog.Log("VerifyValidationOfRoutingNumber", "Enter Last Name");
                office_LeadsHelper.TypeText("LastName", Last);

                executionLog.Log("VerifyValidationOfRoutingNumber", "Select Status");
                office_LeadsHelper.Select("LeadStatus", "New");

                executionLog.Log("VerifyValidationOfRoutingNumber", "Select Responsibility");
                office_LeadsHelper.SelectByText("Responsibility", "Howard Tang");

                executionLog.Log("VerifyValidationOfRoutingNumber", "Save Lead");
                office_LeadsHelper.ClickElement("Save");
                office_LeadsHelper.WaitForWorkAround(2000);
                Console.WriteLine("Lead Created successfully");

                executionLog.Log("VerifyValidationOfRoutingNumber", "Go to Business Details tab");
                office_LeadsHelper.ClickElement("BusinessTab");
                office_LeadsHelper.WaitForWorkAround(2000);
                Console.WriteLine("Redirected to Business Details Tab");

                executionLog.Log("VerifyValidationOfRoutingNumber", "Select Processor");
                office_LeadsHelper.ClickElement("SaveBD");
                //office_LeadsHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyValidationOfRoutingNumber", "Verify validation not appearing");
                office_LeadsHelper.WaitForText("Lead data updated successfully. .",05);
                Console.WriteLine("Validation is not appearing");

                

               

            //}
            //catch (Exception e)
            //{
                
                
                    
            //}
        }
    }
}