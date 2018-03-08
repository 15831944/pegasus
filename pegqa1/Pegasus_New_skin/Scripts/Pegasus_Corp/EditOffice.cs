using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class EditOffice : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Corp")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Corp")]
        public void editOffice()
        {
            string[] username1 = null;
            string[] password1 = null;
            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var corpOffice_OfficeHelper = new CorpOffice_OfficeHelper(GetWebDriver());

            username1 = oXMLData.getData("settings/Credentials", "username_corp");
            password1 = oXMLData.getData("settings/Credentials", "password");

            // Variable random
            var username = "TESTUSER" + GetRandomNumber();
            var name = "Test" + RandomNumber(99, 999);
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("EditOffice", "Login with valid username and password");
                Login(username1[0], password1[0]);

                executionLog.Log("EditOffice", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("EditOffice", "Redirect to Office");
                VisitCorp("offices");

                executionLog.Log("EditOffice", "Verify Page title");
                VerifyTitle("Offices");

                executionLog.Log("EditOffice", "Click on Edit");
                corpOffice_OfficeHelper.ClickElement("EditOffice");

                executionLog.Log("EditOffice", "Verify Page title");
                VerifyTitle("Edit");

                executionLog.Log("EditOffice", "Enter Website");
                corpOffice_OfficeHelper.TypeText("Website", "TEST.COM");

                executionLog.Log("EditOffice", "Enter VendorName");
                corpOffice_OfficeHelper.TypeText("VendorName", "VenderTEST");

                executionLog.Log("EditOffice", "Enter VendorCode");
                corpOffice_OfficeHelper.TypeText("VendorCode", "1234");

                executionLog.Log("EditOffice", "Enter office phone number");
                corpOffice_OfficeHelper.TypeText("OfficePhoneNumber", "1234567890");

                executionLog.Log("EditOffice", "Enter Business Phone Number");
                corpOffice_OfficeHelper.TypeText("BusinessPhoneNumber", "1234567890");

                executionLog.Log("EditOffice", "Enter FaxNumber");
                corpOffice_OfficeHelper.TypeText("FaxNumber", "1234567890");

                executionLog.Log("EditOffice", "Enter LinkedURL");
                corpOffice_OfficeHelper.TypeText("LinkedURL", "Linked.com");

                executionLog.Log("EditOffice", "Enter FacebookURL");
                corpOffice_OfficeHelper.TypeText("FacebookURL", "Facebook.com");

                executionLog.Log("EditOffice", " Click on save button");
                corpOffice_OfficeHelper.ClickElement("SaveEdit");

                executionLog.Log("EditOffice", " Wait for success message");
                corpOffice_OfficeHelper.WaitForText("Office updated successfully.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("EditOffice");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Edit Office");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Edit Office", "Bug", "Medium", "Office page", "QA", "Log in as: " + username1[0] + " / " + password1[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Edit Office");
                        TakeScreenshot("EditOffice");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditOffice.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("EditOffice");
                        string id = loginHelper.getIssueID("Edit Office");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditOffice.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Edit Office"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Edit Office");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("EditOffice");
                executionLog.WriteInExcel("Edit Office", Status, JIRA, "Corp Office");
            }
        }
    }
}