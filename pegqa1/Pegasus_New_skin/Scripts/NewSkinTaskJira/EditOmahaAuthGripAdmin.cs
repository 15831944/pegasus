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
    public class EditOmahaAuthGripAdmin : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS4")]
        [TestCategory("NewSkinTaskJira")]
        public void editOmahaAuthGripAdmin()
        {
            string[] username = null;
            string[] password = null;

            XMLParse oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var masterData_OmahaAuthGridHelper = new MasterData_OmahaAuthGridHelper(GetWebDriver());

            // Variable
            var name = "9" + RandomNumber(99, 999);
            var code = "1" + RandomNumber(99, 999);
            String Status = "Pass";
            String JIRA = "";

            try
            {
                executionLog.Log("EditOmahaAuthGripAdmin", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("EditOmahaAuthGripAdmin", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("EditOmahaAuthGripAdmin", "Click On  Admin");
                VisitOffice("admin");

                executionLog.Log("EditOmahaAuthGripAdmin", "Redirect To auth grid");
                VisitOffice("omaha_auth_grids");
                masterData_OmahaAuthGridHelper.WaitForWorkAround(3000);

                executionLog.Log("EditOmahaAuthGripAdmin", " Click On Create");
                masterData_OmahaAuthGridHelper.ClickElement("Create");
                masterData_OmahaAuthGridHelper.WaitForWorkAround(2000);

                executionLog.Log("EditOmahaAuthGripAdmin", "Enter Grid Id");
                masterData_OmahaAuthGridHelper.TypeText("GridId", name);

                executionLog.Log("EditOmahaAuthGripAdmin", "Enter Visa Pos Authfees");
                masterData_OmahaAuthGridHelper.TypeText("VisaPosAuthfees", code);

                executionLog.Log("EditOmahaAuthGripAdmin", "Enter MC Pos Auth Fees");
                masterData_OmahaAuthGridHelper.TypeText("MCPosAuthFees", name);

                executionLog.Log("EditOmahaAuthGripAdmin", "Enter Amex Pos AuthFees");
                masterData_OmahaAuthGridHelper.TypeText("AmexPosAuthFees", name);

                executionLog.Log("EditOmahaAuthGripAdmin", "Enter Disc Pos Auth Fees");
                masterData_OmahaAuthGridHelper.TypeText("DiscPosAuthFees", code);

                executionLog.Log("EditOmahaAuthGripAdmin", "Enter JCD Pos Auth Fees");
                masterData_OmahaAuthGridHelper.TypeText("JCBPosAuthFees", name);

                executionLog.Log("EditOmahaAuthGripAdmin", "Enter Voice Auth Fees");
                masterData_OmahaAuthGridHelper.TypeText("VoiceAuthFees", code);

                executionLog.Log("EditOmahaAuthGripAdmin", "Enter AVS Electronic Fees");
                masterData_OmahaAuthGridHelper.TypeText("AVSElectronicFees", name);

                executionLog.Log("EditOmahaAuthGripAdmin", "Enter AVS Voice Fees");
                masterData_OmahaAuthGridHelper.TypeText("AVSVoiveFees", code);

                executionLog.Log("EditOmahaAuthGripAdmin", "Enter AVS Voive Fees");
                masterData_OmahaAuthGridHelper.TypeText("ARUFee", name);

                executionLog.Log("EditOmahaAuthGripAdmin", "  Click on Save button");
                masterData_OmahaAuthGridHelper.ClickElement("Save");
                masterData_OmahaAuthGridHelper.WaitForWorkAround(5000);

                executionLog.Log("EditOmahaAuthGripAdmin", "Click on Edit ");
                masterData_OmahaAuthGridHelper.ClickElement("ClickEdit");
                masterData_OmahaAuthGridHelper.WaitForWorkAround(3000);

                executionLog.Log("EditOmahaAuthGripAdmin", "Enter Grid Id");
                masterData_OmahaAuthGridHelper.TypeText("GridId", name);

                executionLog.Log("EditOmahaAuthGripAdmin", "Enter Visa Pos Authfees");
                masterData_OmahaAuthGridHelper.TypeText("VisaPosAuthfees", code);

                executionLog.Log("EditOmahaAuthGripAdmin", "Enter MC Pos Auth Fees");
                masterData_OmahaAuthGridHelper.TypeText("MCPosAuthFees", name);

                executionLog.Log("EditOmahaAuthGripAdmin", "Enter Amex Pos AuthFees");
                masterData_OmahaAuthGridHelper.TypeText("AmexPosAuthFees", name);

                executionLog.Log("EditOmahaAuthGripAdmin", "Enter Disc Pos Auth Fees");
                masterData_OmahaAuthGridHelper.TypeText("DiscPosAuthFees", code);

                executionLog.Log("EditOmahaAuthGripAdmin", "Enter JCD Pos Auth Fees");
                masterData_OmahaAuthGridHelper.TypeText("JCBPosAuthFees", name);

                executionLog.Log("EditOmahaAuthGripAdmin", "Enter Voice Auth Fees");
                masterData_OmahaAuthGridHelper.TypeText("VoiceAuthFees", code);

                executionLog.Log("EditOmahaAuthGripAdmin", "Enter AVS Electronic Fees");
                masterData_OmahaAuthGridHelper.TypeText("AVSElectronicFees", name);

                executionLog.Log("EditOmahaAuthGripAdmin", "Enter AVS Voice Fees");
                masterData_OmahaAuthGridHelper.TypeText("AVSVoiveFees", code);

                executionLog.Log("EditOmahaAuthGripAdmin", "Enter ARU Voive Fees");
                masterData_OmahaAuthGridHelper.TypeText("ARUFee", name);

                executionLog.Log("EditOmahaAuthGripAdmin", "  Click on Save button");
                masterData_OmahaAuthGridHelper.ClickElement("Save");
                masterData_OmahaAuthGridHelper.WaitForWorkAround(3000);

                executionLog.Log("EditOmahaAuthGripAdmin", "Click on delete icon");
                masterData_OmahaAuthGridHelper.ClickElement("DeleteIcon");

                executionLog.Log("EditOmahaAuthGripAdmin", "Click ok to accept alert");
                masterData_OmahaAuthGridHelper.AcceptAlert();

                executionLog.Log("EditOmahaAuthGripAdmin", "Wait for delete message. ");
                masterData_OmahaAuthGridHelper.WaitForText("Master Omaha Auth Grid Deleted Successfully.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";
                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("EditOmahaAuthGripAdmin");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Edit Omaha Auth Grip Admin");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Edit Omaha Auth Grip Admin", "Bug", "Medium", "Partner page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Edit Omaha Auth Grip Admin");
                        TakeScreenshot("EditOmahaAuthGripAdmin");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditOmahaAuthGripAdmin.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("EditOmahaAuthGripAdmin");
                        string id = loginHelper.getIssueID("Edit Omaha Auth Grip Admin");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditOmahaAuthGripAdmin.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Edit Omaha Auth Grip Admin"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Edit Omaha Auth Grip Admin");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("EditOmahaAuthGripAdmin");
                executionLog.WriteInExcel("Edit Omaha Auth Grip Admin", Status, JIRA, "Master Data");
            }
        }
    }
}
