using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class AmexRateCorp : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin")]
        [TestCategory("Fail")]
        [TestCategory("TS8")]
        [TestCategory("Pegasus_NewSkin")]
        public void amexRateCorp()
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
            var corpMasterData_AmexRateHelper = new CorpMasterData_AmexRateHelper(GetWebDriver());

            // Variable
            var Num = "1" + RandomNumber(9, 999);
            var Nam = "2" + RandomNumber(1, 999);
            var name = "3" + RandomNumber(1, 999);
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("AmexRateCorp", "Login with valid credential  Username");
                Login(username[0], password[0]);

                executionLog.Log("AmexRateCorp", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("AmexRateCorp", "Go to amex page");
                VisitCorp("masterdata/amex_rates");
                corpMasterData_AmexRateHelper.WaitForWorkAround(5000);

                executionLog.Log("AmexRateCorp", "Verify Page title");
                VerifyTitle("Master Amex Rates");

                executionLog.Log("AmexRateCorp", "Click On Create");
                corpMasterData_AmexRateHelper.ClickElement("Create");
                corpMasterData_AmexRateHelper.WaitForWorkAround(3000);

                executionLog.Log("AmexRateCorp", "Verify Page title");
                VerifyTitle("Manage Master Amex Rates");
                corpMasterData_AmexRateHelper.WaitForWorkAround(3000);

                executionLog.Log("AmexRateCorp", "Enter Processor name");
                corpMasterData_AmexRateHelper.TypeText("MCCCode", Num);

                executionLog.Log("AmexRateCorp", "Enter ProcessorCode");
                corpMasterData_AmexRateHelper.TypeText("AmexRate", name);

                executionLog.Log("AmexRateCorp", "Enter Amex Per Rate");
                corpMasterData_AmexRateHelper.TypeText("AmexPerItem", Nam);

                executionLog.Log("AmexRateCorp", "Click On Save Btn");
                corpMasterData_AmexRateHelper.ClickElement("Save");
                corpMasterData_AmexRateHelper.WaitForWorkAround(5000);

                executionLog.Log("AmexRateCorp", "Verify Text The Amex Rates is successfully created!!");
                corpMasterData_AmexRateHelper.WaitForText("The Amex Rates is successfully created!!", 5);
                corpMasterData_AmexRateHelper.WaitForWorkAround(3000);

                executionLog.Log("AmexRateCorp", "Search with  MCC Codes");
                corpMasterData_AmexRateHelper.TypeText("SecrhMCCCodes", Num);
                corpMasterData_AmexRateHelper.WaitForWorkAround(4000);

                executionLog.Log("AmexRateCorp", "Click on Edit icon");
                corpMasterData_AmexRateHelper.ClickElement("Edit");
                corpMasterData_AmexRateHelper.WaitForWorkAround(5000);

                executionLog.Log("AmexRateCorp", "Verify Page title");
                VerifyTitle("Manage Master Amex Rates");
                corpMasterData_AmexRateHelper.WaitForWorkAround(3000);

                executionLog.Log("AmexRateCorp", "Enter ProcessorCode");
                corpMasterData_AmexRateHelper.TypeText("AmexRate", name);

                executionLog.Log("AmexRateCorp", "Enter Amex Per Rate");
                corpMasterData_AmexRateHelper.TypeText("AmexPerItem", Nam);

                executionLog.Log("AmexRateCorp", "Click on Save button.");
                corpMasterData_AmexRateHelper.ClickElement("Save");
                corpMasterData_AmexRateHelper.WaitForWorkAround(5000);

                executionLog.Log("AmexRateCorp", "Verify The Amex Rates is successfully updated!!");
                corpMasterData_AmexRateHelper.WaitForText("The Amex Rates is successfully updated!!", 5);
                corpMasterData_AmexRateHelper.WaitForWorkAround(3000);

                executionLog.Log("AmexRateCorp", "Search with  MCC Codes");
                corpMasterData_AmexRateHelper.TypeText("SecrhMCCCodes", Num);
                corpMasterData_AmexRateHelper.WaitForWorkAround(3000);

                executionLog.Log("AmexRateCorp", "Click on delete");
                corpMasterData_AmexRateHelper.ClickElement("Delete");
                corpMasterData_AmexRateHelper.WaitForWorkAround(3000);

                executionLog.Log("AmexRateCorp", "Accept Alert To Confirmm");
                corpMasterData_AmexRateHelper.AcceptAlert();
                corpMasterData_AmexRateHelper.WaitForWorkAround(5000);

                executionLog.Log("AmexRateCorp", "Confirmation delete message");
                corpMasterData_AmexRateHelper.WaitForText("The Amex Rates is successfully deleted!!", 5);
                corpMasterData_AmexRateHelper.WaitForWorkAround(3000);
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("AmexRateCorp");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Amex Rate Corp");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Amex Rate Corp", "Bug", "Medium", "Amex page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Amex Rate Corp");
                        TakeScreenshot("AmexRateCorp");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AmexRateCorp.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("AmexRateCorp");
                        string id = loginHelper.getIssueID("Amex Rate Corp");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AmexRateCorp.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Amex Rate Corp"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Amex Rate Corp");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("AmexRateCorp");
                executionLog.WriteInExcel("Amex Rate Corp", Status, JIRA, "Corp Master Data");
            }
        }
    }
}
