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
    public class OmahaAuthGridCorp : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin")]
        [TestCategory("Temp")]
        [TestCategory("TS8")]
        [TestCategory("Pegasus_NewSkin")]
        public void omahaAuthGridCorp()
        {
            string[] username = null;
            string[] password = null;
            XMLParse oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            // Initializing the objects
            var loginHelper = new LoginHelper(GetWebDriver());
            var executionLog = new ExecutionLog(); ;
            var corpMasterdata_OmahaAuthGridHelper = new CorpMasterdata_OmahaAuthGridHelper(GetWebDriver());
            username = oXMLData.getData("settings/Credentials", "username_corp");
            password = oXMLData.getData("settings/Credentials", "password");

            // Variable
            var name = "2" + RandomNumber(1111, 9999);
            var code = "1" + RandomNumber(1111, 9999);
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("OmahaAuthGridCorp", "Login with valid credential  Username");
                Login(username[0], password[0]);

                executionLog.Log("OmahaAuthGridCorp", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("OmahaAuthGridCorp", "Redirect To URL");
                VisitCorp("masterdata/omaha_auth_grids");

                executionLog.Log("OmahaAuthGridCorp", "Verify Page title");
                VerifyTitle("Corporate Master Omaha Auth Grids");

                executionLog.Log("OmahaAuthGridCorp", " Click On Create");
                corpMasterdata_OmahaAuthGridHelper.ClickElement("Create");
                corpMasterdata_OmahaAuthGridHelper.WaitForWorkAround(3000);

                executionLog.Log("OmahaAuthGridCorp", "Enter Grid Id");
                corpMasterdata_OmahaAuthGridHelper.TypeText("GridId", name);

                executionLog.Log("OmahaAuthGridCorp", "Enter Visa Pos Authfees");
                corpMasterdata_OmahaAuthGridHelper.TypeText("VisaPosAuthfees", code);

                executionLog.Log("OmahaAuthGridCorp", "Enter MC Pos Auth Fees");
                corpMasterdata_OmahaAuthGridHelper.TypeText("MCPosAuthFees", name);

                executionLog.Log("OmahaAuthGridCorp", "Enter Amex Pos AuthFees");
                corpMasterdata_OmahaAuthGridHelper.TypeText("AmexPosAuthFees", name);

                executionLog.Log("OmahaAuthGridCorp", "Enter Disc Pos Auth Fees");
                corpMasterdata_OmahaAuthGridHelper.TypeText("DiscPosAuthFees", code);

                executionLog.Log("OmahaAuthGridCorp", "Enter JCD Pos Auth Fees");
                corpMasterdata_OmahaAuthGridHelper.TypeText("JCDPosAuthFees", name);

                executionLog.Log("OmahaAuthGridCorp", "Enter Voice Auth Fees");
                corpMasterdata_OmahaAuthGridHelper.TypeText("VoiceAuthFees", code);

                executionLog.Log("OmahaAuthGridCorp", "Enter AVS Electronic Fees");
                corpMasterdata_OmahaAuthGridHelper.TypeText("AVSElectronicFees", name);

                executionLog.Log("OmahaAuthGridCorp", "Enter AVS Voice Fees");
                corpMasterdata_OmahaAuthGridHelper.TypeText("AVSVoiveFees", code);

                executionLog.Log("OmahaAuthGridCorp", "Enter AVS Voive Fees");
                corpMasterdata_OmahaAuthGridHelper.TypeText("ARUFees", name);

                executionLog.Log("OmahaAuthGridCorp", "  Click on Save button");
                corpMasterdata_OmahaAuthGridHelper.ClickElement("Save");

                executionLog.Log("OmahaAuthGridCorp", "Wait for confirmation Text");
                corpMasterdata_OmahaAuthGridHelper.WaitForText("Master Omaha Auth Grid Created Successfully.", 10);

                executionLog.Log("OmahaAuthGridCorp", "Search GripIdSrch");
                corpMasterdata_OmahaAuthGridHelper.TypeText("GridIdSearch", name);
                corpMasterdata_OmahaAuthGridHelper.WaitForWorkAround(3000);

                executionLog.Log("OmahaAuthGridCorp", "Click on  Edit");
                corpMasterdata_OmahaAuthGridHelper.ClickElement("ClickEditAuth");
                corpMasterdata_OmahaAuthGridHelper.WaitForWorkAround(3000);

                executionLog.Log("OmahaAuthGridCorp", "Enter Grid Id");
                corpMasterdata_OmahaAuthGridHelper.TypeText("GridId", name);

                executionLog.Log("OmahaAuthGridCorp", "Enter Visa Pos Authfees");
                corpMasterdata_OmahaAuthGridHelper.TypeText("VisaPosAuthfees", code);

                executionLog.Log("OmahaAuthGridCorp", "Enter MC Pos Auth Fees");
                corpMasterdata_OmahaAuthGridHelper.TypeText("MCPosAuthFees", name);

                executionLog.Log("OmahaAuthGridCorp", "Enter Amex Pos AuthFees");
                corpMasterdata_OmahaAuthGridHelper.TypeText("AmexPosAuthFees", name);

                executionLog.Log("OmahaAuthGridCorp", "Enter Disc Pos Auth Fees");
                corpMasterdata_OmahaAuthGridHelper.TypeText("DiscPosAuthFees", code);

                executionLog.Log("OmahaAuthGridCorp", "Enter JCD Pos Auth Fees");
                corpMasterdata_OmahaAuthGridHelper.TypeText("JCDPosAuthFees", name);

                executionLog.Log("OmahaAuthGridCorp", "Enter Voice Auth Fees");
                corpMasterdata_OmahaAuthGridHelper.TypeText("VoiceAuthFees", code);

                executionLog.Log("OmahaAuthGridCorp", "Enter AVS Voice Fees");
                corpMasterdata_OmahaAuthGridHelper.TypeText("AVSVoiveFees", code);

                executionLog.Log("OmahaAuthGridCorp", "Enter AVS Voive Fees");
                corpMasterdata_OmahaAuthGridHelper.TypeText("ARUFees", name);

                executionLog.Log("OmahaAuthGridCorp", "  Click on Save button");
                corpMasterdata_OmahaAuthGridHelper.ClickElement("SaveEditAuth");

                executionLog.Log("OmahaAuthGridCorp", "Verify Corporate Master Omaha Auth Grid Updated Successfully.");
                corpMasterdata_OmahaAuthGridHelper.WaitForText("Master Omaha Auth Grid Updated Successfully.", 10);

                executionLog.Log("OmahaAuthGridCorp", "Click on delete");
                corpMasterdata_OmahaAuthGridHelper.ClickElement("ClickOnDelete");

                executionLog.Log("OmahaAuthGridCorp", "Accept Alert");
                corpMasterdata_OmahaAuthGridHelper.AcceptAlert();

                executionLog.Log("OmahaAuthGridCorp", "Verify Corporate Master Omaha Auth Grid Deleted Successfully.");
                corpMasterdata_OmahaAuthGridHelper.WaitForText("Corporate Master Omaha Auth Grid Deleted Successfully.", 30);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("OmahaAuthGridCorp");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Omaha Auth Grid Corp");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Omaha Auth Grid Corp", "Bug", "Medium", "Corp Omaha page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Omaha Auth Grid Corp");
                        TakeScreenshot("OmahaAuthGridCorp");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\OmahaAuthGridCorp.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("OmahaAuthGridCorp");
                        string id = loginHelper.getIssueID("Omaha Auth Grid Corp");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\OmahaAuthGridCorp.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Omaha Auth Grid Corp"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Omaha Auth Grid Corp");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("OmahaAuthGridCorp");
                executionLog.WriteInExcel("Omaha Auth Grid Corp", Status, JIRA, "Corp Master Data");
            }
        }
    }
}