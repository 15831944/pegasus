using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class VerifyOmahaAuthGridCreationIssue : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Corp")]
        [TestCategory("Test2")]
        public void verifyOmahaAuthGridCreationIssue()
        {
            string[] username = null;
            string[] password = null;

            XMLParse oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var loginHelper = new LoginHelper(GetWebDriver());
            var executionLog = new ExecutionLog();
            var corpMasterdata_OmahaAuthGridHelper = new CorpMasterdata_OmahaAuthGridHelper(GetWebDriver());

            // Variable
            var name = RandomNumber(1, 999).ToString();
            var code = RandomNumber(1, 999).ToString();
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("VerifyOmahaAuthGridCreationIssue", "Login with valid credential  Username");
                Login("newthemecorp", "pegasus");
                corpMasterdata_OmahaAuthGridHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyOmahaAuthGridCreationIssue", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyOmahaAuthGridCreationIssue", "Redirect To URL");
                VisitCorp("masterdata/omaha_auth_grids");

                executionLog.Log("VerifyOmahaAuthGridCreationIssue", "Verify title");
                VerifyTitle("Corporate Master Omaha Auth Grids");

                executionLog.Log("VerifyOmahaAuthGridCreationIssue", " Click on edit icon.");
                corpMasterdata_OmahaAuthGridHelper.ClickElement("ClickEditAuth");

                executionLog.Log("VerifyOmahaAuthGridCreationIssue", " Wait for locator to be present.");
                corpMasterdata_OmahaAuthGridHelper.WaitForElementPresent("EditGridHeading", 10);

                executionLog.Log("VerifyOmahaAuthGridCreationIssue", "Verify overlay heading");
                corpMasterdata_OmahaAuthGridHelper.VerifyText("EditGridHeading", "Edit Grid ID");

                executionLog.Log("VerifyOmahaAuthGridCreationIssue", " Click on cancel button.");
                corpMasterdata_OmahaAuthGridHelper.ClickElement("Cancel");

                executionLog.Log("VerifyOmahaAuthGridCreationIssue", " Wait for locator to be present.");
                corpMasterdata_OmahaAuthGridHelper.WaitForElementPresent("Create", 10);

                executionLog.Log("VerifyOmahaAuthGridCreationIssue", " Click On Create button.");
                corpMasterdata_OmahaAuthGridHelper.ClickElement("Create");

                executionLog.Log("VerifyOmahaAuthGridCreationIssue", " Wait for locator to be present.");
                corpMasterdata_OmahaAuthGridHelper.WaitForElementPresent("EditGridHeading", 10);

                executionLog.Log("VerifyOmahaAuthGridCreationIssue", " Verify overlay heading");
                corpMasterdata_OmahaAuthGridHelper.VerifyText("EditGridHeading", "Add New Grid ID");

                executionLog.Log("VerifyOmahaAuthGridCreationIssue", "Verify grid id Field empty.");
                corpMasterdata_OmahaAuthGridHelper.VerifyText("GridId", "");

                executionLog.Log("VerifyOmahaAuthGridCreationIssue", "Verify VisaPosAuthfees Field empty.");
                corpMasterdata_OmahaAuthGridHelper.VerifyText("VisaPosAuthfees", "");

                executionLog.Log("VerifyOmahaAuthGridCreationIssue", "Verify MCPosAuthFees  Field empty.");
                corpMasterdata_OmahaAuthGridHelper.VerifyText("MCPosAuthFees", "");

                executionLog.Log("VerifyOmahaAuthGridCreationIssue", "Verify AmexPosAuthFees Field empty.");
                corpMasterdata_OmahaAuthGridHelper.VerifyText("AmexPosAuthFees", "");

                executionLog.Log("VerifyOmahaAuthGridCreationIssue", "Verify DiscPosAuthFees Field empty.");
                corpMasterdata_OmahaAuthGridHelper.VerifyText("DiscPosAuthFees", "");


            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyOmahaAuthGridCreationIssue");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Omaha Auth Grid Creation Issue");
                if (!result)
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Omaha Auth Grid Creation Issue", "Bug", "Medium", "Omha AuthGrid", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Omaha Auth Grid Creation Issue");
                        TakeScreenshot("VerifyOmahaAuthGridCreationIssue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyOmahaAuthGridCreationIssue.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyOmahaAuthGridCreationIssue");
                        string id = loginHelper.getIssueID("Verify Omaha Auth Grid Creation Issue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyOmahaAuthGridCreationIssue.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Omaha Auth Grid Creation Issue"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Omaha Auth Grid Creation Issue");
                executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyOmahaAuthGridCreationIssue");
                executionLog.WriteInExcel("Verify Omaha Auth Grid Creation Issue", Status, JIRA, "Corp Master Data");
            }
        }
    }
}

