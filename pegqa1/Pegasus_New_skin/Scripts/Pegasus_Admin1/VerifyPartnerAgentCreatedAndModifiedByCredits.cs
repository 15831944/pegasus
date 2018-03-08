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
    public class VerifyPartnerAgentCreatedAndModifiedByCredits : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin1")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Admin1")]
        public void verifyPartnerAgentCreatedAndModifiedByCredits()
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
            var agent_PartnerAgentHelper = new Agents_PartnerAgentsHelper(GetWebDriver());

            // Variable
            var name = "PAgent" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("VerifyPartnerAgentCreatedAndModifiedByCredits", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyPartnerAgentCreatedAndModifiedByCredits", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("VerifyPartnerAgentCreatedAndModifiedByCredits", "Go to Partner Agent page.");
                VisitOffice("partners/agents");
                agent_PartnerAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyPartnerAgentCreatedAndModifiedByCredits", "Verify page title.");
                VerifyTitle("Partner Agents");

                executionLog.Log("VerifyPartnerAgentCreatedAndModifiedByCredits", "Click On Create button");
                agent_PartnerAgentHelper.ClickElement("Create");
                agent_PartnerAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyPartnerAgentCreatedAndModifiedByCredits", "Verify page title..");
                VerifyTitle("Create a Partner Agent");

                executionLog.Log("VerifyPartnerAgentCreatedAndModifiedByCredits", "Click on save button.");
                agent_PartnerAgentHelper.ClickElement("ClickSave");

                executionLog.Log("VerifyPartnerAgentCreatedAndModifiedByCredits", "Verify validation for first name");
                agent_PartnerAgentHelper.VerifyText("FirstNameError", "This field is required.");

                executionLog.Log("VerifyPartnerAgentCreatedAndModifiedByCredits", "Verify validation for last name");
                agent_PartnerAgentHelper.VerifyText("LastNameError", "This field is required.");

                executionLog.Log("VerifyPartnerAgentCreatedAndModifiedByCredits", "Verify validation for e address type");
                agent_PartnerAgentHelper.VerifyText("AddressTypeError", "This field is required.");

                executionLog.Log("VerifyPartnerAgentCreatedAndModifiedByCredits", "Verify validation for eAddress label ");
                agent_PartnerAgentHelper.VerifyText("AddressLabelError", "This field is required.");

                executionLog.Log("VerifyPartnerAgentCreatedAndModifiedByCredits", "Verify validation for  eaddress");
                agent_PartnerAgentHelper.VerifyText("EaddressError", "This field is required.");

                executionLog.Log("VerifyPartnerAgentCreatedAndModifiedByCredits", "Enter an invalid eAddress");
                agent_PartnerAgentHelper.TypeText("eAddress", "1221313");

                executionLog.Log("VerifyPartnerAgentCreatedAndModifiedByCredits", "Verify validation for invalid eAddress.");
                agent_PartnerAgentHelper.VerifyText("EaddressError", "Please enter a valid email address.");

                executionLog.Log("VerifyPartnerAgentCreatedAndModifiedByCredits", "Enter an invalid eAddress.");
                agent_PartnerAgentHelper.TypeText("eAddress", "asssas");

                executionLog.Log("VerifyPartnerAgentCreatedAndModifiedByCredits", "Verify validation for invalid eaddress");
                agent_PartnerAgentHelper.VerifyText("EaddressError", "Please enter a valid email address.");

                executionLog.Log("VerifyPartnerAgentCreatedAndModifiedByCredits", "Click on cancel button.");
                agent_PartnerAgentHelper.ClickElement("Cancel");
                agent_PartnerAgentHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyPartnerAgentCreatedAndModifiedByCredits", "Verify page title");
                VerifyTitle("Partner Agents");

                executionLog.Log("VerifyPartnerAgentCreatedAndModifiedByCredits", "Click On Create button");
                agent_PartnerAgentHelper.ClickElement("Create");
                agent_PartnerAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyPartnerAgentCreatedAndModifiedByCredits", "Verify page title..");
                VerifyTitle("Create a Partner Agent");

                executionLog.Log("VerifyPartnerAgentCreatedAndModifiedByCredits", "Select Salutation for agent");
                agent_PartnerAgentHelper.Select("SelectSalutation", "Mr");

                executionLog.Log("VerifyPartnerAgentCreatedAndModifiedByCredits", "Enter agent FirstNAME");
                agent_PartnerAgentHelper.TypeText("FirstName", "Test Agent");

                executionLog.Log("VerifyPartnerAgentCreatedAndModifiedByCredits", "Enter agent LastName");
                agent_PartnerAgentHelper.TypeText("LastName", "Tester");

                executionLog.Log("VerifyPartnerAgentCreatedAndModifiedByCredits", "Enter agent Date Of Birth");
                agent_PartnerAgentHelper.TypeText("BirthDay", "08/08/1992");

                executionLog.Log("VerifyPartnerAgentCreatedAndModifiedByCredits", "Enter agent agent DBAName");
                agent_PartnerAgentHelper.TypeText("DBAName", "Test DBA");

                executionLog.Log("VerifyPartnerAgentCreatedAndModifiedByCredits", "Enter agent LinkedInUrl");
                agent_PartnerAgentHelper.TypeText("LinkedInUrl", "LinkedIn.con");

                executionLog.Log("VerifyPartnerAgentCreatedAndModifiedByCredits", "Enter agent FaceBook Url");
                agent_PartnerAgentHelper.TypeText("FaceBookUrl", "Facebook.com");

                executionLog.Log("VerifyPartnerAgentCreatedAndModifiedByCredits", "Enter agent TwitterURL");
                agent_PartnerAgentHelper.TypeText("TwitterURL", "Twitter.com");

                executionLog.Log("VerifyPartnerAgentCreatedAndModifiedByCredits", "Select agent language.");
                agent_PartnerAgentHelper.Select("SelectLanguage", "English");

                executionLog.Log("VerifyPartnerAgentCreatedAndModifiedByCredits", "Select agent eAddressType");
                agent_PartnerAgentHelper.Select("eAddressType", "E-Mail");

                executionLog.Log("VerifyPartnerAgentCreatedAndModifiedByCredits", "Select agent eAddressLebel");
                agent_PartnerAgentHelper.Select("eAddressLebel", "Work");

                executionLog.Log("VerifyPartnerAgentCreatedAndModifiedByCredits", "Enter agent eAddressType");
                var Email = "P.Agent" + GetRandomNumber() + "@yopmail.com";
                agent_PartnerAgentHelper.TypeText("eAddress", Email);

                executionLog.Log("VerifyPartnerAgentCreatedAndModifiedByCredits", "Select agent phone type.");
                agent_PartnerAgentHelper.Select("SelectPhoneType", "Work");

                executionLog.Log("VerifyPartnerAgentCreatedAndModifiedByCredits", "Enter agent PhoneNumber");
                agent_PartnerAgentHelper.TypeText("PhoneNumber", "1212121212");

                executionLog.Log("VerifyPartnerAgentCreatedAndModifiedByCredits", "Select agent Address Type  ");
                agent_PartnerAgentHelper.Select("AddressType", "Office");

                executionLog.Log("VerifyPartnerAgentCreatedAndModifiedByCredits", "Enter agent AddressLine1");
                agent_PartnerAgentHelper.TypeText("AddressLine1", "FC 89");

                executionLog.Log("VerifyPartnerAgentCreatedAndModifiedByCredits", "Enter agent Postal code");
                agent_PartnerAgentHelper.TypeText("PostalCode", "60601");
                agent_PartnerAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyPartnerAgentCreatedAndModifiedByCredits", "Click On Checkbox create user");
                agent_PartnerAgentHelper.ClickViaJavaScript("//*[@id='UserCreateUser']");

                executionLog.Log("VerifyPartnerAgentCreatedAndModifiedByCredits", "Enter agent UserName");
                agent_PartnerAgentHelper.TypeText("UserName", name);

                executionLog.Log("VerifyPartnerAgentCreatedAndModifiedByCredits", "Click On Avatar");
                agent_PartnerAgentHelper.ClickElement("ClickOnAvatar");

                executionLog.Log("VerifyPartnerAgentCreatedAndModifiedByCredits", "Click on save button.");
                agent_PartnerAgentHelper.ClickElement("ClickSave");
                agent_PartnerAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyPartnerAgentCreatedAndModifiedByCredits", "Verify agent created by credits.");
                agent_PartnerAgentHelper.VerifyText("CreatedBy", "Howard Tang");

                executionLog.Log("VerifyPartnerAgentCreatedAndModifiedByCredits", "Verify agent modified by credits.");
                agent_PartnerAgentHelper.VerifyText("ModiCredits", "Howard Tang");

                executionLog.Log("VerifyPartnerAgentCreatedAndModifiedByCredits", "Click on edit  button.");
                agent_PartnerAgentHelper.ClickElement("EditLink");
                agent_PartnerAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyPartnerAgentCreatedAndModifiedByCredits", "Verify page title.");
                VerifyTitle("Edit Partner Agent");

                executionLog.Log("VerifyPartnerAgentCreatedAndModifiedByCredits", "Enter agent LastName");
                agent_PartnerAgentHelper.TypeText("LastName", "Tester");

                executionLog.Log("VerifyPartnerAgentCreatedAndModifiedByCredits", "Enter agent Date Of Birth");
                agent_PartnerAgentHelper.TypeText("BirthDay", "08/08/1993");

                executionLog.Log("VerifyPartnerAgentCreatedAndModifiedByCredits", "Enter agent DBAName");
                agent_PartnerAgentHelper.TypeText("DBAName", "Test DBA");

                executionLog.Log("VerifyPartnerAgentCreatedAndModifiedByCredits", "Enter agent LinkedInUrl");
                agent_PartnerAgentHelper.TypeText("LinkedInUrl", "LinkedIn.con");

                executionLog.Log("VerifyPartnerAgentCreatedAndModifiedByCredits", "Enter agent FaceBook Url");
                agent_PartnerAgentHelper.TypeText("FaceBookUrl", "Facebook.com");

                executionLog.Log("VerifyPartnerAgentCreatedAndModifiedByCredits", "Enter agent TwitterURL");
                agent_PartnerAgentHelper.TypeText("TwitterURL", "Twitter.com");

                executionLog.Log("VerifyPartnerAgentCreatedAndModifiedByCredits", "Select agent language.");
                agent_PartnerAgentHelper.Select("SelectLanguage", "English");

                executionLog.Log("VerifyPartnerAgentCreatedAndModifiedByCredits", "Click on save button.");
                agent_PartnerAgentHelper.ClickElement("ClickSave");
                agent_PartnerAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyPartnerAgentCreatedAndModifiedByCredits", "Verify agent created by credits");
                agent_PartnerAgentHelper.VerifyText("CreatedBy", "Howard Tang");

                executionLog.Log("VerifyPartnerAgentCreatedAndModifiedByCredits", "Verify agent modified by credits");
                agent_PartnerAgentHelper.VerifyText("ModiCredits", "Howard Tang");

                executionLog.Log("VerifyPartnerAgentCreatedAndModifiedByCredits", "Logout from the application.");
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
             //   executionLog.DeleteFile("Error");
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