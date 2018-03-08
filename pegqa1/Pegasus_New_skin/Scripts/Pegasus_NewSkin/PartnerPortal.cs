using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class PartnerPortal : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin")]
        [TestCategory("TS8")]
        [TestCategory("Pegasus_NewSkin")]
        public void partnerPortal()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            //Initializing the objects5
            ExecutionLog executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var agents_PartnerAgentsHelper = new Agents_PartnerAgentsHelper(GetWebDriver());
            var office_UserHelper = new Office_UserHelper(GetWebDriver());

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            //Variable Random        
            var FName = "Test" + RandomNumber(99, 99999);
            var LName = "Test" + RandomNumber(99, 99999);
            var CDBA = "New" + RandomNumber(99, 99999);
            var user = "testing_user" + RandomNumber(99,99999);
            String JIRA = "";
            String Status = "Pass";

            try
            {

            executionLog.Log("PartnerPortal", "Login to office");
            Login(username[0], password[0]);

            executionLog.Log("PartnerPortal", "Redirect to dashboard");
            VerifyTitle("Dashboard");
            Console.WriteLine("Redirected at Dashboard screen.");

            VisitOffice("partners/agents");
            agents_PartnerAgentsHelper.WaitForWorkAround(3000);

            executionLog.Log("PartnerPortal", "Search the existed agent");
            agents_PartnerAgentsHelper.TypeText("AgentName", "QApartner Agent");
            agents_PartnerAgentsHelper.WaitForWorkAround(2000);

            var loc1 = "//table[@id='list1']//tr[2]//td[8]/a";
            if (agents_PartnerAgentsHelper.IsElementPresent(loc1))
            {
                executionLog.Log("PartnerPortal", "Logout from the application");
                VisitOffice("logout");

                executionLog.Log("PartnerPortal", "Login to the application");
                Login("MyPartnerAgent", "1qaz!QAZ");
                agents_PartnerAgentsHelper.WaitForWorkAround(2000);

                executionLog.Log("PartnerPortal", "Verify text partner agent");
                agents_PartnerAgentsHelper.VerifyText("VerifyHeadingPartnerAgnet", "Partner Agents");

                executionLog.Log("PartnerPortal", "Navigate to lead page");
                VisitOffice("partners/lead_create");

                executionLog.Log("PartnerPortal", "Clicl on save button");
                agents_PartnerAgentsHelper.ClickElement("ClickSaveBtn");
                //agents_PartnerAgentsHelper.WaitForWorkAround(2000);

                executionLog.Log("PartnerPortal", "Enter the first name");
                agents_PartnerAgentsHelper.TypeText("EnterFirstName", FName);

                executionLog.Log("PartnerPortal", "Enter the last name");
                agents_PartnerAgentsHelper.TypeText("EnterLastName", LName);

                executionLog.Log("PartnerPortal", "Enter the company name");
                agents_PartnerAgentsHelper.TypeText("LeadCompanyName", CDBA);

                executionLog.Log("PartnerPortal", "Select lead status");
                agents_PartnerAgentsHelper.SelectByText("SelectLeadStatus", "New");

                executionLog.Log("PartnerPortal", "Select lead source");
                agents_PartnerAgentsHelper.SelectByText("SelectSourcePLead", "Email");

                executionLog.Log("PartnerPortal", "Select lead responsibility");
                agents_PartnerAgentsHelper.SelectByText("SelectResponsibities", "Howard Tang");

                executionLog.Log("PartnerPortal", "Click on the save button");
                agents_PartnerAgentsHelper.ClickElement("ClickSaveBtn");
                agents_PartnerAgentsHelper.WaitForWorkAround(3000);

                var loc = "//h3[text()='Existing Leads']";
                if (agents_PartnerAgentsHelper.IsElementPresent(loc))
                {
                    executionLog.Log("PartnerPortal", "Click on lead duplicate");
                    agents_PartnerAgentsHelper.ClickOnDisplayed("CraeteLeadDub");
                    agents_PartnerAgentsHelper.WaitForText("Lead saved successfully.", 10);
                }

                else
                {
                    executionLog.Log("PartnerPortal", "Verify Page text");
                    agents_PartnerAgentsHelper.WaitForText("Lead saved successfully.", 10);

                    executionLog.Log("PartnerPortal", "Click edit lead");
                    agents_PartnerAgentsHelper.ClickElement("ClickEditTask");

                    executionLog.Log("PartnerPortal", "Click on save button");
                    agents_PartnerAgentsHelper.ClickElement("ClickSaveBtn");
                    agents_PartnerAgentsHelper.WaitForWorkAround(3000);

                    executionLog.Log("PartnerPortal", "Wait for confirmation.");
                    agents_PartnerAgentsHelper.WaitForText("Lead updated successfully.", 10);
                }
         }
                else
            {

                executionLog.Log("PartnerAgentWithUser", "Redirect to the URL");
                VisitOffice("partners/agent/create");
                agents_PartnerAgentsHelper.WaitForWorkAround(3000);

                executionLog.Log("PartnerAgentWithUser", "Verify page title.");
                VerifyTitle("Create a Partner Agent");

                executionLog.Log("PartnerAgentWithUser", "Select Salutation");
                agents_PartnerAgentsHelper.Select("SelectSalutation", "Mr");

                executionLog.Log("PartnerAgentWithUser", "Enter FirstNAME");
                agents_PartnerAgentsHelper.TypeText("FirstName", "QApartner");

                executionLog.Log("PartnerAgentWithUser", "Enter LastName");
                agents_PartnerAgentsHelper.TypeText("LastName", "Agent");

                executionLog.Log("PartnerAgentWithUser", "Enter DBAName");
                agents_PartnerAgentsHelper.TypeText("DBAName", "Qa Partner Infotech");

                executionLog.Log("PartnerAgentWithUser", "Select eAddressType");
                agents_PartnerAgentsHelper.Select("eAddressType", "E-Mail");

                executionLog.Log("PartnerAgentWithUser", "Select eAddressLebel");
                agents_PartnerAgentsHelper.Select("eAddressLebel", "Work");

                executionLog.Log("PartnerAgentWithUser", "Enter eAddressType");
                agents_PartnerAgentsHelper.TypeText("eAddress", "QApartner@yopmail.com");

                executionLog.Log("PartnerAgentWithUser", "Select SelectPhoneType");
                agents_PartnerAgentsHelper.Select("SelectPhoneType", "Work");

                executionLog.Log("PartnerAgentWithUser", "Select Address Type  ");
                agents_PartnerAgentsHelper.Select("AddressType", "Office");

                executionLog.Log("PartnerAgentWithUser", "Enter AddressLine1");
                agents_PartnerAgentsHelper.TypeText("AddressLine1", "FC 89");

                executionLog.Log("PartnerAgentWithUser", "Enter Zip code");
                agents_PartnerAgentsHelper.TypeText("PostalCode", "60601");
                agents_PartnerAgentsHelper.WaitForWorkAround(3000);

                executionLog.Log("PartnerAgentWithUser", "Click On Checkbox");
                agents_PartnerAgentsHelper.ClickViaJavaScript("//*[@id='UserCreateUser']");

                executionLog.Log("PartnerAgentWithUser", "Enter UserName");
                agents_PartnerAgentsHelper.TypeText("UserName", "MyPartnerAgent");

                executionLog.Log("PartnerAgentWithUser", "Click on auto generate pasword");
                agents_PartnerAgentsHelper.ClickElement("AutoGenPassword");
                agents_PartnerAgentsHelper.WaitForWorkAround(1000);

                executionLog.Log("PartnerAgentWithUser", "Enter the password");
                agents_PartnerAgentsHelper.TypeText("UserPassword", "1qaz!QAZ");

                executionLog.Log("PartnerAgentWithUser", "Click On Avatar");
                agents_PartnerAgentsHelper.ClickElement("ClickOnAvatar");
                agents_PartnerAgentsHelper.WaitForWorkAround(1000);

                executionLog.Log("PartnerAgentWithUser", "CLICK Save AGENT btn");
                agents_PartnerAgentsHelper.ClickElement("SaveAgent");
                agents_PartnerAgentsHelper.WaitForWorkAround(4000);

                executionLog.Log("PartnerPortal", "Logout from the application");
                VisitOffice("logout");

                GetWebDriver().Navigate().GoToUrl("http://www.yopmail.com");
                agents_PartnerAgentsHelper.WaitForWorkAround(5000);

                executionLog.Log("PartnerPortal", "Enter the email");
                agents_PartnerAgentsHelper.TypeText("LoginName", "QApartner@yopmail.com");               

                executionLog.Log("PartnerPortal", "Click on check inbox");
                agents_PartnerAgentsHelper.ClickElement("CheckInbox");
                agents_PartnerAgentsHelper.WaitForWorkAround(5000);

                executionLog.Log("PartnerPortal", "click on url to active the account");
                agents_PartnerAgentsHelper.ClickElement("ActivateUrl");
                agents_PartnerAgentsHelper.WaitForWorkAround(5000);

                executionLog.Log("PartnerPortal", "Login to the application");
                Login("MyPartnerAgent", "1qaz!QAZ");
                agents_PartnerAgentsHelper.WaitForWorkAround(2000);

                executionLog.Log("PartnerPortal", "Verify text partner agent");
                agents_PartnerAgentsHelper.VerifyText("VerifyHeadingPartnerAgnet", "Partner Agents");

                executionLog.Log("PartnerPortal", "Navigate to lead page");
                VisitOffice("partners/lead_create");

                executionLog.Log("PartnerPortal", "Clicl on save button");
                agents_PartnerAgentsHelper.ClickElement("ClickSaveBtn");
                agents_PartnerAgentsHelper.WaitForWorkAround(2000);

                executionLog.Log("PartnerPortal", "Enter the first name");
                agents_PartnerAgentsHelper.TypeText("EnterFirstName", FName);

                executionLog.Log("PartnerPortal", "Enter the last name");
                agents_PartnerAgentsHelper.TypeText("EnterLastName", LName);

                executionLog.Log("PartnerPortal", "Enter the company name");
                agents_PartnerAgentsHelper.TypeText("LeadCompanyName", CDBA);

                executionLog.Log("PartnerPortal", "Select lead status");
                agents_PartnerAgentsHelper.SelectByText("SelectLeadStatus", "New");

                executionLog.Log("PartnerPortal", "Select lead source");
                agents_PartnerAgentsHelper.SelectByText("SelectSourcePLead", "Email");

                executionLog.Log("PartnerPortal", "Select lead responsibility");
                agents_PartnerAgentsHelper.SelectByText("SelectResponsibities", "Howard Tang");

                executionLog.Log("PartnerPortal", "Click on the save button");
                agents_PartnerAgentsHelper.ClickElement("ClickSaveBtn");
                agents_PartnerAgentsHelper.WaitForWorkAround(3000);

                var loc = "//h3[text()='Existing Leads']";
                if (agents_PartnerAgentsHelper.IsElementPresent(loc))
                {
                    executionLog.Log("PartnerPortal", "Click on lead duplicate");
                    agents_PartnerAgentsHelper.ClickOnDisplayed("CraeteLeadDub");
                    agents_PartnerAgentsHelper.WaitForText("Lead saved successfully.", 10);
                }

                else
                {
                    executionLog.Log("PartnerPortal", "Verify Page text");
                    agents_PartnerAgentsHelper.WaitForText("Lead saved successfully.", 10);

                    executionLog.Log("PartnerPortal", "Click edit lead");
                    agents_PartnerAgentsHelper.ClickElement("ClickEditTask");

                    executionLog.Log("PartnerPortal", "Click on save button");
                    agents_PartnerAgentsHelper.ClickElement("ClickSaveBtn");
                    agents_PartnerAgentsHelper.WaitForWorkAround(3000);

                    executionLog.Log("PartnerPortal", "Wait for confirmation.");
                    agents_PartnerAgentsHelper.WaitForText("Lead updated successfully.", 10);
                    
                }
           }
        }
      catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("PartnerPortal");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("PartnerPortal");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("PartnerPortal", "Bug", "Medium", "Partner Lead page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("PartnerPortal");
                        TakeScreenshot("PartnerPortal");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\Iframe.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("PartnerPortal");
                        string id = loginHelper.getIssueID("PartnerPortal");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\PartnerPortal.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("PartnerPortal"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("PartnerPortal");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("PartnerPortal");
                executionLog.WriteInExcel("PartnerPortal", Status, JIRA, "Partner Portal");
            }
        }
    }
} 