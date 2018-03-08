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
    public class EditAmexRates : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Corp")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Corp")]
        public void editAmexRates()
        {
            string[] username = null;
            string[] password = null;

            XMLParse oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_corp");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var corpMasterData_AmexRateHelper = new CorpMasterData_AmexRateHelper(GetWebDriver());

            // Variable
            var Num = RandomNumber(0, 9999).ToString();
            var Nam = GetRandomNumber().ToString();
            var name = GetRandomNumber().ToString();
            String JIRA = "";
            String Status = "Pass";

            try
            {

            executionLog.Log("EditAmexRates", "Login with valid credential  Username");
            Login(username[0], password[0]);

            executionLog.Log("EditAmexRates", "Verify Page title");
            VerifyTitle("Dashboard");
            Console.WriteLine("Redirected at Dashboard screen.");

            executionLog.Log("EditAmexRates", "Click to Import");
            VisitCorp("masterdata/amex_rates");

            executionLog.Log("EditAmexRates", "Verify Page title");
            VerifyTitle("Master Amex Rates");

            executionLog.Log("EditAmexRates", "Click On Create");
            corpMasterData_AmexRateHelper.ClickElement("Create");

            executionLog.Log("EditAmexRates", "Verify Page title");
            VerifyTitle("Manage Master Amex Rates");

            executionLog.Log("EditAmexRates", "Enter Mcc code");
            corpMasterData_AmexRateHelper.TypeText("MCCCode", Num);

            executionLog.Log("EditAmexRates", "Enter Amex Rate");
            corpMasterData_AmexRateHelper.TypeText("AmexRate", name);

            executionLog.Log("EditAmexRates", "Enter Amex Per Rate");
            corpMasterData_AmexRateHelper.TypeText("AmexPerItem", Nam);

            executionLog.Log("EditAmexRates", "Click On Save Btn");
            corpMasterData_AmexRateHelper.ClickElement("Save");

            executionLog.Log("EditAmexRates", "Wait for success message");
            corpMasterData_AmexRateHelper.WaitForText("The Amex Rates is successfully created!!", 10);

            executionLog.Log("EditAmexRates", "EnterMccCode Search code");
            corpMasterData_AmexRateHelper.TypeText("EnterMccCode", Num);
            corpMasterData_AmexRateHelper.WaitForWorkAround(3000);

            executionLog.Log("EditAmexRates", "Click on  Edit");
            corpMasterData_AmexRateHelper.ClickElement("Edit");

            executionLog.Log("EditAmexRates", "Verify Page title");
            VerifyTitle("Manage Master Amex Rates");

            executionLog.Log("EditAmexRates", "Enter Amex Rate");
            corpMasterData_AmexRateHelper.TypeText("AmexRate", name);

            executionLog.Log("EditAmexRates", "Enter Amex Per Rate");
            corpMasterData_AmexRateHelper.TypeText("AmexPerItem", Nam);

            executionLog.Log("EditAmexRates", "Click On Save Btn");
            corpMasterData_AmexRateHelper.ClickElement("Save");
            corpMasterData_AmexRateHelper.WaitForText("The Amex Rates is successfully updated!!", 10);

            executionLog.Log("EditAmexRates", "Go to amex rate page");
            VisitCorp("masterdata/amex_rates");

            executionLog.Log("EditAmexRates", "Enter Name to search");
            corpMasterData_AmexRateHelper.TypeText("EnterMccCode", Num);
            corpMasterData_AmexRateHelper.WaitForWorkAround(2000);

            executionLog.Log("EditAmexRates", "Click Delete btn  ");
            corpMasterData_AmexRateHelper.ClickElement("Delete");

            executionLog.Log("EditAmexRates", "Accept alert message. ");
            corpMasterData_AmexRateHelper.AcceptAlert();

            executionLog.Log("EditAmexRates", "Wait for delete message. ");
            corpMasterData_AmexRateHelper.WaitForText("The Amex Rates is successfully deleted!!", 10);


        }
  
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("EditAmexRates");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Edit Amex Rates");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Edit Amex Rates", "Bug", "Medium", "Amex page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Edit Amex Rates");
                        TakeScreenshot("EditAmexRates");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditAmexRates.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("EditAmexRates");
                        string id = loginHelper.getIssueID("Edit Amex Rates");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditAmexRates.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Edit Amex Rates"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Edit Amex Rates");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("EditAmexRates");
                executionLog.WriteInExcel("Edit Amex Rates", Status, JIRA, "Corp Master Data");
            }
        }
    }
}  