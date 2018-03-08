using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class CreateOfficeNewSkin : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("Fail")]
        [TestCategory("TS4")]
        [TestCategory("NewSkinTaskJira")]
        public void createOfficeNewSkin()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_corp");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var corpOffice_OfficeHelper = new CorpOffice_OfficeHelper(GetWebDriver());

            // Variable random
            var usernme = "Sysprins" + RandomNumber(44, 99999);
            var name = "Test" + RandomNumber(999, 999999);
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("CreateOfficeNewSkin", "Login with valid username and password");
                Login(username[0], password[0]);

                executionLog.Log("CreateOfficeNewSkin", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("CreateOfficeNewSkin", "Visit Corp offices");
                VisitCorp("offices");
                corpOffice_OfficeHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateOfficeNewSkin", "Click on create office button");
                corpOffice_OfficeHelper.ClickElement("Create");
                corpOffice_OfficeHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateOfficeNewSkin", "Enter Name");
                corpOffice_OfficeHelper.TypeText("Name", name);

                executionLog.Log("CreateOfficeNewSkin", "Enter DBAName");
                corpOffice_OfficeHelper.TypeText("DBAName", "TEST123");

                executionLog.Log("CreateOfficeNewSkin", "Enter Website");
                corpOffice_OfficeHelper.TypeText("Website", "TEST.COM");

                executionLog.Log("CreateOfficeNewSkin", "Enter OfficeCode");
                corpOffice_OfficeHelper.TypeText("OfficeCode", "12345");

                executionLog.Log("CreateOfficeNewSkin", "Enter VenderName");
                corpOffice_OfficeHelper.TypeText("VendorName", "VenderTEST");

                executionLog.Log("CreateOfficeNewSkin", "Enter VenderName");
                corpOffice_OfficeHelper.TypeText("VendorCode", "1234");

                executionLog.Log("CreateOfficeNewSkin", "Enter VenderName");
                corpOffice_OfficeHelper.TypeText("OfficePhoneNumber", "1234567890");

                executionLog.Log("CreateOfficeNewSkin", "Enter VenderName");
                corpOffice_OfficeHelper.TypeText("BusinessPhoneNumber", "1234567890");

                executionLog.Log("CreateOfficeNewSkin", "Enter VenderName");
                corpOffice_OfficeHelper.TypeText("FaxNumber", "1234567890");

                executionLog.Log("CreateOfficeNewSkin", "Enter VenderName");
                corpOffice_OfficeHelper.TypeText("LinkedURL", "Linked.com");

                executionLog.Log("CreateOfficeNewSkin", "Enter VenderName");
                corpOffice_OfficeHelper.TypeText("FacebookURL", "Facebook.com");

                executionLog.Log("CreateOfficeNewSkin", "Enter TwitterURL");
                corpOffice_OfficeHelper.TypeText("TwitterURL", "Twitter.com");

                executionLog.Log("CreateOfficeNewSkin", "Select Address");
                corpOffice_OfficeHelper.Select("AddressType", "Office");

                executionLog.Log("CreateOfficeNewSkin", "Enter AddressLine1");
                corpOffice_OfficeHelper.TypeText("AddressLine1", "FC-89");

                executionLog.Log("CreateOfficeNewSkin", "Select Zip Code");
                corpOffice_OfficeHelper.TypeText("ZIpCode", "60601");
                corpOffice_OfficeHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateOfficeNewSkin", "Enter PrimaryUserName");
                corpOffice_OfficeHelper.TypeText("PrimaryUserName", usernme);

                executionLog.Log("CreateOfficeNewSkin", "Click on AutoGenPassword checkbox");
                corpOffice_OfficeHelper.ClickElement("AutoGenPassword");
                corpOffice_OfficeHelper.WaitForWorkAround(1000);

                executionLog.Log("CreateOfficeNewSkin", "Enter PrimaryPassword");
                corpOffice_OfficeHelper.TypeText("PrimaryPassword", "1qaz!QAZ");

                executionLog.Log("CreateOfficeNewSkin", "Select Salutation");
                corpOffice_OfficeHelper.Select("Salutation", "Mr");
                //corpOffice_OfficeHelper.WaitForWorkAround(2000);

                executionLog.Log("CreateOfficeNewSkin", "Enter FirstName");
                corpOffice_OfficeHelper.TypeText("FirstName", "Test");

                executionLog.Log("CreateOfficeNewSkin", "Enter LastName");
                corpOffice_OfficeHelper.TypeText("LastName", "Tester");

                executionLog.Log("CreateOfficeNewSkin", "Enter eAddress");
                corpOffice_OfficeHelper.TypeText("eAddress", "NewTest@yopmail.com");

                executionLog.Log("CreateOfficeNewSkin", "Click on save button.");
                try
                {
                    corpOffice_OfficeHelper.ClickElement("Save");
                }
                catch (OpenQA.Selenium.WebDriverException)
                { }

                //executionLog.Log("CreateOfficeNewSkin", "Wait for success message");
                //corpOffice_OfficeHelper.WaitForText("Office created successfully.", 60);

                executionLog.Log("CreateOfficeNewSkin", "Go to office page");
                VisitCorp("offices");

                executionLog.Log("CreateOfficeNewSkin", "Verify title");
                VerifyTitle("Offices");
                corpOffice_OfficeHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateOfficeNewSkin", "Enter Name to search");
                corpOffice_OfficeHelper.TypeText("EnterSelenium", name);
                corpOffice_OfficeHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateOfficeNewSkin", "Click on Delete button.");
                corpOffice_OfficeHelper.ClickElement("DeleteOffice");
                corpOffice_OfficeHelper.WaitForWorkAround(2000);

                executionLog.Log("CreateOfficeNewSkin", "Verify page text");
                corpOffice_OfficeHelper.VerifyPageText("Are you sure want to delete the");
                //corpOffice_OfficeHelper.WaitForWorkAround(2000);

                executionLog.Log("CreateOfficeNewSkin", "Click Delete confirm button");
                corpOffice_OfficeHelper.ClickElement("ConfirmDelete");
                corpOffice_OfficeHelper.WaitForWorkAround(1000);

                executionLog.Log("CreateOfficeNewSkin", "Accept alert message.");
                corpOffice_OfficeHelper.AcceptAlert();
                corpOffice_OfficeHelper.WaitForWorkAround(1000);

                executionLog.Log("CreateOfficeNewSkin", "Wait for delete message. ");
                corpOffice_OfficeHelper.WaitForText("Office deleted successfully.", 50);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CreateOfficeNewSkin");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Create Office New Skin");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Create Office New Skin", "Bug", "Medium", "Create Office page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Create Office New Skin");
                        TakeScreenshot("CreateOfficeNewSkin");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateOfficeNewSkin.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CreateOfficeNewSkin");
                        string id = loginHelper.getIssueID("Create Office New Skin");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateOfficeNewSkin.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Create Office New Skin"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Create Office New Skin");
                //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("CreateOfficeNewSkin");
                executionLog.WriteInExcel("Create Office New Skin", Status, JIRA, "Corp Office");
            }
        }
    }
}