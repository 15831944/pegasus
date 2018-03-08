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
    public class CorpCreateOmahaAuthGrid : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Corp")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Corp")]
        public void corpCreateOmahaAuthGrid()
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
            var name = RandomNumber(1, 999).ToString();
            var code = RandomNumber(1, 999).ToString();
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("CreateOmahaAuthGrid", "Login with valid credential  Username");
                Login(username[0], password[0]);
                corpMasterdata_OmahaAuthGridHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateOmahaAuthGrid", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("CreateOmahaAuthGrid", "Redirect To URL");
                VisitCorp("masterdata/omaha_auth_grids");
                corpMasterdata_OmahaAuthGridHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateOmahaAuthGrid", "Verify title");
                VerifyTitle("Corporate Master Omaha Auth Grids");

                executionLog.Log("CreateOmahaAuthGrid", " Click On Create");
                corpMasterdata_OmahaAuthGridHelper.ClickElement("Create");
                corpMasterdata_OmahaAuthGridHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateOmahaAuthGrid", "Enter Grid Id");
                corpMasterdata_OmahaAuthGridHelper.TypeText("GridId", name);

                executionLog.Log("CreateOmahaAuthGrid", "Enter Visa Pos Authfees");
                corpMasterdata_OmahaAuthGridHelper.TypeText("VisaPosAuthfees", code);

                executionLog.Log("CreateOmahaAuthGrid", "Enter MC Pos Auth Fees");
                corpMasterdata_OmahaAuthGridHelper.TypeText("MCPosAuthFees", name);

                executionLog.Log("CreateOmahaAuthGrid", "Enter Amex Pos AuthFees");
                corpMasterdata_OmahaAuthGridHelper.TypeText("AmexPosAuthFees", name);

                executionLog.Log("CreateOmahaAuthGrid", "Enter Disc Pos Auth Fees");
                corpMasterdata_OmahaAuthGridHelper.TypeText("DiscPosAuthFees", code);

                executionLog.Log("CreateOmahaAuthGrid", "Enter JCD Pos Auth Fees");
                corpMasterdata_OmahaAuthGridHelper.TypeText("JCDPosAuthFees", name);

                executionLog.Log("CreateOmahaAuthGrid", "Enter Voice Auth Fees");
                corpMasterdata_OmahaAuthGridHelper.TypeText("VoiceAuthFees", code);

                executionLog.Log("CreateOmahaAuthGrid", "Enter AVS Electronic Fees");
                corpMasterdata_OmahaAuthGridHelper.TypeText("AVSElectronicFees", name);

                executionLog.Log("CreateOmahaAuthGrid", "Enter AVS Voice Fees");
                corpMasterdata_OmahaAuthGridHelper.TypeText("AVSVoiveFees", code);

                executionLog.Log("CreateOmahaAuthGrid", "Enter AVS Voive Fees");
                corpMasterdata_OmahaAuthGridHelper.TypeText("ARUFees", name);

                executionLog.Log("CreateOmahaAuthGrid", "Click on Save button");
                corpMasterdata_OmahaAuthGridHelper.ClickElement("Save");

                executionLog.Log("CreateOmahaAuthGrid", "Verify Page Text");
                corpMasterdata_OmahaAuthGridHelper.WaitForText("Master Omaha Auth Grid Created Successfully.", 10);
                corpMasterdata_OmahaAuthGridHelper.WaitForWorkAround(2000);

                executionLog.Log("CreateOmahaAuthGrid", "Enter Name to search");
                corpMasterdata_OmahaAuthGridHelper.TypeText("GridIdSearch", name);
                corpMasterdata_OmahaAuthGridHelper.WaitForWorkAround(2000);

                executionLog.Log("CreateOmahaAuthGrid", "Click Delete btn  ");
                corpMasterdata_OmahaAuthGridHelper.ClickElement("ClickOnDelete");

                executionLog.Log("CreateOmahaAuthGrid", "Accept alert message. ");
                corpMasterdata_OmahaAuthGridHelper.AcceptAlert();

                executionLog.Log("CreateOmahaAuthGrid", "Wait for delete message. ");
                corpMasterdata_OmahaAuthGridHelper.WaitForText("Corporate Master Omaha Auth Grid Deleted Successfully.", 10);
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
                        loginHelper.CreateIssue("Create Omaha AuthGrid", "Bug", "Medium", "Omha AuthGrid", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
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
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("CreateOmahaAuthGrid");
                executionLog.WriteInExcel("Create Omaha AuthGrid", Status, JIRA, "Corp Master Data");
            }
        }
    }
}
