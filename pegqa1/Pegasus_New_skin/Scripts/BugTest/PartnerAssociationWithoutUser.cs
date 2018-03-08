using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class PartnerAssociationWithoutUser : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void partnerAssociationWithoutUser()
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
            var agent_PartnerAssociationHelper = new Agents_PartnerAssociationHelper(GetWebDriver());

            // Variable
            var name = "TestAgent" + GetRandomNumber();
            var username1 = "testuser" + RandomNumber(111,99999);
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("PartnerAssociationWithoutUser", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("PartnerAssociationWithoutUser", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("PartnerAssociationWithoutUser", "Redirect to create partner agent");
                VisitOffice("partners/association/create");
                agent_PartnerAssociationHelper.WaitForWorkAround(3000);

                executionLog.Log("PartnerAssociationWithoutUser", "Enter Association name");
                agent_PartnerAssociationHelper.TypeText("Name", "AssociationTester");

                executionLog.Log("PartnerAssociationWithoutUser", "Enter DBAName");
                agent_PartnerAssociationHelper.TypeText("DBAName", "Test DBA");

                executionLog.Log("PartnerAssociationWithoutUser", "Select Salutation");
                agent_PartnerAssociationHelper.Select("SelectSalutation", "Mr");

                executionLog.Log("PartnerAssociationWithoutUser", "Enter FirstNAME");
                agent_PartnerAssociationHelper.TypeText("FirstNAME", "Test Agent");

                executionLog.Log("PartnerAssociationWithoutUser", "Enter LastName");
                agent_PartnerAssociationHelper.TypeText("LastName", "Tester");

                executionLog.Log("PartnerAssociationWithoutUser", "Enter the DOB");
                agent_PartnerAssociationHelper.TypeText("PartnerAssoBirthday", "12/04/1992");

                executionLog.Log("PartnerAssociationWithoutUser", "Enter TwitterURL");
                agent_PartnerAssociationHelper.TypeText("TwitterURL", "Twitter.com");

                executionLog.Log("PartnerAssociationWithoutUser", "Select Language");
                agent_PartnerAssociationHelper.Select("SelectLanguage", "English");

                executionLog.Log("PartnerAssociationWithoutUser", "Select eAddressType");
                agent_PartnerAssociationHelper.Select("eAddressType", "E-Mail");

                executionLog.Log("PartnerAssociationWithoutUser", "Select eAddressLebel");
                agent_PartnerAssociationHelper.Select("eAddressLebel", "Work");

                executionLog.Log("PartnerAssociationWithoutUser", "Enter eAddressType");
                agent_PartnerAssociationHelper.TypeText("eAddress", "Test@gmail.com");

                executionLog.Log("PartnerAssociationWithoutUser", "Select SelectPhoneType");
                agent_PartnerAssociationHelper.Select("SelectPhoneType", "Work");

                executionLog.Log("PartnerAssociationWithoutUser", "Select Address Type ");
                agent_PartnerAssociationHelper.Select("AddressType", "Office");

                executionLog.Log("PartnerAssociationWithoutUser", "Enter AddressLine1");
                agent_PartnerAssociationHelper.TypeText("AddressLine1", "FC 89");

                executionLog.Log("PartnerAssociationWithoutUser", "Enter Zip  Code");
                agent_PartnerAssociationHelper.TypeText("PostalCode", "60601");
                agent_PartnerAssociationHelper.WaitForWorkAround(2000);

                executionLog.Log("PartnerAgentBirthDateVerifySave", "Enter Username");
                agent_PartnerAssociationHelper.TypeText("UserName", username1);

                executionLog.Log("PartnerAgentBirthDateVerifySave", "Select Avatar Check Box");
                agent_PartnerAssociationHelper.ClickElement("ClickOnAvatar");

                executionLog.Log("PartnerAssociationWithoutUser", "Click on Save btn");
                agent_PartnerAssociationHelper.ClickElement("ClickSaveBTN");
                agent_PartnerAssociationHelper.WaitForWorkAround(3000);

                executionLog.Log("PartnerAssociationWithoutUser", "Verify tex.");
                agent_PartnerAssociationHelper.VerifyText("VerifyTextPresent", "Partner Associations");
                agent_PartnerAssociationHelper.WaitForWorkAround(3000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";
                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("PartnerAssociationWithoutUser");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Partner Association Without User");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Partner Association Without User", "Bug", "Medium", "Partner Association page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Partner Association Without User");
                        TakeScreenshot("PartnerAssociationWithoutUser");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\PartnerAssociationWithoutUser.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("PartnerAssociationWithoutUser");
                        string id = loginHelper.getIssueID("Partner Association Without User");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\PartnerAssociationWithoutUser.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Partner Association Without User"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Partner Association Without User");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("PartnerAssociationWithoutUser");
                executionLog.WriteInExcel("Partner Association Without User", Status, JIRA, "Agents Portal");
            }
        }
    }
}