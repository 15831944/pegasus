using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class LeadeAddressLabelAutoUpdate : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("SELENIUM_TESTCASE")]
        [TestCategory("Fail")]
        [TestCategory("TS8")]
        public void leadeAddressLabelAutoUpdate()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var office_LeadsHelper = new Office_LeadsHelper(GetWebDriver());

            // Variable
            var FirstName = "Test" + RandomNumber(1111, 99999);
            var Company = "Lead COmp" + RandomNumber(221212, 999999);
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("LeadeAddressLabelAutoUpdate", "Login with valid username and password");
                Login(username[0], password[0]);

                executionLog.Log("LeadeAddressLabelAutoUpdate", "Verify Page title");
                VerifyTitle("Dashboard");
                office_LeadsHelper.WaitForWorkAround(5000);

                executionLog.Log("LeadeAddressLabelAutoUpdate", "Visit Lead");
                VisitOffice("leads");
                office_LeadsHelper.WaitForWorkAround(5000);

                executionLog.Log("LeadeAddressLabelAutoUpdate", "Open any lead.");
                office_LeadsHelper.ClickElement("Lead1");
                office_LeadsHelper.WaitForWorkAround(5000);

                executionLog.Log("LeadeAddressLabelAutoUpdate", "Click on Company detail");
                office_LeadsHelper.ClickElement("CompanyDetails");
                office_LeadsHelper.WaitForWorkAround(5000);

                executionLog.Log("LeadeAddressLabelAutoUpdate", "Select eAddress Type as IM.");
                office_LeadsHelper.SelectByText("eAddressType", "IM");

                executionLog.Log("LeadeAddressLabelAutoUpdate", "Click on Save button.");
                office_LeadsHelper.ClickElement("SaveLeadButton");

                executionLog.Log("LeadeAddressLabelAutoUpdate", "Verify selected eAddress type is IM");
                office_LeadsHelper.VerifySelectedOption("//*[@id='LeadElectronicAddress0ElectronicContentType']", "IM");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("LeadeAddressLabelAutoUpdate");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Lead eAddress Label AutoUpdate");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Lead eAddress Label AutoUpdate", "Bug", "Medium", "Lead page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Lead eAddress Label AutoUpdate");
                        TakeScreenshot("LeadeAddressLabelAutoUpdate");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\LeadeAddressLabelAutoUpdate.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("LeadeAddressLabelAutoUpdate");
                        string id = loginHelper.getIssueID("Lead eAddress Label AutoUpdate");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\LeadeAddressLabelAutoUpdate.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Lead eAddress Label AutoUpdate"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Lead eAddress Label AutoUpdate");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("LeadeAddressLabelAutoUpdate");
                executionLog.WriteInExcel("Lead eAddress Label AutoUpdate", Status, JIRA, "Leads Management");
            }
        }
    }
}

