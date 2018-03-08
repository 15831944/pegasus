using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class VerifyingPartnerAssociationModiifedCredentials : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS3")]
        [TestCategory("BugTestNew")]
        public void verifyingPartnerAssociationModiifedCredentials()
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
            var username1 = "testinguser" + RandomNumber(111,99999);
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("VerifyingPartnerAssociationModiifedCredentials", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyingPartnerAssociationModiifedCredentials", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("VerifyingPartnerAssociationModiifedCredentials", "Redirect to create partner agent");
                VisitOffice("partners/association/create");
                agent_PartnerAssociationHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyingPartnerAssociationModiifedCredentials", "Enter Association name");
                agent_PartnerAssociationHelper.TypeText("Name", "AssociationTester");

                executionLog.Log("VerifyingPartnerAssociationModiifedCredentials", "Enter DBAName");
                agent_PartnerAssociationHelper.TypeText("DBAName", "Test DBA");

                executionLog.Log("VerifyingPartnerAssociationModiifedCredentials", "Select Salutation");
                agent_PartnerAssociationHelper.Select("SelectSalutation", "Mr");

                executionLog.Log("VerifyingPartnerAssociationModiifedCredentials", "Enter FirstNAME");
                agent_PartnerAssociationHelper.TypeText("FirstNAME", "Test Agent");

                executionLog.Log("VerifyingPartnerAssociationModiifedCredentials", "Enter LastName");
                agent_PartnerAssociationHelper.TypeText("LastName", "Tester");

                executionLog.Log("VerifyingPartnerAssociationModiifedCredentials", "Enter TwitterURL");
                agent_PartnerAssociationHelper.TypeText("TwitterURL", "Twitter.com");

                executionLog.Log("VerifyingPartnerAssociationModiifedCredentials", "Enter the DOB");
                agent_PartnerAssociationHelper.TypeText("PartnerAssoBirthday", "12/14/1993");

                executionLog.Log("VerifyingPartnerAssociationModiifedCredentials", "Select Language");
                agent_PartnerAssociationHelper.Select("SelectLanguage", "English");

                executionLog.Log("VerifyingPartnerAssociationModiifedCredentials", "Select eAddressType");
                agent_PartnerAssociationHelper.Select("eAddressType", "E-Mail");

                executionLog.Log("VerifyingPartnerAssociationModiifedCredentials", "Select eAddressLebel");
                agent_PartnerAssociationHelper.Select("eAddressLebel", "Work");

                executionLog.Log("VerifyingPartnerAssociationModiifedCredentials", "Enter eAddressType");
                agent_PartnerAssociationHelper.TypeText("eAddress", "Test@gmail.com");

                executionLog.Log("VerifyingPartnerAssociationModiifedCredentials", "Select SelectPhoneType");
                agent_PartnerAssociationHelper.Select("SelectPhoneType", "Work");

                executionLog.Log("VerifyingPartnerAssociationModiifedCredentials", "Select Address Type ");
                agent_PartnerAssociationHelper.Select("AddressType", "Office");

                executionLog.Log("VerifyingPartnerAssociationModiifedCredentials", "Enter AddressLine1");
                agent_PartnerAssociationHelper.TypeText("AddressLine1", "FC 89");

                executionLog.Log("VerifyingPartnerAssociationModiifedCredentials", "Enter PhoneNumber");
                agent_PartnerAssociationHelper.TypeText("PostalCode", "60601");
                agent_PartnerAssociationHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyingPartnerAssociationModiifedCredentials", "Enter Username");
                agent_PartnerAssociationHelper.TypeText("UserName", username1);

                executionLog.Log("VerifyingPartnerAssociationModiifedCredentials", "Select Avatar");
                agent_PartnerAssociationHelper.ClickElement("ClickOnAvatar");

                executionLog.Log("VerifyingPartnerAssociationModiifedCredentials", "Click on Save");
                agent_PartnerAssociationHelper.ClickElement("ClickSaveBTN");
                agent_PartnerAssociationHelper.WaitForWorkAround(3000);

                agent_PartnerAssociationHelper.VerifyText("VerifyTextPresent", "Partner Associations");
                agent_PartnerAssociationHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyingPartnerAssociationModiifedCredentials", "Verify Modified By credentials");
                agent_PartnerAssociationHelper.VerifyText("ModifiedOn", "By Howard Tang");
                agent_PartnerAssociationHelper.WaitForWorkAround(3000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyingPartnerAssociationModiifedCredentials");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verifying Partner Association Modiifed Credentials");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verifying Partner Association Modiifed Credentials", "Bug", "Medium", "Partner Association page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verifying Partner Association Modiifed Credentials");
                        TakeScreenshot("VerifyingPartnerAssociationModiifedCredentials");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyingPartnerAssociationModiifedCredentials.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyingPartnerAssociationModiifedCredentials");
                        string id = loginHelper.getIssueID("Verifying Partner Association Modiifed Credentials");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyingPartnerAssociationModiifedCredentials.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verifying Partner Association Modiifed Credentials"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verifying Partner Association Modiifed Credentials");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyingPartnerAssociationModiifedCredentials");
                executionLog.WriteInExcel("Verifying Partner Association Modiifed Credentials", Status, JIRA, "Agents Portal");
            }
        }
    }
}