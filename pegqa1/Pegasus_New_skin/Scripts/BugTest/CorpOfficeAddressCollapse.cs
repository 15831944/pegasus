using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class CorpOfficeAddressCollapse : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void corpOfficeAddressCollapse()
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



            //  Variable random
            var name = "Test" + RandomNumber(1, 99);
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("CorpOfficeAddressCollapse", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("CorpOfficeAddressCollapse", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("CorpOfficeAddressCollapse", "Craete Office");
                VisitCorp("offices/create");

                executionLog.Log("CorpOfficeAddressCollapse", "Verify page title");
                VerifyTitle("Create an Office");

                executionLog.Log("CorpOfficeAddressCollapse", "Click Add Addresses Button");
                corp_Office_OfficeHelper.ClickElement("ClickAddAddressBtn");

                executionLog.Log("CorpOfficeAddressCollapse", "Verify text");
                corp_Office_OfficeHelper.VerifyText("VerifyLabelAddressType", "Address Type");
                corp_Office_OfficeHelper.WaitForWorkAround(2000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CorpOfficeAddressCollapse");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Corp Office Address Collapse");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Corp Office Address Collapse", "Bug", "Medium", "Corp Office", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Corp Office Address Collapse");
                        TakeScreenshot("CorpOfficeAddressCollapse");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CorpOfficeAddressCollapse.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CorpOfficeAddressCollapse");
                        string id = loginHelper.getIssueID("Corp Office Address Collapse");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CorpOfficeAddressCollapse.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Corp Office Address Collapse"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Corp Office Address Collapse");
                //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("CorpOfficeAddressCollapse");
                executionLog.WriteInExcel("Corp Office Address Collapse", Status, JIRA, "Office Corp");
            }
        }
    }
}