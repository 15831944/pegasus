using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class CreatePartnerIssue : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS4")]
        [TestCategory("NewSkinTaskJira")]
        public void createPartnerIssue()
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
            var agents_PartnerAssociationHelper = new Agents_PartnerAssociationHelper(GetWebDriver());

            // Variable 
            var Name = "Testagent" + RandomNumber(111, 99999);
            var FirstName = "AgentQa" + RandomNumber(11, 99999);
            var Id = "12345" + RandomNumber(11, 999);
            var username1 = "testinguser" + RandomNumber(111, 999999);
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("CreatePartnerIssue", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("CreatePartnerIssue", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("CreatePartnerIssue", "Go To Create Partner association page");
                VisitOffice("partners/associations");
                agents_PartnerAssociationHelper.WaitForWorkAround(3000);

                executionLog.Log("CreatePartnerIssue", "Search the name");
                agents_PartnerAssociationHelper.TypeText("SearchAssociation", "AssociationTester");
                agents_PartnerAssociationHelper.WaitForWorkAround(2000);

                var loc = "//table[@id='list1']//tr[2]//td[6]/a";
                if (agents_PartnerAssociationHelper.IsElementPresent(loc))
                {

                    executionLog.Log("CreatePartnerAgentAndUserAccount", "Click On Create");
                    agents_PartnerAssociationHelper.clickJS("CreateAsso");
                    agents_PartnerAssociationHelper.WaitForWorkAround(3000);

                    executionLog.Log("CreatePartnerAgentAndUserAccount", "Select Salutation");
                    agents_PartnerAssociationHelper.Select("SelectSalutation", "Mr");

                    executionLog.Log("CreatePartnerAgentAndUserAccount", "Enter the name");
                    agents_PartnerAssociationHelper.TypeText("Name", Name);

                    executionLog.Log("CreatePartnerAgentAndUserAccount", "Enter FirstNAME");
                    agents_PartnerAssociationHelper.TypeText("FirstNAME", "Test Agent");

                    executionLog.Log("CreatePartnerAgentAndUserAccount", "Enter LastName");
                    agents_PartnerAssociationHelper.TypeText("LastName", "Tester");

                    executionLog.Log("CreatePartnerAgentAndUserAccount", "Enter Date Of Birth");
                    agents_PartnerAssociationHelper.TypeText("Assobirth", "08/08/1994");

                    executionLog.Log("CreatePartnerAgentAndUserAccount", "Enter DBAName");
                    agents_PartnerAssociationHelper.TypeText("DBAName", "Test DBA");

                    executionLog.Log("CreatePartnerAgentAndUserAccount", "Enter LinkedInUrl");
                    agents_PartnerAssociationHelper.TypeText("LinkedInUrl", "LinkedIn.con");

                    executionLog.Log("CreatePartnerAgentAndUserAccount", "Enter FaceBook Url");
                    agents_PartnerAssociationHelper.TypeText("FaceBookUrl", "Facebook.com");

                    executionLog.Log("CreatePartnerAgentAndUserAccount", "Enter TwitterURL");
                    agents_PartnerAssociationHelper.TypeText("TwitterURL", "Twitter.com");

                    executionLog.Log("CreatePartnerAgentAndUserAccount", "Select language.");
                    agents_PartnerAssociationHelper.Select("SelectLanguage", "English");

                    executionLog.Log("CreatePartnerAgentAndUserAccount", "Select eAddressType");
                    agents_PartnerAssociationHelper.Select("eAddressType", "E-Mail");

                    executionLog.Log("CreatePartnerAgentAndUserAccount", "Select eAddressLebel");
                    agents_PartnerAssociationHelper.Select("eAddressLebel", "Work");

                    executionLog.Log("CreatePartnerAgentAndUserAccount", "Enter eAddressType");
                    var Email = "P.Agent" + GetRandomNumber() + "@yopmail.com";
                    agents_PartnerAssociationHelper.TypeText("eAddress", Email);

                    executionLog.Log("CreatePartnerAgentAndUserAccount", "Select SelectPhoneType");
                    agents_PartnerAssociationHelper.Select("SelectPhoneType", "Work");

                    executionLog.Log("CreatePartnerAgentAndUserAccount", "Enter PhoneNumber");
                    agents_PartnerAssociationHelper.TypeText("PhoneNumber", "1212121212");

                    executionLog.Log("CreatePartnerAgentAndUserAccount", "Select Address Type  ");
                    agents_PartnerAssociationHelper.Select("AddressType", "Office");

                    executionLog.Log("CreatePartnerAgentAndUserAccount", "Enter AddressLine1");
                    agents_PartnerAssociationHelper.TypeText("AddressLine1", "FC 89");

                    executionLog.Log("CreatePartnerAgentAndUserAccount", "Enter Postal code");
                    agents_PartnerAssociationHelper.TypeText("PostalCode", "60601");
                    agents_PartnerAssociationHelper.WaitForWorkAround(3000);

                    executionLog.Log("CreatePartnerAgentAndUserAccount", "Enter Username");
                    agents_PartnerAssociationHelper.TypeText("UserName", username1);

                    executionLog.Log("CreatePartnerAgentAndUserAccount", "Select Avatar");
                    agents_PartnerAssociationHelper.ClickElement("ClickOnAvatar");

                    executionLog.Log("CreatePartnerAgentAndUserAccount", "Click Save Agent btn");
                    agents_PartnerAssociationHelper.ClickElement("AssSave");
                    agents_PartnerAssociationHelper.WaitForWorkAround(5000);

                    executionLog.Log("CreatePartnerAgentAndUserAccount", "Verify success message. ");
                    agents_PartnerAssociationHelper.WaitForText("Partner Association Created Successfully.", 30);

                }
                else
                {
                    executionLog.Log("CreatePartnerAgentAndUserAccount", "Click On Create");
                    agents_PartnerAssociationHelper.clickJS("CreateAsso");
                    agents_PartnerAssociationHelper.WaitForWorkAround(3000);

                    executionLog.Log("CreatePartnerAgentAndUserAccount", "Select Salutation");
                    agents_PartnerAssociationHelper.Select("SelectSalutation", "Mr");

                    executionLog.Log("CreatePartnerAgentAndUserAccount", "Enter the name");
                    agents_PartnerAssociationHelper.TypeText("Name", "AssociationTester");

                    executionLog.Log("CreatePartnerAgentAndUserAccount", "Enter FirstNAME");
                    agents_PartnerAssociationHelper.TypeText("FirstNAME", "Test Agent");

                    executionLog.Log("CreatePartnerAgentAndUserAccount", "Enter LastName");
                    agents_PartnerAssociationHelper.TypeText("LastName", "Tester");

                    executionLog.Log("CreatePartnerAgentAndUserAccount", "Enter Date Of Birth");
                    agents_PartnerAssociationHelper.TypeText("Assobirth", "08/08/1994");

                    executionLog.Log("CreatePartnerAgentAndUserAccount", "Enter DBAName");
                    agents_PartnerAssociationHelper.TypeText("DBAName", "Test DBA");

                    executionLog.Log("CreatePartnerAgentAndUserAccount", "Enter LinkedInUrl");
                    agents_PartnerAssociationHelper.TypeText("LinkedInUrl", "LinkedIn.con");

                    executionLog.Log("CreatePartnerAgentAndUserAccount", "Enter FaceBook Url");
                    agents_PartnerAssociationHelper.TypeText("FaceBookUrl", "Facebook.com");

                    executionLog.Log("CreatePartnerAgentAndUserAccount", "Enter TwitterURL");
                    agents_PartnerAssociationHelper.TypeText("TwitterURL", "Twitter.com");

                    executionLog.Log("CreatePartnerAgentAndUserAccount", "Select language.");
                    agents_PartnerAssociationHelper.Select("SelectLanguage", "English");

                    executionLog.Log("CreatePartnerAgentAndUserAccount", "Select eAddressType");
                    agents_PartnerAssociationHelper.Select("eAddressType", "E-Mail");

                    executionLog.Log("CreatePartnerAgentAndUserAccount", "Select eAddressLebel");
                    agents_PartnerAssociationHelper.Select("eAddressLebel", "Work");

                    executionLog.Log("CreatePartnerAgentAndUserAccount", "Enter eAddressType");
                    var Email = "P.Agent" + GetRandomNumber() + "@yopmail.com";
                    agents_PartnerAssociationHelper.TypeText("eAddress", Email);

                    executionLog.Log("CreatePartnerAgentAndUserAccount", "Select SelectPhoneType");
                    agents_PartnerAssociationHelper.Select("SelectPhoneType", "Work");

                    executionLog.Log("CreatePartnerAgentAndUserAccount", "Enter PhoneNumber");
                    agents_PartnerAssociationHelper.TypeText("PhoneNumber", "1212121212");

                    executionLog.Log("CreatePartnerAgentAndUserAccount", "Select Address Type  ");
                    agents_PartnerAssociationHelper.Select("AddressType", "Office");

                    executionLog.Log("CreatePartnerAgentAndUserAccount", "Enter AddressLine1");
                    agents_PartnerAssociationHelper.TypeText("AddressLine1", "FC 89");

                    executionLog.Log("CreatePartnerAgentAndUserAccount", "Enter Postal code");
                    agents_PartnerAssociationHelper.TypeText("PostalCode", "60601");
                    agents_PartnerAssociationHelper.WaitForWorkAround(3000);

                    executionLog.Log("CreatePartnerAgentAndUserAccount", "Enter Username");
                    agents_PartnerAssociationHelper.TypeText("UserName", username1);

                    executionLog.Log("CreatePartnerAgentAndUserAccount", "Select Avatar");
                    agents_PartnerAssociationHelper.ClickElement("ClickOnAvatar");

                    executionLog.Log("CreatePartnerAgentAndUserAccount", "Click Save Agent btn");
                    agents_PartnerAssociationHelper.ClickElement("AssSave");
                    agents_PartnerAssociationHelper.WaitForWorkAround(5000);

                    executionLog.Log("CreatePartnerAgentAndUserAccount", "Verify success message. ");
                    agents_PartnerAssociationHelper.WaitForText("Partner Association Created Successfully.", 30);
                }
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CreatePartnerIssue");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Create Partner Issue");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Create Partner Issue", "Bug", "Medium", "Create Partner page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Create Partner Issue");
                        TakeScreenshot("CreatePartnerIssue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreatePartnerIssue.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CreatePartnerIssue");
                        string id = loginHelper.getIssueID("Create Partner Issue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreatePartnerIssue.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Create Partner Issue"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Create Partner Issue");
                executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("CreatePartnerIssue");
                executionLog.WriteInExcel("Create Partner Issue", Status, JIRA, "Partner Portal");
            }
        }
    }
}