using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class VerifyEadressLabelNotChangedAutomatically : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("Temp")]
        public void verifyEadressLabelNotChangedAutomatically()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_corp");
            password = oXMLData.getData("settings/Credentials", "password2");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var corpOffice_OfficeHelper = new CorpOffice_OfficeHelper(GetWebDriver());

            // Variable random
            var usernme = "Sysprins" + RandomNumber(44, 777);
            var name = "Test" + RandomNumber(99, 999);
            String JIRA = "";
            String Status = "Pass";

          try
            {

            executionLog.Log("VerifyEadressLabelNotChangedAutomatically", "Login with valid username and password");
            Login(username[0], password[0]);

            executionLog.Log("VerifyEadressLabelNotChangedAutomatically", "Verify Page title");
            VerifyTitle("Dashboard");

            executionLog.Log("VerifyEadressLabelNotChangedAutomatically", "Visit Corp offices");
            VisitCorp("offices");

            executionLog.Log("VerifyEadressLabelNotChangedAutomatically", "Click on create office button");
            corpOffice_OfficeHelper.ClickElement("Create");
            corpOffice_OfficeHelper.WaitForWorkAround(3000);

            executionLog.Log("VerifyEadressLabelNotChangedAutomatically", "Enter Name");
            corpOffice_OfficeHelper.TypeText("Name", name);

            executionLog.Log("VerifyEadressLabelNotChangedAutomatically", "Enter DBAName");
            corpOffice_OfficeHelper.TypeText("DBAName", "TEST123");

            executionLog.Log("VerifyEadressLabelNotChangedAutomatically", "Enter Website");
            corpOffice_OfficeHelper.TypeText("Website", "TEST.COM");

            executionLog.Log("VerifyEadressLabelNotChangedAutomatically", "Enter OfficeCode");
            corpOffice_OfficeHelper.TypeText("OfficeCode", "12345");

            executionLog.Log("VerifyEadressLabelNotChangedAutomatically", "Enter VenderName");
            corpOffice_OfficeHelper.TypeText("VendorName", "VenderTEST");

            executionLog.Log("VerifyEadressLabelNotChangedAutomatically", "Enter VenderName");
            corpOffice_OfficeHelper.TypeText("VendorCode", "1234");

            executionLog.Log("VerifyEadressLabelNotChangedAutomatically", "Enter VenderName");
            corpOffice_OfficeHelper.TypeText("OfficePhoneNumber", "1234567890");

            executionLog.Log("VerifyEadressLabelNotChangedAutomatically", "Enter VenderName");
            corpOffice_OfficeHelper.TypeText("BusinessPhoneNumber", "1234567890");

            executionLog.Log("VerifyEadressLabelNotChangedAutomatically", "Enter VenderName");
            corpOffice_OfficeHelper.TypeText("FaxNumber", "1234567890");

            executionLog.Log("VerifyEadressLabelNotChangedAutomatically", "Enter VenderName");
            corpOffice_OfficeHelper.TypeText("LinkedURL", "Linked.com");

            executionLog.Log("VerifyEadressLabelNotChangedAutomatically", "Enter VenderName");
            corpOffice_OfficeHelper.TypeText("FacebookURL", "Facebook.com");

            executionLog.Log("VerifyEadressLabelNotChangedAutomatically", "Enter TwitterURL");
            corpOffice_OfficeHelper.TypeText("TwitterURL", "Twitter.com");

            executionLog.Log("VerifyEadressLabelNotChangedAutomatically", "Select Address");
            corpOffice_OfficeHelper.Select("AddressType", "Office");

            executionLog.Log("VerifyEadressLabelNotChangedAutomatically", "Enter AddressLine1");
            corpOffice_OfficeHelper.TypeText("AddressLine1", "FC-89");

            executionLog.Log("VerifyEadressLabelNotChangedAutomatically", "Select Zip Code");
            corpOffice_OfficeHelper.TypeText("ZIpCode", "60601");
            corpOffice_OfficeHelper.WaitForWorkAround(4000);

            executionLog.Log("VerifyEadressLabelNotChangedAutomatically", "Enter PrimaryUserName");
            corpOffice_OfficeHelper.TypeText("PrimaryUserName", usernme);

            executionLog.Log("VerifyEadressLabelNotChangedAutomatically", "Click on AutoGenPassword checkbox");
            corpOffice_OfficeHelper.ClickElement("AutoGenPassword");
            corpOffice_OfficeHelper.WaitForWorkAround(1000);

            executionLog.Log("VerifyEadressLabelNotChangedAutomatically", "Enter PrimaryPassword");
            corpOffice_OfficeHelper.TypeText("PrimaryPassword", "1qaz!QAZ");

            executionLog.Log("VerifyEadressLabelNotChangedAutomatically", "Select Salutation");
            corpOffice_OfficeHelper.Select("Salutation", "Mr");
            corpOffice_OfficeHelper.WaitForWorkAround(2000);

            executionLog.Log("VerifyEadressLabelNotChangedAutomatically", "Enter FirstName");
            corpOffice_OfficeHelper.TypeText("FirstName", "Test");

            executionLog.Log("VerifyEadressLabelNotChangedAutomatically", "Enter LastName");
            corpOffice_OfficeHelper.TypeText("LastName", "Tester");

            executionLog.Log("VerifyEadressLabelNotChangedAutomatically", "Select eAddress type as e-mail.");
            corpOffice_OfficeHelper.Select("EaddressType", "E-Mail");

            executionLog.Log("VerifyEadressLabelNotChangedAutomatically", "Select e-Address label as home.");
            corpOffice_OfficeHelper.Select("EaddressLabel", "Home");

            executionLog.Log("VerifyEadressLabelNotChangedAutomatically", "Enter eAddress");
            corpOffice_OfficeHelper.TypeText("eAddress", "NewTest@yopmail.com");

            executionLog.Log("VerifyEadressLabelNotChangedAutomatically", "Click on save button.");
            corpOffice_OfficeHelper.ClickElement("Save");

            executionLog.Log("VerifyEadressLabelNotChangedAutomatically", "Wait for success message");
            corpOffice_OfficeHelper.WaitForText("Office created successfully.", 10);

            executionLog.Log("VerifyEadressLabelNotChangedAutomatically", "Go to office page");
            VisitCorp("offices");

            executionLog.Log("VerifyEadressLabelNotChangedAutomatically", "Verify title");
            VerifyTitle("Offices");

            executionLog.Log("VerifyEadressLabelNotChangedAutomatically", "Enter Name to search");
            corpOffice_OfficeHelper.TypeText("EnterSelenium", name);
            corpOffice_OfficeHelper.WaitForWorkAround(5000);

            executionLog.Log("VerifyEadressLabelNotChangedAutomatically", "Click on edit icon.");
            corpOffice_OfficeHelper.ClickElement("EditOffice");

            executionLog.Log("VerifyEadressLabelNotChangedAutomatically", "Wait for locator to present.");
            corpOffice_OfficeHelper.WaitForElementPresent("EaddressLabel", 10);

            executionLog.Log("VerifyEadressLabelNotChangedAutomatically", "Verify eaddress label not changed to work.");
            corpOffice_OfficeHelper.VerifyText("EaddressLabel", "Home");

            executionLog.Log("VerifyEadressLabelNotChangedAutomatically", "Click on save.");
            corpOffice_OfficeHelper.ClickElement("SaveEdit");

            executionLog.Log("VerifyEadressLabelNotChangedAutomatically", "Wait for updation success text.");
            corpOffice_OfficeHelper.WaitForText("Office updated successfully.", 10);

            executionLog.Log("VerifyEadressLabelNotChangedAutomatically", "Go to office page");
            VisitCorp("offices");

            executionLog.Log("VerifyEadressLabelNotChangedAutomatically", "Verify title");
            VerifyTitle("Offices");

            executionLog.Log("VerifyEadressLabelNotChangedAutomatically", "Enter Name to search");
            corpOffice_OfficeHelper.TypeText("EnterSelenium", name);
            corpOffice_OfficeHelper.WaitForWorkAround(5000);

            executionLog.Log("VerifyEadressLabelNotChangedAutomatically", "Click Delete btn  ");
            corpOffice_OfficeHelper.ClickElement("DeleteOffice");

            executionLog.Log("VerifyEadressLabelNotChangedAutomatically", "Verify page text");
            corpOffice_OfficeHelper.VerifyPageText("Are you sure want to delete the");

            executionLog.Log("VerifyEadressLabelNotChangedAutomatically", "Click Delete btn  ");
            corpOffice_OfficeHelper.ClickElement("ConfirmDelete");

            executionLog.Log("VerifyEadressLabelNotChangedAutomatically", "Accept alert message. ");
            corpOffice_OfficeHelper.AcceptAlert();

            executionLog.Log("VerifyEadressLabelNotChangedAutomatically", "Wait for delete message. ");
            corpOffice_OfficeHelper.WaitForText("Office deleted successfully.", 20);

        }
        catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyEadressLabelNotChangedAutomatically");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Eadress Label Not Changed Automatically");
                if (!result)
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Eadress Label Not Changed Automatically", "Bug", "Medium", "Create Office page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Eadress Label Not Changed Automatically");
                        TakeScreenshot("VerifyEadressLabelNotChangedAutomatically");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyEadressLabelNotChangedAutomatically.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyEadressLabelNotChangedAutomatically");
                        string id = loginHelper.getIssueID("Verify Eadress Label Not Changed Automatically");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyEadressLabelNotChangedAutomatically.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Eadress Label Not Changed Automatically"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Eadress Label Not Changed Automatically");
                executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyEadressLabelNotChangedAutomatically");
                executionLog.WriteInExcel("Verify Eadress Label Not Changed Automatically", Status, JIRA, "Corp Office");
            }
        }
    }
}