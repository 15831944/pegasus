using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class CreateAmexRates : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Corp")]
        [TestCategory("Pegasus_Corp")]
        public void createAmexRates()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var corpMasterData_AmexRateHelper = new CorpMasterData_AmexRateHelper(GetWebDriver());

            username = oXMLData.getData("settings/Credentials", "username_corp");
            password = oXMLData.getData("settings/Credentials", "password");

            // Variable
            var Num = RandomNumber(1111, 9999).ToString();
            var Nam = GetRandomNumber();
            var name = GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("CreateAmexRates", "Login with valid credential  Username");
                Login(username[0], password[0]);

                executionLog.Log("CreateAmexRates", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("CreateAmexRates", "Go to amex rate page");
                VisitCorp("masterdata/amex_rates");
                corpMasterData_AmexRateHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateAmexRates", "Verify title");
                VerifyTitle("Master Amex Rates");

                executionLog.Log("CreateAmexRates", "Click On Create");
                corpMasterData_AmexRateHelper.ClickElement("Create");
                corpMasterData_AmexRateHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateAmexRates", "Verify title");
                VerifyTitle("Manage Master Amex Rates");

                executionLog.Log("CreateAmexRates", "Enter Processor name");
                corpMasterData_AmexRateHelper.TypeText("MCCCode", Num);
                //corpMasterData_AmexRateHelper.WaitForWorkAround(1000);

                executionLog.Log("CreateAmexRates", "Enter ProcessorCode");
                corpMasterData_AmexRateHelper.TypeText("AmexRate", name);
                //corpMasterData_AmexRateHelper.WaitForWorkAround(1000);

                executionLog.Log("CreateAmexRates", "Enter Amex Per Rate");
                corpMasterData_AmexRateHelper.TypeText("AmexPerItem", Nam);
                //corpMasterData_AmexRateHelper.WaitForWorkAround(1000);

                executionLog.Log("CreateAmexRates", "Click On Save Btn");
                corpMasterData_AmexRateHelper.ClickElement("Save");
                corpMasterData_AmexRateHelper.WaitForText("The Amex Rates is successfully created!!", 30);

                executionLog.Log("CreateAmexRates", "Go to amex rate page");
                VisitCorp("masterdata/amex_rates");
                corpMasterData_AmexRateHelper.WaitForWorkAround(4000);

                executionLog.Log("CreateAmexRates", "Enter Name to search");
                corpMasterData_AmexRateHelper.TypeText("EnterMccCode", Num);
                corpMasterData_AmexRateHelper.WaitForWorkAround(2000);

                executionLog.Log("CreateAmexRates", "Click Delete btn  ");
                corpMasterData_AmexRateHelper.ClickElement("Delete");
                corpMasterData_AmexRateHelper.WaitForWorkAround(1000);

                executionLog.Log("CreateAmexRates", "Accept alert message. ");
                corpMasterData_AmexRateHelper.AcceptAlert();

                executionLog.Log("CreateAmexRates", "Wait for delete message. ");
                corpMasterData_AmexRateHelper.WaitForText("The Amex Rates is successfully deleted!!", 10);


            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CreateAmexRates");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Create Amex Rates");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Create Amex Rates", "Bug", "Medium", "Amex rate page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Create Amex Rates");
                        TakeScreenshot("CreateAmexRates");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateAmexRates.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CreateAmexRates");
                        string id = loginHelper.getIssueID("Create Amex Rates");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateAmexRates.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Create Amex Rates"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Create Amex Rates");
                executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("CreateAmexRates");
                executionLog.WriteInExcel("Create Amex Rates", Status, JIRA, "Corp Master Data");
            }
        }
    }
}