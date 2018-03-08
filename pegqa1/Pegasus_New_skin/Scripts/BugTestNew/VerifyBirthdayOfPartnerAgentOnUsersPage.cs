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
    public class VerifyBirthdayOfPartnerAgentOnUsersPage : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("BugTestNew")]
        public void verifyBirthdayOfPartnerAgentOnUsersPage()
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
            var office_UserHelper = new Office_UserHelper(GetWebDriver());

            // Variable
            var name = "TestAgent" + GetRandomNumber();
            var user = "agentuser" + RandomNumber(111,99999);
            String JIRA = "";
            String Status = "Pass";


            try
            {
                executionLog.Log("VerifyBirthdayOfPartnerAgentOnUsersPage", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyBirthdayOfPartnerAgentOnUsersPage", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("VerifyBirthdayOfPartnerAgentOnUsersPage", "Go to Partner Agent page.");
                VisitOffice("partners/agents");

                executionLog.Log("VerifyBirthdayOfPartnerAgentOnUsersPage", "Verify page title.");
                VerifyTitle("Partner Agents");

                executionLog.Log("VerifyBirthdayOfPartnerAgentOnUsersPage", "Click On Create");
                agent_PartnerAgentHelper.ClickElement("Create");
                agent_PartnerAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyBirthdayOfPartnerAgentOnUsersPage", "Select Salutation");
                agent_PartnerAgentHelper.Select("SelectSalutation", "Mr");

                executionLog.Log("VerifyBirthdayOfPartnerAgentOnUsersPage", "Enter FirstNAME");
                agent_PartnerAgentHelper.TypeText("FirstName", name);

                executionLog.Log("VerifyBirthdayOfPartnerAgentOnUsersPage", "Enter LastName");
                agent_PartnerAgentHelper.TypeText("LastName", "Tester");

                executionLog.Log("VerifyBirthdayOfPartnerAgentOnUsersPage", "Enter DBAName");
                agent_PartnerAgentHelper.TypeText("DBAName", "Test DBA");

                executionLog.Log("VerifyBirthdayOfPartnerAgentOnUsersPage", "Enter LinkedInUrl");
                agent_PartnerAgentHelper.TypeText("LinkedInUrl", "LinkedIn.con");

                executionLog.Log("VerifyBirthdayOfPartnerAgentOnUsersPage", "Enter FaceBook Url");
                agent_PartnerAgentHelper.TypeText("FaceBookUrl", "Facebook.com");

                executionLog.Log("VerifyBirthdayOfPartnerAgentOnUsersPage", "Enter TwitterURL");
                agent_PartnerAgentHelper.TypeText("TwitterURL", "Twitter.com");

                executionLog.Log("VerifyBirthdayOfPartnerAgentOnUsersPage", "Select language.");
                agent_PartnerAgentHelper.Select("SelectLanguage", "English");

                executionLog.Log("VerifyBirthdayOfPartnerAgentOnUsersPage", "Select eAddressType");
                agent_PartnerAgentHelper.Select("eAddressType", "E-Mail");

                executionLog.Log("VerifyBirthdayOfPartnerAgentOnUsersPage", "Select eAddressLebel");
                agent_PartnerAgentHelper.Select("eAddressLebel", "Work");

                executionLog.Log("VerifyBirthdayOfPartnerAgentOnUsersPage", "Enter eAddressType");
                var Email = "P.Agent" + GetRandomNumber() + "@yopmail.com";
                agent_PartnerAgentHelper.TypeText("eAddress", Email);

                executionLog.Log("VerifyBirthdayOfPartnerAgentOnUsersPage", "Select SelectPhoneType");
                agent_PartnerAgentHelper.Select("SelectPhoneType", "Work");

                executionLog.Log("VerifyBirthdayOfPartnerAgentOnUsersPage", "Enter PhoneNumber");
                agent_PartnerAgentHelper.TypeText("PhoneNumber", "1212121212");

                executionLog.Log("VerifyBirthdayOfPartnerAgentOnUsersPage", "Select Address Type  ");
                agent_PartnerAgentHelper.Select("AddressType", "Office");

                executionLog.Log("VerifyBirthdayOfPartnerAgentOnUsersPage", "Enter AddressLine1");
                agent_PartnerAgentHelper.TypeText("AddressLine1", "FC 89");

                executionLog.Log("VerifyBirthdayOfPartnerAgentOnUsersPage", "Enter Postal code");
                agent_PartnerAgentHelper.TypeText("PostalCode", "60601");
                agent_PartnerAgentHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyBirthdayOfPartnerAgentOnUsersPage", "Select User Account Check Box");
                agent_PartnerAgentHelper.ClickJava("UserAccChkBox");

                executionLog.Log("VerifyBirthdayOfPartnerAgentOnUsersPage", "Enter Username");
                agent_PartnerAgentHelper.TypeText("UserName", user);

                executionLog.Log("VerifyBirthdayOfPartnerAgentOnUsersPage", "Select PartnerUser Avatar Check Box");
                agent_PartnerAgentHelper.ClickElement("ParnterUserAvatar");

                executionLog.Log("VerifyBirthdayOfPartnerAgentOnUsersPage", "Click Save Agent btn");
                agent_PartnerAgentHelper.ClickElement("ClickSave");
                agent_PartnerAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyBirthdayOfPartnerAgentOnUsersPage", "Go to All Users page");
                VisitOffice("users");
                office_UserHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyBirthdayOfPartnerAgentOnUsersPage", "Select User Type >> Partner Agent");
                office_UserHelper.SelectByText("SelectUserType", "Partner Agent");
                office_UserHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyBirthdayOfPartnerAgentOnUsersPage", "Enter partner user name");
                office_UserHelper.TypeText("SearchUser", name);
                office_UserHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyBirthdayOfPartnerAgentOnUsersPage", "Open partner agent");
                office_UserHelper.ClickElement("User1");
                office_UserHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyBirthdayOfPartnerAgentOnUsersPage", "Verify birthday is not as 01-Jan-1970");
                var text = office_UserHelper.GetText("//*[@id='page-wrapper']/div[5]/div/div[1]/div[1]/div[2]/div/div[2]/div[2]/div[2]");
                Assert.IsFalse(text.Contains("01-Jan-1970"));
                office_UserHelper.WaitForWorkAround(3000);


            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyBirthdayOfPartnerAgentOnUsersPage");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Birthday Of Partner Agent On Users Page");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Birthday Of Partner Agent On Users Page", "Bug", "Medium", "Partner Agents", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Birthday Of Partner Agent On Users Page");
                        TakeScreenshot("VerifyBirthdayOfPartnerAgentOnUsersPage");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyBirthdayOfPartnerAgentOnUsersPage.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyBirthdayOfPartnerAgentOnUsersPage");
                        string id = loginHelper.getIssueID("Verify Birthday Of Partner Agent On Users Page");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyBirthdayOfPartnerAgentOnUsersPage.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Birthday Of Partner Agent On Users Page"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Birthday Of Partner Agent On Users Page");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyBirthdayOfPartnerAgentOnUsersPage");
                executionLog.WriteInExcel("Verify Birthday Of Partner Agent On Users Page", Status, JIRA, "Agents Portal");
            }
        }
    }
}