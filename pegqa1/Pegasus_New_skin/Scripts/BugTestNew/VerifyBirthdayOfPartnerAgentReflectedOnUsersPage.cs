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
    public class VerifyBirthdayOfPartnerAgentReflectedOnUsersPage : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("BugTestNew")]
        public void verifyBirthdayOfPartnerAgentReflectedOnUsersPage()
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
                executionLog.Log("VerifyBirthdayOfPartnerAgentReflectedOnUsersPage", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyBirthdayOfPartnerAgentReflectedOnUsersPage", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("VerifyBirthdayOfPartnerAgentReflectedOnUsersPage", "Go to Partner Agent page.");
                VisitOffice("partners/agents");

                executionLog.Log("VerifyBirthdayOfPartnerAgentReflectedOnUsersPage", "Verify page title.");
                VerifyTitle("Partner Agents");

                executionLog.Log("VerifyBirthdayOfPartnerAgentReflectedOnUsersPage", "Click On Create");
                agent_PartnerAgentHelper.ClickElement("Create");
                agent_PartnerAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyBirthdayOfPartnerAgentReflectedOnUsersPage", "Select Salutation");
                agent_PartnerAgentHelper.Select("SelectSalutation", "Mr");

                executionLog.Log("VerifyBirthdayOfPartnerAgentReflectedOnUsersPage", "Enter FirstNAME");
                agent_PartnerAgentHelper.TypeText("FirstName", name);

                executionLog.Log("VerifyBirthdayOfPartnerAgentReflectedOnUsersPage", "Enter LastName");
                agent_PartnerAgentHelper.TypeText("LastName", "Tester");

                executionLog.Log("VerifyBirthdayOfPartnerAgentReflectedOnUsersPage", "Enter DBAName");
                agent_PartnerAgentHelper.TypeText("DBAName", "Test DBA");

                executionLog.Log("VerifyBirthdayOfPartnerAgentReflectedOnUsersPage", "Enter LinkedInUrl");
                agent_PartnerAgentHelper.TypeText("LinkedInUrl", "LinkedIn.con");

                executionLog.Log("VerifyBirthdayOfPartnerAgentReflectedOnUsersPage", "Enter Birthdate");
                agent_PartnerAgentHelper.TypeText("BirthDay", "07/07/1992");

                executionLog.Log("VerifyBirthdayOfPartnerAgentReflectedOnUsersPage", "Enter FaceBook Url");
                agent_PartnerAgentHelper.TypeText("FaceBookUrl", "Facebook.com");

                executionLog.Log("VerifyBirthdayOfPartnerAgentReflectedOnUsersPage", "Enter TwitterURL");
                agent_PartnerAgentHelper.TypeText("TwitterURL", "Twitter.com");

                executionLog.Log("VerifyBirthdayOfPartnerAgentReflectedOnUsersPage", "Select language.");
                agent_PartnerAgentHelper.Select("SelectLanguage", "English");

                executionLog.Log("VerifyBirthdayOfPartnerAgentReflectedOnUsersPage", "Select eAddressType");
                agent_PartnerAgentHelper.Select("eAddressType", "E-Mail");

                executionLog.Log("VerifyBirthdayOfPartnerAgentReflectedOnUsersPage", "Select eAddressLebel");
                agent_PartnerAgentHelper.Select("eAddressLebel", "Work");

                executionLog.Log("VerifyBirthdayOfPartnerAgentReflectedOnUsersPage", "Enter eAddressType");
                var Email = "P.Agent" + GetRandomNumber() + "@yopmail.com";
                agent_PartnerAgentHelper.TypeText("eAddress", Email);

                executionLog.Log("VerifyBirthdayOfPartnerAgentReflectedOnUsersPage", "Select SelectPhoneType");
                agent_PartnerAgentHelper.Select("SelectPhoneType", "Work");

                executionLog.Log("VerifyBirthdayOfPartnerAgentReflectedOnUsersPage", "Enter PhoneNumber");
                agent_PartnerAgentHelper.TypeText("PhoneNumber", "1212121212");

                executionLog.Log("VerifyBirthdayOfPartnerAgentReflectedOnUsersPage", "Select Address Type  ");
                agent_PartnerAgentHelper.Select("AddressType", "Office");

                executionLog.Log("VerifyBirthdayOfPartnerAgentReflectedOnUsersPage", "Enter AddressLine1");
                agent_PartnerAgentHelper.TypeText("AddressLine1", "FC 89");

                executionLog.Log("VerifyBirthdayOfPartnerAgentReflectedOnUsersPage", "Enter Postal code");
                agent_PartnerAgentHelper.TypeText("PostalCode", "60601");
                agent_PartnerAgentHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyBirthdayOfPartnerAgentReflectedOnUsersPage", "Select User Account Check Box");
                agent_PartnerAgentHelper.ClickJava("UserAccChkBox");

                executionLog.Log("VerifyBirthdayOfPartnerAgentReflectedOnUsersPage", "Enter Username");
                agent_PartnerAgentHelper.TypeText("UserName", user);

                executionLog.Log("VerifyBirthdayOfPartnerAgentReflectedOnUsersPage", "Select PartnerUser Avatar Check Box");
                agent_PartnerAgentHelper.ClickElement("ParnterUserAvatar");

                executionLog.Log("VerifyBirthdayOfPartnerAgentReflectedOnUsersPage", "Click Save Agent btn");
                agent_PartnerAgentHelper.ClickElement("ClickSave");
                agent_PartnerAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyBirthdayOfPartnerAgentReflectedOnUsersPage", "Go to All Users page");
                VisitOffice("users");
                office_UserHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyBirthdayOfPartnerAgentReflectedOnUsersPage", "Select User Type >> Partner Agent");
                office_UserHelper.SelectByText("SelectUserType", "Partner Agent");
                office_UserHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyBirthdayOfPartnerAgentReflectedOnUsersPage", "Enter partner user name");
                office_UserHelper.TypeText("SearchUser", name);
                office_UserHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyBirthdayOfPartnerAgentReflectedOnUsersPage", "Open partner agent");
                office_UserHelper.ClickElement("User1");
                office_UserHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyBirthdayOfPartnerAgentReflectedOnUsersPage", "Verify birthday is not as 01-Jan-1970");
                var text = office_UserHelper.GetText("//*[@id='page-wrapper']/div[5]/div/div[1]/div[1]/div[2]/div/div[2]/div[2]/div[2]");
                Assert.IsTrue(text.Contains("07-Jul-1992"));
                office_UserHelper.WaitForWorkAround(3000);


            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyBirthdayOfPartnerAgentReflectedOnUsersPage");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Birthday Of Partner Agent Reflected On Users Page");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Birthday Of Partner Agent Reflected On Users Page", "Bug", "Medium", "Partner Agents", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Birthday Of Partner Agent Reflected On Users Page");
                        TakeScreenshot("VerifyBirthdayOfPartnerAgentReflectedOnUsersPage");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyBirthdayOfPartnerAgentReflectedOnUsersPage.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyBirthdayOfPartnerAgentReflectedOnUsersPage");
                        string id = loginHelper.getIssueID("Verify Birthday Of Partner Agent Reflected On Users Page");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyBirthdayOfPartnerAgentReflectedOnUsersPage.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Birthday Of Partner Agent Reflected On Users Page"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Birthday Of Partner Agent Reflected On Users Page");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyBirthdayOfPartnerAgentReflectedOnUsersPage");
                executionLog.WriteInExcel("Verify Birthday Of Partner Agent Reflected On Users Page", Status, JIRA, "Agents Portal");
            }
        }
    }
}