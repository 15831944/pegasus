using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class VerifyingOfficeZipCode : DriverTestCase
    {
        [TestMethod]
        [TestCategory("Bug")]
        [TestCategory("All")]
        [TestCategory("Fail")]
        [TestCategory("TS3")]
        [TestCategory("BugTestNew")]
        public void verifyingOfficeZipCode()
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
            var name = "Test" + RandomNumber(1, 9999999);
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("VerifyingOfficeZipCode", "Login with valid username and password");
                Login(username1[0], password1[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password1[0]);

                executionLog.Log("VerifyingOfficeZipCode", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyingOfficeZipCode", "Go to office page");
                VisitCorp("offices");

                executionLog.Log("VerifyingOfficeZipCode", "Verify Page title");
                VerifyTitle("Offices");
                corpOffice_OfficeHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyingOfficeZipCode", "Click On Create button");
                corpOffice_OfficeHelper.ClickElement("Create");
                corpOffice_OfficeHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyingOfficeZipCode", "Verify title create office");
                VerifyTitle("Create an Office");

                executionLog.Log("VerifyingOfficeZipCode", "Enter Name");
                corpOffice_OfficeHelper.TypeText("Name", name);

                executionLog.Log("VerifyingOfficeZipCode", "Enter DBAName");
                corpOffice_OfficeHelper.TypeText("DBAName", "TEST123");

                executionLog.Log("VerifyingOfficeZipCode", "Enter Website");
                corpOffice_OfficeHelper.TypeText("Website", "TEST.COM");

                executionLog.Log("VerifyingOfficeZipCode", "Enter OfficeCode");
                corpOffice_OfficeHelper.TypeText("OfficeCode", "12345");

                executionLog.Log("VerifyingOfficeZipCode", "Select Address");
                corpOffice_OfficeHelper.Select("AddressType", "Office");

                executionLog.Log("VerifyingOfficeZipCode", "Enter AddressLine1");
                corpOffice_OfficeHelper.TypeText("AddressLine1", "FC-89");

                executionLog.Log("VerifyingOfficeZipCode", "Enter ZipCode");
                corpOffice_OfficeHelper.TypeText("ZIpCode", "60601");
                corpOffice_OfficeHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyingOfficeZipCode", "Enter PrimaryUserName");
                corpOffice_OfficeHelper.TypeText("PrimaryUserName", username);

                executionLog.Log("VerifyingOfficeZipCode", "Click on AutoGenPassword checkbox");
                corpOffice_OfficeHelper.ClickElement("AutoGenPassword");

                executionLog.Log("VerifyingOfficeZipCode", "Enter PrimaryPassword");
                corpOffice_OfficeHelper.TypeText("PrimaryPassword", "pegasus");

                executionLog.Log("VerifyingOfficeZipCode", "Select Salutation");
                corpOffice_OfficeHelper.Select("Salutation", "Mr");
                corpOffice_OfficeHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyingOfficeZipCode", "Enter FirstName");
                corpOffice_OfficeHelper.TypeText("FirstName", "Test");
                corpOffice_OfficeHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyingOfficeZipCode", "Enter LastName");
                corpOffice_OfficeHelper.TypeText("LastName", "Tester");
                corpOffice_OfficeHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyingOfficeZipCode", "Enter eAddress");
                corpOffice_OfficeHelper.TypeText("eAddress", "NewTest@yopmail.com");
                corpOffice_OfficeHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyingOfficeZipCode", " Click on save button");
                corpOffice_OfficeHelper.ClickElement("Save");
                corpOffice_OfficeHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyingOfficeZipCode", "Verify text on the page");
                corpOffice_OfficeHelper.WaitForText("Office created successfully.", 50);

                executionLog.Log("VerifyingOfficeZipCode", "Enter Name to search office");
                corpOffice_OfficeHelper.TypeText("EnterNameToSearch", name);
                corpOffice_OfficeHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyingOfficeZipCode", "Click on first offfice");
                corpOffice_OfficeHelper.ClickElement("Office1");
                corpOffice_OfficeHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyingOfficeZipCode", "Verify office zip code.");
                corpOffice_OfficeHelper.VerifyPageText("60601");
                //corpOffice_OfficeHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyingOfficeZipCode", "Go to office page");
                VisitCorp("offices");
                corpOffice_OfficeHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyingOfficeZipCode", "Verify title");
                VerifyTitle("Offices");
                //corpOffice_OfficeHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyingOfficeZipCode", "Enter Name to search");
                corpOffice_OfficeHelper.TypeText("EnterSelenium", name);
                corpOffice_OfficeHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyingOfficeZipCode", "Click Delete btn");
                corpOffice_OfficeHelper.ClickElement("DeleteOffice");
                //corpOffice_OfficeHelper.WaitForWorkAround(7000);

                executionLog.Log("VerifyingOfficeZipCode", "Verify page text");
                corpOffice_OfficeHelper.VerifyPageText("Are you sure want to delete the");
                //corpOffice_OfficeHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyingOfficeZipCode", "Click Delete btn  ");
                corpOffice_OfficeHelper.ClickElement("ConfirmDelete");
                corpOffice_OfficeHelper.WaitForWorkAround(4000);

                executionLog.Log("VerifyingOfficeZipCode", "Accept alert message. ");
                corpOffice_OfficeHelper.AcceptAlert();
                corpOffice_OfficeHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyingOfficeZipCode", "Wait for delete message. ");
                corpOffice_OfficeHelper.WaitForText("Office deleted successfully.", 50);

            }

            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyingOfficeZipCode");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verifying Office Zip Code");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verifying Office Zip Code", "Bug", "Medium", "Corp Office page", "QA", "Log in as: " + username1[0] + " / " + password1[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verifying Office Zip Code");
                        TakeScreenshot("VerifyingOfficeZipCode");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyingOfficeZipCode.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyingOfficeZipCode");
                        string id = loginHelper.getIssueID("Verifying Office Zip Code");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyingOfficeZipCode.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verifying Office Zip Code"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verifying Office Zip Code");
                //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyingOfficeZipCode");
                executionLog.WriteInExcel("Verifying Office Zip Code", Status, JIRA, "Corp Office");
            }
        }
    }
}