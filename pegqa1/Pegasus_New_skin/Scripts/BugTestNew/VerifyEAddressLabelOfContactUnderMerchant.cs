using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class VerifyEAddressLabelOfContactUnderMerchant : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin")]
        [TestCategory("Fail")]
        [TestCategory("BugTestNew")]
        public void verifyEAddressLabelOfContactUnderMerchant()
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


            var DBA = "ClientDBA" + RandomNumber(111, 999999);
            var contactname = "Contact" + DBA;

            // Variable random
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("VerifyEAddressLabelOfContactUnderMerchant", "Login with valid username and password");
                Login(username[0], password[0]);

                executionLog.Log("VerifyEAddressLabelOfContactUnderMerchant", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("VerifyEAddressLabelOfContactUnderMerchant", "Redirect at Create merchant page");
                VisitOffice("clients/create");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyEAddressLabelOfContactUnderMerchant", "Enter DBA name");
                office_ClientsHelper.TypeText("ClientDBAName", DBA);

                executionLog.Log("VerifyEAddressLabelOfContactUnderMerchant", "Enter First and Last name");
                office_ClientsHelper.TypeText("FirstName", contactname);
                office_ClientsHelper.TypeText("LastName", "Contact");

                executionLog.Log("VerifyEAddressLabelOfContactUnderMerchant", "Select the client status");
                office_ClientsHelper.SelectByText("Status", "New");

                executionLog.Log("VerifyEAddressLabelOfContactUnderMerchant", "select the responsibity");
                office_ClientsHelper.SelectByText("Responsibility", "Howard Tang");

                executionLog.Log("VerifyEAddressLabelOfContactUnderMerchant", "Click on save btn");
                office_ClientsHelper.ClickElement("Save");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyEAddressLabelOfContactUnderMerchant", "Go to Contacts tab");
                office_ClientsHelper.ClickElement("ContactDetails");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyEAddressLabelOfContactUnderMerchant", "Select eAddress Type >> Email");
                office_ClientsHelper.SelectByText("Contact1eAddressType1", "E-Mail");
                office_ClientsHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyEAddressLabelOfContactUnderMerchant", "Select eAddress Label >> Business");
                office_ClientsHelper.SelectByText("Contact1eAddressLabel1", "Business");

                executionLog.Log("VerifyEAddressLabelOfContactUnderMerchant", "Enter eAddress");
                office_ClientsHelper.TypeText("Contact1eAddress1", "testing@yopmail.com");

                executionLog.Log("VerifyEAddressLabelOfContactUnderMerchant", "Click on Save button");
                office_ClientsHelper.ClickElement("ContactSave");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyEAddressLabelOfContactUnderMerchant", "Verify eAddress Label is Business");
                office_ClientsHelper.verifyselectedOptn("Contact1eAddressLabel1", "Business");

                executionLog.Log("VerifyEAddressLabelOfContactUnderMerchant", "Redirect at All merchants page");
                VisitOffice("clients");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyEAddressLabelOfContactUnderMerchant", "Search the company Name");
                office_ClientsHelper.TypeText("SearchClient", DBA);
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyEAddressLabelOfContactUnderMerchant", "Click on check box");
                office_ClientsHelper.ClickElement("ClickOnCheckBox");

                executionLog.Log("VerifyEAddressLabelOfContactUnderMerchant", "Delete the client");
                office_ClientsHelper.ClickJS("DeleteClient");
                office_ClientsHelper.AcceptAlert();
                office_ClientsHelper.WaitForWorkAround(3000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyEAddressLabelOfContactUnderMerchant");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify EAddress Label Of Contact Under Merchant");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify EAddress Label Of Contact Under Merchant", "Bug", "Medium", "Office Merchant page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify EAddress Label Of Contact Under Merchant");
                        TakeScreenshot("VerifyEAddressLabelOfContactUnderMerchant");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyEAddressLabelOfContactUnderMerchant.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyEAddressLabelOfContactUnderMerchant");
                        string id = loginHelper.getIssueID("Verify EAddress Label Of Contact Under Merchant");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyEAddressLabelOfContactUnderMerchant.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify EAddress Label Of Contact Under Merchant"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify EAddress Label Of Contact Under Merchant");
                //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyEAddressLabelOfContactUnderMerchant");
                executionLog.WriteInExcel("Verify EAddress Label Of Contact Under Merchant", Status, JIRA, "Office Merchants");
            }
        }
    }
}
