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
    public class OmahaAuthGridPushToOffice : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Corp")]
        [TestCategory("TS8")]
        [TestCategory("Pegasus_Corp")]
        public void omahaAuthGridPushToOffice()
        {
            string[] username = null;
            string[] username1 = null;
            string[] password = null;

            XMLParse oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var corpMasterdata_OmahaAuthGridHelper = new CorpMasterdata_OmahaAuthGridHelper(GetWebDriver());
            var masterData_OmahaAuthGridHelper = new MasterData_OmahaAuthGridHelper(GetWebDriver());

            username = oXMLData.getData("settings/Credentials", "username_corp");
            username1 = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            //Variable
            String name = RandomNumber(1, 999).ToString();
            String code = RandomNumber(1, 999).ToString();
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("CreateOmahaAuthGrid", "Login with valid credential  Username");
                Login(username[0], password[0]);
               
                executionLog.Log("CreateOmahaAuthGrid", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("CreateOmahaAuthGrid", "Redirect To URL");
                VisitCorp("masterdata/omaha_auth_grids");
                corpMasterdata_OmahaAuthGridHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateOmahaAuthGrid", "Verify Page title");
                VerifyTitle("Corporate Master Omaha Auth Grids");

                executionLog.Log("CreateOmahaAuthGrid", " Click On Create");
                corpMasterdata_OmahaAuthGridHelper.ClickElement("Create");
                corpMasterdata_OmahaAuthGridHelper.WaitForWorkAround(2000);

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

                executionLog.Log("OmahaAuthGridPushToOffice", "Push To Office");
                corpMasterdata_OmahaAuthGridHelper.ClickElement("PushToOffice");

                executionLog.Log("OmahaAuthGridPushToOffice", "Click ok To Confirm");
                corpMasterdata_OmahaAuthGridHelper.AcceptAlert();

                executionLog.Log("OmahaAuthGridPushToOffice", "Verify Confirmation Languges Successfully Pushed to Offices.");
                corpMasterdata_OmahaAuthGridHelper.WaitForText("Omaha Auth Grids successfully pushed to offices.", 40);

                executionLog.Log("OmahaAuthGridPushToOffice", "logout from the application");
                VisitCorp("logout");
                //corpMasterdata_OmahaAuthGridHelper.WaitForWorkAround(7000);

                executionLog.Log("OmahaAuthGridPushToOffice", "Login using office credentials");
                Login(username1[0], password[0]);
                corpMasterdata_OmahaAuthGridHelper.WaitForWorkAround(3000);

                if (GetWebDriver().Title == "Login")

                {
                    Login(username1[0], password[0]);
                }

                VerifyTitle("Dashboard");

                executionLog.Log("OmahaAuthGridPushToOffice", "Redirect to Procesosr");
                VisitOffice("omaha_auth_grids");
                corpMasterdata_OmahaAuthGridHelper.WaitForWorkAround(3000);

                executionLog.Log("OmahaAuthGridPushToOffice", "Verify page title.");
                VerifyTitle("Omaha Auth Grids");
               
                executionLog.Log("OmahaAuthGridPushToOffice", "Search processor using gridId");
                masterData_OmahaAuthGridHelper.TypeText("SearchGridId", name);
                corpMasterdata_OmahaAuthGridHelper.WaitForWorkAround(2000);

                executionLog.Log("OmahaAuthGridPushToOffice", "Verify text on page.");
                corpMasterdata_OmahaAuthGridHelper.VerifyPageText(name);
                
                executionLog.Log("AmexRatesPushToOffice", "Logout button");
                VisitOffice("logout");

                executionLog.Log("AmexRatesPushToOffice", "Login with valid credential");
                Login(username[0], password[0]);

                executionLog.Log("AmexRatesPushToOffice", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("CreateOmahaAuthGrid", "Redirect To URL");
                VisitCorp("masterdata/omaha_auth_grids");
                corpMasterdata_OmahaAuthGridHelper.WaitForWorkAround(3000);

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
                String Description = executionLog.GetAllTextFile("OmahaAuthGridPushToOffice");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Omaha Auth Grid Push To Office");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Omaha Auth Grid Push To Office", "Bug", "Medium", "Omaha Grid page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Omaha Auth Grid Push To Office");
                        TakeScreenshot("OmahaAuthGridPushToOffice");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\OmahaAuthGridPushToOffice.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("OmahaAuthGridPushToOffice");
                        string id = loginHelper.getIssueID("Omaha Auth Grid Push To Office");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\OmahaAuthGridPushToOffice.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Omaha Auth Grid Push To Office"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Omaha Auth Grid Push To Office");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("OmahaAuthGridPushToOffice");
                executionLog.WriteInExcel("Omaha Auth Grid Push To Office", Status, JIRA, "Corp Master Data");
            }
        }
    }
}


