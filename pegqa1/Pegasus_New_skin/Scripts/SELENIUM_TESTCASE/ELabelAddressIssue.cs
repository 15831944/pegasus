using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class ELabelAddressIssue : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("SELENIUM_TESTCASE")]
        [TestCategory("TS8")]
        public void elabelAddressIssue()
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
            var office_ClientsHelper = new Office_ClientsHelper(GetWebDriver());

            // Variable

            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("ELabelAddressIssue", "Login with valid credential  Username");
                Login(username[0], password[0]);

                executionLog.Log("ELabelAddressIssue", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("ELabelAddressIssue", "Goto Merchant.");
                VisitOffice("clients");
                office_ClientsHelper.WaitForWorkAround(5000);

                executionLog.Log("ELabelAddressIssue", "Open First Merchant");
                office_ClientsHelper.ClickElement("Open1stMerchant");
                office_ClientsHelper.WaitForWorkAround(5000);

                executionLog.Log("ELabelAddressIssue", "Click on  Contact Tab.");
                office_ClientsHelper.ClickForce("ClickOnContactTab");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("ELabelAddressIssue", "Click on Add Another Button.");
                office_ClientsHelper.ClickForce("AddAnotherContact");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("ELabelAddressIssue", "Click on Add Another Button.");
                office_ClientsHelper.ClickForce("ContactTab2");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("ELabelAddressIssue", "Select Address Type.");
                office_ClientsHelper.scrollToElement("eAddressType2");
                office_ClientsHelper.WaitForWorkAround(3000);
                office_ClientsHelper.SelectByText("eAddressType2", "IM");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("ELabelAddressIssue", "Verify eAddress Label");
                office_ClientsHelper.verifyselectedOptn("eAddressLabelDrpDwn", "Google");
                office_ClientsHelper.WaitForWorkAround(1000);

                executionLog.Log("ELabelAddressIssue", "Select Address Type.");
                office_ClientsHelper.SelectByText("eAddressType2", "Social Media");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("ELabelAddressIssue", "Verify eAddress Label");
                office_ClientsHelper.verifyselectedOptn("eAddressLabelDrpDwn", "Facebook");
                office_ClientsHelper.WaitForWorkAround(1000);

                executionLog.Log("ELabelAddressIssue", "Select Address Type.");
                office_ClientsHelper.SelectByText("eAddressType2", "Web Links");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("ELabelAddressIssue", "Verify eAddress Label");
                office_ClientsHelper.verifyselectedOptn("eAddressLabelDrpDwn", "Web Link");
                office_ClientsHelper.WaitForWorkAround(1000);

            }

            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("ELabelAddressIssue");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("eLabel Address Issue");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("eLabel Address Issue", "Bug", "Medium", "Amex page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("eLabel Address Issue");
                        TakeScreenshot("ELabelAddressIssue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ELabelAddressIssue.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("ELabelAddressIssue");
                        string id = loginHelper.getIssueID("eLabel Address Issue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ELabelAddressIssue.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("eLabel Address Issue"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("eLabel Address Issue");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("ELabelAddressIssue");
                executionLog.WriteInExcel("eLabel Address Issue", Status, JIRA, "Corp Master Data");
            }
        }
    }
}
