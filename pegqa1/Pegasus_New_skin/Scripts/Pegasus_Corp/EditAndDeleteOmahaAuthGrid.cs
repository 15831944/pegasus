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
    public class EditAndDeleteOmahaAuthGrid : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Corp")]
        [TestCategory("Fail")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Corp")]
        public void editAndDeleteOmahaAuthGrid()
        {
            string[] username = null;
            string[] password = null;
            XMLParse oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            // Initializing the objects
            var loginHelper = new LoginHelper(GetWebDriver());
            var executionLog = new ExecutionLog();
            var corpMasterdata_OmahaAuthGridHelper = new CorpMasterdata_OmahaAuthGridHelper(GetWebDriver());
            username = oXMLData.getData("settings/Credentials", "username_corp");
            password = oXMLData.getData("settings/Credentials", "password");

            // Variable
            var name = RandomNumber(1, 999).ToString();
            var code = "1" + RandomNumber(1, 99);
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("EditAndDeleteOmahaAuthGrid", "Login with valid credential  Username");
                Login(username[0], password[0]);

                executionLog.Log("EditAndDeleteOmahaAuthGrid", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("EditAndDeleteOmahaAuthGrid", "Redirect To URL");
                VisitCorp("masterdata/omaha_auth_grids");
                corpMasterdata_OmahaAuthGridHelper.WaitForWorkAround(3000);

                executionLog.Log("EditAndDeleteOmahaAuthGrid", "Verify Page title");
                VerifyTitle("Corporate Master Omaha Auth Grids");

                executionLog.Log("EditAndDeleteOmahaAuthGrid", " Click On Create");
                corpMasterdata_OmahaAuthGridHelper.ClickElement("Create");
                corpMasterdata_OmahaAuthGridHelper.WaitForWorkAround(3000);

                executionLog.Log("EditAndDeleteOmahaAuthGrid", "Enter Grid Id");
                corpMasterdata_OmahaAuthGridHelper.TypeText("GridId", name);

                executionLog.Log("EditAndDeleteOmahaAuthGrid", "Enter Visa Pos Authfees");
                corpMasterdata_OmahaAuthGridHelper.TypeText("VisaPosAuthfees", code);

                executionLog.Log("EditAndDeleteOmahaAuthGrid", "Enter MC Pos Auth Fees");
                corpMasterdata_OmahaAuthGridHelper.TypeText("MCPosAuthFees", name);

                executionLog.Log("EditAndDeleteOmahaAuthGrid", "Enter Amex Pos AuthFees");
                corpMasterdata_OmahaAuthGridHelper.TypeText("AmexPosAuthFees", name);

                executionLog.Log("EditAndDeleteOmahaAuthGrid", "Enter Disc Pos Auth Fees");
                corpMasterdata_OmahaAuthGridHelper.TypeText("DiscPosAuthFees", code);

                executionLog.Log("EditAndDeleteOmahaAuthGrid", "Enter JCD Pos Auth Fees");
                corpMasterdata_OmahaAuthGridHelper.TypeText("JCDPosAuthFees", name);

                executionLog.Log("EditAndDeleteOmahaAuthGrid", "Enter Voice Auth Fees");
                corpMasterdata_OmahaAuthGridHelper.TypeText("VoiceAuthFees", code);

                executionLog.Log("EditAndDeleteOmahaAuthGrid", "Enter AVS Electronic Fees");
                corpMasterdata_OmahaAuthGridHelper.TypeText("AVSElectronicFees", name);

                executionLog.Log("EditAndDeleteOmahaAuthGrid", "Enter AVS Voice Fees");
                corpMasterdata_OmahaAuthGridHelper.TypeText("AVSVoiveFees", code);

                executionLog.Log("EditAndDeleteOmahaAuthGrid", "Enter AVS Voive Fees");
                corpMasterdata_OmahaAuthGridHelper.TypeText("ARUFees", name);

                executionLog.Log("EditAndDeleteOmahaAuthGrid", "  Click on Save button");
                corpMasterdata_OmahaAuthGridHelper.ClickElement("Save");

                executionLog.Log("EditAndDeleteOmahaAuthGrid", "Verify Page Text");
                corpMasterdata_OmahaAuthGridHelper.WaitForText("Master Omaha Auth Grid Created Successfully.", 10);
                corpMasterdata_OmahaAuthGridHelper.WaitForWorkAround(4000);

                executionLog.Log("EditAndDeleteOmahaAuthGrid", "Search GripIdSrch");
                corpMasterdata_OmahaAuthGridHelper.TypeText("GridIdSearch", name);
                corpMasterdata_OmahaAuthGridHelper.WaitForWorkAround(2000);

                executionLog.Log("EditAndDeleteOmahaAuthGrid", "Click on  Edit");
                corpMasterdata_OmahaAuthGridHelper.ClickElement("ClickEditAuth");
                corpMasterdata_OmahaAuthGridHelper.WaitForWorkAround(3000);

                executionLog.Log("EditAndDeleteOmahaAuthGrid", "Enter Grid Id");
                corpMasterdata_OmahaAuthGridHelper.TypeText("GridId", name);

                executionLog.Log("EditAndDeleteOmahaAuthGrid", "Enter Visa Pos Authfees");
                corpMasterdata_OmahaAuthGridHelper.TypeText("VisaPosAuthfees", code);

                executionLog.Log("EditAndDeleteOmahaAuthGrid", "Enter MC Pos Auth Fees");
                corpMasterdata_OmahaAuthGridHelper.TypeText("MCPosAuthFees", name);

                executionLog.Log("EditAndDeleteOmahaAuthGrid", "Enter Amex Pos AuthFees");
                corpMasterdata_OmahaAuthGridHelper.TypeText("AmexPosAuthFees", name);

                executionLog.Log("EditAndDeleteOmahaAuthGrid", "Enter Disc Pos Auth Fees");
                corpMasterdata_OmahaAuthGridHelper.TypeText("DiscPosAuthFees", code);

                executionLog.Log("EditAndDeleteOmahaAuthGrid", "Enter JCD Pos Auth Fees");
                corpMasterdata_OmahaAuthGridHelper.TypeText("JCDPosAuthFees", name);

                executionLog.Log("EditAndDeleteOmahaAuthGrid", "Enter Voice Auth Fees");
                corpMasterdata_OmahaAuthGridHelper.TypeText("VoiceAuthFees", code);

                executionLog.Log("EditAndDeleteOmahaAuthGrid", "Enter AVS Voice Fees");
                corpMasterdata_OmahaAuthGridHelper.TypeText("AVSVoiveFees", code);

                executionLog.Log("EditAndDeleteOmahaAuthGrid", "Enter AVS Voive Fees");
                corpMasterdata_OmahaAuthGridHelper.TypeText("ARUFees", name);

                executionLog.Log("EditAndDeleteOmahaAuthGrid", "  Click on Save button");
                corpMasterdata_OmahaAuthGridHelper.ClickElement("SaveEditAuth");

                executionLog.Log("EditAndDeleteOmahaAuthGrid", "Verify Corporate Master Omaha Auth Grid Updated Successfully.");
                corpMasterdata_OmahaAuthGridHelper.WaitForText("Master Omaha Auth Grid Updated Successfully.", 10);
                corpMasterdata_OmahaAuthGridHelper.WaitForWorkAround(4000);

                executionLog.Log("EditAndDeleteOmahaAuthGrid", "Click on delete");
                corpMasterdata_OmahaAuthGridHelper.ClickElement("ClickOnDelete");

                executionLog.Log("EditAndDeleteOmahaAuthGrid", "Accept Alert");
                corpMasterdata_OmahaAuthGridHelper.AcceptAlert();

                executionLog.Log("EditAndDeleteOmahaAuthGrid", "Verify Corporate Master Omaha Auth Grid Deleted Successfully.");
                corpMasterdata_OmahaAuthGridHelper.WaitForText("Corporate Master Omaha Auth Grid Deleted Successfully.", 30);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("EditAndDeleteOmahaAuthGrid");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Edit And Delete Omaha Auth Grid");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Edit And Delete Omaha Auth Grid", "Bug", "Medium", "Omaha Auth Grid page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Edit And Delete Omaha Auth Grid");
                        TakeScreenshot("EditAndDeleteOmahaAuthGrid");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditAndDeleteOmahaAuthGrid.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("EditAndDeleteOmahaAuthGrid");
                        string id = loginHelper.getIssueID("Edit And Delete Omaha Auth Grid");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditAndDeleteOmahaAuthGrid.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Edit And Delete Omaha Auth Grid"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Edit And Delete Omaha Auth Grid");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("EditAndDeleteOmahaAuthGrid");
                executionLog.WriteInExcel("Edit And Delete Omaha Auth Grid", Status, JIRA, "Corp Master Data");
            }
        }
    }
}