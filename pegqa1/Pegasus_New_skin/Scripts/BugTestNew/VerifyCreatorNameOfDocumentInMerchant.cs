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
    public class VerifyCreatorNameOfDocumentInMerchant : DriverTestCase
    {
        [TestMethod]
        [TestCategory("Bug")]
        [TestCategory("TS2")]
        [TestCategory("BugTestNew")]
        public void verifyCreatorNameOfDocumentInMerchant()
        {
            string[] username1 = null;
            string[] username = null;
            string[] password = null;

            XMLParse oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_corp");
            username1 = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var corp_MerchantHelper = new Corp_MerchantHelper(GetWebDriver());
            var office_clientsHelper = new Office_ClientsHelper(GetWebDriver());
            var myprofilepagehelper = new MyProfilePageHelper(GetWebDriver());
            var officeActivities_DocumentHelper = new OfficeActivities_DocumentHelper(GetWebDriver());

            // variables
            String JIRA = "";
            String Status = "Pass";
            var docname = "TestDoc" + RandomNumber(111,999999);
            try
            {
                executionLog.Log("VerifyCreatorNameOfDocumentInMerchant", "Login to office portal with valid username and password");
                Login(username1[0], password[0]);
                Console.WriteLine("Logged in as: " + username1[0] + " / " + password[0]);

                executionLog.Log("VerifyCreatorNameOfDocumentInMerchant", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyCreatorNameOfDocumentInMerchant", "Redirect at My Profile page.");
                VisitOffice("myprofile");

                executionLog.Log("VerifyCreatorNameOfDocumentInMerchant", "Click on Edit profile Button.");
                myprofilepagehelper.ClickElement("EditProfileBtn");
                myprofilepagehelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyCreatorNameOfDocumentInMerchant", "Get First Name of User");
                var name = myprofilepagehelper.GetValue("//input[@id='EmployeeFirstName']");

                executionLog.Log("VerifyCreatorNameOfDocumentInMerchant", "Redirect at merchants page.");
                VisitOffice("clients");
                office_clientsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyCreatorNameOfDocumentInMerchant", "Click on a client");
                var clientname = office_clientsHelper.GetText("//table[@id='list1']/tbody/tr[2]/td[15]/a");
                office_clientsHelper.ClickElement("Client1");
                office_clientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCreatorNameOfDocumentInMerchant", "Click on New Task");
                office_clientsHelper.ClickElement("AddDoc");
                office_clientsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyCreatorNameOfDocumentInMerchant", "Enter document name");
                officeActivities_DocumentHelper.TypeText("Name", docname);

                executionLog.Log("VerifyCreatorNameOfDocumentInMerchant", "Upload file");
                var path = GetPathToFile() + "Desert.jpg";
                officeActivities_DocumentHelper.UploadFile("//*[@id='DocumentFiles']", path);
                officeActivities_DocumentHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyCreatorNameOfDocumentInMerchant", "Click on Save button");
                officeActivities_DocumentHelper.ClickElement("ClientPopupSave");
                officeActivities_DocumentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCreatorNameOfDocumentInMerchant", "Logout from office portal");
                VisitOffice("logout");
                office_clientsHelper.WaitForWorkAround(2000); 

                executionLog.Log("VerifyCreatorNameOfDocumentInMerchant", "Login to corporate portal with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyCreatorNameOfDocumentInMerchant", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyCreatorNameOfDocumentInMerchant", "Redirect at merchants page.");
                VisitCorp("merchants");
                corp_MerchantHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyCreatorNameOfDocumentInMerchant", "Enter client name in search box");
                corp_MerchantHelper.TypeText("EnterClinentToSearch", clientname);
                corp_MerchantHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyCreatorNameOfDocumentInMerchant", "Click on Merchant");
                corp_MerchantHelper.ClickElement("ClickOnMerchant");
                corp_MerchantHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyCreatorNameOfDocumentInMerchant", "Select Activity Type >> Documents");
                corp_MerchantHelper.Select("SelectActivityType", "Documents");
                corp_MerchantHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyCreatorNameOfDocumentInMerchant", "Enter document name");
                corp_MerchantHelper.TypeText("SearchActivityName", docname);
                corp_MerchantHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyCreatorNameOfDocumentInMerchant", "Click on document");
                corp_MerchantHelper.ClickElement("ClickOnActivityAny");
                corp_MerchantHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCreatorNameOfDocumentInMerchant", "Verify Created By name is appearing");
                corp_MerchantHelper.VerifyText("CreatedOnText", name);
                Console.WriteLine("Creator Name of Document is "+name);
                

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyCreatorNameOfDocumentInMerchant");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Creator Name Of Document In Merchant");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Creator Name Of Document In Merchant", "Bug", "Medium", "Merchant corp", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Creator Name Of Document In Merchant");
                        TakeScreenshot("VerifyCreatorNameOfDocumentInMerchant");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyCreatorNameOfDocumentInMerchant.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyCreatorNameOfDocumentInMerchant");
                        string id = loginHelper.getIssueID("Verify Creator Name Of Document In Merchant");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyCreatorNameOfDocumentInMerchant.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Creator Name Of Document In Merchant"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Creator Name Of Document In Merchant");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyCreatorNameOfDocumentInMerchant");
                executionLog.WriteInExcel("Verify Creator Name Of Document In Merchant", Status, JIRA, "Corp Merchant");
            }
        }
    }
}