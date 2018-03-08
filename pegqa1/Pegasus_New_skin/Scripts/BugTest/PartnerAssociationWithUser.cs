using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class PartnerAssociationWithUser : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void partnerAssociationWithUser()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var office_UserHelper = new Office_UserHelper(GetWebDriver());
            var agent_PartnerAssociationHelper = new Agents_PartnerAssociationHelper(GetWebDriver());

            // Variable
            var name = "TestAgent" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("PartnerAssociationWithUser", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("PartnerAssociationWithUser", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("PartnerAssociationWithUser", "Redirect to create partner agent");
                VisitOffice("partners/association/create");
                agent_PartnerAssociationHelper.WaitForWorkAround(1000);

                executionLog.Log("PartnerAssociationWithUser", "Enter Association name");
                agent_PartnerAssociationHelper.TypeText("Name", "AssociationTester");

                executionLog.Log("PartnerAssociationWithUser", "Enter DBAName");
                agent_PartnerAssociationHelper.TypeText("DBAName", "Test DBA");

                executionLog.Log("PartnerAssociationWithUser", "Select Salutation");
                agent_PartnerAssociationHelper.Select("SelectSalutation", "Mr");

                executionLog.Log("PartnerAssociationWithUser", "Enter FirstNAME");
                agent_PartnerAssociationHelper.TypeText("FirstNAME", "Test Agent");

                executionLog.Log("PartnerAssociationWithUser", "Enter LastName");
                agent_PartnerAssociationHelper.TypeText("LastName", "Tester");

                executionLog.Log("PartnerAssociationWithUser", "Select Language");
                agent_PartnerAssociationHelper.Select("SelectLanguage", "English");

                executionLog.Log("PartnerAssociationWithUser", "Select eAddressType");
                agent_PartnerAssociationHelper.Select("eAddressType", "E-Mail");

                executionLog.Log("PartnerAssociationWithUser", "Select eAddressLebel");
                agent_PartnerAssociationHelper.Select("eAddressLebel", "Work");

                executionLog.Log("PartnerAssociationWithUser", "Enter eAddressType");
                var Email = "P.Asso" + RandomNumber(1, 999) + "@yopmail.com";
                agent_PartnerAssociationHelper.TypeText("eAddress", Email);

                executionLog.Log("PartnerAssociationWithUser", "Select SelectPhoneType");
                agent_PartnerAssociationHelper.Select("SelectPhoneType", "Work");

                executionLog.Log("PartnerAssociationWithUser", "Select Address Type   ");
                agent_PartnerAssociationHelper.Select("AddressType", "Office");

                executionLog.Log("PartnerAssociationWithUser", "Enter AddressLine1");
                agent_PartnerAssociationHelper.TypeText("AddressLine1", "FC 89");

                executionLog.Log("PartnerAssociationWithUser", "Enter Phone Number");
                agent_PartnerAssociationHelper.TypeText("PostalCode", "60601");
                agent_PartnerAssociationHelper.WaitForWorkAround(2000);

                executionLog.Log("PartnerAssociationWithUser", "Click On Checkbox");
                agent_PartnerAssociationHelper.ClickViaJavaScript("//input[@id='UserCreateUser' and @type='checkbox']");
                agent_PartnerAssociationHelper.WaitForWorkAround(1000);

                executionLog.Log("PartnerAssociationWithUser", "Enter UserName");
                agent_PartnerAssociationHelper.TypeText("UserName", name);

                executionLog.Log("PartnerAssociationWithUser", "Click On Avatar");
                agent_PartnerAssociationHelper.ClickElement("ClickOnAvatar");
                agent_PartnerAssociationHelper.WaitForWorkAround(1000);

                executionLog.Log("PartnerAssociationWithUser", "CLICK Save");
                agent_PartnerAssociationHelper.ClickElement("ClickSaveBTN");
                agent_PartnerAssociationHelper.WaitForWorkAround(1000);

                executionLog.Log("PartnerAssociationWithUser", "Redirect To Admin");
                VisitOffice("admin");
                agent_PartnerAssociationHelper.WaitForWorkAround(1000);

                executionLog.Log("PartnerAssociationWithUser", "Redirect To User");
                VisitOffice("users");
                agent_PartnerAssociationHelper.WaitForWorkAround(1000);

                executionLog.Log("PartnerAssociationWithUser", "Select status");
                office_UserHelper.Select("SelectStatus1", "");
                office_UserHelper.WaitForWorkAround(1000);

                executionLog.Log("PartnerAssociationWithUser", "Enter email in search field.");
                office_UserHelper.TypeText("EnterEmail", Email);
                office_UserHelper.WaitForWorkAround(4000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";
                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("PartnerAssociationWithUser");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Partner Association WithUser");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Partner Association WithUser", "Bug", "Medium", "Partner  Association page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Partner Association WithUser");
                        TakeScreenshot("PartnerAssociationWithUser");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\PartnerAssociationWithUser.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("PartnerAssociationWithUser");
                        string id = loginHelper.getIssueID("Partner Association WithUser");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\PartnerAssociationWithUser.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Partner Association WithUser"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Partner Association WithUser");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("PartnerAssociationWithUser");
                executionLog.WriteInExcel("Partner Association WithUser", Status, JIRA, "Agents Portal");
            }
        }
    }
}