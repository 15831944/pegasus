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
    public class VerifyingOmahaAuthGrid500error : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS3")]
        [TestCategory("BugTestNew")]
        public void verifyingOmahaAuthGrid500error()
        {
            string[] username = null;
            string[] password = null;

            XMLParse oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_corp");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var loginHelper = new LoginHelper(GetWebDriver());
            var executionLog = new ExecutionLog();
            var corpMasterdata_OmahaAuthGridHelper = new CorpMasterdata_OmahaAuthGridHelper(GetWebDriver());

            // Variable
            var name = RandomNumber(1, 999999).ToString();
            var code = RandomNumber(1, 999999).ToString();
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("VerifyingOmahaAuthGrid500error", "Login with valid credentials");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyingOmahaAuthGrid500error", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyingOmahaAuthGrid500error", "Redirect To URL");
                VisitCorp("masterdata/omaha_auth_grids");
                corpMasterdata_OmahaAuthGridHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyingOmahaAuthGrid500error", "Verify Page title");
                VerifyTitle("Corporate Master Omaha Auth Grids");

                executionLog.Log("VerifyingOmahaAuthGrid500error", " Click On Create");
                corpMasterdata_OmahaAuthGridHelper.ClickElement("Create");
                corpMasterdata_OmahaAuthGridHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyingOmahaAuthGrid500error", "Enter Grid Id");
                corpMasterdata_OmahaAuthGridHelper.TypeText("GridId", name);

                executionLog.Log("VerifyingOmahaAuthGrid500error", "Enter Visa Pos Authfees");
                corpMasterdata_OmahaAuthGridHelper.TypeText("VisaPosAuthfees", code);

                executionLog.Log("VerifyingOmahaAuthGrid500error", "Enter MC Pos Auth Fees");
                corpMasterdata_OmahaAuthGridHelper.TypeText("MCPosAuthFees", name);

                executionLog.Log("VerifyingOmahaAuthGrid500error", "Enter Amex Pos AuthFees");
                corpMasterdata_OmahaAuthGridHelper.TypeText("AmexPosAuthFees", name);

                executionLog.Log("VerifyingOmahaAuthGrid500error", "Enter Disc Pos Auth Fees");
                corpMasterdata_OmahaAuthGridHelper.TypeText("DiscPosAuthFees", code);

                executionLog.Log("VerifyingOmahaAuthGrid500error", "Enter JCD Pos Auth Fees");
                corpMasterdata_OmahaAuthGridHelper.TypeText("JCDPosAuthFees", name);

                executionLog.Log("VerifyingOmahaAuthGrid500error", "Enter Voice Auth Fees");
                corpMasterdata_OmahaAuthGridHelper.TypeText("VoiceAuthFees", code);

                executionLog.Log("VerifyingOmahaAuthGrid500error", "Enter AVS Electronic Fees");
                corpMasterdata_OmahaAuthGridHelper.TypeText("AVSElectronicFees", name);

                executionLog.Log("VerifyingOmahaAuthGrid500error", "Enter AVS Voice Fees");
                corpMasterdata_OmahaAuthGridHelper.TypeText("AVSVoiveFees", code);

                executionLog.Log("VerifyingOmahaAuthGrid500error", "Enter AVS Voive Fees");
                corpMasterdata_OmahaAuthGridHelper.TypeText("ARUFees", name);

                executionLog.Log("VerifyingOmahaAuthGrid500error", "Click on Save button");
                corpMasterdata_OmahaAuthGridHelper.ClickElement("Save");
                corpMasterdata_OmahaAuthGridHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyingOmahaAuthGrid500error", "Verify 500 error not present.");
                corpMasterdata_OmahaAuthGridHelper.VerifyTextNotPresent("500 Internal sever error.");

                executionLog.Log("VerifyingOmahaAuthGrid500error", "Verify Page Text");
                corpMasterdata_OmahaAuthGridHelper.WaitForText("Master Omaha Auth Grid Created Successfully.", 10);

                executionLog.Log("VerifyingOmahaAuthGrid500error", "Enter Name to search");
                corpMasterdata_OmahaAuthGridHelper.TypeText("GridIdSearch", name);
                corpMasterdata_OmahaAuthGridHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyingOmahaAuthGrid500error", "Click Delete btn");
                corpMasterdata_OmahaAuthGridHelper.ClickElement("ClickOnDelete");

                executionLog.Log("VerifyingOmahaAuthGrid500error", "Accept alert message. ");
                corpMasterdata_OmahaAuthGridHelper.AcceptAlert();

                executionLog.Log("VerifyingOmahaAuthGrid500error", "Wait for delete message. ");
                corpMasterdata_OmahaAuthGridHelper.WaitForText("Corporate Master Omaha Auth Grid Deleted Successfully.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyingOmahaAuthGrid500error");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("VerifyingOmahaAuthGrid500error");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("VerifyingOmahaAuthGrid500error", "Bug", "Medium", "Omha AuthGrid", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("VerifyingOmahaAuthGrid500error");
                        TakeScreenshot("VerifyingOmahaAuthGrid500error");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyingOmahaAuthGrid500error.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyingOmahaAuthGrid500error");
                        string id = loginHelper.getIssueID("VerifyingOmahaAuthGrid500error");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyingOmahaAuthGrid500error.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("VerifyingOmahaAuthGrid500error"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("VerifyingOmahaAuthGrid500error");
                //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyingOmahaAuthGrid500error");
                executionLog.WriteInExcel("VerifyingOmahaAuthGrid500error", Status, JIRA, "Corp Master Data");
            }
        }
    }
}
