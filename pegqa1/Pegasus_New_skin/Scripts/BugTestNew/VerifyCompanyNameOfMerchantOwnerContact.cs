using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class VerifyCompanyNameOfMerchantOwnerContact : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin")]
        [TestCategory("Fail")]
        [TestCategory("BugTestNew")]
        public void verifyCompanyNameOfMerchantOwnerContact()
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
            var office_ClientsHelper = new Office_ClientsHelper(GetWebDriver());
            var office_ContactsHelper = new Office_ContactsHelper(GetWebDriver());


            var DBA = "ClientDBA" + RandomNumber(111, 999999);
            var ownername = "Owner" + DBA;

            // Variable random
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("VerifyCompanyNameOfMerchantOwnerContact", "Login with valid username and password");
                Login(username[0], password[0]);

                executionLog.Log("VerifyCompanyNameOfMerchantOwnerContact", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("VerifyCompanyNameOfMerchantOwnerContact", "Redirect at Create merchant page");
                VisitOffice("clients/create");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCompanyNameOfMerchantOwnerContact", "Enter DBA name");
                office_ClientsHelper.TypeText("ClientDBAName", DBA);

                executionLog.Log("VerifyCompanyNameOfMerchantOwnerContact", "Select the client status");
                office_ClientsHelper.SelectByText("Status", "New");

                executionLog.Log("VerifyCompanyNameOfMerchantOwnerContact", "select the responsibity");
                office_ClientsHelper.SelectByText("Responsibility", "Howard Tang");

                executionLog.Log("VerifyCompanyNameOfMerchantOwnerContact", "Click on save btn");
                office_ClientsHelper.ClickElement("Save");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCompanyNameOfMerchantOwnerContact", "Go to Owners tab");
                office_ClientsHelper.ClickElement("OwnerTab");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCompanyNameOfMerchantOwnerContact", "Enter Title");
                office_ClientsHelper.TypeText("TitleOwner", "Title");

                executionLog.Log("VerifyCompanyNameOfMerchantOwnerContact", "Enter Owner First Name");
                office_ClientsHelper.TypeText("OwnerFirstName", ownername);

                executionLog.Log("VerifyCompanyNameOfMerchantOwnerContact", "Enter Owner Last Name");
                office_ClientsHelper.TypeText("OwnerLastName", "Owner");

                executionLog.Log("VerifyCompanyNameOfMerchantOwnerContact", "Click on Save button");
                office_ClientsHelper.ClickElement("OwnerSave");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCompanyNameOfMerchantOwnerContact", "Redirect at All Contacts page");
                VisitOffice("contacts");
                office_ContactsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCompanyNameOfMerchantOwnerContact", "Search Contact Name");
                office_ContactsHelper.TypeText("SearchName", ownername);
                office_ContactsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyCompanyNameOfMerchantOwnerContact", "Verify Company name");
                office_ContactsHelper.VerifyText("FirstCompName", DBA);

                executionLog.Log("VerifyCompanyNameOfMerchantOwnerContact", "Redirect at All merchants page");
                VisitOffice("clients");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCompanyNameOfMerchantOwnerContact", "Search the company Name");
                office_ClientsHelper.TypeText("SearchClient", DBA);
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyCompanyNameOfMerchantOwnerContact", "Click on check box");
                office_ClientsHelper.ClickElement("ClickOnCheckBox");

                executionLog.Log("VerifyCompanyNameOfMerchantOwnerContact", "Delete the client");
                office_ClientsHelper.ClickJS("DeleteClient");
                office_ClientsHelper.AcceptAlert();
                office_ClientsHelper.WaitForWorkAround(3000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyCompanyNameOfMerchantOwnerContact");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Company Name Of Merchant Owner Contact");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Company Name Of Merchant Owner Contact", "Bug", "Medium", "Office Merchant page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Company Name Of Merchant Owner Contact");
                        TakeScreenshot("VerifyCompanyNameOfMerchantOwnerContact");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyCompanyNameOfMerchantOwnerContact.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyCompanyNameOfMerchantOwnerContact");
                        string id = loginHelper.getIssueID("Verify Company Name Of Merchant Owner Contact");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyCompanyNameOfMerchantOwnerContact.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Company Name Of Merchant Owner Contact"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Company Name Of Merchant Owner Contact");
                //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyCompanyNameOfMerchantOwnerContact");
                executionLog.WriteInExcel("Verify Company Name Of Merchant Owner Contact", Status, JIRA, "Office Merchants");
            }
        }
    }
}
