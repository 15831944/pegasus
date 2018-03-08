using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class EditAndDeleteMerchnatType : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Corp")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Corp")]
        public void editAndDeleteMerchnatType()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var corpMasterdata_MerchantTypeHelper = new CorpMasterdata_MerchantTypeHelper(GetWebDriver());

            username = oXMLData.getData("settings/Credentials", "username_corp");
            password = oXMLData.getData("settings/Credentials", "password");

            // Variable
            var name = "Test" + GetRandomNumber();
            var EditMerchnat = "Updated Merchnat" + RandomNumber(22, 999);
            var Test = "New" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("EditAndDeleteMerchnatType", "Login with valid credential  Username");
                Login(username[0], password[0]);

                executionLog.Log("EditAndDeleteMerchnatType", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("EditAndDeleteMerchnatType", "Click to Import");
                VisitCorp("masterdata/merchant_types");

                executionLog.Log("EditAndDeleteMerchnatType", "Verify Page title");
                VerifyTitle("Master Merchant Types");

                executionLog.Log("EditAndDeleteMerchnatType", "Click On Create");
                corpMasterdata_MerchantTypeHelper.ClickElement("Create");

                executionLog.Log("EditAndDeleteMerchnatType", "Verify Page title");
                VerifyTitle("Manage Master Merchant Types");

                executionLog.Log("EditAndDeleteMerchnatType", "Enter Merchant Type");
                corpMasterdata_MerchantTypeHelper.TypeText("MerchantType", name);

                executionLog.Log("EditAndDeleteMerchnatType", "Click On Save Btn");
                corpMasterdata_MerchantTypeHelper.ClickElement("Save");
                corpMasterdata_MerchantTypeHelper.WaitForWorkAround(1000);

                executionLog.Log("EditAndDeleteMerchnatType", "Search Merchnat Type");
                corpMasterdata_MerchantTypeHelper.TypeText("SearchMerchnatType", name);
                corpMasterdata_MerchantTypeHelper.WaitForWorkAround(4000);

                executionLog.Log("EditAndDeleteMerchnatType", "Click On Edit");
                corpMasterdata_MerchantTypeHelper.ClickElement("EditMerchnatType");
                corpMasterdata_MerchantTypeHelper.WaitForWorkAround(1000);

                executionLog.Log("EditAndDeleteMerchnatType", "Enter Merchant Type");
                corpMasterdata_MerchantTypeHelper.TypeText("MerchantType", EditMerchnat);

                executionLog.Log("EditAndDeleteMerchnatType", "Click on EDit Save");
                corpMasterdata_MerchantTypeHelper.ClickElement("SaveEdit");

                executionLog.Log("EditAndDeleteMerchnatType", "Verify Confirmation");
                corpMasterdata_MerchantTypeHelper.WaitForText("The merchant type is successfully updated!!", 30);
                corpMasterdata_MerchantTypeHelper.WaitForWorkAround(3000);

                executionLog.Log("EditAndDeleteMerchnatType", "Search Merchnat Type");
                corpMasterdata_MerchantTypeHelper.TypeText("SearchMerchnatType", EditMerchnat);
                corpMasterdata_MerchantTypeHelper.WaitForWorkAround(3000);

                executionLog.Log("EditAndDeleteMerchnatType", "Click Delete");
                corpMasterdata_MerchantTypeHelper.ClickElement("DeleteMerchnatType");

                executionLog.Log("EditAndDeleteMerchnatType", "Accept alert message.");
                corpMasterdata_MerchantTypeHelper.AcceptAlert();

                executionLog.Log("EditAndDeleteMerchnatType", "Verify Confirmtion");
                corpMasterdata_MerchantTypeHelper.WaitForText("The merchant type is successfully deleted!!", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("EditAndDeleteMerchnatType");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Edit And Delete Merchnat Type");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Edit And Delete Merchnat Type", "Bug", "Medium", "Merchant Type page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Edit And Delete Merchnat Type");
                        TakeScreenshot("EditAndDeleteMerchnatType");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditAndDeleteMerchnatType.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("EditAndDeleteMerchnatType");
                        string id = loginHelper.getIssueID("Edit And Delete Merchnat Type");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditAndDeleteMerchnatType.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Edit And Delete Merchnat Type"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Edit And Delete Merchnat Type");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("EditAndDeleteMerchnatType");
                executionLog.WriteInExcel("Edit And Delete Merchnat Type", Status, JIRA, "Corp Master Data");
            }
        }
    }
}