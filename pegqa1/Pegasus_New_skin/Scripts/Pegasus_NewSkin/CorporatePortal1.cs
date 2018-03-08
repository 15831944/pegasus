using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class CorporatePortal1 : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin")]
        [TestCategory("TS8")]
        [TestCategory("Pegasus_NewSkin")]
        public void corporatePortal1()
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
            var usernme = "Sysprins" + RandomNumber(44, 799999977);
            var Office = "Office" + RandomNumber(1, 99999);
            var name = "Test" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("CorporatePortal1", "Login with valid username and password");
                Login(username[0], password[0]);

                executionLog.Log("CorporatePortal1", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("CorporatePortal1", "Redircet to office");
                VisitCorp("offices");

                executionLog.Log("CorporatePortal1", "Click On Create New Button");
                corpOffice_OfficeHelper.ClickElement("Create");

                executionLog.Log("CorporatePortal1", "Enter Name");
                corpOffice_OfficeHelper.TypeText("Name", Office);

                executionLog.Log("CorporatePortal1", "Enter DBAName");
                corpOffice_OfficeHelper.TypeText("DBAName", "TEST123");

                executionLog.Log("CorporatePortal1", "Enter Website");
                corpOffice_OfficeHelper.TypeText("Website", "TEST.COM");

                executionLog.Log("CorporatePortal1", "Enter OfficeCode");
                corpOffice_OfficeHelper.TypeText("OfficeCode", "12345");

                executionLog.Log("CorporatePortal1", "Enter VenderName");
                corpOffice_OfficeHelper.TypeText("VendorName", "VenderTEST");

                executionLog.Log("CorporatePortal1", "Enter Vender code");
                corpOffice_OfficeHelper.TypeText("VendorCode", "1234");

                executionLog.Log("CorporatePortal1", "Enter office phone number");
                corpOffice_OfficeHelper.TypeText("OfficePhoneNumber", "1234567890");

                executionLog.Log("CorporatePortal1", "Enter BusinessPhoneNumber");
                corpOffice_OfficeHelper.TypeText("BusinessPhoneNumber", "1234567890");

                executionLog.Log("CorporatePortal1", "Enter FaxNumber");
                corpOffice_OfficeHelper.TypeText("FaxNumber", "1234567890");

                executionLog.Log("CorporatePortal1", "Enter LinkedURL");
                corpOffice_OfficeHelper.TypeText("LinkedURL", "Linked.com");

                executionLog.Log("CorporatePortal1", "Enter FacebookURL");
                corpOffice_OfficeHelper.TypeText("FacebookURL", "Facebook.com");

                executionLog.Log("CorporatePortal1", "Enter TwitterURL");
                corpOffice_OfficeHelper.TypeText("TwitterURL", "Twitter.com");

                executionLog.Log("CorporatePortal1", "Enter Address");
                corpOffice_OfficeHelper.Select("AddressType", "Office");

                executionLog.Log("CorporatePortal1", "Enter AddressLine1");
                corpOffice_OfficeHelper.TypeText("AddressLine1", "FC-89");

                executionLog.Log("CorporatePortal1", "Enter Zip Code");
                corpOffice_OfficeHelper.TypeText("ZIpCode", "60601");
                corpOffice_OfficeHelper.WaitForWorkAround(4000);

                executionLog.Log("CorporatePortal1", "Enter PrimaryUserName");
                corpOffice_OfficeHelper.TypeText("PrimaryUserName", usernme);

                executionLog.Log("CorporatePortal1", "Click on AutoGenPassword checkbox");
                corpOffice_OfficeHelper.ClickElement("AutoGenPassword");

                executionLog.Log("CorporatePortal1", "Enter Primary Password");
                corpOffice_OfficeHelper.TypeText("PrimaryPassword", "1qaz!QAZ");

                executionLog.Log("CorporatePortal1", "Select Salutation");
                corpOffice_OfficeHelper.Select("Salutation", "Mr");
                corpOffice_OfficeHelper.WaitForWorkAround(2000);

                executionLog.Log("CorporatePortal1", "Enter FirstName");
                corpOffice_OfficeHelper.TypeText("FirstName", "Test");

                executionLog.Log("CorporatePortal1", "Enter LastName");
                corpOffice_OfficeHelper.TypeText("LastName", "Tester");

                executionLog.Log("CorporatePortal1", "Enter eAddress");
                corpOffice_OfficeHelper.TypeText("eAddress", "NewTest@yopmail.com");

                executionLog.Log("CorporatePortal1", "Save");
                corpOffice_OfficeHelper.ClickElement("Save");
                corpOffice_OfficeHelper.WaitForWorkAround(2000);

                executionLog.Log("CorporatePortal1", "Wait for Confirmation");
                corpOffice_OfficeHelper.WaitForText("Office created successfully.", 50);

                executionLog.Log("CorporatePortal1", "Visit Office");
                VisitCorp("offices");

                executionLog.Log("CorporatePortal1", "Enter Name To Search");
                corpOffice_OfficeHelper.TypeText("EnterNameToSearch", Office);

                executionLog.Log("CorporatePortal1", "Click on Edit Icon");
                corpOffice_OfficeHelper.ClickElement("EditOffice");
                corpOffice_OfficeHelper.WaitForWorkAround(3000);

                executionLog.Log("CorporatePortal1", "Click on Save");
                corpOffice_OfficeHelper.ClickElement("SaveEdit");
                corpOffice_OfficeHelper.WaitForWorkAround(3000);

                executionLog.Log("CorporatePortal1", "Wait for Confirmation");
                corpOffice_OfficeHelper.WaitForText("Office updated successfully.", 50);

                executionLog.Log("CorporatePortal1", "Goto Office");
                VisitCorp("offices");

                executionLog.Log("CorporatePortal1", "Enter Name To Search");
                corpOffice_OfficeHelper.TypeText("EnterNameToSearch", Office);

                executionLog.Log("CorporatePortal1", "Click On Office");
                corpOffice_OfficeHelper.ClickElement("ClickOnOffice");

                executionLog.Log("CorporatePortal1", "Click On Send Email");
                corpOffice_OfficeHelper.ClickElement("ClickOnSendEmail");
                corpOffice_OfficeHelper.WaitForWorkAround(2000);

                executionLog.Log("CorporatePortal1", "Enter Email");
                corpOffice_OfficeHelper.TypeText("EnterToEmail", "Test@yopmail.com");

                executionLog.Log("CorporatePortal1", "Enter Email Subject");
                corpOffice_OfficeHelper.TypeText("EnterSubjectEmail", "Test Subject");

                executionLog.Log("CorporatePortal1", "Click On Send Email Pop Up");
                corpOffice_OfficeHelper.ClickElement("ClickOnSendEmailPopUp");
                corpOffice_OfficeHelper.WaitForWorkAround(4000);

                executionLog.Log("CorporatePortal1", "Verify Confirmation");
                corpOffice_OfficeHelper.WaitForText("Email Sent Successfully.", 20);

                executionLog.Log("CorporatePortal1", "Click On Add Notes");
                corpOffice_OfficeHelper.ClickElement("ClickOnAddNotes");
                corpOffice_OfficeHelper.WaitForWorkAround(2000);

                executionLog.Log("CorporatePortal1", "Enter Notes Subject");
                corpOffice_OfficeHelper.TypeText("EnterNoteSubject", "Test Subject");

                executionLog.Log("CorporatePortal1", "Click Save");
                corpOffice_OfficeHelper.ClickElement("SaveNotesOffice");

                executionLog.Log("CorporatePortal1", "Enter Notes Subject");
                corpOffice_OfficeHelper.WaitForText("Note Created Successfully", 10);

                executionLog.Log("CorporatePortal1", "Click on Add Documnet");
                corpOffice_OfficeHelper.ClickElement("AddDocumentOff");
                corpOffice_OfficeHelper.WaitForWorkAround(2000);

                executionLog.Log("CorporatePortal1", "Enter Subject");
                corpOffice_OfficeHelper.TypeText("NameDocumentOff", "Test Doc Name");
                corpOffice_OfficeHelper.WaitForWorkAround(2000);

                executionLog.Log("CorporatePortal1", "Add attachment.");
                var FileName = GetPathToFile() + "index.jpg";
                corpOffice_OfficeHelper.UploadFile("//*[@id='DocumentFiles']", FileName);

                executionLog.Log("CorporatePortal1", "Click Save");
                corpOffice_OfficeHelper.ClickDisplayed("//a[@title='Save']");

                executionLog.Log("CorporatePortal1", "Verify");
                corpOffice_OfficeHelper.WaitForText("Documents successfully Added.", 10);

                executionLog.Log("CorporatePortal1", "Add Meeting");
                corpOffice_OfficeHelper.ClickElement("ClickOnMeeting");
                corpOffice_OfficeHelper.WaitForWorkAround(2000);

                executionLog.Log("CorporatePortal1", "Enter Subject");
                corpOffice_OfficeHelper.TypeText("EnterSubjectMeeting", "Test Subject");
                corpOffice_OfficeHelper.WaitForWorkAround(2000);

                executionLog.Log("CorporatePortal1", "Enter Meeting");
                corpOffice_OfficeHelper.TypeText("EnterMeetingStartDate", "2015-11-03");

                executionLog.Log("CorporatePortal1", "Click Save");
                corpOffice_OfficeHelper.ClickDisplayed("//a[@title='Save']");

                executionLog.Log("CorporatePortal1", "Verify");
                corpOffice_OfficeHelper.WaitForText("Meeting Created Successfully.", 20);

                executionLog.Log("CorporatePortal1", "Go to office page");
                VisitCorp("offices");
                corpOffice_OfficeHelper.WaitForWorkAround(5000);

                executionLog.Log("CorporatePortal1", "Verify title");
                VerifyTitle("Offices");

                executionLog.Log("CorporatePortal1", "Enter Name to search");
                corpOffice_OfficeHelper.TypeText("EnterSelenium", Office);
                corpOffice_OfficeHelper.WaitForWorkAround(5000);

                executionLog.Log("CorporatePortal1", "Click Delete btn  ");
                corpOffice_OfficeHelper.ClickElement("DeleteOffice");
                corpOffice_OfficeHelper.WaitForWorkAround(4000);

                executionLog.Log("CorporatePortal1", "Verify page text");
                corpOffice_OfficeHelper.VerifyPageText("Are you sure want to delete the");
                corpOffice_OfficeHelper.WaitForWorkAround(4000);

                executionLog.Log("CorporatePortal1", "Click Delete btn  ");
                corpOffice_OfficeHelper.ClickElement("ConfirmDelete");
                corpOffice_OfficeHelper.WaitForWorkAround(2000);

                executionLog.Log("CorporatePortal1", "Accept alert message. ");
                corpOffice_OfficeHelper.AcceptAlert();
                corpOffice_OfficeHelper.WaitForWorkAround(2000);

                executionLog.Log("CorporatePortal1", "Wait for delete message. ");
                corpOffice_OfficeHelper.WaitForText("Office deleted successfully.", 50);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CorporatePortal1");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("CorporatePortal1");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("CorporatePortal1", "Bug", "Medium", "Corp Office page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("CorporatePortal1");
                        TakeScreenshot("CorporatePortal1");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CorporatePortal1.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CorporatePortal1");
                        string id = loginHelper.getIssueID("CorporatePortal1");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CorporatePortal1.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("CorporatePortal1"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("CorporatePortal1");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("CorporatePortal1");
                executionLog.WriteInExcel("CorporatePortal1", Status, JIRA, "Corp Offices");
            }
        }
    }
}