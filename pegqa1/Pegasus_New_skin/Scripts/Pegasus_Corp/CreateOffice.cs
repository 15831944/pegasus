using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class CreateOffice : DriverTestCase
    {
        [TestMethod]
        [TestCategory("Corp")]
        [TestCategory("All")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Corp")]
        public void createOffice()
        {
            string[] username1 = null;
            string[] password1 = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username1 = oXMLData.getData("settings/Credentials", "username_corp");
            password1 = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var corpOffice_OfficeHelper = new CorpOffice_OfficeHelper(GetWebDriver());

            // Variable random

            var username = "TESTUSER" + GetRandomNumber();
            var name = "Test" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("CreateOffice", "Login with valid username and password");
                Login(username1[0], password1[0]);

                executionLog.Log("CreateOffice", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("CreateOffice", "Go to office page");
                VisitCorp("offices");
                corpOffice_OfficeHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateOffice", "Verify title");
                VerifyTitle("Offices");

                executionLog.Log("CreateOffice", "Click On Create button");
                corpOffice_OfficeHelper.ClickElement("Create");
                corpOffice_OfficeHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateOffice", "Verify title");
                VerifyTitle("Create an Office");

                executionLog.Log("CreateOffice", "Enter Name");
                corpOffice_OfficeHelper.TypeText("Name", name);

                executionLog.Log("CreateOffice", "Enter DBAName");
                corpOffice_OfficeHelper.TypeText("DBAName", "TEST123");

                executionLog.Log("CreateOffice", "Enter Website");
                corpOffice_OfficeHelper.TypeText("Website", "TEST.COM");

                executionLog.Log("CreateOffice", "Enter OfficeCode");
                corpOffice_OfficeHelper.TypeText("OfficeCode", "12345");

                executionLog.Log("CreateOffice", "Select Address");
                corpOffice_OfficeHelper.Select("AddressType", "Office");

                executionLog.Log("CreateOffice", "Enter AddressLine1");
                corpOffice_OfficeHelper.TypeText("AddressLine1", "FC-89");

                executionLog.Log("CreateOffice", "Enter ZipCode");
                corpOffice_OfficeHelper.TypeText("ZIpCode", "60601");

                executionLog.Log("CreateOffice", "Enter PrimaryUserName");
                corpOffice_OfficeHelper.TypeText("PrimaryUserName", username);

                executionLog.Log("CreateOffice", "Click on AutoGenPassword checkbox");
                corpOffice_OfficeHelper.ClickElement("AutoGenPassword");

                executionLog.Log("CreateOffice", "Enter PrimaryPassword");
                corpOffice_OfficeHelper.TypeText("PrimaryPassword", "pegasus");

                executionLog.Log("CreateOffice", "Select Salutation");
                corpOffice_OfficeHelper.Select("Salutation", "Mr");
                //corpOffice_OfficeHelper.WaitForWorkAround(2000);

                executionLog.Log("CreateOffice", "Enter FirstName");
                corpOffice_OfficeHelper.TypeText("FirstName", "Test");

                executionLog.Log("CreateOffice", "Enter LastName");
                corpOffice_OfficeHelper.TypeText("LastName", "Tester");

                executionLog.Log("CreateOffice", "Enter eAddress");
                corpOffice_OfficeHelper.TypeText("eAddress", "NewTest@yopmail.com");

                executionLog.Log("CreateOffice", " Click on save button");
                try
                {
                    corpOffice_OfficeHelper.ClickElement("Save");
                }
                catch (OpenQA.Selenium.WebDriverException) { }

                executionLog.Log("CreateOffice", "Verify text on the page");
                corpOffice_OfficeHelper.WaitForText("Office created successfully.", 10);

                executionLog.Log("CreateOffice", "Go to office page");
                VisitCorp("offices");
                corpOffice_OfficeHelper.WaitForWorkAround(4000);

                executionLog.Log("CreateOffice", "Verify title");
                VerifyTitle("Offices");

                executionLog.Log("CreateOffice", "Enter Name to search");
                corpOffice_OfficeHelper.TypeText("EnterSelenium", name);
                corpOffice_OfficeHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateOffice", "Click Delete btn  ");
                corpOffice_OfficeHelper.ClickElement("DeleteOffice");

                executionLog.Log("CreateOffice", "Verify page text");
                corpOffice_OfficeHelper.VerifyPageText("Are you sure want to delete the");

                executionLog.Log("CreateOffice", "Click Delete btn  ");
                corpOffice_OfficeHelper.ClickElement("ConfirmDelete");

                executionLog.Log("CreateOffice", "Accept alert message. ");
                corpOffice_OfficeHelper.AcceptAlert();

                executionLog.Log("CreateOffice", "Wait for delete message. ");
                corpOffice_OfficeHelper.WaitForText("Office deleted successfully.", 20);

            }
            
    
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CreateOffice");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Create Office");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Create Office", "Bug", "Medium", "Office page", "QA", "Log in as: " + username1[0] + " / " + password1[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Create Office");
                        TakeScreenshot("CreateOffice");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateOffice.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CreateOffice");
                        string id = loginHelper.getIssueID("Create Office");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateOffice.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Create Office"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Create Office");
                executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("CreateOffice");
                executionLog.WriteInExcel("Create Office", Status, JIRA, "Corp Office");
            }
        }
    }
}


