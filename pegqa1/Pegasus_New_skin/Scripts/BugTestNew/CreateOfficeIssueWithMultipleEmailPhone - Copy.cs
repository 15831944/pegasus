using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class CreateOfficeIssueWithMultipleEmailPhone : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        public void createOfficeIssueWithMultipleEmailPhone()
        {

            string[] username = null;
            string[] password = null;


            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username2");
            password = oXMLData.getData("settings/Credentials", "password2");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var corp_Office_OfficeHelper = new CorpOffice_OfficeHelper(GetWebDriver());

            // Variable random
            var Office = "Test" + GetRandomNumber();
            var User = "User" + GetRandomNumber();
            var Email = "addressTest" + RandomNumber(1, 99) + "@yop.com";
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("CreateOfficeIssueWithMultipleEmailPhone", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("CreateOfficeIssueWithMultipleEmailPhone", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("CreateOfficeIssueWithMultipleEmailPhone", "Redirect at Craete Office page");
                VisitCorp("offices/create");

                executionLog.Log("CreateOfficeIssueWithMultipleEmailPhone", "Verify page title");
                VerifyTitle("Create an Office");

                executionLog.Log("CreateOfficeIssueWithMultipleEmailPhone", "Enter office name.");
                corp_Office_OfficeHelper.TypeText("Name", Office);

                executionLog.Log("CreateOfficeIssueWithMultipleEmailPhone", "Select Address type.");
                corp_Office_OfficeHelper.Select("AddressType", "Office");

                executionLog.Log("CreateOfficeIssueWithMultipleEmailPhone", "Enter AddressLine1");
                corp_Office_OfficeHelper.TypeText("AddressLine1", "FC-89");

                executionLog.Log("CreateOfficeIssueWithMultipleEmailPhone", "Enter Zip Code.");
                corp_Office_OfficeHelper.TypeText("ZIpCode", "60601");
                corp_Office_OfficeHelper.WaitForWorkAround(1000);

                executionLog.Log("CreateOfficeIssueWithMultipleEmailPhone", "Enter a invalid user name.");
                corp_Office_OfficeHelper.TypeText("PrimaryUserName", "tes");

                executionLog.Log("CreateOfficeIssueWithMultipleEmailPhone", "Enter First name.");
                corp_Office_OfficeHelper.TypeText("FirstName", "User");

                executionLog.Log("CreateOfficeIssueWithMultipleEmailPhone", "Enter Last Name");
                corp_Office_OfficeHelper.TypeText("LastName", "Test");

                executionLog.Log("CreateOfficeIssueWithMultipleEmailPhone", "Enter eAddress");
                corp_Office_OfficeHelper.TypeText("eAddress", Email);

                executionLog.Log("CreateOfficeIssueWithMultipleEmailPhone", " Click on Add email");
                corp_Office_OfficeHelper.ClickElement("AddEmail");

                executionLog.Log("CreateOfficeIssueWithMultipleEmailPhone", " Enter email in added email field.");
                corp_Office_OfficeHelper.TypeText("EnterEmail2", "test21@yopmail.com");

                executionLog.Log("CreateOfficeIssueWithMultipleEmailPhone", " Click on Add phone");
                corp_Office_OfficeHelper.ClickElement("AddPhone");

                executionLog.Log("CreateOfficeIssueWithMultipleEmailPhone", " Enter added phone .");
                corp_Office_OfficeHelper.TypeText("Phone2", "1234554321");

                executionLog.Log("CreateOfficeIssueWithMultipleEmailPhone", " Click on Save button.");
                corp_Office_OfficeHelper.ClickElement("Save");

                executionLog.Log("CreateOfficeIssueWithMultipleEmailPhone", " Verify page title as create an office.");
                VerifyTitle("Create an Office");

                executionLog.Log("CreateOfficeIssueWithMultipleEmailPhone", "Enter a avalid user name.");
                corp_Office_OfficeHelper.TypeText("PrimaryUserName", User);

                executionLog.Log("CreateOfficeIssueWithMultipleEmailPhone", " Click on add email button.");
                corp_Office_OfficeHelper.ClickElement("AddEmail");

                executionLog.Log("CreateOfficeIssueWithMultipleEmailPhone", " Enter added email.");
                corp_Office_OfficeHelper.TypeText("EnterEmail2", "test21@yopmail.com");

                executionLog.Log("CreateOfficeIssueWithMultipleEmailPhone", " Click on add phone button.");
                corp_Office_OfficeHelper.ClickElement("AddPhone");

                executionLog.Log("CreateOfficeIssueWithMultipleEmailPhone", " Enter added phone.");
                corp_Office_OfficeHelper.TypeText("Phone2", "1234554321");

                executionLog.Log("CreateOfficeIssueWithMultipleEmailPhone", " Click on Save button.");
                corp_Office_OfficeHelper.ClickElement("Save");

                executionLog.Log("CreateOfficeIssueWithMultipleEmailPhone", "Verify text on the page");
                corp_Office_OfficeHelper.WaitForText("Wait for success message.", 10);

                executionLog.Log("CreateOfficeIssueWithMultipleEmailPhone", "Enter office name to be Searched.");
                corp_Office_OfficeHelper.TypeText("EnterSelenium", Office);
                corp_Office_OfficeHelper.WaitForWorkAround(2000);

                executionLog.Log("CreateOfficeIssueWithMultipleEmailPhone", "Click on Delete icon");
                corp_Office_OfficeHelper.ClickElement("DeleteOffice");

                executionLog.Log("CreateOfficeIssueWithMultipleEmailPhone", "Click Delete to confirm");
                corp_Office_OfficeHelper.ClickElement("ConfirmDelete");

                executionLog.Log("CreateOfficeIssueWithMultipleEmailPhone", "Wait for success message.");
                corp_Office_OfficeHelper.WaitForText("Office deleted successfully", 30);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CreateOfficeIssueWithMultipleEmailPhone");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Create Office Issue With Multiple Email Phone");
                if (!result)
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Create Office Issue With Multiple Email Phone", "Bug", "Medium", "Office corp", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Create Office Issue With Multiple Email Phone");
                        TakeScreenshot("CreateOfficeIssueWithMultipleEmailPhone");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateOfficeIssueWithMultipleEmailPhone.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CreateOfficeIssueWithMultipleEmailPhone");
                        string id = loginHelper.getIssueID("Create Office Issue With Multiple Email Phone");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateOfficeIssueWithMultipleEmailPhone.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Create Office Issue With Multiple Email Phone"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Create Office Issue With Multiple Email Phone");
                executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("CreateOfficeIssueWithMultipleEmailPhone");
                executionLog.WriteInExcel("Create Office Issue With Multiple Email Phone", Status, JIRA, "Corp Office");
            }
        }
    }
}