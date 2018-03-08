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
    public class CreateOmahaAuthGrid : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        [TestCategory("Fail")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin")]
        public void createOmahaAuthGrid()
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
            String name = "8" + RandomNumber(111, 999);
            String code = "1" + RandomNumber(111, 999);
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("CreateOmahaAuthGrid", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("CreateOmahaAuthGrid", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("CreateOmahaAuthGrid", "Click On  Admin");
                VisitOffice("admin");

                executionLog.Log("CreateOmahaAuthGrid", "Redirect To AuthGrid");
                VisitOffice("omaha_auth_grids");
                masterData_OmahaAuthGridHelper.WaitForWorkAround(4000);

                executionLog.Log("CreateOmahaAuthGrid", "Verify title");
                VerifyTitle("Omaha Auth Grids");

                executionLog.Log("CreateOmahaAuthGrid", " Click On Create");
                masterData_OmahaAuthGridHelper.ClickElement("Create");
                masterData_OmahaAuthGridHelper.WaitForWorkAround(4000);

                executionLog.Log("CreateOmahaAuthGrid", "Enter Grid Id");
                masterData_OmahaAuthGridHelper.TypeText("GridId", name);
                masterData_OmahaAuthGridHelper.WaitForWorkAround(500);

                executionLog.Log("CreateOmahaAuthGrid", "Enter Visa Pos Authfees");
                masterData_OmahaAuthGridHelper.TypeText("VisaPosAuthfees", code);
                masterData_OmahaAuthGridHelper.WaitForWorkAround(500);

                executionLog.Log("CreateOmahaAuthGrid", "Enter MC Pos Auth Fees");
                masterData_OmahaAuthGridHelper.TypeText("MCPosAuthFees", code);
                masterData_OmahaAuthGridHelper.WaitForWorkAround(500);

                executionLog.Log("CreateOmahaAuthGrid", "Enter Amex Pos AuthFees");
                masterData_OmahaAuthGridHelper.TypeText("AmexPosAuthFees", code);
                masterData_OmahaAuthGridHelper.WaitForWorkAround(500);

                executionLog.Log("CreateOmahaAuthGrid", "Enter Disc Pos Auth Fees");
                masterData_OmahaAuthGridHelper.TypeText("DiscPosAuthFees", code);
                masterData_OmahaAuthGridHelper.WaitForWorkAround(500);

                executionLog.Log("CreateOmahaAuthGrid", "Enter JCD Pos Auth Fees");
                masterData_OmahaAuthGridHelper.TypeText("JCBPosAuthFees", code);
                masterData_OmahaAuthGridHelper.WaitForWorkAround(500);

                executionLog.Log("CreateOmahaAuthGrid", "Enter Voice Auth Fees");
                masterData_OmahaAuthGridHelper.TypeText("VoiceAuthFees", code);
                masterData_OmahaAuthGridHelper.WaitForWorkAround(500);

                executionLog.Log("CreateOmahaAuthGrid", "Enter AVS Electronic Fees");
                masterData_OmahaAuthGridHelper.TypeText("AVSElectronicFees", code);
                masterData_OmahaAuthGridHelper.WaitForWorkAround(500);

                executionLog.Log("CreateOmahaAuthGrid", "Enter AVS Voice Fees");
                masterData_OmahaAuthGridHelper.TypeText("AVSVoiveFees", code);
                masterData_OmahaAuthGridHelper.WaitForWorkAround(500);


                executionLog.Log("CreateOmahaAuthGrid", "Enter AVS Voive Fees");
                masterData_OmahaAuthGridHelper.TypeText("ARUFee", code);
                masterData_OmahaAuthGridHelper.WaitForWorkAround(500);

                executionLog.Log("CreateOmahaAuthGrid", "  Click on Save button");
                masterData_OmahaAuthGridHelper.ClickElement("Save");
                masterData_OmahaAuthGridHelper.WaitForWorkAround(2000);

                executionLog.Log("CreateOmahaAuthGrid", "Wait for text");
                masterData_OmahaAuthGridHelper.WaitForText("Master Omaha Auth Grid Created Successfully.", 10);
                masterData_OmahaAuthGridHelper.WaitForWorkAround(1000);

                executionLog.Log("CreateOmahaAuthGrid", "Click on delete icon");
                masterData_OmahaAuthGridHelper.ClickElement("DeleteIcon");
                masterData_OmahaAuthGridHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateOmahaAuthGrid", "Click ok to accept alert");
                masterData_OmahaAuthGridHelper.AcceptAlert();
                masterData_OmahaAuthGridHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateOmahaAuthGrid", "Wait for delete message. ");
                masterData_OmahaAuthGridHelper.WaitForText("Master Omaha Auth Grid Deleted Successfully.", 10);
                masterData_OmahaAuthGridHelper.WaitForWorkAround(3000);

                VisitOffice("logout");
            }

            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CreateOmahaAuthGrid");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Create Omaha AuthGrid");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Create Omaha AuthGrid", "Bug", "Medium", "Omaha Auth Grid page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Create Omaha AuthGrid");
                        TakeScreenshot("CreateOmahaAuthGrid");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateOmahaAuthGrid.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CreateOmahaAuthGrid");
                        string id = loginHelper.getIssueID("Create Omaha AuthGrid");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateOmahaAuthGrid.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Create Omaha AuthGrid"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Create Omaha AuthGrid");
               // executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("CreateOmahaAuthGrid");
                executionLog.WriteInExcel("Create Omaha AuthGrid", Status, JIRA, "Office Master Data");
            }
        }
    }
}
