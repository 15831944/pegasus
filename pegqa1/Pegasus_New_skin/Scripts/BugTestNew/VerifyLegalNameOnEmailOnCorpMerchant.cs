using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class VerifyLegalNameOnEmailOnCorpMerchant : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin")]
        [TestCategory("Fail")]
        [TestCategory("TS3")]
        [TestCategory("BugTestNew")]
        public void verifyLegalNameOnEmailOnCorpMerchant()
        {
            string[] username = null;
            string[] password = null;
            string[] username1 = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username1 = oXMLData.getData("settings/Credentials", "username_office");
            username = oXMLData.getData("settings/Credentials", "username_corp");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var corp_MerchantHelper = new Corp_MerchantHelper(GetWebDriver());
            var office_ClientsHelper = new Office_ClientsHelper(GetWebDriver());
            var officeActivities_EmailsHelper = new OfficeActivities_EmailsHelper(GetWebDriver());


            var DBA = "ClientDBA" + RandomNumber(111, 999999);
            var email = DBA + "@yopmail.com";

            // Variable random
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("VerifyLegalNameOnEmailOnCorpMerchant", "Login to office portal with valid username and password");
                Login(username1[0], password[0]);

                executionLog.Log("VerifyLegalNameOnEmailOnCorpMerchant", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("VerifyLegalNameOnEmailOnCorpMerchant", "Redirect to Create Merchant page");
                VisitOffice("clients/create");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyLegalNameOnEmailOnCorpMerchant", "Enter DBA name");
                office_ClientsHelper.TypeText("ClientDBAName", DBA);

                executionLog.Log("VerifyLegalNameOnEmailOnCorpMerchant", "Select the client status");
                office_ClientsHelper.SelectByText("Status", "New");

                executionLog.Log("VerifyLegalNameOnEmailOnCorpMerchant", "select the responsibility");
                office_ClientsHelper.SelectByText("Responsibility", "Howard Tang");

                executionLog.Log("VerifyLegalNameOnEmailOnCorpMerchant", "Click on save btn");
                office_ClientsHelper.ClickElement("Save");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyLegalNameOnEmailOnCorpMerchant", "Go to Company Details tab");
                office_ClientsHelper.ClickElement("CompanyDetailsTab");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyLegalNameOnEmailOnCorpMerchant", "Enter Legal Name");
                office_ClientsHelper.TypeText("ClientLegalName", DBA);
                //office_ClientsHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyLegalNameOnEmailOnCorpMerchant", "Click on Save button");
                office_ClientsHelper.ClickElement("CDSave");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyLegalNameOnEmailOnCorpMerchant", "Click on Send Email button");
                office_ClientsHelper.ClickElement("CDSendEmailBtn");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyLegalNameOnEmailOnCorpMerchant", "Enter email in To");
                officeActivities_EmailsHelper.TypeText("To", email);

                executionLog.Log("VerifyLegalNameOnEmailOnCorpMerchant", "Click on Send button");
                officeActivities_EmailsHelper.ClickElement("Send");
                officeActivities_EmailsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyLegalNameOnEmailOnCorpMerchant", "Logout from office portal");
                VisitOffice("logout");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyLegalNameOnEmailOnCorpMerchant", "Login to corp portal with valid username and password");
                Login(username[0], password[0]);

                executionLog.Log("VerifyLegalNameOnEmailOnCorpMerchant", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("VerifyLegalNameOnEmailOnCorpMerchant", "Redirect to All Merchant page");
                VisitCorp("merchants");
                corp_MerchantHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyLegalNameOnEmailOnCorpMerchant", "Redirect to Create Merchant page");
                corp_MerchantHelper.TypeText("EnterClinentToSearch", DBA);
                corp_MerchantHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyLegalNameOnEmailOnCorpMerchant", "Open created merchant");
                corp_MerchantHelper.ClickElement("OpenMerchant");
                corp_MerchantHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyLegalNameOnEmailOnCorpMerchant", "Open sent email");
                corp_MerchantHelper.ClickElement("ClickOnActivityAny");
                corp_MerchantHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyLegalNameOnEmailOnCorpMerchant", "Verify Legal Name");
                corp_MerchantHelper.VerifyText("LegalName", DBA);
                Console.WriteLine("Legal Name is appearing on View Email page");

                executionLog.Log("VerifyLegalNameOnEmailOnCorpMerchant", "Logout from the corp portal");
                VisitCorp("logout");
                corp_MerchantHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyLegalNameOnEmailOnCorpMerchant", "Login with valid username and password");
                Login(username1[0], password[0]);

                VisitOffice("clients");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyLegalNameOnEmailOnCorpMerchant", "Search the company Name");
                office_ClientsHelper.TypeText("SearchClient", DBA);
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyLegalNameOnEmailOnCorpMerchant", "Click on check box");
                office_ClientsHelper.ClickElement("ClickOnCheckBox");

                executionLog.Log("VerifyLegalNameOnEmailOnCorpMerchant", "Delete the client");
                office_ClientsHelper.ClickJS("DeleteClient");
                office_ClientsHelper.AcceptAlert();
                office_ClientsHelper.WaitForWorkAround(3000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyLegalNameOnEmailOnCorpMerchant");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Legal Name On Email On Corp Merchant");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Legal Name On Email On Corp Merchant", "Bug", "Medium", "Corp Merchant page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Legal Name On Email On Corp Merchant");
                        TakeScreenshot("VerifyLegalNameOnEmailOnCorpMerchant");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyLegalNameOnEmailOnCorpMerchant.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyLegalNameOnEmailOnCorpMerchant");
                        string id = loginHelper.getIssueID("Verify Legal Name On Email On Corp Merchant");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyLegalNameOnEmailOnCorpMerchant.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Legal Name On Email On Corp Merchant"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Legal Name On Email On Corp Merchant");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyLegalNameOnEmailOnCorpMerchant");
                executionLog.WriteInExcel("Verify Legal Name On Email On Corp Merchant", Status, JIRA, "Corp Merchant");
            }
        }
    }
}
