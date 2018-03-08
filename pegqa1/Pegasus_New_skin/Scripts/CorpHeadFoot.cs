using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;
using System.Diagnostics;
namespace Pegasus_New_skin.Scripts
{
    [TestClass]
    public class CorpHeadFoot : DriverTestCase
    {
        ExecutionLog executionLog = new ExecutionLog();

        string[] username = null;
        string[] password = null;
        //define a array which includes the path for every pages.
        string[] corpArray =
        {   "HomeTab","EmployeeTab","OfficesTab+Offices","OfficesTab+Users","OfficesTab+OfficeCodes",
            "ResidualIncomeTab+Imports","ResidualIncomeTab+Payouts+PayoutSummary","ResidualIncomeTab+Payouts+DetailedPayouts","ResidualIncomeTab+Payouts+Reports",
            "ResidualIncomeTab+MasterData+RevenueShareSetup","ResidualIncomeTab+MasterData+AdjustmentsTool","ResidualIncomeTab+MasterData+OfficeLookupCodes",
            "MerchantsTab",
            "StatisticsTab+UserStatistics","StatisticsTab+UsageStatistics",
            "MasterDataTab+RatesAndFees","MasterDataTab+AmexRates","MasterDataTab+MerchantTypes","MasterDataTab+PricingPlans","MasterDataTab+Processors","MasterDataTab+OmahaAuthGrids","MasterDataTab+UserLimits","MasterDataTab+Languages",
            "SystemTab+Avatars","SystemTab+Modules","SystemTab+Themes","SystemTab+Picklists","SystemTab+Settings","SystemTab+EMailTemplates","SystemTab+AuditTrail",
            "PDFTemplatesTab+PDFImportWizard","PDFTemplatesTab+PDFTemplates","PDFTemplatesTab+PDFCategories",
            "FieldDictionaryTab+Tabs","FieldDictionaryTab+Sections","FieldDictionaryTab+Fields+FieldProperties","FieldDictionaryTab+Fields+FieldsOrder","FieldDictionaryTab+FieldGroupingTemplates",
            "IframeAppsTab"
        };
        //common layout of head
        string[] corpHead = { "HeadImage", "Header", "HeadDropDown", "HeadText" };

       CorpHelper corpHelper = null;
        [TestInitialize]
        public void Initialize()
        {
            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username2");
            password = oXMLData.getData("settings/Credentials", "password2");
            corpHelper = new CorpHelper(GetWebDriver());


        }
        //parse the input path array and then go to each page,separate the strings by '+', 
        //mouseover the elements in each string, and click the last element
        public void iterHelper(string[] inArray)
        {
     
            int len = inArray.Length;
            int curlen;
            string[] current = null;
            for (int i = 0; i < len; i++)
            {
                executionLog.Log("CorpHeadFoot", inArray[i]);
                current = inArray[i].Split('+');
                curlen = current.Length;
                for (int j = 0; j < curlen - 1; j++)
                {

                    corpHelper.MouseHover(current[j]);
                    corpHelper.WaitForWorkAround(2000);
                }


                corpHelper.ClickElement(current[curlen - 1]);
                corpHelper.WaitForWorkAround(3000);
                //check the common layout for each page
                foreach (string element in corpHead)
                {
                    Assert.IsTrue(corpHelper.ElementVisible(element));
                }

            }
           
        }
        [TestMethod][TestCategory("All")]
        public void TestCorp()
        {
            var loginHelper = new LoginHelper(GetWebDriver());
            var usernme = "Sysprins" + RandomNumber(44, 799999977);
            var name = "Test" + GetRandomNumber();
            string JIRA = "";
            string Status = "Pass";
            //try
            //{
                Login(username[0], password[0]);
                corpHelper.WaitForWorkAround(6000);
                iterHelper(corpArray);
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine("ERRROROOR");
            //    executionLog.Log("Error", e.StackTrace);
            //    Status = "Fail";

            //    String counter = executionLog.readLastLine("counter");
            //    String Description = executionLog.GetAllTextFile("CorpHeadFoot");
            //    String Error = executionLog.GetAllTextFile("Error");
            //    if (counter == "")
            //    {
            //        counter = "0";
            //    }
            //    bool result = loginHelper.CheckExstingIssue("CorpHeadFoot");
            //    if (!result)
            //    {
            //        if (Int16.Parse(counter) < 5)
            //        {
            //            executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
            //            loginHelper.CreateIssue("CorpHeadFoot", "Bug", "Medium", "Office page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
            //            string id = loginHelper.getIssueID("CorpHeadFoot");
            //            TakeScreenshot("CorpHeadFoot");
            //            string directoryName = loginHelper.GetnewDirectoryName(GetPath());
            //            var location = directoryName + "\\CorpHeadFoot";
            //            loginHelper.AddAttachment(location, id);
            //        }
            //    }
            //    else
            //    {
            //        if (Int16.Parse(counter) < 5)
            //        {
            //            executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
            //            TakeScreenshot("CorpHeadFoot");
            //            string id = loginHelper.getIssueID("CorpHeadFoot");
            //            string directoryName = loginHelper.GetnewDirectoryName(GetPath());
            //            var location = directoryName + "\\CorpHeadFoot";
            //            loginHelper.AddAttachment(location, id);
            //            loginHelper.AddComment(loginHelper.getIssueID("CorpHeadFoot"), "This issue is still occurring");
            //        }
            //    }
            //    JIRA = loginHelper.getIssueID("CorpHeadFoot");
            //    executionLog.DeleteFile("Error");
            //    throw;

            //}
            //finally
            //{
            //    executionLog.DeleteFile("CorpHeadFoot");
            //    executionLog.WriteInExcel("CorpHeadFoot", Status, JIRA, "Office");
            //}

        }
    }
}
