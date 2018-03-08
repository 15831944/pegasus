using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class VerifyingOfficeEadressIssues : DriverTestCase
    {
        [TestMethod]
        [TestCategory("Corp")]
        [TestCategory("All")]
        [TestCategory("Aslam_Corp")]
        public void verifyingOfficeEadressIssues()
        {
            string[] username1 = null;
            string[] password1 = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username1 = oXMLData.getData("settings/Credentials", "username_corp");
            password1 = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var corpOffice_OfficeHelper = new CorpOffice_OfficeHelper(GetWebDriver());

            // Variable random
            String JIRA = "";
            String Status = "Pass";

          try
            {

            executionLog.Log("VerifyingOfficeEadressIssues", "Login with valid username and password");
            Login("newthemecorp", "pegasus");

            executionLog.Log("VerifyingOfficeEadressIssues", "Verify Page title");
            VerifyTitle("Dashboard");
            Console.WriteLine("Redirected at Dashboard screen.");

            executionLog.Log("VerifyingOfficeEadressIssues", "Go to office page");
            VisitCorp("offices");

            executionLog.Log("VerifyingOfficeEadressIssues", "Verify title");
            VerifyTitle("Offices");

            executionLog.Log("VerifyingOfficeEadressIssues", "Click On Create button");
            corpOffice_OfficeHelper.ClickElement("Create");

            executionLog.Log("VerifyingOfficeEadressIssues", "Verify title");
            VerifyTitle("Create an Office");

            executionLog.Log("VerifyingOfficeEadressIssues", "Verify title");
            corpOffice_OfficeHelper.ClickElement("AddEmail");
            corpOffice_OfficeHelper.WaitForWorkAround(1000);

            executionLog.Log("VerifyingOfficeEadressIssues", "Verifies first eaddress label a drop down.");
            corpOffice_OfficeHelper.Select("EaddressLabel", "Home");
            corpOffice_OfficeHelper.WaitForWorkAround(1000);

            executionLog.Log("VerifyingOfficeEadressIssues", "Select eAddress type as social media.");
            corpOffice_OfficeHelper.Select("Eadresstype2", "Social Media");
            corpOffice_OfficeHelper.WaitForWorkAround(1000);

            executionLog.Log("VerifyingOfficeEadressIssues", "Verify eAddress label contains social media options.");
            corpOffice_OfficeHelper.VerifySocialMedia();

        }
        catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyingOfficeEadressIssues");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verifying Office Eadress Issues");
                if (!result)
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verifying Office Eadress Issues", "Bug", "Medium", "Office page", "QA", "Log in as: " + username1[0] + " / " + password1[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verifying Office Eadress Issues");
                        TakeScreenshot("VerifyingOfficeEadressIssues");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyingOfficeEadressIssues.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyingOfficeEadressIssues");
                        string id = loginHelper.getIssueID("Verifying Office Eadress Issues");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyingOfficeEadressIssues.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verifying Office Eadress Issues"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verifying Office Eadress Issues");
                executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyingOfficeEadressIssues");
                executionLog.WriteInExcel("Verifying Office Eadress Issues", Status, JIRA, "Corp Office");
            }
        }
    }
}

