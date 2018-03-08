using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class VerifyOfficeCreatedSuccessfully : DriverTestCase
    {
        [TestMethod]
        [TestCategory("Corp")]
        [TestCategory("All")]
        [TestCategory("Aslam_Corp")]
        public void verifyOfficeCreatedSuccessfully()
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

                executionLog.Log("VerifyOfficeCreatedSuccessfully", "Login with valid username and password");
                Login("newthemecorp", "pegasus");

                executionLog.Log("VerifyOfficeCreatedSuccessfully", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyOfficeCreatedSuccessfully", "Go to office page");
                VisitCorp("offices");

                executionLog.Log("VerifyOfficeCreatedSuccessfully", "Verify title");
                VerifyTitle("Offices");

                executionLog.Log("VerifyOfficeCreatedSuccessfully", "Click On Create button");
                corpOffice_OfficeHelper.ClickElement("Create");

                executionLog.Log("VerifyOfficeCreatedSuccessfully", "Verify title");
                VerifyTitle("Create an Office");

                executionLog.Log("VerifyOfficeCreatedSuccessfully", "Enter Name");
                corpOffice_OfficeHelper.TypeText("Name", name);

                executionLog.Log("VerifyOfficeCreatedSuccessfully", "Enter DBAName");
                corpOffice_OfficeHelper.TypeText("DBAName", "TEST123");

                executionLog.Log("VerifyOfficeCreatedSuccessfully", "Enter Website");
                corpOffice_OfficeHelper.TypeText("Website", "TEST.COM");

                executionLog.Log("VerifyOfficeCreatedSuccessfully", "Enter OfficeCode");
                corpOffice_OfficeHelper.TypeText("OfficeCode", "12345");

                executionLog.Log("VerifyOfficeCreatedSuccessfully", "Select Address");
                corpOffice_OfficeHelper.Select("AddressType", "Office");

                executionLog.Log("VerifyOfficeCreatedSuccessfully", "Enter AddressLine1");
                corpOffice_OfficeHelper.TypeText("AddressLine1", "FC-89");

                executionLog.Log("VerifyOfficeCreatedSuccessfully", "Enter ZipCode");
                corpOffice_OfficeHelper.TypeText("ZIpCode", "60601");

                executionLog.Log("VerifyOfficeCreatedSuccessfully", "Enter PrimaryUserName");
                corpOffice_OfficeHelper.TypeText("PrimaryUserName", username);

                executionLog.Log("VerifyOfficeCreatedSuccessfully", "Click on AutoGenPassword checkbox");
                corpOffice_OfficeHelper.ClickElement("AutoGenPassword");

                executionLog.Log("VerifyOfficeCreatedSuccessfully", "Enter PrimaryPassword");
                corpOffice_OfficeHelper.TypeText("PrimaryPassword", "pegasus");

                executionLog.Log("VerifyOfficeCreatedSuccessfully", "Select Salutation");
                corpOffice_OfficeHelper.Select("Salutation", "Mr");
                corpOffice_OfficeHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyOfficeCreatedSuccessfully", "Enter FirstName");
                corpOffice_OfficeHelper.TypeText("FirstName", "Test");

                executionLog.Log("VerifyOfficeCreatedSuccessfully", "Enter LastName");
                corpOffice_OfficeHelper.TypeText("LastName", "Tester");

                executionLog.Log("VerifyOfficeCreatedSuccessfully", "Enter eAddress");
                corpOffice_OfficeHelper.TypeText("eAddress", "NewTest@yopmail.com");

                executionLog.Log("VerifyOfficeCreatedSuccessfully", " Click on save button");
                corpOffice_OfficeHelper.ClickElement("Save");

                executionLog.Log("VerifyOfficeCreatedSuccessfully", "Verify text on the page");
                corpOffice_OfficeHelper.WaitForText("Office created successfully.", 10);

                executionLog.Log("VerifyOfficeCreatedSuccessfully", "Verify unexpected text not present.");
                corpOffice_OfficeHelper.VerifyTextNot("Array  ....");

                executionLog.Log("VerifyOfficeCreatedSuccessfully", "Go to office page");
                VisitCorp("offices");

                executionLog.Log("VerifyOfficeCreatedSuccessfully", "Verify title");
                VerifyTitle("Offices");

                executionLog.Log("VerifyOfficeCreatedSuccessfully", "Enter Name to search");
                corpOffice_OfficeHelper.TypeText("EnterSelenium", name);
                corpOffice_OfficeHelper.WaitForWorkAround(5000);

                executionLog.Log("VerifyOfficeCreatedSuccessfully", "Click Delete btn  ");
                corpOffice_OfficeHelper.ClickElement("DeleteOffice");

                executionLog.Log("VerifyOfficeCreatedSuccessfully", "Verify page text");
                corpOffice_OfficeHelper.VerifyPageText("Are you sure want to delete the");

                executionLog.Log("VerifyOfficeCreatedSuccessfully", "Click Delete btn  ");
                corpOffice_OfficeHelper.ClickElement("ConfirmDelete");

                executionLog.Log("VerifyOfficeCreatedSuccessfully", "Accept alert message. ");
                corpOffice_OfficeHelper.AcceptAlert();

                executionLog.Log("VerifyOfficeCreatedSuccessfully", "Wait for delete message. ");
                corpOffice_OfficeHelper.WaitForText("Office deleted successfully.", 20);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyOfficeCreatedSuccessfully");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Office Created Successfully");
                if (!result)
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Office Created Successfully", "Bug", "Medium", "Office page", "QA", "Log in as: " + username1[0] + " / " + password1[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Office Created Successfully");
                        TakeScreenshot("VerifyOfficeCreatedSuccessfully");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyOfficeCreatedSuccessfully.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyOfficeCreatedSuccessfully");
                        string id = loginHelper.getIssueID("Verify Office Created Successfully");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyOfficeCreatedSuccessfully.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Office Created Successfully"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Office Created Successfully");
                executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyOfficeCreatedSuccessfully");
                executionLog.WriteInExcel("Verify Office Created Successfully", Status, JIRA, "Corp Office");
            }
        }
    }
}


