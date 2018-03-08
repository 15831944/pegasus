using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class DeleteOffice : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin")]
        [TestCategory("Fail")]
        [TestCategory("TS8")]
        [TestCategory("Pegasus_NewSkin")]
        public void deleteOffice()
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

            //Variable random
            var usernme = "TESTUSER" + RandomNumber(676, 99999);
            var name = "DeleteOffice" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("DeleteOffice", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("DeleteOffice", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("DeleteOffice", "Redirect at office page.");
                VisitCorp("offices");
                corpOffice_OfficeHelper.WaitForWorkAround(4000);

                executionLog.Log("DeleteOffice", "Click on create button.");
                corpOffice_OfficeHelper.ClickElement("Create");
                corpOffice_OfficeHelper.WaitForWorkAround(4000);

                executionLog.Log("DeleteOffice", "Enter Name");
                corpOffice_OfficeHelper.TypeText("Name", name);

                executionLog.Log("DeleteOffice", "Enter DBAName");
                corpOffice_OfficeHelper.TypeText("DBAName", "TEST123");

                executionLog.Log("DeleteOffice", "Enter Website");
                corpOffice_OfficeHelper.TypeText("Website", "TEST.COM");

                executionLog.Log("DeleteOffice", "Enter VenderName");
                corpOffice_OfficeHelper.TypeText("VendorName", "VenderTEST");

                executionLog.Log("DeleteOffice", "Enter Vendor code");
                corpOffice_OfficeHelper.TypeText("VendorCode", "1234");

                executionLog.Log("DeleteOffice", "Enter Phone number ");
                corpOffice_OfficeHelper.TypeText("OfficePhoneNumber", "1234567890");

                executionLog.Log("DeleteOffice", "Enter Business number");
                corpOffice_OfficeHelper.TypeText("BusinessPhoneNumber", "1234567890");

                executionLog.Log("DeleteOffice", "Enter Fax number");
                corpOffice_OfficeHelper.TypeText("FaxNumber", "1234567890");

                executionLog.Log("DeleteOffice", "Enter Linked URL");
                corpOffice_OfficeHelper.TypeText("LinkedURL", "Linked.com");

                executionLog.Log("DeleteOffice", "Enter Facebook url");
                corpOffice_OfficeHelper.TypeText("FacebookURL", "Facebook.com");

                executionLog.Log("DeleteOffice", "Enter TwitterURL");
                corpOffice_OfficeHelper.TypeText("TwitterURL", "Twitter.com");

                executionLog.Log("DeleteOffice", "Select Address");
                corpOffice_OfficeHelper.Select("AddressType", "Office");

                executionLog.Log("DeleteOffice", "Enter AddressLine1");
                corpOffice_OfficeHelper.TypeText("AddressLine1", "FC-89");

                executionLog.Log("DeleteOffice", "Enter Zip Code");
                corpOffice_OfficeHelper.TypeText("ZIpCode", "60601");
                corpOffice_OfficeHelper.WaitForWorkAround(3000);

                executionLog.Log("DeleteOffice", "Enter PrimaryUserName");
                corpOffice_OfficeHelper.TypeText("PrimaryUserName", usernme);

                executionLog.Log("DeleteOffice", "Click on AutoGenPassword checkbox");
                corpOffice_OfficeHelper.ClickElement("AutoGenPassword");

                executionLog.Log("DeleteOffice", "Enter PrimaryPassword");
                corpOffice_OfficeHelper.TypeText("PrimaryPassword", "1qaz!QAZ");

                executionLog.Log("DeleteOffice", "Select Salutation");
                corpOffice_OfficeHelper.Select("Salutation", "Mr");
                corpOffice_OfficeHelper.WaitForWorkAround(2000);

                executionLog.Log("DeleteOffice", "Enter FirstName");
                corpOffice_OfficeHelper.TypeText("FirstName", "Test");

                executionLog.Log("DeleteOffice", "Enter LastName");
                corpOffice_OfficeHelper.TypeText("LastName", "Tester");

                executionLog.Log("DeleteOffice", "Enter eAddress");
                corpOffice_OfficeHelper.TypeText("eAddress", "NewTest@yopmail.com");

                executionLog.Log("DeleteOffice", "Click on save");
                corpOffice_OfficeHelper.ClickElement("Save");

                executionLog.Log("DeleteOffice", "Verify text on the page");
                corpOffice_OfficeHelper.WaitForText("Office created successfully.", 70);

                executionLog.Log("DeleteOffice", "Search office by name.");
                corpOffice_OfficeHelper.TypeText("EnterNameToSearch", name);
                corpOffice_OfficeHelper.WaitForWorkAround(5000);

                executionLog.Log("DeleteOffice", "Click on delete.");
                corpOffice_OfficeHelper.ClickElement("DeleteOffice");
                corpOffice_OfficeHelper.WaitForWorkAround(3000);

                executionLog.Log("DeleteOffice", "Click on confirm delete.");
                corpOffice_OfficeHelper.ClickElement("ConfirmDelete");
                corpOffice_OfficeHelper.WaitForWorkAround(2000);

                executionLog.Log("DeleteOffice", "verify messsage Office deleted successfully.");
                corpOffice_OfficeHelper.WaitForText("Office deleted successfully.", 70);

            }
            catch (Exception e)
            {
                Console.WriteLine("ERRROROOR");
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("DeleteOffice");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Delete Office");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Delete Office", "Bug", "Medium", "Corp office page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Delete Office");
                        TakeScreenshot("DeleteOffice");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\DeleteOffice.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("DeleteOffice");
                        string id = loginHelper.getIssueID("Delete Office");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\DeleteOffice.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Delete Office"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Delete Office");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("DeleteOffice");
                executionLog.WriteInExcel("Delete Office", Status, JIRA, "Corp Offices");
            }
        }
    }
}