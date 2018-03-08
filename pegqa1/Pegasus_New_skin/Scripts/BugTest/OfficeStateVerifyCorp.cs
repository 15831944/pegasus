using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class OfficeStateVerifyCorp : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void officeStateVerifyCorp()
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
            var corp_Office_OfficeHelper = new CorpOffice_OfficeHelper(GetWebDriver());

            // Variable random
            var Email = "addressTest" + GetRandomNumber() + "@yop.com";
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("OfficeStateVerifyCorp", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("OfficeStateVerifyCorp", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("OfficeStateVerifyCorp", "Craete Office");
                VisitCorp("offices/create");

                executionLog.Log("OfficeStateVerifyCorp", "Verify Page title");
                VerifyTitle("Create an Office");

                executionLog.Log("OfficeStateVerifyCorp", "Enter Name");
                corp_Office_OfficeHelper.TypeText("Name", "Office OFFICE");

                executionLog.Log("OfficeStateVerifyCorp", "Enter Address");
                corp_Office_OfficeHelper.Select("AddressType", "Office");

                executionLog.Log("OfficeStateVerifyCorp", "Enter AddressLine1");
                corp_Office_OfficeHelper.TypeText("AddressLine1", "FC-89");

                corp_Office_OfficeHelper.TypeText("ZIpCode", "60601");
                corp_Office_OfficeHelper.WaitForWorkAround(2000);

                executionLog.Log("OfficeStateVerifyCorp", "Enter Username");
                corp_Office_OfficeHelper.TypeText("PrimaryUserName", "Tes");

                executionLog.Log("OfficeStateVerifyCorp", "Enter First NAME");
                corp_Office_OfficeHelper.TypeText("FirstName", "User");

                executionLog.Log("OfficeStateVerifyCorp", "Enter Last Name");
                corp_Office_OfficeHelper.TypeText("LastName", "Test");

                executionLog.Log("OfficeStateVerifyCorp", "Enter eAddress");
                corp_Office_OfficeHelper.TypeText("eAddress", Email);

                executionLog.Log("OfficeStateVerifyCorp", " Click on Save");
                corp_Office_OfficeHelper.ClickElement("Save");
                corp_Office_OfficeHelper.WaitForWorkAround(5000);

                executionLog.Log("OfficeStateVerifyCorp", "Verify text on the page");
                corp_Office_OfficeHelper.VerifyText("SelectState", "IL");
                corp_Office_OfficeHelper.WaitForWorkAround(3000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("OfficeStateVerifyCorp");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Office State Verify Corp");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Office State Verify Corp", "Bug", "Medium", "Office page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Office State Verify Corp");
                        TakeScreenshot("OfficeStateVerifyCorp");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\OfficeStateVerifyCorp.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("OfficeStateVerifyCorp");
                        string id = loginHelper.getIssueID("Office State Verify Corp");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\OfficeStateVerifyCorp.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Office State Verify Corp"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Office State Verify Corp");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("OfficeStateVerifyCorp");
                executionLog.WriteInExcel("Office State Verify Corp", Status, JIRA, "Corp Offices");
            }
        }
    }
}