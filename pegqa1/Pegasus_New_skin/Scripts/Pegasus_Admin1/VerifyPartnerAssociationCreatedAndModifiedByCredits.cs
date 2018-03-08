using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class VerifyPartnerAssociationCreatedAndModifiedByCredits : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin1")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Admin1")]
        public void verifyPartnerAssociationCreatedAndModifiedByCredits()
        {
            string[] username = null;
            string[] password = null;

            XMLParse oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var agent_PartnerAssociationHelper = new Agents_PartnerAssociationHelper(GetWebDriver());

            // Variable
            var Email = "P.Asso" + RandomNumber(1, 999) + "@yopmail.com";
            var name = "PAgent" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("VerifyPartnerAssociationCreatedAndModifiedByCredits", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyPartnerAssociationCreatedAndModifiedByCredits", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("VerifyPartnerAssociationCreatedAndModifiedByCredits", "Redirect to partner Association page");
                VisitOffice("partners/associations");
                agent_PartnerAssociationHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyPartnerAssociationCreatedAndModifiedByCredits", "Verify page title.");
                VerifyTitle("Partner Associations");

                executionLog.Log("VerifyPartnerAssociationCreatedAndModifiedByCredits", "Click On Create button");
                agent_PartnerAssociationHelper.ClickElement("ClickAddNewAgentCode");
                agent_PartnerAssociationHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyPartnerAssociationCreatedAndModifiedByCredits", "Verify page title..");
                VerifyTitle("Create a Partner Association");

                executionLog.Log("VerifyPartnerAssociationCreatedAndModifiedByCredits", "Enter Association name");
                agent_PartnerAssociationHelper.TypeText("Birthday", "07/07/1995");

                executionLog.Log("VerifyPartnerAssociationCreatedAndModifiedByCredits", "Click on save button.");
                agent_PartnerAssociationHelper.ClickElement("ClickSaveBTN");

                executionLog.Log("VerifyPartnerAssociationCreatedAndModifiedByCredits", "Verify validation for association name.");
                agent_PartnerAssociationHelper.VerifyText("NameError", "This field is required.");

                executionLog.Log("VerifyPartnerAssociationCreatedAndModifiedByCredits", "Verify validation for first name");
                agent_PartnerAssociationHelper.VerifyText("FirstNameErr", "This field is required.");

                executionLog.Log("VerifyPartnerAssociationCreatedAndModifiedByCredits", "Verify validation for last name");
                agent_PartnerAssociationHelper.VerifyText("LastNameErr", "This field is required.");

                executionLog.Log("VerifyPartnerAssociationCreatedAndModifiedByCredits", "Verify validation for e address type");
                agent_PartnerAssociationHelper.VerifyText("EtypeError", "This field is required.");

                executionLog.Log("VerifyPartnerAssociationCreatedAndModifiedByCredits", "Verify validation for eAddress label ");
                agent_PartnerAssociationHelper.VerifyText("ElabelError", "This field is required.");

                executionLog.Log("VerifyPartnerAssociationCreatedAndModifiedByCredits", "Verify validation for  eaddress");
                agent_PartnerAssociationHelper.VerifyText("eAddressErrror", "This field is required.");

                executionLog.Log("VerifyPartnerAssociationCreatedAndModifiedByCredits", "Enter an invalid eAddress");
                agent_PartnerAssociationHelper.TypeText("eAddress", "1221313");

                executionLog.Log("VerifyPartnerAssociationCreatedAndModifiedByCredits", "Verify validation for invalid eAddress.");
                agent_PartnerAssociationHelper.VerifyText("eAddressErrror", "Please enter a valid email address.");

                executionLog.Log("VerifyPartnerAssociationCreatedAndModifiedByCredits", "Enter an invalid eAddress.");
                agent_PartnerAssociationHelper.TypeText("eAddress", "asssas");

                executionLog.Log("VerifyPartnerAssociationCreatedAndModifiedByCredits", "Verify validation for invalid eaddress");
                agent_PartnerAssociationHelper.VerifyText("eAddressErrror", "Please enter a valid email address.");

                executionLog.Log("VerifyPartnerAssociationCreatedAndModifiedByCredits", "Click on cancel button.");
                agent_PartnerAssociationHelper.ClickElement("Cancel");
                agent_PartnerAssociationHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyPartnerAssociationCreatedAndModifiedByCredits", "Verify page title");
                VerifyTitle("Partner Associations");

                executionLog.Log("VerifyPartnerAssociationCreatedAndModifiedByCredits", "Click On Create button");
                agent_PartnerAssociationHelper.ClickElement("ClickAddNewAgentCode");
                agent_PartnerAssociationHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyPartnerAssociationCreatedAndModifiedByCredits", "Verify page title..");
                VerifyTitle("Create a Partner Association");

                executionLog.Log("VerifyPartnerAssociationCreatedAndModifiedByCredits", "Enter Association name");
                agent_PartnerAssociationHelper.TypeText("Birthday", "07/07/1995");

                executionLog.Log("VerifyPartnerAssociationCreatedAndModifiedByCredits", "Enter Association name");
                agent_PartnerAssociationHelper.TypeText("Name", "AssociationTester");

                executionLog.Log("VerifyPartnerAssociationCreatedAndModifiedByCredits", "Enter Association DBAName");
                agent_PartnerAssociationHelper.TypeText("DBAName", "Test DBA");

                executionLog.Log("VerifyPartnerAssociationCreatedAndModifiedByCredits", "Select Association Salutation");
                agent_PartnerAssociationHelper.Select("SelectSalutation", "Mr");

                executionLog.Log("VerifyPartnerAssociationCreatedAndModifiedByCredits", "Enter Association FirstNAME");
                agent_PartnerAssociationHelper.TypeText("FirstNAME", "Test Agent");

                executionLog.Log("VerifyPartnerAssociationCreatedAndModifiedByCredits", "Enter Association LastName");
                agent_PartnerAssociationHelper.TypeText("LastName", "Tester");

                executionLog.Log("VerifyPartnerAssociationCreatedAndModifiedByCredits", "Select Association Language");
                agent_PartnerAssociationHelper.Select("SelectLanguage", "English");

                executionLog.Log("VerifyPartnerAssociationCreatedAndModifiedByCredits", "Select Association eAddressType");
                agent_PartnerAssociationHelper.Select("eAddressType", "E-Mail");

                executionLog.Log("VerifyPartnerAssociationCreatedAndModifiedByCredits", "Select Association eAddressLebel");
                agent_PartnerAssociationHelper.Select("eAddressLebel", "Work");

                executionLog.Log("VerifyPartnerAssociationCreatedAndModifiedByCredits", "Enter Association eAddressType");
                agent_PartnerAssociationHelper.TypeText("eAddress", Email);

                executionLog.Log("VerifyPartnerAssociationCreatedAndModifiedByCredits", "Select Association PhoneType");
                agent_PartnerAssociationHelper.Select("SelectPhoneType", "Work");

                executionLog.Log("VerifyPartnerAssociationCreatedAndModifiedByCredits", "Select Association Address Type   ");
                agent_PartnerAssociationHelper.Select("AddressType", "Office");

                executionLog.Log("VerifyPartnerAssociationCreatedAndModifiedByCredits", "Enter Association AddressLine1");
                agent_PartnerAssociationHelper.TypeText("AddressLine1", "FC 89");

                executionLog.Log("VerifyPartnerAssociationCreatedAndModifiedByCredits", "Enter Association Phone Number");
                agent_PartnerAssociationHelper.TypeText("PostalCode", "60601");
                agent_PartnerAssociationHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyPartnerAssociationCreatedAndModifiedByCredits", "Enter Association UserName");
                agent_PartnerAssociationHelper.TypeText("UserName", name);

                executionLog.Log("VerifyPartnerAssociationCreatedAndModifiedByCredits", "Click On Avatar");
                agent_PartnerAssociationHelper.ClickElement("ClickOnAvatar");
                agent_PartnerAssociationHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyPartnerAssociationCreatedAndModifiedByCredits", "Click on Save button.");
                agent_PartnerAssociationHelper.ClickElement("ClickSaveBTN");
                agent_PartnerAssociationHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyPartnerAssociationCreatedAndModifiedByCredits", "Verify association created by credits.");
                agent_PartnerAssociationHelper.VerifyText("CreatedBy", "Howard Tang");

                executionLog.Log("VerifyPartnerAssociationCreatedAndModifiedByCredits", "Verify Association modified by credits.");
                agent_PartnerAssociationHelper.VerifyText("ModifiedBy", "Howard Tang");

                executionLog.Log("VerifyPartnerAssociationCreatedAndModifiedByCredits", "Click on edit  button.");
                agent_PartnerAssociationHelper.ClickElement("EditLink");
                agent_PartnerAssociationHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyPartnerAssociationCreatedAndModifiedByCredits", "Verify page title.");
                VerifyTitle("Edit Partner Association");

                executionLog.Log("VerifyPartnerAssociationCreatedAndModifiedByCredits", "Enter Association FirstNAME");
                agent_PartnerAssociationHelper.TypeText("FirstNAME", "Test Agent");

                executionLog.Log("VerifyPartnerAssociationCreatedAndModifiedByCredits", "Enter Association LastName");
                agent_PartnerAssociationHelper.TypeText("LastName", "Tester");

                executionLog.Log("VerifyPartnerAssociationCreatedAndModifiedByCredits", "Select Association Language");
                agent_PartnerAssociationHelper.Select("SelectLanguage", "English");

                executionLog.Log("VerifyPartnerAssociationCreatedAndModifiedByCredits", "Select Association eAddressType");
                agent_PartnerAssociationHelper.Select("eAddressType", "E-Mail");

                executionLog.Log("VerifyPartnerAssociationCreatedAndModifiedByCredits", "Select Association eAddressLebel");
                agent_PartnerAssociationHelper.Select("eAddressLebel", "Work");

                executionLog.Log("VerifyPartnerAssociationCreatedAndModifiedByCredits", "Enter Association eAddressType");
                agent_PartnerAssociationHelper.TypeText("eAddress", Email);

                executionLog.Log("VerifyPartnerAssociationCreatedAndModifiedByCredits", "Select Association PhoneType");
                agent_PartnerAssociationHelper.Select("SelectPhoneType", "Work");

                executionLog.Log("VerifyPartnerAssociationCreatedAndModifiedByCredits", "Select Association Address Type   ");
                agent_PartnerAssociationHelper.Select("AddressType", "Office");

                executionLog.Log("VerifyPartnerAssociationCreatedAndModifiedByCredits", "Enter Association AddressLine1");
                agent_PartnerAssociationHelper.TypeText("AddressLine1", "FC 89");

                executionLog.Log("VerifyPartnerAssociationCreatedAndModifiedByCredits", "Enter Association Phone Number");
                agent_PartnerAssociationHelper.TypeText("PostalCode", "60601");
                agent_PartnerAssociationHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyPartnerAssociationCreatedAndModifiedByCredits", "Click on save button.");
                agent_PartnerAssociationHelper.ClickElement("ClickSaveBTN");
                agent_PartnerAssociationHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyPartnerAssociationCreatedAndModifiedByCredits", "Verify association created by credits");
                agent_PartnerAssociationHelper.VerifyText("CreatedBy", "Howard Tang");

                executionLog.Log("VerifyPartnerAssociationCreatedAndModifiedByCredits", "Verify association modified by credits");
                agent_PartnerAssociationHelper.VerifyText("ModifiedBy", "Howard Tang");

                executionLog.Log("VerifyPartnerAssociationCreatedAndModifiedByCredits", "Logout from the application.");
                VisitOffice("logout");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyTicketsCreatedAndModifiedByCredits");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Tickets Created And Modified By Credits");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Tickets Created And Modified By Credits", "Bug", "Medium", "Ticket page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Tickets Created And Modified By Credits");
                        TakeScreenshot("VerifyTicketsCreatedAndModifiedByCredits");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyTicketsCreatedAndModifiedByCredits.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyTicketsCreatedAndModifiedByCredits");
                        string id = loginHelper.getIssueID("Verify Tickets Created And Modified By Credits");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyTicketsCreatedAndModifiedByCredits.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Tickets Created And Modified By Credits"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Tickets Created And Modified By Credits");
          //      executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyTicketsCreatedAndModifiedByCredits");
                executionLog.WriteInExcel("Verify Tickets Created And Modified By Credits", Status, JIRA, "Office tickets");
            }
        }
    }
}