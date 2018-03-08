using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class OfficeEaddressLabelIssue : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS2")]
        [TestCategory("BugTestNew")]
        public void officeEaddressLabelIssue()
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
            var usernme = "Sysprins" + RandomNumber(44, 777);
            var name = "Test" + RandomNumber(99, 999);
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("OfficeEaddressLabelIssue", "Login with valid username and password");
                Login(username[0], password[0]);

                executionLog.Log("OfficeEaddressLabelIssue", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("OfficeEaddressLabelIssue", "Redirect to office page.");
                VisitCorp("offices");

                executionLog.Log("OfficeEaddressLabelIssue", "Click on edit office icon");
                corpOffice_OfficeHelper.ClickElement("EditOffice");

                executionLog.Log("OfficeEaddressLabelIssue", "Select eAddress Type");
                corpOffice_OfficeHelper.Select("EaddressType", "Web Links");

                executionLog.Log("OfficeEaddressLabelIssue", "Enter e Address");
                corpOffice_OfficeHelper.TypeText("eAddress", "https://www.mypegasuscrm.com/newthemecorp/offices/edit/1649");

                executionLog.Log("OfficeEaddressLabelIssue", "Save updated details");
                corpOffice_OfficeHelper.ClickElement("SaveEdit");
                corpOffice_OfficeHelper.WaitForWorkAround(3000);

                executionLog.Log("OfficeEaddressLabelIssue", "Click Edit Again");
                corpOffice_OfficeHelper.ClickElement("EditLink");
                corpOffice_OfficeHelper.WaitForWorkAround(3000);

                executionLog.Log("OfficeEaddressLabelIssue", "Verify eAddress Label text.");
                corpOffice_OfficeHelper.VerifyText("EaddressLabel", "Web Link");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("OfficeEaddressLabelIssue");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Office Eaddress Label Issue");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Office Eaddress Label Issue", "Bug", "Medium", "Corp Office page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Office Eaddress Label Issue");
                        TakeScreenshot("OfficeEaddressLabelIssue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\OfficeEaddressLabelIssue.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("OfficeEaddressLabelIssue");
                        string id = loginHelper.getIssueID("Office Eaddress Label Issue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\OfficeEaddressLabelIssue.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Office Eaddress Label Issue"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Office Eaddress Label Issue");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("OfficeEaddressLabelIssue");
                executionLog.WriteInExcel("Office Eaddress Label Issue", Status, JIRA, "Corp Offices");
            }
        }
    }
}