using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class OfficeEadresslabelAutoPopUpIssue : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        public void officeEadresslabelAutoPopUpIssue()
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
                executionLog.Log("OfficeEadresslabelAutoPopUpIssue", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("OfficeEadresslabelAutoPopUpIssue", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("OfficeEadresslabelAutoPopUpIssue", "Redirect at Create Office page");
                VisitCorp("offices/create");

                executionLog.Log("OfficeEadresslabelAutoPopUpIssue", "Verify page title");
                VerifyTitle("Create an Office");

                executionLog.Log("OfficeEadresslabelAutoPopUpIssue", "Enter office name.");
                corp_Office_OfficeHelper.TypeText("Name", Office);

                executionLog.Log("OfficeEadresslabelAutoPopUpIssue", "Select Address type.");
                corp_Office_OfficeHelper.Select("AddressType", "Office");

                executionLog.Log("OfficeEadresslabelAutoPopUpIssue", "Enter AddressLine1");
                corp_Office_OfficeHelper.TypeText("AddressLine1", "FC-89");

                executionLog.Log("OfficeEadresslabelAutoPopUpIssue", "Enter Zip Code.");
                corp_Office_OfficeHelper.TypeText("ZIpCode", "60601");
                corp_Office_OfficeHelper.WaitForWorkAround(1000);

                executionLog.Log("OfficeEadresslabelAutoPopUpIssue", "Enter a valid user name.");
                corp_Office_OfficeHelper.TypeText("PrimaryUserName", User);

                executionLog.Log("OfficeEadresslabelAutoPopUpIssue", "Enter First name.");
                corp_Office_OfficeHelper.TypeText("FirstName", "User");

                executionLog.Log("OfficeEadresslabelAutoPopUpIssue", "Enter Last Name");
                corp_Office_OfficeHelper.TypeText("LastName", "Test");

                executionLog.Log("OfficeEadresslabelAutoPopUpIssue", "Enter eAddress");
                corp_Office_OfficeHelper.TypeText("eAddress", Email);

                executionLog.Log("OfficeEadresslabelAutoPopUpIssue", " Click on Save button.");
                corp_Office_OfficeHelper.ClickElement("Save");

                executionLog.Log("OfficeEadresslabelAutoPopUpIssue", "Verify text on the page");
                corp_Office_OfficeHelper.WaitForText("Office created successfully.", 10);

                executionLog.Log("OfficeEadresslabelAutoPopUpIssue", "Enter office name to be Searched.");
                corp_Office_OfficeHelper.TypeText("EnterSelenium", Office);
                corp_Office_OfficeHelper.WaitForWorkAround(2000);

                executionLog.Log("OfficeEadresslabelAutoPopUpIssue", "Click on edit icon.");
                corp_Office_OfficeHelper.ClickElement("EditOffice");
                corp_Office_OfficeHelper.WaitForWorkAround(2000);

                executionLog.Log("OfficeEadresslabelAutoPopUpIssue", " Click on add email.");
                corp_Office_OfficeHelper.ClickElement("EditAddEmail");

                executionLog.Log("OfficeEadresslabelAutoPopUpIssue", "Select email type as weblink.");
                corp_Office_OfficeHelper.Select("EmailType2", "Web Links");
                corp_Office_OfficeHelper.WaitForWorkAround(2000);

                executionLog.Log("OfficeEadresslabelAutoPopUpIssue", " Enter email in added email field.");
                corp_Office_OfficeHelper.TypeText("EnterEmail2", "test21@yopmail.com");

                executionLog.Log("OfficeEadresslabelAutoPopUpIssue", "Click on save button.");
                corp_Office_OfficeHelper.ClickElement("SaveEdit");
                corp_Office_OfficeHelper.WaitForWorkAround(2000);

                executionLog.Log("OfficeEadresslabelAutoPopUpIssue", "Click on edit link to edit office..");
                corp_Office_OfficeHelper.ClickElement("EditLink");
                corp_Office_OfficeHelper.WaitForWorkAround(2000);

                executionLog.Log("OfficeEadresslabelAutoPopUpIssue", "Verify e address label for added email is weblink.");
                corp_Office_OfficeHelper.VerifyeLabel("VerifyLabel2");
                corp_Office_OfficeHelper.WaitForWorkAround(2000);

                executionLog.Log("OfficeEadresslabelAutoPopUpIssue", "Click on save button.");
                corp_Office_OfficeHelper.ClickElement("SaveEdit");
                corp_Office_OfficeHelper.WaitForWorkAround(2000);

                executionLog.Log("OfficeEadresslabelAutoPopUpIssue", "Enter office name to be Searched.");
                VisitCorp("offices");
                corp_Office_OfficeHelper.WaitForWorkAround(2000);

                executionLog.Log("OfficeEadresslabelAutoPopUpIssue", "Enter office name to be Searched.");
                corp_Office_OfficeHelper.TypeText("EnterSelenium", Office);
                corp_Office_OfficeHelper.WaitForWorkAround(2000);

                executionLog.Log("OfficeEadresslabelAutoPopUpIssue", "Click on Delete icon");
                corp_Office_OfficeHelper.ClickElement("DeleteOffice");

                executionLog.Log("OfficeEadresslabelAutoPopUpIssue", "Click Delete to confirm");
                corp_Office_OfficeHelper.ClickElement("ConfirmDelete");

                executionLog.Log("OfficeEadresslabelAutoPopUpIssue", "Wait for success message.");
                corp_Office_OfficeHelper.WaitForText("Office deleted successfully", 30);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("OfficeEadresslabelAutoPopUpIssue");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Office Eadress label Auto PopUp Issue");
                if (!result)
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Office Eadress label Auto PopUp Issue", "Bug", "Medium", "Office corp", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Office Eadress label Auto PopUp Issue");
                        TakeScreenshot("OfficeEadresslabelAutoPopUpIssue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\OfficeEadresslabelAutoPopUpIssue.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("OfficeEadresslabelAutoPopUpIssue");
                        string id = loginHelper.getIssueID("Office Eadress label Auto PopUp Issue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\OfficeEadresslabelAutoPopUpIssue.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Office Eadress label Auto PopUp Issue"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Office Eadress label Auto PopUp Issue");
                executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("OfficeEadresslabelAutoPopUpIssue");
                executionLog.WriteInExcel("Office Eadress label Auto PopUp Issue", Status, JIRA, "Corp Office");
            }
        }
    }
}
